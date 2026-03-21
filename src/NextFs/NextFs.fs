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

[<StringEnum; RequireQualifiedAccess>]
type RevalidatePathType =
    | [<CompiledName("page")>] Page
    | [<CompiledName("layout")>] Layout

[<StringEnum; RequireQualifiedAccess>]
type CacheProfile =
    | [<CompiledName("default")>] Default
    | [<CompiledName("seconds")>] Seconds
    | [<CompiledName("minutes")>] Minutes
    | [<CompiledName("hours")>] Hours
    | [<CompiledName("days")>] Days
    | [<CompiledName("weeks")>] Weeks
    | [<CompiledName("max")>] Max

[<StringEnum; RequireQualifiedAccess>]
type RevalidateTagProfile =
    | [<CompiledName("max")>] Max

[<StringEnum; RequireQualifiedAccess>]
type ServerFetchCache =
    | [<CompiledName("force-cache")>] ForceCache
    | [<CompiledName("no-store")>] NoStore

[<StringEnum; RequireQualifiedAccess>]
type RouteRuntime =
    | [<CompiledName("nodejs")>] NodeJs
    | [<CompiledName("edge")>] Edge

[<StringEnum; RequireQualifiedAccess>]
type RouterTransitionType =
    | [<CompiledName("push")>] Push
    | [<CompiledName("replace")>] Replace
    | [<CompiledName("traverse")>] Traverse

[<StringEnum; RequireQualifiedAccess>]
type InstrumentationRouterKind =
    | [<CompiledName("Pages Router")>] PagesRouter
    | [<CompiledName("App Router")>] AppRouter

[<StringEnum; RequireQualifiedAccess>]
type InstrumentationRouteType =
    | [<CompiledName("render")>] Render
    | [<CompiledName("route")>] Route
    | [<CompiledName("action")>] Action
    | [<CompiledName("proxy")>] Proxy

[<StringEnum; RequireQualifiedAccess>]
type InstrumentationRenderSource =
    | [<CompiledName("react-server-components")>] ReactServerComponents
    | [<CompiledName("react-server-components-payload")>] ReactServerComponentsPayload
    | [<CompiledName("server-rendering")>] ServerRendering

[<StringEnum; RequireQualifiedAccess>]
type InstrumentationRevalidateReason =
    | [<CompiledName("on-demand")>] OnDemand
    | [<CompiledName("stale")>] Stale

[<StringEnum; RequireQualifiedAccess>]
type InstrumentationRenderType =
    | [<CompiledName("dynamic")>] Dynamic
    | [<CompiledName("dynamic-resume")>] DynamicResume

[<StringEnum; RequireQualifiedAccess>]
type WebVitalMetricRating =
    | [<CompiledName("good")>] Good
    | [<CompiledName("needs-improvement")>] NeedsImprovement
    | [<CompiledName("poor")>] Poor

[<StringEnum; RequireQualifiedAccess>]
type WebVitalNavigationType =
    | [<CompiledName("navigate")>] Navigate
    | [<CompiledName("reload")>] Reload
    | [<CompiledName("prerender")>] Prerender
    | [<CompiledName("back-forward")>] BackForward
    | [<CompiledName("back-forward-cache")>] BackForwardCache
    | [<CompiledName("restore")>] Restore

[<StringEnum; RequireQualifiedAccess>]
type ImageResponseEmoji =
    | [<CompiledName("twemoji")>] Twemoji
    | [<CompiledName("blobmoji")>] Blobmoji
    | [<CompiledName("noto")>] Noto
    | [<CompiledName("openmoji")>] Openmoji

[<StringEnum; RequireQualifiedAccess>]
type ImageResponseFontStyle =
    | [<CompiledName("normal")>] Normal
    | [<CompiledName("italic")>] Italic

[<StringEnum; RequireQualifiedAccess>]
type FontDisplay =
    | [<CompiledName("auto")>] Auto
    | [<CompiledName("block")>] Block
    | [<CompiledName("swap")>] Swap
    | [<CompiledName("fallback")>] Fallback
    | [<CompiledName("optional")>] Optional

[<StringEnum; RequireQualifiedAccess>]
type CookiePriority =
    | [<CompiledName("low")>] Low
    | [<CompiledName("medium")>] Medium
    | [<CompiledName("high")>] High

[<StringEnum; RequireQualifiedAccess>]
type CookieSameSite =
    | [<CompiledName("lax")>] Lax
    | [<CompiledName("strict")>] Strict
    | [<CompiledName("none")>] None

[<StringEnum; RequireQualifiedAccess>]
type RouteHasType =
    | [<CompiledName("header")>] Header
    | [<CompiledName("cookie")>] Cookie
    | [<CompiledName("query")>] Query
    | [<CompiledName("host")>] Host

type NavigationEvent =
    abstract preventDefault: unit -> unit

type LinkStatus =
    abstract pending: bool

type ReadonlyURLSearchParams =
    abstract get: name: string -> string option
    abstract getAll: name: string -> string array
    abstract has: name: string -> bool
    abstract toString: unit -> string

