[<NextFs.NextFsEntry("instrumentation.js", Named="register onRequestError")>]
module Instrumentation

open NextFs

let register() =
    ()

let onRequestError(error: InstrumentationError, request: InstrumentationRequest, context: InstrumentationContext) =
    ignore error.message
    ignore error.digest
    ignore request.path
    ignore context.routeType
