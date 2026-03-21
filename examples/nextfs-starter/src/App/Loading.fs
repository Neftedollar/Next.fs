module App.Loading

open Feliz

let Loading() =
    Html.main [
        prop.className "mx-auto flex min-h-screen max-w-3xl items-center justify-center px-8 text-slate-300"
        prop.children [
            Html.div [
                prop.className "rounded-xl border border-slate-800 bg-slate-900 px-6 py-4"
                prop.text "Loading route segment..."
            ]
        ]
    ]
