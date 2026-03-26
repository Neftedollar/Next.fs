#!/usr/bin/env node

/**
 * nextfs-scan.mjs
 *
 * Scans F# source files referenced by a .fsproj for [<NextFsEntry(...)>] and
 * [<NextFsStaticExport(...)>] attributes and generates nextfs.entries.json.
 *
 * Usage:
 *   node tools/nextfs-scan.mjs <path/to/Project.fsproj>
 *
 * The generated nextfs.entries.json is written next to the package.json
 * (one level up from the directory containing the .fsproj, or in the same
 * directory when package.json is colocated with the .fsproj).
 */

import fs from "node:fs/promises";
import path from "node:path";
import process from "node:process";

// ---------------------------------------------------------------------------
// Attribute parsers
// ---------------------------------------------------------------------------

/**
 * Extract the first positional string argument from an F# attribute args string.
 * e.g. `"app/layout.js", Default="Layout"` → `"app/layout.js"`
 */
function parseFirstStringArg(argsStr) {
  const match = argsStr.match(/^\s*"([^"]*)"/);
  return match ? match[1] : null;
}

/**
 * Extract a named string property from an F# attribute args string.
 * e.g. `Default="Layout"` given name `Default` → `"Layout"`
 */
function parseNamedString(argsStr, name) {
  const regex = new RegExp(`\\b${name}\\s*=\\s*"([^"]*)"`);
  const match = argsStr.match(regex);
  return match ? match[1] : null;
}

/**
 * Extract a named bool property from an F# attribute args string.
 * e.g. `ExportAll=true` given name `ExportAll` → `true`
 */
function parseNamedBool(argsStr, name) {
  const regex = new RegExp(`\\b${name}\\s*=\\s*(true|false)`);
  const match = argsStr.match(regex);
  return match ? match[1] === "true" : false;
}

/**
 * Find all [<NextFsEntry(...)>] attributes in an F# source string
 * and return parsed entry descriptors.
 */
function parseNextFsEntries(source) {
  const results = [];
  // [\s\S]*? — non-greedy, handles multi-line attribute args
  const entryRegex = /\[<(?:NextFs\.)?NextFsEntry\(([\s\S]*?)\)>\]/g;
  let match;

  while ((match = entryRegex.exec(source)) !== null) {
    const args = match[1];
    const output = parseFirstStringArg(args);
    if (!output) continue;

    results.push({
      output,
      directive: parseNamedString(args, "Directive") || undefined,
      default: parseNamedString(args, "Default") || undefined,
      named: parseNamedString(args, "Named") || undefined,
      exportAll: parseNamedBool(args, "ExportAll"),
    });
  }

  return results;
}

/**
 * Find all [<NextFsStaticExport("name", """json""")>] attributes in an F# source string.
 * Both triple-quoted (""") and regular-quoted strings are supported for the JSON arg.
 */
