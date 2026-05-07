# Enemy AI Test Log

## Task 4.5 Single Zombie AI Test
- Source prefab path: `Assets/Julhiecio TPS Controller/Demos/Demo Prefabs/AI/Zombie AI.prefab`.
- Scene object name: disabled JU TPS zombie validation enemy under `_Systems/AI_Test`.
- Test spawn position for saved scene: `(15, 0, -128)`, rotation `(0, 250, 0)`.
- Close-range temporary Play Mode validation position: `(-2, 0, -133)`, used only to confirm detection/attack without changing JU TPS source assets.
- Player object used: `_PlayerSetup/Player_JUTPS`.
- NavMesh status: baked NavMesh data is assigned to `_Level/NavMesh_Setup` at `Assets/_LastStand/Scenes/LS_Arena_01/NavMesh-NavMesh_Setup.asset`.
- Prefab component observations: the root object has `JU_AI_Zombie`, `JUCharacterController`, `JUHealth`, `DamageableBody`, `AdvancedRagdollController`, `JUInventory`, `Rigidbody`, `CapsuleCollider`, animator, audio, and melee hand damagers.
- NavMeshAgent observation: no standard `UnityEngine.AI.NavMeshAgent` was found on the root object; this prefab appears to use JU TPS zombie AI navigation through `JU_AI_Zombie` and its navigation settings.
- Detection setup: `Player_JUTPS` already uses the `Player` tag, which matches the zombie AI target tag list.
- Play Mode test result: pass. The enemy remained stable on the ground, the player/camera/UI stayed stable, and Unity Console stayed free of red errors and warnings after the final validation pass.
- Movement result: pass. At the saved safe-distance placement, the enemy generated movement through JU TPS random/navigation behaviour. In the temporary close-range pass, the enemy moved from near `(-2, 0, -133)` toward `Player_JUTPS`.
- Detection result: pass in close-range validation. `JU_AI_Zombie` reported `CurrentTarget`/attack target references to `Player_JUTPS` when close enough for its default 10 metre field-of-view.
- Attack result: pass. The close-range pass confirmed melee attack behaviour by reducing player health from the default play-mode value to `16`.
- Console result: 0 errors and 0 warnings after the final refresh/clear/check. One intermediate MCP-only error was caused by attempting to move a GameObject during Play Mode; it was cleared and was not a gameplay or compile error.
- Issues found: the default zombie field-of-view distance is `10`, so the saved 15-30 metre test placement does not immediately detect the player without player noise or later tuning.
- Fixes applied: created `_Systems/AI_Test` and instantiated exactly one scene-only test enemy. No JU TPS prefab/source changes were made.
- Final scene state: disabled test enemy remains under `_Systems/AI_Test`, positioned at `(15, 0, -128)`, but inactive so it does not interfere with future setup.
- Recommendation for next step: Task 5 should create Last Stand-owned enemy prefab wrappers/variants and define safe detection/speed/health tuning before building wave spawning.

## Task 5.5 Melee Enemy Variant Validation
- Fist enemy prefab path: `Assets/_LastStand/Prefabs/Enemies/Enemy_FistMelee_JUTPS.prefab`.
- Fist test object path: `_Systems/AI_Test/MeleeVariant_Test/Enemy_Test_FistMelee`.
- Fist spawn position: saved disabled at `(15, 0, -128)` with rotation `(0, 250, 0)`.
- Fist movement result: pass. With the scene-instance `JU_AI_AttackActionExample.Target` assigned to `_PlayerSetup/Player_JUTPS`, the enemy moved from the test spawn toward the player.
- Fist attack result: pass. The enemy reached close range and performed unarmed/fist melee attack behaviour.
- Fist effect on player health: pass. Player health dropped from `400` to `0` during the close-range validation.
- Knife enemy prefab path: `Assets/_LastStand/Prefabs/Enemies/Enemy_KnifeMelee_JUTPS.prefab`.
- Knife test object path: `_Systems/AI_Test/MeleeVariant_Test/Enemy_Test_KnifeMelee`.
- Knife spawn position: saved disabled at `(-8, 0, -134)` with rotation `(0, 220, 0)` after close-range validation.
- Knife/blade weapon used: the configured JU TPS AI Attack demo right-hand `Katana` child with `MeleeWeapon` and `Damager` setup.
- Weapon mapping note: Coursework001 describes a knife-based melee enemy. In the JU TPS implementation, that design role is represented by the AI Attack demo's configured Katana/blade melee enemy.
- Katana status: Katana remains intentionally unchanged as the armed close-range blade/knife melee implementation. Do not replace weapon references unless a functional error appears.
- Knife movement result: pass with note. The enemy was stable and moved/rotated, but the far test position did not reliably close distance within the observation window. A close-range validation position near `Player_JUTPS` confirmed approach and attack behaviour.
- Knife attack result: pass. The enemy used the blade melee setup at close range.
- Knife effect on player health: pass. Player health dropped from `400` to `0` during the close-range validation.
- Console result: 0 errors and 0 warnings after final clear/recheck.
- Final scene state: both `Enemy_Test_FistMelee` and `Enemy_Test_KnifeMelee` are disabled under `_Systems/AI_Test/MeleeVariant_Test`; no active melee test enemies are left for scene startup.
- Issues and fixes: both project-owned prefabs inherited a demo-scene target reference that becomes `null` in `LS_Arena_01`; safe scene-instance target assignment to `Player_JUTPS` was applied to the test instances. A later spawn/integration system should assign targets at runtime rather than relying on demo-scene references.
- Recommendation for Task 5.6: validate `Enemy_Ranged_JUTPS.prefab` separately, confirm P226/ranged behaviour, and again verify runtime target assignment needs.
