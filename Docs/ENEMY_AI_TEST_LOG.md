# Enemy AI Test Log

## Task 4.5 Single Zombie AI Test
- Source prefab path: `Assets/Julhiecio TPS Controller/Demos/Demo Prefabs/AI/Zombie AI.prefab`.
- Scene object name: `_Systems/AI_Test/Enemy_Test_Zombie_Walker`.
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
- Final scene state: disabled test enemy. `Enemy_Test_Zombie_Walker` remains in the scene under `_Systems/AI_Test`, positioned at `(15, 0, -128)`, but inactive so it does not interfere with future setup.
- Recommendation for next step: Task 5 should create Last Stand-owned enemy prefab wrappers/variants and define safe detection/speed/health tuning before building wave spawning.