function parseStaticExports(source) {
  const results = [];

  // Triple-quoted JSON: [<NextFsStaticExport("name", """...""")>]
  const tripleRegex = /\[<(?:NextFs\.)?NextFsStaticExport\(\s*"([^"]+)"\s*,\s*"""([\s\S]*?)"""\s*\)>\]/g;
  let match;
  while ((match = tripleRegex.exec(source)) !== null) {
    try {
      results.push({ name: match[1], value: JSON.parse(match[2]) });
    } catch {
      console.warn(`Warning: Could not parse JSON for static export "${match[1]}"`);
    }
  }

  // Regular-quoted JSON: [<NextFsStaticExport("name", "...")>]
  const singleRegex = /\[<(?:NextFs\.)?NextFsStaticExport\(\s*"([^"]+)"\s*,\s*"([^"]*)"\s*\)>\]/g;
  while ((match = singleRegex.exec(source)) !== null) {
    try {
      results.push({ name: match[1], value: JSON.parse(match[2]) });
    } catch {
      console.warn(`Warning: Could not parse JSON for static export "${match[1]}"`);
    }
  }

  return results;
}

// ---------------------------------------------------------------------------
// .fsproj reader
// ---------------------------------------------------------------------------

/**
 * Parse a .fsproj file and return the list of <Compile Include="..."> values
 * (forward-slash normalised, relative to the .fsproj directory).
 */
async function parseFsproj(fsprojPath) {
  const content = await fs.readFile(fsprojPath, "utf8");
  const files = [];
  const regex = /<Compile\s+Include="([^"]+)"/g;
  let match;
  while ((match = regex.exec(content)) !== null) {
    files.push(match[1].replace(/\\/g, "/"));
  }
  return files;
}

// ---------------------------------------------------------------------------
// Main
// ---------------------------------------------------------------------------

async function main() {
  const [fsprojArg] = process.argv.slice(2);

  if (!fsprojArg) {
    console.error("Usage: node tools/nextfs-scan.mjs <path/to/Project.fsproj>");
    process.exitCode = 1;
    return;
  }

  const fsprojPath = path.resolve(process.cwd(), fsprojArg);
  const fsprojDir = path.dirname(fsprojPath);

  // The output directory for nextfs.entries.json is where package.json lives.
  // Walk upwards from fsprojDir until we find a package.json (max 2 levels).
  let outputDir = fsprojDir;
  for (const candidate of [fsprojDir, path.dirname(fsprojDir)]) {
    const hasPackageJson = await fs
      .access(path.join(candidate, "package.json"))
      .then(() => true)
      .catch(() => false);
    if (hasPackageJson) {
      outputDir = candidate;
      break;
    }
  }

  const compileFiles = await parseFsproj(fsprojPath);
  const entries = [];

  for (const includeValue of compileFiles) {
    const fsFilePath = path.join(fsprojDir, includeValue);
    let source;

    try {
      source = await fs.readFile(fsFilePath, "utf8");
    } catch {
      console.warn(`Warning: Could not read ${fsFilePath}`);
      continue;
    }

    const entryAttrs = parseNextFsEntries(source);
    if (entryAttrs.length === 0) continue;

    const staticExportAttrs = parseStaticExports(source);

    // Derive the Fable output path from the source file's include path.
    // Convention: .fable/<include-path-with-.fs-replaced-by-.js>
    const fromPath = "./.fable/" + includeValue.replace(/\.fs$/, ".js");

    for (const entryAttr of entryAttrs) {
      const entry = {
        from: fromPath,
        to: "./" + entryAttr.output,
      };

      if (entryAttr.directive) {
        entry.directive = entryAttr.directive;
      }

      if (entryAttr.default) {
        entry.defaultFromNamed = entryAttr.default;
      }

      if (entryAttr.named) {
        entry.named = entryAttr.named.trim().split(/\s+/).filter(Boolean);
      }

      if (entryAttr.exportAll) {
        entry.exportAll = true;
      }

      if (staticExportAttrs.length > 0) {
        entry.staticExports = {};
        for (const se of staticExportAttrs) {
          entry.staticExports[se.name] = se.value;
        }
      }

      entries.push(entry);
    }
  }

  if (entries.length === 0) {
    console.error(
      "No [<NextFsEntry(...)>] attributes found. " +
        "Add NextFsEntry attributes to your F# modules and re-run."
    );
    process.exitCode = 1;
    return;
  }

  const outputPath = path.join(outputDir, "nextfs.entries.json");
  const output = JSON.stringify({ entries }, null, 2) + "\n";

  await fs.writeFile(outputPath, output, "utf8");
  console.log(
    `nextfs-scan: wrote ${entries.length} ${entries.length === 1 ? "entry" : "entries"} → ${path.relative(process.cwd(), outputPath)}`
  );
}

main().catch((error) => {
  console.error(error.message);
  process.exitCode = 1;
});
