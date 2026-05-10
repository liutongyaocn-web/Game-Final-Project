# Minimap / Radar Availability Audit

## Purpose
This audit checks whether the imported JU TPS or JUTPS Addons content already includes a minimap, radar, compass, blip, or marker system that can be safely reused for Last Stand.

The goal is investigation only. No minimap was implemented, no custom minimap camera/render texture/layer setup was created, and no third-party source assets were modified.

## Search Locations
- `Assets/Julhiecio TPS Controller/**`
- `Assets/JUTPS Addons/**`
- `Assets/_LastStand/**` for project-owned context

## Search Terms
- `Minimap`
- `MiniMap`
- `Mini Map`
- `Radar`
- `Compass`
- `Map`
- `Blip`
- `Marker`
- `Icon`
- `UIMap`
- `Navigation`
- `Objective Marker`
- `Enemy Marker`
- `Player Marker`

## Candidate Assets Found
No ready-made minimap, radar, compass, blip, or UI map system was found in JU TPS or JUTPS Addons.

The search found several related UI/VFX assets, but they are not full minimap systems. They do not provide a top-down map, player marker, enemy marker tracking, or runtime-spawned enemy registration.

| Path | Type | Description | Supports player marker? | Supports enemy markers? | Runtime-spawn compatibility | Integration complexity | Risks |
|---|---|---|---|---|---|---|---|
| `Assets/Julhiecio TPS Controller/Demos/Demo Prefabs/AI/AI Alert Indicator.prefab` | Prefab | World-space AI alert indicator shown above an AI when it reacts. | No | Not as a minimap marker; it is a per-enemy alert visual. | Could be added to enemy prefabs, but it does not provide central map tracking. | Low/Medium | Useful as awareness VFX only, not a minimap. Could add visual clutter. |
| `Assets/Julhiecio TPS Controller/Scripts/AI/Vfx/JU_AiAlert.cs` | Script | Drives the AI alert sprite and rotates it toward `Camera.main`. | No | Only supports local alert state on an AI object. | No central support for runtime-spawned enemy blips. | Low/Medium | Depends on JU TPS AI event assumptions and does not solve map UI. |
| `Assets/Julhiecio TPS Controller/Demos/Demo Prefabs/Interactable/InfoIcon.prefab` | Prefab | World-space interactable/info icon visual. | No | No | Could possibly mark extraction or interactables, but not dynamic enemies. | Low/Medium | Not a HUD map. Would need project-owned objective-marker design. |
| `Assets/Julhiecio TPS Controller/Scripts/Effects/HitMarkerEffect.cs` and `hit-marker` audio | Script/audio | Hit feedback when damaging enemies. | No | No | Not relevant to spawned enemy map tracking. | Low | Unrelated to minimap/radar. |
| `Assets/Julhiecio TPS Controller/Textures/Component Icons/**` and item icons | Textures | Generic editor, component, inventory, or HUD icon artwork. | No | No | Art-only; no tracking logic. | Low as art only | Icons alone do not provide minimap behaviour. |

## Recommendation
Postpone minimap implementation before submission.

JU TPS does not appear to include a ready-made minimap/radar system that can be dropped into `LS_Arena_01` safely. A real minimap would likely require a new project-owned camera, render texture, layers, blip components, enemy registration, and runtime-spawn integration. That is a higher-risk custom UI system at the current final polish stage.

The current Last Stand HUD already communicates the most important survival information: wave, enemies remaining, kills, score, survival time, FPS, health, and objective. That is stronger and safer evidence for the final demo than rushing a fragile minimap.

## If Implementation Is Recommended Later
A later minimap task should be project-owned and explicit:
- Create a minimap camera and render texture.
- Define minimap-only layers or marker icons.
- Add player and enemy marker components.
- Register runtime-spawned enemies from `SpawnDirector` or `EnemyLifecycleReporter`.
- Hide/remove markers on enemy defeat.
- Test performance in the POLYGON city arena.

This should not modify JU TPS source assets.

## If Not Recommended Before Submission
Do not implement a full minimap from scratch unless spare time remains after final video/release work.

Safer alternatives are:
- Add a simple extraction/objective marker.
- Add optional enemy awareness indicators using a project-owned wrapper around the existing AI alert visual.
- Keep the current HUD and rely on enemy pressure/audio/line-of-sight for navigation.

## Coursework/Video Relevance
The minimap audit supports submission readiness by showing that UI scope was assessed rather than added blindly. The current HUD and objective text already provide clear UI/statistics evidence, while avoiding a risky late-stage custom minimap protects runtime stability for the final video.
