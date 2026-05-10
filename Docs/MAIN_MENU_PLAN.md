# Main Menu Plan

## Purpose
The Last Stand main menu gives the project a complete start flow before entering `LS_Arena_01`. It presents the game title, objective, controls, and a safe quit option without modifying JU TPS demo menu assets.

## JU TPS Demo Menu Reference
The JU TPS demo menu was inspected as a visual/style reference only:

- Source scene: `Assets/Julhiecio TPS Controller/Demos/Demo Scenes/Menu/Menu.unity`
- Referenced visual assets observed: `Menu Background.png`, `TPS1884.png`, and `Russo_One.ttf`
- UI structure observed: standard Unity UI text/button hierarchy with a dark background, large title text, and rectangular demo selector buttons
- Reference title observed: `JU TPS 3 DEMOS`

No JU TPS demo scene, prefab, script, texture, or font asset was modified.

## Last Stand Menu Structure
Created scene:

- `Assets/_LastStand/Scenes/LS_MainMenu.unity`

Scene hierarchy:

- `_MenuSystems`
- `_MenuUI`
- `_MenuCamera`
- `_MenuLighting`
- `_MenuVisuals`

The menu uses a screen-space overlay Canvas with a dark background, a large `Last Stand` title, the subtitle `Survive five waves and reach extraction.`, and rectangular buttons inspired by the JU TPS demo menu presentation.

## MainMenuController Script Summary
Created script:

- `Assets/_LastStand/Scripts/UI/MainMenuController.cs`

Responsibilities:

- Load `LS_Arena_01` when Start Game is pressed.
- Toggle the controls panel.
- Close the controls panel.
- Quit safely: log/no-op in the Unity Editor and `Application.Quit()` in builds.
- Reset `Time.timeScale` before scene load.

The controller uses Unity UI button callbacks and does not use old input polling.

## Controls Panel Content
The controls panel includes:

- WASD: Move
- Mouse: Look / Aim
- Left Mouse: Shoot / Attack
- Right Mouse: Aim
- R: Reload / Restart after end screen
- Shift: Run
- Ctrl: Roll
- C: Crouch
- Z: Prone
- Esc: Pause / JU TPS menu if available

## Scene Loading Setup
`MainMenuController.gameSceneName` is set to `LS_Arena_01`. The Start Game flow is intended to load the final gameplay scene, where `WaveManager.autoStartOnPlay` is already enabled so Wave 1 begins automatically.

## Build Settings Note
`ProjectSettings/EditorBuildSettings.asset` was intentionally updated to include:

1. `Assets/_LastStand/Scenes/LS_MainMenu.unity`
2. `Assets/_LastStand/Scenes/LS_Arena_01.unity`

This is required so the menu can load the gameplay scene by name in a built player. Third-party demo scenes were not modified.

## Runtime Validation Result
Unity refresh/compile validation reported 0 red Console errors after the menu script and scene were created.

Validated through Unity MCP:

- `LS_MainMenu.unity` exists and opens.
- `MainMenuController` is present and has serialized references to the Start, Controls, Close Controls, and Quit buttons.
- The controls panel exists and is inactive by default.
- `LS_Arena_01` loads successfully and auto-starts Wave 1 when opened in Play Mode.
- Wave 1 entered spawning state with 5 total waves configured and no red Console errors.

MCP could not physically click UGUI buttons in Game view, and arbitrary runtime C# execution hit a Windows filename/extension tooling limit. Therefore, the final physical Start Game and Controls button click should be manually confirmed in the Unity Game view.

## Task 22A-Fix White Screen Repair
After the first menu pass, opening `LS_MainMenu` in Play Mode showed a white Game view instead of the menu.

Cause found:

- `MainMenuCanvas` had been saved as a World Space Canvas with a small `100 x 100` RectTransform.
- The full-screen `Background` Image was still pure white.
- Several `Text` components had empty serialized text values, so the UI offered no visible menu content.

Fix applied:

- Set `MainMenuCanvas` to Screen Space - Overlay.
- Set `CanvasScaler` to Scale With Screen Size at `1920 x 1080`, match `0.5`.
- Set the full-screen background to a dark readable color.
- Reapplied visible text values for `LAST STAND`, subtitle, buttons, and controls panel.
- Set button and controls panel colors to readable dark/accent tones.
- Kept the controls panel inactive by default.

Validation:

- Play Mode component inspection confirmed the Canvas is active, Screen Space - Overlay, and using the corrected scaler settings.
- `Background` Image is dark instead of white.
- `Title_Text` contains `LAST STAND` with light text.
- Console reported 0 red errors.
- Physical button clicks still need a quick manual Game view check because MCP cannot reliably click UGUI buttons.

## Task 22D Pause Menu Integration Fix
After adding `LS_MainMenu`, `LS_Arena_01` still had the JU TPS pause menu configured to return to the old demo scene name `Menu`. Because Build Settings now use `LS_MainMenu` instead of the JU TPS demo menu scene, the JU TPS pause Menu button could throw a missing-scene error.

The gameplay scene instance was updated so `_UISetup/UI_JUTPS_Default/Pause Screen` has `JU_UIPause.MainMenuScene = LS_MainMenu`. This preserves the Last Stand main menu start flow while keeping the JU TPS pause menu pointed at the correct project-owned menu scene.

## Task 22D-R Return Clickability Fix
`MainMenuController` now resets menu input state whenever `LS_MainMenu` loads. This protects the menu from inherited gameplay/pause state after returning from the JU TPS pause menu.

Reset behaviour:

- `Time.timeScale = 1`
- cursor unlocked
- cursor visible
- current EventSystem selected object cleared

No scene changes were needed for this fix.

## Deliberately Not Implemented
- Save system.
- Settings menu.
- Pause menu.
- Complex animated menu transitions.
- New gameplay systems.

## Coursework002 Evidence Supported
- UI evidence: project now has an initial main menu and visible controls.
- Game logic evidence: Start Game leads into the final Last Stand wave scene.
- Video/live demo evidence: the project can be introduced from a complete menu flow.
- Code quality evidence: menu scene and controller are project-owned and separate from JU TPS demo UI.
