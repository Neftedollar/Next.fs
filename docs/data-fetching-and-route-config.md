---
title: Data Fetching & Route Config
layout: default
nav_order: 3
---

# Data Fetching And Route Config

This page covers two parts of App Router work that are easy to lose when translating examples from TypeScript to F#:

- server `fetch()` options
- route segment config exports

## Server Fetch

Next.js extends server `fetch()` with cache-aware options:

- `cache`
- `next.revalidate`
- `next.tags`

`NextFs` exposes those through:

- `ServerFetch.fetch`
- `ServerFetch.fetchWithInit`
- `ServerFetch.fetchFrom`
- `ServerFetch.fetchFromWithInit`
- `ServerFetchInit`
- `NextFetchOptions`
- `ServerFetchCache`
- `Revalidate`

Example:

```fsharp
let loadPosts() =
    async {
        let! response =
            Async.AwaitPromise(
                ServerFetch.fetchWithInit "https://example.com/api/posts" (
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

        return! Async.AwaitPromise(response.json<{| items: obj array |}>())
    }
    |> Async.StartAsPromise
```

Current semantics are based on the official `fetch` reference for Next.js 16.2.1:

- `cache: "force-cache"` reads and populates the server-side cache
- `cache: "no-store"` opts the request out of persistence
- `next.revalidate` accepts `false`, `0`, or a number of seconds
- `next.tags` attaches cache tags used by `revalidateTag`

Source: [Next.js fetch API reference](https://nextjs.org/docs/app/api-reference/functions/fetch)

## Route Segment Config

For current App Router route segment exports, plain F# values are usually enough:

```fsharp
let runtime = RouteRuntime.Edge
let preferredRegion = PreferredRegion.home
let dynamicParams = true
let maxDuration = 30
```

Those values can be exported from:

- `layout` files
- `page` files
- `route` handlers

Current documented config surface in Next.js 16.2.1:

- `dynamicParams`
- `runtime`
- `preferredRegion`
- `maxDuration`

Source: [Next.js route segment config reference](https://nextjs.org/docs/app/api-reference/file-conventions/route-segment-config)

## Route Handlers And Wrappers

If a route handler is wrapped through `nextfs.entries.json`, remember that the generated wrapper must re-export the config values too:

```json
{
  "from": "./.fable/App/Api/Posts.js",
  "to": "./app/api/posts/route.js",
  "named": ["GET", "POST", "runtime", "preferredRegion", "maxDuration"]
}
```

Without those names in the wrapper manifest, Next.js will only see the HTTP verb exports.

## Multiple Sitemaps

`generateSitemaps()` returns an array of objects with an `id` property. `NextFs` provides a small helper for that shape:

```fsharp
let generateSitemaps() =
    [|
        GenerateSitemapsEntry.create [
            GenerateSitemapsEntry.id 0
        ]
        GenerateSitemapsEntry.create [
            GenerateSitemapsEntry.id "archive"
        ]
    |]
```

The corresponding sitemap file can read the promised `id` through `SitemapProps`:

```fsharp
let sitemap (props: SitemapProps) =
    async {
        let! sitemapId = Async.AwaitPromise props.id

        return
            [|
                MetadataRoute.SitemapEntry.create [
                    MetadataRoute.SitemapEntry.url (sprintf "https://example.com/%s" sitemapId)
                ]
            |]
    }
    |> Async.StartAsPromise
```

Source: [Next.js generateSitemaps reference](https://nextjs.org/docs/app/api-reference/functions/generate-sitemaps)

## Notes

- `SitemapProps.id` is a promise of `string` in current Next.js 16 behavior.
- Route handler `fetch` requests are not memoized as part of the React tree render pass.
- Wrapper files remain a packaging layer only; they do not replace the underlying Next.js conventions.
