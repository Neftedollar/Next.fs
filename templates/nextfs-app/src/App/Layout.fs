[<NextFs.NextFsEntry("app/layout.js", Default="Layout", Named="metadata viewport")>]
module NextFsApp.App.Layout

open Feliz
open NextFs.Dsl

let og =
    openGraph {
        title "NextFsApp"
        ogType "website"
    }

let metadata =
    metadata {
        title "NextFsApp"
        description "Built with NextFs and Fable"
        openGraph og
    }

let viewport =
    viewport {
        themeColor "#111827"
        colorScheme "dark light"
    }

[<ReactComponent>]
let Layout (props: {| children: ReactElement |}) =
    Html.html [
        prop.lang "en"
        prop.children [
            Html.body [
                prop.className "bg-gray-900 text-gray-100 min-h-screen"
                prop.children [ props.children ]
            ]
        ]
    ]
