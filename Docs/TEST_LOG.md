# Test Log

| Date/time | Area | Method | Result | Notes |
|---|---|---|---|---|
| 2026-05-06 17:57 +01:00 | Initial project inspection | Unity MCP editor state and Console check after asset refresh | Pass | Unity editor idle, not compiling, Console returned 0 errors/warnings. |
| 2026-05-06 17:57 +01:00 | Repository inspection | `git status`, project folder inspection | Pass with notes | Existing imported asset/project-setting changes are uncommitted; no custom gameplay files existed before setup. |
| 2026-05-06 18:02 +01:00 | Scaffold baseline | Documentation and Git staging review | Pass with notes | No gameplay test yet and no major scene test yet because this task only prepares the scaffold/docs baseline. |
| 2026-05-06 18:12 +01:00 | Unity validation for Task 0C | Unity MCP asset refresh and Console read | Pass | Console returned 0 errors and 0 warnings. No gameplay test yet. No scene test yet. |
| 2026-05-06 18:16 +01:00 | Task 0D documentation guardrails | Unity MCP asset refresh and Console read | Pass | Console returned 0 errors and 0 warnings. No gameplay test yet. No scene test yet. |
| 2026-05-06 18:16 +01:00 | Task 1 scene audit | File inspection, Unity MCP hierarchy read, and Console read | Pass with notes | Console showed 0 red errors and 1 MCP transport warning. No gameplay test yet. No scene test yet. Demo scene opened additively for inspection only; do not save third-party scene changes. |
| 2026-05-06 19:51 +01:00 | Task 2 initial scene creation | Copied source scene, loaded `LS_Arena_01`, added hierarchy/markers, saved custom scene, refreshed Unity, and read Console | Pass with notes | Scene opened and saved successfully. Console showed 0 red errors and 3 MCP warnings. No gameplay test yet. No player/AI test yet. |
| 2026-05-06 20:00 +01:00 | Task 2.5 arena boundary refinement | Opened `LS_Arena_01`, confirmed marker layout, added boundary preview/planning notes, saved scene, refreshed Unity, and read Console | Pass with notes | Console showed 0 red errors and 1 MCP transport warning. No player test yet. No AI/NavMesh test yet. |

## Manual Test Checklist To Build Up
- Launch Last Stand scene.
- Move, aim, shoot, reload, switch weapons.
- Damage player and enemies.
- Complete waves 1-5.
- Verify score, kills, wave number, timer, ammo, health, FPS.
- Verify pickups.
- Verify pause/restart.
- Verify win/extraction and lose/death flows.
| 2026-05-06 20:23 +01:00 | Task 3 JU TPS integration | Instantiated JU TPS player, camera, and default UI into LS_Arena_01; disabled copied demo camera; entered Play Mode through Unity MCP | Needs final manual/MCP confirmation | First Play Mode pass exposed copied-scene BoxCollider negative-scale errors and a two-audio-listener warning while SampleScene was loaded additively. Negative scale overrides were normalised in the custom scene only. Manual WASD/mouse movement test is still needed after Unity MCP reconnection. |
| 2026-05-06 20:27 +01:00 | Task 3 Play Mode verification after collider cleanup | Refreshed Unity, entered Play Mode for approximately 6 seconds, exited Play Mode, and read Console errors/warnings | Pass with warning | Console returned 0 red errors. Repeated warning remained: There are 2 audio listeners in the scene, caused by unsaved additive SampleScene still being loaded alongside LS_Arena_01; SampleScene was not saved or staged. Manual WASD/mouse movement test still needed. |
| 2026-05-06 22:22 +01:00 | Task 3.5 JU TPS foundation validation | Confirmed only LS_Arena_01 loaded, checked scene objects, entered Play Mode for approximately 8 seconds, checked player transform, stopped Play Mode, and read Console | Pass with manual input notes | Only LS_Arena_01 was loaded. Player stayed near (-10, 0, -136) and did not fall through the map. CameraController_JUTPS, Player_JUTPS, and UI_JUTPS_Default were active. Copied Demo/Main Camera was inactive. Console returned 0 errors and 0 warnings, and the previous 2 audio listeners warning was gone. WASD, mouse look, aim, shoot, reload, run, roll, crouch, and prone still require hands-on input confirmation. |
| 2026-05-06 22:45 +01:00 | Task 3.6 user hands-on JU TPS input validation | User manually tested the JU TPS foundation in Unity with only LS_Arena_01 loaded | Pass with polish notes | Manual player spawn test passed: player stayed on the ground. Camera follow passed. Mouse look passed. JU TPS default UI was visible. Console had no red errors. The previous 2 Audio Listeners warning was resolved after closing SampleScene. Basic JU TPS player/camera/UI foundation is working. Known polish: camera feels slightly low, and the default JU TPS UI is functional but needs a Last Stand-specific HUD layer later. Enemy, NavMesh, wave, pickup, and custom HUD tests are not started yet. |
