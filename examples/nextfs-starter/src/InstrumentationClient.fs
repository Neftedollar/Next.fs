module InstrumentationClient

open Fable.Core
open NextFs

[<Emit("performance.mark('nextfs-starter-client-bootstrap')")>]
let private markBootstrap() : unit = jsNative

do markBootstrap()

let onRouterTransitionStart(url: string, navigationType: RouterTransitionType) =
    ignore url
    ignore navigationType
