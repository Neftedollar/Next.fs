[<NextFs.NextFsEntry("app/layout.js", Default="Layout", Named="metadata")>]
module App.Layout

open Feliz
open NextFs

let metadata =
    Metadata.create [
        Metadata.title "NextFs Elmish"
        Metadata.description "Elmish MVU pattern in a Next.js App Router project powered by F# and Fable."
    ]

let Layout (props: LayoutProps<obj>) =
    Html.html [
        prop.lang "en"
        prop.children [
            Html.body [
                prop.style [
                    style.margin 0
                    style.fontFamily "system-ui, -apple-system, sans-serif"
                    style.backgroundColor "#0f172a"
                    style.color "#f1f5f9"
                ]
                prop.children [ props.children ]
            ]
        ]
    ]
