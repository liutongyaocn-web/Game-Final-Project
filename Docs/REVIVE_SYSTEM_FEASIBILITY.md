# Revive Current Wave Feasibility Audit

## Requested Design
When the player dies, the end screen should offer:
- `Revive Current Wave`
- `Restart From Beginning`

The player has 5 revive chances per run. If all revive chances are used, the revive option should be disabled or hidden and the player should only be able to restart from the beginning.

Suggested controls:
- `E` = Revive Current Wave
- `R` = Restart From Beginning

This audit does not implement the system. It only checks whether the current Last Stand codebase can support it safely.

## Current System Support
The current architecture is close enough to support a revive feature, but not with a simple end-screen-only patch. A safe implementation would need coordinated changes across:
- `GameFlowManager`
- `WaveManager`
- `EnemyLifecycleReporter`
- `EnemyDeathDropper`
- `PlayerDeathMonitor`
- `LastStandStatsManager`
- `EndScreenController`
- a new project-owned player revive helper

The feature is feasible, but it is medium-to-high risk because it must reset JU TPS death state, clear live enemies without counting them as kills/drops, and restart the current wave without resetting the whole run.

## WaveManager Audit
`WaveManager` already has useful internal state:
- `currentWaveIndex`
- `currentWave`
- `state`
- `enemiesSpawnedThisWave`
- `totalEnemiesToSpawnThisWave`
- `aliveEnemies`
- `spawnQueue`
- `waveRoutine`

Existing useful public methods/properties:
- `CurrentWaveIndex`
- `CurrentWaveNumber`
- `StartWave(int waveIndex)`
- `StopWaves()`
- `RegisterEnemyRemoved(GameObject enemy)`
- `NotifyEnemyDefeated(GameObject enemy)`

Current restart support is incomplete:
- `StartWave(currentWaveIndex)` can rebuild the current wave queue and reset wave counters.
- `StopActiveRoutine()` exists but is private.
- `StartWave` clears `aliveEnemies` but does not destroy current runtime enemies.
- `StopWaves()` clears state and ends stats, but it resets the run to idle rather than reviving the current wave.

Required changes:
- Add a public `RestartCurrentWaveForRevive()` or similar.
- Stop the active wave coroutine.
- Clear `spawnQueue`.
- Destroy current spawned enemies safely.
- Reset `enemiesSpawnedThisWave`, `totalEnemiesToSpawnThisWave`, and `aliveEnemies`.
- Restart `RunWaveRoutine(currentWaveIndex)` without calling `statsManager.BeginRun`, because that would wipe kills/score/survival time.

## Enemy Cleanup Audit
Runtime enemies are parented under `_Systems/Spawned_Enemies` through `SpawnDirector.spawnedEnemyParent`.

It should be possible to clean up current enemies by iterating `aliveEnemies` or the `Spawned_Enemies` parent. However, destroying enemies is not currently safe for revive cleanup:
- `EnemyLifecycleReporter.OnDestroy` reports defeat when `reportOnDestroy` is true.
- `ReportDefeated` calls `WaveManager.NotifyEnemyDefeated`.
- `NotifyEnemyDefeated` increments stats through `LastStandStatsManager.RegisterEnemyDefeated`.
- `EnemyDeathDropper` listens to `EnemyLifecycleReporter.Defeated` and may spawn pickups.

If revive cleanup simply destroys runtime enemies, it can incorrectly:
- increase kills
- increase score
- increase defeated-this-wave count
- spawn health/ammo drops
- advance the wave lifecycle

Required changes:
- Add suppression support to `EnemyLifecycleReporter`, for example `SuppressReportingForCleanup()` or `DestroyWithoutDefeatReport`.
- Ensure `EnemyDeathDropper` does not drop pickups during suppressed cleanup.
- Add a `WaveManager.ClearAliveEnemiesForRevive()` path that disables reporter defeat reporting before destroying enemy objects.
- Prefer cleaning only the enemies spawned for the current wave rather than every object in the scene.

## Player Health Restore Audit
Current project-owned `PlayerHealthReader` only reads health through reflection. It does not write health or reset death state.

JU TPS stores health in `JUTPS.JUHealth`:
- public `Health`
- public `MaxHealth`
- private-set `IsDead`
- public `ResetHealth()`

