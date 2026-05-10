# Extraction Marker Plan

## Problem Observed
Manual testing confirmed that extraction unlocks after Wave 5, but the player did not have a clear visual indication of where to go. With no minimap planned for the final build, the extraction point needed a low-risk in-world marker.

## Solution
`LS_Arena_01` now has a project-owned extraction marker attached to `_ExtractionObjective/Extraction_Point`.

The marker is hidden before extraction unlocks. When extraction unlocks, a vertical green/yellow beacon and world label appear at the extraction point. A small HUD distance prompt also appears near the top centre:

- `Extraction: Xm`

When extraction completes, the marker and HUD distance prompt hide while the victory end screen takes over.

## Script Summary
`Assets/_LastStand/Scripts/GameFlow/ExtractionMarkerController.cs` observes existing game-flow state without changing it.

Responsibilities:
- Read `ExtractionObjective.IsUnlocked` and `IsCompleted`.
- Read `GameFlowManager.ExtractionUnlocked` and `VictoryReached`.
- Toggle a scene marker root.
- Update an optional world label.
- Update an optional HUD distance text.
- Face the world label toward the main camera.

The script does not start waves, unlock extraction, complete victory, or affect player/enemy behaviour.

## Scene Setup
`_ExtractionObjective/Extraction_Point` now contains:
- `ExtractionMarkerController`
- `ExtractionMarker` visual root, inactive by default
- `ExtractionMarker_Beam`
- `ExtractionMarker_Orb`
- `ExtractionMarker_Label`

`LastStandHUD_Canvas` now contains:
- `HUD_ExtractionDistanceText`, inactive by default

The marker uses simple scene primitives and built-in UI/text components. No minimap, render texture, new layer setup, or third-party source asset modification was introduced.

## Runtime Validation Result
Unity MCP validation used safe debug paths:

- Default Play Mode produced 0 red Console errors.
- Before extraction unlock, the marker root remained hidden.
- Debug extraction unlock made `GameFlowManager` enter `ExtractionUnlocked`.
- The extraction marker appeared at the extraction point.
- HUD objective showed `Objective: Reach extraction`.
- HUD distance prompt showed `Extraction: 54m` during validation.
- Debug extraction completion made `GameFlowManager` enter `Victory`.
- `CurrentObjectiveText` became `Extraction complete`.
- Console remained free of red Last Stand gameplay errors.

Debug validation flags were restored to false after testing. Final scene settings remain:
- `WaveManager.autoStartOnPlay = true`
- `SpawnDirector.debugSpawnOnStart = false`
- `PlayerDeathMonitor.debugReportDeathOnStart = false`

## What Was Not Implemented
- Minimap.
- Complex navigation arrow.
- Full objective system.
- New extraction gameplay logic.
- Wave, enemy, pickup, score, or player-controller changes.

## Coursework/Video Relevance
The marker improves final objective clarity. During the video or live demo, the player can now see where to go once extraction unlocks, making the win condition easier to demonstrate and explain.
