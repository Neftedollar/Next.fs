namespace NextFs.Dsl

open NextFs

// ── OpenGraph ──

type OpenGraphState = { Fields: (string * obj) list }

type OpenGraphBuilder() =
    member _.Yield(_: unit) = { Fields = [] }

    [<CustomOperation("title")>]
    member _.Title(state: OpenGraphState, value: string) =
        { state with Fields = state.Fields @ [ MetadataOpenGraph.title value ] }

    [<CustomOperation("description")>]
    member _.Description(state: OpenGraphState, value: string) =
        { state with Fields = state.Fields @ [ MetadataOpenGraph.description value ] }

    [<CustomOperation("url")>]
    member _.Url(state: OpenGraphState, value: string) =
        { state with Fields = state.Fields @ [ MetadataOpenGraph.url value ] }

    [<CustomOperation("siteName")>]
    member _.SiteName(state: OpenGraphState, value: string) =
        { state with Fields = state.Fields @ [ MetadataOpenGraph.siteName value ] }

    [<CustomOperation("locale")>]
    member _.Locale(state: OpenGraphState, value: string) =
        { state with Fields = state.Fields @ [ MetadataOpenGraph.locale value ] }

    [<CustomOperation("ogType")>]
    member _.OgType(state: OpenGraphState, value: string) =
        { state with Fields = state.Fields @ [ MetadataOpenGraph.type' value ] }

    member _.Run(state: OpenGraphState) : obj =
        MetadataOpenGraph.create state.Fields

// ── Twitter ──

type TwitterState = { Fields: (string * obj) list }

type TwitterBuilder() =
    member _.Yield(_: unit) = { Fields = [] }

    [<CustomOperation("card")>]
    member _.Card(state: TwitterState, value: string) =
        { state with Fields = state.Fields @ [ MetadataTwitter.card value ] }

    [<CustomOperation("title")>]
    member _.Title(state: TwitterState, value: string) =
        { state with Fields = state.Fields @ [ MetadataTwitter.title value ] }

    [<CustomOperation("description")>]
    member _.Description(state: TwitterState, value: string) =
        { state with Fields = state.Fields @ [ MetadataTwitter.description value ] }

    [<CustomOperation("site")>]
    member _.Site(state: TwitterState, value: string) =
        { state with Fields = state.Fields @ [ MetadataTwitter.site value ] }

    [<CustomOperation("creator")>]
    member _.Creator(state: TwitterState, value: string) =
        { state with Fields = state.Fields @ [ MetadataTwitter.creator value ] }

    member _.Run(state: TwitterState) : obj =
        MetadataTwitter.create state.Fields

// ── Viewport ──

type ViewportState = { Fields: (string * obj) list }

type ViewportBuilder() =
    member _.Yield(_: unit) = { Fields = [] }

    [<CustomOperation("themeColor")>]
    member _.ThemeColor(state: ViewportState, value: string) =
        { state with Fields = state.Fields @ [ Viewport.themeColor value ] }

    [<CustomOperation("colorScheme")>]
    member _.ColorScheme(state: ViewportState, value: string) =
        { state with Fields = state.Fields @ [ Viewport.colorScheme value ] }

    [<CustomOperation("width")>]
    member _.Width(state: ViewportState, value: string) =
        { state with Fields = state.Fields @ [ Viewport.width value ] }

    [<CustomOperation("initialScale")>]
    member _.InitialScale(state: ViewportState, value: float) =
        { state with Fields = state.Fields @ [ Viewport.initialScale value ] }

    [<CustomOperation("maximumScale")>]
    member _.MaximumScale(state: ViewportState, value: float) =
        { state with Fields = state.Fields @ [ Viewport.maximumScale value ] }

    [<CustomOperation("userScalable")>]
    member _.UserScalable(state: ViewportState, value: bool) =
        { state with Fields = state.Fields @ [ Viewport.userScalable value ] }

    member _.Run(state: ViewportState) : obj =
        Viewport.create state.Fields

// ── Metadata (top-level) ──

type MetadataState = { Fields: (string * obj) list }

type MetadataBuilder() =
    member _.Yield(_: unit) = { Fields = [] }

    [<CustomOperation("title")>]
    member _.Title(state: MetadataState, value: string) =
        { state with Fields = state.Fields @ [ Metadata.title value ] }

    [<CustomOperation("titleTemplate")>]
    member _.TitleTemplate(state: MetadataState, defaultVal: string, template: string) =
        let t =
            Metadata.titleTemplate (
                MetadataTitle.create [
                    MetadataTitle.defaultValue defaultVal
                    MetadataTitle.template template
                ])
        { state with Fields = state.Fields @ [ t ] }

    [<CustomOperation("description")>]
    member _.Description(state: MetadataState, value: string) =
        { state with Fields = state.Fields @ [ Metadata.description value ] }

    [<CustomOperation("applicationName")>]
    member _.ApplicationName(state: MetadataState, value: string) =
        { state with Fields = state.Fields @ [ Metadata.applicationName value ] }

    [<CustomOperation("keywords")>]
    member _.Keywords(state: MetadataState, values: string list) =
        { state with Fields = state.Fields @ [ Metadata.keywords values ] }

    [<CustomOperation("creator")>]
    member _.Creator(state: MetadataState, value: string) =
        { state with Fields = state.Fields @ [ Metadata.creator value ] }

    [<CustomOperation("publisher")>]
    member _.Publisher(state: MetadataState, value: string) =
        { state with Fields = state.Fields @ [ Metadata.publisher value ] }

    [<CustomOperation("manifest")>]
    member _.Manifest(state: MetadataState, value: string) =
        { state with Fields = state.Fields @ [ Metadata.manifest value ] }

    [<CustomOperation("metadataBase")>]
    member _.MetadataBase(state: MetadataState, value: obj) =
        { state with Fields = state.Fields @ [ Metadata.metadataBase value ] }

    [<CustomOperation("openGraph")>]
    member _.OpenGraph(state: MetadataState, value: obj) =
        { state with Fields = state.Fields @ [ Metadata.openGraph value ] }

    [<CustomOperation("twitter")>]
    member _.Twitter(state: MetadataState, value: obj) =
        { state with Fields = state.Fields @ [ Metadata.twitter value ] }

    [<CustomOperation("icons")>]
    member _.Icons(state: MetadataState, value: obj) =
        { state with Fields = state.Fields @ [ Metadata.icons value ] }

    [<CustomOperation("robots")>]
    member _.Robots(state: MetadataState, value: obj) =
        { state with Fields = state.Fields @ [ Metadata.robots value ] }

    member _.Run(state: MetadataState) : obj =
        Metadata.create state.Fields

[<AutoOpen>]
module MetadataBuilderAutoOpen =
    let metadata = MetadataBuilder()
    let openGraph = OpenGraphBuilder()
    let twitter = TwitterBuilder()
    let viewport = ViewportBuilder()
