module NextFs.Smoke.Proxy

open Fable.Core
open Fable.Core.JsInterop
open NextFs

let config =
    ProxyConfig.create [
        ProxyConfig.matchersMany [
            box "/dashboard/:path*"
            ProxyMatcher.create [
                ProxyMatcher.source "/((?!api|_next/static|_next/image|favicon.ico|sitemap.xml|robots.txt).*)"
                ProxyMatcher.has [
                    RouteHas.create [
                        RouteHas.type' RouteHasType.Header
                        RouteHas.key "x-nextfs"
                    ]
                ]
                ProxyMatcher.missing [
                    RouteHas.create [
                        RouteHas.type' RouteHasType.Cookie
                        RouteHas.key "session"
                        RouteHas.value "active"
                    ]
                ]
            ]
        ]
        ProxyConfig.regionsMany [ "iad1"; "fra1" ]
        ProxyConfig.allowDynamicMany [ "**/node_modules/sharp/**" ]
    ]

let proxy (request: NextRequest) (event: NextFetchEvent) =
    let requestCookie =
        createObj [
            "name" ==> "session"
            "value" ==> "active"
        ]
        |> unbox<RequestCookie>

    ignore (request.cookies.get requestCookie)
    ignore (request.cookies.getAll())
    ignore (request.cookies.getAll requestCookie)
    ignore (request.cookies.set requestCookie)
    ignore (request.cookies.delete [| "legacy-session"; "preview-token" |])

    let forwardedUrl = request.nextUrl.clone()
    forwardedUrl.pathname <- "/dashboard"
    forwardedUrl.searchParams.set("from", request.nextUrl.pathname)

    let response = ServerResponse.next()

    response.headers.set("x-nextfs-proxy", "enabled")

    ignore (
        response.cookies.set(
            "session",
            "active",
            CookieOptions.create [
                CookieOptions.path "/"
                CookieOptions.httpOnly true
                CookieOptions.sameSite CookieSameSite.Lax
                CookieOptions.priority CookiePriority.High
            ]
        )
    )

    ignore (
        response.cookies.set(
            createObj [
                "name" ==> "theme"
                "value" ==> "dark"
                "path" ==> "/"
            ]
            |> unbox<ResponseCookie>
        )
    )

    ignore (
        response.cookies.delete(
            CookieOptions.create [
                CookieOptions.name "legacy-theme"
                CookieOptions.path "/"
            ]
        )
    )

    event.waitUntil(async { return () } |> Async.StartAsPromise)

    if request.cookies.has "session" then
        response
    else
        ServerResponse.rewrite (box forwardedUrl)