type URLSearchParamsCollection =
    abstract append: name: string * value: string -> unit
    abstract delete: name: string -> unit
    abstract get: name: string -> string option
    abstract getAll: name: string -> string array
    abstract has: name: string -> bool
    abstract set: name: string * value: string -> unit
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

type ImagePropsResult =
    abstract props: obj

type ResponseCookie =
    abstract name: string
    abstract value: string
    abstract domain: string option
    abstract path: string option
    abstract secure: bool option
    abstract sameSite: obj option
    abstract partitioned: bool option
    abstract expires: obj option
    abstract httpOnly: bool option
    abstract maxAge: int option
    abstract priority: CookiePriority option

type RequestCookieStore =
    abstract get: name: string -> RequestCookie option
    abstract get: cookie: RequestCookie -> RequestCookie option
    abstract getAll: unit -> RequestCookie array
    abstract getAll: ?name: string -> RequestCookie array
    abstract getAll: cookie: RequestCookie -> RequestCookie array
    abstract has: name: string -> bool
    abstract set: name: string * value: string -> RequestCookieStore
    abstract set: cookie: RequestCookie -> RequestCookieStore
    abstract delete: name: string -> bool
    abstract delete: names: string array -> bool array
    abstract clear: unit -> RequestCookieStore
    abstract toString: unit -> string

type DraftModeState =
    abstract isEnabled: bool
    abstract enable: unit -> unit
    abstract disable: unit -> unit

type UserAgentBrowser =
    abstract name: string option
    abstract version: string option

type UserAgentDevice =
    abstract model: string option
    abstract ``type``: string option
    abstract vendor: string option

type UserAgentEngine =
    abstract name: string option
    abstract version: string option

type UserAgentOperatingSystem =
    abstract name: string option
    abstract version: string option

type UserAgentCpu =
    abstract architecture: string option

type UserAgentInfo =
    abstract isBot: bool
    abstract browser: UserAgentBrowser
    abstract device: UserAgentDevice
    abstract engine: UserAgentEngine
    abstract os: UserAgentOperatingSystem
    abstract cpu: UserAgentCpu

type WebVitalMetric =
    abstract id: string
    abstract name: string
    abstract delta: float
    abstract entries: obj array
    abstract navigationType: WebVitalNavigationType
    abstract rating: WebVitalMetricRating
    abstract value: float

type FontStyleObject =
    abstract fontFamily: string
    abstract fontWeight: int option
    abstract fontStyle: string option

type LoadedFont =
    abstract className: string
    abstract style: FontStyleObject
    abstract variable: string option

type PageProps<'routeParams, 'searchParams> =
    abstract ``params``: JS.Promise<'routeParams>
    abstract searchParams: JS.Promise<'searchParams>

type LayoutProps<'routeParams> =
    abstract ``params``: JS.Promise<'routeParams>
    abstract children: ReactElement

type TemplateProps =
    abstract children: ReactElement

type DefaultProps<'routeParams> =
    abstract ``params``: JS.Promise<'routeParams>

type ErrorWithDigest =
    abstract name: string option
    abstract message: string
    abstract stack: string option
    abstract digest: string option

type ErrorBoundaryProps =
    abstract error: ErrorWithDigest
    abstract ``unstable_retry``: unit -> unit
    abstract reset: unit -> unit

type InstrumentationError =
    abstract name: string option
    abstract message: string
    abstract stack: string option
    abstract digest: string

type InstrumentationRequest =
    abstract path: string
    abstract method: string
    abstract headers: obj

type InstrumentationContext =
    abstract routerKind: InstrumentationRouterKind
    abstract routePath: string
    abstract routeType: InstrumentationRouteType
    abstract renderSource: InstrumentationRenderSource option
    abstract revalidateReason: InstrumentationRevalidateReason option
    abstract renderType: InstrumentationRenderType option

type ResolvingMetadata = JS.Promise<obj>
type ResolvingViewport = JS.Promise<obj>

type SitemapProps =
    abstract id: JS.Promise<string>

type ImageGenerationProps<'routeParams, 'id> =
    abstract ``params``: JS.Promise<'routeParams>
    abstract id: JS.Promise<'id>

module Directive =
    [<Emit("'use server'", isStatement = true)>]
    let inline useServer () : unit = jsNative

    [<Emit("'use cache'", isStatement = true)>]
    let inline useCache () : unit = jsNative

    [<Emit("'use cache: private'", isStatement = true)>]
    let inline useCachePrivate () : unit = jsNative

    [<Emit("'use cache: remote'", isStatement = true)>]
    let inline useCacheRemote () : unit = jsNative

type FormDataCollection =
    abstract append: name: string * value: obj -> unit
    abstract delete: name: string -> unit
    abstract get: name: string -> obj option
    abstract getAll: name: string -> obj array
    abstract has: name: string -> bool
    abstract set: name: string * value: obj -> unit

type NextUrl =
    abstract basePath: string with get, set
    abstract buildId: string option with get, set
    abstract locale: string with get, set
    abstract defaultLocale: string option
    abstract domainLocale: obj option
    abstract host: string with get, set
    abstract hostname: string with get, set
    abstract port: string with get, set
    abstract protocol: string with get, set
    abstract href: string with get, set
    abstract origin: string
    abstract pathname: string with get, set
    abstract hash: string with get, set
    abstract search: string with get, set
    abstract password: string with get, set
    abstract username: string with get, set
    abstract searchParams: URLSearchParamsCollection
    abstract clone: unit -> NextUrl

