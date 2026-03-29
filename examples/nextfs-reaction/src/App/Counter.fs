/// Reactive counter component using AsyncRx streams.
///
/// This module is NOT an entry point — it is imported by App.Page,
/// which carries the 'use client' directive. Everything imported
/// transitively by a 'use client' entry is treated as client-side code
/// by Next.js, so no separate wrapper is needed here.
///
/// NOTE: We use FSharp.Control.AsyncRx directly (the core of
/// Fable.Reaction) instead of the Fable.Reaction wrapper package,
/// because Fable.Reaction 3.x depends on old Fable.React 6.x whose
/// ReactElement type is incompatible with modern Feliz/Fable.React.Types.
module App.Counter

open Feliz
open FSharp.Control

// ---------------------------------------------------------------------------
// Model
// ---------------------------------------------------------------------------

type SaveStatus =
    | Idle
    | Saving
    | Saved of int

type Model = {
    Count: int
    AutoRun: bool
    SaveStatus: SaveStatus
}

// ---------------------------------------------------------------------------
// Messages
// ---------------------------------------------------------------------------

type Msg =
    | Increment
    | Decrement
    | Reset
    | ToggleAuto
    | AutoTick
    | Save of int
    | SaveStarted
    | SaveComplete of int

// ---------------------------------------------------------------------------
// Update (pure — no Cmd, side-effects live in the stream)
// ---------------------------------------------------------------------------

let private initialModel = { Count = 0; AutoRun = false; SaveStatus = Idle }

let private update (model: Model) (msg: Msg) =
    match msg with
    | Increment    -> { model with Count = model.Count + 1; SaveStatus = Idle }
    | Decrement    -> { model with Count = max 0 (model.Count - 1); SaveStatus = Idle }
    | Reset        -> initialModel
    | ToggleAuto   -> { model with AutoRun = not model.AutoRun }
    | AutoTick     -> { model with Count = model.Count + 1 }
    | Save _       -> model
    | SaveStarted  -> { model with SaveStatus = Saving }
    | SaveComplete n -> { model with SaveStatus = Saved n }

// ---------------------------------------------------------------------------
// Stream (reactive side-effects replace Cmd)
// ---------------------------------------------------------------------------

/// Simulates persisting the count to a server.
///
/// Replace the Async.Sleep body with a real server action call:
///
///   let! saved = Async.AwaitPromise(ServerActions.saveCount count)
///   return saved
///
/// See the example README for the full server action pattern.
let private simulateSave (count: int) : Async<int> =
    async {
        do! Async.Sleep 600
        return count
    }

/// Build the reactive pipeline. Returns (processed observable, tag).
///
/// The tag string controls re-subscription: when it changes the caller
/// disposes the previous subscription and subscribes to the new stream.
/// This is equivalent to the TaggedStream concept in Fable.Reaction.
///
/// Key advantages over Elmish Cmd:
/// - flatMapLatest cancels an in-flight save if a new one arrives
/// - AsyncRx.interval creates a timer stream merged into messages
/// - Tag-based re-subscription reconfigures streams on model change
let private buildStream (model: Model) (msgs: IAsyncObservable<Msg>) =
    // Save messages: flatMapLatest cancels an in-flight save if a new
    // one arrives — something that requires manual tracking in Elmish.
    //
    // Equivalent to:
    //   asyncRx { yield SaveStarted
    //             let! saved = AsyncRx.ofAsync (simulateSave count)
    //             yield SaveComplete saved }
    let saves =
        msgs
        |> AsyncRx.choose (function Save count -> Some count | _ -> None)
        |> AsyncRx.flatMapLatest (fun count ->
            AsyncRx.ofAsync (simulateSave count)
            |> AsyncRx.map SaveComplete
            |> AsyncRx.startWith [ SaveStarted ])

    // Non-save messages pass through unchanged.
    let others =
        msgs
        |> AsyncRx.choose (function Save _ -> None | msg -> Some msg)

    let combined = AsyncRx.merge saves others

    // When AutoRun is toggled the tag changes ("manual" / "auto"),
    // causing re-subscription. The auto branch merges a 1-second
    // interval stream that emits AutoTick messages.
    if model.AutoRun then
        let ticks =
            AsyncRx.interval 0 1000
            |> AsyncRx.map (fun _ -> AutoTick)
        combined |> AsyncRx.merge ticks, "auto"
    else
        combined, "manual"

// ---------------------------------------------------------------------------
// Component
// ---------------------------------------------------------------------------

