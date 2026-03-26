namespace NextFs.Dsl

open Fable.Core
open NextFs

[<AutoOpen>]
module AsyncHelpers =

    /// Async wrappers for Next.js server request helpers.
    module ServerAsync =

        let inline headers () : Async<HeadersCollection> =
            Async.AwaitPromise(Server.headers())

        let inline cookies () : Async<RequestCookieStore> =
            Async.AwaitPromise(Server.cookies())

        let inline draftMode () : Async<DraftModeState> =
            Async.AwaitPromise(Server.draftMode())

        let inline connection () : Async<unit> =
            Async.AwaitPromise(Server.connection())

    /// Async wrappers for awaiting Next.js props that arrive as promises.
    module PropsAsync =

        let inline routeParams (props: PageProps<'routeParams, 'searchParams>) : Async<'routeParams> =
            Async.AwaitPromise props.``params``

        let inline searchParams (props: PageProps<'routeParams, 'searchParams>) : Async<'searchParams> =
            Async.AwaitPromise props.searchParams

        let inline layoutParams (props: LayoutProps<'routeParams>) : Async<'routeParams> =
            Async.AwaitPromise props.``params``

        let inline defaultParams (props: DefaultProps<'routeParams>) : Async<'routeParams> =
            Async.AwaitPromise props.``params``

        let inline contextParams (ctx: RouteHandlerContext<'routeParams>) : Async<'routeParams> =
            Async.AwaitPromise ctx.``params``
