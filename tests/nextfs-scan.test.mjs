import assert from "node:assert/strict";
import { spawn } from "node:child_process";
import { mkdtemp, mkdir, readFile, writeFile } from "node:fs/promises";
import { tmpdir } from "node:os";
import path from "node:path";
import test from "node:test";
import { fileURLToPath } from "node:url";

const repoRoot = path.resolve(path.dirname(fileURLToPath(import.meta.url)), "..");
const scannerPath = path.join(repoRoot, "tools", "nextfs-scan.mjs");

async function makeWorkspace() {
  return mkdtemp(path.join(tmpdir(), "nextfs-scan-"));
}

async function writeTextFile(filePath, value) {
  await mkdir(path.dirname(filePath), { recursive: true });
  await writeFile(filePath, value, "utf8");
}

async function writeFsproj(dir, includes) {
  const items = includes.map((f) => `    <Compile Include="${f}" />`).join("\n");
  const content = `<Project Sdk="Microsoft.NET.Sdk">
  <ItemGroup>
${items}
  </ItemGroup>
</Project>
`;
  await writeTextFile(dir, content);
}

async function runScanner(cwd, args = []) {
  return new Promise((resolve) => {
    const child = spawn(process.execPath, [scannerPath, ...args], {
      cwd,
      stdio: ["ignore", "pipe", "pipe"],
    });

    let stdout = "";
    let stderr = "";

    child.stdout.setEncoding("utf8");
    child.stderr.setEncoding("utf8");
    child.stdout.on("data", (chunk) => { stdout += chunk; });
    child.stderr.on("data", (chunk) => { stderr += chunk; });
    child.on("close", (code) => { resolve({ code, stdout, stderr }); });
  });
}

async function readJson(filePath) {
  return JSON.parse(await readFile(filePath, "utf8"));
}

// ---------------------------------------------------------------------------
// Happy-path tests
// ---------------------------------------------------------------------------

test("generates entries from NextFs.NextFsEntry attributes (fully-qualified form)", async () => {
  const root = await makeWorkspace();
  const srcDir = path.join(root, "src");

  await writeFsproj(path.join(srcDir, "Project.fsproj"), ["App/Layout.fs"]);
  await writeTextFile(path.join(root, "package.json"), "{}");
  await writeTextFile(
    path.join(srcDir, "App/Layout.fs"),
    '[<NextFs.NextFsEntry("app/layout.js", Default="Layout", Named="metadata viewport")>]\nmodule App.Layout\n',
  );

  const result = await runScanner(root, ["src/Project.fsproj"]);

  assert.equal(result.code, 0, result.stderr);
  const json = await readJson(path.join(root, "nextfs.entries.json"));
  assert.deepEqual(json.entries, [
    {
      from: "./.fable/App/Layout.js",
      to: "./app/layout.js",
      defaultFromNamed: "Layout",
      named: ["metadata", "viewport"],
    },
  ]);
});

test("also accepts unqualified NextFsEntry (without NextFs. prefix)", async () => {
  const root = await makeWorkspace();
  const srcDir = path.join(root, "src");

  await writeFsproj(path.join(srcDir, "Project.fsproj"), ["App/Page.fs"]);
  await writeTextFile(path.join(root, "package.json"), "{}");
  await writeTextFile(
    path.join(srcDir, "App/Page.fs"),
    '[<NextFsEntry("app/page.js", Directive="use client", Default="Page")>]\nmodule App.Page\n',
  );

  const result = await runScanner(root, ["src/Project.fsproj"]);

  assert.equal(result.code, 0, result.stderr);
  const json = await readJson(path.join(root, "nextfs.entries.json"));
  assert.deepEqual(json.entries, [
    {
      from: "./.fable/App/Page.js",
      to: "./app/page.js",
      directive: "use client",
      defaultFromNamed: "Page",
    },
  ]);
});

test("handles Directive, Named, and ExportAll options", async () => {
  const root = await makeWorkspace();
  const srcDir = path.join(root, "src");

  await writeFsproj(path.join(srcDir, "Project.fsproj"), [
    "App/Actions.fs",
    "InstrumentationClient.fs",
  ]);
  await writeTextFile(path.join(root, "package.json"), "{}");
  await writeTextFile(
    path.join(srcDir, "App/Actions.fs"),
    '[<NextFs.NextFsEntry("app/actions.js", Directive="use server", Named="createPost deletePost")>]\nmodule App.Actions\n',
  );
  await writeTextFile(
    path.join(srcDir, "InstrumentationClient.fs"),
    '[<NextFs.NextFsEntry("instrumentation-client.js", ExportAll=true)>]\nmodule InstrumentationClient\n',
  );

  const result = await runScanner(root, ["src/Project.fsproj"]);

  assert.equal(result.code, 0, result.stderr);
  const json = await readJson(path.join(root, "nextfs.entries.json"));
  assert.deepEqual(json.entries, [
    {
      from: "./.fable/App/Actions.js",
      to: "./app/actions.js",
      directive: "use server",
      named: ["createPost", "deletePost"],
    },
    {
      from: "./.fable/InstrumentationClient.js",
      to: "./instrumentation-client.js",
      exportAll: true,
    },
  ]);
});

test("includes NextFsStaticExport as staticExports in the entry", async () => {
  const root = await makeWorkspace();
  const srcDir = path.join(root, "src");

  await writeFsproj(path.join(srcDir, "Project.fsproj"), ["Proxy.fs"]);
  await writeTextFile(path.join(root, "package.json"), "{}");
  await writeTextFile(
    path.join(srcDir, "Proxy.fs"),
    '[<NextFs.NextFsEntry("proxy.js", Named="proxy")>]\n[<NextFs.NextFsStaticExport("config", """{"matcher":["/(.*)"]}""")>]\nmodule Proxy\n',
  );

  const result = await runScanner(root, ["src/Project.fsproj"]);

  assert.equal(result.code, 0, result.stderr);
  const json = await readJson(path.join(root, "nextfs.entries.json"));
  assert.deepEqual(json.entries, [
    {
      from: "./.fable/Proxy.js",
      to: "./proxy.js",
      named: ["proxy"],
      staticExports: { config: { matcher: ["/(.*)" ] } },
    },
  ]);
});

