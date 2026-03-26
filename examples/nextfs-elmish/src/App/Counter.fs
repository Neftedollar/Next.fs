/// Elmish counter component.
///
/// This module is NOT an entry point — it is imported by App.Page,
/// which carries the 'use client' directive. Everything imported
/// transitively by a 'use client' entry is treated as client-side code
/// by Next.js, so no separate wrapper is needed here.
module App.Counter

open Feliz
open Feliz.UseElmish
open Elmish

// ---------------------------------------------------------------------------
// Model
// ---------------------------------------------------------------------------

type SaveStatus =
    | Idle
    | Saving
    | Saved of int

type Model = {
    Count: int
    SaveStatus: SaveStatus
}

// ---------------------------------------------------------------------------
// Messages
// ---------------------------------------------------------------------------

type Msg =
    | Increment
    | Decrement
    | Reset
    | Save
    | SaveComplete of int

// ---------------------------------------------------------------------------
// Init / Update
// ---------------------------------------------------------------------------

let private init () =
    { Count = 0; SaveStatus = Idle }, Cmd.none

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

let private update msg model =
    match msg with
    | Increment ->
        { model with Count = model.Count + 1; SaveStatus = Idle }, Cmd.none
    | Decrement ->
        { model with Count = max 0 (model.Count - 1); SaveStatus = Idle }, Cmd.none
    | Reset ->
        { model with Count = 0; SaveStatus = Idle }, Cmd.none
    | Save ->
        { model with SaveStatus = Saving },
        Cmd.OfAsync.perform simulateSave model.Count SaveComplete
    | SaveComplete saved ->
        { model with SaveStatus = Saved saved }, Cmd.none

// ---------------------------------------------------------------------------
// View
// ---------------------------------------------------------------------------

[<ReactComponent>]
let Counter () =
    let model, dispatch = React.useElmish(init, update)

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
                prop.text "Elmish counter"
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
                        prop.onClick (fun _ -> dispatch Decrement)
                        prop.disabled (model.Count = 0)
                        prop.text "−"
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
                        prop.onClick (fun _ -> dispatch Increment)
                        prop.text "+"
                    ]
                ]
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
                        prop.onClick (fun _ -> dispatch Reset)
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
                        prop.onClick (fun _ -> dispatch Save)
                        prop.disabled isSaving
                        prop.text (if isSaving then "Saving…" else "Save")
                    ]
                ]
            ]

            match model.SaveStatus with
            | Saved n ->
                Html.p [
                    prop.style [ style.margin 0; style.fontSize 13; style.color "#4ade80" ]
                    prop.text $"✓ Saved: {n}"
                ]
            | _ -> Html.none
        ]
    ]
