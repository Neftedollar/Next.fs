module NextFs.Smoke.Advanced

open Fable.Core
open Feliz
open NextFs

let protectPage isAllowed =
    if not isAllowed then
        Navigation.forbidden()

let requireSignIn isSignedIn =
    if not isSignedIn then
        Navigation.unauthorized()

let rethrow(error: obj) =
    Navigation.unstableRethrow error

let proxyLike (request: NextRequest) =
    let agent = Server.userAgent request
    let parsedAgent = Server.userAgentFromString "Mozilla/5.0 (NextFs Smoke)"

    Server.after(fun () ->
        ignore agent.isBot
        ignore parsedAgent.browser.name
        ignore request.nextUrl.basePath)

    if agent.isBot then
        ServerResponse.next()
    else
        ServerResponse.rewrite (box request.nextUrl)

[<ReactComponent>]
let ClientInstrumentation() =
    let status = Link.useLinkStatus()

    WebVitals.useReportWebVitals(fun metric ->
        ignore metric.name
        ignore metric.navigationType
        ignore metric.rating)

    Html.p [
        prop.text (
            if status.pending then
                "Pending navigation"
            else
                "Idle navigation"
        )
    ]
