---
title: Server & Client Patterns
layout: default
nav_order: 4
---

# Server And Client Patterns

This guide collects the App Router patterns that usually matter first when moving a Next.js app from JavaScript or TypeScript to F#.

The goal is not to hide Next.js. The goal is to express the same shapes through `NextFs` bindings with as little translation noise as possible.

## Client entry with a wrapper

If an entry module uses client hooks such as `NavigationClient.useRouter()` or `NavigationClient.usePathname()`, keep the F# file focused on the component and generate a thin wrapper with `'use client'`.

```fsharp
module App.Page

open Fable.Core
open Feliz
open NextFs

[<ExportDefault>]
[<ReactComponent>]
let Page() =
    let pathname = NavigationClient.usePathname()

    Html.main [
        Html.h1 "Client page"
        Html.code pathname
    ]
```

Wrapper entry:

```json
{
  "directive": "use client",
  "from": "./.fable/App/Page.js",
  "to": "./app/page.js",
  "defaultFromNamed": "Page"
}
```

## Inline server action

Use `Directive.useServer()` inside the function body when the server action is defined inline inside a server component, layout, or dedicated server module.

```fsharp
let savePost (formData: FormDataCollection) =
    Directive.useServer()

    let title =
        match formData.get("title") with
        | Some (:? string as value) -> value
        | _ -> "Untitled"

    title |> ignore
```

## Shared server-action module

If you want a dedicated action module, export named functions and generate a `'use server'` wrapper.

```fsharp
module App.Actions

open NextFs

let createPost (_formData: FormDataCollection) =
    Directive.useServer()
    ()
```

Wrapper entry:

```json
{
  "directive": "use server",
  "from": "./.fable/App/Actions.js",
  "to": "./app/actions.js",
  "named": ["createPost"]
}
```

`use server` wrappers must stay on named exports only. `NextFs` rejects default exports and `export *` for that case.

## Route handlers

Route handlers use JavaScript-shaped arguments and `CompiledName` for HTTP verbs.

```fsharp
module App.Api.Posts

open Fable.Core
open Fable.Core.JsInterop
open NextFs

[<CompiledName("POST")>]
let post (request: NextRequest) =
    async {
        let! payload = Async.AwaitPromise(request.json<obj>())

        return
            ServerResponse.jsonWithInit
                (createObj [ "received" ==> payload ])
                (ResponseInit.create [ ResponseInit.status 201 ])
    }
    |> Async.StartAsPromise
```

## Request and response constructors

`NextFs` exposes constructors for `NextRequest` and `NextResponse`, which is useful for proxy-style code, tests, and lower-level interop.

```fsharp
open Fable.Core.JsInterop
open NextFs

let request =
    ServerRequest.createWithInit "https://example.com/dashboard" (
        NextRequestInit.create [
            NextRequestInit.method' "POST"
            NextRequestInit.body "name=nextfs"
            NextRequestInit.nextConfig (
                NextConfig.create [
                    NextConfig.basePath "/docs"
                    NextConfig.trailingSlash false
                ]
            )
            NextRequestInit.duplexHalf
        ]
    )

let response =
    ServerResponse.createWithInit
        (box """{"ok":true}""")
        (ResponseInit.create [
            ResponseInit.status 202
            ResponseInit.url request.url
        ])
```

## Server-inserted HTML

Use `NavigationClient.useServerInsertedHTML()` when you need the App Router server-inserted HTML hook, for example with CSS-in-JS registries.

```fsharp
[<ReactComponent>]
let StyleRegistry() =
    NavigationClient.useServerInsertedHTML(fun () ->
        Html.style [
            prop.text ".nextfs-registry{display:block;}"
        ])

    Html.div [
        prop.className "nextfs-registry"
        prop.hidden true
    ]
```

## Action mismatch guard

`NavigationClient.unstableIsUnrecognizedActionError()` is useful when a client action call fails because the browser is talking to a different deployment than the server.

```fsharp
let isDeploymentMismatch(error: obj) =
    NavigationClient.unstableIsUnrecognizedActionError error
```

## Selected layout segments

The App Router segment hooks are available with and without `parallelRouteKey`.

```fsharp
let segment = NavigationClient.useSelectedLayoutSegment()
let analyticsSegment = NavigationClient.useSelectedLayoutSegmentFor "analytics"
let children = NavigationClient.useSelectedLayoutSegmentsFor "children"
```

## Image props extraction

If you need the computed `<img>` props instead of the `Image` component directly, use `Image.getImageProps()`.

```fsharp
open Fable.Core.JsInterop

let heroImage =
    Image.getImageProps (
        createObj [
            "src" ==> "/hero.png"
            "alt" ==> "Hero"
            "width" ==> 1280
            "height" ==> 720
        ]
    )
```

## Where to go next

- wrapper rules: [Directives and wrappers](directives-wrappers.md)
- App Router conventions: [Special files](special-files.md)
- project structure reference: [Starter example](https://github.com/Neftedollar/Next.fs/tree/main/examples/nextfs-starter)
- lookup table: [API reference](api-reference.md)
