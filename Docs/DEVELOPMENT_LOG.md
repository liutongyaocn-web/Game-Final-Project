# Development Log

## 2026-05-06 17:57 +01:00 - Project Foundation
- Confirmed Unity project name: Game Final Project.
- Confirmed Unity version from ProjectSettings/ProjectVersion.txt: 6000.4.4f1.
- Confirmed imported assets: JU TPS, JUTPS Addons, Synty POLYGON Apocalypse/Generic, Unity MCP package.
- Confirmed active scene: Assets/Scenes/SampleScene.unity.
- Confirmed build settings currently reference JU TPS demo scenes.
- Created custom working area under Assets/_LastStand.
- Created Coursework002 documentation foundation under Docs.
- No third-party source assets or demo prefabs were modified.

## 2026-05-06 18:02 +01:00 - Clean Scaffold Baseline
- Confirmed current branch: main.
- Confirmed Unity .gitignore already exists and covers generated Unity folders/files.
- Created root README.md describing Last Stand, coursework context, Unity version, asset strategy, planned custom systems, documentation, and Git workflow.
- Updated scaffold documentation only; no gameplay implementation was started.
- Current active scene remains Assets/Scenes/SampleScene.unity.
- Build settings still reference JU TPS demo scenes and will need replacing when the final Last Stand scene is ready.

## Next Planned Task
- Scene selection and arena planning for Assets/_LastStand/Scenes/LS_Arena_01.unity.

## 2026-05-06 18:16 +01:00 - Task 0D JU TPS Integration Guardrails
- Created `Docs/JUTPS_INTEGRATION_GUIDE.md` before scene creation or gameplay implementation.
- Documented that JU TPS is the gameplay foundation for controller, camera, weapon handling, aiming, shooting, reload, inventory, health/damage, animation, ragdoll, and AI foundations.
- Documented that POLYGON Apocalypse remains environment/map/prop content during core development.
- Documented that custom Coursework002 work belongs under `Assets/_LastStand` and should extend or integrate with JU TPS rather than replacing it.
- Confirmed this task is documentation-only: no gameplay systems, custom scene, Build Settings change, or third-party source asset edits were made.

## Next Planned Task
- Scene selection and arena planning for `Assets/_LastStand/Scenes/LS_Arena_01.unity`, following the JU TPS integration guide.

## 2026-05-06 18:16 +01:00 - Task 1 POLYGON Apocalypse Scene Audit
- Found POLYGON Apocalypse scenes: `Demo_Building_Interior_Dressing.unity`, `Demo_Bunker.unity`, `Demo_City_Standard_RenderPipeline.unity`, `Demo_City_Universal_RenderPipeline.unity`, and `Overview.unity`.
- Found POLYGON Generic overview scene: `Assets/Synty/PolygonGeneric/Scenes/Overview.unity`.
- Inspected `Demo_City_Universal_RenderPipeline.unity` through file metadata and Unity MCP hierarchy. It contains grouped `Buildings`, `Ground`, `Terrain`, `Props`, `Vehicles`, `Vehicles_Wrecked`, `Trees`, `DeadBodies`, `Weapons`, lighting, camera, and volume objects.
- Recommended `Assets/Synty/PolygonApocalypse/Scenes/Demo_City_Universal_RenderPipeline.unity` as the source for a cropped quarantine checkpoint / military evacuation roadblock area.
- No gameplay code was implemented.
- No Build Settings changes were made.
- No third-party scene was saved or intentionally modified. The demo scene was opened additively for inspection only; Unity marked it dirty in-editor, so it must be closed without saving.
- Final Console read showed 0 red errors and 1 Unity MCP transport warning. Unity also generated local lighting/shader settings files during inspection; these were deliberately left uncommitted.

## Next Planned Task
- Task 2: create `Assets/_LastStand/Scenes/LS_Arena_01.unity` as a new custom scene using the selected arena plan, with only custom scene changes saved under `Assets/_LastStand/Scenes`.

