# API Reference

This page is a compact map of the public surface currently provided by `NextFs`.

It is not intended to replace the source file. Its purpose is to help you find the right module quickly when translating an App Router example from JavaScript or TypeScript into F#.

## Components

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

Related types:

- `AppRouterInstance`
- `NavigateOptions`
- `PrefetchOptions`
- `ReadonlyURLSearchParams`
- `RedirectType`

## Server

### `Server`

Bindings for `next/headers` and request-context helpers from `next/server`.

Common members:

- `Server.headers`
- `Server.cookies`
- `Server.draftMode`
- `Server.connection`

Related types:

- `HeadersCollection`
- `RequestCookieStore`
- `DraftModeState`
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

## Utility types

- `Href.create`
- `Href.pathname`
- `Href.query`
- `Href.hash`
- `ScriptStrategy`
- `ImageLoading`
- `ImagePlaceholder`

## Notes

- Keep route handlers and multi-argument server actions uncurried.
- Use wrapper files for file-level `'use client'` and `'use server'`.
- For a full project layout, see [the starter example](../examples/nextfs-starter/README.md).
