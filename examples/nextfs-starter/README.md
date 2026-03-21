# NextFs Starter

Minimal App Router starter showing the intended NextFs flow:

- F# source lives under `src/App/**`
- root-level F# entry points can live outside `App/**`, for example `src/Proxy.fs`
- compiled Fable output lands in `.fable/**`
- thin Next.js wrapper files live under `app/**`
- `nextfs.entries.json` tells the wrapper generator where each JS entry should land
- layout, metadata, viewport, `next/font`, route handlers, and `proxy.js` can all come from F#

## Tree

```text
examples/nextfs-starter/
├── app/
│   ├── actions.js              # generated
│   ├── api/posts/route.js      # generated
│   ├── components/client-counter.js # generated
│   ├── default.js              # generated
│   ├── error.js                # generated
│   ├── forbidden.js            # generated
│   ├── global-error.js         # generated
│   ├── global-not-found.js     # generated
│   ├── layout.js               # generated
│   ├── loading.js              # generated
│   ├── not-found.js            # generated
│   ├── page.js                 # generated
│   ├── template.js             # generated
│   └── unauthorized.js         # generated
├── next.config.mjs             # source
├── instrumentation-client.js   # generated
├── instrumentation.js          # generated
├── proxy.js                    # generated
├── src/
│   ├── Instrumentation.fs      # source
│   ├── InstrumentationClient.fs # source
│   ├── Proxy.fs                # source
│   ├── NextFs.Starter.fsproj   # source
│   └── App/
│       ├── Actions.fs          # source
│       ├── Api/Posts.fs        # source
│       ├── ClientCounter.fs    # source
│       ├── Default.fs          # source
│       ├── Error.fs            # source
│       ├── Forbidden.fs        # source
│       ├── GlobalError.fs      # source
│       ├── GlobalNotFound.fs   # source
│       ├── Layout.fs           # source
│       ├── Loading.fs          # source
│       ├── NotFound.fs         # source
│       ├── Page.fs             # source
│       ├── Template.fs         # source
│       └── Unauthorized.fs     # source
└── nextfs.entries.json         # source
```

## Files

- `src/NextFs.Starter.fsproj` - F# project for the example modules
- `src/Instrumentation.fs` - root `instrumentation.js` entry from F#
- `src/InstrumentationClient.fs` - root `instrumentation-client.js` entry with client transition hooks
- `src/Proxy.fs` - root-level `proxy.js` entry exported from F#
- `next.config.mjs` - enables `experimental.authInterrupts` and `experimental.globalNotFound`
- `src/App/Layout.fs` - root layout plus `metadata` and `viewport` exports
- `src/App/Page.fs` - client page component with an inline server action
- `src/App/ClientCounter.fs` - client component boundary with App Router segment hooks
- `src/App/Actions.fs` - standalone server action entry
- `src/App/Api/Posts.fs` - route handler plus `runtime`, `preferredRegion`, and `maxDuration` exports
- `src/App/Error.fs` and `src/App/GlobalError.fs` - client error boundaries from F#
- `src/App/Loading.fs`, `src/App/NotFound.fs`, `src/App/GlobalNotFound.fs` - route fallback UIs
- `src/App/Template.fs` and `src/App/Default.fs` - special-file component conventions
- `src/App/Forbidden.fs` and `src/App/Unauthorized.fs` - auth interrupt UIs
- `proxy.js` - generated wrapper for the proxy entry
- `instrumentation.js` - generated wrapper for server instrumentation exports
- `instrumentation-client.js` - generated wrapper for client instrumentation side effects
- `app/error.js` and `app/global-error.js` - generated client wrappers for error boundaries
- `app/global-not-found.js` - generated root not-found entry
- `app/layout.js` - generated wrapper for the root layout plus metadata exports
- `app/page.js` - generated wrapper for the page entry
- `app/components/client-counter.js` - generated wrapper for the client component
- `app/actions.js` - generated wrapper for the server action
- `app/api/posts/route.js` - generated wrapper for the route handler
- route-handler wrappers can re-export config values alongside HTTP verbs

## Local Flow

Start from the repository root:

```bash
dotnet tool restore
```

Then switch to `examples/nextfs-starter` and install the JavaScript dependencies:

```bash
npm install
```

The shortest repeatable loop is:

```bash
npm run sync:app
npm run dev
```

For iterative work, keep Fable and Next.js in separate terminals:

```bash
npm run watch:fable
npm run dev
```

Command boundaries:

- `npm run build:fsharp` only verifies that the F# project compiles under .NET. It does not emit `.fable/**`.
- `npm run build:fable` is the actual JavaScript emit step for Next.js entry modules.
- `npm run gen:wrappers` regenerates the checked-in `app/**`, `proxy.js`, and instrumentation wrappers from `nextfs.entries.json`.

The checked-in `app/**` and `proxy.js` files are generated wrappers. Regenerate them after each Fable emit, or let `watch:fable` do it for you.

The starter route handler also demonstrates typed server `fetch()` options through `ServerFetchInit` and `NextFetchOptions`.

## Generated vs. Source Files

Edit these files by hand:

- `src/**`
- `next.config.mjs`
- `nextfs.entries.json`

Do not edit these by hand:

- `.fable/**`
- `app/**`
- `proxy.js`
- `instrumentation.js`
- `instrumentation-client.js`

## Current Fable Note

As of March 21, 2026, some environments still hit an upstream `dotnet fable` hang in Fable 4.29.x. The current upstream tracker is [fable-compiler/Fable#4326](https://github.com/fable-compiler/Fable/issues/4326).

This starter still documents the intended pipeline and keeps the generated wrappers checked in, but the live Fable emit step may depend on the host shell and SDK setup until that upstream issue is fully resolved.

The special-file entries in this starter intentionally demonstrate a broad App Router surface. `forbidden.js`, `unauthorized.js`, and `global-not-found.js` depend on experimental Next.js flags, so treat them as current-pattern examples rather than frozen APIs.

For the code patterns behind these files, see [the starter walkthrough](../../docs/starter-app-walkthrough.md), [Data fetching and route config](../../docs/data-fetching-and-route-config.md), [Server and client patterns](../../docs/server-client-patterns.md), [Directives and wrappers](../../docs/directives-wrappers.md), and [Special files](../../docs/special-files.md).
