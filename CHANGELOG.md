# Changelog

## 1.1.0 - 2026-03-26

- added `NextFs.Dsl` package with computation expressions and async helpers for Next.js App Router
- added DSL & Computation Expressions documentation page
- updated release workflow to publish `NextFs.Dsl` alongside `NextFs`

## 1.0.0 - 2026-03-24

### Eliminated manual nextfs.entries.json

- added `[<NextFs.NextFsEntry>]` and `[<NextFs.NextFsStaticExport>]` F# attribute types so entry modules can declare their own wrapper shape directly in source
- added `tools/nextfs-scan.mjs` that reads a `.fsproj`, scans `[<NextFsEntry>]`-annotated F# files, and writes `nextfs.entries.json` automatically — no handwritten JSON
- annotated all 16 starter entry modules with `[<NextFs.NextFsEntry>]` attributes covering `Default`, `Named`, `Directive`, `ExportAll`, and `StaticExport` scenarios
- added `scan` npm script and wired it into `sync:app` and `watch:fable` so the manifest stays current during development
- added 11 automated tests for the scanner in `tests/nextfs-scan.test.mjs`

### CI and E2E coverage

- added a "scan + manifest diff" CI step that regenerates `nextfs.entries.json` from F# attributes and fails the build if the committed file is out of date
- added an E2E smoke step: `next start` in the background, `curl /` and `curl /api/posts` must return HTTP 200

### Repository formalities

