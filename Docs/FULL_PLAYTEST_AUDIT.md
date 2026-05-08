# Full Playtest Audit

## Purpose
Record a Codex/MCP smoke audit of the current Last Stand vertical slice so manual playtest time can focus on feel, full-flow completion, and visual polish rather than basic wiring checks.

## Static Scene/Config Audit Result
- Active scene: `Assets/_LastStand/Scenes/LS_Arena_01.unity`.
- Unity reported the scene as dirty before the audit began. The audit did not save or stage the scene.
- `_Systems/WaveSystem` exists and has `WaveManager`.
- `_Systems/SpawnSystem` exists and has `SpawnDirector`.
- `_Systems/StatsSystem` exists and has `LastStandStatsManager`.
- `_Systems/GameFlowSystem` exists and has `GameFlowManager` and `PlayerDeathMonitor`.
- `_SpawnPoints` exists and has `SpawnPointGroup`.
- `_UISetup/LastStandHUD` exists with `LastStandHudController` and `EndScreenController`.
- `Extraction_Point` exists with `ExtractionObjective` and a trigger `BoxCollider`.
- `WaveManager` references five `WaveDefinition` assets.
- `SpawnDirector` references `_SpawnPoints`, `_Systems/Spawned_Enemies`, and `Player_JUTPS`.
- `GameFlowManager` references `WaveManager`, `LastStandStatsManager`, and `ExtractionObjective`.
- HUD references `LastStandStatsManager`, `WaveManager`, and `GameFlowManager`.
- `EndScreen_Panel` is inactive by default after cleanup.

## Safe Default Settings
- `WaveManager.autoStartOnPlay = false`
- `SpawnDirector.debugSpawnOnStart = false`
- `PlayerDeathMonitor.debugReportDeathOnStart = false`
- `GameFlowManager.debugUnlockExtractionOnStart = false`
- `GameFlowManager.debugCompleteExtractionOnStart = false`

## Enemy Prefab Audit
The three project-owned enemy prefabs were checked for core Last Stand bridge components:

| Prefab | EnemyTargetBinder | EnemyLifecycleReporter | EnemyDeathDropper | Missing script search |
|---|---:|---:|---:|---:|
| `Enemy_FistMelee_JUTPS.prefab` | Present | Present | Present | None found by prefab text search |
| `Enemy_KnifeMelee_JUTPS.prefab` | Present | Present | Present | None found by prefab text search |
| `Enemy_Ranged_JUTPS.prefab` | Present | Present | Present | None found by prefab text search |

## Console Audit Result
- Unity refresh/compile was completed before runtime checks.
- Console returned 0 red errors and 0 warnings during the static audit.
- Console returned 0 red errors and 0 warnings after Wave 1 smoke testing.
- Console returned 0 red errors and 0 warnings after ranged enemy smoke testing.
- Console returned 0 red errors and 0 warnings after failed/victory debug end-state tests.
- Regular JU TPS inventory/SwitchID logs remain acceptable if they appear as normal logs rather than red errors.

## Wave/Spawn Smoke Test Result
MCP temporarily enabled `WaveManager.autoStartOnPlay` for Play Mode, then restored it to false.

Observed after approximately 35 seconds:
- `WaveManager.State = Spawning`
- `CurrentWaveNumber = 1`
- `TotalWaves = 5`
- `EnemiesSpawnedThisWave = 2`
- `TotalEnemiesToSpawnThisWave = 4`
- `AliveEnemyCount = 2`
- Wave 1 `maxAliveAtOnce` was respected.
- Runtime enemies were parented under `_Systems/Spawned_Enemies`.
- Spawned enemies had `EnemyTargetBinder`, `EnemyLifecycleReporter`, `EnemyDeathDropper`, and `SpawnedEnemyRuntimeInfo`.
- `EnemyTargetBinder` successfully assigned `Player_JUTPS` into the JU TPS AI target fields.
- `LastStandStatsManager` showed wave `1 / 5`, `2 / 4` enemies, and survival time increasing.
- `PlayerHealthReader` read `400 / 400`.
- `FpsCounter` updated during Play Mode.

## Stats/HUD Smoke Test Result
- Stats updated from idle values into active Wave 1 values.
- HUD backing components had valid references to stats, FPS, health, wave, and game flow systems.
- Health was readable as `400 / 400` during the automated smoke.
- FPS value was updating.
- MCP did not perform visual layout polish checks beyond component/state inspection.

