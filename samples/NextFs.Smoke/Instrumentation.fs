module NextFs.Smoke.Instrumentation

open NextFs

let register() =
    ()

let onRequestError(error: InstrumentationError, request: InstrumentationRequest, context: InstrumentationContext) =
    ignore error.message
    ignore error.digest
    ignore request.path
    ignore request.method
    ignore request.headers
    ignore context.routerKind
    ignore context.routePath
    ignore context.routeType
    ignore context.renderSource
    ignore context.revalidateReason
    ignore context.renderType

let onRouterTransitionStart(url: string, navigationType: RouterTransitionType) =
    ignore url
    ignore navigationType
