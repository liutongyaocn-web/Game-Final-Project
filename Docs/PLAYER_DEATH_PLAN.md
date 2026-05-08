# Player Death and Game Over Foundation

## Purpose
This task adds the foundation for Last Stand's failure condition. When `Player_JUTPS` health reaches zero, a project-owned monitor reports player death to `GameFlowManager`, which moves the run into the `Failed` state.

## Foundation Scope
This is intentionally a state and reporting foundation. It does not add a full game-over screen, restart button, pause menu, pickups, or enemy drops.

## PlayerDeathMonitor Script Summary
`Assets/_LastStand/Scripts/GameFlow/PlayerDeathMonitor.cs` monitors `Player_JUTPS` without directly depending on JU TPS classes. It prefers the existing `PlayerHealthReader` when available and falls back to reflection over likely health fields/properties on the player and child components.

When a readable health value reaches `0` or below, it calls `GameFlowManager.FailRun()` once. It does not destroy the player, reload the scene, or show restart UI.

The script also exposes `DebugReportPlayerDeath()` and a disabled-by-default debug validation toggle so the failure flow can be tested safely without relying on combat damage.

## GameFlowManager Failed State Behaviour
`GameFlowManager.FailRun()` sets the current state to `Failed`, prevents later extraction unlock/victory completion, and calls `LastStandStatsManager.EndRun()` if stats are assigned.

`CurrentObjectiveText` returns `You died` while failed.

## HUD Objective Behaviour
`LastStandHudController` already reads `GameFlowManager.CurrentObjectiveText`. During Task 15 debug validation, the HUD objective displayed:

`Objective: You died`

No full game-over panel is implemented yet.

## Runtime Validation Result
Validation used `PlayerDeathMonitor`'s debug start toggle temporarily in Play Mode, then restored it to false before saving.

- `GameFlowManager` entered `Failed`.
- `GameFlowManager.CurrentObjectiveText` became `You died`.
- HUD objective text became `Objective: You died`.
- Unity Console reported 0 errors and 0 warnings.
- `WaveManager.autoStartOnPlay` remained false.
- `SpawnDirector.debugSpawnOnStart` remained false.
- No runtime-spawned enemies remain saved in the scene.

Runtime enemy-damage death was not repeated in this task. It should be manually checked later during a full combat pass, especially because ranged enemies are still highly lethal.

## Deliberately Not Implemented
- Full game-over screen.
- Restart button.
- Pause menu.
- Pickups.
- Enemy health/ammo drops.
- Ranged damage tuning.

## Future Pickup/Drop Design
- Do not use fixed pickup points.
- Health/ammo should drop from killed enemies.
- Later implementation should inspect or reference the JU TPS AI Attack Demo health/ammo pickup and drop setup.

## Manual Validation Still Needed
- Let enemies reduce player health to zero during a normal combat run.
- Confirm `PlayerDeathMonitor` detects the real JU TPS health state in live combat.
- Confirm survival time stops during an active run after death.
- Confirm full win and fail flows in the same final playtest build.

## Coursework002 Evidence Supported
- Game mechanics: the game can now enter a failure state when the player dies.
- UI/statistics: HUD objective reflects failure state.
- C# code quality: player-death monitoring is separated from game-flow state and HUD presentation.
- Video demo: win and fail conditions can be explained clearly.
