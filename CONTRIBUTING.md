# Contributing

Thanks for contributing to `NextFs`.

## Ground Rules

- Keep changes focused. Split unrelated work into separate PRs.
- Prefer small API increments over broad speculative abstractions.
- Match modern Next.js behavior first, then improve F# ergonomics around it.
- Update docs and smoke samples when public API changes.

## Before Opening An Issue

- Check the README and existing issues first.
- Reduce the problem to the smallest repro you can.
- When reporting a binding mismatch, include the relevant Next.js API and the expected JavaScript shape.

## Commit Convention

This repository uses a lightweight Conventional Commits style:

```text
<type>(<scope>): <summary>
```

Recommended `type` values:

- `feat`
- `fix`
- `docs`
- `refactor`
- `test`
- `ci`
- `chore`

Recommended `scope` values:

- `bindings`
- `navigation`
- `server`
- `directives`
- `tooling`
- `samples`
- `repo`
- `docs`

Examples:

```text
feat(server): add NextResponse rewrite helpers
fix(directives): reject invalid use-server wrapper exports
docs(repo): clarify wrapper generation workflow
```

Guidelines:

- Use the imperative mood.
- Keep the summary short.
- Describe the user-visible or repository-visible change, not the implementation detail.

## Optional Local Commit Template

The repository includes [.gitmessage.txt](/Users/roman/Documents/dev/Next_fs/.gitmessage.txt).

To enable it locally:

```bash
git config commit.template .gitmessage.txt
```

## Local Tools

The repository pins `femto` as a local .NET tool in [.config/dotnet-tools.json](/Users/roman/Documents/dev/Next_fs/.config/dotnet-tools.json).

Restore local tools before running validation commands:

```bash
dotnet tool restore
```

If the generated `GoogleFont` catalog needs to be refreshed against a newer Next.js release, regenerate it from official type definitions with:

```bash
node tools/generate-google-font-bindings.mjs
```

## Pull Requests

Before opening a PR:

- run `dotnet tool restore`
- run `dotnet femto --validate src/NextFs/NextFs.fsproj`
- run `node --test tests/*.mjs`
- run `dotnet build NextFs.slnx -v minimal`
- run `dotnet pack src/NextFs/NextFs.fsproj -c Release -o artifacts`
- run `node tools/nextfs-entry.mjs samples/nextfs.entries.json`
- run `node tools/nextfs-entry.mjs examples/nextfs-starter/nextfs.entries.json`

PRs should explain:

- what changed
- why it changed
- how it was validated
- whether docs or samples were updated

## API Changes

If a PR changes public bindings:

- update `README.md` when the change affects how the library is used
- update `samples/NextFs.Smoke`
- update `examples/nextfs-starter` when the change affects the intended real-project shape
- call out any intentional mismatch or limitation compared with native Next.js behavior
