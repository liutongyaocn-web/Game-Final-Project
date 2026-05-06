# JU TPS Integration Guide for Last Stand

## Purpose
JU TPS is used as the foundation for stable third-person shooter gameplay in Last Stand. It provides the controller, camera, weapon handling, aiming, shooting, reload, inventory, health/damage, animation, ragdoll, and AI foundations that the Coursework002 vertical slice can build on.

This project should use JU TPS for moment-to-moment player and enemy behaviour, while Last Stand custom systems focus on wave structure, progression, scoring, HUD extensions, pickups, objectives, and coursework evidence.

## Project Rule
Custom Coursework002 code must extend, configure, or integrate with JU TPS rather than replacing it. A separate movement, shooting, inventory, health, or AI stack should only be created if JU TPS cannot support the required feature after a small isolated test.

## Folder Safety Rules
- Custom scripts go under `Assets/_LastStand/Scripts`.
- Custom prefabs/wrappers go under `Assets/_LastStand/Prefabs`.
- Custom scenes go under `Assets/_LastStand/Scenes`.
- ScriptableObjects go under `Assets/_LastStand/ScriptableObjects`.
- Do not edit `Assets/Julhiecio TPS Controller` directly unless explicitly instructed.
- Do not edit `Assets/JUTPS Addons` directly unless explicitly instructed.
- Do not edit `Assets/Synty` directly unless explicitly instructed.

## New Scene Setup Rules
Future scene setup should follow JU TPS scene bootstrap rules:
- Create a new scene.
- Delete the default Main Camera.
- Add the appropriate JU TPS Camera Controller prefab.
- Add a JU TPS character prefab or use JU TPS Quick Setup when needed.
- Add the JU TPS Default User Interface first.
- Extend the UI with Last Stand HUD elements later instead of replacing everything immediately.

The planned main custom scene is `Assets/_LastStand/Scenes/LS_Arena_01.unity`.

## Player Setup Rules
- Use the JU TPS default player/character foundation first.
- Prefer Advanced TPS if available and stable in this project.
- Keep default JU TPS movement, aiming, fire mode, reload, roll, crouch/prone, damage, ragdoll, and animation systems.
- Do not attempt POLYGON character skin replacement during core development.
- Any later skin replacement must be done only as an optional isolated experiment.

## Weapon Setup Rules
- Prefer JU TPS Weapon components and the JU TPS item/inventory system.
- Configure pistol, rifle, shotgun, or other weapons through JU TPS where possible.
- Custom code should not implement a separate shooting system unless absolutely required.
- Last Stand scripts may listen to or wrap weapon/ammo state for HUD/statistics.
- Any custom weapon balancing data should live under `Assets/_LastStand/ScriptableObjects`.

## Enemy AI Setup Rules
- Prefer JU TPS AI models/behaviours for enemy movement, detection, chasing, attacking, and damage.
- Use NavMesh where required by JU TPS AI.
- Create enemy prefab variants/wrappers under `Assets/_LastStand/Prefabs/Enemies`.
- Last Stand custom code should manage spawning, waves, scoring, and death reporting.
- Avoid replacing JU TPS AI with a fully custom AI unless necessary.
- Planned enemy types:
  1. Walker / slow melee infected
  2. Runner / fast melee infected
  3. Ranged infected

## UI Integration Rules
- Start with JU TPS Default User Interface.
- Treat JU TPS Default User Interface as a functional base. Last Stand will add a custom HUD layer rather than immediately replacing all JU TPS UI.
- Add or wrap custom Last Stand HUD elements for:
  - current wave
  - score
  - kills
  - survival timer
  - FPS
  - objective text
  - pickup/interact prompts
- Keep `HUDPresenter` separate from gameplay systems.
- UI should read from `StatsManager`, `GameFlowManager`, and `WaveManager` rather than directly controlling gameplay.

## Game Flow Integration Rules
JU TPS handles moment-to-moment third-person gameplay.

Last Stand custom systems handle:
- `GameFlowManager`
- `WaveManager`
- `SpawnDirector`
- `DifficultyScaler`
- `StatsManager`
- Pickup rules
- Win/lose/restart
- Extraction objective
- Coursework report/video evidence

## Content Generation Rules
Coursework002 content generation will be implemented mainly as procedural/controlled wave generation:
- weighted enemy selection
- spawn point selection
- difficulty scaling across waves
- max alive enemies
- spawn timing

The map itself will be manually selected/cropped from POLYGON Apocalypse demo content.

## Documentation Rules
Every feature task must update:
- `Docs/REPORT_RECORD.md`
- `Docs/DEVELOPMENT_LOG.md`
- `Docs/TEST_LOG.md`
- `Docs/AI_USAGE_LOG.md`

Update `Docs/ASSESSMENT_MAPPING.md` when a feature provides evidence for a marking criterion.

## Git Rules
- Do not use `git add .`.
- Stage explicit files only.
- Do not stage third-party source asset folders.
- Commit only after Unity Console has no new red errors.
- Use readable conventional commit messages.
- Push after a verified coherent task.

## Reference Notes
- JU TPS documentation describes the toolkit as a Unity template/gamekit/plugin for third-person shooter games with locomotion, ragdoll, AI, inventory, weapons, and items.
- JU TPS scene setup documentation recommends creating a new scene, deleting the default Main Camera, adding a Camera Controller prefab, adding a JU character, and adding the JUTPS Default User Interface.
- JU TPS character setup documentation lists Advanced TPS as the richer player setup, with components such as foot placement, footstep, body lean, vehicle support, procedural driving animation, and ragdoll controller.
- JU TPS weapon/item documentation covers holdable items, weapon components, fire modes, bullets, muzzle flash, ammo values, reload audio, and related weapon setup.
- JU TPS AI documentation describes ready-made AI models and modular behaviours such as attack, damage detection, field of view, follow point, follow waypoint, hear sensor, random movement, and escape.
