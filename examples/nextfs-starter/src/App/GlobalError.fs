[<NextFs.NextFsEntry("app/global-error.js", Directive="use client", Default="GlobalError")>]
module App.GlobalError

open Feliz
open NextFs

let GlobalError(props: ErrorBoundaryProps) =
    Html.html [
        prop.lang "en"
        prop.children [
            Html.body [
                prop.className "min-h-screen bg-slate-950 px-8 py-24 text-slate-100"
                prop.children [
                    Html.main [
                        prop.className "mx-auto flex max-w-3xl flex-col gap-4"
                        prop.children [
                            Html.h1 [
                                prop.className "text-3xl font-semibold"
                                prop.text "Global error"
                            ]
                            Html.p [
                                prop.className "max-w-xl text-slate-300"
                                prop.text props.error.message
                            ]
                            Html.button [
                                prop.type'.button
                                prop.className "w-fit rounded-md bg-emerald-400 px-4 py-2 font-medium text-slate-950"
                                prop.onClick (fun _ -> props.``unstable_retry``())
                                prop.text "Retry app shell"
                            ]
                        ]
                    ]
                ]
            ]
        ]
    ]
