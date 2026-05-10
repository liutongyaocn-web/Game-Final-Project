# Last Stand Coursework002 Video Demo Script

Recommended length: 9-11 minutes. Maximum length: 12 minutes.

## 1. Title and Game Overview
Opening narration:

> This is Last Stand, a 3D single-player zombie survival shooter built in Unity for Coursework002. The player must survive five enemy waves in an apocalypse checkpoint arena, collect supplies from defeated enemies, and reach extraction after the final wave.

Show:
- `LS_MainMenu`
- Game title: `Last Stand`
- Briefly mention the playable loop: survive, fight, collect supplies, reach extraction.

## 2. Story and Game Logic
Narration:

> The game is set around an abandoned roadblock/checkpoint in an apocalypse city. The player is a lone survivor trying to hold out against infected enemies. The win condition is to complete Wave 5 and enter the extraction point. The fail condition is simple: if the player's health reaches zero, the run enters the Failed state and the You Died screen appears.

Show:
- Environment framing in `LS_Arena_01`.
- Player start area.
- Extraction marker location if already unlocked through the backup route later.

## 3. Main Menu and Start Flow
Narration:

> The project has a custom Last Stand main menu. It provides a start flow and a controls panel before entering the gameplay scene.

Show:
- `LS_MainMenu`
- Controls panel briefly:
  - WASD move
  - Mouse look/aim
  - Left Mouse shoot/attack
  - Right Mouse aim
  - R reload / restart after end screen
  - Shift run
  - Ctrl roll
  - C crouch
  - Z prone
  - Esc pause/JU TPS menu
- Click Start Game.
- Confirm `LS_Arena_01` loads and Wave 1 starts automatically.

## 4. Player Controls and Combat
Narration:

> The third-person player, camera, combat, health, inventory, and movement foundation come from JU TPS. My coursework implementation builds the Last Stand game systems around that foundation instead of modifying JU TPS source code.

Show:
- Move around the arena.
- Camera look.
- Aim and shoot if practical.
- Reload if visible.
- Dodge/roll or sprint if useful.

Mention:
- JU TPS provides the base third-person controller.
- Custom Last Stand code lives under `Assets/_LastStand`.

## 5. HUD and Statistics
Narration:

> The custom HUD displays the important gameplay statistics: current wave, enemies remaining in the current wave, player health, kills, score, survival time, and the current objective. These values come from the project-owned `LastStandStatsManager`.

Show:
- Wave value, e.g. `Wave 1/5`.
- Enemies remaining, e.g. `Enemies 3/3`.
- Health.
- Kills and score.
- Time.
- Objective text.

Mention:
- Enemy remaining means enemies not yet defeated in the current wave.
- Stats are separate from the HUD so the logic can be reused or displayed differently later.

## 6. Enemy AI
Narration:

> Last Stand has three enemy variants. The fist melee enemy pressures the player up close. The knife or blade melee enemy is a stronger close-range threat. The ranged enemy uses a gun and creates distance pressure, but its attack range was tuned after playtesting so it is fairer.

Show or explain:
- Fist-based melee enemy.
- Knife/blade melee enemy.
- Ranged gun enemy.

Technical explanation:

> These enemies are based on JU TPS AI Attack examples but wrapped as project-owned Last Stand prefabs. `EnemyTargetBinder` assigns `Player_JUTPS` as the target at runtime, and `EnemyLifecycleReporter` notifies the wave and stats systems when an enemy is defeated.

## 7. Wave Progression and Content Generation
Narration:

> Enemies are not manually placed for each wave. The game uses five `WaveDefinition` assets. `WaveManager` reads those assets, builds the current wave, and asks `SpawnDirector` to spawn enemies at valid spawn points from `SpawnPointGroup`.

Show:
- Wave 1 auto-starting.
- Enemies spawning dynamically.
- If switching to editor/code view, show:
  - `Assets/_LastStand/ScriptableObjects/Waves`
  - `WaveDefinition_01` through `WaveDefinition_05`
  - `SpawnDirector`
  - `WaveManager`

