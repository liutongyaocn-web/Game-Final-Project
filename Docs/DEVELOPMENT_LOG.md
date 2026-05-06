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
