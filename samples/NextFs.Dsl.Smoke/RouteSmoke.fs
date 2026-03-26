module NextFs.Dsl.Smoke.RouteSmoke

open Fable.Core
open Fable.Core.JsInterop
open NextFs
open NextFs.Dsl

[<CompiledName("GET")>]
let get (request: NextRequest, ctx: RouteHandlerContext<{| slug: string |}>) =
    route {
        let! routeParams = PropsAsync.contextParams ctx
        return
            Route.jsonOk (createObj [
                "slug" ==> routeParams.slug
                "pathname" ==> request.nextUrl.pathname
            ])
    }

[<CompiledName("POST")>]
let post (request: NextRequest) =
    route {
        let! payload = request.json<obj>()
        let! cookies = ServerAsync.cookies()
        let! draft = ServerAsync.draftMode()

        ignore (cookies.get "theme")

        if not draft.isEnabled then
            draft.enable()

        return Route.json payload 201
    }

let getText () =
    route {
        return Route.textOk "hello"
    }

let getRedirect () =
    route {
        return Route.redirect "/dashboard"
    }

let getRaw () =
    route {
        return ServerResponse.next()
    }
