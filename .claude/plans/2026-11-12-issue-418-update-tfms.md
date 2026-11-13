# Issue #418 — Update TFMs

Issue: https://github.com/ardalis/GuardClauses/issues/418
Branch: `issue-418-update-tfms`

> Update target frameworks to only those currently supported by Microsoft.

## Context (as of 2026-11-12)

From the official .NET release metadata (`releases-index.json`):

| Channel | Type | Support phase | EOL |
|---------|------|---------------|-----|
| .NET 11 | STS | active (11.0.0 GA) | 2028-11-14 |
| .NET 10 | LTS | active | 2028-11-14 |
| .NET 9  | STS | **EOL** | 2026-11-10 |
| .NET 8  | LTS | **EOL** | 2026-11-10 |

Current state:
- Library: `net8.0;netstandard2.1;netstandard2.0`
- Tests: `net8.0`
- CI `build.yml`: SDK `8.x`; `publish.yml`: SDK `6.0.x` (which can't build net8.0 or later)

## Assumptions

- **`net8.0` → `net10.0;net11.0`.** Both are the only supported modern .NET runtimes.
- **Keep `netstandard2.0`.** It's the only way to serve .NET Framework 4.6.2–4.8.1, which Microsoft
  still supports. Removing it would drop real, supported consumers.
- **Drop `netstandard2.1`.** Every runtime that implements netstandard2.1 but not netstandard2.0-only
  (.NET Core 3.x, .NET 5–9, Xamarin, Mono) is out of Microsoft support. Any remaining consumers
  (for example Unity) still resolve the `netstandard2.0` asset, so nobody loses the package. The source
  has no `NETSTANDARD2_1`-specific code paths (`NETSTANDARD2_0_OR_GREATER` also covers 2.0).
- Keep `LangVersion` 11 unchanged. That's out of scope.
- No version bump/release notes change. Publishing is the maintainer's call. Noted in PR.

## Phase 1 — Library & test TFMs
- [x] `src/GuardClauses/GuardClauses.csproj`: `TargetFrameworks` → `net11.0;net10.0;netstandard2.0`
- [x] `test/GuardClauses.UnitTests/GuardClauses.UnitTests.csproj`: `TargetFramework` → `TargetFrameworks` `net11.0;net10.0`
      so the full suite runs against both supported runtimes.

## Phase 2 — CI
- [x] `.github/workflows/build.yml`: install SDKs `10.x` and `11.x`
- [x] `.github/workflows/publish.yml`: SDK `6.0.x` → `11.x` (needed to build net11.0)

## Phase 3 — Automated tests
- No new behavior, so no new test cases. Coverage comes from running the existing unit test suite
  on **both** net10.0 and net11.0 (multi-targeted test project). That also exercises the
  `NET5_0_OR_GREATER`/`NET6_0_OR_GREATER` code paths on each new TFM.
- [x] Confirm the packed nupkg has `lib/net10.0`, `lib/net11.0`, `lib/netstandard2.0` (no 2.1, no net8.0).

## Phase 4 — Independent review
- [x] A separate agent (fresh context) reviews the full diff against the issue: correctness, missed
      TFM references, CI breakage, security/perf/accessibility.
- [x] Address significant findings, or record why not.

## Phase 5 — Final verification
- [x] `dotnet build -c Release` is clean
- [x] `dotnet test -c Release` passes on all test TFMs
- [x] `dotnet format --verify-no-changes` reports no changes

## Phase 6 — PR
- [x] Push branch and open PR with `Fixes #418`

## Outcomes & notes

- **Test fix found during implementation:** two `GuardAgainstOutOfRangeForEnumerableDecimal` cases failed on net11.0.
  `ClassData` passed `double` range values (`0.1`) that xUnit converts to `decimal`. .NET 11 changed
  double→decimal conversion (`(decimal)0.1` is now `0.1000000000000000055511151231`), so `0.1m` fell below the
  range start. The library is correct. All range values in that file are now `decimal` literals.
- **Format:** `dotnet format --verify-no-changes` already fails on `main` (~980 whitespace violations across the repo).
  Reformatting the whole repo is out of scope. The files this PR touches pass a scoped `--include` check (pre-existing
  whitespace issues in the touched test file were fixed).
- **Review findings addressed:** added `setup-dotnet` (10.x/11.x) to `codeql-analysis.yml` so autobuild can build net11.0
  before runner images ship the .NET 11 SDK. Updated the stale netstandard2.1 doc comment. Made all decimal range values
  literals.
- **Review findings not changed (noted in PR):**
  - Dropping net8.0 means net5–net9 consumers fall back to `netstandard2.0`. That asset lacks the
    `ReadOnlySpan<char>` `Empty`/`WhiteSpace` overloads (`NET5_0_OR_GREATER`) and the `TryGetNonEnumeratedCount`
    fast path. Under NuGet guidance that's a breaking change and warrants a major version bump. The version is left at
    5.0.0 so merging doesn't auto-publish. The maintainer decides.
  - `net11.0` and `net10.0` currently compile identically. net11.0 is kept because the issue asks for all supported TFMs.
  - With a multi-targeted test project, the CI coverage summary picks one of the two cobertura files (last one wins).
    Fine for now.
  - `rohith/publish-nuget@v2` is archived. That's a separate concern.
