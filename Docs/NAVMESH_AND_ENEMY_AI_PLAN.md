# NavMesh and Enemy AI Plan

## Purpose
This step prepares navigation and enemy AI support for Last Stand without implementing the wave system yet. JU TPS remains the preferred foundation for enemy movement, detection, attack, damage, health, animation, and ragdoll behaviour. Custom Last Stand code will be added later for spawning, waves, scoring, death reporting, and HUD/statistics integration.

## NavMesh Status
- `NavMeshSurface` availability: not available in the current project assemblies.
- `NavMeshModifier` availability: not available in the current project assemblies.
- Package status: `Packages/manifest.json` includes built-in `com.unity.modules.ai`, but does not include `com.unity.ai.navigation`.
- Existing scene objects: no `NavMeshSurface` or `NavMeshModifier` objects were found in `LS_Arena_01`.
- Existing navigation components: `Player_JUTPS` currently has a `NavMeshObstacle`, inherited from the JU TPS player prefab.
- `NavMesh_Setup` object: not created, because the required `NavMeshSurface` component is not available.
- First-pass bake: not completed. Manual/safe package setup is required first.
- Planned arena bounds: approximately x `-70` to `80`, z `-220` to `-95`.

Known concerns for future NavMesh setup:
- Curbs and road height changes may fragment walkable areas.
- Blocked streets, dense props, and vehicle colliders may create narrow or disconnected paths.
- Open roads need future blocker objects, barricades, or invisible walls to keep the survival arena controlled.
- Boundary preview objects are planning markers only and should not be treated as final blockers.
- The copied city scene is larger than the intended arena, so NavMesh baking should be limited to the controlled combat area once AI Navigation is available.

Safe next step:
- Add or enable Unity AI Navigation / `com.unity.ai.navigation` through a deliberate package-management task, then create `_Level/NavMesh_Setup` with a `NavMeshSurface` and bake a first-pass arena NavMesh.

## Enemy Prefab Candidates

| Candidate prefab path | Description / likely use | AI type or observed components | Melee/ranged suitability | Recommended Last Stand role | Risks/manual setup |
|---|---|---|---|---|---|
| `Assets/Julhiecio TPS Controller/Demos/Demo Prefabs/AI/Zombie AI.prefab` | JU TPS zombie-style AI character with humanoid body, ragdoll, health, hitboxes, and alert indicator. | Root includes `JUCharacterController`, `JUHealth`, `DamageableBody`, `AdvancedRagdollController`, `JUInventory`, and `JU_AI_Zombie`. | Best melee candidate. Includes hand hitboxes/damagers. | Walker / slow melee infected; possible Runner after speed/health tuning later. | Needs NavMesh/navigation validation in `LS_Arena_01`; speed/health tuning should be done in safe Last Stand variants later. |
| `Assets/Julhiecio TPS Controller/Demos/Demo Prefabs/AI/Patrol AI.prefab` | Armed patrol-style JU TPS AI character. | Root includes `JUCharacterController`, `JUHealth`, `DamageableBody`, `AdvancedRagdollController`, `JUInventory`, `JU_AI_PatrolCharacter`, and `JUInteractionSystem`; has nested `P226` weapon. | Strong ranged candidate. | Ranged infected or armed hostile stand-in. | May need patrol/target setup, weapon behaviour validation, faction/tag setup, and careful balancing. |
| `Assets/Julhiecio TPS Controller/Demos/Demo Scenes/AI/Examples/Attack/AI Sample Attack.prefab` | JU TPS example AI for attack behaviour. | Root includes `JUCharacterController`, `JUHealth`, `DamageableBody`, `AdvancedRagdollController`, `JUInventory`, and `JU_AI_AttackActionExample`; includes nested `P226` and `Katana`. | Mixed melee/ranged example. | Reference candidate for attack behaviour, not first production enemy. | Example prefab may depend on demo-scene assumptions; use for learning before wrapping. |
| `Assets/Julhiecio TPS Controller/Demos/Demo Scenes/AI/Examples/Escape/AI Sample Escape.prefab` | JU TPS example AI for escape behaviour. | Root includes `JUCharacterController`, `JUHealth`, `DamageableBody`, `AdvancedRagdollController`, `JUInventory`, `JU_AI_EscapeActionExample`, and `NavMeshObstacle`; includes nested `P226` and `Katana`. | Mixed capability but behaviour is escape-focused. | Reference only; not recommended for main zombie wave roles. | Behaviour goal does not match survival enemy pressure; may require demo-scene setup. |
| `Assets/Julhiecio TPS Controller/Demos/Demo Prefabs/AI/AI Alert Indicator.prefab` | Visual alert indicator used by AI prefabs. | Contains JU TPS AI alert visual component. | Not an enemy. | Supporting UI/VFX for AI awareness. | Use only through existing JU TPS AI prefab references unless a later task needs custom alert visuals. |

## Recommended Enemy Roles
1. Walker / slow melee infected
   - Source candidate: `Zombie AI.prefab`.
   - Low speed.
   - Medium health.
   - Close-range pressure.

2. Runner / fast melee infected
   - Source candidate: `Zombie AI.prefab` as a later Last Stand variant.
   - Higher speed.
   - Lower health.
   - Close-range pressure and flanking.

3. Ranged infected
   - Source candidate: `Patrol AI.prefab`.
   - Lower health.
   - Ranged attack or weapon pressure.
   - Forces the player to use cover and movement.

## Future Integration Plan
- Task 4.5: after AI Navigation availability is resolved, place one test enemy and validate NavMesh movement/combat in `LS_Arena_01`.
- Later: create Last Stand enemy prefab variants/wrappers under `Assets/_LastStand/Prefabs/Enemies`.
- Later: create `EnemyDeathReporter` to translate JU TPS enemy death into Last Stand kill/score events.
- Later: create `WaveManager` and `SpawnDirector`.
- Later: connect kills, score, wave state, and survival timing to `StatsManager` and `HUDPresenter`.

## Coursework002 Evidence Supported
- Game AI: identifies JU TPS AI candidates and planned roles for melee and ranged enemy behaviours.
- Game mechanics: prepares navigation and enemy pressure needed for survival combat.
- Content generation: future wave/spawn system will use weighted enemy selection and spawn points.
- Level progression: enemy roles will scale across waves 1-5.
- C# quality: custom integration scripts are planned as small wrappers around JU TPS rather than a duplicated AI stack.

## Risks and Mitigation
- JU TPS AI may require specific scene setup: validate one enemy in Task 4.5 before creating many variants.
- NavMesh may need manual package setup and bake: do not create workaround navigation systems until AI Navigation is available.
- Full city scene may be too large: keep the playable arena bounded and bake only the controlled area later.
- Enemy prefabs may require manual Inspector setup: document source prefab paths and required fields during the first enemy test.
- Avoid actor skin replacement: use JU TPS-compatible characters first to protect stability.
