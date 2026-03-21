module NextFs.Smoke.Route

open Fable.Core
open Fable.Core.JsInterop
open NextFs

[<CompiledName("GET")>]
let get (request: NextRequest, ctx: RouteHandlerContext<{| slug: string |}>) =
    async {
        let! routeParams = Async.AwaitPromise ctx.``params``

        return
            ServerResponse.jsonWithInit
                (createObj [
                    "slug" ==> routeParams.slug
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
        return ServerResponse.json payload
    }
    |> Async.StartAsPromise