## 2026-05-06 19:51 +01:00 - Task 2 Initial LS_Arena_01 Scene Creation
- Created `Assets/_LastStand/Scenes/LS_Arena_01.unity`.
- Source scene path: `Assets/Synty/PolygonApocalypse/Scenes/Demo_City_Universal_RenderPipeline.unity`.
- Workflow used a file copy into `_LastStand` followed by edits only to the copied custom scene.
- Full demo content was copied as the first pass; no crop or large deletion was made because the exact combat area still needs visual/manual verification.
- Created top-level custom hierarchy: `_Systems`, `_PlayerSetup`, `_CameraSetup`, `_UISetup`, `_Level`, `_SpawnPoints`, `_PickupPoints`, `_ExtractionObjective`, `_Lighting`, `_Audio`, and `_DebugMarkers`.
- Created player, camera, arena centre, extraction, melee spawn, ranged spawn, ammo pickup, health pickup, boundary, and scene note markers.
- NavMesh observation: no `NavMeshSurface` or `NavMeshModifier` objects were found; NavMesh setup/bake is needed later.
- Camera/light observation: copied demo contains one `Demo/Main Camera` and one `Demo/Directional Light`. Final JU TPS camera setup is deferred.
- No gameplay scripts were created or modified.
- No player, weapons, enemies, waves, AI, or Build Settings changes were made.
- The original Synty demo scene was not saved or staged.

## Next Planned Task
- Task 3: add the JU TPS-compatible player/camera/UI foundation to the custom scene, then validate Console and basic scene loading before any wave logic.

## 2026-05-06 20:00 +01:00 - Task 2.5 Arena Boundary And Marker Refinement
- Opened and refined only `Assets/_LastStand/Scenes/LS_Arena_01.unity`.
- Confirmed the arena should be a medium-sized checkpoint / evacuation roadblock area, not the entire copied city.
- Confirmed most existing marker positions and adjusted `Player_Start` rotation to face into the combat space.
- Created `_DebugMarkers/Arena_Boundary_Preview`.
- Created boundary preview cube placeholders: `BoundaryPreview_North`, `BoundaryPreview_South`, `BoundaryPreview_East`, and `BoundaryPreview_West`.
- Created `_DebugMarkers/Arena_Planning_Notes`.
- Created planning note markers: `Note_Player_start_safe_but_exposed`, `Note_Extraction_after_wave_5`, `Note_Spawn_points_outside_player_view`, and `Note_Pickups_risk_reward_positions`.
- No large environment crop or deletion was made; far-away copied demo content remains for later visual review after player/camera validation.
- NavMesh concerns recorded: road/curb transitions, blocked streets, dense props, vehicle colliders, and boundary preview objects may affect walkability later.
- No gameplay scripts were added.
- No JU TPS player/camera/UI was added.
- No Build Settings changes were made.
- No third-party source scene was modified.

## Next Planned Task
- Task 3: add JU TPS-compatible player, camera, and UI foundation to `LS_Arena_01`, then validate basic player/camera scene readiness before implementing waves.

## 2026-05-06 18:12 +01:00 - Task 0C Unity Validation And Project Configuration Baseline
- Confirmed current branch: main.
- Refreshed Unity assets through MCP; Unity remained idle and not compiling.
- Unity Console validation result: 0 errors and 0 warnings.
- Inspected Packages and ProjectSettings diffs for local absolute paths, credentials, tokens, and user-specific machine data; none were found.
- Prepared safe project configuration baseline candidates from Packages/manifest.json, Packages/packages-lock.json, and standard Unity ProjectSettings files.
- Included ProjectSettings/Packages/com.unity.testtools.codecoverage/Settings.json as empty standard Unity package configuration.
- Deliberately left third-party source asset folders uncommitted: Assets/Julhiecio TPS Controller, Assets/JUTPS Addons, and Assets/Synty.
- Deliberately left generated/local folders uncommitted or ignored: Library, Temp, Logs, UserSettings, .vs, build outputs, and caches.
- Active scene remains Assets/Scenes/SampleScene.unity.
- Build settings still reference JU TPS demo scenes; no Build Settings changes were made in this task.

## Next Planned Task
- Scene selection and arena planning for Assets/_LastStand/Scenes/LS_Arena_01.unity.

