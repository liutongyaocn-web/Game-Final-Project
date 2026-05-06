# Test Log

| Date/time | Area | Method | Result | Notes |
|---|---|---|---|---|
| 2026-05-06 17:57 +01:00 | Initial project inspection | Unity MCP editor state and Console check after asset refresh | Pass | Unity editor idle, not compiling, Console returned 0 errors/warnings. |
| 2026-05-06 17:57 +01:00 | Repository inspection | `git status`, project folder inspection | Pass with notes | Existing imported asset/project-setting changes are uncommitted; no custom gameplay files existed before setup. |
| 2026-05-06 18:02 +01:00 | Scaffold baseline | Documentation and Git staging review | Pass with notes | No gameplay test yet and no major scene test yet because this task only prepares the scaffold/docs baseline. |
| 2026-05-06 18:12 +01:00 | Unity validation for Task 0C | Unity MCP asset refresh and Console read | Pass | Console returned 0 errors and 0 warnings. No gameplay test yet. No scene test yet. |
| 2026-05-06 18:16 +01:00 | Task 0D documentation guardrails | Unity MCP asset refresh and Console read | Pass | Console returned 0 errors and 0 warnings. No gameplay test yet. No scene test yet. |
| 2026-05-06 18:16 +01:00 | Task 1 scene audit | File inspection, Unity MCP hierarchy read, and Console read | Pass with notes | Console showed 0 red errors and 1 MCP transport warning. No gameplay test yet. No scene test yet. Demo scene opened additively for inspection only; do not save third-party scene changes. |

## Manual Test Checklist To Build Up
- Launch Last Stand scene.
- Move, aim, shoot, reload, switch weapons.
- Damage player and enemies.
- Complete waves 1-5.
- Verify score, kills, wave number, timer, ammo, health, FPS.
- Verify pickups.
- Verify pause/restart.
- Verify win/extraction and lose/death flows.
