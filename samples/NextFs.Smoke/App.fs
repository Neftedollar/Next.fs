module NextFs.Smoke.App

open Fable.Core
open Fable.Core.JsInterop
open Feliz
open NextFs

let inter =
    GoogleFont.Inter (
        FontOptions.create [
            FontOptions.subsets [ "latin" ]
            FontOptions.display FontDisplay.Swap
            FontOptions.variable "--font-inter"
        ]
    )

let brandFont =
    Font.local (
        FontOptions.create [
            FontOptions.src "./fonts/brand.woff2"
            FontOptions.weight "400"
            FontOptions.variable "--font-brand"
            FontOptions.declarations [
                FontDeclaration.create [
                    FontDeclaration.prop "ascent-override"
                    FontDeclaration.value "90%"
                ]
            ]
        ]
    )

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
    let pathname = NavigationClient.usePathname()
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
    ignore inter.style.fontFamily
    ignore brandFont.style.fontFamily

    Html.main [
        prop.className (
            System.String.Join(
                " ",
                [|
                    inter.className
                    inter.variable |> Option.defaultValue ""
                    brandFont.variable |> Option.defaultValue ""
                |]
            )
        )
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
