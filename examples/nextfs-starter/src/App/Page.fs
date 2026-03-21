module App.Page

open Fable.Core
open Feliz
open NextFs
open App.ClientCounter

let savePost (formData: FormDataCollection) =
    Directive.useServer()

    let title =
        match formData.get("title") with
        | Some (:? string as value) when not (System.String.IsNullOrWhiteSpace value) -> value.Trim()
        | _ -> "Untitled"

    let _ = title
    ()

[<ExportDefault>]
let Page() =
    let pathname = Navigation.usePathname()

    Html.main [
        prop.className "mx-auto flex min-h-screen max-w-3xl flex-col gap-8 p-8 text-slate-100"
        prop.children [
            Html.section [
                prop.className "space-y-4"
                prop.children [
                    Html.p [
                        prop.className "text-xs uppercase tracking-[0.2em] text-slate-400"
                        prop.text "NextFs starter"
                    ]
                    Html.h1 [
                        prop.className "text-4xl font-semibold"
                        prop.text "Next.js App Router with F# sources"
                    ]
                    Html.p [
                        prop.className "max-w-2xl text-slate-300"
                        prop.text "The page wrapper is client-side, the layout font comes from next/font in F#, the route handler can mutate cookies, and the project root proxy entry also comes from F#."
                    ]
                    Html.code [
                        prop.className "rounded bg-slate-800 px-2 py-1 text-sm"
                        prop.text pathname
                    ]
                ]
            ]

            ClientCounter()

            Html.section [
                prop.className "rounded-xl border border-slate-700 bg-slate-900 p-4"
                prop.children [
                    Html.p [
                        prop.className "text-sm font-medium text-slate-300"
                        prop.text "Create a post"
                    ]
                    Form.create [
                        Form.serverAction savePost
                        prop.className "mt-4 flex flex-col gap-3 sm:flex-row"
                        prop.children [
                            Html.input [
                                prop.name "title"
                                prop.placeholder "Post title"
                                prop.className "min-w-0 flex-1 rounded-md border border-slate-700 bg-slate-950 px-3 py-2 text-slate-100"
                            ]
                            Html.button [
                                prop.type'.submit
                                prop.className "rounded-md bg-emerald-400 px-4 py-2 font-medium text-slate-950"
                                prop.text "Submit action"
                            ]
                        ]
                    ]
                ]
            ]

            Html.section [
                prop.className "text-sm text-slate-400"
                prop.children [
                    Link.create [
                        Link.href "/api/posts"
                        prop.className "text-emerald-300 hover:underline"
                        prop.text "Open the route handler"
                    ]
                ]
            ]
        ]
    ]
