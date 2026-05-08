# Enemy Variant Plan

## Purpose
These enemy prefabs are project-owned wrappers created from configured JU TPS AI Attack example instances. They preserve the working JU TPS AI, character, health, damage, inventory, weapon, animation, and ragdoll setup while keeping Last Stand-owned assets under `Assets/_LastStand/Prefabs/Enemies` for later wave spawning.

No custom wave, score, HUD, pickup, or death-reporting systems are implemented in this task.

## Source Examples

| Source scene or prefab path | Hierarchy object name | Base prefab / source | Observed AI components | Inventory / weapon setup | Health / damage setup | Intended Last Stand use | Risks / notes |
|---|---|---|---|---|---|---|---|
| `Assets/Julhiecio TPS Controller/Demos/Demo Scenes/AI/Examples/Attack/AI Attack Example.unity` | `AI Sample Attack Punch` | Configured instance from `AI Sample Attack.prefab` example content | `JU_AI_AttackActionExample`, `JUCharacterController`, `JUInventory`, `JUHealth`, `DamageableBody`, `AdvancedRagdollController` | `ItemToEquipOnStart = -1`; P226 and Katana objects exist in the shared example hierarchy, but no item is equipped at start. | `JUHealth` 100/100, body damage multipliers, hand hitbox/damager components. | Fist-based melee enemy. | Must be validated in `LS_Arena_01`; demo-scene target reference may need runtime assignment later. |
| `Assets/Julhiecio TPS Controller/Demos/Demo Scenes/AI/Examples/Attack/AI Attack Example.unity` | `AI Sample Attack Melee` | Configured instance from `AI Sample Attack.prefab` example content | `JU_AI_AttackActionExample`, `JUCharacterController`, `JUInventory`, `JUHealth`, `DamageableBody`, `AdvancedRagdollController` | `ItemToEquipOnStart = 1`; right-hand Katana child has `MeleeWeapon`, `BoxCollider`, `Rigidbody`, and a child `Damager`. | `JUHealth` 100/100, body damage multipliers, Katana damager plus character hitbox/damager components. | Knife-based melee enemy. | The JU TPS Attack demo implements the Coursework001 knife/melee role as an armed Katana/blade enemy. Keep this configuration unless a functional error appears. |
| `Assets/Julhiecio TPS Controller/Demos/Demo Scenes/AI/Examples/Attack/AI Attack Example.unity` | `AI Sample Attack Gun` | Configured instance from `AI Sample Attack.prefab` example content | `JU_AI_AttackActionExample`, `JUCharacterController`, `JUInventory`, `JUHealth`, `DamageableBody`, `AdvancedRagdollController` | `ItemToEquipOnStart = 0`; right-hand P226 child has `Weapon`, `PreventGunClipping`, and `JU_AI_WeaponSoundSource`. | `JUHealth` 100/100, body damage multipliers, JU TPS weapon damage setup inherited from the example weapon. | Ranged enemy. | Needs one-enemy ranged validation in the arena before adding it to waves. |

## Created Enemy Variants

| Variant prefab path | Source configured object | Intended role | Key configuration evidence | Tuning status | Ready for testing |
|---|---|---|---|---|---|
| `Assets/_LastStand/Prefabs/Enemies/Enemy_FistMelee_JUTPS.prefab` | `AI Sample Attack Punch` | Fist-based melee enemy | Root name is `Enemy_FistMelee_JUTPS`; tag is `Enemy`; `ItemToEquipOnStart = -1`; includes JU TPS hand hitbox/damager setup. | Default JU TPS example values preserved. | Yes, for single-prefab validation. |
| `Assets/_LastStand/Prefabs/Enemies/Enemy_KnifeMelee_JUTPS.prefab` | `AI Sample Attack Melee` | Knife-based melee enemy | Root name is `Enemy_KnifeMelee_JUTPS`; tag is `Enemy`; `ItemToEquipOnStart = 1`; includes right-hand Katana `MeleeWeapon` and damager. | Default JU TPS example values preserved. | Yes, for single-prefab validation. |
| `Assets/_LastStand/Prefabs/Enemies/Enemy_Ranged_JUTPS.prefab` | `AI Sample Attack Gun` | Ranged enemy | Root name is `Enemy_Ranged_JUTPS`; tag is `Enemy`; `ItemToEquipOnStart = 0`; includes right-hand P226 `Weapon`. | Default JU TPS example values preserved. | Yes, for single-prefab validation. |

## Task 5.5 Validation Status

| Variant prefab path | Validation result | Ready for later wave spawning | Notes |
|---|---|---|---|
| `Assets/_LastStand/Prefabs/Enemies/Enemy_FistMelee_JUTPS.prefab` | Pass | Conditionally yes | Movement toward the player and unarmed melee damage were confirmed in `LS_Arena_01`. Later spawning code should assign the JU TPS AI target at runtime. |
| `Assets/_LastStand/Prefabs/Enemies/Enemy_KnifeMelee_JUTPS.prefab` | Pass with notes | Conditionally yes | Close-range blade melee damage was confirmed using the inherited JU TPS AI Attack Katana configuration. This is the project's knife/blade melee implementation. Later spawning code should assign the JU TPS AI target at runtime. |
| `Assets/_LastStand/Prefabs/Enemies/Enemy_Ranged_JUTPS.prefab` | Not tested in Task 5.5 | No | Reserved for Task 5.6 ranged enemy validation. |

