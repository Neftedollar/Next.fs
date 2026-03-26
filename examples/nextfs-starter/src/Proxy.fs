[<NextFs.NextFsEntry("proxy.js", Named="proxy")>]
[<NextFs.NextFsStaticExport("config", """{"matcher":["/((?!_next/static|_next/image|favicon.ico).*)"]}""")>]
module Proxy

open Fable.Core
open NextFs

let config =
    ProxyConfig.create [
        ProxyConfig.matcher "/((?!_next/static|_next/image|favicon.ico).*)"
    ]

let proxy (request: NextRequest) (event: NextFetchEvent) =
    let response = ServerResponse.next()

    response.headers.set("x-nextfs-proxy", "enabled")

    ignore (
        response.cookies.set(
            "starter-proxy",
            request.nextUrl.pathname,
            CookieOptions.create [
                CookieOptions.path "/"
                CookieOptions.sameSite CookieSameSite.Lax
            ]
        )
    )

    event.waitUntil(async { return () } |> Async.StartAsPromise)
    response
