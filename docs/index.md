---
title: Home
layout: home
nav_order: 0
---

# NextFs

[![CI](https://github.com/Neftedollar/Next.fs/actions/workflows/ci.yml/badge.svg)](https://github.com/Neftedollar/Next.fs/actions/workflows/ci.yml)
[![NuGet](https://img.shields.io/nuget/v/NextFs.svg)](https://www.nuget.org/packages/NextFs)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://github.com/Neftedollar/Next.fs/blob/main/LICENSE)

Thin Fable binding layer for writing Next.js App Router applications in F#.

The package stays close to native Next.js concepts instead of introducing a separate framework. If you already understand how a feature works in Next.js, the goal is that you can express the same shape in F# with minimal translation.

## Install

```bash
dotnet add package NextFs
```

## Reading Path

1. [Quickstart](quickstart.md) — add the package and write your first page
2. [Starter App Walkthrough](starter-app-walkthrough.md) — step through the full example
3. [Data Fetching & Route Config](data-fetching-and-route-config.md) — server `fetch()` and route segment exports
4. [Server & Client Patterns](server-client-patterns.md) — route handlers, mixed flows
5. [Directives & Wrappers](directives-wrappers.md) — `'use client'` / `'use server'` rules
6. [Special Files](special-files.md) — `error.js`, `loading.js`, `not-found.js`, and more
7. [API Reference](api-reference.md) — complete module and type lookup

## Other Guides

- [DSL & Computation Expressions](dsl.md)
- [Instrumentation](instrumentation.md)
- [Migration Guide (0.x → 1.0)](migration.md)
- [Package Design & Limitations](package-design-limitations.md)
- [FAQ](faq.md)

## Compatibility

| Package | Version |
|---------|---------|
| `NextFs` | `1.0.x` |
| `next` | `>= 15.0.0 < 17.0.0` |
| `react` / `react-dom` | `>= 18.2.0 < 20.0.0` |
| `Fable.Core` | `4.5.0` |
| `Feliz` | `2.9.0` |

## Source & Examples

- [GitHub repository](https://github.com/Neftedollar/Next.fs)
- [Starter example](https://github.com/Neftedollar/Next.fs/tree/main/examples/nextfs-starter)
- [Elmish example](https://github.com/Neftedollar/Next.fs/tree/main/examples/nextfs-elmish)
- [Reaction example](https://github.com/Neftedollar/Next.fs/tree/main/examples/nextfs-reaction)
