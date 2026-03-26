[<NextFs.NextFsEntry("app/actions.js", Directive="use server", Named="createPost")>]
module App.Actions

open System
open Fable.Core.JsInterop
open NextFs

let createPost (formData: FormDataCollection) =
    Directive.useServer()

    let title =
        match formData.get("title") with
        | Some (:? string as value) when not (String.IsNullOrWhiteSpace value) -> value.Trim()
        | _ -> "Untitled"

    let publishedAt = DateTime.UtcNow.ToString("O")
    createObj [
        "title" ==> title
        "publishedAt" ==> publishedAt
    ]
