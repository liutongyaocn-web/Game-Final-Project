# Level Selection Plan - LS_Arena_01

## Purpose
`LS_Arena_01` will be built from a cropped and controlled section of POLYGON Apocalypse demo content. This saves production time, keeps the environment visually strong, and still leaves the Coursework002 contribution focused on gameplay structure, JU TPS integration, waves, AI spawning, statistics, objectives, and polish.

The final scene should not reuse an oversized full demo map as-is. It should select a manageable combat area, copy or instance only the needed environment pieces into `Assets/_LastStand/Scenes/LS_Arena_01.unity`, and keep all custom systems under `Assets/_LastStand`.

## Candidate Scenes Found

| Scene path | Description | Pros | Cons | Suitability score out of 5 |
|---|---|---|---|---|
| `Assets/Synty/PolygonApocalypse/Scenes/Demo_City_Universal_RenderPipeline.unity` | Complete URP apocalypse city demo with grouped `Buildings`, `Ground`, `Terrain`, `Props`, `Vehicles`, `Vehicles_Wrecked`, `Trees`, `DeadBodies`, `Weapons`, lighting, camera, and volume. | Best fit for Unity 6 URP-style project; strong urban survival theme; contains roadblocks, wrecked vehicles, buildings, cover, possible military/checkpoint clusters, and enough space to crop a compact arena. | Very large and dense; full map would be too much for Coursework002 if used directly; needs careful cropping and performance control. | 5 |
| `Assets/Synty/PolygonApocalypse/Scenes/Demo_City_Standard_RenderPipeline.unity` | Standard render pipeline version of the same city demo. | Same layout benefits as the URP city scene; good fallback if rendering setup requires it. | Less suitable than URP variant for the current configured project; same size/complexity risks. | 4 |
| `Assets/Synty/PolygonApocalypse/Scenes/Demo_Bunker.unity` | Small bunker/interior-style demo with props, building pieces, point lights, spotlights, hospital beds, toilets, and background elements. | Strong military/evacuation mood; compact; clear boundaries; good extraction-room reference. | Likely too narrow/interior-heavy for third-person wave combat and NavMesh routes unless expanded with exterior props. | 3 |
| `Assets/Synty/PolygonApocalypse/Scenes/Demo_Building_Interior_Dressing.unity` | Showcase of dressed building interiors such as shops, warehouses, houses, diners, apartments, industrial buildings, church, and market pieces. | Useful source for interior dressing and pickup/objective props. | Prop/interior showcase rather than a playable arena; weak for wave movement routes by itself. | 2 |
| `Assets/Synty/PolygonApocalypse/Scenes/Overview.unity` | Apocalypse asset overview with grouped characters, vehicles, buildings, props, weapons, dead bodies, environment pieces, and attachments. | Good asset reference catalogue for choosing barricades, vehicles, props, and dressing. | Showcase scene, not a playable level; should not be the main source arena. | 2 |
| `Assets/Synty/PolygonGeneric/Scenes/Overview.unity` | Generic Synty overview with base props, buildings, environment, and weapons. | Useful backup for generic props/material style. | Not apocalypse-specific and not a playable level. | 1 |

## Recommended Scene / Area

Chosen source scene path:

`Assets/Synty/PolygonApocalypse/Scenes/Demo_City_Universal_RenderPipeline.unity`

Recommended area:

A cropped quarantine checkpoint / military evacuation roadblock inside the URP city demo. The audit found useful military and evacuation props in the city scene, including army trucks, ambulances, prison bus, big rig/trailer assets, barricaded doors, bunker entrance pieces, wrecked vehicles, buildings, dead bodies, road/ground sections, and dense props. The future arena should use a compact roadblock/bunker-approach section rather than the full city map.

Why it fits Last Stand:

