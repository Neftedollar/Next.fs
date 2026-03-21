# NextFs Starter

Minimal App Router starter showing the intended NextFs flow:

- F# source lives under `src/App/**`
- thin Next.js wrapper files live under `app/**`
- `nextfs.entries.json` tells the wrapper generator where each JS entry should land

## Files

- `src/NextFs.Starter.fsproj` - F# project for the example modules
- `src/App/Page.fs` - client page component with an inline server action
- `src/App/ClientCounter.fs` - client component boundary
- `src/App/Actions.fs` - standalone server action entry
- `src/App/Api/Posts.fs` - route handler
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

The `app/layout.jsx` file is plain Next.js and does not need a wrapper.
