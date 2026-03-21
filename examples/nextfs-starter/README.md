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
│   ├── layout.js               # generated
│   └── page.js                 # generated
├── proxy.js                    # generated
├── src/
│   ├── Proxy.fs                # source
│   ├── NextFs.Starter.fsproj   # source
│   └── App/
│       ├── Actions.fs          # source
│       ├── Api/Posts.fs        # source
│       ├── ClientCounter.fs    # source
│       ├── Layout.fs           # source
│       └── Page.fs             # source
└── nextfs.entries.json         # source
```

## Files

- `src/NextFs.Starter.fsproj` - F# project for the example modules
- `src/Proxy.fs` - root-level `proxy.js` entry exported from F#
- `src/App/Layout.fs` - root layout plus `metadata` and `viewport` exports
- `src/App/Page.fs` - client page component with an inline server action
- `src/App/ClientCounter.fs` - client component boundary
- `src/App/Actions.fs` - standalone server action entry
- `src/App/Api/Posts.fs` - route handler
- `proxy.js` - generated wrapper for the proxy entry
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
