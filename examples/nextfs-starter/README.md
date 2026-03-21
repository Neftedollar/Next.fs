# NextFs Starter

Minimal App Router starter showing the intended NextFs flow:

- F# source lives under `src/App/**`
- root-level F# entry points can live outside `App/**`, for example `src/Instrumentation.fs`
- compiled Fable output lands in `.fable/**`
- thin Next.js wrapper files live under `app/**`
- `nextfs.entries.json` tells the wrapper generator where each JS entry should land
- layout, metadata, viewport, `next/font`, route handlers, and instrumentation can all come from F#

## Tree

```text
examples/nextfs-starter/
├── app/
│   ├── actions.js              # generated
│   ├── api/posts/route.js      # generated
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
- `src/Proxy.fs` - optional root-level proxy example kept in F# source form
- `next.config.mjs` - enables `experimental.authInterrupts` and `experimental.globalNotFound`
- `src/App/Layout.fs` - root layout plus `metadata` and `viewport` exports
- `src/App/Page.fs` - client page component that posts to the route handler
- `src/App/ClientCounter.fs` - client component boundary with App Router segment hooks
- `src/App/Actions.fs` - standalone server action entry
- `src/App/Api/Posts.fs` - route handler plus `runtime`, `preferredRegion`, and `maxDuration` exports
- `src/App/Error.fs` and `src/App/GlobalError.fs` - client error boundaries from F#
- `src/App/Loading.fs`, `src/App/NotFound.fs`, `src/App/GlobalNotFound.fs` - route fallback UIs
- `src/App/Template.fs` and `src/App/Default.fs` - special-file component conventions
- `src/App/Forbidden.fs` and `src/App/Unauthorized.fs` - auth interrupt UIs
- `instrumentation.js` - generated wrapper for server instrumentation exports
- `instrumentation-client.js` - generated wrapper for client instrumentation side effects
- `app/error.js` and `app/global-error.js` - generated client wrappers for error boundaries
- `app/global-not-found.js` - generated root not-found entry
- `app/layout.js` - generated wrapper for the root layout plus metadata exports
- `app/page.js` - generated wrapper for the page entry
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
- `npm run gen:wrappers` regenerates the checked-in `app/**` and instrumentation wrappers from `nextfs.entries.json`.

The checked-in `app/**`, `instrumentation.js`, and `instrumentation-client.js` files are generated wrappers. Regenerate them after each Fable emit, or let `watch:fable` do it for you.

The starter route handler also demonstrates typed server `fetch()` options through `ServerFetchInit` and `NextFetchOptions`.

## Generated vs. Source Files

Edit these files by hand:

- `src/**`
- `next.config.mjs`
- `nextfs.entries.json`

Do not edit these by hand:

- `.fable/**`
- `app/**`
- `instrumentation.js`
- `instrumentation-client.js`

## Build Status

As of March 21, 2026, this starter is validated end-to-end with:

```bash
npm run sync:app
npm run build
```

That flow performs a real Fable emit, regenerates the checked-in wrappers, and completes a real `next build` on Next 16.

The special-file entries in this starter intentionally demonstrate a broad App Router surface. `forbidden.js`, `unauthorized.js`, and `global-not-found.js` depend on experimental Next.js flags, so treat them as current-pattern examples rather than frozen APIs.

For the code patterns behind these files, see [the starter walkthrough](../../docs/starter-app-walkthrough.md), [Data fetching and route config](../../docs/data-fetching-and-route-config.md), [Server and client patterns](../../docs/server-client-patterns.md), [Directives and wrappers](../../docs/directives-wrappers.md), and [Special files](../../docs/special-files.md).
