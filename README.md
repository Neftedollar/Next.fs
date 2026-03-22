# NextFs

[![CI](https://github.com/Neftedollar/Next.fs/actions/workflows/ci.yml/badge.svg)](https://github.com/Neftedollar/Next.fs/actions/workflows/ci.yml)
[![NuGet Publish](https://github.com/Neftedollar/Next.fs/actions/workflows/publish-nuget.yml/badge.svg)](https://github.com/Neftedollar/Next.fs/actions/workflows/publish-nuget.yml)
[![NuGet](https://img.shields.io/nuget/v/NextFs.svg)](https://www.nuget.org/packages/NextFs)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](./LICENSE)

`NextFs` is a thin Fable binding layer for writing Next.js App Router applications in F#.

The package stays close to native Next.js concepts instead of introducing a separate framework. If you already understand how a feature works in Next.js, the goal is that you can express the same shape in F# with minimal translation.

## Start Here

If you are evaluating the repository for the first time, use this path:

1. read [Quickstart](docs/quickstart.md)
2. read [Starter app walkthrough](docs/starter-app-walkthrough.md)
3. inspect [the starter example](examples/nextfs-starter/README.md)
4. learn the wrapper rules in [Directives and wrappers](docs/directives-wrappers.md)
5. use [Data fetching and route config](docs/data-fetching-and-route-config.md) for server `fetch()` and route segment exports
6. use [Server and client patterns](docs/server-client-patterns.md) for route handlers, wrappers, and mixed App Router flows
7. use [Special files](docs/special-files.md) for `error.js`, `loading.js`, `not-found.js`, and auth-interrupt conventions
8. use [API reference](docs/api-reference.md) as the lookup table

## What It Covers

- `next/link`, `next/image`, `next/script`, `next/form`, `next/head`
- `next/font/local` and a generated `next/font/google` catalog
- App Router hooks and helpers from `next/navigation`
- request helpers from `next/headers`
- `NextRequest`, `NextResponse`, and route handler helpers from `next/server`
- `NextRequest` / `NextResponse` constructors and init builders
- typed server `fetch()` helpers for `cache`, `next.revalidate`, and `next.tags`
- `proxy.js` config builders and `NextFetchEvent`
- root instrumentation entry patterns for `instrumentation.js` and `instrumentation-client.js`
- App Router special-file helpers and documented patterns for `error.js`, `global-error.js`, `loading.js`, `not-found.js`, `global-not-found.js`, `template.js`, `default.js`, `forbidden.js`, and `unauthorized.js`
- cache invalidation and cache directives from `next/cache`
- metadata, viewport, robots, sitemap, manifest, and image-metadata builders
- `ImageResponse` bindings for Open Graph and icon generation
- request/response cookie option builders
- `NavigationClient.useServerInsertedHTML`, `NavigationClient.unstableIsUnrecognizedActionError`, `LinkClient.useLinkStatus`, `WebVitals.useReportWebVitals`, `after`, `userAgent`, `forbidden`, and `unauthorized`
- `Image.getImageProps()` and action-mismatch detection for client-side server-action calls
- inline `Directive.useServer()` and `Directive.useCache()` support
- wrapper generation for file-level `'use client'` and `'use server'`

## Compatibility

- `NextFs`: `0.9.x`
- `next`: `>= 15.0.0 < 17.0.0`
- `react`: `>= 18.2.0 < 20.0.0`
- `react-dom`: `>= 18.2.0 < 20.0.0`
- core Fable dependencies in this repo: `Fable.Core 4.5.0`, `Feliz 2.9.0`

## Install

```bash
dotnet add package NextFs
```

Your consuming Next.js app still provides the JavaScript runtime packages:

- `next`
- `react`
- `react-dom`

`NextFs` publishes Femto metadata for those packages, so a Fable consumer can check or resolve them with:

```bash
dotnet femto yourProject.fsproj
dotnet femto --resolve yourProject.fsproj
```

For repository work, restore local tools first:

```bash
dotnet tool restore
```

The repo-local tool manifest currently includes `femto` and `fable`.

## Quick Start

```fsharp
module App.Page

open Fable.Core
open Feliz
open NextFs

[<ReactComponent>]
let NavLink() =
    Link.create [
        Link.href "/dashboard"
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

Route handlers use JavaScript-shaped arguments and HTTP verb exports:

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

Server-side helpers follow the asynchronous shape of modern Next.js APIs:

```fsharp
module App.ServerPage

open Feliz
open NextFs

[<ExportDefault>]
let Page() =
    async {
        let! headers = Async.AwaitPromise(Server.headers())
        let userAgent = headers.get("user-agent") |> Option.defaultValue "unknown"

        return Html.pre userAgent
    }
    |> Async.StartAsPromise
```

## Cache And Server Actions

`NextFs` now includes a baseline `next/cache` surface for App Router workflows:

- `Directive.useCache()`
- `Directive.useCachePrivate()`
- `Directive.useCacheRemote()`
- `Cache.cacheLifeProfile`
- `Cache.cacheLife`
- `Cache.cacheTag` / `Cache.cacheTags`
- `Cache.revalidatePath`
- `Cache.revalidateTag`
- `Cache.updateTag`
- `Cache.refresh`
- `Cache.noStore`

Example:

```fsharp
let loadNavigationLabels () =
    Directive.useCache()
    Cache.cacheLifeProfile CacheProfile.Hours
    Cache.cacheTags [ "navigation"; "searches" ]

    [| "Home"; "Search"; "Docs" |]

let saveSearch (_formData: obj) =
    Directive.useServer()
    Cache.updateTag "searches"
    Cache.revalidatePath "/"
    Cache.refresh()
    ()
```

## Metadata And Special Files

`NextFs` now covers the parts of App Router you need to export from layouts and metadata files:

- `Metadata`
- `MetadataOpenGraph`
- `MetadataTwitter`
- `Viewport`
- `MetadataRoute.Robots`
- `MetadataRoute.SitemapEntry`
- `MetadataRoute.Manifest`
- `ImageMetadata`
- `ImageResponse`

Example:

```fsharp
let metadata =
    Metadata.create [
        Metadata.titleTemplate (
            MetadataTitle.create [
                MetadataTitle.defaultValue "NextFs"
                MetadataTitle.template "%s | NextFs"
            ]
        )
        Metadata.description "Next.js App Router bindings for F#."
        Metadata.openGraph (
            MetadataOpenGraph.create [
                MetadataOpenGraph.title "NextFs"
                MetadataOpenGraph.type' "website"
            ]
        )
    ]

let viewport =
    Viewport.create [
        Viewport.themeColor "#111827"
        Viewport.colorScheme "dark light"
    ]
```

For generated icon or Open Graph routes:

```fsharp
let generateImageMetadata() =
    [|
        ImageMetadata.create [
            ImageMetadata.id "small"
            ImageMetadata.contentType "image/png"
        ]
    |]
```

## Fonts, Proxy, And Cookies

The package now also covers the main App Router surfaces that typically force people back to handwritten JavaScript:

- `Font.local`
- `GoogleFont.Inter`, `GoogleFont.Roboto`, and the rest of the generated `next/font/google` catalog
- `FontOptions`, `LocalFontSource`, and `FontDeclaration`
- `ProxyConfig`, `ProxyMatcher`, `RouteHas`, and `NextFetchEvent`
- `CookieOptions` for request/response cookie mutation

Example:

```fsharp
let inter =
    GoogleFont.Inter(
        box {|
            subsets = [| "latin" |]
            display = "swap"
            variable = "--font-inter"
        |}
    )

For production App Router builds, keep `next/font` options as anonymous-record or object-literal expressions in the entry module. Next.js statically analyzes these calls and rejects helper-built objects.

let config =
    ProxyConfig.create [
        ProxyConfig.matcher "/dashboard/:path*"
    ]
```

## More App Router Helpers

Beyond the baseline router/navigation APIs, `NextFs` now also includes:

- `LinkClient.useLinkStatus()`
- `WebVitals.useReportWebVitals(...)`
- `NavigationClient.useServerInsertedHTML(...)`
- `NavigationClient.useSelectedLayoutSegmentFor(...)`
- `NavigationClient.useSelectedLayoutSegmentsFor(...)`
- `NavigationClient.unstableIsUnrecognizedActionError(...)`
- `Navigation.forbidden()`
- `Navigation.unauthorized()`
- `Navigation.unstableRethrow(...)`
- `Server.after(...)`
- `Server.userAgent(...)`
- `ServerFetch.fetch(...)`
- `ServerFetch.fetchWithInit(...)`
- `ServerRequest.createWithInit(...)`
- `ServerResponse.createWithInit(...)`
- `Image.getImageProps(...)`

These helpers are compile-smoked in `samples/NextFs.Smoke`.

## Server Fetch And Route Config

`NextFs` now includes typed helpers for Next.js server `fetch()` options and route segment exports:

- `ServerFetch.fetch(...)`
- `ServerFetch.fetchWithInit(...)`
- `ServerFetchInit`
- `NextFetchOptions`
- `ServerFetchCache`
- `Revalidate.seconds`, `Revalidate.forever`, `Revalidate.neverCache`
- `RouteRuntime`
- `PreferredRegion`
- `GenerateSitemapsEntry`

Example:

```fsharp
let runtime = RouteRuntime.Edge
let preferredRegion = PreferredRegion.home
let maxDuration = 30

let loadPosts() =
    ServerFetch.fetchWithInit "https://example.com/api/posts" (
        ServerFetchInit.create [
            ServerFetchInit.cache ServerFetchCache.ForceCache
            ServerFetchInit.next (
                NextFetchOptions.create [
                    NextFetchOptions.revalidate (Revalidate.seconds 900)
                    NextFetchOptions.tags [ "posts"; "homepage" ]
                ]
            )
        ]
    )
```

## Special Files

The repository now also documents and demonstrates the App Router special-file flow from F#:

- `error.js` and `global-error.js` via `ErrorBoundaryProps`
- `template.js` via `TemplateProps`
- `default.js` via `DefaultProps<'T>`
- starter patterns for `loading.js`, `not-found.js`, `global-not-found.js`, `forbidden.js`, and `unauthorized.js`

The dedicated guide is in [Special files](docs/special-files.md).

## App Router Directives And Wrappers

Inline directives work for function-level cases:

- `Directive.useServer()`
- `Directive.useCache()`
- `Directive.useCachePrivate()`
- `Directive.useCacheRemote()`

File-level `'use client'` and `'use server'` directives are different. Fable emits imports first, so App Router entry files still need thin wrapper modules when the directive must appear at the top of the generated JavaScript file.

Generate them with:

```bash
node tools/nextfs-entry.mjs samples/nextfs.entries.json
```

For `'use server'` wrappers, only named exports are allowed. The generator rejects default exports and `export *` for that case.

## Repository Layout

- `src/NextFs` contains the bindings package
- `samples/NextFs.Smoke` contains compile-smoke coverage of the package surface
- `examples/nextfs-starter` contains a minimal end-to-end App Router starter, including `layout`, `route`, `proxy`, instrumentation, and special-file entries generated from F#
- `tests/nextfs-entry.test.mjs` covers the wrapper generator
- `tools/nextfs-entry.mjs` generates directive wrapper files
- `tools/generate-google-font-bindings.mjs` regenerates the `GoogleFont` binding catalog from official Next.js type definitions

## Samples vs Examples

The repository uses `samples` and `examples` for different jobs:

- `samples` are verification-oriented. They exist to compile, exercise binding shapes, and catch regressions quickly.
- `examples` are usage-oriented. They are meant to look like real consumer projects someone could copy, inspect, or adapt.

In practice:

- `samples/NextFs.Smoke` is a compile-smoke project for the public API surface.
- `samples/app` is generated wrapper output used as a small fixture for the wrapper tool.
- `examples/nextfs-starter` is a runnable App Router starter that demonstrates the intended project layout.

## Examples And Docs

- [Docs folder guide](docs/README.md)
- [Examples folder guide](examples/README.md)
- [Samples folder guide](samples/README.md)
- [Tools folder guide](tools/README.md)
- [Local tool manifest guide](.config/README.md)
- [Quickstart](docs/quickstart.md)
- [Starter app walkthrough](docs/starter-app-walkthrough.md)
- [Data fetching and route config](docs/data-fetching-and-route-config.md)
- [Server and client patterns](docs/server-client-patterns.md)
- [Instrumentation](docs/instrumentation.md)
- [API reference](docs/api-reference.md)
- [Directives and wrappers](docs/directives-wrappers.md)
- [Special files](docs/special-files.md)
- [Package design and limitations](docs/package-design-limitations.md)
- [Starter example](examples/nextfs-starter/README.md)

## Validation

```bash
dotnet tool restore
dotnet femto --validate src/NextFs/NextFs.fsproj
node --test tests/*.mjs
dotnet build NextFs.slnx -v minimal
dotnet pack src/NextFs/NextFs.fsproj -c Release -o artifacts
node tools/nextfs-entry.mjs samples/nextfs.entries.json
node tools/nextfs-entry.mjs examples/nextfs-starter/nextfs.entries.json
git diff --exit-code -- examples/nextfs-starter/app examples/nextfs-starter/proxy.js examples/nextfs-starter/instrumentation.js examples/nextfs-starter/instrumentation-client.js
```

## NuGet Publishing

The repository includes `.github/workflows/publish-nuget.yml`, which publishes `NextFs` to nuget.org on manual dispatch or tag pushes matching `v*`.

Publishing uses NuGet Trusted Publishing through GitHub OIDC rather than a long-lived API key.

Package page:

- https://www.nuget.org/packages/NextFs

## Contributing

Contribution workflow and commit conventions are documented in [CONTRIBUTING.md](CONTRIBUTING.md).
