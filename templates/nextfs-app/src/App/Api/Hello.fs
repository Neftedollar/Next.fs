[<NextFs.NextFsEntry("app/api/hello/route.js", Named="GET")>]
module NextFsApp.App.Api.Hello

open Fable.Core
open Fable.Core.JsInterop
open NextFs
open NextFs.Dsl

[<CompiledName("GET")>]
let get (request: NextRequest, _ctx: RouteHandlerContext<obj>) =
    route {
        let! data =
            fetch "https://httpbin.org/get" {
                noStore
            }

        let! (json: obj) = Async.AwaitPromise (data.json ())

        return
            Route.jsonOk (
                createObj [
                    "message" ==> "Hello from NextFs!"
                    "pathname" ==> request.nextUrl.pathname
                    "origin" ==> json?origin
                ]
            )
    }
