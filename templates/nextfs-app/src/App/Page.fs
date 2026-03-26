[<NextFs.NextFsEntry("app/page.js", Default="Page")>]
module NextFsApp.App.Page

open Fable.Core
open Feliz
open NextFs
open NextFs.Dsl

[<ExportDefault>]
let Page () =
    serverComponent {
        let! headers = ServerAsync.headers ()
        let ua = headers.get "user-agent" |> Option.defaultValue "unknown"

        return
            Html.main [
                prop.className "max-w-2xl mx-auto p-8"
                prop.children [
                    Html.h1 [
                        prop.className "text-4xl font-bold mb-4"
                        prop.text "NextFsApp"
                    ]
                    Html.p [
                        prop.className "text-gray-400 mb-8"
                        prop.text "Built with F#, Fable, and Next.js App Router."
                    ]
                    Html.pre [
                        prop.className "bg-gray-800 rounded p-4 text-sm"
                        prop.text (sprintf "User-Agent: %s" ua)
                    ]
                    Link.create [
                        Link.href "/api/hello"
                        prop.className "inline-block mt-6 text-blue-400 hover:underline"
                        prop.text "Try the API route →"
                    ]
                ]
            ]
    }
