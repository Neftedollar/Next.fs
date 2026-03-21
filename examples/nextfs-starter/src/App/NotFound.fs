module App.NotFound

open Fable.Core
open Feliz
open NextFs

[<ExportDefault>]
let NotFound() =
    Html.main [
        prop.className "mx-auto flex min-h-screen max-w-3xl flex-col justify-center gap-4 px-8 text-slate-100"
        prop.children [
            Html.h1 [
                prop.className "text-4xl font-semibold"
                prop.text "404"
            ]
            Html.p [
                prop.className "max-w-xl text-slate-300"
                prop.text "The requested route segment could not be resolved."
            ]
            Link.create [
                Link.href "/"
                prop.className "text-emerald-300 hover:underline"
                prop.text "Return home"
            ]
        ]
    ]
