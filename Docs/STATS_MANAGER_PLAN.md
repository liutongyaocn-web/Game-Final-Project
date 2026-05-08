# StatsManager Plan

## Purpose
The StatsManager foundation tracks the core gameplay statistics needed for Last Stand: kills, score, current wave, enemy counts, and survival time. The data is now displayed by the project-owned Last Stand HUD layer.

## Why Stats Are Separate From HUD
Statistics are gameplay state. HUD is presentation. Keeping them separate allows `LastStandStatsManager` to be tested and reused before a custom Last Stand HUD layer is added on top of the JU TPS default UI.

## Relationship To WaveManager
`WaveManager` reports:
- run start and total wave count
- current wave number
- enemies spawned this wave
- total enemies in the current wave
- alive enemy count
- enemy defeat events

## Relationship To EnemyDefinition
`EnemyDefinition` provides the configured score value for each enemy type. This keeps scoring data in ScriptableObject assets rather than hard-coded in the wave or stats systems.

## Relationship To SpawnedEnemyRuntimeInfo
`SpawnedEnemyRuntimeInfo` is attached to runtime-spawned enemy roots by `SpawnDirector`. It records:
- source `EnemyDefinition`
- wave number
- score value
- spawn time
- whether the enemy has already been counted as defeated

This prevents duplicate score/kills if a lifecycle event is reported more than once.

## LastStandStatsManager Script Summary
`Assets/_LastStand/Scripts/Stats/LastStandStatsManager.cs` tracks run-level and wave-level stats. It exposes read-only properties for future HUD work and public methods for WaveManager integration:
- `ResetStats`
- `BeginRun`
- `EndRun`
- `SetCurrentWave`
- `SetWaveEnemyCounts`
- `RegisterEnemyDefeated`

## Tracked Stats
- kills
- score
- current wave
- total waves
- enemies alive
- enemies spawned this wave
- total enemies this wave
- survival time

## Scoring Table
| Enemy role | EnemyDefinition score value |
|---|---:|
| FistMelee | 100 |
| Knife/BladeMelee | 150 |
| Ranged | 200 |

## Runtime Validation Result
Validation was performed in `LS_Arena_01` with `WaveManager.autoStartOnPlay` temporarily enabled:
- Wave 1 started successfully.
- Spawned enemies received `SpawnedEnemyRuntimeInfo`.
- `CurrentWaveNumber` became `1`.
- `TotalWaves` was `5`.
- Wave 1 total enemy count was `4`.
- Alive enemy count reached `2`, matching Wave 1 `maxAliveAtOnce`.
- Survival time increased during Play Mode.
- Destroy-fallback defeat validation increased kills from `0` to `1`.
- Score increased from `0` to `100` for a fist melee enemy.
- WaveManager continued spawning to maintain max-alive pressure after the enemy was removed.
- Unity Console reported 0 errors and 0 warnings.
- `autoStartOnPlay` was restored to false before saving.
- No runtime-spawned enemies remain saved in the scene.

The user also manually confirmed after Task 11 that normal combat kills allow WaveManager to continue spawning/progressing, so the stats path is ready for a later hands-on kill/score pass.

## Task 13 HUD Display Integration
`LastStandStatsManager` is now displayed through `LastStandHudController` in `LS_Arena_01`. The HUD shows wave, enemy count, kills, score, survival time, FPS, player health when readable, and objective text while keeping the stats logic separate from UI presentation.

Runtime validation confirmed the HUD read Wave 1 stats (`1 / 5`, `2 / 4` enemies), survival/FPS values, player health (`400 / 400`), and updated kills/score to `1` and `100` after a defeat event.

## Task 14 Victory Integration
`GameFlowManager` can call `LastStandStatsManager.EndRun()` when extraction is completed. This allows survival time to continue after the final wave is cleared and stop when the player actually reaches extraction.

## Deliberately Not Implemented
- Pickups or enemy death drops.
- Game over UI.
- Ranged enemy damage tuning.

## Future Pickup/Drop Design
- Do not use fixed pickup points.
- Health/ammo should drop from killed enemies.
- Later implementation should inspect or reference the JU TPS AI Attack Demo health/ammo pickup and drop setup.

## Coursework002 Evidence Supported
- UI/statistics: provides data for wave, kills, score, enemy remaining, and survival timer HUD.
- Game mechanics: connects enemy defeat to kills and score.
- Level progression: statistics reflect the active wave and wave enemy counts.
- C# code quality: keeps runtime metadata and statistics separate from UI, spawning, and JU TPS internals.