Mention:
- Max-alive limits prevent every enemy spawning at once.
- Wave 5 unlocks extraction.
- The pacing was shortened for a demo-friendly 5-wave run while retaining progression.

## 8. Pickups and Resource Loop
Narration:

> The game does not use fixed health or ammo pickup points. Instead, defeated enemies can drop health or ammo pickups. This creates a risk-reward loop: kill enemies, decide whether to move out and collect supplies, then continue surviving.

Show:
- Kill an enemy if possible.
- HealthPowerUp or AmmoPowerUp appearing near the enemy death location.
- Player picking up a health/ammo item if one appears.

Technical explanation:

> The drop system uses `EnemyDeathDropper`, `EnemyDropTable`, and `DropItemEntry`. The pickups reference existing JU TPS pickup prefabs, so I did not rewrite JU TPS pickup behaviour.

## 9. Victory and Failure
Narration for failure:

> If the player health reaches zero, `PlayerDeathMonitor` reports the death to `GameFlowManager`. The state changes to Failed, the survival timer stops, and the You Died end screen appears.

Show:
- You Died screen if practical.
- Press R to restart.

Narration for victory:

> After Wave 5 is cleared, extraction unlocks. The new extraction marker and distance prompt tell the player where to go. Entering the extraction trigger completes the run and shows Extraction Complete.

Show:
- Extraction marker and HUD distance prompt.
- Extraction Complete screen.
- Press R restart if practical.

If full Wave 5 is too long, use the backup plan below and state it clearly:

> For recording time, I am using the debug extraction path to demonstrate the final objective UI. The actual wave data still contains five waves, and Wave 5 is configured to unlock extraction.

## 10. Code and GitHub Evidence
Narration:

> The project uses GitHub commits and documentation throughout development. The custom implementation is organised under `Assets/_LastStand`, separate from third-party packages.

Show:
- GitHub repository.
- Commit history briefly.
- Key folders:
  - `Assets/_LastStand/Scripts/AI`
  - `Assets/_LastStand/Scripts/Waves`
  - `Assets/_LastStand/Scripts/Spawning`
  - `Assets/_LastStand/Scripts/Stats`
  - `Assets/_LastStand/Scripts/UI`
  - `Assets/_LastStand/Scripts/GameFlow`
  - `Assets/_LastStand/ScriptableObjects`
  - `Docs`

Mention:
- A GitHub release will be created for final submission.
- Documentation records tests, AI usage, design decisions, and known limitations.

## 11. External Assets
Narration:

> The project uses JU TPS for the third-person shooter foundation and POLYGON Apocalypse Pack for the environment. These are third-party dependencies. My custom coursework implementation is kept under `Assets/_LastStand`, with wrapper scripts, data assets, scene setup, wave logic, stats, HUD, game flow, drops, and documentation.

Mention:
- JU TPS: player, camera, combat, AI foundation, inventory/pickup behaviour.
- POLYGON Apocalypse: environment/props.
- No JU TPS or Synty/POLYGON source assets were modified as part of the custom systems.

## 12. Conclusion
Narration:

> Last Stand now has a complete playable vertical slice: main menu, arena, player combat, three enemy types, five waves, dynamic spawning, statistics, enemy drops, player death, extraction victory, end screens, and restart. The remaining limitations are mostly polish: the HUD is functional and clear rather than highly stylised, occasional NavMesh edge cases may exist in detailed map areas, a minimap was considered but postponed to avoid late risk, and a revive-current-wave feature was audited but not implemented because it touched too many systems this late.

End with:
- One last gameplay shot or end screen.
- Brief thanks/closing line.