## 2026-05-06 20:23 +01:00 - Task 3 JU TPS Player Camera UI Integration
- Integrated JU TPS gameplay foundation into Assets/_LastStand/Scenes/LS_Arena_01.unity.
- Player prefab used: Assets/Julhiecio TPS Controller/Demos/Demo Prefabs/Character Prefabs/TPS Character.prefab.
- Camera prefab used: Assets/Julhiecio TPS Controller/Prefabs/Game/Camera Prefabs/ThirdPerson Camera Controller.prefab.
- Default UI prefab used: Assets/Julhiecio TPS Controller/Prefabs/Game/UI Interfaces/JUTPS Default User Interface.prefab.
- Scene hierarchy updated under _PlayerSetup, _CameraSetup, and _UISetup with Player_JUTPS, CameraController_JUTPS, and UI_JUTPS_Default.
- Player placement: approximately (-10, 0, -136) with Y rotation 80, matching the arena start area and facing into the combat space.
- Demo camera handling: copied Demo/Main Camera is disabled in LS_Arena_01 only; it was not deleted and the original Synty scene was not modified.
- Camera setup: CameraController_JUTPS has obvious follow/target fields assigned to Player_JUTPS where Unity MCP allowed it. The player-side MyPivotCamera field may still need manual Inspector confirmation because it expects a JU TPS camera component reference.
- Added Note_JUTPS_player_camera_UI_integrated under _DebugMarkers/Arena_Planning_Notes.
- Play Mode first pass showed the player/camera/UI scene setup could enter Play Mode, but surfaced copied-scene BoxCollider does not support negative scale or size errors from mirrored POLYGON ground pieces.
- Applied a targeted scene-only cleanup by normalising negative local scale overrides in the copied custom LS_Arena_01 scene. No Synty source assets or prefabs were modified.
- No custom gameplay systems, wave logic, pickups, enemy AI, actor skin replacement, or Build Settings changes were implemented.

## Next Planned Task
- Task 4: validate player movement/camera manually in the arena, then prepare NavMesh and JU TPS enemy integration planning before adding wave logic.

### Task 3 Validation Update
- After refreshing Unity and re-running Play Mode, the Console showed 0 red errors.
- The copied-scene BoxCollider negative-scale errors did not return after normalising the custom scene copy.
- A repeated two-audio-listener warning remains while SampleScene is still loaded additively and dirty in the editor. SampleScene was not saved, modified intentionally, or staged.

## 2026-05-06 22:22 +01:00 - Task 3.5 JU TPS Foundation Validation
- Validated Assets/_LastStand/Scenes/LS_Arena_01.unity with only the custom arena scene loaded.
- Confirmed Player_JUTPS, CameraController_JUTPS, and UI_JUTPS_Default are active in the scene.
- Confirmed copied Demo/Main Camera remains inactive in the custom scene.
- Confirmed there is one active Unity camera in LS_Arena_01, so the earlier two-audio-listener warning is gone when SampleScene is closed.
- Entered Play Mode for a short stability check; Player_JUTPS remained near the intended start position and did not fall through the map.
- Unity Console after Play Mode: 0 errors and 0 warnings.
- No scene-level fixes were required in this task.
- No gameplay systems, wave logic, pickups, enemy AI, custom combat scripts, actor skin replacement, or Build Settings changes were implemented.

## Next Planned Task
- Task 4: perform/record hands-on input validation for movement, mouse look, aim, shoot/reload, stance actions, then begin NavMesh and JU TPS enemy integration planning.

## 2026-05-06 22:45 +01:00 - Task 3.6 Manual JU TPS Input Validation Record
- User manually validated the core JU TPS player, camera, and UI behaviour in Unity with only LS_Arena_01 loaded.
- Confirmed the player remains on the ground, the camera follows the player, mouse look controls the view, and the JU TPS default UI is visible.
- Confirmed Unity Console has no red errors and the earlier two-audio-listener warning disappeared after closing SampleScene.
- The JU TPS foundation is stable enough to proceed toward NavMesh and enemy setup planning.
- Logged later polish tasks: camera position feels slightly low, and the JU TPS default UI should be extended with a Last Stand-specific HUD layer.
- No scene files, code, gameplay systems, ProjectSettings, Build Settings, or third-party assets were changed in this task.

