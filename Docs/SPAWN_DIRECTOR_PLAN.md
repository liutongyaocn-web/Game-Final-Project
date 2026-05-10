# SpawnDirector Plan

## Purpose
`SpawnDirector` is the first Last Stand runtime spawning foundation. It can instantiate one enemy from an `EnemyDefinition` at an eligible `LastStandSpawnPoint`, using the arena's `SpawnPointGroup`.

This task prepares spawning for later wave execution, but does not implement `WaveManager` or full wave gameplay.

## Relationship To EnemyDefinition
`EnemyDefinition` provides the enemy prefab and combat role. `SpawnDirector` reads these values instead of hard-coding enemy prefab references.

## Relationship To SpawnPointGroup
`SpawnPointGroup` provides eligible scene spawn points by role and wave number. `SpawnDirector` asks for melee, ranged, or mixed spawn points depending on the enemy role.

## Relationship To EnemyTargetBinder
All enemy prefabs already include `EnemyTargetBinder`. After instantiation, the spawned enemy can bind to `Player_JUTPS` automatically on `Start`. `SpawnDirector` also calls `BindTarget(player)` immediately when a binder and player are assigned, giving future systems a predictable target binding point.

## SpawnDirector Script Summary
`Assets/_LastStand/Scripts/Spawning/SpawnDirector.cs` supports:
- selecting a spawn point for an enemy definition and wave number
- mapping enemy combat roles to spawn point roles
- optional player-distance filtering
- optional NavMesh sampling with `NavMesh.SamplePosition`
- instantiating a prefab under a spawned enemy parent
- debug-only single enemy spawn on Start

It does not read full wave lists, spawn multiple enemies, track kills, update UI, or manage game state.

## Role Mapping

| Enemy role | Spawn point role |
|---|---|
| `FistMelee` | `Melee` |
| `KnifeMelee` | `Melee` |
| `Ranged` | `Ranged` |

## NavMesh Validation Approach
When enabled, the director samples each candidate point with `UnityEngine.AI.NavMesh.SamplePosition` using the configured radius. Points that cannot sample to NavMesh are skipped. Spawned enemies use the sampled position when available and the marker rotation for facing direction.

## Runtime Validation Result
- Scene: `Assets/_LastStand/Scenes/LS_Arena_01.unity`
- Spawn system object: `_Systems/SpawnSystem`
- Spawn parent: `_Systems/Spawned_Enemies`
- Fist debug spawn: validated from `EnemyDefinition_FistMelee.asset` at wave 1.
- Ranged debug spawn: validated from `EnemyDefinition_Ranged.asset` at wave 3.
- EnemyTargetBinder: spawned enemies bound to `Player_JUTPS`.
- Console result: 0 red errors after final validation.
- Final saved state: `debugSpawnOnStart` restored to false and no runtime-spawned enemies saved in the scene.

## Deliberately Not Implemented
- No `WaveManager`.
- No full wave execution.
- No spawn weights from `WaveDefinition`.
- No enemy pooling.
- No stats, score, HUD, pickups, enemy death reporting, or drop logic.
- No damage balance changes.

## Future WaveManager Integration
Future `WaveManager` can read a `WaveDefinition`, ask `SpawnDirector` to spawn selected `EnemyDefinition` entries over time, then track living enemies and wave completion.

## Task 10 WaveManager Integration
`WaveManager` now consumes `SpawnDirector` for wave-based spawning. Runtime validation confirmed Wave 1 can request spawns through `SpawnDirector`, which selects melee spawn points and instantiates enemies from `EnemyDefinition` data.

## Task 11 Lifecycle Integration
`SpawnDirector` still only selects spawn points and instantiates prefabs. After `SpawnDirector` returns the spawned enemy GameObject, `WaveManager` configures `EnemyLifecycleReporter` on the enemy so lifecycle reporting stays owned by the wave layer rather than the spawn-location layer.

## Task 12 Runtime Metadata Integration
`SpawnDirector` now attaches `SpawnedEnemyRuntimeInfo` to runtime-spawned enemies. The metadata records the source `EnemyDefinition`, wave number, configured score value, and spawn time so `LastStandStatsManager` can award score/kills without hard-coded prefab checks.

## Task 25B Spawn Fallback Fix
SpawnDirector now uses a safer fallback when normal spawn filtering fails:
- first tries role-eligible points with player-distance and NavMesh checks
- if all points fail distance filtering, retries role-eligible points that still sample to NavMesh
- uses a fallback NavMesh sample radius of `6`
- does not ignore the requested enemy role

This reduces the chance that a final ranged Wave 4 spawn fails because all ranged points are temporarily too close or slightly outside the primary NavMesh sample radius.

## Pickup/Drop Design Decision
- Do not use fixed health/ammo pickup points.
- Future health/ammo pickups should drop from killed enemies.
- Later implementation should inspect or reference the JU TPS AI Attack Demo health/ammo drop and pickup setup.

## Known Risks
- The ranged enemy's damage is high and needs later balancing.
- Ranged spawn line of sight may feel unfair if positions are not tuned.
- Spawn point distances need hands-on gameplay validation.
- Runtime-spawned enemies may need pooling later to avoid allocation spikes.

## Coursework002 Evidence Supported
- Content generation: controlled enemy instantiation from data and spawn points.
- Game AI: spawned enemies use JU TPS behaviours and runtime target binding.
- Level progression: spawning can now be driven by wave number and role.
- C# code quality: modular runtime spawning layer integrates with ScriptableObject data and scene components.
