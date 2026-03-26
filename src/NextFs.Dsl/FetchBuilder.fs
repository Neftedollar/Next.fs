namespace NextFs.Dsl

open Fable.Core
open Fable.Core.JsInterop
open NextFs

type FetchState =
    { Url: string
      Cache: ServerFetchCache option
      Revalidate: obj option
      Tags: string list option }

type FetchBuilder(url: string) =

    member _.Yield(_: unit) : FetchState =
        { Url = url; Cache = None; Revalidate = None; Tags = None }

    [<CustomOperation("noStore")>]
    member _.NoStore(state: FetchState) =
        { state with Cache = Some ServerFetchCache.NoStore }

    [<CustomOperation("forceCache")>]
    member _.ForceCache(state: FetchState) =
        { state with Cache = Some ServerFetchCache.ForceCache }

    [<CustomOperation("cache")>]
    member _.Cache(state: FetchState, value: ServerFetchCache) =
        { state with Cache = Some value }

    [<CustomOperation("revalidate")>]
    member _.Revalidate(state: FetchState, seconds: int) =
        { state with Revalidate = Some (Revalidate.seconds seconds) }

    [<CustomOperation("revalidateNever")>]
    member _.RevalidateNever(state: FetchState) =
        { state with Revalidate = Some Revalidate.neverCache }

    [<CustomOperation("revalidateForever")>]
    member _.RevalidateForever(state: FetchState) =
        { state with Revalidate = Some Revalidate.forever }

    [<CustomOperation("tags")>]
    member _.Tags(state: FetchState, values: string list) =
        { state with Tags = Some values }

    [<CustomOperation("tag")>]
    member _.Tag(state: FetchState, value: string) =
        { state with Tags = Some [ value ] }

    member _.Run(state: FetchState) : Async<ServerFetchResponse> =
        let initFields = ResizeArray<string * obj>()

        state.Cache |> Option.iter (fun c ->
            initFields.Add(ServerFetchInit.cache c))

        let nextFields = ResizeArray<string * obj>()

        state.Revalidate |> Option.iter (fun r ->
            nextFields.Add(NextFetchOptions.revalidate r))

        state.Tags |> Option.iter (fun t ->
            nextFields.Add(NextFetchOptions.tags t))

        if nextFields.Count > 0 then
            initFields.Add(
                ServerFetchInit.next (
                    NextFetchOptions.create (Seq.toList nextFields)))

        let promise =
            if initFields.Count > 0 then
                ServerFetch.fetchWithInit state.Url (ServerFetchInit.create (Seq.toList initFields))
            else
                ServerFetch.fetch state.Url

        Async.AwaitPromise promise

[<AutoOpen>]
module FetchBuilderAutoOpen =

    let fetch (url: string) = FetchBuilder(url)
