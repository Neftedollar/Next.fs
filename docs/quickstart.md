# Quickstart

`NextFs` is a binding package for Fable projects that target the Next.js App Router.

## 1. Add the package

```bash
dotnet add package NextFs
```

The consuming Next.js app still needs compatible JavaScript dependencies:

- `next`
- `react`
- `react-dom`

The package metadata in `NextFs.fsproj` currently targets `next >= 15.0.0 < 17.0.0` and `react` / `react-dom >= 18.2.0 < 20.0.0`.

## 2. Use the bindings

For a simple App Router page, import `NextFs` alongside your usual Fable and Feliz modules:

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

The main component helpers in the package are:

- `Font.local`, `GoogleFont.Inter`, and other generated `GoogleFont.*` loaders
- `Link.create`, `Image.create`, `Script.create`, `Form.create`, `Head.create`
- `NavigationClient.useRouter`, `NavigationClient.usePathname`, `NavigationClient.useSearchParams`, `NavigationClient.useParams`
- `NavigationClient.useServerInsertedHTML`, `NavigationClient.useSelectedLayoutSegmentFor`, `NavigationClient.unstableIsUnrecognizedActionError`
- `Server.headers`, `Server.cookies`, `Server.draftMode`, `Server.connection`
- `ServerFetch.fetch`, `ServerFetchInit.create`, `NextFetchOptions.create`, `Revalidate.seconds`
- `Cache.cacheLifeProfile`, `Cache.cacheTag`, `Cache.revalidatePath`, `Cache.revalidateTag`, `Cache.updateTag`, `Cache.refresh`
- `Metadata.create`, `Viewport.create`, `ImageResponse.createWithOptions`
- `ServerRequest.createWithInit`, `ServerResponse.json`, `ServerResponse.createWithInit`, `ServerResponse.redirect`, `ServerResponse.rewrite`, `ServerResponse.next`
- `Image.getImageProps`
- `ProxyConfig.create`, `ProxyMatcher.create`, `RouteHas.create`, `CookieOptions.create`

If an entry module uses client-only hooks such as `NavigationClient.usePathname` or `NavigationClient.useRouter`, generate a `use client` wrapper for that file. See [Directives and wrappers](directives-wrappers.md).

For mixed server/client App Router flows such as route handlers, `useServerInsertedHTML`, request/response construction, or client-side server-action error guards, use [Server and client patterns](server-client-patterns.md).

For `error.js`, `global-error.js`, `loading.js`, `not-found.js`, `global-not-found.js`, `template.js`, `default.js`, `forbidden.js`, and `unauthorized.js`, use [Special files](special-files.md).

## 3. Handle server APIs asynchronously

Server-side request APIs are promise-based in Next.js, so the F# bindings are also async:

```fsharp
module App.ServerPage

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

## 4. Use route handlers with `CompiledName`

Route handlers should be exported with JavaScript-shaped signatures and HTTP verb names:

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

If the exported entry module needs a file-level directive, use the wrapper generator described in [Directives and wrappers](directives-wrappers.md).

## 5. Use cache and server actions where appropriate

`NextFs` also exposes the core `next/cache` invalidation APIs used in App Router applications:

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

## 6. Use fonts and proxy when you need them

`next/font` stays close to the original Next.js shape:

```fsharp
let inter =
    GoogleFont.Inter(
        box {|
            subsets = [| "latin" |]
            display = "swap"
        |}
    )
```

For `next/font` in real App Router entries, prefer anonymous-record or object-literal options. Next.js statically analyzes the loader call and can reject helper-built option objects in production builds.

`proxy.js` can also be driven from F# by exporting a `proxy` function and `config` object through the wrapper generator:

```fsharp
let config =
    ProxyConfig.create [
        ProxyConfig.matcher "/dashboard/:path*"
    ]
```

## 7. Use typed server fetch options for cached data

`NextFs` also exposes the Next.js server `fetch()` extensions for `cache`, `next.revalidate`, and `next.tags`:

```fsharp
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

If you need route-level config on `layout`, `page`, or `route` files, export plain values from F#:

```fsharp
let runtime = RouteRuntime.Edge
let preferredRegion = PreferredRegion.home
let dynamicParams = true
let maxDuration = 30
```

For the full pattern, including `generateSitemaps`, use [Data fetching and route config](data-fetching-and-route-config.md).

## 8. Use the starter when you want a full folder layout

If you want to see the intended shape of a real App Router project, start with [Starter app walkthrough](starter-app-walkthrough.md) and then inspect [the starter example](../examples/nextfs-starter/README.md). It includes:

- F# source modules under `src/App/**`
- root-level instrumentation entries outside `app/**`
- Fable output under `.fable/**`
- generated wrapper files under `app/**`
- special-file entries such as `error`, `loading`, `not-found`, `template`, and auth interrupts
- a wrapper manifest
- a reference F# example project
- a root layout exported from F# with `metadata`, `viewport`, and `next/font` usage

The practical loop is:

```bash
dotnet tool restore
cd examples/nextfs-starter
npm install
npm run sync:app
npm run dev
```

`build:fsharp` only checks .NET compilation. `build:fable` is the real JavaScript emit step. The starter example is also validated with a real `next build`.
