module NextFs.Smoke.App

open Fable.Core
open Fable.Core.JsInterop
open Feliz
open NextFs

let saveSearch (_formData: obj) =
    Directive.useServer()
    Cache.updateTag "searches"
    Cache.revalidatePath "/"
    Cache.refresh()
    ()

let loadNavigationLabels () =
    Directive.useCache()
    Cache.cacheLifeProfile CacheProfile.Hours
    Cache.cacheTags [ "navigation"; "searches" ]

    [|
        "Home"
        "Search"
        "Docs"
    |]

[<ReactComponent>]
let NavigationMenu() =
    let pathname = Navigation.usePathname()
    let navigationLabels = loadNavigationLabels ()

    Html.nav [
        prop.className "navigation-menu"
        prop.children [
            Html.ul [
                prop.children [
                    for label in navigationLabels do
                        Html.li [
                            prop.key label
                            prop.text label
                        ]
                ]
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
