# Package Design and Limitations

`NextFs` is a binding package, not a framework layer.

## Design

The package mirrors the current Next.js App Router surface in a small set of modules and types:

- component helpers: `Link`, `Image`, `Script`, `Form`, `Head`
- navigation helpers: `Navigation`
- server helpers: `Server`
- cache helpers: `Cache`, `CacheLife`
- response helpers: `ServerResponse`, `ResponseInit`, `NextResponseInit`
- request and route types: `NextRequest`, `NextResponse`, `RouteHandlerContext<'T>`
- utility types: `Href`, `ScriptStrategy`, `RedirectType`, `ImageLoading`, `ImagePlaceholder`, `CacheProfile`, `RevalidatePathType`, `RevalidateTagProfile`

The goal is to keep the F# API close to the underlying JavaScript API, so existing Next.js examples can usually be translated without inventing new concepts.

## Runtime dependencies

The binding package declares the runtime packages that a consumer app must already have:

- `next`
- `react`
- `react-dom`

The current package metadata targets:

- `next >= 15.0.0 < 17.0.0`
- `react >= 18.2.0 < 20.0.0`
- `react-dom >= 18.2.0 < 20.0.0`

## App Router Expectations

The package is aimed at App Router usage.

That means:

- server APIs are promise-based and should be awaited in F# using `Async.AwaitPromise`
- route handlers should use uncurried parameters, such as `let get (request: NextRequest, ctx: RouteHandlerContext<_>) = ...`
- HTTP verb exports should use `[<CompiledName("GET")>]`, `[<CompiledName("POST")>]`, and similar attributes
- server actions should either use `Directive.useServer()` inline or a generated wrapper file
- cacheable functions can use `Directive.useCache()` and related helpers from `next/cache`

## Current Limits

The package surface is intentionally small and does not try to cover every Next.js feature.

What is covered today is the set of bindings already present in `src/NextFs/NextFs.fs`. If you need an API that is not there, treat that as unsupported rather than inferred.

The wrapper generator is also deliberately narrow:

- it only understands file-level `"use client"` and `"use server"` directives
- it only writes re-export wrappers
- it does not transform compiled F# output

For concrete end-to-end examples of the supported patterns, use:

- `samples/NextFs.Smoke` for compile-smoke coverage of the public API
- `examples/nextfs-starter` for a minimal App Router project structure
