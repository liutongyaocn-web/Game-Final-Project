# Enemy Definition Plan

## Purpose
The enemy definition layer stores Last Stand enemy configuration as project-owned ScriptableObject assets. Future `WaveManager` and `SpawnDirector` systems can select enemy types from data instead of hard-coded prefab references.

This task only adds data and inspector-friendly C# types. It does not implement wave spawning, scoring, HUD, pickups, enemy death reporting, or custom AI logic.

## EnemyDefinition Script Summary
`EnemyDefinition` is a ScriptableObject under `Assets/_LastStand/Scripts/AI`. It stores:
- enemy id and display name
- combat role
- prefab reference
- score value
- minimum wave
- spawn weight
- recommended maximum alive count
- description
- validation status and notes

`OnValidate` keeps the data safe for future designers by clamping score values, wave numbers, spawn weights, and max-alive values to sensible minimums. It does not reference JU TPS classes directly.

## EnemyCombatRole Enum Summary
`EnemyCombatRole` defines the three validated Last Stand enemy roles:
- `FistMelee`
- `KnifeMelee`
- `Ranged`

These names match the Coursework001 enemy design while allowing implementation through stable JU TPS AI Attack example wrappers.

## Created EnemyDefinition Assets

| Asset path | Role | Prefab | Score value | Min wave | Spawn weight | Recommended max alive | Validation status |
|---|---|---|---:|---:|---:|---:|---|
| `Assets/_LastStand/ScriptableObjects/Enemies/EnemyDefinition_FistMelee.asset` | `FistMelee` | `Enemy_FistMelee_JUTPS.prefab` | 100 | 1 | 1.0 | 5 | Validated in `LS_Arena_01` |
| `Assets/_LastStand/ScriptableObjects/Enemies/EnemyDefinition_KnifeMelee.asset` | `KnifeMelee` | `Enemy_KnifeMelee_JUTPS.prefab` | 150 | 2 | 0.75 | 3 | Validated in `LS_Arena_01` |
| `Assets/_LastStand/ScriptableObjects/Enemies/EnemyDefinition_Ranged.asset` | `Ranged` | `Enemy_Ranged_JUTPS.prefab` | 200 | 3 | 0.5 | 2 | Validated in `LS_Arena_01` |

## Why This Supports Future WaveManager/SpawnDirector
The future wave system can load or reference a list of `EnemyDefinition` assets, then use `minWave`, `spawnWeight`, and `recommendedMaxAlive` to decide which enemy types can appear in each wave. This keeps balancing data outside wave code and makes the implementation easier to explain in the report and live Q&A.

## Coursework002 Evidence Supported
- Game AI: the three validated enemy behaviours are represented as explicit data.
- Game mechanics: each role has scoring, wave introduction, and max-alive balancing values.
- Content generation: future waves can use weighted enemy selection from definitions.
- Level progression: `minWave` introduces enemy roles gradually across the 5-wave game.
- C# code quality: custom data assets keep gameplay configuration separate from JU TPS source assets and future runtime systems.

## Known Balancing Notes
- The ranged enemy has high damage in validation, so later wave balancing should tune ranged count, spawn distance, sightlines, accuracy, or weapon damage through safe project-owned configuration where possible.
- The knife/blade melee enemy uses the JU TPS AI Attack demo's Katana/blade setup to represent the original knife-based melee role.
- Runtime spawning will need to assign JU TPS AI targets because the AI Attack example target reference is scene-specific.