type NextRequest =
    abstract cookies: RequestCookieStore
    abstract headers: HeadersCollection
    abstract method: string
    abstract nextUrl: NextUrl
    abstract url: string
    abstract bodyUsed: bool
    abstract clone: unit -> obj
    abstract arrayBuffer: unit -> JS.Promise<obj>
    abstract blob: unit -> JS.Promise<obj>
    abstract formData: unit -> JS.Promise<FormDataCollection>
    abstract json<'T>: unit -> JS.Promise<'T>
    abstract text: unit -> JS.Promise<string>

type RouteHandlerContext<'routeParams> =
    abstract ``params``: JS.Promise<'routeParams>

type NextFetchEvent =
    abstract waitUntil<'T>: promise: JS.Promise<'T> -> unit
    abstract passThroughOnException: unit -> unit

type ResponseCookieStore =
    abstract get: name: string -> ResponseCookie option
    abstract get: cookie: ResponseCookie -> ResponseCookie option
    abstract getAll: unit -> ResponseCookie array
    abstract getAll: ?name: string -> ResponseCookie array
    abstract getAll: cookie: ResponseCookie -> ResponseCookie array
    abstract has: name: string -> bool
    abstract set: name: string * value: string * ?cookie: obj -> ResponseCookieStore
    abstract set: cookie: ResponseCookie -> ResponseCookieStore
    abstract delete: name: string -> ResponseCookieStore
    abstract delete: cookie: obj -> ResponseCookieStore

type NextResponse =
    abstract cookies: ResponseCookieStore
    abstract headers: HeadersCollection
    abstract status: int
    abstract statusText: string
    abstract ok: bool
    abstract redirected: bool
    abstract url: string
    abstract bodyUsed: bool
    abstract clone: unit -> obj
    abstract arrayBuffer: unit -> JS.Promise<obj>
    abstract blob: unit -> JS.Promise<obj>
    abstract formData: unit -> JS.Promise<FormDataCollection>
    abstract json<'T>: unit -> JS.Promise<'T>
    abstract text: unit -> JS.Promise<string>

type ServerFetchResponse =
    abstract headers: HeadersCollection
    abstract status: int
    abstract statusText: string
    abstract ok: bool
    abstract redirected: bool
    abstract url: string
    abstract bodyUsed: bool
    abstract clone: unit -> obj
    abstract arrayBuffer: unit -> JS.Promise<obj>
    abstract blob: unit -> JS.Promise<obj>
    abstract formData: unit -> JS.Promise<FormDataCollection>
    abstract json<'T>: unit -> JS.Promise<'T>
    abstract text: unit -> JS.Promise<string>

module NextConfig =
    let create(fields: (string * obj) list) : obj =
        createObj fields

    let basePath(value: string) : string * obj =
        "basePath" ==> value

    let i18n(value: obj) : string * obj =
        "i18n" ==> value

    let trailingSlash(value: bool) : string * obj =
        "trailingSlash" ==> value

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

    let nextConfig(value: obj) : string * obj =
        "nextConfig" ==> value

    let url(value: string) : string * obj =
        "url" ==> value

module NextRequestInit =
    let create(fields: (string * obj) list) : obj =
        createObj fields

    let method'(value: string) : string * obj =
        "method" ==> value

    let headers(value: HeadersCollection) : string * obj =
        "headers" ==> value

    let headersObject(value: obj) : string * obj =
        "headers" ==> value

    let body(value: obj) : string * obj =
        "body" ==> value

    let nextConfig(value: obj) : string * obj =
        "nextConfig" ==> value

    let signal(value: obj) : string * obj =
        "signal" ==> value

    let duplexHalf : string * obj =
        "duplex" ==> "half"

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

module NextFetchOptions =
    let create(fields: (string * obj) list) : obj =
        createObj fields

    let revalidate(value: obj) : string * obj =
        "revalidate" ==> value

    let tags(values: seq<string>) : string * obj =
        "tags" ==> Seq.toArray values

    let tag(value: string) : string * obj =
        "tags" ==> [| value |]

module ServerFetchInit =
    let create(fields: (string * obj) list) : obj =
        createObj fields

    let method'(value: string) : string * obj =
        "method" ==> value

    let headers(value: HeadersCollection) : string * obj =
        "headers" ==> value

    let headersObject(value: obj) : string * obj =
        "headers" ==> value

    let body(value: obj) : string * obj =
        "body" ==> value

    let cache(value: ServerFetchCache) : string * obj =
        "cache" ==> value

    let next(value: obj) : string * obj =
        "next" ==> value

    let signal(value: obj) : string * obj =
        "signal" ==> value

module GenerateSitemapsEntry =
    let create(fields: (string * obj) list) : obj =
        createObj fields

    let id(value: obj) : string * obj =
        "id" ==> value