[<ReactComponent>]
let Counter () =
    let model, dispatch = React.useReducer(update, initialModel)

    // AsyncRx subject bridges imperative dispatch → reactive stream.
    // Created once via useMemo; the observer pushes, the observable pulls.
    let pair = React.useMemo((fun () -> AsyncRx.subject<Msg>()), [||])
    let observer = fst pair
    let msgObservable = snd pair

    // Build stream pipeline and extract tag for re-subscription control.
    let (processed, tag) = buildStream model msgObservable

    // Convert IAsyncObservable to IObservable so we can subscribe
    // synchronously inside a React effect. Memoised by tag so the
    // observable identity is stable between renders with the same tag.
    let syncObs =
        React.useMemo(
            (fun () -> processed |> AsyncRx.toObservable),
            [| tag :> obj |])

    // Subscribe to the stream; re-subscribe when tag changes.
    let subRef = React.useRef(Option<System.IDisposable>.None)

    React.useEffect(
        (fun () ->
            subRef.current |> Option.iter (fun d -> d.Dispose())
            let sub = syncObs |> Observable.subscribe dispatch
            subRef.current <- Some sub),
        [| syncObs :> obj |])

    // Cleanup final subscription on unmount.
    React.useEffectOnce(fun () ->
        { new System.IDisposable with
            member _.Dispose() =
                subRef.current |> Option.iter (fun d -> d.Dispose()) })

    // Reactive dispatch: push message into the subject.
    let send msg =
        observer.OnNextAsync(msg) |> Async.StartImmediate

    // ------ View ------

    let isSaving = model.SaveStatus = Saving

    Html.section [
        prop.style [
            style.maxWidth 360
            style.margin.auto
            style.padding 32
            style.borderRadius 12
            style.backgroundColor "#1e293b"
            style.display.flex
            style.flexDirection.column
            style.gap 16
        ]
        prop.children [
            Html.p [
                prop.style [
                    style.margin 0
                    style.fontSize 11
                    style.letterSpacing 3
                    style.color "#64748b"
                    style.textTransform.uppercase
                ]
                prop.text "Reaction counter"
            ]

            Html.div [
                prop.style [
                    style.fontSize 72
                    style.fontWeight 700
                    style.textAlign.center
                    style.padding (16, 0)
                    style.color "#f8fafc"
                ]
                prop.text (string model.Count)
            ]

            Html.div [
                prop.style [
                    style.display.flex
                    style.gap 8
                    style.justifyContent.center
                ]
                prop.children [
                    Html.button [
                        prop.style [
                            style.width 48
                            style.height 48
                            style.fontSize 22
                            style.cursor.pointer
                            style.borderRadius 8
                            style.border (1, borderStyle.solid, "#334155")
                            style.backgroundColor "#334155"
                            style.color "#f1f5f9"
                        ]
                        prop.onClick (fun _ -> send Decrement)
                        prop.disabled (model.Count = 0)
                        prop.text "\u2212"
                    ]
                    Html.button [
                        prop.style [
                            style.width 48
                            style.height 48
                            style.fontSize 22
                            style.cursor.pointer
                            style.borderRadius 8
                            style.border (1, borderStyle.solid, "#334155")
                            style.backgroundColor "#334155"
                            style.color "#f1f5f9"
                        ]
                        prop.onClick (fun _ -> send Increment)
                        prop.text "+"
                    ]
                ]
            ]

            Html.button [
                prop.style [
                    style.padding (8, 0)
                    style.cursor.pointer
                    style.borderRadius 6
                    style.border (1, borderStyle.solid, (if model.AutoRun then "#4ade80" else "#334155"))
                    style.backgroundColor (if model.AutoRun then "rgba(74,222,128,0.2)" else "transparent")
                    style.color (if model.AutoRun then "#4ade80" else "#94a3b8")
                    style.fontSize 14
                ]
                prop.onClick (fun _ -> send ToggleAuto)
                prop.text (if model.AutoRun then "Auto \u25cf" else "Auto \u25cb")
            ]

            Html.div [
                prop.style [ style.display.flex; style.gap 8 ]
                prop.children [
                    Html.button [
                        prop.style [
                            style.flexGrow 1
                            style.padding (8, 0)
                            style.cursor.pointer
                            style.borderRadius 6
                            style.border (1, borderStyle.solid, "#334155")
                            style.backgroundColor "transparent"
                            style.color "#94a3b8"
                            style.fontSize 14
                        ]
                        prop.onClick (fun _ -> send Reset)
                        prop.text "Reset"
                    ]
                    Html.button [
                        prop.style [
                            style.flexGrow 1
                            style.padding (8, 0)
                            style.cursor.pointer
                            style.borderRadius 6
                            style.borderWidth 0
                            style.backgroundColor "#4ade80"
                            style.color "#0f172a"
                            style.fontWeight 600
                            style.fontSize 14
                            style.opacity (if isSaving then 0.7 else 1.0)
                        ]
                        prop.onClick (fun _ -> send (Save model.Count))
                        prop.disabled isSaving
                        prop.text (if isSaving then "Saving\u2026" else "Save")
                    ]
                ]
            ]

            match model.SaveStatus with
            | Saved n ->
                Html.p [
                    prop.style [ style.margin 0; style.fontSize 13; style.color "#4ade80" ]
                    prop.text (sprintf "\u2713 Saved: %d" n)
                ]
            | _ -> Html.none
        ]
    ]
