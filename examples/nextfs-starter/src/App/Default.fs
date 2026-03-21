module App.Default

open Fable.Core
open Feliz
open NextFs

let Default(props: DefaultProps<obj>) =
    async {
        let! _ = Async.AwaitPromise props.``params``

        return
            Html.main [
                prop.className "mx-auto flex min-h-screen max-w-3xl items-center justify-center px-8 text-slate-300"
                prop.text "Fallback UI for a recovered parallel-route state."
            ]
    }
    |> Async.StartAsPromise
