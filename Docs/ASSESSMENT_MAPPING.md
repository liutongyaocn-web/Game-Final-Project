# Assessment Mapping

| Coursework evidence area | Planned Last Stand evidence | Current status |
|---|---|---|
| Game story and logic | Lone survivor in an apocalypse arena, 5-wave survival loop, extraction/win condition, death/lose condition. | Planned; story and loop documented. |
| UI and statistics | HUD for health, ammo, reload state, weapon, score, kills, wave, survival timer, and FPS. | Planned. |
| Game mechanics | JU TPS provides base controller, camera, aiming, shooting, reload, inventory, health/damage, and interaction foundations; Last Stand code provides wave rules, pickups, objectives, win/lose flow, and restart integration. | Planned around JU TPS stable gameplay foundation. |
| Game AI | JU TPS provides base AI movement/detection/chase/attack/damage behaviours; Last Stand code provides enemy type wrappers, spawning, wave difficulty, score, kills, and death reporting. | Planned using JU TPS-compatible AI/wrappers where practical. |
| Animation | JU TPS provides player locomotion/combat/reload/ragdoll and compatible zombie movement/attack/hit/death animation foundations. | Asset support imported; integration guardrails documented. |
| Content generation | Last Stand wave generation will use weighted enemy selection, spawn point selection, max-alive limits, spawn timing, and difficulty scaling; Codex-assisted content/scripts are logged and reviewed. | Planned; documentation foundation and AI usage log created. |
| Level progression | POLYGON Apocalypse provides arena/environment content; Last Stand code provides escalating waves 1-5, combat pacing, extraction endpoint, and objective progression. | Planned. |
| GitHub release/version control | Small verified commits, clean scope, documentation logs, final release before submission. | Scaffold baseline prepared for commit. |
| C# code quality | Modular custom systems under `Assets/_LastStand`, events for decoupling, ScriptableObjects for data, pooling for repeated spawning/effects where useful, and integration wrappers rather than duplicated JU TPS systems. | Planned; JU TPS integration rules documented; no custom gameplay code yet. |
