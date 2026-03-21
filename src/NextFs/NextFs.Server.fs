namespace NextFs

open Fable.Core
open Feliz

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

module Navigation =
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
    let private cacheLifeImport: obj -> unit = jsNative

    [<Import("cacheTag", "next/cache")>]
    let private cacheTagImport: string -> unit = jsNative

    [<Import("refresh", "next/cache")>]
    let private refreshImport: unit -> unit = jsNative

    [<Import("revalidatePath", "next/cache")>]
    let private revalidatePathImport: string -> unit = jsNative

    [<Import("revalidatePath", "next/cache")>]
    let private revalidatePathTypeImport: string * RevalidatePathType -> unit = jsNative

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
        cacheLifeImport options

    let cacheTag(tag: string) : unit =
        cacheTagImport tag

    let cacheTags(tags: seq<string>) : unit =
        Seq.iter cacheTagImport tags

    let refresh() : unit =
        refreshImport()

    let revalidatePath(path: string) : unit =
        revalidatePathImport path

    let revalidatePathType(path: string) (pathType: RevalidatePathType) : unit =
        revalidatePathTypeImport(path, pathType)

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
    let private afterImport: (unit -> unit) -> unit = jsNative

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
        afterImport(fun () -> ignore (callback()))

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
    let private fetchImport(url: obj) : JS.Promise<ServerFetchResponse> = jsNative

    [<Emit("fetch($0, $1)")>]
    let private fetchWithInitImport(url: obj, init: obj) : JS.Promise<ServerFetchResponse> = jsNative

    let fetch(url: string) : JS.Promise<ServerFetchResponse> =
        fetchImport(box url)

    let fetchWithInit(url: string) (init: obj) : JS.Promise<ServerFetchResponse> =
        fetchWithInitImport(box url, init)

    let fetchFrom(input: obj) : JS.Promise<ServerFetchResponse> =
        fetchImport input

    let fetchFromWithInit(input: obj) (init: obj) : JS.Promise<ServerFetchResponse> =
        fetchWithInitImport(input, init)

module ServerResponse =
    [<Import("NextResponse", "next/server")>]
    let private nextResponseImport: obj = jsNative

    [<Emit("new $0()")>]
    let private createImport(nextResponseType: obj) : NextResponse = jsNative

    [<Emit("new $0($1)")>]
    let private createWithBodyImport(nextResponseType: obj, body: obj) : NextResponse = jsNative

    [<Emit("new $0($1, $2)")>]
    let private createWithInitImport(nextResponseType: obj, body: obj, init: obj) : NextResponse = jsNative

    [<Emit("$0.json($1)")>]
    let private jsonImport(nextResponseType: obj, body: obj) : NextResponse = jsNative

    [<Emit("$0.json($1, $2)")>]
    let private jsonWithInitImport(nextResponseType: obj, body: obj, init: obj) : NextResponse = jsNative

    [<Emit("$0.redirect($1)")>]
    let private redirectImport(nextResponseType: obj, url: obj) : NextResponse = jsNative

    [<Emit("$0.redirect($1, $2)")>]
    let private redirectWithInitImport(nextResponseType: obj, url: obj, init: obj) : NextResponse = jsNative

    [<Emit("$0.rewrite($1)")>]
    let private rewriteImport(nextResponseType: obj, url: obj) : NextResponse = jsNative

    [<Emit("$0.rewrite($1, $2)")>]
    let private rewriteWithInitImport(nextResponseType: obj, url: obj, init: obj) : NextResponse = jsNative

    [<Emit("$0.next()")>]
    let private nextImport(nextResponseType: obj) : NextResponse = jsNative

    [<Emit("$0.next($1)")>]
    let private nextWithInitImport(nextResponseType: obj, init: obj) : NextResponse = jsNative

    let create() : NextResponse =
        createImport nextResponseImport

    let createWithBody(body: obj) : NextResponse =
        createWithBodyImport(nextResponseImport, body)

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
        nextImport nextResponseImport

    let nextWithInit(init: obj) : NextResponse =
        nextWithInitImport(nextResponseImport, init)
