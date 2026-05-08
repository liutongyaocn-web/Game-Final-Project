# Game Flow and Extraction Plan

## Purpose
This task adds the foundation for Last Stand's final objective: after the final wave is cleared, extraction unlocks, and the player wins by entering the extraction trigger.

## Final Game Flow
- `Idle`: the run has not started.
- `Running`: waves are active and the player must survive.
- `ExtractionUnlocked`: all waves are complete and the player should reach extraction.
- `Victory`: the player entered the unlocked extraction objective.
- `Failed`: the player has died and the run is no longer completable.

## GameFlowManager Script Summary
`Assets/_LastStand/Scripts/GameFlow/GameFlowManager.cs` owns high-level run state. It listens for `WaveManager.FinalWaveCompleted`, unlocks extraction, records victory, and exposes `CurrentObjectiveText` for the HUD.

`GameFlowManager` deliberately does not start waves automatically. Wave start control stays with `WaveManager` and future menu/game-start logic.

## ExtractionObjective Script Summary
`Assets/_LastStand/Scripts/GameFlow/ExtractionObjective.cs` is attached to the scene extraction point. It owns the trigger interaction, ignores entry while locked, accepts `Player_JUTPS` by object name or `Player` tag, and calls `GameFlowManager.CompleteExtraction()` when completed.

## WaveManager Final-Wave Completion Integration
`WaveManager` now exposes:
- `FinalWaveCompleted`
- `HasCompletedAllWaves`

When the final configured wave completes, `WaveManager` sets `HasCompletedAllWaves` and raises the event. `GameFlowManager` handles extraction unlock instead of putting extraction logic inside the wave system.

## HUD Objective Integration
`LastStandHudController` now optionally reads `GameFlowManager.CurrentObjectiveText`. If no `GameFlowManager` is available, the previous HUD fallback objective logic remains in place.

## Task 17 End-State UI Integration
Victory and Failed states now display a simple end-state panel through `EndScreenController`. The end screen reads `GameFlowManager.CurrentState`, shows either `Extraction Complete` or `You Died`, and provides a `Press R to Restart` prompt.

## Task 15 Player Death Integration
`GameFlowManager` now supports the `Failed` state through `PlayerDeathMonitor`. When player health reaches zero, or when the debug validation method is used, `FailRun()` stops the stats timer and exposes `You died` through `CurrentObjectiveText`.

Extraction unlock and extraction completion now ignore requests after the run has failed.

## Scene Setup
`LS_Arena_01` now contains:
- `_Systems/GameFlowSystem` with `GameFlowManager`
- `_ExtractionObjective/Extraction_Point` with `ExtractionObjective`
- `Extraction_Point` `BoxCollider` configured as a trigger with approximate size `4 x 3 x 4`
- HUD controller reference to `GameFlowManager`

`GameFlowManager` references the scene `WaveManager`, `LastStandStatsManager`, and `ExtractionObjective`.

## Runtime Validation Result
Full five-wave completion was not required for this task. Instead, debug validation toggles were used temporarily during Play Mode and restored to false before saving:
- Debug extraction unlock set `GameFlowManager` to `ExtractionUnlocked`.
- `ExtractionObjective.IsUnlocked` became true.
- HUD objective text changed to `Objective: Reach extraction`.
- Debug extraction completion set `GameFlowManager` to `Victory`.
- `ExtractionObjective.IsCompleted` became true.
- HUD objective text changed to `Objective: Extraction complete`.
- Unity Console reported 0 errors and 0 warnings after final validation.

## Deliberately Not Implemented
- Main menu.
- Pause menu.
- Pickups.
- Enemy health/ammo drops.
- Extraction visual polish.

## Future Pickup/Drop Design
- Do not use fixed pickup points.
- Health/ammo should drop from killed enemies.
- Later implementation should inspect or reference the JU TPS AI Attack Demo health/ammo pickup and drop setup.

## Known Limitations
- Full Wave 5 clear into extraction should be manually validated later.
- Extraction marker visual polish is still needed.
- Real enemy-damage player death should be manually rechecked during a full combat pass.

## Coursework002 Evidence Supported
- Game mechanics: player has a clear final extraction objective and victory state.
- Level progression: completing the final wave unlocks extraction.
- UI/statistics: HUD objective text reflects game flow.
- C# code quality: wave logic, game flow state, extraction trigger, and HUD presentation are separated.
