# Tools

This folder contains repository tooling scripts.

Current scripts:

- `nextfs-scan.mjs`: scans a `.fsproj` for `[<NextFs.NextFsEntry>]` attributes and writes `nextfs.entries.json` — the recommended first step in the wrapper pipeline
- `nextfs-entry.mjs`: reads `nextfs.entries.json` and generates thin Next.js wrapper files (re-exports with optional directives)
- `generate-google-font-bindings.mjs`: regenerates the `GoogleFont.*` binding catalog from official Next.js type definitions

Typical usage order:

```bash
# 1. Scan F# source for entry annotations
node tools/nextfs-scan.mjs src/YourProject.fsproj

# 2. Generate wrappers from the manifest
node tools/nextfs-entry.mjs nextfs.entries.json
```

In the starter these are wired as `npm run scan` and `npm run gen:wrappers`, both called by `npm run sync:app`.

Use these scripts when you are working on:

- wrapper generation
- generated binding maintenance
- repository automation around build artifacts

These tools support both [samples](../samples/README.md) and [examples](../examples/README.md), but they are not part of the public runtime API of `NextFs` itself.