## Next Planned Task
- Task 4: begin NavMesh and enemy setup planning around the manually validated JU TPS player/camera/UI foundation.

## 2026-05-06 22:59 +01:00 - Task 4 NavMesh And Enemy Prefab Audit
- Inspected `LS_Arena_01` for NavMesh-related scene setup.
- `NavMeshSurface` and `NavMeshModifier` are not available in the current project assemblies.
- `Packages/manifest.json` includes built-in `com.unity.modules.ai`, but does not include `com.unity.ai.navigation`.
- No `NavMeshSurface` or `NavMeshModifier` scene objects were found. `Player_JUTPS` has a JU TPS inherited `NavMeshObstacle`.
- Did not create `_Level/NavMesh_Setup` because the required `NavMeshSurface` component is unavailable.
- Did not bake NavMesh in this task; safe package setup for Unity AI Navigation is required first.
- Audited JU TPS enemy prefab candidates: `Zombie AI.prefab`, `Patrol AI.prefab`, `AI Sample Attack.prefab`, `AI Sample Escape.prefab`, and `AI Alert Indicator.prefab`.
- Initially recommended `Zombie AI.prefab` and `Patrol AI.prefab` as broad melee/ranged candidates. Later Task 5 clarification refined this to the configured AI Attack example instances for fist-based melee, knife-based melee, and ranged enemy wrappers.
- No enemy variants/wrappers were created because NavMesh availability and first enemy behaviour validation should happen first.
- No gameplay systems, wave logic, spawning, scoring, pickups, custom HUD, Build Settings, actor skin replacement, or third-party source edits were made.

## Next Planned Task
- Task 4.5: resolve AI Navigation/NavMeshSurface availability, then place one safe test enemy in `LS_Arena_01` and validate movement/combat before creating Last Stand enemy variants.

## 2026-05-06 23:23 +01:00 - Task 4.1R Unity AI Navigation Package Retry
- Retried adding Unity's official AI Navigation package after the Windows application-control issue was addressed.
- Added `com.unity.ai.navigation` through Unity Package Manager.
- Unity resolved package version `2.0.12`.
- Verified `NavMeshSurface` is available as `Unity.AI.Navigation.NavMeshSurface`.
- Verified `NavMeshModifier` is available as `Unity.AI.Navigation.NavMeshModifier`.
- Unity Console after import/refresh: 0 errors and 0 warnings.
- No `LS_Arena_01` scene changes were made.
- No NavMesh bake, enemy placement, wave logic, spawning, scoring, pickups, custom HUD, Build Settings, actor skin replacement, or third-party source edits were made.

## Next Planned Task
- Task 4.2: create `_Level/NavMesh_Setup` in `LS_Arena_01`, configure `NavMeshSurface`, and bake a first-pass arena NavMesh.

## 2026-05-06 23:30 +01:00 - Task 4.2 First-Pass Arena NavMesh Setup
- Created `_Level/NavMesh_Setup` in `Assets/_LastStand/Scenes/LS_Arena_01.unity`.
- Added `Unity.AI.Navigation.NavMeshSurface`.
- Configured the surface to collect objects by Volume.
- Set the setup object position to `(5, 2, -157.5)` and the surface volume size to `(160, 20, 140)`, covering the intended checkpoint/evacuation roadblock arena rather than the full copied city.
- Kept default humanoid agent settings and did not tune radius/height yet.
- Attempted an MCP-based bake, but the MCP `execute_code` tool failed with a Windows filename/extension length error before running the bake code.
- No NavMeshData asset was generated. Manual bake is required: select `_Level/NavMesh_Setup`, then use the `NavMeshSurface` Bake button in the Inspector.
- Unity Console was cleared after the MCP tool error and returned 0 errors and 0 warnings.
- No enemies were placed and no gameplay systems, wave logic, spawning, scoring, pickups, custom HUD, Build Settings, actor skin replacement, or third-party source edits were made.

