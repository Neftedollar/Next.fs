---
title: DSL & Computation Expressions
layout: default
nav_order: 11
---

# DSL & Computation Expressions

`NextFs.Dsl` is a separate package that provides a high-level F# DSL over NextFs bindings — computation expressions, async helpers, and response shortcuts.

## Install

```bash
dotnet add package NextFs.Dsl
```

This pulls in `NextFs` automatically.

## Async Helpers

Eliminate `Async.AwaitPromise(Server.xxx())` boilerplate:

```fsharp
open NextFs.Dsl

// Before
let! headers = Async.AwaitPromise(Server.headers())
let! cookies = Async.AwaitPromise(Server.cookies())

// After
let! headers = ServerAsync.headers()
let! cookies = ServerAsync.cookies()
```

Available in `ServerAsync`:

- `headers()` — request headers
- `cookies()` — cookie store
- `draftMode()` — draft mode state
- `connection()` — signal request-time dependency

Props unwrapping via `PropsAsync`:

```fsharp
let! routeParams = PropsAsync.routeParams pageProps
let! searchParams = PropsAsync.searchParams pageProps
let! layoutParams = PropsAsync.layoutParams layoutProps
let! defaultParams = PropsAsync.defaultParams defaultProps
let! contextParams = PropsAsync.contextParams ctx
```

## Route Handlers

The `route { }` CE wraps `async + Async.StartAsPromise` and supports `let!` on both `Async<_>` and `JS.Promise<_>`:

```fsharp
open NextFs
open NextFs.Dsl

[<CompiledName("GET")>]
let get (request: NextRequest, ctx: RouteHandlerContext<{| slug: string |}>) =
    route {
        let! routeParams = PropsAsync.contextParams ctx
        return
            Route.jsonOk {| slug = routeParams.slug |}
    }

[<CompiledName("POST")>]
let post (request: NextRequest) =
    route {
        let! payload = request.json<obj>()
        return Route.json payload 201
    }
```

Response helpers in `Route`:

| Helper | Description |
|--------|-------------|
| `Route.jsonOk body` | JSON 200 |
| `Route.json body status` | JSON with custom status |
| `Route.textOk body` | Plain text 200 |
| `Route.text body status` | Plain text with custom status |
| `Route.redirect url` | Redirect response |
| `Route.rewrite url` | Rewrite response |

## Server Fetch

The `fetch url { }` CE replaces deeply nested `ServerFetchInit.create [ ... ]`:

```fsharp
// Before
let! response =
    Async.AwaitPromise(
        ServerFetch.fetchWithInit "https://api.example.com/posts" (
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
    )

// After
let! response =
    fetch "https://api.example.com/posts" {
        forceCache
        revalidate 900
        tags [ "posts"; "homepage" ]
    }
```

Available operations:

| Operation | Description |
|-----------|-------------|
| `noStore` | Opt out of caching |
| `forceCache` | Force cache |
| `cache value` | Set cache mode directly |
| `revalidate seconds` | Revalidate interval |
| `revalidateNever` | Revalidate 0 (never cache) |
| `revalidateForever` | Never revalidate |
| `tags ["a"; "b"]` | Cache tags for on-demand revalidation |
| `tag "a"` | Single cache tag |

The CE returns `Async<ServerFetchResponse>`, so it composes directly with `let!` in `route { }` or `async { }`.

## Metadata

The `metadata { }` CE and its nested builders replace `Metadata.create [ ... ]`:

```fsharp
let og =
    openGraph {
        title "My App"
        description "App Router in F#"
        ogType "website"
    }

let tw =
    twitter {
        card "summary_large_image"
        title "My App"
    }

let metadata =
    metadata {
        titleTemplate "My App" "%s | My App"
        description "Next.js App Router bindings for F#."
        keywords [ "fable"; "nextjs"; "fsharp" ]
        openGraph og
        twitter tw
    }

let viewport =
    viewport {
        themeColor "#111827"
        colorScheme "dark light"
    }
```

Build `openGraph` and `twitter` objects outside the `metadata { }` block to avoid name collision with the custom operations.

## Server Components

The `serverComponent { }` CE wraps async server pages and layouts:

```fsharp
[<ExportDefault>]
let Page (props: PageProps<{| slug: string |}, obj>) =
    serverComponent {
        let! routeParams = PropsAsync.routeParams props
        let! headers = ServerAsync.headers()
        let ua = headers.get "user-agent" |> Option.defaultValue "unknown"

        return
            Html.main [
                Html.h1 (sprintf "Page: %s" routeParams.slug)
                Html.pre ua
            ]
    }
```

It supports `let!` on both `Async<_>` and `JS.Promise<_>`, and auto-wraps with `Async.StartAsPromise`.
