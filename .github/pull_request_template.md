## Summary

- what changed
- why it changed

## Validation

- [ ] `dotnet build NextFs.slnx -v minimal`
- [ ] `dotnet pack src/NextFs/NextFs.fsproj -c Release -o artifacts`
- [ ] `node tools/nextfs-entry.mjs samples/nextfs.entries.json`

## Checklist

- [ ] docs updated when public API changed
- [ ] smoke sample updated when relevant
- [ ] change is scoped and does not mix unrelated work
- [ ] commit messages follow repository convention

## Notes

- breaking changes:
- follow-up work:
- related issues:
