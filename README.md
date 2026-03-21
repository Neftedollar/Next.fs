# NextFs

[![CI](https://github.com/Neftedollar/Next.fs/actions/workflows/ci.yml/badge.svg)](https://github.com/Neftedollar/Next.fs/actions/workflows/ci.yml)
[![NuGet Publish](https://github.com/Neftedollar/Next.fs/actions/workflows/publish-nuget.yml/badge.svg)](https://github.com/Neftedollar/Next.fs/actions/workflows/publish-nuget.yml)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](./LICENSE)

`NextFs` is a Fable-first binding layer for building Next.js applications with F#.

Status: experimental, but already usable as a bootstrap layer for App Router projects.

## What It Covers

- Next.js components: `next/link`, `next/image`, `next/script`, `next/form`, `next/head`
- App Router hooks and helpers from `next/navigation`
- Async server request APIs from `next/headers` and `next/server`
- `NextRequest` / `NextResponse` baseline for route handlers
- Inline `Directive.useServer()` support for F# server actions
- Wrapper generation for file-level `'use client'` / `'use server'` entry files

The current package is aimed at Next.js 15/16 style App Router usage, including async `headers()` and `cookies()`.

## Repository Layout

- `src/NextFs` contains the bindings package
- `samples/NextFs.Smoke` contains a compile-smoke consumer project
- `tools/nextfs-entry.mjs` generates thin Next.js wrapper files with file-level directives
- `samples/nextfs.entries.json` shows the wrapper manifest format

## Install

```bash
dotnet add package NextFs
```

Runtime dependencies are expected to come from the consuming Next.js app:

- `next`
- `react`
- `react-dom`

## Example

```fsharp
module App.Page

open Fable.Core
open Feliz
open NextFs

[<ReactComponent>]
let NavLink() =
    Link.create [
        Link.href "/dashboard"
        prop.className "nav-link"
        prop.text "Dashboard"
    ]

[<ExportDefault>]
let Page() =
    Html.main [
        prop.children [
            Html.h1 "Hello from Fable + Next.js"
            NavLink()
        ]
    ]
```

Typed object `href` values can be built with `Href.create`:

```fsharp
open Fable.Core.JsInterop

Link.create [
    Link.hrefObject (
        Href.create [
            Href.pathname "/search"
            Href.query (createObj [ "q" ==> "fable" ])
        ]
    )
    prop.text "Search"
]
```

Server-side request data is exposed as async APIs:

```fsharp
module App.ServerPage

open Fable.Core
open Feliz
open NextFs

[<ExportDefault>]
let Page() =
    async {
        let! headers = Async.AwaitPromise(Server.headers())
        let userAgent = headers.get("user-agent") |> Option.defaultValue "unknown"

        return
            Html.pre [
                prop.text userAgent
            ]
    }
    |> Async.StartAsPromise
```

Route handlers can use `NextRequest`, async route params, and `NextResponse`:

```fsharp
module App.Api.Posts

open Fable.Core
open Fable.Core.JsInterop
open NextFs

[<CompiledName("GET")>]
let get (request: NextRequest, ctx: RouteHandlerContext<{| slug: string |}>) =
    async {
        let! routeParams = Async.AwaitPromise ctx.``params``

        return
            ServerResponse.jsonWithInit
                (createObj [
                    "slug" ==> routeParams.slug
                    "pathname" ==> request.nextUrl.pathname
                ])
                (ResponseInit.create [
                    ResponseInit.status 200
                ])
    }
    |> Async.StartAsPromise
```

Inline server actions can emit `'use server'` from F#:

```fsharp
let saveSearch (formData: obj) =
    Directive.useServer()
    ()
```

## File Directives

Next.js requires `'use client'` and `'use server'` to appear at the top of the generated JavaScript file.

`Directive.useServer()` covers the inline function-level case. File-level directives are different: Fable-generated ESM imports appear before emitted statements, so client/server entry modules still need a thin wrapper file.

The repository includes a wrapper generator:

```bash
node tools/nextfs-entry.mjs samples/nextfs.entries.json
```

Example config:

```json
{
  "entries": [
    {
      "directive": "use client",
      "from": "./.fable/App.Page.js",
      "to": "./app/page.js",
      "default": true
    },
    {
      "directive": "use server",
      "from": "./.fable/App.Actions.js",
      "to": "./app/actions.js",
      "named": ["createPost", "deletePost"]
    },
    {
      "from": "./.fable/App.Api.Posts.js",
      "to": "./app/api/posts/route.js",
      "named": ["GET", "POST"]
    }
  ]
}
```

For `'use server'` wrapper entries, only named exports are allowed. This matches Next.js expectations and avoids invalid export shapes.

## Interop Notes

- Route handlers should be exported with JavaScript-shaped signatures: `let get (request, ctx) = ...`
- Multi-argument server actions should also be uncurried: `let update (prevState, formData) = ...`
- Use `[<CompiledName("GET")>]`, `[<CompiledName("POST")>]`, and similar attributes for route handler exports

## Local Validation

```bash
dotnet build NextFs.slnx -v minimal
dotnet pack src/NextFs/NextFs.fsproj -c Release -o artifacts
node tools/nextfs-entry.mjs samples/nextfs.entries.json
```

## NuGet Publishing

The repository includes [publish-nuget.yml](/Users/roman/Documents/dev/Next_fs/.github/workflows/publish-nuget.yml), which publishes `NextFs` to nuget.org on:

- manual `workflow_dispatch`
- git tag pushes matching `v*`

It is configured for NuGet Trusted Publishing via GitHub OIDC, not a long-lived API key.

Before the first publish, create a trusted publishing policy on nuget.org with:

- Repository Owner: `Neftedollar`
- Repository: `Next.fs`
- Workflow File: `publish-nuget.yml`
- Environment: `release`

## Roadmap

- automate wrapper generation in the consumer workflow
- expand `next/server` coverage beyond the current `NextResponse` baseline
- add stronger typing for metadata, route config, and server action conventions

## Contributing

Contribution workflow and commit conventions are documented in [CONTRIBUTING.md](/Users/roman/Documents/dev/Next_fs/CONTRIBUTING.md).
