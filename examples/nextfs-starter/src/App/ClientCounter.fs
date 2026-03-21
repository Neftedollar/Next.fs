module App.ClientCounter

open Fable.Core
open Feliz
open NextFs

[<ExportDefault>]
[<ReactComponent>]
let ClientCounter() =
    let router = Navigation.useRouter()
    let segments = Navigation.useSelectedLayoutSegments()
    let segmentSummary =
        match segments with
        | [||] -> "(root)"
        | values -> System.String.Join("/", values)

    ignore (Navigation.unstableIsUnrecognizedActionError (box (System.Exception "demo")))

    Html.section [
        prop.className "rounded-xl border border-slate-700 bg-slate-900 p-4 text-slate-100"
        prop.children [
            Html.p [
                prop.className "text-xs uppercase tracking-[0.2em] text-slate-400"
                prop.text "Client component wrapper"
            ]
            Html.h2 [
                prop.className "mt-2 text-lg font-semibold"
                prop.text "Refresh the route from the client"
            ]
            Html.p [
                prop.className "mt-2 text-sm text-slate-400"
                prop.text $"Active segments: {segmentSummary}"
            ]
            Html.button [
                prop.type'.button
                prop.className "mt-4 rounded-md bg-white px-3 py-2 text-sm font-medium text-slate-900"
                prop.onClick (fun _ -> router.refresh())
                prop.text "Refresh route"
            ]
        ]
    ]
