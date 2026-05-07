# Coursework002 Report Record - Last Stand

Working evidence record for CMP-6056B / CMP-7042B Coursework002. Update this continuously as features are implemented and tested.

## Initial Coursework002 Rebuild Entry
- This project is a clean rebuild for Coursework002 rather than continuing from the Coursework001 demonstration-style scene.
- JU TPS default characters and compatible AI are used first to protect gameplay stability and avoid wasting final-project time on skin, skeleton, IK, weapon alignment, or ragdoll replacement issues.
- POLYGON Apocalypse is used primarily for environment, map, buildings, props, cover, vehicles, barricades, and atmosphere rather than replacing the JU TPS actor setup.
- Custom coursework code, wrappers, scenes, prefabs, and data will be placed under `Assets/_LastStand`.
- Documentation will be maintained throughout development in `Docs` so the final report, video demo, Q&A evidence, and release notes can be produced quickly.

## Development Strategy Entry - JU TPS Integration
- JU TPS is intentionally used to reduce low-level third-person shooter implementation risk for movement, camera, aiming, shooting, reload, inventory, health/damage, animation, ragdoll, and baseline AI.
- The student's custom contribution focuses on game structure, wave progression, enemy AI integration, statistics, UI extensions, pickup rules, win/lose flow, extraction objective, documentation, and final playable vertical-slice polish.
- POLYGON Apocalypse is used for map/environment content only during core development, including buildings, props, cover, vehicles, barricades, and apocalypse atmosphere.
- Actor skin replacement is deliberately avoided during core development because it can create skeleton, IK, weapon alignment, animation, and ragdoll risks that do not improve the required Coursework002 gameplay loop.

## 1. Game Story And Game Logic
- Game title: Last Stand.
- Concept: a lone survivor fights repeated zombie waves in an apocalypse combat area.
- Target loop: prepare, survive wave, collect supplies, progress difficulty, extract/win after wave 5 or lose on player death.
- Current status: documentation and project structure foundation prepared; gameplay implementation not started in this pass.

## 2. UI And Game Statistics
- Required HUD/statistics: health, ammo, reload state, selected weapon, score, kills, wave number, survival timer, FPS.
- Statistics calculation notes to document later: score formula, kill counting, wave completion, survival timer start/end, FPS sampling method.

## 3. Game Mechanics
- Required mechanics: third-person movement, aiming, shooting, reload, weapon switching, damage, pickups, pause/restart, win/lose flow.
- Strategy: use JU TPS default player and compatible default/AI characters for stability.

## 4. Game AI
- Required AI: at least three enemy types with different attack behaviours.
- Strategy: use JU TPS-compatible AI where practical and wrap/customise behaviour inside `Assets/_LastStand`.

## 5. Animation
- Evidence to capture later: JU TPS locomotion, aiming, reload, shooting, zombie movement, hit/death reactions, pickup/UI animation if used.

## 6. Content Generation
- Evidence to capture later: Codex-assisted scripts/docs, student review/modification, generated configuration data, and any procedural/content placement support.

## 7. Level Design And Progression
- Required progression: 5 waves with increasing difficulty and an extraction/win condition.
- Environment strategy: use POLYGON Apocalypse for map, buildings, props, cover, vehicles, barricades, and atmosphere.
- Task 1 level planning completed: POLYGON Apocalypse `Demo_City_Universal_RenderPipeline.unity` is the recommended source for a cropped quarantine checkpoint / military evacuation roadblock arena.
- The final `LS_Arena_01` should use a controlled section of the city demo rather than the oversized full map, supporting reliable NavMesh, readable wave combat, and manageable performance.
- This level plan supports Coursework002 evidence for level progression, content generation through wave/spawn rules, UI/statistics demonstration, and final game logic.
- Task 2 created the initial custom arena scene at `Assets/_LastStand/Scenes/LS_Arena_01.unity` from a copied POLYGON Apocalypse city demo scene.
- The source Synty demo scene was not saved or modified for this task.
- The copied scene now includes spawn, pickup, extraction, boundary, player start, camera start, and arena centre markers so future wave-based survival work has clear planning anchors.
- This supports Coursework002 evidence for level design, progression, enemy spawning/content generation, AI approach planning, and UI/statistics demonstration.
- Task 2.5 refined the arena boundary and marker layout inside `LS_Arena_01`.
- The full copied city demo is now controlled through a smaller intended playable combat area marked by boundary preview objects, keeping the scope suitable for a final vertical slice.
- This improves evidence clarity for level progression, wave spawning, enemy AI approach routes, pickup risk/reward design, and video demonstration.

## 8. GitHub/Version Control Evidence
- Repository: https://github.com/liutongyaocn-web/Game-Final-Project
- Current observed branch: `main`.
- Commit style target: Conventional Commit messages such as `docs(cw002): ...`, `feat(waves): ...`, `feat(ui): ...`.
- Current observation: imported assets/project settings are uncommitted from asset import work; do not mix future gameplay commits with unrelated changes.

## 9. External Assets And Attribution
- JU TPS: gameplay controller, default player/AI-compatible systems, UI and effects where used.
- JUTPS Addons: optional dash/double-jump assets imported, use only if needed.
- POLYGON Apocalypse / POLYGON Generic - Art by Synty: environment, buildings, props, vehicles, apocalypse set dressing.
- Unity MCP / Codex workflow: development assistance and editor automation.

## 10. AI Tool Usage And Student Modifications
- Tool used: Codex.
- AI-generated/assisted scripts must include the required header comment and be reviewed/modified by the student.
- Detailed entries are tracked in `Docs/AI_USAGE_LOG.md`.

