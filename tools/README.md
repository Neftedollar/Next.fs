# Tools

This folder contains repository tooling scripts.

Current scripts:

- `nextfs-entry.mjs`: generates thin Next.js wrapper files from `nextfs.entries.json`
- `generate-google-font-bindings.mjs`: regenerates the `GoogleFont.*` binding catalog from official Next.js type definitions

Use these scripts when you are working on:

- wrapper generation
- generated binding maintenance
- repository automation around build artifacts

These tools support both [samples](../samples/README.md) and [examples](../examples/README.md), but they are not part of the public runtime API of `NextFs` itself.