test("skips F# files that have no NextFsEntry attribute", async () => {
  const root = await makeWorkspace();
  const srcDir = path.join(root, "src");

  await writeFsproj(path.join(srcDir, "Project.fsproj"), [
    "App/Layout.fs",
    "App/Helpers.fs",
  ]);
  await writeTextFile(path.join(root, "package.json"), "{}");
  await writeTextFile(
    path.join(srcDir, "App/Layout.fs"),
    '[<NextFs.NextFsEntry("app/layout.js", Default="Layout")>]\nmodule App.Layout\n',
  );
  await writeTextFile(
    path.join(srcDir, "App/Helpers.fs"),
    "module App.Helpers\n\nlet helper x = x + 1\n",
  );

  const result = await runScanner(root, ["src/Project.fsproj"]);

  assert.equal(result.code, 0, result.stderr);
  const json = await readJson(path.join(root, "nextfs.entries.json"));
  assert.equal(json.entries.length, 1);
  assert.equal(json.entries[0].to, "./app/layout.js");
});

test("writes nextfs.entries.json next to package.json one level up from fsproj", async () => {
  const root = await makeWorkspace();
  const srcDir = path.join(root, "src");

  // package.json is at root, fsproj is in src/
  await writeFsproj(path.join(srcDir, "Project.fsproj"), ["App/Page.fs"]);
  await writeTextFile(path.join(root, "package.json"), "{}");
  await writeTextFile(
    path.join(srcDir, "App/Page.fs"),
    '[<NextFs.NextFsEntry("app/page.js", Default="Page")>]\nmodule App.Page\n',
  );

  const result = await runScanner(root, ["src/Project.fsproj"]);

  assert.equal(result.code, 0, result.stderr);
  // JSON should be at root, NOT in src/
  const json = await readJson(path.join(root, "nextfs.entries.json"));
  assert.ok(json.entries.length > 0);
  // Verify it was NOT written inside src/
  await assert.rejects(() => readFile(path.join(srcDir, "nextfs.entries.json"), "utf8"));
});

test("scanner output is stable across consecutive runs (idempotent)", async () => {
  const root = await makeWorkspace();
  const srcDir = path.join(root, "src");

  await writeFsproj(path.join(srcDir, "Project.fsproj"), ["App/Layout.fs"]);
  await writeTextFile(path.join(root, "package.json"), "{}");
  await writeTextFile(
    path.join(srcDir, "App/Layout.fs"),
    '[<NextFs.NextFsEntry("app/layout.js", Default="Layout", Named="metadata")>]\nmodule App.Layout\n',
  );

  await runScanner(root, ["src/Project.fsproj"]);
  const first = await readFile(path.join(root, "nextfs.entries.json"), "utf8");

  await runScanner(root, ["src/Project.fsproj"]);
  const second = await readFile(path.join(root, "nextfs.entries.json"), "utf8");

  assert.equal(first, second);
});

test("derives from-path correctly for nested and root-level F# files", async () => {
  const root = await makeWorkspace();
  const srcDir = path.join(root, "src");

  await writeFsproj(path.join(srcDir, "Project.fsproj"), [
    "Proxy.fs",
    "App/Api/Posts.fs",
  ]);
  await writeTextFile(path.join(root, "package.json"), "{}");
  await writeTextFile(
    path.join(srcDir, "Proxy.fs"),
    '[<NextFs.NextFsEntry("proxy.js", Named="proxy")>]\nmodule Proxy\n',
  );
  await writeTextFile(
    path.join(srcDir, "App/Api/Posts.fs"),
    '[<NextFs.NextFsEntry("app/api/posts/route.js", Named="GET POST")>]\nmodule App.Api.Posts\n',
  );

  const result = await runScanner(root, ["src/Project.fsproj"]);

  assert.equal(result.code, 0, result.stderr);
  const json = await readJson(path.join(root, "nextfs.entries.json"));
  assert.equal(json.entries[0].from, "./.fable/Proxy.js");
  assert.equal(json.entries[1].from, "./.fable/App/Api/Posts.js");
});

// ---------------------------------------------------------------------------
// Error cases
// ---------------------------------------------------------------------------

test("exits with error when no .fsproj argument is given", async () => {
  const root = await makeWorkspace();
  const result = await runScanner(root, []);

  assert.notEqual(result.code, 0);
  assert.match(result.stderr, /Usage:/);
});

test("exits with error when the .fsproj file does not exist", async () => {
  const root = await makeWorkspace();
  const result = await runScanner(root, ["src/Missing.fsproj"]);

  assert.notEqual(result.code, 0);
});

test("exits with error when no NextFsEntry attributes are found in any file", async () => {
  const root = await makeWorkspace();
  const srcDir = path.join(root, "src");

  await writeFsproj(path.join(srcDir, "Project.fsproj"), ["App/Page.fs"]);
  await writeTextFile(path.join(root, "package.json"), "{}");
  await writeTextFile(path.join(srcDir, "App/Page.fs"), "module App.Page\n\nlet x = 1\n");

  const result = await runScanner(root, ["src/Project.fsproj"]);

  assert.notEqual(result.code, 0);
  assert.match(result.stderr, /No \[<NextFsEntry/);
});
