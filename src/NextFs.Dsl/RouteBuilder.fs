namespace NextFs.Dsl

open Fable.Core
open Fable.Core.JsInterop
open NextFs

type RouteBuilder() =
    member inline _.Zero() = async.Return(ServerResponse.create())
    member inline _.Return(x: NextResponse) = async.Return(x)
    member inline _.ReturnFrom(x: Async<NextResponse>) = x
    member inline _.Bind(x: Async<'T>, f: 'T -> Async<NextResponse>) = async.Bind(x, f)
    member inline _.Bind(x: JS.Promise<'T>, f: 'T -> Async<NextResponse>) = async.Bind(Async.AwaitPromise x, f)
    member inline _.Delay(f: unit -> Async<NextResponse>) = async.Delay(f)
    member inline _.Combine(a: Async<NextResponse>, b: Async<NextResponse>) = async.Bind(a, fun _ -> b)
    member inline _.TryWith(body: Async<NextResponse>, handler: exn -> Async<NextResponse>) = async.TryWith(body, handler)
    member inline _.TryFinally(body: Async<NextResponse>, compensation: unit -> unit) = async.TryFinally(body, compensation)
    member inline _.Using(resource: 'T :> System.IDisposable, binder: 'T -> Async<NextResponse>) = async.Using(resource, binder)
    member inline _.Run(computation: Async<NextResponse>) : JS.Promise<NextResponse> =
        computation |> Async.StartAsPromise

[<AutoOpen>]
module RouteBuilderAutoOpen =

    let route = RouteBuilder()

    module Route =

        let inline json (body: 'T) (status: int) : NextResponse =
            ServerResponse.jsonWithInit body (ResponseInit.create [ ResponseInit.status status ])

        let inline jsonOk (body: 'T) : NextResponse =
            ServerResponse.json body

        let inline text (body: string) (status: int) : NextResponse =
            ServerResponse.createWithInit
                (box body)
                (ResponseInit.create [
                    ResponseInit.status status
                    ResponseInit.headersObject (Fable.Core.JsInterop.createObj [ "content-type" ==> "text/plain" ])
                ])

        let inline textOk (body: string) : NextResponse =
            text body 200

        let inline redirect (url: string) : NextResponse =
            ServerResponse.redirect (box url)

        let inline rewrite (url: string) : NextResponse =
            ServerResponse.rewrite (box url)
