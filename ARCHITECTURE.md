# Architecture

This document describes how `NextFs` works end-to-end, from F# source to a running Next.js application.

## Pipeline Overview

```
F# source files
  (NextFs bindings)
       │
       ▼
  Fable compiler
  (fable-compiler, via npm run sync:app)
       │
       ▼
  Generated JavaScript
  (.js files in app/)
       │
       ├──▶  nextfs-entry.mjs (wrapper generator)
       │         Reads nextfs.entries.json
       │         Emits 'use client' / 'use server' wrapper files
       │
       ▼
  Next.js build
  (next build / next start)
       │
       ▼
  Running App Router application
```

## Key Components

### `src/NextFs/` — Bindings package

The core library. Thin Fable wrappers over Next.js APIs with no runtime logic of their own.

| File | Covers |
|------|--------|
| `NextFs.fs` | Link, Image, Script, Form, navigation, metadata, cache, fonts, proxy, server actions |
| `NextFs.Server.fs` | `NextRequest`, `NextResponse`, route handlers, `next/headers`, `next/server` |
| `NextFs.Client.fs` | Client-only hooks (`useLinkStatus`, `useReportWebVitals`, etc.) |
| `NextFs.Font.Google.g.fs` + Chunk files | Generated `GoogleFont.*` catalog from official Next.js type definitions |

### `tools/nextfs-entry.mjs` — Wrapper generator

Next.js requires `'use client'` or `'use server'` to appear as the **first token** in a file.
Fable always emits `import` statements before any user code, so file-level directives must
live in a thin wrapper module that re-exports the Fable output.

The generator reads a `nextfs.entries.json` config and writes wrapper `.js` files:

```json
[
  { "input": "app/layout.js", "directive": "use client" },
  { "input": "app/actions.js", "directive": "use server" }
]
```

### `samples/NextFs.Smoke/` — Compile-smoke coverage

An F# project that imports and references the entire public API surface.
Its only job is to compile without errors — it verifies that all bindings are
syntactically and type-correct from a consumer perspective.

### `examples/nextfs-starter/` — End-to-end starter

A minimal but complete App Router project demonstrating the intended layout:
layout, page, route handler, proxy config, instrumentation, and special files
(`error.js`, `loading.js`, `not-found.js`, `forbidden.js`, `unauthorized.js`, etc.)
all generated from F#.

## Femto Metadata

`NextFs.fsproj` embeds Femto metadata describing the required npm peer dependencies
(`next`, `react`, `react-dom`) and their accepted version ranges.
Consumers can validate or auto-resolve these with:

```bash
dotnet femto --validate yourProject.fsproj
dotnet femto --resolve yourProject.fsproj
```

## Versioning

`NextFs` follows semver. The public API is the set of F# types and module members
exported from `src/NextFs/`. After v1.0, breaking changes require a major version bump.

APIs that depend on Next.js experimental flags are marked `[experimental]` in the
[API reference](docs/api-reference.md) and may change without a major bump while
the underlying Next.js feature is unstable.
