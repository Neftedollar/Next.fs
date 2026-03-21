module NextFs.Smoke.Metadata

open Fable.Core
open Fable.Core.JsInterop
open Feliz
open NextFs

let metadata =
    Metadata.create [
        Metadata.titleTemplate (
            MetadataTitle.create [
                MetadataTitle.defaultValue "NextFs"
                MetadataTitle.template "%s | NextFs"
            ]
        )
        Metadata.description "Compile-smoke coverage for metadata bindings."
        Metadata.applicationName "NextFs Smoke"
        Metadata.keywords [ "fable"; "nextjs"; "fsharp" ]
        Metadata.openGraph (
            MetadataOpenGraph.create [
                MetadataOpenGraph.title "NextFs Smoke"
                MetadataOpenGraph.description "App Router bindings"
                MetadataOpenGraph.imagesMany [
                    MetadataImage.create [
                        MetadataImage.url "https://example.com/og.png"
                        MetadataImage.width 1200
                        MetadataImage.height 630
                        MetadataImage.alt "NextFs social image"
                    ]
                ]
                MetadataOpenGraph.type' "website"
            ]
        )
        Metadata.twitter (
            MetadataTwitter.create [
                MetadataTwitter.card "summary_large_image"
                MetadataTwitter.title "NextFs Smoke"
                MetadataTwitter.description "App Router bindings"
            ]
        )
        Metadata.verification (
            MetadataVerification.create [
                MetadataVerification.google "google-site-verification-token"
            ]
        )
    ]

let viewport =
    Viewport.create [
        Viewport.themeColors [
            ThemeColor.create [
                ThemeColor.media "(prefers-color-scheme: light)"
                ThemeColor.color "#ffffff"
            ]
            ThemeColor.create [
                ThemeColor.media "(prefers-color-scheme: dark)"
                ThemeColor.color "#111827"
            ]
        ]
        Viewport.colorScheme "dark light"
        Viewport.initialScale 1.0
        Viewport.viewportFit "cover"
    ]

let runtime = RouteRuntime.NodeJs
let preferredRegion = PreferredRegion.regions [ "fra1"; "iad1" ]
let dynamicParams = false
let maxDuration = 30

let generateStaticParams() =
    [|
        {| slug = "hello" |}
        {| slug = "docs" |}
    |]

let generateMetadata
    (props: PageProps<{| slug: string |}, {| preview: string option |}>)
    (_parent: ResolvingMetadata)
    =
    async {
        let! routeParams = Async.AwaitPromise props.``params``

        return
            Metadata.create [
                Metadata.title (sprintf "%s | NextFs" routeParams.slug)
            ]
    }
    |> Async.StartAsPromise

let generateViewport (props: PageProps<{| slug: string |}, obj>) =
    async {
        let! routeParams = Async.AwaitPromise props.``params``

        return
            Viewport.create [
                Viewport.themeColor (
                    if routeParams.slug = "docs" then "#10b981" else "#111827"
                )
            ]
    }
    |> Async.StartAsPromise

let robots() =
    MetadataRoute.Robots.create [
        MetadataRoute.Robots.rulesMany [
            MetadataRoute.RobotsRule.create [
                MetadataRoute.RobotsRule.userAgent "*"
                MetadataRoute.RobotsRule.allow "/"
                MetadataRoute.RobotsRule.disallow "/private"
                MetadataRoute.RobotsRule.crawlDelay 5
            ]
        ]
        MetadataRoute.Robots.sitemap "https://example.com/sitemap.xml"
        MetadataRoute.Robots.host "https://example.com"
    ]

let sitemap() =
    [|
        MetadataRoute.SitemapEntry.create [
            MetadataRoute.SitemapEntry.url "https://example.com"
            MetadataRoute.SitemapEntry.changeFrequency MetadataRoute.SitemapChangeFrequency.Daily
            MetadataRoute.SitemapEntry.priority 1.0
            MetadataRoute.SitemapEntry.images [ "https://example.com/og.png" ]
        ]
    |]

let manifest() =
    MetadataRoute.Manifest.create [
        MetadataRoute.Manifest.name "NextFs Starter"
        MetadataRoute.Manifest.shortName "NextFs"
        MetadataRoute.Manifest.description "Fable bindings for Next.js"
        MetadataRoute.Manifest.startUrl "/"
        MetadataRoute.Manifest.display "standalone"
        MetadataRoute.Manifest.backgroundColor "#111827"
        MetadataRoute.Manifest.themeColor "#10b981"
        MetadataRoute.Manifest.icons [
            MetadataRoute.ManifestIcon.create [
                MetadataRoute.ManifestIcon.src "/icon-192.png"
                MetadataRoute.ManifestIcon.sizes "192x192"
                MetadataRoute.ManifestIcon.type' "image/png"
            ]
        ]
    ]

let generateImageMetadata() =
    [|
        ImageMetadata.create [
            ImageMetadata.id "small"
            ImageMetadata.size (
                ImageMetadataSize.create [
                    ImageMetadataSize.width 48
                    ImageMetadataSize.height 48
                ]
            )
            ImageMetadata.contentType "image/png"
        ]
        ImageMetadata.create [
            ImageMetadata.id "large"
            ImageMetadata.alt "NextFs icon"
            ImageMetadata.size (
                ImageMetadataSize.create [
                    ImageMetadataSize.width 72
                    ImageMetadataSize.height 72
                ]
            )
            ImageMetadata.contentType "image/png"
        ]
    |]

let icon (props: ImageGenerationProps<{| slug: string |}, string>) =
    async {
        let! imageId = Async.AwaitPromise props.id
        let! routeParams = Async.AwaitPromise props.``params``

        return
            ImageResponse.createWithOptions
                (Html.div [
                    prop.text (sprintf "Icon %s for %s" imageId routeParams.slug)
                ])
                (ImageResponseOptions.create [
                    ImageResponseOptions.width 1200
                    ImageResponseOptions.height 630
                    ImageResponseOptions.emoji ImageResponseEmoji.Twemoji
                ])
    }
    |> Async.StartAsPromise
