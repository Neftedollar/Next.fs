namespace NextFs

open Fable.Core
open Feliz

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

    /// Sets `placeholder` directly to an inline data URL (e.g. `"data:image/png;base64,..."`).
    /// This is the self-contained form: no separate blur source is needed.
    /// To use Next.js blur effect instead, combine `placeholder ImagePlaceholder.Blur` with `blurDataUrl`.
    let placeholderDataUrl(value: string) : IReactProperty =
        Props.mkAttr "placeholder" value

    /// Sets `blurDataURL` — the low-resolution source image used when `placeholder` is `ImagePlaceholder.Blur`.
    /// Always pair this with `placeholder ImagePlaceholder.Blur`.
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
