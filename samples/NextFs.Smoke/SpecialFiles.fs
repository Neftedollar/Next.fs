module NextFs.Smoke.SpecialFiles

open Fable.Core
open Feliz
open NextFs

let template (props: TemplateProps) =
    Html.section [
        prop.className "template-shell"
        prop.children [ props.children ]
    ]

let defaultRoute (props: DefaultProps<{| team: string |}>) =
    async {
        let! routeParams = Async.AwaitPromise props.``params``

        return
            Html.div [
                prop.text (sprintf "Fallback slot for %s" routeParams.team)
            ]
    }
    |> Async.StartAsPromise

let loading() =
    Html.p "Loading segment..."

let notFound() =
    Html.main [
        prop.children [
            Html.h1 "Not found"
            Html.p "The requested resource was not found."
        ]
    ]

let forbidden() =
    Html.main [
        prop.children [
            Html.h1 "Forbidden"
            Html.p "You do not have access to this route."
        ]
    ]

let unauthorized() =
    Html.main [
        prop.children [
            Html.h1 "Unauthorized"
            Html.p "Sign in to continue."
        ]
    ]

[<ReactComponent>]
let ErrorView(props: ErrorBoundaryProps) =
    Html.div [
        prop.children [
            Html.h1 "Route error"
            Html.p props.error.message
            Html.button [
                prop.type'.button
                prop.onClick (fun _ -> props.``unstable_retry``())
                prop.text "Retry"
            ]
        ]
    ]

let globalError (props: ErrorBoundaryProps) =
    Html.html [
        prop.lang "en"
        prop.children [
            Html.body [
                prop.children [
                    Html.h1 "Global error"
                    Html.p props.error.message
                    Html.button [
                        prop.type'.button
                        prop.onClick (fun _ -> props.reset())
                        prop.text "Reset"
                    ]
                ]
            ]
        ]
    ]
