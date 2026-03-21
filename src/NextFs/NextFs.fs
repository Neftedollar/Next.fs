namespace NextFs

open Fable.Core
open Fable.Core.JsInterop
open Feliz

module private ReactInterop =
    let inline createElement (componentImport: obj) (props: IReactProperty list) : ReactElement =
        Fable.React.ReactBindings.React.createElement(componentImport, createObj !!props, [||])

module private Props =
    let inline mkAttr (key: string) (value: obj) : IReactProperty =
        PropHelper.mkAttr key value

[<Erase>]
type Href =
    static member inline pathname(value: string) : string * obj = "pathname" ==> value
    static member inline query(value: obj) : string * obj = "query" ==> value
    static member inline hash(value: string) : string * obj = "hash" ==> value
    static member inline create(fields: (string * obj) list) : obj = createObj fields

[<StringEnum; RequireQualifiedAccess>]
type ScriptStrategy =
    | [<CompiledName("beforeInteractive")>] BeforeInteractive
    | [<CompiledName("afterInteractive")>] AfterInteractive
    | [<CompiledName("lazyOnload")>] LazyOnload
    | [<CompiledName("worker")>] Worker

[<StringEnum; RequireQualifiedAccess>]
type RedirectType =
    | [<CompiledName("replace")>] Replace
    | [<CompiledName("push")>] Push

[<StringEnum; RequireQualifiedAccess>]
type ImageLoading =
    | [<CompiledName("lazy")>] Lazy
    | [<CompiledName("eager")>] Eager

[<StringEnum; RequireQualifiedAccess>]
type ImagePlaceholder =
    | [<CompiledName("empty")>] Empty
    | [<CompiledName("blur")>] Blur

type NavigationEvent =
    abstract preventDefault: unit -> unit

type ReadonlyURLSearchParams =
    abstract get: name: string -> string option
    abstract getAll: name: string -> string array
    abstract has: name: string -> bool
    abstract toString: unit -> string

type HeadersCollection =
    abstract append: name: string * value: string -> unit
    abstract delete: name: string -> unit
    abstract get: name: string -> string option
    abstract has: name: string -> bool
    abstract set: name: string * value: string -> unit
    abstract toString: unit -> string

type NavigateOptions =
    abstract scroll: bool with get, set
    abstract transitionTypes: string array with get, set

type PrefetchOptions =
    abstract onInvalidate: (unit -> unit) with get, set

type AppRouterInstance =
    abstract push: href: string * ?options: NavigateOptions -> unit
    abstract replace: href: string * ?options: NavigateOptions -> unit
    abstract refresh: unit -> unit
    abstract prefetch: href: string * ?options: PrefetchOptions -> unit
    abstract back: unit -> unit
    abstract forward: unit -> unit

type RequestCookie =
    abstract name: string
    abstract value: string

type ResponseCookie =
    abstract name: string
    abstract value: string

type RequestCookieStore =
    abstract get: name: string -> RequestCookie option
    abstract getAll: ?name: string -> RequestCookie array
    abstract has: name: string -> bool
    abstract set: name: string * value: string -> unit
    abstract delete: name: string -> unit
    abstract clear: unit -> unit
    abstract toString: unit -> string

type DraftModeState =
    abstract isEnabled: bool
    abstract enable: unit -> unit
    abstract disable: unit -> unit

module Directive =
    [<Emit("'use server'", isStatement = true)>]
    let inline useServer () : unit = jsNative

type FormDataCollection =
    abstract append: name: string * value: obj -> unit
    abstract delete: name: string -> unit
    abstract get: name: string -> obj option
    abstract getAll: name: string -> obj array
    abstract has: name: string -> bool
    abstract set: name: string * value: obj -> unit

type NextUrl =
    abstract href: string
    abstract pathname: string
    abstract search: string
    abstract searchParams: ReadonlyURLSearchParams

type NextRequest =
    abstract cookies: RequestCookieStore
    abstract headers: HeadersCollection
    abstract method: string
    abstract nextUrl: NextUrl
    abstract url: string
    abstract formData: unit -> JS.Promise<FormDataCollection>
    abstract json<'T>: unit -> JS.Promise<'T>
    abstract text: unit -> JS.Promise<string>

type RouteHandlerContext<'routeParams> =
    abstract ``params``: JS.Promise<'routeParams>

type ResponseCookieStore =
    abstract get: name: string -> ResponseCookie option
    abstract getAll: ?name: string -> ResponseCookie array
    abstract has: name: string -> bool
    abstract set: name: string * value: string -> unit
    abstract delete: name: string -> bool

