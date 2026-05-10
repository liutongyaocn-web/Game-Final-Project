# Pause Menu Fix

## Problem Observed
After adding `LS_MainMenu`, gameplay still loaded and waves still worked, but pressing `Esc` or using the JU TPS pause/menu flow could produce red Console errors. The JU TPS pause UI no longer behaved as expected.

The user also reported slightly more stutter after starting from the menu.

## Exact Errors
Unity Console contained these relevant JU TPS errors:

```text
Scene 'Menu' couldn't be loaded because it has not been added to the active build profile or shared scene list or the AssetBundle has not been loaded.
UnityEngine.SceneManagement.SceneManager:LoadSceneAsync (string)
JUTPS.UI.JU_UIPause:OnPressMainMenuButton () (at Assets/Julhiecio TPS Controller/Scripts/UI/JU_UIPause.cs:213)
UnityEngine.EventSystems.EventSystem:Update ()
```

```text
Coroutine couldn't be started because the the game object 'Pause Screen' is inactive!
UnityEngine.MonoBehaviour:StartCoroutine (System.Collections.IEnumerator)
JUTPS.UI.JU_UIPause:OnPressSomething (...) (at Assets/Julhiecio TPS Controller/Scripts/UI/JU_UIPause.cs:127)
```

## Root Cause
`LS_Arena_01` still had the JU TPS pause component configured with:

- `JU_UIPause.MainMenuScene = Menu`

Task 22A intentionally changed Build Settings so the final project uses:

- `LS_MainMenu`
- `LS_Arena_01`

The old JU TPS demo `Menu` scene is no longer in the build scene list. When the JU TPS pause menu's Menu button tried to load `Menu`, the load failed. `JU_UIPause.OnPressMainMenuButton` then deactivated the `Pause Screen` GameObject to prevent interaction during loading. Because the scene did not actually change, the inactive object still had the input callback registered, which led to the later inactive-coroutine error.

This was a scene-instance configuration mismatch caused by replacing the demo menu build flow with the Last Stand menu build flow.

## Fix Applied
Updated the project-owned gameplay scene instance only:

- Scene: `Assets/_LastStand/Scenes/LS_Arena_01.unity`
- Object: `_UISetup/UI_JUTPS_Default/Pause Screen`
- Component: `JUTPS.UI.JU_UIPause`
- Field changed: `MainMenuScene`
- Old value: `Menu`
- New value: `LS_MainMenu`

No JU TPS source scripts, source prefabs, or demo scenes were modified.

## Scene Audit
Direct `LS_Arena_01` Play Mode audit found:

- EventSystem count: 1
- Main menu UI objects in gameplay scene: none found
- `_MenuSystems` in gameplay scene: none found
- AudioListener count before Play Mode audit: 1
- `UI_JUTPS_Default` remains active
- Last Stand HUD remains separate from JU TPS default UI

`MainMenuController.StartGame()` uses `SceneManager.LoadScene(gameSceneName)` without `DontDestroyOnLoad`, so the menu scene should not persist into gameplay.

## Validation Result
After applying the scene-instance fix:

- Unity Console was cleared.
- Unity refresh completed.
- Console reported 0 red errors.
- Direct `LS_Arena_01` Play Mode smoke reported 0 red errors.
- Direct gameplay scene inspection found only one EventSystem and no persistent main menu UI objects.

MCP could not physically press `Esc` or click the JU TPS pause menu buttons in the Game view. The user should manually confirm:

- `Esc` opens the JU TPS pause UI.
- The JU TPS pause menu's Menu button returns to `LS_MainMenu`.
- No red errors appear after returning to the menu.

## Stutter / Performance Notes
No duplicate menu UI, duplicate EventSystem, duplicate AudioListener, or persistent menu systems were found in direct gameplay Play Mode. That makes the main-menu scene persistence unlikely as the stutter cause.

Remaining stutter is likely general scene/runtime load cost from the city scene, JU TPS systems, wave spawning, or Editor Play Mode overhead. This task does not optimise the POLYGON city scene or gameplay systems.

## Deliberately Not Implemented
- No minimap.
- No new gameplay systems.
- No enemy/wave balance changes.
- No pause menu rewrite.
- No JU TPS source edits.
