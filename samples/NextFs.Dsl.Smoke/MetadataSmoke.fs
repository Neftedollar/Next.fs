module NextFs.Dsl.Smoke.MetadataSmoke

open Fable.Core
open NextFs
open NextFs.Dsl

let simpleMetadata =
    metadata {
        title "NextFs"
        description "Fable bindings for Next.js"
    }

let fullMetadata =
    let og =
        openGraph {
            title "NextFs Smoke"
            description "App Router bindings"
            ogType "website"
            siteName "NextFs"
            url "https://example.com"
            locale "en_US"
        }
    let tw =
        twitter {
            card "summary_large_image"
            title "NextFs Smoke"
            description "App Router bindings"
            site "@neftedollar"
            creator "@neftedollar"
        }
    metadata {
        titleTemplate "NextFs" "%s | NextFs"
        description "Compile-smoke coverage for DSL metadata."
        applicationName "NextFs Smoke"
        keywords [ "fable"; "nextjs"; "fsharp" ]
        creator "Roman Melnikov"
        publisher "Neftedollar"
        manifest "/manifest.json"
        openGraph og
        twitter tw
    }

let simpleViewport =
    viewport {
        themeColor "#111827"
        colorScheme "dark light"
    }

let fullViewport =
    viewport {
        width "device-width"
        initialScale 1.0
        maximumScale 2.0
        userScalable true
    }

let generateMetadata (props: PageProps<{| slug: string |}, obj>) =
    async {
        let! routeParams = PropsAsync.routeParams props
        return
            metadata {
                title (sprintf "%s | NextFs" routeParams.slug)
            }
    }
    |> Async.StartAsPromise
