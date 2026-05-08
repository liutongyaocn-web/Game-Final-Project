# End Screen and Restart Plan

## Purpose
The end screen foundation gives Last Stand a clear conclusion state for both failure and victory. It sits on top of the existing HUD layer and reads `GameFlowManager` state rather than duplicating game-flow logic.

## End States Supported
- Failed: displays `You Died`.
- Victory: displays `Extraction Complete`.

## EndScreenController Script Summary
`Assets/_LastStand/Scripts/UI/EndScreenController.cs` is a small Unity UI controller. It:
- reads `GameFlowManager.CurrentState`
- reads `LastStandStatsManager` for kills, score, survival time, and wave count
- shows the end panel only when the game flow reaches `Failed` or `Victory`
- unlocks the cursor when the end screen is visible
- reloads the active scene when `R` is pressed
- optionally supports a Unity UI restart button if one is added later

The restart method uses `SceneManager.LoadScene(SceneManager.GetActiveScene().name)` and resets `Time.timeScale` to `1` before loading.

## UI Scene Setup
`LS_Arena_01` now includes an end screen under the Last Stand HUD canvas:

- `_UISetup/LastStandHUD/LastStandHUD_Canvas/EndScreen`
- `_UISetup/LastStandHUD/LastStandHUD_Canvas/EndScreen/EndScreen_Panel`
- `Title_Text`
- `Subtitle_Text`
- `Stats_Text`
- `Restart_Text`
- `EndScreen_Controller`

The panel is inactive by default. The visible restart prompt is text-only: `Press R to Restart`. A clickable button was not added in this task because the keyboard restart path is sufficient and avoids unnecessary UI/EventSystem setup.

## Restart Method
- Press `R` after the end panel is visible.
- The controller reloads the currently active scene.
- No Build Settings changes are required.

## Runtime Validation Result
Debug validation was performed in Play Mode:

- Failure path: `PlayerDeathMonitor.debugReportDeathOnStart` was temporarily enabled.
- The end panel appeared.
- Title text showed `You Died`.
- GameFlow/HUD objective remained in the failed state.
- Victory path: `GameFlowManager.debugCompleteExtractionOnStart` was temporarily enabled.
- The end panel appeared.
- Title text showed `Extraction Complete`.
- Unity Console reported 0 errors and 0 warnings.

The `R` restart path is implemented in code, but a full keypress/reload validation should be completed manually because MCP did not send keyboard input to the Game view during this task.

Before saving:
- `WaveManager.autoStartOnPlay = false`
- `SpawnDirector.debugSpawnOnStart = false`
- `PlayerDeathMonitor.debugReportDeathOnStart = false`
- `GameFlowManager` extraction debug toggles were false
- `EndScreen_Panel` was inactive
- no runtime enemies or pickups were saved

## Deliberately Not Implemented
- Main menu.
- Pause menu.
- Settings menu.
- Full score breakdown.
- Save system.
- Build Settings changes.

## Coursework002 Evidence Supported
- UI evidence: visible end-state panel for victory and failure.
- Game mechanics evidence: win/fail states are communicated clearly.
- Video demo evidence: gameplay can end with an obvious conclusion and restart prompt.
- C# code quality: `EndScreenController` stays separate from `GameFlowManager`, `WaveManager`, and HUD stat logic.