module CookieOptions =
    let create(fields: (string * obj) list) : obj =
        createObj fields

    let name(value: string) : string * obj =
        "name" ==> value

    let value(value: string) : string * obj =
        "value" ==> value

    let domain(value: string) : string * obj =
        "domain" ==> value

    let path(value: string) : string * obj =
        "path" ==> value

    let secure(value: bool) : string * obj =
        "secure" ==> value

    let sameSite(value: CookieSameSite) : string * obj =
        "sameSite" ==> value

    let sameSiteMode(value: bool) : string * obj =
        "sameSite" ==> value

    let partitioned(value: bool) : string * obj =
        "partitioned" ==> value

    let expires(value: obj) : string * obj =
        "expires" ==> value

    let httpOnly(value: bool) : string * obj =
        "httpOnly" ==> value

    let maxAge(value: int) : string * obj =
        "maxAge" ==> value

    let priority(value: CookiePriority) : string * obj =
        "priority" ==> value

module LocalFontSource =
    let create(fields: (string * obj) list) : obj =
        createObj fields

    let path(value: string) : string * obj =
        "path" ==> value

    let weight(value: string) : string * obj =
        "weight" ==> value

    let style(value: string) : string * obj =
        "style" ==> value

module FontDeclaration =
    let create(fields: (string * obj) list) : obj =
        createObj fields

    let prop(value: string) : string * obj =
        "prop" ==> value

    let value(value: string) : string * obj =
        "value" ==> value

module FontOptions =
    let create(fields: (string * obj) list) : obj =
        createObj fields

    let src(value: string) : string * obj =
        "src" ==> value

    let sources(values: seq<obj>) : string * obj =
        "src" ==> Seq.toArray values

    let weight(value: string) : string * obj =
        "weight" ==> value

    let weights(values: seq<string>) : string * obj =
        "weight" ==> Seq.toArray values

    let style(value: string) : string * obj =
        "style" ==> value

    let styles(values: seq<string>) : string * obj =
        "style" ==> Seq.toArray values

    let subsets(values: seq<string>) : string * obj =
        "subsets" ==> Seq.toArray values

    let axes(values: seq<string>) : string * obj =
        "axes" ==> Seq.toArray values

    let display(value: FontDisplay) : string * obj =
        "display" ==> value

    let preload(value: bool) : string * obj =
        "preload" ==> value

    let fallback(values: seq<string>) : string * obj =
        "fallback" ==> Seq.toArray values

    let adjustFontFallback(value: bool) : string * obj =
        "adjustFontFallback" ==> value

    let adjustFontFallbackFamily(value: string) : string * obj =
        "adjustFontFallback" ==> value

    let variable(value: string) : string * obj =
        "variable" ==> value

    let declarations(values: seq<obj>) : string * obj =
        "declarations" ==> Seq.toArray values

module RouteHas =
    let create(fields: (string * obj) list) : obj =
        createObj fields

    let type'(value: RouteHasType) : string * obj =
        "type" ==> value

    let key(value: string) : string * obj =
        "key" ==> value

    let value(value: string) : string * obj =
        "value" ==> value

module ProxyMatcher =
    let create(fields: (string * obj) list) : obj =
        createObj fields

    let source(value: string) : string * obj =
        "source" ==> value

    let localeFalse : string * obj =
        "locale" ==> false

    let has(values: seq<obj>) : string * obj =
        "has" ==> Seq.toArray values

    let missing(values: seq<obj>) : string * obj =
        "missing" ==> Seq.toArray values

module ProxyConfig =
    let create(fields: (string * obj) list) : obj =
        createObj fields

    let matcher(value: string) : string * obj =
        "matcher" ==> value

    let matchers(values: seq<string>) : string * obj =
        "matcher" ==> Seq.toArray values

    let matchersMany(values: seq<obj>) : string * obj =
        "matcher" ==> Seq.toArray values

    let regions(value: string) : string * obj =
        "regions" ==> value

    let regionsMany(values: seq<string>) : string * obj =
        "regions" ==> Seq.toArray values

    let allowDynamic(value: string) : string * obj =
        "unstable_allowDynamic" ==> value

    let allowDynamicMany(values: seq<string>) : string * obj =
        "unstable_allowDynamic" ==> Seq.toArray values

module CacheLife =
    let create(fields: (string * obj) list) : obj =
        createObj fields

    let stale(value: int) : string * obj =
        "stale" ==> value

    let revalidate(value: int) : string * obj =
        "revalidate" ==> value

    let expire(value: int) : string * obj =
        "expire" ==> value

module ImageResponseFont =
    let create(fields: (string * obj) list) : obj =
        createObj fields

    let name(value: string) : string * obj =
        "name" ==> value

    let data(value: obj) : string * obj =
        "data" ==> value

    let weight(value: int) : string * obj =
        "weight" ==> value

    let style(value: ImageResponseFontStyle) : string * obj =
        "style" ==> value

module ImageResponseOptions =
    let create(fields: (string * obj) list) : obj =
        createObj fields

    let width(value: int) : string * obj =
        "width" ==> value

    let height(value: int) : string * obj =
        "height" ==> value

    let emoji(value: ImageResponseEmoji) : string * obj =
        "emoji" ==> value

    let fonts(values: seq<obj>) : string * obj =
        "fonts" ==> Seq.toArray values

    let debug(value: bool) : string * obj =
        "debug" ==> value

    let status(value: int) : string * obj =
        "status" ==> value

    let statusText(value: string) : string * obj =
        "statusText" ==> value

    let headersObject(value: obj) : string * obj =
        "headers" ==> value

