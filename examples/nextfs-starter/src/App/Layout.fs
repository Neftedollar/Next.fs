[<NextFs.NextFsEntry("app/layout.js", Default="Layout", Named="metadata viewport")>]
module App.Layout

open Feliz
open NextFs

let inter =
    GoogleFont.Inter(
        box {|
            subsets = [| "latin" |]
            display = "swap"
            variable = "--font-inter"
        |}
    )

let metadata =
    Metadata.create [
        Metadata.titleTemplate (
            MetadataTitle.create [
                MetadataTitle.defaultValue "NextFs Starter"
                MetadataTitle.template "%s | NextFs Starter"
            ]
        )
        Metadata.description "Minimal Next.js App Router starter driven by F# source files."
        Metadata.openGraph (
            MetadataOpenGraph.create [
                MetadataOpenGraph.title "NextFs Starter"
                MetadataOpenGraph.description "A minimal Fable + Next.js App Router skeleton."
                MetadataOpenGraph.type' "website"
            ]
        )
        Metadata.twitter (
            MetadataTwitter.create [
                MetadataTwitter.card "summary_large_image"
                MetadataTwitter.title "NextFs Starter"
                MetadataTwitter.description "A minimal Fable + Next.js App Router skeleton."
            ]
        )
    ]

let viewport =
    Viewport.create [
        Viewport.themeColors [
            ThemeColor.create [
                ThemeColor.media "(prefers-color-scheme: light)"
                ThemeColor.color "#f8fafc"
            ]
            ThemeColor.create [
                ThemeColor.media "(prefers-color-scheme: dark)"
                ThemeColor.color "#020617"
            ]
        ]
        Viewport.colorScheme "dark light"
        Viewport.viewportFit "cover"
    ]

let Layout(props: LayoutProps<obj>) =
    Html.html [
        prop.lang "en"
        prop.className (inter.variable |> Option.defaultValue "")
        prop.children [
            Html.body [
                prop.className (
                    System.String.Join(
                        " ",
                        [|
                            inter.className
                            "min-h-screen bg-slate-950 text-slate-100"
                        |]
                    )
                )
                prop.children [ props.children ]
            ]
        ]
    ]
