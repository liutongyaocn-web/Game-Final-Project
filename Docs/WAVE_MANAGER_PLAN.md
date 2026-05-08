# WaveManager Plan

## Purpose
`WaveManager` is the runtime foundation for Last Stand's five-wave survival loop. It reads `WaveDefinition` assets, expands their enemy entries into a temporary spawn queue, and asks `SpawnDirector` to spawn enemies over time.

## Relationship To WaveDefinition
`WaveDefinition` provides the wave number, enemy composition, max-alive limit, spawn interval, intermission duration, and extraction unlock flag. `WaveManager` uses this data directly instead of hard-coding wave contents.

## Relationship To SpawnDirector
`WaveManager` does not choose scene positions or instantiate prefabs itself. It delegates each enemy spawn request to `SpawnDirector`, which selects an eligible spawn point and instantiates the enemy prefab.

## Relationship To EnemyTargetBinder
Spawned enemy prefabs already include `EnemyTargetBinder`. Runtime validation confirmed that spawned enemies bind to `Player_JUTPS` and use JU TPS AI behaviour after spawning.

## WaveState Summary
- `Idle`: no wave is running.
- `Starting`: a wave has been selected and is waiting for its start delay.
- `Spawning`: enemies are being spawned while respecting max alive.
- `WaitingForClear`: all planned enemies are spawned, and the manager is waiting for active enemies to disappear.
- `Intermission`: time between waves.
- `Completed`: final wave/extraction condition reached.
- `Failed`: invalid setup or unrecoverable start condition.

## WaveManager Script Summary
`Assets/_LastStand/Scripts/Waves/WaveManager.cs` provides:
- `StartWaves`
- `StartWave(int waveIndex)`
- `StopWaves`
- `RegisterEnemyRemoved(GameObject enemy)`
- `NotifyEnemyDefeated(GameObject enemy)`
- read-only properties for current wave, state, counts, alive enemies, and extraction readiness
- debug context menu entry points

## Wave Execution Flow
`Idle -> Starting -> Spawning -> WaitingForClear -> Intermission -> Completed`

For non-final waves, the manager waits for intermission and starts the next wave. For the final wave or a wave with extraction unlock enabled, it ends in `Completed`.

## Implemented In Task 10
- `WaveState` enum.
- `WaveManager` runtime foundation.
- `_Systems/WaveSystem` scene object.
- `WaveManager` references to the five `WaveDefinition` assets in order.
- `WaveManager` reference to the scene `SpawnDirector`.
- Safe Wave 1 runtime validation with `autoStartOnPlay` temporarily enabled.

## Deliberately Not Implemented
- Final death/lifecycle reporting.
- Score and kill tracking.
- HUD updates.
- Pickups and enemy death drops.
- Extraction activation.
- Win/lose UI.
- Game over flow.

## Temporary Enemy Lifecycle Limitation
Enemy death/lifecycle reporting has not been implemented yet. `WaveManager` currently removes null or inactive GameObjects from its alive list. If JU TPS dead enemies remain active in the scene, waves may not complete automatically until a future `EnemyDeathReporter` or `EnemyLifecycleBridge` tells the manager an enemy has been defeated.

## Runtime Validation Result
- Wave 1 start/spawn validation passed.
- `WaveManager` spawned enemies through `SpawnDirector`.
- `maxAliveAtOnce` was respected during the observation window: Wave 1 stopped at two active enemies even though four total enemies are queued.
- Spawned enemies bound to `Player_JUTPS` through `EnemyTargetBinder`.
- Unity Console had 0 errors and 0 warnings after final validation.
- `autoStartOnPlay` was restored to false before saving.
- No runtime-spawned enemies remain saved in the scene.

## Future EnemyDeathReporter Requirement
A future bridge should listen for JU TPS enemy death/disable events and call `WaveManager.NotifyEnemyDefeated(enemy)`. This will allow waves to clear reliably, drive score/kills, and trigger intermission/extraction progression.

## Task 11 EnemyLifecycleReporter Integration
`WaveManager` now configures `EnemyLifecycleReporter` on each spawned enemy. The reporter calls `WaveManager.NotifyEnemyDefeated(GameObject enemy)` once when it detects defeat or receives a destroy fallback. Runtime validation confirmed that spawned enemies receive the reporter and that destroying runtime-spawned enemies lets Wave 1 clear and progress into Wave 2.

Real hands-on JU TPS death detection from player combat still needs confirmation, but the bridge is now in place for wave progression, future score/kills, and future enemy drop logic.

## Task 12 Stats Integration
`WaveManager` now reports run start, current wave, wave enemy counts, alive enemy count, and enemy defeat events to `LastStandStatsManager`. This allows statistics to update independently of the future HUD.

## Task 13 HUD Readout
The Last Stand HUD now reads wave and enemy-count state through `LastStandStatsManager` and `WaveManager`. Runtime validation confirmed the HUD displayed Wave 1 as `1 / 5`, enemy count as `2 / 4`, and objective text while `WaveManager.autoStartOnPlay` was temporarily enabled for testing and restored to false before saving.

## Task 14 Final-Wave Completion Signal
`WaveManager` now exposes `FinalWaveCompleted` and `HasCompletedAllWaves`. When the final wave completes, it signals `GameFlowManager` so extraction can unlock outside the wave system. This keeps wave spawning/progression separate from victory and extraction responsibilities.

## Future Enemy Death Drop Design
- Do not use fixed pickup points.
- Health/ammo should drop from killed enemies.
- Later implementation should inspect or reference the JU TPS AI Attack Demo health/ammo pickup and drop setup.

## Known Balance Risks
- Ranged enemy damage is high.
- Ranged spawn line of sight may feel unfair without tuning.
- `maxAliveAtOnce` values may need adjustment after hands-on wave tests.

## Coursework002 Evidence Supported
- Level progression: configured wave sequence is now executable as a foundation.
- Content generation: enemies are generated over time from wave data.
- Game AI: runtime-spawned JU TPS enemies target and attack the player.
- C# code quality: wave orchestration is separated from spawn location selection and enemy prefab data.