module ImageMetadataSize =
    let create(fields: (string * obj) list) : obj =
        createObj fields

    let width(value: int) : string * obj =
        "width" ==> value

    let height(value: int) : string * obj =
        "height" ==> value

module ImageMetadata =
    let create(fields: (string * obj) list) : obj =
        createObj fields

    let id(value: obj) : string * obj =
        "id" ==> value

    let alt(value: string) : string * obj =
        "alt" ==> value

    let size(value: obj) : string * obj =
        "size" ==> value

    let contentType(value: string) : string * obj =
        "contentType" ==> value

module MetadataTitle =
    let create(fields: (string * obj) list) : obj =
        createObj fields

    let defaultValue(value: string) : string * obj =
        "default" ==> value

    let template(value: string) : string * obj =
        "template" ==> value

    let absolute(value: string) : string * obj =
        "absolute" ==> value

module MetadataImage =
    let create(fields: (string * obj) list) : obj =
        createObj fields

    let url(value: string) : string * obj =
        "url" ==> value

    let urlObject(value: obj) : string * obj =
        "url" ==> value

    let secureUrl(value: string) : string * obj =
        "secureUrl" ==> value

    let width(value: int) : string * obj =
        "width" ==> value

    let height(value: int) : string * obj =
        "height" ==> value

    let alt(value: string) : string * obj =
        "alt" ==> value

    let type'(value: string) : string * obj =
        "type" ==> value

module MetadataAlternates =
    let create(fields: (string * obj) list) : obj =
        createObj fields

    let canonical(value: string) : string * obj =
        "canonical" ==> value

    let languages(value: obj) : string * obj =
        "languages" ==> value

    let media(value: obj) : string * obj =
        "media" ==> value

    let types(value: obj) : string * obj =
        "types" ==> value

module MetadataOpenGraph =
    let create(fields: (string * obj) list) : obj =
        createObj fields

    let title(value: string) : string * obj =
        "title" ==> value

    let description(value: string) : string * obj =
        "description" ==> value

    let url(value: string) : string * obj =
        "url" ==> value

    let siteName(value: string) : string * obj =
        "siteName" ==> value

    let locale(value: string) : string * obj =
        "locale" ==> value

    let type'(value: string) : string * obj =
        "type" ==> value

    let images(value: obj) : string * obj =
        "images" ==> value

    let imagesMany(values: seq<obj>) : string * obj =
        "images" ==> Seq.toArray values

module TwitterAppValues =
    let create(fields: (string * obj) list) : obj =
        createObj fields

    let iphone(value: string) : string * obj =
        "iphone" ==> value

    let ipad(value: string) : string * obj =
        "ipad" ==> value

    let googlePlay(value: string) : string * obj =
        "googleplay" ==> value

module TwitterApp =
    let create(fields: (string * obj) list) : obj =
        createObj fields

    let name(value: string) : string * obj =
        "name" ==> value

    let id(value: obj) : string * obj =
        "id" ==> value

    let url(value: obj) : string * obj =
        "url" ==> value

module MetadataTwitter =
    let create(fields: (string * obj) list) : obj =
        createObj fields

    let card(value: string) : string * obj =
        "card" ==> value

    let title(value: string) : string * obj =
        "title" ==> value

    let description(value: string) : string * obj =
        "description" ==> value

    let site(value: string) : string * obj =
        "site" ==> value

    let siteId(value: string) : string * obj =
        "siteId" ==> value

    let creator(value: string) : string * obj =
        "creator" ==> value

    let creatorId(value: string) : string * obj =
        "creatorId" ==> value

    let images(value: obj) : string * obj =
        "images" ==> value

    let imagesMany(values: seq<obj>) : string * obj =
        "images" ==> Seq.toArray values

    let app(value: obj) : string * obj =
        "app" ==> value

module MetadataVerification =
    let create(fields: (string * obj) list) : obj =
        createObj fields

    let google(value: string) : string * obj =
        "google" ==> value

    let yandex(value: string) : string * obj =
        "yandex" ==> value

    let yahoo(value: string) : string * obj =
        "yahoo" ==> value

    let other(value: obj) : string * obj =
        "other" ==> value

module ThemeColor =
    let create(fields: (string * obj) list) : obj =
        createObj fields

    let color(value: string) : string * obj =
        "color" ==> value

    let media(value: string) : string * obj =
        "media" ==> value

module Viewport =
    let create(fields: (string * obj) list) : obj =
        createObj fields

    let themeColor(value: string) : string * obj =
        "themeColor" ==> value

    let themeColorObject(value: obj) : string * obj =
        "themeColor" ==> value

    let themeColors(values: seq<obj>) : string * obj =
        "themeColor" ==> Seq.toArray values

    let width(value: string) : string * obj =
        "width" ==> value

    let initialScale(value: float) : string * obj =
        "initialScale" ==> value

    let maximumScale(value: float) : string * obj =
        "maximumScale" ==> value

    let userScalable(value: bool) : string * obj =
        "userScalable" ==> value

    let colorScheme(value: string) : string * obj =
        "colorScheme" ==> value

    let viewportFit(value: string) : string * obj =
        "viewportFit" ==> value