## 11. Known Limitations And Future Improvements
- Current limitation: custom gameplay loop is not implemented yet.
- Current limitation: build settings still reference JU TPS demo scenes.
- Current limitation: active scene is the default `Assets/Scenes/SampleScene.unity`.

## Task 3 JU TPS Gameplay Foundation Integration
- JU TPS player, camera controller, and default UI were integrated into Assets/_LastStand/Scenes/LS_Arena_01.unity as the stable third-person gameplay foundation.
- Default JU TPS player/camera/UI were used deliberately to avoid unnecessary low-level controller, camera, combat, animation, and UI foundation risk.
- This supports Coursework002 evidence for player control, animation, combat-readiness, and UI foundation before custom Last Stand systems are layered around it.
- The student's custom contribution will continue around this foundation through wave progression, spawn control, statistics, HUD extensions, pickup rules, win/lose flow, and extraction objective logic.
- The copied POLYGON scene remained a custom scene copy under _LastStand; no Synty or JU TPS source prefabs were modified.

## Task 3.5 JU TPS Foundation Validation
- The JU TPS foundation was validated in the custom LS_Arena_01 arena with only the Last Stand scene loaded.
- Player, camera controller, and default JU TPS UI are now confirmed as the base for later Coursework002 systems.
- The short Play Mode stability check showed the player spawning correctly without falling through the map, and the Console showed 0 errors and 0 warnings.
- This supports future demonstration evidence for player control, third-person camera, animation foundation, combat foundation, and UI foundation.
- Hands-on input checks for movement, mouse look, aiming, shooting, reloading, and stance actions remain a manual validation item before building wave gameplay around the foundation.

## Task 3.6 Manual JU TPS Input Validation
- Manual Unity testing confirmed that the JU TPS player, camera, and default UI work in the custom LS_Arena_01 scene.
- The player stays on the ground, camera follow works, mouse look controls the view, the default UI is visible, and the Console has no red errors.
- The previous two-audio-listener warning was resolved by testing with only LS_Arena_01 loaded.
- This supports Coursework002 evidence for player control, camera behaviour, animation/combat foundation, and UI foundation.
- The default JU TPS UI will later be extended with Last Stand-specific HUD elements for wave number, score, kills, survival timer, FPS, pickups, and objective text.
- Known polish items: camera position feels slightly low, and the default UI needs Last Stand styling and statistics presentation.

## Task 4 NavMesh And JU TPS Enemy Candidate Audit
- NavMesh and JU TPS enemy prefab candidates were audited as the foundation for Coursework002 AI evidence.
- The project currently lacks `NavMeshSurface`/Unity AI Navigation package support, so no NavMesh setup or bake was created in this task.
- JU TPS enemy candidates were identified without modifying third-party prefabs. Later Task 5 refinement selected the configured AI Attack example instances for the three Coursework001-aligned enemy roles: fist-based melee, knife-based melee, and ranged enemy.
- The project will use JU TPS AI where possible and add custom Last Stand code later for wave spawning, enemy death reporting, score/kills, statistics, and HUD integration.
- This prepares evidence for game AI, level progression, content generation through future wave spawning, and modular C# integration.

## Task 4.2 First-Pass Arena NavMesh Setup
- A first-pass `NavMeshSurface` setup was created in the custom `LS_Arena_01` scene at `_Level/NavMesh_Setup`.
- The surface is configured as a bounded Volume around the intended checkpoint/evacuation arena, avoiding an uncontrolled full-city navigation setup.
- This supports future Coursework002 AI/pathfinding evidence by preparing a controlled navigation area for JU TPS enemy testing.
- The NavMesh bake still needs to be performed manually from the `NavMeshSurface` Inspector because MCP bake automation was blocked by a tool command-length issue.
- No enemies or gameplay systems were added in this task.

## Task 4.3 Manual NavMesh Bake
- The arena NavMesh was manually baked for `LS_Arena_01`.
- Generated NavMesh data is stored under the custom scene folder at `Assets/_LastStand/Scenes/LS_Arena_01/NavMesh-NavMesh_Setup.asset`.
- The baked NavMesh prepares the scene for JU TPS enemy pathfinding tests.
- This supports future Coursework002 Game AI evidence, especially enemy navigation, approach routes, and arena pathfinding constraints.
- No enemies, wave systems, scoring, pickups, or combat logic were added in this task.

## Task 4.5 Single JU TPS Zombie AI Validation
- A single JU TPS Zombie AI was tested in `LS_Arena_01` as the first enemy AI validation step.
- The test confirmed that a JU TPS zombie prefab can exist in the custom arena scene, remain stable, move through its JU TPS navigation behaviour, detect the JU TPS player at close range, and apply melee damage.
- This validates the foundation for Coursework002 Game AI evidence without implementing the full wave system yet.
- The saved scene keeps the test enemy disabled under `_Systems/AI_Test`, so it remains explainable evidence without disrupting future tasks.
- Later custom Last Stand systems will manage enemy variants, spawning, waves, scoring, death reporting, statistics, and HUD integration.

## Task 5 Enemy Variant Wrappers
- Last Stand enemy roles were formalised as three JU TPS-based project-owned prefabs under `Assets/_LastStand/Prefabs/Enemies`.
- The source configuration came from the JU TPS AI Attack example instances rather than from generic renamed base prefabs.
- Created `Enemy_FistMelee_JUTPS`, `Enemy_KnifeMelee_JUTPS`, and `Enemy_Ranged_JUTPS` to match Coursework001 terminology and support Coursework002 AI/mechanics evidence.
- This prepares the project for later wave-based spawning, difficulty progression, enemy death reporting, score/kills/statistics, and HUD integration.
- The project still avoids actor skin replacement and does not modify JU TPS source prefabs or scenes.
