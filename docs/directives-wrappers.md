# Directives and Wrappers

Next.js file directives are a packaging concern, not just an F# syntax concern.

## Inline server actions

`Directive.useServer()` emits a `'use server'` statement inside the function body. Use it for inline server actions:

```fsharp
open NextFs

let saveSearch (_formData: obj) =
    Directive.useServer()
    ()
```

This covers the function-level server-action case.

`NextFs` also exposes cache directives for modern App Router caching flows:

```fsharp
let loadNavigationLabels () =
    Directive.useCache()
    Cache.cacheLifeProfile CacheProfile.Hours
    Cache.cacheTag "navigation"

    [| "Home"; "Docs" |]
```

Available inline directives today:

- `Directive.useServer()`
- `Directive.useCache()`
- `Directive.useCachePrivate()`
- `Directive.useCacheRemote()`

## Why wrappers exist

Next.js expects `'use client'` and `'use server'` to be the first statement in the emitted JavaScript file.

Fable-generated output starts with imports, so an App Router entry module cannot always carry the directive directly. The fix is a thin wrapper file that contains the directive and re-exports the compiled Fable module.

## Wrapper generator

The repository includes a small generator:

```bash
node tools/nextfs-entry.mjs samples/nextfs.entries.json
```

It reads a config file and writes wrapper files to the target paths.

Supported entry fields:

- `from`: compiled Fable output to re-export
- `to`: wrapper file to generate
- `directive`: optional, either `"use client"` or `"use server"`
- `default`: re-export the default export
- `named`: re-export the named exports listed in the array
- `exportAll`: re-export everything from the source module

Common patterns:

| Case | `directive` | `default` | `named` | Typical `to` |
| --- | --- | --- | --- | --- |
| client page | `"use client"` | `true` | none | `app/page.js` |
| client component | `"use client"` | `true` | none | `app/components/counter.js` |
| server actions module | `"use server"` | `false` | `["createPost"]` | `app/actions.js` |
| route handler | omitted | `false` | `["GET", "POST"]` | `app/api/posts/route.js` |
| proxy entry | omitted | `false` | `["proxy", "config"]` | `proxy.js` |
| server instrumentation | omitted | `false` | `["register", "onRequestError"]` | `instrumentation.js` |
| client instrumentation | omitted | `false` | none | `instrumentation-client.js` via `exportAll` |
| segment error | `"use client"` | `true` | none | `app/error.js` |
| global error | `"use client"` | `true` | none | `app/global-error.js` |
| global not found | omitted | `true` | `["metadata"]` optional | `app/global-not-found.js` |

Current rules enforced by the generator:

- `directive` must be omitted, `"use client"`, or `"use server"`
- `use server` wrappers cannot use `default`
- `use server` wrappers cannot use `exportAll`
- each entry must export at least one symbol
- generated output is deterministic and covered by `tests/nextfs-entry.test.mjs`

## Example config

```json
{
  "entries": [
    {
      "directive": "use client",
      "from": "./.fable/App.Page.js",
      "to": "./app/page.js",
      "default": true
    },
    {
      "directive": "use server",
      "from": "./.fable/App.Actions.js",
      "to": "./app/actions.js",
      "named": ["createPost", "deletePost"]
    },
    {
      "from": "./.fable/App.Api.Posts.js",
      "to": "./app/api/posts/route.js",
      "named": ["GET", "POST"]
    },
    {
      "from": "./.fable/Proxy.js",
      "to": "./proxy.js",
      "named": ["proxy", "config"]
    },
    {
      "from": "./.fable/Instrumentation.js",
      "to": "./instrumentation.js",
      "named": ["register", "onRequestError"]
    },
    {
      "from": "./.fable/InstrumentationClient.js",
      "to": "./instrumentation-client.js",
      "exportAll": true
    }
  ]
}
```

That example shows the three common cases:

- a client entry page with a default export
- a server-actions module with named exports only
- a route-handler module that does not need a directive
- a root-level `proxy.js` entry exported from F#
- root instrumentation entries that can stay directive-free