module Metadata =
    let create(fields: (string * obj) list) : obj =
        createObj fields

    let title(value: string) : string * obj =
        "title" ==> value

    let titleTemplate(value: obj) : string * obj =
        "title" ==> value

    let description(value: string) : string * obj =
        "description" ==> value

    let applicationName(value: string) : string * obj =
        "applicationName" ==> value

    let generator(value: string) : string * obj =
        "generator" ==> value

    let keywords(values: seq<string>) : string * obj =
        "keywords" ==> Seq.toArray values

    let creator(value: string) : string * obj =
        "creator" ==> value

    let publisher(value: string) : string * obj =
        "publisher" ==> value

    let metadataBase(value: obj) : string * obj =
        "metadataBase" ==> value

    let alternates(value: obj) : string * obj =
        "alternates" ==> value

    let openGraph(value: obj) : string * obj =
        "openGraph" ==> value

    let twitter(value: obj) : string * obj =
        "twitter" ==> value

    let robots(value: obj) : string * obj =
        "robots" ==> value

    let manifest(value: string) : string * obj =
        "manifest" ==> value

    let icons(value: obj) : string * obj =
        "icons" ==> value

    let verification(value: obj) : string * obj =
        "verification" ==> value

    let other(value: obj) : string * obj =
        "other" ==> value

module PreferredRegion =
    let auto: string = "auto"
    let globalRegion: string = "global"
    let home: string = "home"

    let region(value: string) : string =
        value

    let regions(values: seq<string>) : string array =
        Seq.toArray values

module Revalidate =
    let forever: obj = box false
    let neverCache: obj = box 0

    let seconds(value: int) : obj =
        box value

module MetadataRoute =
    [<StringEnum; RequireQualifiedAccess>]
    type SitemapChangeFrequency =
        | [<CompiledName("always")>] Always
        | [<CompiledName("hourly")>] Hourly
        | [<CompiledName("daily")>] Daily
        | [<CompiledName("weekly")>] Weekly
        | [<CompiledName("monthly")>] Monthly
        | [<CompiledName("yearly")>] Yearly
        | [<CompiledName("never")>] Never

    module RobotsRule =
        let create(fields: (string * obj) list) : obj =
            createObj fields

        let userAgent(value: string) : string * obj =
            "userAgent" ==> value

        let userAgents(values: seq<string>) : string * obj =
            "userAgent" ==> Seq.toArray values

        let allow(value: string) : string * obj =
            "allow" ==> value

        let disallow(value: string) : string * obj =
            "disallow" ==> value

        let crawlDelay(value: int) : string * obj =
            "crawlDelay" ==> value

    module Robots =
        let create(fields: (string * obj) list) : obj =
            createObj fields

        let rules(value: obj) : string * obj =
            "rules" ==> value

        let rulesMany(values: seq<obj>) : string * obj =
            "rules" ==> Seq.toArray values

        let sitemap(value: string) : string * obj =
            "sitemap" ==> value

        let sitemaps(values: seq<string>) : string * obj =
            "sitemap" ==> Seq.toArray values

        let host(value: string) : string * obj =
            "host" ==> value

    module SitemapEntry =
        let create(fields: (string * obj) list) : obj =
            createObj fields

        let url(value: string) : string * obj =
            "url" ==> value

        let lastModified(value: obj) : string * obj =
            "lastModified" ==> value

        let changeFrequency(value: SitemapChangeFrequency) : string * obj =
            "changeFrequency" ==> value

        let priority(value: float) : string * obj =
            "priority" ==> value

        let images(values: seq<string>) : string * obj =
            "images" ==> Seq.toArray values

    module ManifestIcon =
        let create(fields: (string * obj) list) : obj =
            createObj fields

        let src(value: string) : string * obj =
            "src" ==> value

        let sizes(value: string) : string * obj =
            "sizes" ==> value

        let type'(value: string) : string * obj =
            "type" ==> value

        let purpose(value: string) : string * obj =
            "purpose" ==> value

    module Manifest =
        let create(fields: (string * obj) list) : obj =
            createObj fields

        let name(value: string) : string * obj =
            "name" ==> value

        let shortName(value: string) : string * obj =
            "short_name" ==> value

        let description(value: string) : string * obj =
            "description" ==> value

        let startUrl(value: string) : string * obj =
            "start_url" ==> value

        let display(value: string) : string * obj =
            "display" ==> value

        let backgroundColor(value: string) : string * obj =
            "background_color" ==> value

        let themeColor(value: string) : string * obj =
            "theme_color" ==> value

        let icons(values: seq<obj>) : string * obj =
            "icons" ==> Seq.toArray values

        let id(value: string) : string * obj =
            "id" ==> value

        let orientation(value: string) : string * obj =
            "orientation" ==> value

