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

## Task 4.1R AI Navigation Package
- Package added: `com.unity.ai.navigation`.
- Resolved version: `2.0.12`.
- `NavMeshSurface` availability after retry: available as `Unity.AI.Navigation.NavMeshSurface`.
- `NavMeshModifier` availability after retry: available as `Unity.AI.Navigation.NavMeshModifier`; `NavMeshModifierVolume` is also available.
- Unity Console after package import and refresh: 0 errors and 0 warnings.
- No NavMesh bake was performed in this task.
- No scene, enemy, gameplay, or third-party asset changes were made.
- Next step: create `_Level/NavMesh_Setup` in `LS_Arena_01`, configure a first-pass arena `NavMeshSurface`, and bake/validate the controlled arena area.

## Task 4.2 First-Pass Arena NavMesh
- NavMeshSurface object path: `_Level/NavMesh_Setup`.
- Component: `Unity.AI.Navigation.NavMeshSurface`.
- Collection mode: Volume.
- Object world position: `(5, 2, -157.5)`.
- Surface local center: `(0, 0, 0)`.
- Volume size: `(160, 20, 140)`, covering approximately x `-75` to `85`, y `-8` to `12`, z `-227.5` to `-87.5`.
- Agent type: default humanoid agent (`agentTypeID` 0).
- `ignoreNavMeshAgent`: true.
- `ignoreNavMeshObstacle`: true.
- Bake result: not completed through MCP. The in-memory MCP `execute_code` bake attempt failed because the MCP/Mono command exceeded the Windows filename/extension length limit, so no workaround script was created.
- Generated NavMesh data location: none yet. No NavMeshData asset was generated in `Assets/_LastStand/Scenes`.
- Manual bake instructions: select `_Level/NavMesh_Setup`, inspect `NavMeshSurface`, then click `Bake`.
- Visual/inspection observations: the configured volume matches the intended arena area rather than the full city, but walkable coverage still needs visual confirmation after manual bake.
- Known future tuning needs: curbs, vehicle colliders, dense props, open road exits, and boundary preview objects may need blockers/modifiers after the first enemy movement test.
- Next step: bake manually or through a safe editor workflow, then place one test enemy in Task 4.5.

## Task 4.3 Manual NavMesh Bake Result
- Manual bake completed by user from `_Level/NavMesh_Setup` using the `NavMeshSurface` Inspector.
- NavMeshSurface object path: `_Level/NavMesh_Setup`.
- Generated NavMesh data location: `Assets/_LastStand/Scenes/LS_Arena_01/NavMesh-NavMesh_Setup.asset`.
- Unity also generated: `Assets/_LastStand/Scenes/LS_Arena_01.meta` and `Assets/_LastStand/Scenes/LS_Arena_01/NavMesh-NavMesh_Setup.asset.meta`.
- The `NavMeshSurface` now references the generated NavMeshData asset.
- Collection mode remains Volume.
- Volume remains centered around the intended arena: object position `(5, 2, -157.5)`, surface center `(0, 0, 0)`, size `(160, 20, 140)`.
- Coverage observations: `Player_Start`, `Arena_Center`, melee spawn points, ranged spawn points, extraction point, and pickup points are inside the configured NavMesh volume.
- Visual blue NavMesh coverage still needs manual confirmation in Scene view, especially at curbs, blocked roads, props, vehicles, and road exits.
- Remaining issues to inspect: disconnected islands, holes near props, insufficient coverage at spawn/extraction points, and open routes out of the intended arena.
- Next step: one-enemy movement/combat test in Task 4.5.

## Task 4.5 Single Enemy Validation
- Test source prefab: `Assets/Julhiecio TPS Controller/Demos/Demo Prefabs/AI/Zombie AI.prefab`.
- Scene test object: `_Systems/AI_Test/Enemy_Test_Zombie_Walker`.
- Saved test placement: `(15, 0, -128)`, rotation `(0, 250, 0)`.
- Final saved state: disabled, so the validation object is available for inspection but does not interfere with future scene work.
- Source prefab modification: none.
- NavMeshAgent observation: no standard `UnityEngine.AI.NavMeshAgent` was found on the root object; the zombie uses JU TPS `JU_AI_Zombie` navigation/behaviour components.
- Movement validation: confirmed. The zombie stood on the arena floor and moved using JU TPS navigation/random movement at the saved placement.
- Detection/combat validation: confirmed in a temporary close-range Play Mode pass. The zombie detected `Player_JUTPS`, entered attack targeting, moved close, and reduced player health.
- Console result: 0 errors and 0 warnings after final validation.
- Known tuning need: the default zombie field-of-view distance is `10`, so future Last Stand enemy variants should tune detection distance, speed, health, and attack pressure deliberately under `Assets/_LastStand` instead of editing the JU TPS source prefab.
- Next step: create Last Stand enemy wrappers/variants and then connect enemy death/spawn events to custom wave/stat systems.

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
