---
title: API Reference
layout: default
nav_order: 7
---

# API Reference

This page is a compact map of the public surface currently provided by `NextFs`.

It is not intended to replace the source file. Its purpose is to help you find the right module quickly when translating an App Router example from JavaScript or TypeScript into F#.

Items marked **experimental** depend on Next.js `experimental` flags or `unstable_*` upstream functions and may change without a NextFs major bump.

## Components

### `Font`

Bindings for `next/font/local`.

Common members:

- `Font.local`

Related helpers and types:

- `FontOptions`
- `LocalFontSource`
- `FontDeclaration`
- `LoadedFont`
- `FontStyleObject`
- `FontDisplay`

### `GoogleFont`

Generated named bindings for `next/font/google`.

Examples:

- `GoogleFont.Inter`
- `GoogleFont.Roboto`
- `GoogleFont.Roboto_Mono`

For real `next/font` entry modules, prefer anonymous-record or object-literal options so Next.js can statically analyze the loader call.

### `Link`

Bindings for `next/link`.

Common members:

- `Link.create`
- `Link.href`
- `Link.hrefObject`
- `Link.replace`
- `Link.scroll`
- `Link.prefetch`
- `Link.prefetchAuto`
- `Link.onNavigate`
- `Link.transitionTypes`

### `LinkClient`

Client-only bindings for `next/link`.

Common members:

- `LinkClient.useLinkStatus`

### `Image`

Bindings for `next/image`.

Common members:

- `Image.create`
- `Image.getImageProps`
- `Image.src`
- `Image.alt`
- `Image.width`
- `Image.height`
- `Image.fill`
- `Image.sizes`
- `Image.quality`
- `Image.preload`
- `Image.priority`
- `Image.loading`
- `Image.placeholder`
- `Image.blurDataUrl`
- `Image.unoptimized`
- `Image.overrideSrc`

Related types:

- `ImagePropsResult`

### `Script`

Bindings for `next/script`.

Common members:

- `Script.create`
- `Script.src`
- `Script.strategy`
- `Script.id`
- `Script.onLoad`
- `Script.onReady`
- `Script.onError`

### `Form`

Bindings for `next/form`.

Common members:

- `Form.create`
- `Form.action`
- `Form.serverAction`
- `Form.replace`
- `Form.scroll`
- `Form.prefetch`

### `Head`

Bindings for `next/head`.

Common member:

- `Head.create`

## Navigation

### `Navigation`

Server-safe bindings for `next/navigation`.

Common members:

- `Navigation.redirect`
- `Navigation.redirectWith`
- `Navigation.permanentRedirect`
- `Navigation.notFound`
- `Navigation.forbidden` — **experimental**, requires `experimental.authInterrupts: true`
- `Navigation.unauthorized` — **experimental**, requires `experimental.authInterrupts: true`
- `Navigation.unstableRethrow`

Related types:

- `AppRouterInstance`
- `LinkStatus`
- `NavigateOptions`
- `PrefetchOptions`
- `ReadonlyURLSearchParams`
- `URLSearchParamsCollection`
- `RedirectType`

These map to the App Router special files:

- `Navigation.notFound()` -> `not-found.js`
- `Navigation.forbidden()` -> `forbidden.js`
- `Navigation.unauthorized()` -> `unauthorized.js`

### `NavigationClient`

Client-only bindings for App Router hooks.

Common members:

- `NavigationClient.useRouter`
- `NavigationClient.usePathname`
- `NavigationClient.useSearchParams`
- `NavigationClient.useParams`
- `NavigationClient.useSelectedLayoutSegment`
- `NavigationClient.useSelectedLayoutSegments`
- `NavigationClient.useSelectedLayoutSegmentFor`
- `NavigationClient.useSelectedLayoutSegmentsFor`
- `NavigationClient.useServerInsertedHTML`
- `NavigationClient.unstableIsUnrecognizedActionError` — **experimental**, wraps upstream `unstable_*` function

### `WebVitals`

Bindings for `next/web-vitals`.

Common members:

- `WebVitals.useReportWebVitals`

Related types:

- `WebVitalMetric`
- `WebVitalMetricRating`
- `WebVitalNavigationType`

## Server

### `Server`

Bindings for `next/headers` and request-context helpers from `next/server`.

Common members:

- `Server.after`
- `Server.afterAsync`
- `Server.headers`
- `Server.cookies`
- `Server.draftMode`
- `Server.connection`
- `Server.userAgent`
- `Server.userAgentFromString`

Related types:

