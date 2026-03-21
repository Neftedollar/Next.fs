module App.Forbidden

open Fable.Core
open Feliz

[<ExportDefault>]
let Forbidden() =
    Html.main [
        prop.className "mx-auto flex min-h-screen max-w-3xl flex-col justify-center gap-4 px-8 text-slate-100"
        prop.children [
            Html.h1 [
                prop.className "text-4xl font-semibold"
                prop.text "403"
            ]
            Html.p [
                prop.className "max-w-xl text-slate-300"
                prop.text "This resource exists, but the current user is not allowed to access it."
            ]
        ]
    ]
