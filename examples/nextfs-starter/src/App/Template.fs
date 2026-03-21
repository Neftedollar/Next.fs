module App.Template

open Fable.Core
open Feliz
open NextFs

[<ExportDefault>]
let Template(props: TemplateProps) =
    Html.div [
        prop.className "min-h-screen"
        prop.children [ props.children ]
    ]
