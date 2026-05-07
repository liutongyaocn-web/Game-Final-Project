# Spawn Point Plan

## Purpose
The spawn point foundation gives `LS_Arena_01` project-owned markers that future runtime systems can query when choosing safe enemy spawn positions. This prepares the arena for wave spawning without implementing `SpawnDirector` or `WaveManager` yet.

## Why Spawn Points Are Separate From WaveDefinition Data
`WaveDefinition` assets describe what should spawn and when. Spawn points describe where enemies may spawn in a specific scene. Keeping these separate lets the same enemy/wave data remain reusable while each scene controls its own layout, NavMesh routes, line-of-sight risks, and balance.

## SpawnPointRole Summary
- `Melee`: intended for fist and knife/blade melee enemies.
- `Ranged`: intended for gun/ranged enemies.
- `Mixed`: can be used by melee or ranged requests.

## LastStandSpawnPoint Script Summary
`LastStandSpawnPoint` stores scene-specific spawn metadata:
- spawn id
- role
- minimum and preferred distance from the player
- whether a future line-of-sight check is required
- early-wave availability
- minimum wave
- notes

It validates simple data ranges in the Inspector and exposes `IsEligibleForWave(int waveNumber)` plus a `Position` property.

## SpawnPointGroup Script Summary
`SpawnPointGroup` stores and auto-collects child `LastStandSpawnPoint` components. Future systems can query all points or get points eligible for a role and wave number. It does not instantiate enemies and does not choose random/weighted points yet.

## Configured LS_Arena_01 Spawn Points

| Object path | spawnId | Role | Min distance | Preferred distance | Minimum wave | Line-of-sight check | Notes |
|---|---|---|---:|---:|---:|---|---|
| `_SpawnPoints/Spawn_Melee_A` | `melee_a` | Melee | 12 | 25 | 1 | false | Early melee approach from one side of the arena. |
| `_SpawnPoints/Spawn_Melee_B` | `melee_b` | Melee | 12 | 25 | 1 | false | Secondary melee approach route. |
| `_SpawnPoints/Spawn_Melee_C` | `melee_c` | Melee | 12 | 30 | 2 | false | Later melee pressure route. |
| `_SpawnPoints/Spawn_Ranged_A` | `ranged_a` | Ranged | 18 | 32 | 3 | true | Ranged line-of-sight point. Needs later balance because ranged damage is high. |
| `_SpawnPoints/Spawn_Ranged_B` | `ranged_b` | Ranged | 18 | 35 | 4 | true | Alternate ranged pressure point for later waves. |

## Relationship To Future SpawnDirector
`SpawnDirector` can query `SpawnPointGroup.GetEligiblePoints(role, waveNumber)`, filter for distance/line of sight/NavMesh checks later, and instantiate enemy prefabs from `EnemyDefinition` assets at the chosen point.

## Relationship To Future WaveManager
`WaveManager` can use each wave's composition to request melee or ranged spawn points through `SpawnDirector`. The minimum wave values help prevent later pressure routes from being used too early.

## Balance Risks
- Ranged enemies are highly lethal, so ranged spawn points require later line-of-sight, cover, and distance validation.
- Melee spawn points need NavMesh route checks to avoid enemies getting stuck or taking too long to engage.
- Spawn points should avoid immediate unfair spawns inside the player's view unless the wave design explicitly calls for visible pressure.

## Coursework002 Evidence Supported
- Level progression: spawn availability changes across early and later waves.
- Content generation: future enemy generation can use controlled scene spawn points instead of arbitrary positions.
- Game AI: melee and ranged roles support distinct approach and pressure behaviours.
- C# code quality: reusable scene components separate spawn metadata from runtime spawning logic.