JU TPS character controller code also has a public-looking resurrection method:
- `RessurectCharacter()`

Important: setting only `JUHealth.Health = MaxHealth` is probably not enough after real death. JU TPS death state also affects:
- `JUCharacterControllerCore.IsDead`
- collider trigger state
- rigidbody kinematic/constraints state
- layer
- animator state
- ragdoll state
- camera dead-state transition

JU TPS source already demonstrates the correct kind of reset in:
- `JUCharacterControllerCore.RessurectCharacter()`
- `SceneController.RespawnPlayer()`

Required changes:
- Add a project-owned `PlayerReviveHelper`.
- Find `Player_JUTPS`.
- Prefer calling `RessurectCharacter()` by reflection if present.
- Fallback to calling `JUHealth.ResetHealth()` or setting `Health = MaxHealth` only if resurrection method is unavailable.
- After revive, refresh `PlayerHealthReader`.
- Manually validate that player movement/camera/input recover after death.

Conclusion: player health can likely be restored safely, but only if the implementation resets JU TPS character death state, not just the health value.

## Player Position Reset Audit
`Player_Start` exists in `LS_Arena_01` at approximately:
- position `(-10, 0, -136)`
- rotation about Y `80` degrees

Moving the player back to `Player_Start` is reasonable, but should happen after or alongside the JU TPS resurrection reset.

Risks:
- If the player is ragdolled, moving only the root transform may not reset ragdoll bones.
- CharacterController/Collider/Rigidbody state may still be in dead configuration if `RessurectCharacter()` is not called.
- Camera pivot may need one frame to recover.

Required changes:
- `PlayerReviveHelper` should reference or find `Player_Start`.
- Temporarily handle physics safely while moving the player.
- Prefer JU TPS resurrection first, then set transform position/rotation.
- Manual validation must confirm player can move/aim/shoot after revive.

## GameFlowManager Audit
`GameFlowManager` currently supports:
- `Idle`
- `Running`
- `ExtractionUnlocked`
- `Victory`
- `Failed`

Current constraints:
- `FailRun()` sets state to `Failed` and calls `statsManager.EndRun()`.
- `BeginRun()` refuses to run if state is `Victory` or `Failed`.
- `UnlockExtraction()` and `CompleteExtraction()` refuse to run if state is `Failed`.
- There is no current method to leave `Failed` and return to `Running`.

Required changes:
- Add `maxRevives = 5`, `revivesRemaining`, and possibly `revivesUsed`.
- Add read-only properties for UI.
- Add `CanRevive`.
- Add `ReviveCurrentWave()` or `TryReviveCurrentWave()`.
- Coordinate:
  - enemy cleanup
  - player revive
  - player position reset
  - current wave restart
  - stats timer resume
  - `PlayerDeathMonitor` death-report reset
- Guard victory/extraction so revive cannot happen after `Victory`.

Recommendation: revives should live in `GameFlowManager`, because it owns run-level state and can coordinate WaveManager, StatsManager, PlayerDeathMonitor, and the end screen.

## EndScreenController Audit
Current `EndScreenController`:
- shows panel on `Failed` or `Victory`
- displays a title, subtitle, stats text
- supports restart by button and `R`
- uses New Input System safely through `Keyboard.current.rKey.wasPressedThisFrame`

Required UI changes:
- Add optional `reviveButton`
- Add optional `reviveKey`, likely `E`
- Add revive text/status:
  - `Revives Remaining: X / 5`
  - `Press E to Revive Current Wave`
  - `Press R to Restart From Beginning`
- Hide or disable revive controls when `revivesRemaining == 0` or state is `Victory`.
- Add New Input System polling for `Keyboard.current.eKey.wasPressedThisFrame`.
- Keep `R` as scene reload/restart from beginning.

This is technically straightforward once GameFlowManager exposes `CanRevive` and `RevivesRemaining`.

## Stats Implications
Current stats:
- `BeginRun` resets kills, score, wave, enemy counts, survival time, and starts the timer.
- `EndRun` stops the timer.
- `SetCurrentWave` resets `enemiesDefeatedThisWave` when the wave number changes.
- `RegisterEnemyDefeated` increments kills, score, and `enemiesDefeatedThisWave`.

