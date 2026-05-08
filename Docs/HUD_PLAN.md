# Last Stand HUD Plan

## Purpose
The Last Stand HUD layer displays the runtime statistics needed for Coursework002 evidence while keeping the JU TPS default UI active as the base gameplay interface.

## Why HUD Is Separate From StatsManager
`LastStandStatsManager` owns gameplay statistics. `LastStandHudController` only presents those values on screen. This keeps scoring, wave state, and survival time independent from UI layout and makes later HUD polish safer.

## HUD Elements Implemented
- Current wave / total waves.
- Enemies alive / total enemies this wave.
- Kills.
- Score.
- Survival time.
- FPS.
- Player health when readable.
- Current objective text.

TextMeshPro is not currently available in the project manifest, so this pass uses `UnityEngine.UI.Text` from Unity UI. This avoids adding another package during a scene/UI integration task.

## FpsCounter Summary
`Assets/_LastStand/Scripts/UI/FpsCounter.cs` calculates an approximate FPS value at a configurable interval using unscaled delta time. It has no UI references and can be reused by any future HUD presenter.

## PlayerHealthReader Summary
`Assets/_LastStand/Scripts/UI/PlayerHealthReader.cs` reads `Player_JUTPS` health through reflection without compile-time JU TPS references. It searches likely health fields/properties on player components and children, reads numeric values safely, and displays an unavailable state if no compatible value is found.

## LastStandHudController Summary
`Assets/_LastStand/Scripts/UI/LastStandHudController.cs` reads from `LastStandStatsManager`, `WaveManager`, `FpsCounter`, and `PlayerHealthReader`, then writes formatted values to the HUD text objects. Missing references are handled with safe placeholder values rather than runtime exceptions.

## Task 14 Game Flow Objective Integration
`LastStandHudController` now optionally reads `GameFlowManager.CurrentObjectiveText`. This allows the objective line to change from preparation, to survival, to `Reach extraction`, and finally `Extraction complete` without putting game-flow logic inside the HUD.

## Task 15 Failure Objective Integration
The HUD objective also reflects `GameFlowManager`'s `Failed` state. Player-death debug validation confirmed the objective text can display `Objective: You died`.

## Task 17 End Screen Integration
The HUD is now complemented by a simple end screen layer. `EndScreenController` displays a centered panel for `Victory` and `Failed` states while the normal HUD remains the live gameplay stat layer.

## Scene UI Setup
`LS_Arena_01` now contains:
- `_UISetup/LastStandHUD`
- `_UISetup/LastStandHUD/LastStandHUD_Canvas`
- `_UISetup/LastStandHUD/LastStandHUD_Controller`
- `_UISetup/LastStandHUD/LastStandHUD_Canvas/EndScreen`

The canvas is screen-space overlay with a `CanvasScaler` set to scale with a 1920 x 1080 reference resolution. The stat block is anchored to the top-left, and objective text is placed near the upper center. `UI_JUTPS_Default` remains active underneath this project-owned HUD layer.

## Runtime Validation Result
Validation temporarily enabled `WaveManager.autoStartOnPlay` and then restored it to false before saving. During Play Mode, live UGUI text components confirmed:
- `Wave: 1 / 5`
- `Enemies: 2 / 4`
- `FPS: 70`
- `Health: 400 / 400`
- Destroy-fallback defeat validation updated HUD text to `Kills: 1` and `Score: 100`.

The MCP screenshot returned the scene view without clearly compositing the overlay, but live Canvas/Text component state confirmed the HUD values were active and updating. Unity Console reported 0 errors and 0 warnings after final validation.

## Health Reading Result
`PlayerHealthReader` successfully read the JU TPS player health during validation and displayed `Health: 400 / 400`.

## Deliberately Not Implemented
- Pickups.
- Enemy health/ammo drops.
- Extraction activation.
- Main menu.
- Pause menu.
- Ranged enemy damage tuning.

## Future Pickup/Drop Design
- Do not use fixed pickup points.
- Health/ammo should drop from killed enemies.
- Later implementation should inspect or reference the JU TPS AI Attack Demo health/ammo pickup and drop setup.

## Coursework002 Evidence Supported
- UI/statistics: visible HUD for wave, enemies, kills, score, survival time, FPS, health, and objective text.
- Game mechanics: players can observe combat progress, enemy pressure, and scoring feedback.
- C# code quality: separates stats, health reading, FPS tracking, and HUD presentation.
- Video demo: on-screen values make final gameplay evidence easier to explain and verify.
