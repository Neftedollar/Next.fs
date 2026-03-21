# Samples

This folder contains verification-oriented sample code.

Unlike `examples`, the purpose here is not to be a polished consumer app. The purpose is to cover binding shapes, keep regression checks cheap, and provide small focused fixtures for tooling.

Current contents:

- [NextFs.Smoke](NextFs.Smoke/README.md): compile-smoke F# project covering the public API surface
- [app](app/README.md): generated wrapper fixtures used with `nextfs.entries.json`
- `nextfs.entries.json`: sample wrapper manifest for `tools/nextfs-entry.mjs`

Use `samples` when you want:

- compile-time coverage of new bindings
- small focused examples of exports or wrapper shapes
- tooling fixtures that should stay simple

For a more realistic consumer project, use [examples](../examples/README.md).
