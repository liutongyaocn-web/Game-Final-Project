# Enemy Death Drop Plan

## Purpose
Enemy death drops give Last Stand a resource-recovery foundation without using fixed health or ammo pickup points. Defeated enemies can now spawn JU TPS pickup prefabs near their death location, keeping pickup behaviour owned by JU TPS while the drop timing and chance are owned by Last Stand.

## Design Decision
- Do not use fixed pickup points for gameplay.
- Do not use existing `_PickupPoints` markers for recovery items.
- Health and ammo recovery should come from defeated enemies.
- Do not rewrite JU TPS pickup or inventory behaviour when usable JU TPS pickup prefabs already exist.

## JU TPS Pickup Assets Inspected
The JU TPS AI Attack example folder and related demo prefabs were inspected without modifying third-party assets:
- `Assets/Julhiecio TPS Controller/Demos/Demo Scenes/AI/Examples/Attack/`
- `Assets/Julhiecio TPS Controller/Demos/Demo Prefabs/`
- `Assets/Julhiecio TPS Controller/Demos/Demo Prefabs/Interactable/`
- `Assets/JUTPS Addons`

The AI Attack player setup includes pickup/inventory support such as `EnablePickup`, default pickup input, and auto-equip settings. The health/ammo pickup prefabs were identified in the JU TPS demo interactable prefab folder rather than as custom Last Stand assets.

## Identified Health Pickup Prefab
`Assets/Julhiecio TPS Controller/Demos/Demo Prefabs/Interactable/HealthPowerUp.prefab`

Observed setup:
- Uses the JU TPS `HealthPowerUp` component.
- Configured with `HealthToAdd: 100`.
- Includes JU TPS effect/rotation support.
- Treated as the valid health pickup candidate for Last Stand enemy death drops.

## Identified Ammo Pickup Prefab
`Assets/Julhiecio TPS Controller/Demos/Demo Prefabs/Interactable/AmmoPowerUp.prefab`

Observed setup:
- Uses the JU TPS `AmmoBox` component.
- Configured with `AmmoCount: 32`.
- Uses `WeaponName: AnyWeapon`.
- Includes JU TPS effect/rotation support.
- Treated as the valid ammo pickup candidate for Last Stand enemy death drops.

## EnemyDropTable Summary
`Assets/_LastStand/Scripts/Pickups/EnemyDropTable.cs` is a ScriptableObject drop table. It stores drop entries, optional wave restrictions, scatter radius, vertical offset, and whether only one item should drop per defeated enemy.

Created asset:
`Assets/_LastStand/ScriptableObjects/Pickups/EnemyDropTable_Default.asset`

Initial entries:
| Pickup | Chance | Min wave | Notes |
|---|---:|---:|---|
| `HealthPowerUp.prefab` | 0.20 | 1 | Emergency recovery drop from defeated enemies. |
| `AmmoPowerUp.prefab` | 0.30 | 1 | Ammunition recovery drop from defeated enemies. |

## EnemyDeathDropper Summary
`Assets/_LastStand/Scripts/Pickups/EnemyDeathDropper.cs` is a project-owned component added to enemy prefab roots. It subscribes to `EnemyLifecycleReporter`, reads the spawned enemy wave number from `SpawnedEnemyRuntimeInfo` when available, selects an eligible drop from `EnemyDropTable`, and instantiates the selected JU TPS pickup prefab at the enemy death position with a small offset/scatter.

The component does not:
- add score or kills
- handle player pickup manually
- modify pickup prefabs
- parent pickups to defeated enemies
- reference JU TPS classes directly

## EnemyLifecycleReporter Event Integration
`EnemyLifecycleReporter` now exposes:
`Defeated(EnemyLifecycleReporter reporter, GameObject enemy, string reason)`

The event is invoked exactly once from the existing `ReportDefeated` path. `WaveManager.NotifyEnemyDefeated` remains intact, so wave progression and drop spawning both respond to the same defeat report while staying separated.

## Prefabs Updated
`EnemyDeathDropper` was added to:
- `Assets/_LastStand/Prefabs/Enemies/Enemy_FistMelee_JUTPS.prefab`
- `Assets/_LastStand/Prefabs/Enemies/Enemy_KnifeMelee_JUTPS.prefab`
- `Assets/_LastStand/Prefabs/Enemies/Enemy_Ranged_JUTPS.prefab`

Each prefab references `EnemyDropTable_Default.asset` and keeps:
- `autoFindLifecycleReporter = true`
- `dropOnlyOnce = true`
- `logDropEvents = false`

## Runtime Validation Result
Unity compilation and prefab/drop-table wiring were validated with 0 Console errors and 0 warnings. The drop table asset references the JU TPS health and ammo pickup prefabs, and all three Last Stand enemy prefabs include `EnemyDeathDropper`.

A hands-on Play Mode drop test was not completed in this task because no scene/debug setup changes were required and the implementation should remain focused. Runtime drop instantiation should be verified during the next gameplay pass by starting Wave 1, killing an enemy, and confirming a pickup appears near the defeated enemy.

## Pickup Interaction Result
Pickup interaction/effect was not manually confirmed in this task. The selected prefabs are JU TPS pickup prefabs, so the intended next check is to confirm that the existing JU TPS player pickup/inventory/health behaviour handles them correctly after they are dropped.

## Manual Drop and Pickup Validation
Enemy death drops were manually tested in `LS_Arena_01` after Task 16.

- Wave 1 was started in Play Mode.
- Enemies were killed through normal player combat.
- `HealthPowerUp` spawned near defeated enemies.
- `AmmoPowerUp` spawned near defeated enemies.
- `Player_JUTPS` successfully picked up `HealthPowerUp`.
- Health recovery worked.
- `Player_JUTPS` successfully picked up `AmmoPowerUp`.
- Ammo pickup worked.
- Unity Console remained free of red errors and red assertions.
- Temporary debug/start settings were restored after Play Mode:
  - `WaveManager.autoStartOnPlay = false`
  - `SpawnDirector.debugSpawnOnStart = false`
  - `PlayerDeathMonitor.debugReportDeathOnStart = false`

This confirms the intended risk-reward pickup loop works: defeat enemies, move to dropped supplies, recover resources, and continue surviving.

## Deliberately Not Implemented
- Custom pickup mechanics.
- Fixed pickup points.
- Inventory rewrite.
- HUD pickup notifications.
- Pickup balancing beyond initial drop chances.
- Health/ammo drop pooling.

## Known Limitations
- Drop chance balance is provisional.
- Ranged enemy lethality still needs separate balancing.
- Pickup interaction with the player needs a manual Play Mode confirmation.
- If JU TPS pickup prefabs require additional scene-level manager setup, that should be documented and handled separately without editing JU TPS source assets.

## Coursework002 Evidence Supported
- Game mechanics: defeated enemies can generate health/ammo recovery opportunities.
- Content generation: pickups are generated dynamically from enemy defeat rather than placed as fixed resources.
- C# code quality: drop table, dropper, and lifecycle event responsibilities are separated.
- Video demo evidence: supports risk-reward survival gameplay by encouraging players to leave safe positions for supplies.
