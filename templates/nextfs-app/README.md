# NextFsApp

A Next.js App Router application written in F# with [NextFs](https://github.com/Neftedollar/Next.fs) and [NextFs.Dsl](https://www.nuget.org/packages/NextFs.Dsl).

## Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) 8.0+
- [Node.js](https://nodejs.org/) 18+
- [Fable](https://fable.io/) (`dotnet tool install fable`)

## Getting Started

```bash
npm install
npm run sync:app   # compile F# → scan entries → generate wrappers
npm run dev        # start Next.js dev server
```

## Development

Run two terminals for iterative development:

```bash
# Terminal 1: watch F# changes and regenerate wrappers
npm run watch:fable

# Terminal 2: Next.js dev server
npm run dev
```

## Project Structure

- `src/` — F# source files (edit these)
- `.fable/` — compiled Fable output (generated, do not edit)
- `app/` — Next.js entry wrappers (generated, do not edit)
- `tools/` — NextFs scanner and wrapper generator

## Build

```bash
npm run sync:app
npm run build
```

## Learn More

- [NextFs Documentation](https://neftedollar.github.io/Next.fs/)
- [Next.js Documentation](https://nextjs.org/docs)
- [Fable Documentation](https://fable.io/docs/)
