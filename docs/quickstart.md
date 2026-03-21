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

- `Link.create`, `Image.create`, `Script.create`, `Form.create`, `Head.create`
- `Navigation.useRouter`, `Navigation.usePathname`, `Navigation.useSearchParams`, `Navigation.useParams`
- `Server.headers`, `Server.cookies`, `Server.draftMode`, `Server.connection`
- `Cache.cacheLifeProfile`, `Cache.cacheTag`, `Cache.revalidatePath`, `Cache.revalidateTag`, `Cache.updateTag`, `Cache.refresh`
- `ServerResponse.json`, `ServerResponse.jsonWithInit`, `ServerResponse.redirect`, `ServerResponse.rewrite`, `ServerResponse.next`

If an entry module uses client-only hooks such as `Navigation.usePathname` or `Navigation.useRouter`, generate a `use client` wrapper for that file. See [Directives and wrappers](directives-wrappers.md).

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

## 6. Use the starter when you want a full folder layout

If you want to see the intended shape of a real App Router project, use [the starter example](../examples/nextfs-starter/README.md). It includes:

- F# source modules under `src/App/**`
- generated wrapper files under `app/**`
- a wrapper manifest
- a buildable F# example project
