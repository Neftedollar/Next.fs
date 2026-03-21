module App.Unauthorized

open Feliz

let Unauthorized() =
    Html.main [
        prop.className "mx-auto flex min-h-screen max-w-3xl flex-col justify-center gap-4 px-8 text-slate-100"
        prop.children [
            Html.h1 [
                prop.className "text-4xl font-semibold"
                prop.text "401"
            ]
            Html.p [
                prop.className "max-w-xl text-slate-300"
                prop.text "Please authenticate before continuing."
            ]
            Html.button [
                prop.type'.button
                prop.className "w-fit rounded-md bg-emerald-400 px-4 py-2 font-medium text-slate-950"
                prop.text "Mock sign-in"
            ]
        ]
    ]