Simple fair design:
- Kills and score should survive a revive.
- Survival time should pause on the death screen and resume after revive.
- Current wave enemy counts should reset when the current wave restarts.
- Revives used/remaining should be tracked in `GameFlowManager`, optionally mirrored to stats later.

Required changes:
- Add a stats method such as `ResumeRunTimer()` or `SetRunActive(true)` that does not reset the run.
- Add a method to reset current wave counts without resetting kills/score/time.
- Ensure cleanup-destroyed enemies do not call `RegisterEnemyDefeated`.

## Required Implementation Tasks If Approved
1. Add `PlayerReviveHelper`.
2. Add cleanup suppression to `EnemyLifecycleReporter`.
3. Update `EnemyDeathDropper` if needed so suppressed defeats do not drop pickups.
4. Add `WaveManager.RestartCurrentWaveForRevive()` and safe enemy cleanup.
5. Add `LastStandStatsManager` resume/pause/current-wave-reset support.
6. Add `PlayerDeathMonitor.ResetDeathReport()` so it can detect another death after revive.
7. Add revive state/properties/methods to `GameFlowManager`.
8. Update `EndScreenController` with revive UI/key/button support.
9. Update the scene only after scripts are safe, wiring revive UI references if needed.
10. Perform hands-on validation.

## Risks
| Risk | Level | Notes |
|---|---|---|
| Player remains dead/ragdolled after health restore | High | Must use JU TPS resurrection behaviour, not health-only write. |
| Enemy cleanup counts as kills/score/drops | High | Current `OnDestroy` defeat reporting makes naive cleanup unsafe. |
| Wave coroutine/state desync | Medium/High | Must stop coroutine and rebuild queue cleanly. |
| Stats timer or wave counts reset incorrectly | Medium | Need resume/reset methods that do not wipe the whole run. |
| PlayerDeathMonitor only reports once | Medium | Needs reset after revive. |
| UI input conflicts | Low/Medium | New Input System pattern is already established in `EndScreenController`. |
| Scope creep before submission | Medium | Revive touches multiple core systems late in the project. |

## Recommendation
Implement a simplified version only if there is enough time for hands-on validation.

The feature is feasible, but it is not a tiny polish task. The safest version should:
- revive only from the death end screen
- restart the current wave from the beginning
- preserve kills, score, and survival time
- pause survival timer while dead
- clear current enemies without defeat reporting or drops
- restore the player through JU TPS resurrection behaviour
- move player back to `Player_Start`
- keep `R` as full scene restart

If time is tight, postpone revive and keep the existing failure/restart flow. The current submission is already acceptable without revive.

## Proposed Implementation Plan If Approved
1. Implement `PlayerReviveHelper` with reflection-based calls to `RessurectCharacter()` and fallback `JUHealth.ResetHealth()`.
2. Add `EnemyLifecycleReporter.SuppressDefeatReporting()` or equivalent.
3. Add `WaveManager.ClearAliveEnemiesForCleanup()` and `RestartCurrentWaveForRevive()`.
4. Add `LastStandStatsManager.PauseRunTimer()`, `ResumeRunTimer()`, and wave-count reset support.
5. Add revive counters and `TryReviveCurrentWave()` to `GameFlowManager`.
6. Add `ResetDeathReport()` to `PlayerDeathMonitor`.
7. Update `EndScreenController` with revive display, `E` key, and optional revive button.
8. Wire UI references in `LS_Arena_01`.
9. Validate the full loop manually.

## What Should Be Tested Manually
- Die during Wave 1, revive, and confirm player can move/aim/shoot.
- Confirm health restores to `400 / 400`.
- Confirm player returns to `Player_Start`.
- Confirm Wave 1 restarts from the beginning.
- Confirm cleanup enemies do not add kills/score.
- Confirm cleanup enemies do not drop pickups.
- Confirm survival timer pauses on death and resumes on revive.
- Confirm revive count decreases from `5 / 5`.
- Use all revives and confirm revive option disappears/locks.
- Confirm `R` still restarts from beginning.
- Confirm death can be detected again after a revive.
- Confirm final Wave 5/extraction behaviour is not broken.
