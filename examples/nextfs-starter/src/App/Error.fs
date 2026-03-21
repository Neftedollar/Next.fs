module App.Error

open Feliz
open Fable.Core
open NextFs

[<CompiledName("Error")>]
let errorBoundary(props: ErrorBoundaryProps) =
    Html.main [
        prop.className "mx-auto flex min-h-screen max-w-3xl flex-col justify-center gap-4 px-8 text-slate-100"
        prop.children [
            Html.h1 [
                prop.className "text-3xl font-semibold"
                prop.text "Segment error"
            ]
            Html.p [
                prop.className "max-w-xl text-slate-300"
                prop.text props.error.message
            ]
            Html.button [
                prop.type'.button
                prop.className "w-fit rounded-md bg-white px-4 py-2 font-medium text-slate-950"
                prop.onClick (fun _ -> props.``unstable_retry``())
                prop.text "Retry segment"
            ]
        ]
    ]