type NextResponse =
    abstract cookies: ResponseCookieStore
    abstract headers: HeadersCollection
    abstract status: int
    abstract statusText: string
    abstract json<'T>: unit -> JS.Promise<'T>
    abstract text: unit -> JS.Promise<string>

module ResponseInit =
    let create(fields: (string * obj) list) : obj =
        createObj fields

    let status(value: int) : string * obj =
        "status" ==> value

    let statusText(value: string) : string * obj =
        "statusText" ==> value

    let headers(value: HeadersCollection) : string * obj =
        "headers" ==> value

    let headersObject(value: obj) : string * obj =
        "headers" ==> value

module NextResponseInit =
    let create(fields: (string * obj) list) : obj =
        createObj fields

    let headers(value: HeadersCollection) : string * obj =
        "headers" ==> value

    let headersObject(value: obj) : string * obj =
        "headers" ==> value

    let request(value: obj) : string * obj =
        "request" ==> value

    let requestHeaders(value: HeadersCollection) : string * obj =
        "request" ==> createObj [ "headers" ==> value ]

module Link =
    [<ImportDefault("next/link")>]
    let private componentImport: obj = jsNative

    let create(props: IReactProperty list) : ReactElement =
        ReactInterop.createElement componentImport props

    let href(value: string) : IReactProperty =
        Props.mkAttr "href" value

    let hrefObject(value: obj) : IReactProperty =
        Props.mkAttr "href" value

    let replace(value: bool) : IReactProperty =
        Props.mkAttr "replace" value

    let scroll(value: bool) : IReactProperty =
        Props.mkAttr "scroll" value

    let prefetch(value: bool) : IReactProperty =
        Props.mkAttr "prefetch" value

    let prefetchAuto : IReactProperty =
        Props.mkAttr "prefetch" null

    let onNavigate(handler: NavigationEvent -> unit) : IReactProperty =
        Props.mkAttr "onNavigate" handler

    let transitionTypes(values: seq<string>) : IReactProperty =
        Props.mkAttr "transitionTypes" (Seq.toArray values)

module Image =
    [<ImportDefault("next/image")>]
    let private componentImport: obj = jsNative

    let create(props: IReactProperty list) : ReactElement =
        ReactInterop.createElement componentImport props

    let src(value: string) : IReactProperty =
        Props.mkAttr "src" value

    let alt(value: string) : IReactProperty =
        Props.mkAttr "alt" value

    let width(value: int) : IReactProperty =
        Props.mkAttr "width" value

    let height(value: int) : IReactProperty =
        Props.mkAttr "height" value

    let fill(value: bool) : IReactProperty =
        Props.mkAttr "fill" value

    let sizes(value: string) : IReactProperty =
        Props.mkAttr "sizes" value

    let quality(value: int) : IReactProperty =
        Props.mkAttr "quality" value

    let preload(value: bool) : IReactProperty =
        Props.mkAttr "preload" value

    let priority(value: bool) : IReactProperty =
        Props.mkAttr "priority" value

    let loading(value: ImageLoading) : IReactProperty =
        Props.mkAttr "loading" value

    let placeholder(value: ImagePlaceholder) : IReactProperty =
        Props.mkAttr "placeholder" value

    let placeholderDataUrl(value: string) : IReactProperty =
        Props.mkAttr "placeholder" value

    let blurDataUrl(value: string) : IReactProperty =
        Props.mkAttr "blurDataURL" value

    let unoptimized(value: bool) : IReactProperty =
        Props.mkAttr "unoptimized" value

    let overrideSrc(value: string) : IReactProperty =
        Props.mkAttr "overrideSrc" value

    let onLoad(handler: obj -> unit) : IReactProperty =
        Props.mkAttr "onLoad" handler

    let onError(handler: obj -> unit) : IReactProperty =
        Props.mkAttr "onError" handler

module Script =
    [<ImportDefault("next/script")>]
    let private componentImport: obj = jsNative

    let create(props: IReactProperty list) : ReactElement =
        ReactInterop.createElement componentImport props

    let src(value: string) : IReactProperty =
        Props.mkAttr "src" value

    let strategy(value: ScriptStrategy) : IReactProperty =
        Props.mkAttr "strategy" value

    let id(value: string) : IReactProperty =
        Props.mkAttr "id" value

    let onLoad(handler: unit -> unit) : IReactProperty =
        Props.mkAttr "onLoad" handler

    let onReady(handler: unit -> unit) : IReactProperty =
        Props.mkAttr "onReady" handler

    let onError(handler: obj -> unit) : IReactProperty =
        Props.mkAttr "onError" handler