- It supports a clear zombie survival story: the survivor reaches an abandoned evacuation checkpoint and must hold out long enough to extract.
- It naturally supports line-of-sight breaks using vehicles, barricades, building corners, wreckage, tents/containers, and props.
- It can be bounded with vehicles, fences, barricaded doors, rubble, and blocked roads so the player cannot run infinitely far away.
- It gives enough approach lanes for three enemy types without becoming open-world in scope.
- It gives a clear final objective: reach or activate an extraction point near the bunker entrance, evacuation truck, ambulance, radio tower, or marked checkpoint gate.

Expected arena boundaries:

- Block the main road exits with wrecked vehicles, barricades, container props, fences, and collapsed debris.
- Keep the playable area roughly one street block or checkpoint compound, not the whole city.
- Use building facades, barricaded doors, and non-enterable structures as natural walls.

Expected player start area:

- Start near a defensible checkpoint interior edge, behind an ambulance/army truck or temporary barricade.
- Give the player immediate sight of the main objective route and first pickup zone.

Expected enemy spawn zones:

- Spawn zone A: blocked road beyond the main checkpoint gate.
- Spawn zone B: side alley or building-corner approach outside immediate player view.
- Spawn zone C: bunker/evacuation perimeter or wrecked-vehicle lane.
- Spawn zone D: late-wave pressure spawn behind distant barricades, still outside direct player view.

Expected pickup zones:

- Ammo crate near the player start but slightly exposed.
- Health pickup near an ambulance/medical tent area.
- Risk/reward ammo pickup beside wrecked vehicles or barricade cover.
- Late-wave supply pickup near the extraction route.

Expected extraction/final objective location:

- A radio beacon, bunker entrance, evacuation truck, or checkpoint gate at the far side of the arena.
- The extraction point should unlock after wave 5 and be visible enough for video demonstration.

## Arena Design Goals
- 5-wave survival combat.
- Strong line-of-sight breaks.
- Enough cover but not too much.
- Enemies can approach from multiple directions.
- Player cannot run infinitely far away.
- NavMesh should remain simple and reliable.
- Suitable for video demonstration and live demo.

## Planned Scene Structure

Planned hierarchy for the future `LS_Arena_01`:

```text
LS_Arena_01
- _Systems
- Player
- Cameras
- UI
- Level
- SpawnPoints
- Pickups
- ExtractionPoint
- Lighting
- Audio
```

## Coursework002 Evidence Supported
- Game story and logic: the arena represents an abandoned evacuation/checkpoint area where the survivor makes a final stand and escapes after wave 5.
- Game mechanics: the space supports JU TPS movement, aiming, shooting, reload, cover use, pickup collection, and extraction.
- Game AI: multiple approach lanes support walker, runner, and ranged infected behaviours.
- Content generation through wave/spawn system: spawn point selection, weighted enemy selection, max alive enemies, spawn timing, and difficulty scaling can all be demonstrated in one controlled arena.
- Level progression: the arena can become more dangerous across five waves while keeping the same readable layout.
- UI/statistics demonstration: wave number, kills, score, survival timer, objective text, FPS, health, ammo, and pickup prompts can be shown clearly.
- Animation/combat demonstration: JU TPS player animation, weapon handling, reload, enemy attacks, hit reactions, and death/ragdoll behaviour can be shown in a compact combat space.

## Risks and Mitigation
- Scene too large: crop to a small combat area and avoid importing the full demo hierarchy into the final scene.
- NavMesh complexity: use simple walkable zones, blocked roads, and clear enemy approach lanes.
- Enemy spawn unfairness: spawn outside immediate player view and use short delay/telegraphing where needed.
- Performance: remove or disable unnecessary distant objects later if needed.
- Third-party asset licensing: document attribution and do not redistribute raw paid assets publicly.
- Demo scene contamination: do not save changes to third-party demo scenes; create `LS_Arena_01` under `Assets/_LastStand/Scenes` in a later task.

## Next Task Proposal
Task 2: Create `LS_Arena_01` as a new custom scene using the selected source environment area, add the initial hierarchy, lighting/camera placeholders, and save only under `Assets/_LastStand/Scenes`.
