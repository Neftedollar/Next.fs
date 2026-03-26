# Examples

This folder contains consumer-facing example projects.

Unlike `samples`, these are meant to show how `NextFs` should be used in a real app layout.

Current contents:

- [nextfs-starter](nextfs-starter/README.md): minimal Next.js App Router starter using F# source files, generated wrappers, route handlers, instrumentation, and proxy entries
- [nextfs-elmish](nextfs-elmish/README.md): demonstrates the Elmish MVU pattern (`Feliz.UseElmish`) inside a Next.js App Router client component, including async commands and guidance for wiring server actions
- [nextfs-reaction](nextfs-reaction/README.md): demonstrates async reactive streams (`FSharp.Control.AsyncRx`) inside a Next.js App Router client component, with stream merging, tag-based re-subscription, and automatic cancellation via `flatMapLatest`

Use `examples` when you want:

- a folder structure to copy or adapt
- npm scripts and wrapper-generation flow
- a concrete App Router project shape

For smoke-style API coverage instead of a runnable app, use [samples](../samples/README.md).
