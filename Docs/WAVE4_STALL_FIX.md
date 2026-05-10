# Wave 4 Stall Fix

## Problem Observed
Manual playtest found that Wave 4 could stall near the end:

- Wave 4 has 7 enemies.
- The first 6 enemies appeared to work.
- The final enemy seemed not to spawn or refresh correctly.
- Wave progression could not continue.

## Likely Root Cause
`WaveManager` dequeued an enemy before confirming that `SpawnDirector.SpawnEnemy(...)` returned a valid spawned GameObject.

If the final Wave 4 spawn attempt returned `null` because no ranged point survived filters or NavMesh sampling, that queued enemy was permanently consumed. This could leave the wave short of its intended total and make progression unreliable.

## Code Changes Made
Updated:

- `Assets/_LastStand/Scripts/Waves/WaveManager.cs`
- `Assets/_LastStand/Scripts/Spawning/SpawnDirector.cs`

## Failed Spawn Handling
`WaveManager` now:

- reads the next queued enemy with `spawnQueue.Peek()`
- calls `SpawnDirector.SpawnEnemy(...)`
- dequeues only after a valid enemy GameObject is returned
- increments `enemiesSpawnedThisWave` only after a valid spawn
- retains the queued enemy if spawning fails
- retries after a short delay
- records diagnostic state:
  - `PendingSpawnQueueCount`
  - `SpawnFailureCountThisWave`
  - `LastSpawnFailureReason`

This means a failed spawn attempt can no longer permanently consume the final queued enemy.

## SpawnDirector Fallback
`SpawnDirector` now has a safer fallback path:

- first tries normal role-eligible spawn points with distance and NavMesh checks
- if distance filtering removes every candidate, falls back to role-eligible points that still sample to NavMesh
- uses a larger fallback NavMesh sample radius of `6`
- still does not ignore enemy role
- still returns `null` if no role-eligible point can be sampled on NavMesh

This keeps ranged/melee role separation while reducing the chance that one strict point filter blocks the wave.

## Alive Enemy Cleanup
`WaveManager` still removes null or inactive enemies from `aliveEnemies`. Defeated enemies are removed through `EnemyLifecycleReporter -> WaveManager.NotifyEnemyDefeated(...)`, which also updates stats. No new lifecycle system was added.

## Validation Result
Unity script validation passed for:

- `WaveManager.cs`
- `SpawnDirector.cs`

Unity Console returned 0 red errors after compilation.

A light Play Mode smoke was run from `LS_Arena_01`. Wave 1 auto-started and spawned enemies. The run was not used as a full Wave 4 validation because the current scene does not have a safe direct Wave 4 start/debug path without adding extra debug API.

## Remaining Manual Test
Replay the full flow and confirm:

- Wave 4 resolves all 7 enemies.
- If the final ranged spawn fails once, it retries instead of disappearing from the queue.
- Wave 5 starts after Wave 4 is cleared.
- Console remains free of Last Stand gameplay red errors.
