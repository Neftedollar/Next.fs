namespace NextFs

open Fable.Core
open Feliz

type Font = Font

[<AutoOpen>]
module FontAutoOpen =
    type Font with
        [<ImportDefault("next/font/local")>]
        static member local(options: obj) : LoadedFont = jsNative

module LinkClient =
    [<Import("useLinkStatus", "next/link")>]
    let private useLinkStatusImport: unit -> LinkStatus = jsNative

    let useLinkStatus() : LinkStatus =
        useLinkStatusImport()

module WebVitals =
    [<Import("useReportWebVitals", "next/web-vitals")>]
    let private useReportWebVitalsImport: (WebVitalMetric -> unit) -> unit = jsNative

    let useReportWebVitals(handler: WebVitalMetric -> unit) : unit =
        useReportWebVitalsImport handler

module NavigationClient =
    [<Import("useRouter", "next/navigation")>]
    let private useRouterImport: unit -> AppRouterInstance = jsNative

    [<Import("usePathname", "next/navigation")>]
    let private usePathnameImport: unit -> string = jsNative

    [<Import("useSearchParams", "next/navigation")>]
    let private useSearchParamsImport: unit -> ReadonlyURLSearchParams = jsNative

    [<Import("useParams", "next/navigation")>]
    let private useParamsImport<'T> : unit -> 'T = jsNative

    [<Import("useSelectedLayoutSegment", "next/navigation")>]
    let private useSelectedLayoutSegmentImport: unit -> string option = jsNative

    [<Import("useSelectedLayoutSegment", "next/navigation")>]
    let private useSelectedLayoutSegmentWithKeyImport: string -> string option = jsNative

    [<Import("useSelectedLayoutSegments", "next/navigation")>]
    let private useSelectedLayoutSegmentsImport: unit -> string array = jsNative

    [<Import("useSelectedLayoutSegments", "next/navigation")>]
    let private useSelectedLayoutSegmentsWithKeyImport: string -> string array = jsNative

    [<Import("useServerInsertedHTML", "next/navigation")>]
    let private useServerInsertedHTMLImport: (unit -> ReactElement) -> unit = jsNative

    [<Import("unstable_isUnrecognizedActionError", "next/navigation")>]
    let private unstableIsUnrecognizedActionErrorImport: obj -> bool = jsNative

    let useRouter() : AppRouterInstance =
        useRouterImport()

    let usePathname() : string =
        usePathnameImport()

    let useSearchParams() : ReadonlyURLSearchParams =
        useSearchParamsImport()

    let useParams<'T>() : 'T =
        useParamsImport<'T>()

    let useSelectedLayoutSegment() : string option =
        useSelectedLayoutSegmentImport()

    let useSelectedLayoutSegmentFor(parallelRouteKey: string) : string option =
        useSelectedLayoutSegmentWithKeyImport parallelRouteKey

    let useSelectedLayoutSegments() : string array =
        useSelectedLayoutSegmentsImport()

    let useSelectedLayoutSegmentsFor(parallelRouteKey: string) : string array =
        useSelectedLayoutSegmentsWithKeyImport parallelRouteKey

    let useServerInsertedHTML(render: unit -> ReactElement) : unit =
        useServerInsertedHTMLImport render

    /// Checks whether an error thrown during a client-side server-action call is an
    /// "unrecognized action" error — i.e. the server rejected the request because it
    /// could not find the action in its action manifest.
    ///
    /// This is an unstable Next.js API (next/navigation unstable_isUnrecognizedActionError)
    /// and may change or be removed in a future Next.js release.
    let unstableIsUnrecognizedActionError(error: obj) : bool =
        unstableIsUnrecognizedActionErrorImport error
