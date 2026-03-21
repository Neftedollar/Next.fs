# .config

This folder contains repository-local configuration files.

Current contents:

- `dotnet-tools.json`: local .NET tool manifest for repo development

At the moment the manifest restores:

- `femto` for npm dependency metadata validation
- `fable` for local Fable compilation and starter-app workflows

Typical usage from the repository root:

```bash
dotnet tool restore
```

This folder is for contributor tooling, not for the published `NextFs` runtime surface.
