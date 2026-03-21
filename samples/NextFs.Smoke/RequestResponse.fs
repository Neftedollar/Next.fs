module NextFs.Smoke.RequestResponse

open Fable.Core
open Fable.Core.JsInterop
open Feliz
open NextFs

let request =
    ServerRequest.createWithInit "https://example.com/dashboard?tab=analytics" (
        NextRequestInit.create [
            NextRequestInit.method' "POST"
            NextRequestInit.headersObject (createObj [ "x-nextfs" ==> "smoke" ])
            NextRequestInit.body "name=nextfs"
            NextRequestInit.nextConfig (
                NextConfig.create [
                    NextConfig.basePath "/docs"
                    NextConfig.trailingSlash false
                ]
            )
            NextRequestInit.duplexHalf
        ]
    )

let response =
    ServerResponse.createWithInit
        (box """{"ok":true}""")
        (ResponseInit.create [
            ResponseInit.status 202
            ResponseInit.statusText "Accepted"
            ResponseInit.headersObject (createObj [ "content-type" ==> "application/json" ])
            ResponseInit.url "https://example.com/dashboard"
        ])

let forwardedResponse =
    ServerResponse.nextWithInit (
        NextResponseInit.create [
            NextResponseInit.requestHeaders request.headers
        ]
    )

let imageProps =
    Image.getImageProps (
        createObj [
            "src" ==> "/hero.png"
            "alt" ==> "Hero"
            "width" ==> 960
            "height" ==> 540
        ]
    )

let isActionMismatch(error: obj) =
    Navigation.unstableIsUnrecognizedActionError error

[<ReactComponent>]
let ServerInsertedStyles(parallelRouteKey: string) =
    let activeSegment = Navigation.useSelectedLayoutSegmentFor parallelRouteKey
    let activeSegments = Navigation.useSelectedLayoutSegmentsFor parallelRouteKey

    Navigation.useServerInsertedHTML(fun () ->
        Html.style [
            prop.text ".nextfs-request-response-smoke{display:block;}"
        ])

    Html.div [
        prop.className "nextfs-request-response-smoke"
        prop.hidden true
        prop.title (
            System.String.Join(
                "/",
                [|
                    activeSegment |> Option.defaultValue "none"
                    System.String.Join("/", activeSegments)
                |]
            )
        )
    ]

let inspect() =
    let clonedRequest = request.clone()
    let clonedResponse = response.clone()

    ignore request.bodyUsed
    ignore response.bodyUsed
    ignore response.ok
    ignore response.redirected
    ignore clonedRequest
    ignore clonedResponse
    ignore imageProps.props
    ignore forwardedResponse.headers
    ignore (isActionMismatch (box (System.Exception "boom")))