## Next Planned Task
- Task 4.5: manually bake/inspect the NavMesh if needed, then place one test enemy and validate movement/combat before creating enemy variants.

## 2026-05-06 23:39 +01:00 - Task 4.3 Manual NavMesh Bake Record
- User manually baked the `NavMeshSurface` on `_Level/NavMesh_Setup`.
- The scene now references generated NavMesh data at `Assets/_LastStand/Scenes/LS_Arena_01/NavMesh-NavMesh_Setup.asset`.
- Confirmed the `NavMeshSurface` still uses Volume collection and the intended arena volume size `(160, 20, 140)`.
- Confirmed key planning markers are inside the configured volume: `Player_Start`, `Arena_Center`, melee/ranged spawn points, `Extraction_Point`, and pickup points.
- Visual walkable coverage still needs manual Scene view confirmation before trusting enemy movement.
- No enemies were placed.
- No gameplay systems, wave logic, spawning, scoring, pickups, custom HUD, Build Settings, actor skin replacement, or third-party source edits were made.

## Next Planned Task
- Task 4.5: place one safe JU TPS enemy test instance and validate NavMesh movement/combat using the baked arena NavMesh.

## 2026-05-07 00:10 +01:00 - Task 4.5 Single JU TPS Zombie AI Test
- Placed exactly one JU TPS Zombie AI test instance in `LS_Arena_01`.
- Source prefab: `Assets/Julhiecio TPS Controller/Demos/Demo Prefabs/AI/Zombie AI.prefab`.
- Scene object: a disabled JU TPS zombie validation enemy under `_Systems/AI_Test`.
- Confirmed the prefab uses JU TPS AI/character/health/damage/ragdoll components and does not expose a standard root `NavMeshAgent`.
- Confirmed `Player_JUTPS` already has the `Player` tag expected by the zombie target list.
- Play Mode validation showed the zombie remains stable, uses JU TPS navigation movement, detects the player at close range, and can apply melee damage.
- Final scene state keeps the test enemy disabled at `(15, 0, -128)` so it remains available for inspection without disrupting future development.
- Unity Console after final refresh/check: 0 errors and 0 warnings.
- No wave logic, spawning systems, scoring, pickups, custom HUD, Build Settings changes, actor skin replacement, custom scripts, or third-party source edits were made.

## Next Planned Task
- Task 5: create safe Last Stand enemy wrappers/variants and plan integration hooks for later wave spawning, enemy death reporting, score/kills, and HUD statistics.

## 2026-05-07 00:50 +01:00 - Task 5 Last Stand Enemy Variant Wrappers
- Inspected the JU TPS AI Attack example at `Assets/Julhiecio TPS Controller/Demos/Demo Scenes/AI/Examples/Attack/AI Attack Example.unity`.
- Found the configured enemy instances matching Coursework001 terminology: `AI Sample Attack Punch`, `AI Sample Attack Melee`, and `AI Sample Attack Gun`.
- Created project-owned wrappers under `Assets/_LastStand/Prefabs/Enemies`:
  - `Enemy_FistMelee_JUTPS.prefab` from `AI Sample Attack Punch`, with `ItemToEquipOnStart = -1`.
  - `Enemy_KnifeMelee_JUTPS.prefab` from `AI Sample Attack Melee`, with `ItemToEquipOnStart = 1` and a right-hand Katana `MeleeWeapon`.
  - `Enemy_Ranged_JUTPS.prefab` from `AI Sample Attack Gun`, with `ItemToEquipOnStart = 0` and a right-hand P226 `Weapon`.
- No disabled preview instances were added to `LS_Arena_01`.
- No wave, spawn, stat, pickup, custom HUD, enemy death reporting, Build Settings, actor skin replacement, or third-party source edits were implemented.

## Next Planned Task
- Task 5.5: validate the fist-based melee, knife-based melee, and ranged enemy wrappers one at a time in `LS_Arena_01`.

