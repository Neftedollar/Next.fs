module NextFs.Smoke.DataFetching

open Fable.Core
open Fable.Core.JsInterop
open NextFs

let runtime = RouteRuntime.Edge
let preferredRegion = PreferredRegion.home
let dynamicParams = true
let maxDuration = 30

let generateSitemaps() =
    [|
        GenerateSitemapsEntry.create [
            GenerateSitemapsEntry.id 0
        ]
        GenerateSitemapsEntry.create [
            GenerateSitemapsEntry.id "archive"
        ]
    |]

let loadPosts() =
    async {
        let! response =
            Async.AwaitPromise(
                ServerFetch.fetchWithInit "https://example.com/api/posts" (
                    ServerFetchInit.create [
                        ServerFetchInit.method' "GET"
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

        let! payload =
            Async.AwaitPromise(
                response.json<{| items: {| slug: string; updatedAt: string |} array |}>()
            )

        ignore response.ok
        ignore (response.headers.get "content-type")

        return payload.items
    }
    |> Async.StartAsPromise

let sitemap (props: SitemapProps) =
    async {
        let! sitemapId = Async.AwaitPromise props.id
        let! posts = Async.AwaitPromise(loadPosts())

        return
            posts
            |> Array.map (fun post ->
                MetadataRoute.SitemapEntry.create [
                    MetadataRoute.SitemapEntry.url (sprintf "https://example.com/%s/%s" sitemapId post.slug)
                    MetadataRoute.SitemapEntry.lastModified post.updatedAt
                ])
    }
    |> Async.StartAsPromise
