# Changelog

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