## 2026-05-07 11:49 +01:00 - Task 5.5 Melee Enemy Variant Validation
- Validated `Enemy_FistMelee_JUTPS.prefab` in `LS_Arena_01`.
- Fist enemy moved toward `Player_JUTPS` and applied unarmed melee damage, reducing player health from `400` to `0` during Play Mode validation.
- Validated `Enemy_KnifeMelee_JUTPS.prefab` in `LS_Arena_01`.
- Knife enemy kept the JU TPS AI Attack demo's configured Katana blade setup. This represents the Coursework001 knife-based melee role as an armed close-range blade enemy, and weapon references should not be changed unless a functional error appears.
- Knife enemy close-range validation confirmed blade melee damage, reducing player health from `400` to `0`.
- Safe scene-instance fix applied: the Attack example AI `Target` field was assigned to `Player_JUTPS` on test instances because the demo-scene target reference becomes `null` in `LS_Arena_01`.
- Final saved scene state keeps both melee test enemies disabled under `_Systems/AI_Test/MeleeVariant_Test`.
- No ranged enemy test, wave/spawn/stat systems, pickups, custom HUD, EnemyDeathReporter, Build Settings changes, actor skin replacement, or third-party source edits were implemented.

## Next Planned Task
- Task 5.6: validate `Enemy_Ranged_JUTPS.prefab` separately and confirm ranged/P226 behaviour.

## 2026-05-07 12:20 +01:00 - Task 5.6 Ranged Enemy Variant Validation
- Validated `Enemy_Ranged_JUTPS.prefab` in `LS_Arena_01`.
- Test object: `_Systems/AI_Test/RangedVariant_Test/Enemy_Test_Ranged`.
- The ranged enemy uses the JU TPS AI Attack demo's configured P226/gun setup.
- Safe scene-instance fix applied: `JU_AI_AttackActionExample.Target` was assigned to `_PlayerSetup/Player_JUTPS`, matching the target-assignment requirement found during melee validation.
- Initial placement at `(15, 0, -128)` confirmed movement on the arena floor toward the player.
- Clearer line-of-sight validation at `(-2, 0, -132)` confirmed ranged/gun damage, reducing player health from `400` to `0`.
- Final saved scene state keeps `Enemy_Test_Ranged` disabled under `_Systems/AI_Test/RangedVariant_Test`.
- Unity Console after final check: 0 errors and 0 warnings.
- No wave/spawn/stat systems, pickups, custom HUD, EnemyDeathReporter, Build Settings changes, actor skin replacement, ranged prefab source edits, or third-party source edits were implemented.

## Next Planned Task
- Task 6: design and implement the first Last Stand-owned runtime integration layer, likely starting with safe enemy target assignment and enemy definitions before WaveManager/SpawnDirector.

## 2026-05-07 14:00 +01:00 - Task 6 EnemyDefinition Data Assets
- Created `Assets/_LastStand/Scripts/AI/EnemyCombatRole.cs`.
- Created `Assets/_LastStand/Scripts/AI/EnemyDefinition.cs`.
- `EnemyDefinition` is a small ScriptableObject data type with inspector fields for enemy id, display name, role, prefab, score value, minimum wave, spawn weight, recommended max alive, description, and validation notes.
- Added `OnValidate` safeguards for non-negative score/spawn weight, minimum wave `1`, minimum recommended max alive `1`, and safe enemy id auto-fill.
- Created `Assets/_LastStand/ScriptableObjects/Enemies/EnemyDefinition_FistMelee.asset`.
- Created `Assets/_LastStand/ScriptableObjects/Enemies/EnemyDefinition_KnifeMelee.asset`.
- Created `Assets/_LastStand/ScriptableObjects/Enemies/EnemyDefinition_Ranged.asset`.
- Verified the three assets reference the correct Last Stand enemy prefabs.
- Unity Console after script import/refresh: 0 errors and 0 warnings.
- No wave, spawn, stats, pickup, custom HUD, EnemyDeathReporter, Build Settings, actor skin replacement, scene, or third-party source changes were made.

## Next Planned Task
- Task 6.5 or Task 7: add the first runtime integration layer for assigning JU TPS AI targets when enemies are spawned, then begin WaveManager/SpawnDirector design.
