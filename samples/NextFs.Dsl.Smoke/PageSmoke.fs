module NextFs.Dsl.Smoke.PageSmoke

open Feliz
open NextFs
open NextFs.Dsl

let serverPage (props: PageProps<{| slug: string |}, obj>) =
    serverComponent {
        let! routeParams = PropsAsync.routeParams props
        let! headers = ServerAsync.headers()
        let ua = headers.get "user-agent" |> Option.defaultValue "unknown"

        return
            Html.main [
                prop.children [
                    Html.h1 (sprintf "Page: %s" routeParams.slug)
                    Html.pre ua
                ]
            ]
    }

let serverDefault (props: DefaultProps<{| team: string |}>) =
    serverComponent {
        let! routeParams = PropsAsync.defaultParams props
        return
            Html.div [
                prop.text (sprintf "Fallback for %s" routeParams.team)
            ]
    }

let serverLayout (props: LayoutProps<{| lang: string |}>) =
    serverComponent {
        let! routeParams = PropsAsync.layoutParams props
        return
            Html.div [
                prop.lang routeParams.lang
                prop.children [ Html.text "layout" ]
            ]
    }