## Task 5.6 Validation Status

| Variant prefab path | Validation result | Ready for later wave spawning | Notes |
|---|---|---|---|
| `Assets/_LastStand/Prefabs/Enemies/Enemy_Ranged_JUTPS.prefab` | Pass | Conditionally yes | The ranged variant detected/targeted `Player_JUTPS` after safe scene-instance target assignment, moved on the arena floor, used the inherited P226/gun attack behaviour, and reduced player health from `400` to `0`. Later spawning code should assign the JU TPS AI target at runtime and tune fair ranged spawn distances/line of sight. |

## Task 18 Ranged Balance Update
After playtesting, `Enemy_Ranged_JUTPS.prefab` was tuned for fairness without changing damage:
- `Attack.GunAttack.MaxDistance`: `15` -> `8.5`
- `Attack.GunAttack.Shooting.MaxShotDistance`: `100` -> `30`

This keeps the ranged enemy as distance pressure while reducing long-range attacks that could hit the player before the enemy was clearly visible.

## Task 6 EnemyDefinition Assets

The three validated enemy prefabs now have project-owned `EnemyDefinition` ScriptableObject assets under `Assets/_LastStand/ScriptableObjects/Enemies`:
- `EnemyDefinition_FistMelee.asset` references `Enemy_FistMelee_JUTPS.prefab`.
- `EnemyDefinition_KnifeMelee.asset` references `Enemy_KnifeMelee_JUTPS.prefab`.
- `EnemyDefinition_Ranged.asset` references `Enemy_Ranged_JUTPS.prefab`.

These definitions keep prefab references, scores, minimum wave numbers, spawn weights, and recommended max-alive values in data for future `WaveManager` and `SpawnDirector` work.

## Task 6.5 Runtime Target Binding

All three enemy variants now include `EnemyTargetBinder` on the root prefab. The binder finds `Player_JUTPS` or the `Player` tag at runtime and assigns compatible empty JU TPS AI target fields by reflection. This removes the need for manual scene-instance target assignment when the prefabs are spawned later.

Validated result:
- `Enemy_Ranged_JUTPS.prefab` successfully targeted and damaged `Player_JUTPS` in `LS_Arena_01` without manual target assignment.
- Fist and knife/blade prefabs received the same binder component and should be rechecked quickly when wave spawning begins.

## Task 11 Lifecycle Reporting

All three enemy variants now include `EnemyLifecycleReporter` on the root prefab. The reporter is configured by `WaveManager` after spawning and can notify `WaveManager.NotifyEnemyDefeated` when an enemy is destroyed or when likely JU TPS health/death fields indicate defeat.

Validated result:
- Runtime-spawned fist melee enemies received the reporter and were configured with `_Systems/WaveSystem`.
- Destroy-fallback lifecycle reporting allowed Wave 1 to clear and progress into Wave 2 during validation.
- Real hands-on death detection from normal player combat still needs a later manual pass.

## Planned Enemy Roles

1. Fist-based melee enemy
   - Close-range pressure using unarmed punch attacks.
   - Planned as the simplest early-wave enemy.
   - Later tuning direction: moderate health, clear approach behaviour, low-to-medium damage.

2. Knife-based melee enemy
   - Close-range pressure using a melee weapon.
   - Planned as a stronger melee threat than the fist enemy.
   - Later tuning direction: slightly higher danger, careful attack-distance validation, readable animation.

3. Ranged enemy
   - Distance pressure using a gun/ranged weapon.
   - Planned to force movement, cover use, and target priority decisions.
   - Later tuning direction: lower health, controlled accuracy/fire rate, fair spawn locations.

## Future Integration
- Task 5.5/5.6: validate each enemy variant one at a time in `LS_Arena_01`.
- Later: use `EnemyDefinition` assets as the data source for Last Stand wave/spawn selection.
- Later: add Last Stand runtime target assignment if the JU TPS demo target reference is not suitable outside the example scene.
- Later: create `EnemyDeathReporter`.
- Later: create `EnemyDefinition` ScriptableObjects if tuning data needs to be kept separate from JU TPS prefab assets.
- Later: create `WaveManager` and `SpawnDirector`.
- Later: connect kills, score, waves, and survival timing to `StatsManager` and `HUDPresenter`.

## Coursework002 Evidence Supported
- Game AI: establishes three distinct JU TPS-based enemy behaviours matching Coursework001 terminology.
- Game mechanics: prepares close-range fist, close-range weapon, and ranged pressure types for survival combat.
- Animation/combat: preserves JU TPS attack, weapon, damage, health, and ragdoll foundations.
- Content generation: future wave spawning can select among three project-owned enemy prefabs.
- Level progression: later waves can mix enemy roles to increase difficulty.

## Risks and Mitigation
- JU TPS Attack example instances carry a demo-scene target reference; future testing should confirm how target selection behaves when spawned in `LS_Arena_01`.
- The knife-based enemy intentionally keeps the JU TPS Attack demo's Katana/blade melee configuration; do not replace weapon references unless a functional error appears.
- The ranged enemy needs careful fairness tuning so it does not overwhelm the player from spawn.
- Source JU TPS prefabs and scenes must not be modified.
- Actor skin replacement remains out of scope during core development.
