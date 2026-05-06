# Assessment Mapping

| Coursework evidence area | Planned Last Stand evidence | Current status |
|---|---|---|
| Game story and logic | Lone survivor in an apocalypse arena, 5-wave survival loop, extraction/win condition, death/lose condition. | Planned; story and loop documented. |
| UI and statistics | HUD for health, ammo, reload state, weapon, score, kills, wave, survival timer, and FPS. | Planned. |
| Game mechanics | JU TPS provides base controller, camera, aiming, shooting, reload, inventory, health/damage, and interaction foundations; Last Stand code provides wave rules, pickups, objectives, win/lose flow, and restart integration. | Planned around JU TPS stable gameplay foundation. |
| Game AI | JU TPS provides base AI movement/detection/chase/attack/damage behaviours; Last Stand code provides enemy type wrappers, spawning, wave difficulty, score, kills, and death reporting. | Planned using JU TPS-compatible AI/wrappers where practical. |
| Animation | JU TPS provides player locomotion/combat/reload/ragdoll and compatible zombie movement/attack/hit/death animation foundations. | Asset support imported; integration guardrails documented. |
| Content generation | Last Stand wave generation will use weighted enemy selection, spawn point selection, max-alive limits, spawn timing, and difficulty scaling; Codex-assisted planning/scripts are logged and reviewed. | Planned; Task 1 level plan defines spawn-zone evidence opportunities. |
| Level progression | POLYGON Apocalypse `Demo_City_Universal_RenderPipeline.unity` is the recommended source for a cropped quarantine checkpoint / military evacuation roadblock arena; Last Stand code provides escalating waves 1-5, combat pacing, extraction endpoint, and objective progression. | Planned; Task 1 level selection plan created. |
| External assets and attribution | POLYGON Apocalypse is used for environment/map/props only; JU TPS is used for gameplay foundation; attribution is tracked separately from student-authored systems. | Attribution updated; raw third-party source assets remain uncommitted in this task. |
| GitHub release/version control | Small verified commits, clean scope, documentation logs, final release before submission. | Scaffold baseline prepared for commit. |
| C# code quality | Modular custom systems under `Assets/_LastStand`, events for decoupling, ScriptableObjects for data, pooling for repeated spawning/effects where useful, and integration wrappers rather than duplicated JU TPS systems. | Planned; JU TPS integration rules documented; no custom gameplay code yet. |
