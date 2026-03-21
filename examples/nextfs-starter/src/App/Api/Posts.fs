module App.Api.Posts

open Fable.Core
open Fable.Core.JsInterop
open NextFs

[<CompiledName("GET")>]
let get (request: NextRequest) =
    async {
        return
            ServerResponse.jsonWithInit
                (createObj [
                    "ok" ==> true
                    "pathname" ==> request.nextUrl.pathname
                ])
                (ResponseInit.create [
                    ResponseInit.status 200
                ])
    }
    |> Async.StartAsPromise

[<CompiledName("POST")>]
let post (request: NextRequest) =
    async {
        let! payload = Async.AwaitPromise(request.json<obj>())

        return
            ServerResponse.jsonWithInit
                (createObj [
                    "received" ==> payload
                ])
                (ResponseInit.create [
                    ResponseInit.status 201
                ])
    }
    |> Async.StartAsPromise
