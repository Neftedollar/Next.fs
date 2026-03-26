/// Root page — client component.
/// Marked 'use client' because it imports Counter which uses Elmish hooks.
/// Next.js treats the entire import tree of this entry as client-side code.
[<NextFs.NextFsEntry("app/page.js", Directive="use client", Default="Page")>]
module App.Page

open Feliz
open App.Counter

let Page () =
    Html.main [
        prop.style [
            style.minHeight (length.vh 100)
            style.display.flex
            style.flexDirection.column
            style.alignItems.center
            style.justifyContent.center
            style.gap 32
            style.padding 32
        ]
        prop.children [
            Html.div [
                prop.style [ style.textAlign.center ]
                prop.children [
                    Html.h1 [
                        prop.style [ style.margin 0; style.fontSize 22; style.fontWeight 700 ]
                        prop.text "NextFs + Elmish"
                    ]
                    Html.p [
                        prop.style [ style.margin (8, 0, 0, 0); style.color "#64748b"; style.fontSize 13 ]
                        prop.text "Elmish MVU running inside a Next.js App Router client component"
                    ]
                ]
            ]
            Counter()
        ]
    ]
