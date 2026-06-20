# LetheChat-Dev — Agent guidance

Windows desktop LLM chat app (WinForms + WPF hybrid) built on the `LetheAISharp` library.

## Scope

- **In scope siblings**: `LetheChat-Dev` (this app) and `LetheAISharp` (the library it references).
- **Out of scope**: all other sibling projects (`DiscordAIBot`, `LetheChat` non-Dev, `ObsidianToolset`, etc.).
  - Exception: plugin projects such as `WaifuPlugin` matter only because they are copied into this app's `plugins/` folder at build time.

## Build

- The solution file is one level up: `../Lethe AI.sln`. There is no `.sln` inside this repo folder.
- Build everything (required because `LetheChat-Dev` references `../LetheAISharp/LetheAISharp.csproj`):
  ```powershell
  dotnet build "../Lethe AI.sln"
  ```
- Build just this project (LetheAISharp will still build transitively):
  ```powershell
  dotnet build LetheChat-Dev.csproj
  ```
- Target framework: `net10.0-windows10.0.17763.0`; `OutputType` is `WinExe`.
- `AllowUnsafeBlocks` is enabled.

## Run

- The executable is produced at `bin/Debug|Release/net10.0-windows10.0.17763.0/LetheChat-Dev.exe`.
- It is a Windows GUI app; run it directly or from Visual Studio. `dotnet run` is possible but will launch a WinForms app from the CLI.

## Tests / lint / typecheck

- **No test framework** is configured; there are no test projects in the solution.
- **No CI workflows**, pre-commit hooks, or dedicated lint tools. The only verification is `dotnet build` compiler output.
- Treat build warnings as the primary quality signal.

## Code conventions

- **JSON**: use **Newtonsoft.Json** everywhere (`JsonConvert`, `[JsonProperty]`). Do not introduce `System.Text.Json`.
- **Nullable**: enabled (`<Nullable>enable</Nullable>`).
- **Implicit usings**: enabled.
- **Namespace/folder matching**: disabled. IDE0130 is suppressed in `.editorconfig` and `GlobalSuppressions.cs`.
- **Naming style rules**: IDE1006 is suppressed in `GlobalSuppressions.cs`; do not "fix" existing naming that violates it.
- No emojis in source or docs (follow the library convention).

## Architecture

- **Entry point**: `Program.cs` → `ApplicationConfiguration.Initialize()` → `DataFiles.LoadDB()` → `MainForm`.
- **Global state**: `Program.Settings` (`LetheChatSettings`), `Program.BigForm` (the `MainForm` instance), `Program.LlamaCppProcess`, `Program.Audio`.
- **Library bridge**: everything LLM-related goes through `LetheAISharp.LLM.LLMEngine` (referenced from the sibling library). See `LetheAISharp/AGENTS.md` for library-level guidance.
- **Data loading**: `DataFiles.LoadDB()` reads JSON assets from `data/` subfolders (`chars`, `instruct`, `params`, `worlds`, `sysprompts`, `websites`, `pointsystems`) at runtime.
- **Settings**: `settings.json` is read/written in the working directory at startup.
- **Plugin DLLs**: loaded at runtime from `plugins/`. The shared `../Plugin.targets` auto-copies plugin project outputs (`WaifuPlugin`, etc.) into `LetheChat-Dev/bin/$(Configuration)/net10.0-windows10.0.17763.0/plugins/` after build.

## Project layout

- Root-level `.cs` files (`Program.cs`, `MainForm.cs`) plus `src/`:
  - `src/forms/` — WinForms editors and dialogs.
  - `src/controls/` — custom WinForms controls.
  - `src/AgentPlugins/`, `src/plugins/` — app-side plugin/agent implementations.
  - `src/character/`, `src/files/`, `src/helpers/`, `src/security/`, `src/slash/`, `src/types/`, `src/web/` — domain modules.
  - `src/GBNF/` — grammar-related code.
- `data/` — runtime content (characters, instruction formats, sampler params, worlds, system prompts, etc.) copied to output via `CopyToOutputDirectory=PreserveNewest`.
- `mods/`, `plugins/` — runtime extension points, also copied to output.

## UI preference

- Prefer richer log UI: structured lists with log level and message, instead of plain text boxes for log output displays (preserved from `.github/copilot-instructions.md`).