module ImageResponse =
    [<Import("ImageResponse", "next/og")>]
    let private imageResponseImport: obj = jsNative

    [<Emit("new $0($1)")>]
    let private createImport(imageResponseType: obj, element: ReactElement) : obj = jsNative

    [<Emit("new $0($1, $2)")>]
    let private createWithOptionsImport(imageResponseType: obj, element: ReactElement, options: obj) : obj = jsNative

    let create(element: ReactElement) : obj =
        createImport(imageResponseImport, element)

    let createWithOptions(element: ReactElement) (options: obj) : obj =
        createWithOptionsImport(imageResponseImport, element, options)

module Font =
    [<ImportDefault("next/font/local")>]
    let private localImport: obj -> LoadedFont = jsNative

    let local(options: obj) : LoadedFont =
        localImport options

module Link =
    [<ImportDefault("next/link")>]
    let private componentImport: obj = jsNative

    [<Import("useLinkStatus", "next/link")>]
    let private useLinkStatusImport: unit -> LinkStatus = jsNative

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

    let useLinkStatus() : LinkStatus =
        useLinkStatusImport()

module Image =
    [<ImportDefault("next/image")>]
    let private componentImport: obj = jsNative

    [<Import("getImageProps", "next/image")>]
    let private getImagePropsImport: obj -> ImagePropsResult = jsNative

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

    let getImageProps(options: obj) : ImagePropsResult =
        getImagePropsImport options

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

module WebVitals =
    [<Import("useReportWebVitals", "next/web-vitals")>]
    let private useReportWebVitalsImport: (WebVitalMetric -> unit) -> unit = jsNative

    let useReportWebVitals(handler: WebVitalMetric -> unit) : unit =
        useReportWebVitalsImport handler

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

    [<Import("useSelectedLayoutSegment", "next/navigation")>]
    let private useSelectedLayoutSegmentWithKeyImport: string -> string option = jsNative

    [<Import("useSelectedLayoutSegments", "next/navigation")>]
    let private useSelectedLayoutSegmentsImport: unit -> string array = jsNative

    [<Import("useSelectedLayoutSegments", "next/navigation")>]
    let private useSelectedLayoutSegmentsWithKeyImport: string -> string array = jsNative

    [<Import("useServerInsertedHTML", "next/navigation")>]
    let private useServerInsertedHTMLImport: (unit -> ReactElement) -> unit = jsNative

    [<Import("redirect", "next/navigation")>]
    let private redirectImport: string * RedirectType -> unit = jsNative

    [<Import("permanentRedirect", "next/navigation")>]
    let private permanentRedirectImport: string -> unit = jsNative

    [<Import("notFound", "next/navigation")>]
    let private notFoundImport: unit -> unit = jsNative

    [<Import("forbidden", "next/navigation")>]
    let private forbiddenImport: unit -> unit = jsNative

    [<Import("unauthorized", "next/navigation")>]
    let private unauthorizedImport: unit -> unit = jsNative

    [<Import("unstable_rethrow", "next/navigation")>]
    let private unstableRethrowImport: obj -> unit = jsNative

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

    let redirect(path: string) : unit =
        redirectImport(path, RedirectType.Replace)

    let redirectWith(path: string, redirectType: RedirectType) : unit =
        redirectImport(path, redirectType)

    let permanentRedirect(path: string) : unit =
        permanentRedirectImport(path)

    let notFound() : unit =
        notFoundImport()

    let forbidden() : unit =
        forbiddenImport()

    let unauthorized() : unit =
        unauthorizedImport()

    let unstableRethrow(error: obj) : unit =
        unstableRethrowImport error

    let unstableIsUnrecognizedActionError(error: obj) : bool =
        unstableIsUnrecognizedActionErrorImport error

module ServerRequest =
    [<Import("NextRequest", "next/server")>]
    let private nextRequestImport: obj = jsNative

    [<Emit("new $0($1)")>]
    let private createImport(nextRequestType: obj, input: obj) : NextRequest = jsNative

    [<Emit("new $0($1, $2)")>]
    let private createWithInitImport(nextRequestType: obj, input: obj, init: obj) : NextRequest = jsNative

    let create(url: string) : NextRequest =
        createImport(nextRequestImport, box url)

    let createFrom(input: obj) : NextRequest =
        createImport(nextRequestImport, input)

    let createWithInit(url: string) (init: obj) : NextRequest =
        createWithInitImport(nextRequestImport, box url, init)

    let createFromWithInit(input: obj) (init: obj) : NextRequest =
        createWithInitImport(nextRequestImport, input, init)

