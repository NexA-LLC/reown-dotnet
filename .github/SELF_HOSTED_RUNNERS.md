# NexA fork runner policy

Public self-hosted execution is prepared but off by default. The buildman6
Default runner group currently excludes public repositories. Enabling it
requires both public-repository runner access and the repository/organization
variable `NEXA_PUBLIC_SELF_HOSTED_RUNNER_ENABLED` set to `true`. This PR changes
neither setting. With the variable unset or false, eligible jobs keep their
original Ubuntu runner. The existing repository and trusted-event checks still
apply after opt-in; untrusted events never gain access to the private Mac.

The .NET-only package/tag release jobs and the source-only Unity-version sync
job may select buildman6 through `[self-hosted, macOS, ARM64, buildchan]` only
for `push`/`workflow_dispatch` in `NexA-LLC/reown-dotnet`. Other events retain the
existing Ubuntu fallback. Their triggers, publication/tag conditions, .NET
8.0.x version, commands, and artifact selections remain configured.

SDK, dotnet-script, CLI user state, and NuGet packages use job-temporary paths.
Git identity is checkout-local. Retired setup-dotnet v3 moves to the supported
Node20 v4 action without changing the .NET SDK version. No package was
published, tag pushed, version sync run, or manual workflow dispatched locally.

The Windows-only .NET unit/integration test matrix and `sonarcloud.yml` remain
on Windows. `unity-bulid-test.yml` retains the Linux Unity-builder job and its
Windows64/Android/WebGL target matrix: the game-ci Docker action and disk-space
cleanup require the existing Linux environment. The dependent Vercel deploy
job remains in that pipeline. The single Mac does not replace those OS targets.

Local validation covers actionlint, Bash syntax, unchanged tests/matrices/gates,
and whitespace. SDK compilation, signing/publication, and Unity execution on
the Mac remain unverified. Revert this PR to restore previous runner/tool paths.
