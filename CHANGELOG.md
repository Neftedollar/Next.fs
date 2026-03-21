# Changelog

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
