# Ranged Enemy Balance

## Problem Observed
Manual playtesting after the restart input fix found that the ranged enemy was too punishing mainly because it could engage from too far away. The player could be damaged before clearly seeing the ranged enemy, which made the challenge feel less readable.

## Fields And Components Inspected
Inspected project-owned prefab:
`Assets/_LastStand/Prefabs/Enemies/Enemy_Ranged_JUTPS.prefab`

Relevant JU TPS AI Attack component fields found on the prefab:
- `Attack.GunAttack.MinDistance`
- `Attack.GunAttack.MaxDistance`
- `Attack.GunAttack.StartRunDistance`
- `Attack.GunAttack.StopDistance`
- `Attack.GunAttack.Shooting.Precision`
- `Attack.GunAttack.Shooting.MaxShotDistance`
- `Attack.MeleeAttack.AttackDistance`

The prefab also contains inherited P226 weapon setup, but damage values were not changed in this task.

## Values Changed
Only the project-owned `Enemy_Ranged_JUTPS.prefab` was changed.

| Field | Before | After | Reason |
|---|---:|---:|---|
| `Attack.GunAttack.MaxDistance` | 15 | 8.5 | Reduces the distance at which the AI should consider gun attack behaviour valid. |
| `Attack.GunAttack.Shooting.MaxShotDistance` | 100 | 30 | Reduces excessive shot reach while keeping a ranged threat distinct from melee enemies. |

Fields left unchanged:
- `Attack.GunAttack.MinDistance = 4`
- `Attack.GunAttack.StartRunDistance = 4`
- `Attack.GunAttack.StopDistance = 3`
- damage/health/fire rate/accuracy

## Before / After Behaviour
Before:
- Ranged enemy could create pressure from a long distance.
- Player could be hit before clearly seeing or reacting to the enemy.

Expected after:
- Ranged enemy still creates distance pressure.
- Ranged enemy should attack from a more readable medium range.
- Player should have more time to see, move, or use cover.
- Ranged enemy should not behave like a melee enemy because its gun attack band still extends beyond melee range.

## Validation Result
Unity refresh/compile completed with 0 errors and 0 warnings after the prefab edit. A manual ranged encounter pass is still needed to confirm the feel in `LS_Arena_01`, especially around sightlines and stuck NavMesh areas.

Task 17R manual validation note:
- Failed-state `R` restart was manually validated and works.
- Victory-state `R` restart is still pending because full victory flow is slower to reach.

## Remaining Balance Notes
- Some enemies can still get stuck in detailed map/NavMesh areas; this should be handled separately with spawn-point tuning, blockers, or NavMesh/obstacle cleanup.
- Ranged enemy damage remains high but was not changed first because range/readability was the main problem observed.
- Ranged spawn line of sight should still be reviewed during final playtest.

## Why Damage Was Not Changed First
The playtest issue was mainly readability and engagement distance, not only raw damage. Reducing attack and shot range preserves the ranged enemy's role while giving the player a fairer chance to identify and respond to the threat.

## Coursework / Video Relevance
- Shows playtest feedback being used to tune enemy behaviour.
- Keeps the ranged enemy challenging while making the final demo more readable.
- Supports Game AI and game mechanics evidence by demonstrating role-specific enemy tuning.
