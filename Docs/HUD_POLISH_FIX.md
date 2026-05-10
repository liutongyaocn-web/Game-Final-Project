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

## Task 24S Minimal HUD Conversion
Task 24R was still too visually heavy, so the HUD was converted to a smaller minimal layout.

Minimal layout changes:
- `HUD_StatsPanel` reduced to approximately `286 x 152`.
- `HUD_ObjectivePanel` reduced to approximately `460 x 32`.
- Stats panel alpha reduced to `0.24`.
- Objective panel alpha reduced to `0.22`.
- Wave and Enemies Remaining text reduced to font size `16`.
- Health text reduced to font size `15`.
- Kills, Score, and Time reduced to font size `14`.
- FPS reduced to font size `11`.
- Objective text reduced to font size `17`.

The large dark panels were not removed entirely because a faint backing panel helps readability over the bright POLYGON city scene, but they are now low-opacity and much smaller.

Unity MCP was not connected during this follow-up, so Play Mode validation must be completed manually. Static scene inspection confirmed the target RectTransform, alpha, and font-size values were applied. The known `UnityEditor.EditorStyles` / `InspectorWindow` / UIElements stack trace remains documented as editor UI noise, not a Last Stand gameplay exception.

## Task 24U Full HD Compact Layout
The HUD was reworked again after Full HD `1920 x 1080` testing showed the minimal vertical stack was still too tall and visually distracting.

Final compact layout:
- Stats panel is approximately `340 x 105`, anchored top-left at `x 18`, `y -18`.
- Stats panel background alpha is `0.22`.
- Objective panel is approximately `480 x 30`, anchored top-centre at `y -18`.
- Objective panel background alpha is `0.18`.
- HUD stat output is combined into four short rows:
  - `Wave 1/5    Enemies 3/3`
  - `HP 400/400    Kills 0`
  - `Score 0    Time 00:09`
  - `FPS: 66`
- Objective text remains a short top-centre strip, not a large banner.

`LastStandHudController` was changed only for display formatting. It still reads the same StatsManager, health, FPS, and objective data. No stats calculation, gameplay logic, wave logic, enemy logic, or spawn logic was changed.

Unity MCP still had no active Unity session during this pass, so Full HD Play Mode validation remains a manual follow-up. The known `UnityEditor.EditorStyles` / `InspectorWindow` / UIElements stack trace should continue to be treated as editor UI noise unless a future stack references `_LastStand` gameplay code.

## Task 24V Background Panel Removal
The dark translucent HUD backing panels were removed visually by setting their Image alpha values to `0`.

Affected objects:
- `HUD_StatsPanel` background Image alpha: `0.22 -> 0`
- `HUD_ObjectivePanel` background Image alpha: `0.18 -> 0`

The panel GameObjects and RectTransforms were kept so the compact text layout remains stable. No text objects were deleted and no `LastStandHudController` references were changed.

Unity MCP still had no active Unity session, so Play Mode validation remains a manual follow-up. The known `UnityEditor.EditorStyles` / `InspectorWindow` / UIElements stack trace remains editor UI noise, not a Last Stand gameplay exception.
