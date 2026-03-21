# NextFs.Smoke

`NextFs.Smoke` is the repository's compile-smoke project.

It exists to verify that the current binding surface still compiles as expected after changes in:

- `src/NextFs/NextFs.fs`
- wrapper-generation rules
- starter-app conventions reflected back into the bindings

The files are intentionally split by feature area:

- `App.fs`: baseline component bindings
- `Advanced.fs`: extra navigation and helper coverage
- `DataFetching.fs`: server `fetch()`, sitemap, and route config coverage
- `Instrumentation.fs`: root instrumentation entry shapes
- `Metadata.fs`: metadata, viewport, sitemap, manifest, and image metadata coverage
- `Proxy.fs`: `proxy.js` and matcher coverage
- `RequestResponse.fs`: request/response constructors and helpers
- `Route.fs`: route-handler coverage
- `SpecialFiles.fs`: App Router special-file coverage

This project is not a runnable Next.js app. It is a fast regression net for the binding layer.
