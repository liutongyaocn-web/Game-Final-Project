# Enemy Target Binding Plan

## Purpose
Last Stand enemy prefabs are based on JU TPS AI Attack example objects. Those examples use a scene-specific `Target` reference, which becomes `null` when the project-owned prefabs are used in `LS_Arena_01` or spawned later by a custom system.

`EnemyTargetBinder` provides a small project-owned runtime bridge so spawned enemies can find and target `Player_JUTPS` without editing JU TPS source scripts or hard-coding JU TPS API references.

## Why Runtime Target Binding Is Needed
Task 5.5 and Task 5.6 showed that the Attack example wrappers work once their target is assigned to `Player_JUTPS`. Future `WaveManager` and `SpawnDirector` systems will instantiate enemies at runtime, so the prefabs need a repeatable binding step instead of manual scene-instance setup.

## Relationship To Task 5.6
Task 5.6 validated `Enemy_Ranged_JUTPS` by manually assigning its JU TPS AI `Target` field to `Player_JUTPS`. Task 6.5 repeats that validation without manual assignment. Before Play Mode, the ranged test instance had `Target = null`; during Play Mode, `EnemyTargetBinder` bound it to `Player_JUTPS`, and the enemy attacked successfully.

## EnemyTargetBinder Script Summary
`Assets/_LastStand/Scripts/AI/EnemyTargetBinder.cs`:
- searches for an explicit target, `Player_JUTPS`, or an object tagged `Player`
- searches this enemy and child `MonoBehaviour` components
- prefers component type names containing `JU_AI`, `JUAI`, or `AI`
- looks for target-like members named `Target`, `target`, `CurrentTarget`, `currentTarget`, `TargetToAttack`, or `targetToAttack`
- assigns compatible empty fields/properties using reflection
- supports `GameObject`, `Transform`, and `Component` target member types
- avoids logging unless `logBindingResult` is enabled
- exposes `BindTarget(GameObject targetOverride = null)` for future spawn code

The script does not reference JU TPS classes directly.

## Target Lookup Method
1. Use `explicitTarget` if assigned.
2. Find a GameObject named `Player_JUTPS`.
3. Find a GameObject tagged `Player`, if the tag exists.

## JU TPS Integration Approach
The validated JU TPS AI Attack wrappers expose a serialized `Target` field on `JU_AI_AttackActionExample`. The field expects a Unity object reference compatible with the player GameObject. `EnemyTargetBinder` assigns this field at runtime when it is empty.

Observed setup:
- component type: `JU.CharacterSystem.AI.Examples.JU_AI_AttackActionExample`
- target member: `Target`
- member type: compatible with `GameObject`
- additional runtime target data, such as target collider, character, health, distance, and path information, is then populated by JU TPS during Play Mode

## Prefabs Updated
- `Assets/_LastStand/Prefabs/Enemies/Enemy_FistMelee_JUTPS.prefab`
- `Assets/_LastStand/Prefabs/Enemies/Enemy_KnifeMelee_JUTPS.prefab`
- `Assets/_LastStand/Prefabs/Enemies/Enemy_Ranged_JUTPS.prefab`

Each root prefab now has `EnemyTargetBinder` configured with:
- `playerObjectName = Player_JUTPS`
- `playerTag = Player`
- `bindOnStart = true`
- `includeChildren = true`
- `logBindingResult = false`

## Validation Result
- Scene: `Assets/_LastStand/Scenes/LS_Arena_01.unity`
- Test object: `_Systems/AI_Test/TargetBinder_Test/Enemy_Test_TargetBinder_Ranged`
- Prefab tested: `Enemy_Ranged_JUTPS.prefab`
- Manual target assignment: not used
- Before Play Mode: `JU_AI_AttackActionExample.Target` was `null`
- During Play Mode: `Target` was bound to `Player_JUTPS`
- Result: ranged enemy moved, targeted the player, fired/attacked with the P226/gun setup, and reduced player health from `400` to `0`
- Console: 0 errors and 0 warnings after final check
- Final scene state: test enemy disabled

## Risks And Limitations
- Reflection avoids a compile-time JU TPS dependency, but field names must remain compatible with the JU TPS components used by the project.
- The binder intentionally fills empty target members and does not replace already assigned manual references.
- Ranged enemy balance remains a later tuning task because validation damage is high.
- Future spawning code should call `BindTarget` after instantiation if enemies are spawned inactive first.

## Future SpawnDirector/WaveManager Support
The binder prepares the enemy prefabs for runtime spawning. A future `SpawnDirector` can instantiate an enemy prefab from an `EnemyDefinition`, then rely on `EnemyTargetBinder` or call `BindTarget(player)` directly before enabling combat behaviour.
