namespace NextFs.Dsl

open Fable.Core
open Feliz

type ServerComponentBuilder() =
    member inline _.Zero() = async.Return(Html.none)
    member inline _.Return(x: ReactElement) = async.Return(x)
    member inline _.ReturnFrom(x: Async<ReactElement>) = x
    member inline _.Bind(x: Async<'T>, f: 'T -> Async<ReactElement>) = async.Bind(x, f)
    member inline _.Bind(x: JS.Promise<'T>, f: 'T -> Async<ReactElement>) = async.Bind(Async.AwaitPromise x, f)
    member inline _.Delay(f: unit -> Async<ReactElement>) = async.Delay(f)
    member inline _.Combine(a: Async<ReactElement>, b: Async<ReactElement>) = async.Bind(a, fun _ -> b)
    member inline _.TryWith(body: Async<ReactElement>, handler: exn -> Async<ReactElement>) = async.TryWith(body, handler)
    member inline _.TryFinally(body: Async<ReactElement>, compensation: unit -> unit) = async.TryFinally(body, compensation)
    member inline _.Using(resource: 'T :> System.IDisposable, binder: 'T -> Async<ReactElement>) = async.Using(resource, binder)
    member inline _.Run(computation: Async<ReactElement>) : JS.Promise<ReactElement> =
        computation |> Async.StartAsPromise

[<AutoOpen>]
module PageHelpersAutoOpen =

    let serverComponent = ServerComponentBuilder()
