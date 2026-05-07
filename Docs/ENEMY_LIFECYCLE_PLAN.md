# Enemy Lifecycle Reporter Plan

## Purpose
`EnemyLifecycleReporter` gives the project-owned wave system a bridge from JU TPS enemy prefabs back to `WaveManager`. The goal is to let waves clear when enemies are defeated without editing JU TPS source scripts or third-party prefabs.

## Why WaveManager Needs Enemy Defeat Reporting
Task 10 confirmed that `WaveManager` can spawn enemies through `SpawnDirector`, but it could only remove null or inactive GameObjects from its alive list. JU TPS enemies may ragdoll, remain active, disable, or be destroyed depending on their internal setup, so Last Stand needs a project-owned reporter on each spawned enemy.

## JU TPS Components Inspected
The three Last Stand enemy prefabs include these relevant JU TPS components:

| Component | Observed purpose | Useful lifecycle data |
|---|---|---|
| `JUTPS.JUCharacterController` | Character movement, combat, animation, ragdoll state, item equip state | `IsDead`, `CharacterHealth`, `IsRagdolled` |
| `JUTPS.JUHealth` | Health and death events | `IsDead`, `Health`, `MaxHealth`, `OnDeath`, `OnDamaged` |
| `JUTPS.ArmorSystem.DamageableBody` | Body part damage multipliers | Damage routing support |
| `JUTPS.PhysicsScripts.AdvancedRagdollController` | Ragdoll state after death/damage | `IsOnRagdoll`, `State` |
| `JU.CharacterSystem.AI.Examples.JU_AI_AttackActionExample` | AI attack and target behaviour | Targeting and navigation state |

## EnemyLifecycleReporter Script Summary
`Assets/_LastStand/Scripts/AI/EnemyLifecycleReporter.cs` is a small MonoBehaviour bridge. It has no compile-time dependency on JU TPS classes. It can:
- auto-find `WaveManager`
- be configured by `WaveManager` after spawning
- poll likely health/death fields by reflection
- report defeated enemies once
- use destroy fallback reporting
- expose `ForceReportDefeatedForDebug` for future validation

## Detection Strategies
- Health/death polling: checks likely bool members such as `IsDead`, `Dead`, `IsAlive`, and likely numeric members such as `Health`, `CurrentHealth`, `HP`, and `Life`.
- Destroy fallback: reports defeat from `OnDestroy` during Play Mode, unless the application is quitting.
- Disable fallback: available but disabled by default, because disabled scene test enemies should not count as defeated.
- Debug force-report method: `ForceReportDefeatedForDebug` calls the same reporting path without needing enemy combat.

## Prefabs Updated
`EnemyLifecycleReporter` was added to:
- `Assets/_LastStand/Prefabs/Enemies/Enemy_FistMelee_JUTPS.prefab`
- `Assets/_LastStand/Prefabs/Enemies/Enemy_KnifeMelee_JUTPS.prefab`
- `Assets/_LastStand/Prefabs/Enemies/Enemy_Ranged_JUTPS.prefab`

Default settings:
- `autoFindWaveManager = true`
- `pollHealthState = true`
- `pollIntervalSeconds = 0.2`
- `reportOnDestroy = true`
- `reportOnDisable = false`
- `logLifecycleEvents = false`

## WaveManager Integration
`WaveManager` now configures a reporter after each successful spawn. If the spawned enemy prefab already has `EnemyLifecycleReporter`, it calls `Configure(this)`. If a spawned enemy is missing the component, `WaveManager` adds one to the spawned root and configures it.

`WaveManager.NotifyEnemyDefeated(GameObject enemy)` remains the single project-owned entry point for removing defeated enemies from `aliveEnemies`.

## Runtime Validation Result
Validation was performed in `LS_Arena_01`:
- Wave 1 was temporarily started through `autoStartOnPlay`.
- Spawned fist melee enemies included `EnemyLifecycleReporter`.
- The reporter was configured with `_Systems/WaveSystem` `WaveManager`.
- `EnemyTargetBinder` still bound spawned enemies to `Player_JUTPS`.
- A Play Mode destroy-fallback validation was performed by deleting runtime-spawned enemies.
- `WaveManager` progressed from Wave 1 into Wave 2 after the spawned enemies were removed, confirming lifecycle notifications can unblock wave progression.
- `autoStartOnPlay` was restored to false before saving.
- No runtime-spawned enemies remain saved in the scene.

## Deliberately Not Implemented
- Score.
- Kill statistics.
- HUD updates.
- Pickups or enemy death drops.
- Extraction objective.
- Game over or win flow.
- Ranged enemy damage balancing.

## Known Limitations
- Real hands-on JU TPS death detection from player damage still needs confirmation.
- MCP could not invoke the debug context method directly because Unity's temporary code execution still hit the Windows filename/extension length limit.
- `OnDestroy` fallback is validated, but if JU TPS leaves defeated enemies active without changing `IsDead` or `Health`, further lifecycle tuning may be needed.
- `reportOnDisable` remains false to avoid counting disabled test/prefab preview enemies as defeated.

## Manual Validation Still Needed
During a later hands-on gameplay pass, kill a spawned fist, knife/blade, and ranged enemy through normal combat and confirm:
- `EnemyLifecycleReporter` reports once.
- `WaveManager.AliveEnemyCount` decreases.
- Waves can complete without deleting enemies manually.

## Future Pickup/Drop Design
- Do not use fixed pickup points.
- Health/ammo should drop from killed enemies.
- Later implementation should inspect or reference the JU TPS AI Attack Demo health/ammo pickup and drop setup.

## Coursework002 Evidence Supported
- C# code quality: project-owned bridge keeps WaveManager decoupled from JU TPS internals.
- Game AI: enemy lifecycle now connects spawned AI enemies to wave progression.
- Level progression: waves can move forward when enemies are removed/defeated.
- Future mechanics: prepares kill, score, HUD, and drop systems.
