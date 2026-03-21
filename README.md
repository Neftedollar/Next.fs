# NextFs

`NextFs` is a Fable-first binding layer for modern Next.js apps.

Current scope:

- Built-in Next.js components: `next/link`, `next/image`, `next/script`, `next/form`, `next/head`
- App Router hooks and helpers from `next/navigation`
- Server-side request APIs from `next/headers` and `next/server`
- `NextRequest` / `NextResponse` baseline for route handlers
- Inline `'use server'` helper for server actions
- A wrapper generator for file-level `'use client'` / `'use server'` entrypoints

The current package targets modern Next.js App Router usage on Next.js 15/16, including async `headers()` / `cookies()`.

## Install

```bash
dotnet add package NextFs
```

Native runtime dependencies are expected to come from your Next.js app:

- `next`
- `react`
- `react-dom`

## Example

```fsharp
module App.Page

open Fable.Core
open Fable.Core.JsInterop
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
        Html.h1 "Hello from Fable + Next.js"
        NavLink()
    ]
```

Typed object `href` values can be built with `Href.create`:

```fsharp
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

Server-side request data is exposed as async APIs, matching modern Next.js:

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

Route handlers can use `NextRequest`, async `params`, and `NextResponse`:

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

Inline server actions can now be expressed directly from F#:

```fsharp
let saveSearch (formData: obj) =
    Directive.useServer()
    // mutate state here
    ()
```

## File Directives

Next.js requires `'use client'` and `'use server'` to appear at the top of the generated JavaScript file.

Fable can emit `'use server'` inside a function body, but file-level directives still land after ESM imports in generated modules. For that reason, file-level entrypoints still need a thin wrapper.

The repository includes a small wrapper generator:

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

The generator creates thin JS entry files that re-export your Fable output with the correct file-level directive.

For `'use server'` entries, only named exports are allowed. This is deliberate: Next.js rejects `"use server"` files that export non-async values or broad `export *` re-exports.

## Important F# Interop Note

When exporting functions that Next.js will call directly, use JavaScript-shaped signatures.

- Route handlers should be uncurried: `let get (request, ctx) = ...`
- Multi-argument server actions should also be uncurried: `let update (prevState, formData) = ...`
- Use `[<CompiledName("GET")>]`, `[<CompiledName("POST")>]`, etc. for route handler exports

## Remaining Gap

File-level directives are handled through generated wrappers, and inline `'use server'` is available from F#. The next step is automating wrapper generation in the consumer workflow so this no longer depends on a separate manual command/config step.
