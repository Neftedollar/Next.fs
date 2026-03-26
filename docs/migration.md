---
title: Migration Guide (0.x → 1.0)
layout: default
nav_order: 9
---

# Migration Guide: 0.x → 1.0

This page covers the changes needed when upgrading from NextFs 0.x to 1.0.

## Breaking Change: Entry Manifest

The biggest change in 1.0 is the elimination of handwritten `nextfs.entries.json`.

### Before (0.x)

You maintained `nextfs.entries.json` manually:

```json
{
  "entries": [
    {
      "fable": ".fable/App.Page/App.Page.js",
      "output": "app/page.js",
      "directive": "use client",
      "default": "Page"
    }
  ]
}
```

### After (1.0)

Annotate each entry module directly in F#:

```fsharp
[<NextFs.NextFsEntry("app/page.js", Directive="use client", Default="Page")>]
module App.Page
```

Then generate the manifest automatically:

```bash
node tools/nextfs-scan.mjs path/to/YourProject.fsproj
```

The scanner reads `[<NextFsEntry>]` attributes from your `.fs` files and writes `nextfs.entries.json` for you.

## Step-By-Step Upgrade

### 1. Update the package

```bash
dotnet add package NextFs --version 1.0.0
```

### 2. Add attributes to every entry module

For each entry in your existing `nextfs.entries.json`, add a matching `[<NextFs.NextFsEntry(...)>]` attribute **before** the `module` declaration and **before** any `open` statements.

| JSON field | Attribute parameter |
|------------|-------------------|
| `"output"` | positional: `"app/page.js"` |
| `"directive"` | `Directive="use client"` or `Directive="use server"` |
| `"default"` | `Default="Page"` |
| `"named"` | `Named="metadata viewport"` (space-separated) |
| `"exportAll"` | `ExportAll=true` |

Example — a client page with metadata:

```fsharp
// Before: only module declaration
module App.Page

// After: add attribute before module
[<NextFs.NextFsEntry("app/page.js", Directive="use client", Default="Page", Named="metadata")>]
module App.Page
```

For static literal exports (e.g. proxy config), use `[<NextFs.NextFsStaticExport>]`:

```fsharp
[<NextFs.NextFsEntry("proxy.js", Named="proxy")>]
[<NextFs.NextFsStaticExport("config", """{"matcher":["/((?!_next).*)"]}""")>]
module Proxy
```

### 3. Add scan script to package.json

```json
{
  "scripts": {
    "scan": "node tools/nextfs-scan.mjs src/YourProject.fsproj"
  }
}
```

### 4. Regenerate the manifest

```bash
npm run scan
```

This overwrites `nextfs.entries.json` from your F# attributes. Verify the output matches your previous manual file.

### 5. Regenerate wrappers

```bash
node tools/nextfs-entry.mjs nextfs.entries.json
```

The generated wrapper files should be identical to what you had before.

### 6. Verify the build

```bash
dotnet fable src/YourProject.fsproj
npm run build
```

### 7. Remove manual JSON maintenance

From this point, `nextfs.entries.json` is generated — never edit it by hand. Add the scan step to your dev workflow:

```json
{
  "scripts": {
    "scan": "node tools/nextfs-scan.mjs src/YourProject.fsproj",
    "sync:app": "npm run scan && node tools/nextfs-entry.mjs nextfs.entries.json"
  }
}
```

## API Changes

### Renamed modules (0.9 → 1.0)

No module renames in 1.0. The client/server split introduced in 0.9 (`NavigationClient`, `LinkClient`, `WebVitals` for client hooks vs `Navigation`, `Server` for server helpers) remains unchanged.

### New APIs in 1.0

- `[<NextFs.NextFsEntry>]` attribute
- `[<NextFs.NextFsStaticExport>]` attribute
- `tools/nextfs-scan.mjs` scanner

### Experimental APIs

The following APIs are included in 1.0 but depend on Next.js experimental flags. They may change without a NextFs major bump if Next.js changes or removes them:

- `Navigation.forbidden()` / `Navigation.unauthorized()` — require `experimental.authInterrupts: true`
- `Directive.useCachePrivate()` / `Directive.useCacheRemote()` — require `experimental.dynamicIO`
- `NavigationClient.unstableIsUnrecognizedActionError` — wraps an upstream `unstable_*` function
- `ProxyConfig` — proxy support varies by Next.js version

## Questions?

See the [FAQ](faq.md) or open a [GitHub Discussion](https://github.com/Neftedollar/Next.fs/discussions).
