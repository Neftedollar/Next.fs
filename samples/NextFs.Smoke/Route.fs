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
        let! draftMode = Async.AwaitPromise(Server.draftMode())
        let! cookieStore = Async.AwaitPromise(Server.cookies())

        ignore (cookieStore.get "theme")
        ignore (cookieStore.getAll())
        ignore (cookieStore.has "theme")

        if not draftMode.isEnabled then
            draftMode.enable()

        Cache.revalidateTagWithProfile "posts" RevalidateTagProfile.Max
        Cache.revalidatePathType "/blog" RevalidatePathType.Page

        let response = ServerResponse.json payload

        ignore (
            response.cookies.set(
                "draft-mode",
                "enabled",
                CookieOptions.create [
                    CookieOptions.path "/"
                    CookieOptions.httpOnly true
                    CookieOptions.sameSite CookieSameSite.Lax
                ]
            )
        )

        ignore (
            response.cookies.delete(
                CookieOptions.create [
                    CookieOptions.name "legacy-posts"
                    CookieOptions.path "/blog"
                ]
            )
        )

        return response
    }
    |> Async.StartAsPromise
