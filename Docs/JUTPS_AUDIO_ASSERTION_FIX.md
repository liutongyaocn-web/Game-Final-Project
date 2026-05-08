# JU TPS Audio Assertion Fix

## Observed Assertion
During manual real player death validation, gameplay logic worked, but Unity Console showed a red JU TPS assertion:

`The JUApplyAudioVolumeSettings from gameObject Enemy_FistMelee_JUTPS(Clone) does not have an audio tag.`

The JU Inventory messages about item `SwitchID` changes, such as the Katana log, are regular JU TPS inventory logs and are not treated as errors for this task.

## Cause
The Last Stand enemy wrapper prefabs contained `JUApplyAudioVolumeSettings` components with an empty serialized `AudioTag` reference on embedded weapon/audio objects. These project-owned wrappers were created from configured JU TPS examples, but a few embedded audio-volume settings did not retain the required JU TPS audio tag.

## Prefabs Inspected
- `Assets/_LastStand/Prefabs/Enemies/Enemy_FistMelee_JUTPS.prefab`
- `Assets/_LastStand/Prefabs/Enemies/Enemy_KnifeMelee_JUTPS.prefab`
- `Assets/_LastStand/Prefabs/Enemies/Enemy_Ranged_JUTPS.prefab`

## Source Configuration Reference
The JU TPS source prefabs and demo references use:

- Audio tag asset: `Assets/Julhiecio TPS Controller/Audio/SFX Audio Tag.asset`
- GUID: `b7f60ffc055fd01479c79dd9351b4dff`

Examples inspected included JU TPS AI/character/item prefabs and the AI Attack example source assets. No source JU TPS assets were modified.

## Fix Applied
The empty `AudioTag: {fileID: 0}` references on `JUApplyAudioVolumeSettings` components were set to the same JU TPS `SFX Audio Tag.asset` reference used by the source configuration:

`AudioTag: {fileID: 11400000, guid: b7f60ffc055fd01479c79dd9351b4dff, type: 2}`

Fixed prefabs:
- `Enemy_FistMelee_JUTPS.prefab`
- `Enemy_KnifeMelee_JUTPS.prefab`
- `Enemy_Ranged_JUTPS.prefab`

`JU_AI_WeaponSoundSource.SoundTag` fields were inspected separately. Those are not the component named in the assertion and were left unchanged to avoid speculative JU TPS audio behaviour changes.

## Validation Result
- All three Last Stand enemy prefabs were checked after the edit and no `JUApplyAudioVolumeSettings` `AudioTag: {fileID: 0}` entries remain.
- Unity was refreshed after the prefab edit.
- Unity Console reported 0 errors and 0 warnings after refresh.
- A runtime enemy-spawn Play Mode validation was not performed by Codex in this task because it would require temporary scene/debug changes, and the task scope is restricted to enemy prefabs and documentation.

## Manual Follow-Up
- Run Wave 1 or debug-spawn `Enemy_FistMelee_JUTPS`.
- Confirm the `JUApplyAudioVolumeSettings` audio-tag assertion no longer appears.
- Treat regular JU TPS `SwitchID` inventory logs as acceptable unless they become red errors.

## Manual Runtime Revalidation
After Task 15.6, the user manually revalidated enemy spawning in Unity:

- Enemy spawning was tested after the SFX Audio Tag fix.
- The `JUApplyAudioVolumeSettings` audio tag assertion no longer appeared.
- Unity Console showed no red errors and no red assertions.
- Regular JU TPS `SwitchID` inventory log messages may still appear and are acceptable for this task.
