[<NextFs.NextFsEntry("app/global-not-found.js", Default="GlobalNotFound", Named="metadata")>]
module App.GlobalNotFound

open Feliz
open NextFs

let inter =
    GoogleFont.Inter(
        box {|
            subsets = [| "latin" |]
            display = "swap"
        |}
    )

let metadata =
    Metadata.create [
        Metadata.title "Not Found | NextFs Starter"
        Metadata.description "Global not found page rendered directly from F#."
    ]

let GlobalNotFound() =
    Html.html [
        prop.lang "en"
        prop.className inter.className
        prop.children [
            Html.body [
                prop.className "min-h-screen bg-slate-950 px-8 py-24 text-slate-100"
                prop.children [
                    Html.main [
                        prop.className "mx-auto flex max-w-3xl flex-col gap-4"
                        prop.children [
                            Html.h1 [
                                prop.className "text-4xl font-semibold"
                                prop.text "Global 404"
                            ]
                            Html.p [
                                prop.className "max-w-xl text-slate-300"
                                prop.text "No route matched this URL, so Next.js rendered the global-not-found entry."
                            ]
                        ]
                    ]
                ]
            ]
        ]
    ]
