# Main Menu Return Fix

## Problem Observed
After returning from the JU TPS pause UI to `LS_MainMenu`, the main menu appeared but the buttons could not be clicked.

## Root Cause
The menu scene could inherit gameplay/pause state from `LS_Arena_01`:

- cursor locked by gameplay camera/controller state
- cursor hidden
- `Time.timeScale` left paused
- stale selected UI object in the current `EventSystem`

The menu scene itself was visible and had an EventSystem, but `MainMenuController` did not reset the input/cursor state when the scene loaded.

## Fix
`Assets/_LastStand/Scripts/UI/MainMenuController.cs` now resets menu state in `Awake`.

Added serialized safeguards:

- `resetCursorOnStart = true`
- `resetTimeScaleOnStart = true`
- `clearSelectedUiOnStart = true`

On menu load, the controller now:

- sets `Time.timeScale = 1`
- sets `Cursor.lockState = CursorLockMode.None`
- sets `Cursor.visible = true`
- clears `EventSystem.current` selected object if an EventSystem exists

The Start Game flow still loads `LS_Arena_01` normally and does not use `DontDestroyOnLoad`.

## Validation Result
Validation through Unity MCP:

- `MainMenuController.cs` compiled with 0 red Console errors.
- `LS_MainMenu` contains exactly one EventSystem.
- `MainMenuController` has all three reset toggles enabled.
- In Play Mode, the EventSystem current selected object was null.
- Console reported 0 red errors.

MCP cannot reliably click UGUI buttons or simulate the full JU TPS pause menu return path, so the user should manually confirm:

- Pause menu Menu button returns to `LS_MainMenu`.
- Cursor is visible and unlocked after returning.
- Start Game is clickable.
- Controls panel opens/closes.

## Scene Changes
No scene changes were required for this task.

## Remaining Manual Checks
- Confirm the pause menu -> `LS_MainMenu` -> Start Game loop in the Unity Game view.
- Confirm Quit remains safe in the Editor.