- `HeadersCollection`
- `RequestCookieStore`
- `CookieOptions`
- `CookiePriority`
- `CookieSameSite`
- `DraftModeState`
- `UserAgentInfo`
- `UserAgentBrowser`
- `UserAgentDevice`
- `UserAgentEngine`
- `UserAgentOperatingSystem`
- `UserAgentCpu`
- `NextRequest`
- `NextUrl`
- `FormDataCollection`

### `ServerRequest`

Constructors for `NextRequest`.

Common members:

- `ServerRequest.create`
- `ServerRequest.createFrom`
- `ServerRequest.createWithInit`
- `ServerRequest.createFromWithInit`

Related helpers and types:

- `NextRequestInit`
- `NextConfig`

### `ServerResponse`

Static helpers over `NextResponse`.

Common members:

- `ServerResponse.create`
- `ServerResponse.createWithBody`
- `ServerResponse.createWithInit`
- `ServerResponse.json`
- `ServerResponse.jsonWithInit`
- `ServerResponse.redirect`
- `ServerResponse.redirectWithInit`
- `ServerResponse.rewrite`
- `ServerResponse.rewriteWithInit`
- `ServerResponse.next`
- `ServerResponse.nextWithInit`

Related types:

- `NextResponse`
- `ResponseInit`
- `NextResponseInit`
- `ResponseCookieStore`

`NextRequest` and `NextResponse` also expose common Web `Request` / `Response` members used in route handlers and proxy flows:

- `clone`
- `arrayBuffer`
- `blob`
- `formData`
- `bodyUsed`

### `ServerFetch`

Bindings for Next.js server `fetch()` usage.

Common members:

- `ServerFetch.fetch`
- `ServerFetch.fetchWithInit`
- `ServerFetch.fetchFrom`
- `ServerFetch.fetchFromWithInit`

Related helpers and types:

- `ServerFetchInit`
- `NextFetchOptions`
- `ServerFetchResponse`
- `ServerFetchCache`
- `Revalidate`

### `Proxy`

Bindings used for `proxy.js`. **Experimental** — proxy support varies by Next.js version.

Common members:

- `ProxyConfig.create`
- `ProxyConfig.matcher`
- `ProxyConfig.matchers`
- `ProxyConfig.matchersMany`
- `ProxyConfig.regions`
- `ProxyConfig.regionsMany`
- `ProxyConfig.allowDynamic`
- `ProxyConfig.allowDynamicMany`
- `ProxyMatcher.create`
- `ProxyMatcher.source`
- `ProxyMatcher.localeFalse`
- `ProxyMatcher.has`
- `ProxyMatcher.missing`
- `RouteHas.create`
- `RouteHas.type'`
- `RouteHas.key`
- `RouteHas.value`

Related types:

- `NextFetchEvent`
- `RouteHasType`

### `Instrumentation`

Types intended for root `instrumentation.js` and `instrumentation-client.js` files.

Common types:

- `InstrumentationError`
- `InstrumentationRequest`
- `InstrumentationContext`
- `InstrumentationRouterKind`
- `InstrumentationRouteType`
- `InstrumentationRenderSource`
- `InstrumentationRevalidateReason`
- `InstrumentationRenderType`
- `RouterTransitionType`

## Special Files

Types intended for App Router special files:

- `ErrorBoundaryProps`
- `ErrorWithDigest`
- `TemplateProps`
- `DefaultProps<'T>`

These are typically used with:

- `error.js`
- `global-error.js`
- `template.js`
- `default.js`

### Route handlers

Use:

- `NextRequest`
- `RouteHandlerContext<'T>`
- `[<CompiledName("GET")>]`, `[<CompiledName("POST")>]`, and similar attributes

## Cache

### `Directive`

Inline directives currently provided:

- `Directive.useServer()`
- `Directive.useCache()`
- `Directive.useCachePrivate()` — **experimental**, requires `experimental.dynamicIO`
- `Directive.useCacheRemote()` — **experimental**, requires `experimental.dynamicIO`

### `Cache`

Bindings for `next/cache`.

Common members:

- `Cache.cacheLifeProfile`
- `Cache.cacheLifeProfileName`
- `Cache.cacheLife`
- `Cache.cacheTag`
- `Cache.cacheTags`
- `Cache.refresh`
- `Cache.revalidatePath`
- `Cache.revalidatePathType`
- `Cache.revalidateTag`
- `Cache.revalidateTagWithProfile`
- `Cache.revalidateTagWithCustomProfile`
- `Cache.updateTag`
- `Cache.noStore`

Related helpers and types:

- `CacheLife.create`
- `CacheLife.stale`
- `CacheLife.revalidate`
- `CacheLife.expire`
- `CacheProfile`
- `RevalidatePathType`
- `RevalidateTagProfile`

