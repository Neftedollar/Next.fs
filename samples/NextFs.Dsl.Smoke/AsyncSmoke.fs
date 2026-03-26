module NextFs.Dsl.Smoke.AsyncSmoke

open NextFs
open NextFs.Dsl

let serverHelpers () =
    async {
        let! headers = ServerAsync.headers()
        let! cookies = ServerAsync.cookies()
        let! draft = ServerAsync.draftMode()
        do! ServerAsync.connection()

        ignore (headers.get "user-agent")
        ignore (cookies.get "theme")
        ignore draft.isEnabled
    }

let propsHelpers (pageProps: PageProps<{| slug: string |}, {| q: string option |}>) =
    async {
        let! routeParams = PropsAsync.routeParams pageProps
        let! searchParams = PropsAsync.searchParams pageProps

        ignore routeParams.slug
        ignore searchParams.q
    }

let layoutPropsHelpers (layoutProps: LayoutProps<{| team: string |}>) =
    async {
        let! routeParams = PropsAsync.layoutParams layoutProps
        ignore routeParams.team
    }

let defaultPropsHelpers (defaultProps: DefaultProps<{| team: string |}>) =
    async {
        let! routeParams = PropsAsync.defaultParams defaultProps
        ignore routeParams.team
    }

let contextParamsHelpers (ctx: RouteHandlerContext<{| id: string |}>) =
    async {
        let! routeParams = PropsAsync.contextParams ctx
        ignore routeParams.id
    }
