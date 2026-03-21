# Package Design and Limitations

`NextFs` is a binding package, not a framework layer.

## Design

The package mirrors the current Next.js App Router surface in a small set of modules and types:

- component helpers: `Link`, `Image`, `Script`, `Form`, `Head`
- font helpers: `Font`, `GoogleFont`, `FontOptions`, `LocalFontSource`, `FontDeclaration`
- navigation helpers: `Navigation`
- client reporting helpers: `WebVitals`
- server helpers: `Server`
- proxy helpers: `ProxyConfig`, `ProxyMatcher`, `RouteHas`
- cache helpers: `Cache`, `CacheLife`
- metadata helpers: `Metadata`, `MetadataOpenGraph`, `MetadataTwitter`, `Viewport`, `MetadataRoute`, `ImageResponse`, `ImageMetadata`
- response helpers: `ServerResponse`, `ResponseInit`, `NextResponseInit`
- request and route types: `NextRequest`, `NextResponse`, `RouteHandlerContext<'T>`
- utility types: `Href`, `ScriptStrategy`, `RedirectType`, `ImageLoading`, `ImagePlaceholder`, `CacheProfile`, `RevalidatePathType`, `RevalidateTagProfile`, `RouteRuntime`

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

The package also keeps most configuration values JavaScript-shaped on purpose:

- metadata, proxy, cookie, and font option objects are built with `createObj`-style helper modules
- `GoogleFont` is generated from official Next.js type declarations, but the option objects are still intentionally loose
- this keeps the bindings maintainable as Next.js evolves, at the cost of not encoding every per-font constraint in the F# type system

The wrapper generator is also deliberately narrow:

- it only understands file-level `"use client"` and `"use server"` directives
- it only writes re-export wrappers
- it does not transform compiled F# output

For concrete end-to-end examples of the supported patterns, use:

- `samples/NextFs.Smoke` for compile-smoke coverage of the public API
- `examples/nextfs-starter` for a minimal App Router project structure
