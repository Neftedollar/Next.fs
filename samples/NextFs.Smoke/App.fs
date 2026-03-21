module NextFs.Smoke.App

open Fable.Core
open Fable.Core.JsInterop
open Feliz
open NextFs

let saveSearch (_formData: obj) =
    Directive.useServer()
    ()

[<ReactComponent>]
let NavigationMenu() =
    let pathname = Navigation.usePathname()

    Html.nav [
        prop.className "navigation-menu"
        prop.children [
            Link.create [
                Link.href "/"
                prop.text "Home"
            ]
            Link.create [
                Link.hrefObject (
                    Href.create [
                        Href.pathname "/search"
                        Href.query (createObj [ "q" ==> "fable" ])
                    ]
                )
                prop.text "Search"
            ]
            Html.code pathname
        ]
    ]

[<ReactComponent>]
let SearchForm() =
    Form.create [
        Form.serverAction saveSearch
        Form.prefetch true
        prop.children [
            Html.input [
                prop.name "q"
                prop.placeholder "Search"
            ]
            Html.button [
                prop.type'.submit
                prop.text "Go"
            ]
        ]
    ]

[<ExportDefault>]
let Page() =
    Html.main [
        prop.children [
            Html.h1 "NextFs smoke test"
            NavigationMenu()
            Image.create [
                Image.src "/hero.png"
                Image.alt "Hero image"
                Image.width 960
                Image.height 540
                Image.preload true
            ]
            SearchForm()
        ]
    ]
