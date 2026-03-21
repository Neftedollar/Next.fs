# API Reference

This page is a compact map of the public surface currently provided by `NextFs`.

It is not intended to replace the source file. Its purpose is to help you find the right module quickly when translating an App Router example from JavaScript or TypeScript into F#.

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

All loaders accept a JavaScript-shaped options object created with `FontOptions.create`.

### `Link`

Bindings for `next/link`.

Common members:

- `Link.create`
- `Link.useLinkStatus`
- `Link.href`
- `Link.hrefObject`
- `Link.replace`
- `Link.scroll`
- `Link.prefetch`
- `Link.prefetchAuto`
- `Link.onNavigate`
- `Link.transitionTypes`

### `Image`

Bindings for `next/image`.

Common members:

- `Image.create`
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

Bindings for `next/navigation`.

Common members:

- `Navigation.useRouter`
- `Navigation.usePathname`
- `Navigation.useSearchParams`
- `Navigation.useParams`
- `Navigation.useSelectedLayoutSegment`
- `Navigation.useSelectedLayoutSegments`
- `Navigation.redirect`
- `Navigation.redirectWith`
- `Navigation.permanentRedirect`
- `Navigation.notFound`
- `Navigation.forbidden`
- `Navigation.unauthorized`
- `Navigation.unstableRethrow`

Related types:

- `AppRouterInstance`
- `LinkStatus`
- `NavigateOptions`
- `PrefetchOptions`
- `ReadonlyURLSearchParams`
- `URLSearchParamsCollection`
- `RedirectType`

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

### `ServerResponse`

Static helpers over `NextResponse`.

Common members:

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

### `Proxy`

Bindings used for `proxy.js`.

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
- `Directive.useCachePrivate()`
- `Directive.useCacheRemote()`

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
- `RouteRuntime`
- `LoadedFont`
- `ScriptStrategy`
- `ImageLoading`
- `ImagePlaceholder`

## Notes

- Keep route handlers and multi-argument server actions uncurried.
- Use wrapper files for file-level `'use client'` and `'use server'`.
- For a full project layout, see [the starter example](../examples/nextfs-starter/README.md).
