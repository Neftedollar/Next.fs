---
title: Instrumentation
layout: default
nav_order: 8
---

# Instrumentation

`NextFs` can also drive the root Next.js instrumentation entry files from F#:

- `instrumentation.js`
- `instrumentation-client.js`

These are root conventions, like `proxy.js`, so the generated wrapper files live at the project root rather than under `app/**`.

## Server instrumentation

Use a normal F# module that exports `register` and, optionally, `onRequestError`.

```fsharp
module Instrumentation

open NextFs

let register() =
    ()

let onRequestError(error: InstrumentationError, request: InstrumentationRequest, context: InstrumentationContext) =
    ignore error.message
    ignore error.digest
    ignore request.path
    ignore context.routeType
```

Wrapper entry:

```json
{
  "from": "./.fable/Instrumentation.js",
  "to": "./instrumentation.js",
  "named": ["register", "onRequestError"]
}
```

## Client instrumentation

`instrumentation-client.js` is a side-effect entry. A thin `export *` wrapper is enough because importing the compiled Fable module will run its top-level code and re-export any optional named hooks.

```fsharp
module InstrumentationClient

open Fable.Core
open NextFs

[<Emit("performance.mark('nextfs-client-bootstrap')")>]
let private markBootstrap() : unit = jsNative

do markBootstrap()

let onRouterTransitionStart(url: string, navigationType: RouterTransitionType) =
    ignore url
    ignore navigationType
```

Wrapper entry:

```json
{
  "from": "./.fable/InstrumentationClient.js",
  "to": "./instrumentation-client.js",
  "exportAll": true
}
```

## Types

Server instrumentation helpers:

- `InstrumentationError`
- `InstrumentationRequest`
- `InstrumentationContext`
- `InstrumentationRouterKind`
- `InstrumentationRouteType`
- `InstrumentationRenderSource`
- `InstrumentationRevalidateReason`
- `InstrumentationRenderType`

Client instrumentation helper:

- `RouterTransitionType`

## Notes

- `instrumentation.js` and `instrumentation-client.js` do not need `'use client'` or `'use server'` wrappers.
- For multi-argument exports such as `onRequestError`, keep the F# signature JavaScript-shaped.
- `instrumentation-client.js` is best modeled as a root module with top-level side effects plus optional named exports.

## Related

- root request interception: [proxy.js / Proxy](directives-wrappers.md)
- mixed App Router flows: [Server and client patterns](server-client-patterns.md)
- example layout: [Starter example](https://github.com/Neftedollar/Next.fs/tree/main/examples/nextfs-starter)
