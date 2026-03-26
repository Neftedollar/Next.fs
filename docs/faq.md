# FAQ

Common questions about using `NextFs`.

---

## Why doesn't my `'use client'` directive work?

Fable-generated JavaScript starts with `import` statements. Next.js requires `'use client'` to be the **first** token in the file, before any imports.

The fix is a thin wrapper file. Annotate your module with `[<NextFs.NextFsEntry>]` and run `npm run sync:app` — the generator writes the wrapper automatically:

```fsharp
[<NextFs.NextFsEntry("app/counter.js", Directive="use client", Default="Counter")>]
module App.Counter
```

See [Directives and wrappers](directives-wrappers.md) for the full explanation.

---

## Why does `next build` reject my font options?

Next.js statically analyzes `next/font` loader calls at build time. It cannot trace option objects that are built by helper functions.

This **fails** in a production build:

```fsharp
let opts = FontOptions.create [ FontOptions.subsets ["latin"] ]
let inter = GoogleFont.Inter opts  // static analysis cannot follow this
```

This **works**:

```fsharp
let inter =
    GoogleFont.Inter(
        box {| subsets = [| "latin" |]; display = "swap" |}
    )
```

Keep the options as an anonymous-record or object-literal expression directly inside the loader call, inside the entry module.

---

## My F# module compiles fine but the wrapper exports nothing

Check that:

1. The F# binding you want to export is at module level, not nested inside a type.
2. The `Named` or `Default` parameter of `[<NextFs.NextFsEntry>]` matches the exact Fable-compiled name.

Fable can rename let-bindings. Use `[<CompiledName("exactName")>]` on the binding if you need to control the output name. Route handler verbs require `[<CompiledName("GET")>]` etc.

---

## Can I use `NavigationClient.useRouter` in a server component?

No. All `NavigationClient.*` hooks are client-only. Using them in a server component will throw at runtime.

The module split is intentional:

- `Navigation.*` — server-safe (redirects, `notFound`, etc.)
- `NavigationClient.*` — client-only hooks (useRouter, usePathname, etc.)

If you need routing in a server component, use `Navigation.redirect` or `Navigation.notFound` directly.

---

## `Server.headers()` returns a Promise — how do I await it?

Server-side request APIs are promise-based in Next.js 15+. Unwrap them with `Async.AwaitPromise`:

```fsharp
[<ExportDefault>]
let Page() =
    async {
        let! headers = Async.AwaitPromise(Server.headers())
        let ua = headers.get("user-agent") |> Option.defaultValue "unknown"
        return Html.pre ua
    }
    |> Async.StartAsPromise
```

---

## Can I use `Directive.useServer()` inside a client component?

No. Next.js rejects inline `'use server'` inside a `'use client'` file at build time.

Define server actions in a separate module annotated with `Directive="use server"` and import them into your client component.

---

## Why is `forbidden()` / `unauthorized()` not working?

These APIs require the `experimental.authInterrupts` flag in `next.config.mjs`:

```js
const nextConfig = {
  experimental: {
    authInterrupts: true,
  },
}
```

See the [Next.js documentation](https://nextjs.org/docs/app/api-reference/functions/forbidden) for details.

---

## My `proxy.js` config isn't working — the middleware never runs

The middleware `config` object must be statically analyzable. Use `[<NextFs.NextFsStaticExport>]` instead of a Fable-computed `ProxyConfig`:

```fsharp
[<NextFs.NextFsEntry("proxy.js", Named="proxy")>]
[<NextFs.NextFsStaticExport("config", """{"matcher":["/((?!_next/static|_next/image|favicon.ico).*)"]}""")>]
module Proxy
```

`ProxyConfig.create` is still useful for the runtime `proxy` function itself — just not for the `config` object that Next.js reads at build time.

---

## How do I add route segment config (`runtime`, `dynamicParams`, `maxDuration`)?

Export plain `let` values from the F# module and list them in the `Named` parameter:

```fsharp
[<NextFs.NextFsEntry("app/api/posts/route.js", Named="GET POST runtime maxDuration")>]
module App.Api.Posts

open NextFs

[<CompiledName("GET")>]
let get (request: NextRequest, _ctx: obj) = ...

let runtime = RouteRuntime.Edge
let maxDuration = 30
```

---

## `nextfs.entries.json` keeps getting regenerated with different content

Make sure you always run `npm run scan` from the same working directory (the project root, where `package.json` lives). The scanner anchors output paths relative to the `package.json` location.

Also check that you haven't mixed `[<NextFsEntry>]` (unqualified) and `[<NextFs.NextFsEntry>]` (qualified) forms — both are accepted but be consistent.

---

## How do I write a test for a server component that calls `Server.headers()`?

Server components that call request-context APIs cannot be unit-tested in isolation because those APIs throw outside a Next.js request context.

Options:

- Extract business logic into a pure F# function, test that function directly.
- Use the E2E pattern from the starter CI: `next build && next start`, then `curl` the rendered output.

---

## Can I use Elmish with NextFs?

Yes, via `Feliz.UseElmish`. Add `Fable.Elmish` and `Feliz.UseElmish` to your `.fsproj`, then use `React.useElmish` inside a `[<ReactComponent>]`:

```fsharp
open Feliz.UseElmish
open Elmish

[<ReactComponent>]
let Counter () =
    let model, dispatch = React.useElmish(init, update)
    Html.div [ ... ]
```

The component must sit inside (or be) a `'use client'` entry. It does not need its own `[<NextFsEntry>]` annotation if it is imported by a page or layout that already carries the `'use client'` directive.

`Program.withReactSynchronous` and `Program.withReactBatched` do **not** work with App Router. Next.js owns the React root, so mounting Elmish directly to a DOM node is not supported. `React.useElmish` is the correct replacement.

For a complete working example, see [examples/nextfs-elmish](../examples/nextfs-elmish/README.md).

---

## Where do I report a binding mismatch?

Open an issue at [github.com/Neftedollar/Next.fs/issues](https://github.com/Neftedollar/Next.fs/issues). Include:

- the Next.js API you expected to call
- the JavaScript shape it produces
- the F# you wrote and what was emitted
