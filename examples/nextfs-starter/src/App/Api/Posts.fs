[<NextFs.NextFsEntry("app/api/posts/route.js", Named="GET POST runtime preferredRegion maxDuration")>]
module App.Api.Posts

open Fable.Core
open Fable.Core.JsInterop
open NextFs

let runtime = RouteRuntime.NodeJs
let preferredRegion = PreferredRegion.home
let maxDuration = 15

[<CompiledName("GET")>]
let get (request: NextRequest) =
    async {
        let! cookieStore = Async.AwaitPromise(Server.cookies())
        let! upstream =
            Async.AwaitPromise(
                ServerFetch.fetchWithInit "https://example.com/api/posts" (
                    ServerFetchInit.create [
                        ServerFetchInit.cache ServerFetchCache.NoStore
                        ServerFetchInit.next (
                            NextFetchOptions.create [
                                NextFetchOptions.revalidate Revalidate.neverCache
                                NextFetchOptions.tag "starter-posts"
                            ]
                        )
                    ]
                )
            )

        ignore upstream.status

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