## Enemy Lifecycle/Drop Wiring Result
- Spawned enemies had `EnemyLifecycleReporter` configured with `WaveManager`.
- Spawned enemies had `EnemyDeathDropper` referencing `EnemyDropTable_Default.asset`.
- Spawned enemies had `SpawnedEnemyRuntimeInfo` with `EnemyDefinition_FistMelee`, wave `1`, and score `100`.
- MCP could not force a defeat through direct component property editing because Unity MCP blocks that action during Play Mode.
- Manual Task 16.5 already confirmed real enemy death drops and pickup interaction.

## Ranged Enemy Balance Smoke Result
MCP temporarily enabled `SpawnDirector.debugSpawnOnStart` with `EnemyDefinition_Ranged` at wave `3`, then restored it to false.

Observed:
- One `Enemy_Ranged_JUTPS_Runtime` spawned under `_Systems/Spawned_Enemies`.
- It spawned on/near the baked play area at approximately `(-9.63, 0, -140.80)`.
- It had `SpawnedEnemyRuntimeInfo` with `EnemyDefinition_Ranged`, wave `3`, and score `200`.
- `EnemyTargetBinder` assigned `Player_JUTPS`.
- The JU TPS AI target fields pointed to `Player_JUTPS`.
- The enemy equipped `P226`.
- The character entered `FiringMode = true` and had a right-hand weapon assigned.
- Tuned values were active at runtime:
  - `Attack.GunAttack.MaxDistance = 8.5`
  - `Attack.GunAttack.Shooting.MaxShotDistance = 30`
- Console stayed clean.

This confirms the ranged enemy still reaches ranged combat state after the range tuning. Final feel still needs a human live encounter check.

## Failed-State Test Result
MCP temporarily enabled `PlayerDeathMonitor.debugReportDeathOnStart`, then restored it to false.

Observed:
- `GameFlowManager.CurrentState = Failed`
- `CurrentObjectiveText = You died`
- `EndScreen_Panel` became active.
- Console stayed clean.

## Victory-State Test Result
MCP temporarily enabled `GameFlowManager.debugCompleteExtractionOnStart`, then restored it to false.

Observed:
- `GameFlowManager.CurrentState = Victory`
- `ExtractionUnlocked = true`
- `VictoryReached = true`
- `CurrentObjectiveText = Extraction complete`
- `ExtractionObjective.IsUnlocked = true`
- `ExtractionObjective.IsCompleted = true`
- `EndScreen_Panel` became active.
- Console stayed clean.

## Restart Validation Result
- MCP did not simulate New Input System keyboard input.
- `EndScreenController` code inspection confirms `Keyboard.current.rKey.wasPressedThisFrame` is used under `ENABLE_INPUT_SYSTEM`.
- `RestartScene()` resets `Time.timeScale` and reloads the active scene.
- The user previously manually validated failed-state `R` restart after Task 17R.
- Victory-state `R` restart remains a manual follow-up unless tested in Game view.

## Cleanup Result
- Play Mode was exited.
- `WaveManager.autoStartOnPlay` restored to false.
- `SpawnDirector.debugSpawnOnStart` restored to false.
- `PlayerDeathMonitor.debugReportDeathOnStart` restored to false.
- `GameFlowManager` debug extraction flags restored to false.
- `EndScreen_Panel` is inactive by default.
- No runtime enemies or pickups were saved by this docs-only audit.
- The scene remained dirty, matching the pre-audit state, and was not saved or staged.

## Known Issues
- Ranged enemy needs final manual feel check after the range tuning.
- Some NavMesh stuck spots may still exist in detailed map areas.
- HUD is readable but visually plain.
- Victory-state `R` restart still needs manual Game view validation.
- MCP `execute_code` failed with a Windows command-length/toolchain issue, so direct runtime method calls were not available.

## Recommended Next Fixes
- Manual live encounter test for the tuned ranged enemy.
- NavMesh/spawn fairness pass around any observed stuck areas.
- Full Wave 5 to extraction pass, including victory-state restart with `R`.
- HUD/end screen visual polish pass after mechanics are stable.

## What Was Not Tested Automatically
- Manual shooting/killing during this audit.
- Pickup collection during this audit.
- Full five-wave completion.
- True keyboard `R` press through the New Input System.
- Final ranged combat feel from the player's camera.
