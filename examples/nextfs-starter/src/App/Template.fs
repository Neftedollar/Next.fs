module App.Template

open Feliz
open NextFs

let Template(props: TemplateProps) =
    Html.div [
        prop.className "min-h-screen"
        prop.children [ props.children ]
    ]
