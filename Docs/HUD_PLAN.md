# Last Stand HUD Plan

## Purpose
The Last Stand HUD layer displays the runtime statistics needed for Coursework002 evidence while keeping the JU TPS default UI active as the base gameplay interface.

## Why HUD Is Separate From StatsManager
`LastStandStatsManager` owns gameplay statistics. `LastStandHudController` only presents those values on screen. This keeps scoring, wave state, and survival time independent from UI layout and makes later HUD polish safer.

## HUD Elements Implemented
- Current wave / total waves.
- Enemies remaining / total enemies this wave.
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

## Task 22B Enemy Count Semantics
The enemy count label now displays wave progress as `Enemies Remaining: X / Total` instead of alive enemies only.

Meaning:
- `Total` is the total enemy count for the current wave.
- `Remaining` is the number of enemies in the current wave that have not yet been defeated.
- Remaining includes both currently alive enemies and enemies still waiting to spawn.

This is clearer for players than the old `Enemies: alive / total` display because it answers the practical question: how many enemies are left before the current wave can end?

## Task 24 HUD Visual Polish
The HUD received a light visual polish pass without changing `LastStandHudController` logic.

Changes:
- Added a translucent dark stats panel behind the left HUD text.
- Added a translucent dark objective banner behind the objective text.
- Re-parented the existing text objects under those panels while preserving controller references.
- Made Wave and Enemies Remaining the most prominent left-side values.
- Used subtle accent colours for enemies remaining and health.
- Made FPS smaller and less visually dominant.

This keeps the HUD readable for gameplay and clearer for video evidence while leaving JU TPS default UI active.

## Task 24R Compact HUD Fix
The Task 24 HUD panels were reduced after playtest feedback because the first polish pass blocked too much of the view.

Compact layout:
- Stats panel reduced to roughly `360 x 218`.
- Objective banner reduced to roughly `560 x 46`.
- Core stat fonts reduced into the `19-22` range.
- FPS reduced to font size `15`.
- Objective text reduced to font size `24`.
- Background opacity was lowered so the HUD remains readable but less visually heavy.

No HUD controller logic was changed. The existing `LastStandHudController` references remain intact.

Console investigation found the reported red stack trace came from Unity Editor Inspector/UIElements internals, not `_LastStand` gameplay scripts or old `UnityEngine.Input` usage.

## Task 24S Minimal HUD Conversion
The HUD was reduced again after further playtest feedback.

Minimal layout:
- Stats panel: roughly `286 x 152`, top-left.
- Objective banner: roughly `460 x 32`, top-centre.
- Panel opacity: low alpha around `0.22-0.24`.
- Main stat fonts: `15-16`.
- Secondary stat fonts: `14`.
- FPS font: `11`.
- Objective font: `17`.

The large visual treatment from Task 24 was intentionally dialed back. The HUD now prioritizes low obstruction over presentation weight while preserving the same displayed data and `LastStandHudController` references.

## Task 24U Full HD Compact Gameplay Layout
After testing in a `1920 x 1080` Game view, the HUD was reworked from separate vertical stat labels into combined compact rows.

Visible stat rows:
- `Wave X/Y    Enemies A/B`
- `HP C/M    Kills K`
- `Score S    Time MM:SS`
- `FPS: N`

Layout:
- Stats panel: about `340 x 105`, top-left.
- Objective panel: about `480 x 30`, top-centre.
- Main stats font: `14-15`.
- FPS font: `11`.
- Objective font: `16`.

This keeps all previous data visible while making the HUD much less distracting for Full HD recording and normal play.

## Task 24V No Background Panels
The compact HUD no longer shows dark translucent rectangular backing panels.

Implementation:
- `HUD_StatsPanel` Image alpha set to `0`.
- `HUD_ObjectivePanel` Image alpha set to `0`.
- Parent RectTransforms were kept for layout.
- Text objects and `LastStandHudController` references were left unchanged.

This further reduces screen obstruction while preserving the compact Full HD stat layout.

## Task 22A Main Menu Integration
The HUD remains the in-game stat/objective layer, while `LS_MainMenu.unity` now provides the pre-game start flow. The main menu loads `LS_Arena_01`, where the HUD appears during gameplay and Wave 1 auto-starts.

## Scene UI Setup
`LS_Arena_01` now contains:
- `_UISetup/LastStandHUD`
- `_UISetup/LastStandHUD/LastStandHUD_Canvas`
- `_UISetup/LastStandHUD/LastStandHUD_Controller`
- `_UISetup/LastStandHUD/LastStandHUD_Canvas/HUD_StatsPanel`
- `_UISetup/LastStandHUD/LastStandHUD_Canvas/HUD_ObjectivePanel`
- `_UISetup/LastStandHUD/LastStandHUD_Canvas/EndScreen`

The canvas is screen-space overlay with a `CanvasScaler` set to scale with a 1920 x 1080 reference resolution. The stat block is anchored to the top-left inside a semi-transparent dark panel, and objective text is placed near the upper center inside a matching dark banner. `UI_JUTPS_Default` remains active underneath this project-owned HUD layer.

## Runtime Validation Result
Validation temporarily enabled `WaveManager.autoStartOnPlay` and then restored it to false before saving. During Play Mode, live UGUI text components confirmed:
- `Wave: 1 / 5`
- `Enemies: 2 / 4`
- `FPS: 70`
- `Health: 400 / 400`
- Destroy-fallback defeat validation updated HUD text to `Kills: 1` and `Score: 100`.

The MCP screenshot returned the scene view without clearly compositing the overlay, but live Canvas/Text component state confirmed the HUD values were active and updating. Unity Console reported 0 errors and 0 warnings after final validation.

Task 22B validation confirmed the revised enemy text:
- Initial Wave 1 display: `Enemies Remaining: 3 / 3`.
- After one runtime defeat/removal proxy: `Enemies Remaining: 2 / 3`.
- Unity Console reported 0 red errors.

## Health Reading Result
`PlayerHealthReader` successfully read the JU TPS player health during validation and displayed `Health: 400 / 400`.

## Deliberately Not Implemented
- Pickups.
- Enemy health/ammo drops.
- Extraction activation.
- Pause menu.
- Ranged enemy damage tuning.

## Future Pickup/Drop Design
- Do not use fixed pickup points.
- Health/ammo should drop from killed enemies.
- Later implementation should inspect or reference the JU TPS AI Attack Demo health/ammo pickup and drop setup.

## Coursework002 Evidence Supported
- UI/statistics: visible HUD for wave, enemies remaining, kills, score, survival time, FPS, health, and objective text.
- Game mechanics: players can observe combat progress, enemy pressure, and scoring feedback.
- C# code quality: separates stats, health reading, FPS tracking, and HUD presentation.
- Video demo: on-screen values make final gameplay evidence easier to explain and verify.
