# Coursework002 Report Record - Last Stand

Working evidence record for CMP-6056B / CMP-7042B Coursework002. Update this continuously as features are implemented and tested.

## Initial Coursework002 Rebuild Entry
- This project is a clean rebuild for Coursework002 rather than continuing from the Coursework001 demonstration-style scene.
- JU TPS default characters and compatible AI are used first to protect gameplay stability and avoid wasting final-project time on skin, skeleton, IK, weapon alignment, or ragdoll replacement issues.
- POLYGON Apocalypse is used primarily for environment, map, buildings, props, cover, vehicles, barricades, and atmosphere rather than replacing the JU TPS actor setup.
- Custom coursework code, wrappers, scenes, prefabs, and data will be placed under `Assets/_LastStand`.
- Documentation will be maintained throughout development in `Docs` so the final report, video demo, Q&A evidence, and release notes can be produced quickly.

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
