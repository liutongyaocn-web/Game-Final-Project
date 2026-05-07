# Wave Definition Plan

## Purpose
The wave definition layer stores Last Stand's five-wave survival progression as ScriptableObject data. Future runtime systems can read these assets to decide which enemies appear, how many spawn, how quickly they spawn, and when extraction unlocks.

## Why Wave Definitions Are Data-Only In This Task
This task prepares configuration only. It does not implement `WaveManager`, `SpawnDirector`, enemy pooling, score updates, HUD updates, pickups, extraction logic, or enemy death reporting. Keeping the wave plan as data first makes the final gameplay loop easier to tune and explain.

## Relationship To EnemyDefinition Assets
Each wave entry references an `EnemyDefinition` asset created in Task 6. Those enemy definitions reference the validated JU TPS-based enemy prefabs, which now include runtime target binding from Task 6.5.

## WaveEnemyEntry Script Summary
`WaveEnemyEntry` is a serializable data class containing:
- enemy definition reference
- spawn count
- spawn weight
- minimum and maximum spawn group size
- notes

Validation clamps counts, weights, and group sizes to safe values.

## WaveDefinition Script Summary
`WaveDefinition` is a ScriptableObject containing:
- wave number, display name, and objective text
- enemy composition entries
- max alive enemy count
- spawn interval
- start delay
- intermission duration
- extraction unlock flag
- difficulty notes
- runtime validation flag

It also exposes helper properties for total enemy count and checking whether the wave includes fist, knife/blade, or ranged enemy roles.

## Created Wave Assets

| Wave asset | Wave number | Enemy mix | Total enemies | Max alive | Spawn interval | Extraction unlock | Balance notes |
|---|---:|---|---:|---:|---:|---|---|
| `Assets/_LastStand/ScriptableObjects/Waves/WaveDefinition_01.asset` | 1 | 4 fist melee | 4 | 2 | 4.0s | No | Introduces only the fist-based melee enemy. |
| `Assets/_LastStand/ScriptableObjects/Waves/WaveDefinition_02.asset` | 2 | 5 fist melee, 2 knife/blade melee | 7 | 3 | 3.5s | No | Adds armed melee pressure while staying manageable. |
| `Assets/_LastStand/ScriptableObjects/Waves/WaveDefinition_03.asset` | 3 | 4 fist melee, 3 knife/blade melee, 1 ranged | 8 | 3 | 3.0s | No | Introduces ranged pressure cautiously because ranged damage is high. |
| `Assets/_LastStand/ScriptableObjects/Waves/WaveDefinition_04.asset` | 4 | 5 fist melee, 4 knife/blade melee, 2 ranged | 11 | 4 | 2.75s | No | Mixed pressure with limited ranged count. |
| `Assets/_LastStand/ScriptableObjects/Waves/WaveDefinition_05.asset` | 5 | 6 fist melee, 4 knife/blade melee, 3 ranged | 13 | 5 | 2.5s | Yes | Final wave unlocks extraction; ranged count remains controlled. |

## How This Supports Future SpawnDirector
`SpawnDirector` can use each wave's enemy entries, spawn weights, group sizes, and max-alive value to choose what to spawn and when. Because each entry points to an `EnemyDefinition`, the director can spawn project-owned prefabs without hard-coded references.

## How This Supports Future WaveManager
`WaveManager` can use wave order, start delay, intermission time, total enemy count, and extraction unlock flags to run the 5-wave survival loop. The data already describes when the final objective should become available.

## Coursework002 Evidence Supported
- Level progression: five waves gradually increase enemy variety and pressure.
- Content generation: enemy composition, weights, group sizes, and timing are data-driven.
- Game AI: validated enemy roles are assigned to wave data.
- Game mechanics: wave survival, pacing, max alive limits, and extraction unlock are explicitly planned.
- C# code quality: ScriptableObjects separate balancing data from future runtime systems.

## Known Balancing Risks
- The ranged enemy is highly lethal, so ranged counts and sightlines must stay conservative until damage/fairness tuning is completed.
- Group spawning should be tested carefully to avoid unfair surround behaviour.
- Spawn points must be validated against the baked NavMesh and line-of-sight rules before runtime spawning is enabled.
- `validatedForRuntime` remains false until a future WaveManager/SpawnDirector test proves each wave works in Play Mode.