module Cache =
    [<Import("cacheLife", "next/cache")>]
    let private cacheLifeProfileImport: CacheProfile -> unit = jsNative

    [<Import("cacheLife", "next/cache")>]
    let private cacheLifeProfileNameImport: string -> unit = jsNative

    [<Import("cacheLife", "next/cache")>]
    let private cacheLifeOptionsImport: obj -> unit = jsNative

    [<Import("cacheTag", "next/cache")>]
    let private cacheTagImport([<System.ParamArray>] tags: string array) : unit = jsNative

    [<Import("refresh", "next/cache")>]
    let private refreshImport: unit -> unit = jsNative

    [<Import("revalidatePath", "next/cache")>]
    let private revalidatePathImport: string -> unit = jsNative

    [<Import("revalidatePath", "next/cache")>]
    let private revalidatePathWithTypeImport: string * RevalidatePathType -> unit = jsNative

    [<Import("revalidateTag", "next/cache")>]
    let private revalidateTagImport: string -> unit = jsNative

    [<Import("revalidateTag", "next/cache")>]
    let private revalidateTagWithProfileImport: string * RevalidateTagProfile -> unit = jsNative

    [<Import("revalidateTag", "next/cache")>]
    let private revalidateTagWithCustomProfileImport: string * string -> unit = jsNative

    [<Import("updateTag", "next/cache")>]
    let private updateTagImport: string -> unit = jsNative

    [<Import("unstable_noStore", "next/cache")>]
    let private noStoreImport: unit -> unit = jsNative

    let cacheLifeProfile(profile: CacheProfile) : unit =
        cacheLifeProfileImport profile

    let cacheLifeProfileName(profileName: string) : unit =
        cacheLifeProfileNameImport profileName

    let cacheLife(options: obj) : unit =
        cacheLifeOptionsImport options

    let cacheTag(tag: string) : unit =
        cacheTagImport [| tag |]

    let cacheTags(tags: seq<string>) : unit =
        cacheTagImport (Seq.toArray tags)

    let refresh() : unit =
        refreshImport()

    let revalidatePath(path: string) : unit =
        revalidatePathImport path

    let revalidatePathType(path: string) (pathType: RevalidatePathType) : unit =
        revalidatePathWithTypeImport(path, pathType)

    let revalidateTag(tag: string) : unit =
        revalidateTagImport tag

    let revalidateTagWithProfile(tag: string) (profile: RevalidateTagProfile) : unit =
        revalidateTagWithProfileImport(tag, profile)

    let revalidateTagWithCustomProfile(tag: string) (profileName: string) : unit =
        revalidateTagWithCustomProfileImport(tag, profileName)

    let updateTag(tag: string) : unit =
        updateTagImport tag

    let noStore() : unit =
        noStoreImport()

module Server =
    [<Import("after", "next/server")>]
    let private afterImport: obj -> unit = jsNative

    [<Import("headers", "next/headers")>]
    let private headersImport: unit -> JS.Promise<HeadersCollection> = jsNative

    [<Import("cookies", "next/headers")>]
    let private cookiesImport: unit -> JS.Promise<RequestCookieStore> = jsNative

    [<Import("draftMode", "next/headers")>]
    let private draftModeImport: unit -> JS.Promise<DraftModeState> = jsNative

    [<Import("connection", "next/server")>]
    let private connectionImport: unit -> JS.Promise<unit> = jsNative

    [<Import("userAgent", "next/server")>]
    let private userAgentImport: NextRequest -> UserAgentInfo = jsNative

    [<Import("userAgentFromString", "next/server")>]
    let private userAgentFromStringImport: string -> UserAgentInfo = jsNative

    let after(callback: unit -> unit) : unit =
        afterImport callback

    let afterAsync(callback: unit -> JS.Promise<unit>) : unit =
        afterImport callback

    let headers() : JS.Promise<HeadersCollection> =
        headersImport()

    let cookies() : JS.Promise<RequestCookieStore> =
        cookiesImport()

    let draftMode() : JS.Promise<DraftModeState> =
        draftModeImport()

    let connection() : JS.Promise<unit> =
        connectionImport()

    let userAgent(request: NextRequest) : UserAgentInfo =
        userAgentImport request

    let userAgentFromString(userAgentValue: string) : UserAgentInfo =
        userAgentFromStringImport userAgentValue

module ServerFetch =
    [<Emit("fetch($0)")>]
    let private fetchInput(input: obj) : JS.Promise<ServerFetchResponse> = jsNative

    [<Emit("fetch($0, $1)")>]
    let private fetchInputWithInit(input: obj, init: obj) : JS.Promise<ServerFetchResponse> = jsNative

    let fetch(url: string) : JS.Promise<ServerFetchResponse> =
        fetchInput(box url)

    let fetchWithInit(url: string) (init: obj) : JS.Promise<ServerFetchResponse> =
        fetchInputWithInit(box url, init)

    let fetchFrom(input: obj) : JS.Promise<ServerFetchResponse> =
        fetchInput input

    let fetchFromWithInit(input: obj) (init: obj) : JS.Promise<ServerFetchResponse> =
        fetchInputWithInit(input, init)

module ServerResponse =
    [<Import("NextResponse", "next/server")>]
    let private nextResponseImport: obj = jsNative

    [<Emit("new $0()")>]
    let private createEmptyImport(nextResponse: obj) : NextResponse = jsNative

    [<Emit("new $0($1)")>]
    let private createImport(nextResponse: obj, body: obj) : NextResponse = jsNative

    [<Emit("new $0($1, $2)")>]
    let private createWithInitImport(nextResponse: obj, body: obj, init: obj) : NextResponse = jsNative

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

    let create() : NextResponse =
        createEmptyImport nextResponseImport

    let createWithBody(body: obj) : NextResponse =
        createImport(nextResponseImport, body)

    let createWithInit(body: obj) (init: obj) : NextResponse =
        createWithInitImport(nextResponseImport, body, init)

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
