# Changelog

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
