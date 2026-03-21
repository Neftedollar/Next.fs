# NextFs Starter

Minimal App Router starter showing the intended NextFs flow:

- F# source lives under `src/App/**`
- root-level F# entry points can live outside `App/**`, for example `src/Proxy.fs`
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
├── proxy.js                    # generated
├── src/
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
- `src/Proxy.fs` - root-level `proxy.js` entry exported from F#
- `next.config.mjs` - enables `experimental.authInterrupts` and `experimental.globalNotFound`
- `src/App/Layout.fs` - root layout plus `metadata` and `viewport` exports
- `src/App/Page.fs` - client page component with an inline server action
- `src/App/ClientCounter.fs` - client component boundary
- `src/App/Actions.fs` - standalone server action entry
- `src/App/Api/Posts.fs` - route handler
- `src/App/Error.fs` and `src/App/GlobalError.fs` - client error boundaries from F#
- `src/App/Loading.fs`, `src/App/NotFound.fs`, `src/App/GlobalNotFound.fs` - route fallback UIs
- `src/App/Template.fs` and `src/App/Default.fs` - special-file component conventions
- `src/App/Forbidden.fs` and `src/App/Unauthorized.fs` - auth interrupt UIs
- `proxy.js` - generated wrapper for the proxy entry
- `app/error.js` and `app/global-error.js` - generated client wrappers for error boundaries
- `app/global-not-found.js` - generated root not-found entry
- `app/layout.js` - generated wrapper for the root layout plus metadata exports
- `app/page.js` - generated wrapper for the page entry
- `app/components/client-counter.js` - generated wrapper for the client component
- `app/actions.js` - generated wrapper for the server action
- `app/api/posts/route.js` - generated wrapper for the route handler

## Local Flow

Assume your Fable build emits JS into `.fable/` at the project root.

```bash
npm install
dotnet build src/NextFs.Starter.fsproj
node ../../tools/nextfs-entry.mjs nextfs.entries.json
npm run dev
```

The checked-in `app/**` and `proxy.js` files are generated wrappers. In a real project you regenerate them after each Fable build.

The special-file entries in this starter intentionally demonstrate a broad App Router surface. `forbidden.js`, `unauthorized.js`, and `global-not-found.js` depend on experimental Next.js flags, so treat them as current-pattern examples rather than frozen APIs.
