module App.Api.Posts

open Fable.Core
open Fable.Core.JsInterop
open NextFs

[<CompiledName("GET")>]
let get (request: NextRequest) =
    async {
        let! cookieStore = Async.AwaitPromise(Server.cookies())

        return
            ServerResponse.jsonWithInit
                (createObj [
                    "ok" ==> true
                    "pathname" ==> request.nextUrl.pathname
                    "theme" ==> (cookieStore.get "theme" |> Option.map _.value |> Option.defaultValue "system")
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

        let response =
            ServerResponse.jsonWithInit
                (createObj [
                    "received" ==> payload
                ])
                (ResponseInit.create [
                    ResponseInit.status 201
                ])

        ignore (
            response.cookies.set(
                "last-posted-at",
                System.DateTime.UtcNow.ToString("O"),
                CookieOptions.create [
                    CookieOptions.path "/"
                    CookieOptions.httpOnly true
                    CookieOptions.sameSite CookieSameSite.Lax
                ]
            )
        )

        return response
    }
    |> Async.StartAsPromise