module Form =
    [<ImportDefault("next/form")>]
    let private componentImport: obj = jsNative

    let create(props: IReactProperty list) : ReactElement =
        ReactInterop.createElement componentImport props

    let action(value: string) : IReactProperty =
        Props.mkAttr "action" value

    let inline serverAction(value: 'T) : IReactProperty =
        Props.mkAttr "action" value

    let replace(value: bool) : IReactProperty =
        Props.mkAttr "replace" value

    let scroll(value: bool) : IReactProperty =
        Props.mkAttr "scroll" value

    let prefetch(value: bool) : IReactProperty =
        Props.mkAttr "prefetch" value

module Head =
    [<ImportDefault("next/head")>]
    let private componentImport: obj = jsNative

    let create(props: IReactProperty list) : ReactElement =
        ReactInterop.createElement componentImport props

module Navigation =
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

    [<Import("useSelectedLayoutSegments", "next/navigation")>]
    let private useSelectedLayoutSegmentsImport: unit -> string array = jsNative

    [<Import("redirect", "next/navigation")>]
    let private redirectImport: string * RedirectType -> unit = jsNative

    [<Import("permanentRedirect", "next/navigation")>]
    let private permanentRedirectImport: string -> unit = jsNative

    [<Import("notFound", "next/navigation")>]
    let private notFoundImport: unit -> unit = jsNative

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

    let useSelectedLayoutSegments() : string array =
        useSelectedLayoutSegmentsImport()

    let redirect(path: string) : unit =
        redirectImport(path, RedirectType.Replace)

    let redirectWith(path: string, redirectType: RedirectType) : unit =
        redirectImport(path, redirectType)

    let permanentRedirect(path: string) : unit =
        permanentRedirectImport(path)

    let notFound() : unit =
        notFoundImport()

module Server =
    [<Import("headers", "next/headers")>]
    let private headersImport: unit -> JS.Promise<HeadersCollection> = jsNative

    [<Import("cookies", "next/headers")>]
    let private cookiesImport: unit -> JS.Promise<RequestCookieStore> = jsNative

    [<Import("draftMode", "next/headers")>]
    let private draftModeImport: unit -> JS.Promise<DraftModeState> = jsNative

    [<Import("connection", "next/server")>]
    let private connectionImport: unit -> JS.Promise<unit> = jsNative

    let headers() : JS.Promise<HeadersCollection> =
        headersImport()

    let cookies() : JS.Promise<RequestCookieStore> =
        cookiesImport()

    let draftMode() : JS.Promise<DraftModeState> =
        draftModeImport()

    let connection() : JS.Promise<unit> =
        connectionImport()

module ServerResponse =
    [<Import("NextResponse", "next/server")>]
    let private nextResponseImport: obj = jsNative

    [<Emit("$0.json($1)")>]
    let private jsonImport(nextResponse: obj, body: obj) : NextResponse = jsNative

    [<Emit("$0.json($1, $2)")>]
    let private jsonWithInitImport(nextResponse: obj, body: obj, init: obj) : NextResponse = jsNative

    [<Emit("$0.redirect($1)")>]
    let private redirectImport(nextResponse: obj, url: obj) : NextResponse = jsNative

    [<Emit("$0.redirect($1, $2)")>]
    let private redirectWithInitImport(nextResponse: obj, url: obj, init: obj) : NextResponse = jsNative

    [<Emit("$0.rewrite($1)")>]
    let private rewriteImport(nextResponse: obj, url: obj) : NextResponse = jsNative

    [<Emit("$0.rewrite($1, $2)")>]
    let private rewriteWithInitImport(nextResponse: obj, url: obj, init: obj) : NextResponse = jsNative

    [<Emit("$0.next()")>]
    let private nextImport(nextResponse: obj) : NextResponse = jsNative

    [<Emit("$0.next($1)")>]
    let private nextWithInitImport(nextResponse: obj, init: obj) : NextResponse = jsNative

    let json<'T>(body: 'T) : NextResponse =
        jsonImport(nextResponseImport, box body)

    let jsonWithInit<'T>(body: 'T) (init: obj) : NextResponse =
        jsonWithInitImport(nextResponseImport, box body, init)

    let redirect(url: obj) : NextResponse =
        redirectImport(nextResponseImport, url)

    let redirectWithInit(url: obj) (init: obj) : NextResponse =
        redirectWithInitImport(nextResponseImport, url, init)

    let rewrite(url: obj) : NextResponse =
        rewriteImport(nextResponseImport, url)

    let rewriteWithInit(url: obj) (init: obj) : NextResponse =
        rewriteWithInitImport(nextResponseImport, url, init)

    let next() : NextResponse =
        nextImport(nextResponseImport)

    let nextWithInit(init: obj) : NextResponse =
        nextWithInitImport(nextResponseImport, init)