## Metadata And Special Files

### `Metadata`

Top-level metadata object builders for layout/page exports.

Common members:

- `Metadata.create`
- `Metadata.title`
- `Metadata.titleTemplate`
- `Metadata.description`
- `Metadata.applicationName`
- `Metadata.generator`
- `Metadata.keywords`
- `Metadata.creator`
- `Metadata.publisher`
- `Metadata.metadataBase`
- `Metadata.alternates`
- `Metadata.openGraph`
- `Metadata.twitter`
- `Metadata.robots`
- `Metadata.manifest`
- `Metadata.icons`
- `Metadata.verification`
- `Metadata.other`

Related helper modules:

- `MetadataTitle`
- `MetadataImage`
- `MetadataAlternates`
- `MetadataOpenGraph`
- `MetadataTwitter`
- `MetadataVerification`
- `ThemeColor`
- `Viewport`

### `MetadataRoute`

Builders for App Router metadata route files.

Common submodules:

- `MetadataRoute.RobotsRule`
- `MetadataRoute.Robots`
- `MetadataRoute.SitemapEntry`
- `MetadataRoute.ManifestIcon`
- `MetadataRoute.Manifest`

Related types:

- `MetadataRoute.SitemapChangeFrequency`

### `ImageMetadata`

Builders for `generateImageMetadata` results.

Common members:

- `ImageMetadata.create`
- `ImageMetadata.id`
- `ImageMetadata.alt`
- `ImageMetadata.size`
- `ImageMetadata.contentType`

Related helpers:

- `ImageMetadataSize`
- `ImageGenerationProps<'routeParams, 'id>`

### `ImageResponse`

Bindings for `ImageResponse` from `next/og`.

Common members:

- `ImageResponse.create`
- `ImageResponse.createWithOptions`

Related helpers and types:

- `ImageResponseOptions`
- `ImageResponseFont`
- `ImageResponseEmoji`
- `ImageResponseFontStyle`

### Page And Layout Props

Utility interfaces for async App Router props:

- `PageProps<'routeParams, 'searchParams>`
- `LayoutProps<'routeParams>`
- `ResolvingMetadata`
- `ResolvingViewport`
- `SitemapProps`

## Utility types

- `Href.create`
- `Href.pathname`
- `Href.query`
- `Href.hash`
- `PreferredRegion.auto`
- `PreferredRegion.globalRegion`
- `PreferredRegion.home`
- `PreferredRegion.region`
- `PreferredRegion.regions`
- `GenerateSitemapsEntry.create`
- `GenerateSitemapsEntry.id`
- `Revalidate.forever`
- `Revalidate.neverCache`
- `Revalidate.seconds`
- `RouteRuntime`
- `LoadedFont`
- `ScriptStrategy`
- `ImageLoading`
- `ImagePlaceholder`

## Tooling Attributes

### `NextFsEntryAttribute`

Marks an F# module as a Next.js App Router entry point. The scanner (`tools/nextfs-scan.mjs`) reads this attribute to auto-generate `nextfs.entries.json`.

Must be placed immediately before the `module` declaration, before any `open` statements. Use the fully-qualified form `[<NextFs.NextFsEntry(...)>]`.

```fsharp
[<NextFs.NextFsEntry("app/page.js", Directive="use client", Default="Page")>]
module App.Page
```

Parameters:

- `output` (positional): wrapper output path, e.g. `"app/page.js"`
- `Directive`: `"use client"` or `"use server"` (optional)
- `Default`: named F# binding to re-export as the JavaScript default
- `Named`: space-separated list of named exports, e.g. `"metadata viewport"`
- `ExportAll`: `true` to emit `export * from ...`

### `NextFsStaticExportAttribute`

Adds a static literal export to the generated wrapper. Use when Next.js needs a statically-analyzable literal that Fable cannot guarantee to produce.

```fsharp
[<NextFs.NextFsEntry("proxy.js", Named="proxy")>]
[<NextFs.NextFsStaticExport("config", """{"matcher":["/((?!_next).*)"]}""")>]
module Proxy
```

Parameters:

- `name` (positional): export name, e.g. `"config"`
- `json` (positional): JSON string to inline as the exported value

## Notes

- Keep route handlers and multi-argument server actions uncurried.
- Annotate entry modules with `[<NextFs.NextFsEntry>]` and run `npm run scan` instead of writing `nextfs.entries.json` by hand.
- Use wrapper files for file-level `'use client'` and `'use server'`.
- For a full project layout, see [the starter example](https://github.com/Neftedollar/Next.fs/tree/main/examples/nextfs-starter).