## 10-Minute Timeline
| Time | Segment | Shot |
|---|---|---|
| 0:00-0:45 | Title and overview | Main menu title, short game description |
| 0:45-1:30 | Story and objectives | Arena/checkpoint, win/fail explanation |
| 1:30-2:15 | Menu and controls | Controls panel, Start Game |
| 2:15-3:15 | Player controls/combat | Move, aim, shoot, reload/sprint |
| 3:15-4:10 | HUD/statistics | Wave, enemies, health, kills, score, time, objective |
| 4:10-5:25 | Enemy AI | Fist, blade, ranged enemy explanation/demo |
| 5:25-6:30 | Waves/spawning | WaveDefinition, SpawnDirector, dynamic spawning |
| 6:30-7:20 | Pickups | Enemy death drop, health/ammo pickup loop |
| 7:20-8:40 | Failure/victory | You Died, extraction marker, Extraction Complete, R restart |
| 8:40-9:40 | Code/GitHub evidence | Scripts, ScriptableObjects, Docs, commits |
| 9:40-10:00 | Conclusion | Summary and honest limitations |

## Shot Checklist
- [ ] `LS_MainMenu` title visible.
- [ ] Controls panel visible.
- [ ] Start Game loads `LS_Arena_01`.
- [ ] Wave 1 starts automatically.
- [ ] HUD shows wave, enemies, health, kills, score, time, objective.
- [ ] Movement/camera/combat shown.
- [ ] Fist melee enemy shown.
- [ ] Knife/blade melee enemy shown or explained with prefab/data.
- [ ] Ranged enemy shown or explained with range tuning.
- [ ] Enemy spawn from wave system shown.
- [ ] Enemy death updates kills/score.
- [ ] Health/ammo drop shown or explained.
- [ ] You Died end screen shown.
- [ ] Extraction marker/distance prompt shown.
- [ ] Extraction Complete end screen shown.
- [ ] R restart shown.
- [ ] GitHub repo/commit history shown.
- [ ] `Assets/_LastStand` folders shown.
- [ ] Third-party asset dependencies explained.

## Backup Plan If Full Wave 5 Takes Too Long
Use this route if the full five-wave run is too slow or risky during recording:

1. Demonstrate normal early gameplay with Wave 1 and Wave 2.
2. Show the `WaveDefinition_05` asset in Unity to prove Wave 5 exists and unlocks extraction.
3. Explain that full Wave 5 normally unlocks extraction.
4. Use the debug extraction/victory path to show:
   - extraction marker appearing
   - HUD objective/distance prompt
   - extraction trigger/victory flow
   - `Extraction Complete` end screen
5. Clearly state this is a recording-time shortcut for demonstrating the final objective, not a replacement for the actual configured wave flow.

## Q&A Preparation
### What did I implement?
I implemented the Last Stand-specific systems under `Assets/_LastStand`: enemy definitions, wave definitions, spawning, target binding, enemy lifecycle reporting, stats, HUD, game flow, extraction objective, player death monitoring, enemy drops, main menu, end screen, restart, extraction marker, and documentation.

### Why use JU TPS?
JU TPS provides a strong third-person shooter foundation: player movement, camera, aiming, shooting, inventory, health, pickups, and AI examples. Using it let the coursework focus on building a complete game loop and custom systems rather than recreating a shooter controller from scratch.

### How is game AI implemented?
The enemy behaviour comes from JU TPS AI Attack examples. Last Stand wraps those as project-owned enemy prefabs, binds them to `Player_JUTPS` at runtime with `EnemyTargetBinder`, and tracks defeat with `EnemyLifecycleReporter`.

### How is content generation shown?
Enemies are spawned dynamically from `WaveDefinition` assets by `WaveManager` and `SpawnDirector`. Enemy drops are also generated dynamically from defeated enemies using `EnemyDeathDropper` and `EnemyDropTable`.

### How are statistics calculated?
`LastStandStatsManager` tracks current wave, total waves, enemies remaining, kills, score, and survival time. `SpawnedEnemyRuntimeInfo` stores wave/enemy metadata on spawned enemies so kills and score can be counted safely.

### What external assets were used?
JU TPS was used for third-person shooter and AI foundations. POLYGON Apocalypse/Synty assets were used for the environment and props. The custom coursework implementation is separated under `Assets/_LastStand`.

### What would I improve with more time?
I would polish NavMesh edge cases, add a safer objective/minimap indicator if needed, improve HUD art styling, add a fully validated revive-current-wave system, and continue balancing ranged enemy difficulty through more playtests.
