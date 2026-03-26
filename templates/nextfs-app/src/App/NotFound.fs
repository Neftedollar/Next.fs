[<NextFs.NextFsEntry("app/not-found.js", Default="NotFound")>]
module NextFsApp.App.NotFound

open Feliz
open NextFs

[<ReactComponent>]
let NotFound () =
    Html.main [
        prop.className "max-w-2xl mx-auto p-8 text-center"
        prop.children [
            Html.h1 [
                prop.className "text-4xl font-bold mb-4"
                prop.text "404"
            ]
            Html.p [
                prop.className "text-gray-400 mb-6"
                prop.text "Page not found."
            ]
            Link.create [
                Link.href "/"
                prop.className "text-blue-400 hover:underline"
                prop.text "Go home"
            ]
        ]
    ]
