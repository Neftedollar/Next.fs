---
title: Special Files
layout: default
nav_order: 6
---

# Special Files

`NextFs` can drive App Router special files from F# source modules, but the rules are not identical for every file.

Use this page as the file-convention map for:

- `error.js`
- `global-error.js`
- `loading.js`
- `not-found.js`
- `global-not-found.js`
- `template.js`
- `default.js`
- `forbidden.js`
- `unauthorized.js`

## Convention Matrix

| Next.js file | F# pattern | Wrapper | Notes |
| --- | --- | --- | --- |
| `app/error.js` | client component with `ErrorBoundaryProps` | required | file must start with `'use client'` |
| `app/global-error.js` | client component with `ErrorBoundaryProps` | required | must render its own `<html>` and `<body>` |
| `app/loading.js` | plain default-export component | optional | Suspense fallback, no special props |
| `app/not-found.js` | plain default-export component | optional | rendered by `Navigation.notFound()` |
| `app/global-not-found.js` | plain default-export component, usually with metadata | optional | bypasses layouts entirely |
| `app/template.js` | default-export component with `TemplateProps` | optional | remounts children on navigation |
| `app/default.js` | default-export component with `DefaultProps<'T>` | optional | intended for parallel routes |
| `app/forbidden.js` | plain default-export component | optional | requires `experimental.authInterrupts` |
| `app/unauthorized.js` | plain default-export component | optional | requires `experimental.authInterrupts` |

## Typed Helpers

`NextFs` includes a small helper surface for the special files that have non-trivial props:

- `ErrorBoundaryProps`
- `ErrorWithDigest`
- `TemplateProps`
- `DefaultProps<'T>`

Example `error.js` source:

```fsharp
module App.Error

open Fable.Core
open Feliz
open NextFs

[<ExportDefault>]
[<ReactComponent>]
let Error(props: ErrorBoundaryProps) =
    Html.main [
        prop.children [
            Html.h1 "Segment error"
            Html.p props.error.message
            Html.button [
                prop.type'.button
                prop.onClick (fun _ -> props.``unstable_retry``())
                prop.text "Retry"
            ]
        ]
    ]
```

`error.js` and `global-error.js` are Client Components in Next.js, so they still need generated wrappers with `"use client"`.

## Config Flags

Some special files require explicit Next.js config:

- `global-not-found.js` requires `experimental.globalNotFound: true`
- `forbidden.js` and `unauthorized.js` require `experimental.authInterrupts: true`

Example:

```js
/** @type {import('next').NextConfig} */
const nextConfig = {
  experimental: {
    authInterrupts: true,
    globalNotFound: true,
  },
}

export default nextConfig
```

These APIs are version-sensitive. `authInterrupts`, `forbidden.js`, and `unauthorized.js` are still documented by Next.js as experimental. Treat them as supported by current Next.js, but not frozen.

## Important Differences

- `global-error.js` cannot export `metadata` or `generateMetadata`; use HTML elements such as `<title>` inside the component when needed.
- `global-error.js` must render its own `<html>` and `<body>`.
- `global-not-found.js` also bypasses layouts, so it must bring its own fonts, styles, and document shell.
- `default.js` is primarily for parallel-route recovery. Do not treat it as a generic fallback page.
- `template.js` is different from `layout.js`: it remounts its children when the segment changes.

## Wrapper Example

```json
{
  "entries": [
    {
      "directive": "use client",
      "from": "./.fable/App.Error.js",
      "to": "./app/error.js",
      "default": true
    },
    {
      "from": "./.fable/App.GlobalNotFound.js",
      "to": "./app/global-not-found.js",
      "default": true,
      "named": ["metadata"]
    }
  ]
}
```

For the exact generator rules, see [Directives and wrappers](directives-wrappers.md). For a concrete layout of these files in a real project, see [the starter example](https://github.com/Neftedollar/Next.fs/tree/main/examples/nextfs-starter).
