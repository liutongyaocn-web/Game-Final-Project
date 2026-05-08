# Final Gameplay Pacing

## Problem Observed
Manual playtesting showed that the core 5-wave loop basically works, but the rhythm was too slow for comfortable validation and final demonstration. The player did not naturally reach victory during ordinary test time, which made the extraction and victory restart flow harder to verify.

The ranged enemy is still dangerous, even after range tuning, so pacing changes prioritise shorter wave length and limited ranged counts rather than damage changes.

## Start Flow Change
`WaveManager.autoStartOnPlay` is now enabled in `Assets/_LastStand/Scenes/LS_Arena_01.unity`.

This means the final playable scene begins the wave loop automatically when Play Mode starts, without requiring the user to select `_Systems/WaveSystem` or manually trigger a debug start.

Debug-only settings remain disabled:
- `SpawnDirector.debugSpawnOnStart = false`
- `PlayerDeathMonitor.debugReportDeathOnStart = false`
- `GameFlowManager.debugUnlockExtractionOnStart = false`
- `GameFlowManager.debugCompleteExtractionOnStart = false`

## Wave Balance Changes
The five-wave structure is retained, but the total enemy count was reduced from 37 to 28 and intermissions were shortened from 8 seconds to 5 seconds for Waves 1-4.

| Wave | Old composition | New composition | Old total | New total | Key timing change |
|---|---|---|---:|---:|---|
| 1 | 4 fist | 3 fist | 4 | 3 | Spawn interval `4.0s` -> `2.5s`; intermission `8s` -> `5s` |
| 2 | 5 fist, 2 knife/blade | 3 fist, 1 knife/blade | 7 | 4 | Spawn interval `3.5s` -> `2.5s`; intermission `8s` -> `5s` |
| 3 | 4 fist, 3 knife/blade, 1 ranged | 3 fist, 2 knife/blade, 1 ranged | 8 | 6 | Spawn interval `3.0s` -> `2.25s`; intermission `8s` -> `5s` |
| 4 | 5 fist, 4 knife/blade, 2 ranged | 4 fist, 2 knife/blade, 1 ranged | 11 | 7 | Spawn interval `2.75s` -> `2.0s`; intermission `8s` -> `5s` |
| 5 | 6 fist, 4 knife/blade, 3 ranged | 4 fist, 3 knife/blade, 1 ranged | 13 | 8 | Spawn interval `2.5s` -> `2.0s`; max alive `5` -> `4` |

## Final Wave Composition

| Wave | Fist melee | Knife/blade melee | Ranged | Max alive | Spawn interval | Extraction unlock |
|---|---:|---:|---:|---:|---:|---|
| 1 | 3 | 0 | 0 | 2 | 2.5s | No |
| 2 | 3 | 1 | 0 | 3 | 2.5s | No |
| 3 | 3 | 2 | 1 | 3 | 2.25s | No |
| 4 | 4 | 2 | 1 | 4 | 2.0s | No |
| 5 | 4 | 3 | 1 | 4 | 2.0s | Yes |

## Why 5 Waves Are Retained
The original design and existing documentation frame Last Stand as a 5-wave survival game. Keeping five waves preserves the intended level progression:
- Wave 1 teaches fist melee pressure.
- Wave 2 adds the knife/blade melee enemy.
- Wave 3 introduces ranged pressure.
- Wave 4 mixes all enemy types.
- Wave 5 remains the final survival test and unlocks extraction.

## Ranged Enemy Decision
Wave 5 uses 1 ranged enemy, not 2. This keeps the ranged enemy important while respecting the playtest feedback that ranged pressure can become lethal quickly. Damage was not changed in this task.

## Demo And Assessment Support
The final scene is now easier to run directly in Play Mode, and a complete 5-wave pass should be more realistic during a demo or video recording. The shorter wave data keeps evidence for level progression, game AI variety, enemy spawning, player survival, drops, HUD statistics, and extraction victory without dragging the test session.

## Validation Result
Unity imported the edited scene and wave assets, and the Editor log showed no C# compile errors from these data-only changes. Unity MCP stopped responding after the refresh, so a final Play Mode auto-start smoke could not be completed in this task. The previous Task 18.5 smoke audit had already validated Wave 1 auto-start behaviour when the same WaveManager flag was temporarily enabled.

## Remaining Manual Checks
- Full Wave 5 clear.
- Extraction trigger after final wave completion.
- Victory-state `R` restart.
- Ranged enemy feel after shortened wave pacing.
- NavMesh stuck spots in detailed map areas.