- added `CODE_OF_CONDUCT.md`
- added `ARCHITECTURE.md` with an ASCII pipeline diagram (F# → Fable → nextfs-scan → nextfs-entry → Next.js)
- added "Zero to running in 5 minutes" quickstart to `CONTRIBUTING.md`
- improved NuGet package tags to include `app-router`, `nextjs-app-router`, `fable-binding`, `binding`, `dotnet`
- added doc comment to `NavigationClient.unstableIsUnrecognizedActionError` explaining its experimental status

### Stability notes for 1.0

The following APIs are **stable** and covered by compile-smoke and the E2E starter build:

- `Link`, `Image`, `Script`, `Form`, `Head` component builders
- `NavigationClient` hooks (`useRouter`, `usePathname`, `useSearchParams`, `useParams`, `useSelectedLayoutSegment*`, `useServerInsertedHTML`)
- `Navigation` helpers (`redirect`, `notFound`, `forbidden`, `unauthorized`, `unstableRethrow`)
- `Server` helpers (`headers`, `cookies`, `draftMode`, `connection`, `after`, `userAgent`)
- `ServerFetch` and `ServerFetchInit` typed `fetch()` extensions
- `Cache` invalidation APIs (`cacheTag`, `cacheTags`, `cacheLife*`, `revalidatePath`, `revalidateTag*`, `refresh`, `noStore`, `updateTag`)
- `Directive` inline directives (`useServer`, `useCache`, `useCachePrivate`, `useCacheRemote`)
- `Metadata`, `Viewport`, `MetadataOpenGraph`, `MetadataTwitter`, `MetadataRoute.*`, `ImageMetadata`, `ImageResponse`
- `ServerRequest`, `ServerResponse`, `ResponseInit`, `RouteHandlerContext`
- `Font.local` and the generated `GoogleFont.*` catalog
- `ProxyConfig`, `ProxyMatcher`, `RouteHas`, `NextFetchEvent`
- `CookieOptions`, `ErrorBoundaryProps`, `TemplateProps`, `DefaultProps`
- `RouteRuntime`, `PreferredRegion`, `GenerateSitemapsEntry`, `Revalidate`
- `[<NextFsEntry>]` / `[<NextFsStaticExport>]` attributes and `nextfs-scan.mjs`

The following APIs are **experimental** — they depend on Next.js `experimental` flags or `unstable_*` upstream functions and may change without a NextFs major bump if Next.js changes or removes them:

- `Navigation.forbidden()` and `Navigation.unauthorized()` — require `experimental.authInterrupts: true`
- `ProxyConfig` / middleware config — `next.config.mjs` proxy support is still marked experimental in some Next.js builds
- `NavigationClient.unstableIsUnrecognizedActionError` — wraps `next/navigation unstable_isUnrecognizedActionError`
- `Directive.useCachePrivate()` and `Directive.useCacheRemote()` — require `experimental.dynamicIO` or equivalent flags

## 0.9.0 - 2026-03-21

- fixed the starter and the library layout so a real `Fable -> wrappers -> next build` pipeline now succeeds on Next.js 16
- split `NextFs` into core, component, server, and client files so client entries no longer pull server-only imports and server entries no longer pull client-only hooks
- moved browser-only App Router hooks to `NavigationClient`, `LinkClient`, and `WebVitals` while keeping server-safe navigation helpers under `Navigation`
- changed the generated `GoogleFont` catalog to chunked files, which avoids the Fable/FCS stack overflow caused by a single monolithic font binding file
- expanded the wrapper generator with stale-file pruning, duplicate-output validation, and fallback resolution from dotted Fable paths to nested `.fable/App/...` output paths
- aligned the starter manifest and wrappers with real Fable output paths, added `package-lock.json`, and wired the starter example into CI with a real `next build`
- updated the documentation to reflect the working starter pipeline, client/server binding split, and `next/font` literal-object requirement

## 0.8.0 - 2026-03-21

- added typed Next.js server `fetch()` bindings through `ServerFetch`, `ServerFetchInit`, `NextFetchOptions`, `ServerFetchResponse`, `ServerFetchCache`, and `Revalidate`
- added `GenerateSitemapsEntry` helpers for multi-sitemap flows and a new compile-smoke file covering server fetch and sitemap generation patterns
- expanded the starter route handler to export route segment config and demonstrate typed `fetch(..., { next: { revalidate, tags } })`
- expanded wrapper-generator regression coverage for route-handler config exports
- added dedicated documentation for server fetch and route segment config flows

## 0.7.0 - 2026-03-21

- added instrumentation types for root `instrumentation.js` and `instrumentation-client.js` flows
- expanded the starter example with F#-driven server and client instrumentation entries plus generated root wrappers
- added compile-smoke coverage for instrumentation export shapes
- added `docs/instrumentation.md` and linked instrumentation flows from the wrapper and starter docs
- expanded wrapper-generator tests for root instrumentation entries

## 0.6.0 - 2026-03-21

- added `ServerRequest` constructors plus `NextRequestInit` and `NextConfig` builders for lower-level request interop
- added `ServerResponse.create*` helpers and richer `NextRequest` / `NextResponse` body APIs
- added `Navigation.useServerInsertedHTML`, `Navigation.useSelectedLayoutSegmentFor`, `Navigation.useSelectedLayoutSegmentsFor`, and `Navigation.unstableIsUnrecognizedActionError`
- added `Image.getImageProps()` and a dedicated compile-smoke file for request/response and client/server helper flows
- expanded wrapper-generator coverage for the remaining App Router special-file wrapper shapes
- added `docs/server-client-patterns.md` and linked it from the main repository docs

## 0.5.0 - 2026-03-21

- added helper types for App Router special files: `ErrorBoundaryProps`, `ErrorWithDigest`, `TemplateProps`, and `DefaultProps<'T>`
- added compile-smoke coverage for `error.js`, `global-error.js`, `loading.js`, `not-found.js`, `forbidden.js`, `unauthorized.js`, `template.js`, and `default.js`
- expanded the starter example with F#-driven special-file entries and `next.config.mjs` flags for `authInterrupts` and `globalNotFound`
- added dedicated documentation for special-file conventions in `docs/special-files.md`
- added wrapper-generator tests for special-file wrapper shapes

## 0.4.0 - 2026-03-21

- added `next/font/local` bindings plus a generated `GoogleFont` catalog sourced from official Next.js type definitions
- added font option builders, local font source/declaration helpers, and typed font results
- added `proxy.js` helpers: `ProxyConfig`, `ProxyMatcher`, `RouteHas`, and `NextFetchEvent`
- expanded request/response cookie support with `CookieOptions`, richer cookie shapes, and proxy-style mutation flows
- expanded compile-smoke coverage for fonts, cookies, and proxy behavior
- updated the starter example to include F#-driven `next/font`, cookie-aware route handling, and a root `proxy.js` entry
- added wrapper-generator regression coverage for root-level `.fable` re-export paths

## 0.3.0 - 2026-03-21

- added metadata, viewport, robots, sitemap, manifest, and image-metadata builders
- added `ImageResponse` bindings for Open Graph, icon, and metadata image generation flows
- added `useLinkStatus`, `useReportWebVitals`, `after`, `userAgent`, `forbidden`, `unauthorized`, and `unstableRethrow`
- expanded compile-smoke coverage for metadata files, special route files, and advanced App Router helpers
- updated the starter example to export the root layout, metadata, and viewport from F#

## 0.2.0 - 2026-03-21

- added `next/cache` bindings and inline cache directives
- expanded the smoke sample to cover cache invalidation flows
- added automated Node tests for the wrapper generator and wired them into CI
- added `examples/nextfs-starter` as a minimal App Router starter
- expanded repository documentation with quickstart, API reference, wrapper guidance, and package limitations

## 0.1.0 - 2026-03-21

- initial public release with App Router component, navigation, and server bindings
- added wrapper generation for file-level `'use client'` and `'use server'`
- published the package through GitHub Actions and NuGet Trusted Publishing
