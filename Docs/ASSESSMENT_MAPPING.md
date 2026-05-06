# Assessment Mapping

| Coursework evidence area | Planned Last Stand evidence | Current status |
|---|---|---|
| Game story and logic | Lone survivor in an apocalypse arena, 5-wave survival loop, extraction/win condition, death/lose condition. | Planned; story and loop documented. |
| UI and statistics | JU TPS default UI is now present and active in `LS_Arena_01` as the base interface; custom HUD extensions will add wave, score, kills, survival timer, FPS, objective text, and pickup prompts. | UI foundation manually validated; custom Last Stand HUD layer planned. |
| Game mechanics | JU TPS provides base controller, camera, aiming, shooting, reload, inventory, health/damage, and interaction foundations; Last Stand code provides wave rules, pickups, objectives, win/lose flow, and restart integration. | Player control and camera foundation manually validated; camera height polish planned. |
| Game AI | JU TPS provides base AI movement/detection/chase/attack/damage behaviours; Last Stand code provides enemy type wrappers, spawning, wave difficulty, score, kills, and death reporting. | JU TPS enemy candidates audited; `NavMeshSurface` setup exists in `LS_Arena_01`; manual bake/first enemy test planned next. |
| Animation | JU TPS provides player locomotion/combat/reload/ragdoll and compatible zombie movement/attack/hit/death animation foundations. | JU TPS default player foundation validated; animation/combat evidence is ready for later demonstration through JU TPS systems. |
| Content generation | Last Stand wave generation will use weighted enemy selection, spawn point selection, max-alive limits, spawn timing, and difficulty scaling; Codex-assisted planning/scripts are logged and reviewed. | Planned; enemy role candidates and arena NavMesh setup will support future weighted wave spawning. |
| Level progression | POLYGON Apocalypse `Demo_City_Universal_RenderPipeline.unity` is the recommended source for a cropped quarantine checkpoint / military evacuation roadblock arena; Last Stand code provides escalating waves 1-5, combat pacing, extraction endpoint, and objective progression. | Planned; bounded arena navigation setup supports future wave 1-5 progression and enemy approach routes. |
| External assets and attribution | POLYGON Apocalypse is used for environment/map/props only; JU TPS is used for gameplay foundation; attribution is tracked separately from student-authored systems. | Attribution updated; raw third-party source assets remain uncommitted in this task. |
| GitHub release/version control | Small verified commits, clean scope, documentation logs, final release before submission. | Scaffold baseline prepared for commit. |
| C# code quality | Modular custom systems under `Assets/_LastStand`, events for decoupling, ScriptableObjects for data, pooling for repeated spawning/effects where useful, and integration wrappers rather than duplicated JU TPS systems. | Planned; custom enemy/wave/stat integration will wrap JU TPS AI rather than replacing it. |


