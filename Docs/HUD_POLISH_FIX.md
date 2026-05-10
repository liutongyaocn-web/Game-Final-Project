# HUD Polish Fix

## Problem Observed
Task 24 improved the HUD visually, but the first pass was too large for normal play:
- The left stats panel covered too much of the gameplay view.
- The objective banner was wider and taller than needed.
- A red Console stack trace appeared during playtesting, including `UnityEditor.EditorStyles.get_toolbarButtonRight`.

## Fix Applied
The HUD was resized in `LS_Arena_01` using existing standard Unity UI objects only.

Compact layout changes:
- `HUD_StatsPanel` reduced to approximately `360 x 218`.
- `HUD_ObjectivePanel` reduced to approximately `560 x 46`.
- Stats panel alpha reduced to `0.48`.
- Objective panel alpha reduced to `0.42`.
- Wave text reduced to font size `22`.
- Enemies Remaining text reduced to font size `21`.
- Health text reduced to font size `20`.
- Kills, Score, and Time reduced to font size `19`.
- FPS reduced to font size `15`.
- Objective text reduced to font size `24`.

The HUD still displays Wave, Enemies Remaining, Health, Kills, Score, Time, FPS, and Objective.

## Console Investigation
The repeated red stack trace was:

`NullReferenceException: Object reference not set to an instance of an object`

The stack referenced Unity Editor internals:
- `UnityEditor.EditorStyles.get_toolbarButtonRight`
- `UnityEditor.PropertyEditor`
- `UnityEditor.InspectorWindow`
- `UnityEngine.UIElements`

Filtered Console checks found no red entries referencing:
- `_LastStand`
- `LastStand`
- `Input`
- `EndScreenController`
- `MainMenuController`
- `LastStandHudController`
- `PlayerHealthReader`
- `LastStandStatsManager`

No old Input System error was found. The previous `UnityEngine.Input.GetKeyDown` issue did not return.

Conclusion: the observed red stack trace is Unity Editor Inspector/UIElements tooling noise, not a Last Stand gameplay script exception.

## Validation Result
Unity was refreshed and the Console was cleared before validation. A fresh pre-Play-Mode Console check returned 0 red errors.

Play Mode validation confirmed:
- Wave 1 auto-started.
- HUD still displayed `Wave: 1 / 5`.
- The compact HUD remained wired after resizing.
- Console filters for `_LastStand`, `LastStand`, and `Input` returned 0 red entries.

MCP could not simulate real WASD/mouse input in the Game view, so player-facing movement still benefits from a quick hands-on check. The Console investigation did not find any Last Stand or old-input exception path tied to normal gameplay input.

## Remaining Visual Notes
- The compact HUD should be checked once at the final recording resolution.
- The HUD is intentionally simple and readable rather than highly styled.
- No gameplay logic or HUD controller script changes were made.
