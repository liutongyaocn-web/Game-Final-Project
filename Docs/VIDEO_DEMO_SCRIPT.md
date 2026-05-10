# Last Stand Coursework002 Video Demo Script

Recommended length: 9-11 minutes. Maximum length: 12 minutes.

The video should be based on the final GitHub release version: `[FINAL_RELEASE_TAG]`.

If development continued after `v1.0.0`, create a newer release such as `v1.0.1` before recording, then use that release in the video and final PDF.

## 1. Title and Game Overview
Target time: `0:00-0:40`

Opening narration:

> This is Last Stand, a 3D single-player zombie survival shooter built in Unity for Coursework002. The player survives five waves in an apocalypse checkpoint arena, collects supplies from defeated enemies, and reaches extraction after the final wave.

Show:
- `LS_MainMenu`.
- Game title: `Last Stand`.
- One sentence summary: survive, fight, collect supplies, reach extraction.

## 2. Story and Game Logic
Target time: `0:40-1:20`

Narration:

> The game is set around an abandoned apocalypse roadblock. The player is a lone survivor holding out against infected enemies. The win condition is to complete Wave 5 and enter the extraction point. The fail condition is that player health reaches zero, which triggers the Failed state and the You Died end screen.

Show:
- Arena/checkpoint environment.
- Player start area.
- Briefly mention the extraction point and final objective.

Assessment coverage:
- Story and game logic.
- Win condition.
- Fail condition.
- Level objective.

## 3. Main Menu and Controls
Target time: `1:20-2:00`

Narration:

> The project has a custom Last Stand main menu with a start flow and a controls panel before gameplay.

Show:
- `LS_MainMenu`.
- Controls panel:
  - WASD: move
  - Mouse: look / aim
  - Left Mouse: shoot / attack
  - Right Mouse: aim
  - R: reload / restart after end screen
  - Shift: run
  - Ctrl: roll
  - C: crouch
  - Z: prone
  - Esc: JU TPS pause menu
- Click Start Game.
- `LS_Arena_01` loads and Wave 1 auto-starts.

## 4. Player Movement, Shooting, Reload, and Combat
Target time: `2:00-3:00`

Narration:

> JU TPS provides the third-person player, camera, aiming, shooting, health, inventory, and combat foundation. My custom Last Stand systems build the survival game loop around that foundation while keeping my coursework implementation under `Assets/_LastStand`.

Show:
- Movement and camera look.
- Aim and shoot.
- Reload if practical.
- Sprint/roll/crouch if useful.
- Basic combat with an early enemy.

Assessment coverage:
- Game mechanics.
- Player control.
- Combat loop.
- Third-person shooter foundation.

## 5. HUD and Statistics
Target time: `3:00-3:50`

Narration:

> The custom HUD shows wave, enemies remaining, health, kills, score, survival time, and objective text. These values come from the project-owned `LastStandStatsManager`. Enemies Remaining means enemies not yet defeated in the current wave, including enemies still waiting to spawn.

Continue:

> The UI is implemented with Unity Canvas. The HUD uses screen-space UI and a scalable anchored layout for common PC 16:9 screen sizes. The Canvas Scaler uses a Full HD-style reference resolution, so the HUD stays readable for recording without blocking the gameplay view.

Show:
- HUD during Wave 1.
- `Wave 1/5`.
- `Enemies 3/3` or current remaining value.
- Health.
- Kills/score.
- Survival time.
- Objective text.
- If practical, briefly show `LastStandHUD_Canvas` and Canvas Scaler in the Inspector.

Avoid overclaiming:
- Say the game is designed mainly for PC keyboard/mouse, not mobile.

Assessment coverage:
- UI and game statistics.
- Stats calculation.
- Screen-space Canvas.
- Canvas Scaler / screen-size support.

## 6. Enemy AI
Target time: `3:50-5:00`

Narration:

> Last Stand has three enemy variants. The fist melee enemy pressures the player up close. The knife or blade melee enemy is a stronger close-range threat. The ranged enemy uses a gun and creates distance pressure, with its attack range tuned after playtesting.

Show or explain:
- Fist melee enemy.
- Knife/blade melee enemy.
- Ranged enemy.

Technical narration:

> The enemies are based on JU TPS AI Attack examples, but they are integrated as project-owned Last Stand prefabs. `EnemyTargetBinder` assigns `Player_JUTPS` as the target at runtime. `EnemyLifecycleReporter` reports defeats back to WaveManager, StatsManager, and drop systems.

Show if switching to code/editor:
- `Assets/_LastStand/Prefabs/Enemies`
- `EnemyTargetBinder`
- `EnemyLifecycleReporter`
- EnemyDefinition assets.

Assessment coverage:
- Game AI.
- Runtime target binding.
- Enemy lifecycle/death reporting.

## 7. Animation
Target time: `5:00-5:40`

Narration:

> The game demonstrates character animation through player movement, aiming, shooting, reloading, enemy chasing, melee attacks, ranged attacks, hit reactions, and death behaviour. The base animation and controller systems come from JU TPS, while my custom Last Stand systems connect those animated characters to wave spawning, lifecycle reporting, statistics, drops, and game flow.

Shots to capture:
- Player run.
- Player aim/shoot.
- Player reload if visible.
- Enemy chasing.
- Fist or knife/blade attack.
- Ranged attack.
- Enemy hit/death/ragdoll or death behaviour if practical.
- Optional Inspector shot of player/enemy Animator or JU TPS animation-related components.

Clarify:
- Animation assets/controllers are JU TPS-based third-party assets.
- Coursework contribution is integration into a complete playable game loop.
- When discussing external assets later, show the JU TPS source page because the animation foundation is part of that setup.

Assessment coverage:
- Animation.
- Player and enemy animated behaviours.
- Integration of third-party animation systems into custom gameplay.

## 8. Wave Progression and Content Generation
Target time: `5:40-6:50`

Narration:

> Enemies are not manually placed for each wave. The game uses five `WaveDefinition` assets. `WaveManager` reads those assets, builds the current wave, and asks `SpawnDirector` to spawn enemies at valid spawn points from `SpawnPointGroup`.

Show:
- Wave 1 auto-start.
- Enemies spawning dynamically.
- `Assets/_LastStand/ScriptableObjects/Waves`.
- `WaveDefinition_01` through `WaveDefinition_05`.
- `SpawnDirector`.
- `WaveManager`.

Mention:
- Max-alive limits prevent all enemies spawning at once.
- Wave 5 unlocks extraction.
- Final pacing was shortened for a demo-friendly five-wave run while keeping progression.

Assessment coverage:
- Level progression.
- Content generation.
- ScriptableObject-driven wave data.
- Dynamic spawning.

## 9. Drops and Resource Loop
Target time: `6:50-7:35`

Narration:

> Last Stand does not use fixed health or ammo pickup points. Defeated enemies can drop health or ammo pickups. This creates a risk-reward loop: kill enemies, move out to collect supplies, recover resources, and survive longer.

Show:
- Kill an enemy if practical.
- HealthPowerUp or AmmoPowerUp near enemy death location.
- Player collecting a pickup if one appears.

Technical narration:

> The system uses `EnemyDeathDropper`, `EnemyDropTable`, and `DropItemEntry`. The pickup behaviour references existing JU TPS pickup prefabs, so I did not rewrite JU TPS inventory or pickup logic.

Assessment coverage:
- Game mechanics.
- Content generation.
- Resource loop.

## 10. Failure, Victory, Extraction, and Restart
Target time: `7:35-8:35`

Failure narration:

> If the player's health reaches zero, `PlayerDeathMonitor` reports death to `GameFlowManager`. The state becomes Failed, the survival timer stops, and the You Died end screen appears.

Victory narration:

> After Wave 5 is cleared, extraction unlocks. The extraction marker and distance prompt show where to go. Entering the trigger completes the run and shows Extraction Complete.

Show:
- `You Died` screen.
- `R` restart.
- Extraction marker and distance prompt.
- `Extraction Complete` screen.
- `R` restart from the end state if practical.

Assessment coverage:
- Game logic.
- Win/fail states.
- Objective clarity.
- UI feedback.

## 11. GitHub Release, Commits, and Custom Code Evidence
Target time: `8:35-9:25`

Narration:

> A GitHub release has been created for final submission, and this video demo is based on that release version: `[FINAL_RELEASE_TAG]`. The repository also shows the staged commit history and development documentation.

Show:
- GitHub repository.
- GitHub release page.
- Release tag `[FINAL_RELEASE_TAG]`.
- Commit history.
- Key folders:
  - `Assets/_LastStand/Scripts/AI`
  - `Assets/_LastStand/Scripts/Waves`
  - `Assets/_LastStand/Scripts/Spawning`
  - `Assets/_LastStand/Scripts/Stats`
  - `Assets/_LastStand/Scripts/UI`
  - `Assets/_LastStand/Scripts/GameFlow`
  - `Assets/_LastStand/ScriptableObjects`
  - `Docs`
- README or release notes if useful.

Assessment coverage:
- GitHub/source code evidence.
- Release evidence.
- Custom code separation.
- Development process.

## 12. External Asset Source Pages
Target time: `9:25-9:50`

Narration:

> The project uses JU TPS for the third-person shooter foundation and POLYGON Apocalypse for the environment. These are external dependencies. My custom coursework implementation is separated under `Assets/_LastStand`.

Required source-page shots:
- JU TPS Asset Store page or JU TPS documentation/source page.
- POLYGON Apocalypse Pack / Synty source page.
- Unity AI Navigation package page if convenient.

Mention:
- JU TPS provides player, camera, combat, AI examples, inventory/pickup behaviour, and animation/controller foundations.
- POLYGON Apocalypse/Synty provides the environment and props.
- Unity AI Navigation supports NavMesh setup.
- Custom coursework systems are project-owned and separated from third-party source assets.

Assessment coverage:
- External asset attribution.
- Clear separation between third-party assets and own work.
- Animation source clarity.

## 13. Conclusion and Honest Limitations
Target time: `9:50-10:10`

Narration:

> Last Stand has a complete playable vertical slice: main menu, arena, player combat, three enemy types, five waves, dynamic spawning, statistics, enemy drops, player death, extraction victory, end screens, and restart. Remaining limitations are mostly polish: the HUD is functional and clear rather than highly stylised, occasional NavMesh edge cases may exist in detailed city props, a minimap was audited but postponed because there was no ready-made JU TPS minimap, and a revive-current-wave system was audited but postponed because it was too risky this late.

End with:
- Gameplay shot, extraction marker, or end screen.
- Brief closing line.

## Final Timeline
| Time | Segment | Shot |
|---|---|---|
| 0:00-0:40 | Title + game overview | Main menu title and summary |
| 0:40-1:20 | Story, win condition, fail condition | Arena/checkpoint and objective explanation |
| 1:20-2:00 | Main menu + controls | Controls panel and Start Game |
| 2:00-3:00 | Player movement, shooting, reload, combat | Movement, aim, shoot, reload, early combat |
| 3:00-3:50 | HUD + statistics + Canvas Scaler | HUD values and optional Canvas Scaler shot |
| 3:50-5:00 | Enemy AI | Fist, knife/blade, ranged, binder, lifecycle |
| 5:00-5:40 | Animation | Player/enemy movement, attack, reload, death |
| 5:40-6:50 | Wave progression + dynamic spawning | WaveDefinition assets, WaveManager, SpawnDirector |
| 6:50-7:35 | Drops/resource loop | Health/ammo drops and collection |
| 7:35-8:35 | Failure and victory/extraction | You Died, extraction marker, Extraction Complete, R restart |
| 8:35-9:25 | GitHub release, commits, code/docs | Release page, commits, `_LastStand`, Docs |
| 9:25-9:50 | External asset source pages | JU TPS, Synty/POLYGON, Unity AI Navigation |
| 9:50-10:10 | Conclusion + limitations | Summary and final gameplay/end screen |

## Shot Checklist
- [ ] `LS_MainMenu` title visible.
- [ ] Controls panel visible.
- [ ] Start Game loads `LS_Arena_01`.
- [ ] Wave 1 starts automatically.
- [ ] HUD shows wave, enemies, health, kills, score, time, objective.
- [ ] Canvas / Canvas Scaler shown briefly if practical.
- [ ] Movement/camera/combat shown.
- [ ] Player aim/shoot/reload animation shown if practical.
- [ ] Fist melee enemy shown.
- [ ] Knife/blade melee enemy shown.
- [ ] Ranged enemy shown.
- [ ] Enemy chasing/attack animation shown.
- [ ] Enemy death/hit reaction shown if practical.
- [ ] Enemy spawn from wave system shown.
- [ ] WaveDefinition assets shown.
- [ ] Enemy death updates kills/score.
- [ ] Health/ammo drop shown or explained.
- [ ] You Died end screen shown.
- [ ] Extraction marker/distance prompt shown.
- [ ] Extraction Complete end screen shown.
- [ ] R restart shown.
- [ ] GitHub release page shown.
- [ ] Commit history shown.
- [ ] `Assets/_LastStand` folders shown.
- [ ] Docs folder shown.
- [ ] JU TPS source page shown.
- [ ] POLYGON Apocalypse / Synty source page shown.
- [ ] Unity AI Navigation package page shown if convenient.

## Backup Plan If Full Wave 5 Takes Too Long
Use this route if the full five-wave run is too slow or risky during recording:

1. Demonstrate normal early gameplay with Wave 1 and Wave 2.
2. Show `WaveDefinition_05` in Unity to prove the final wave exists.
3. Explain that Wave 5 is configured to unlock extraction.
4. Use the debug extraction/victory path only to demonstrate the final objective UI:
   - extraction marker appearing
   - HUD objective/distance prompt
   - extraction trigger/victory flow
   - `Extraction Complete` end screen
5. State clearly:

> This is a recording-time shortcut to demonstrate the final objective UI. The actual game flow still uses five configured waves, and Wave 5 unlocks extraction.

## Assessment Coverage Reminder
| Marking area | What to show |
|---|---|
| Story/game logic | Menu, arena, five waves, extraction, death/failure |
| UI/statistics | HUD, StatsManager, enemies remaining, Canvas Scaler |
| Game mechanics | Combat, waves, drops, extraction, restart |
| Game AI | Three enemy types, target binding, lifecycle reporting |
| Animation | Player/enemy movement, attack, reload, death behaviours |
| Content generation | WaveDefinition, SpawnDirector, dynamic drops |
| GitHub | Release, commits, custom code folders, Docs |
| External assets | JU TPS, POLYGON Apocalypse/Synty, Unity AI Navigation |

## Q&A Preparation
### What did I implement?
I implemented the Last Stand-specific systems under `Assets/_LastStand`: enemy definitions, wave definitions, spawning, target binding, enemy lifecycle reporting, stats, HUD, game flow, extraction objective, player death monitoring, enemy drops, main menu, end screen, restart, extraction marker, and documentation.

### Why use JU TPS?
JU TPS provides a strong third-person shooter foundation: player movement, camera, aiming, shooting, inventory, health, pickups, AI examples, and animation/controller systems. This let the coursework focus on building a complete game loop and custom systems.

### How is game AI implemented?
Enemy behaviours come from JU TPS AI Attack examples. Last Stand wraps those as project-owned enemy prefabs, binds them to `Player_JUTPS` at runtime with `EnemyTargetBinder`, and tracks defeat with `EnemyLifecycleReporter`.

### How do you show animation?
Show the player moving, aiming, shooting, reloading, and enemies chasing, attacking, and dying. Explain that base animation assets/controllers come from JU TPS, while the coursework contribution connects them into custom waves, stats, drops, and game flow.

### How is content generation shown?
Enemies are spawned dynamically from `WaveDefinition` assets by `WaveManager` and `SpawnDirector`. Enemy drops are generated dynamically from defeated enemies using `EnemyDeathDropper` and `EnemyDropTable`.

### How are statistics calculated?
`LastStandStatsManager` tracks current wave, total waves, enemies remaining, kills, score, and survival time. `SpawnedEnemyRuntimeInfo` stores wave/enemy metadata on spawned enemies so kills and score can be counted safely.

### How do you support different screen sizes?
The HUD uses Unity Canvas with screen-space UI, anchored RectTransforms, and a Canvas Scaler using a Full HD-style reference resolution. It is designed for common PC 16:9 screen sizes and keyboard/mouse play.

### What is your own work versus third-party assets?
JU TPS and POLYGON Apocalypse/Synty are third-party dependencies. My own coursework systems are under `Assets/_LastStand`, including wave/spawn/stat/UI/game-flow/drop integration and project documentation.

### What external assets were used?
JU TPS was used for third-person shooter, AI, pickup, and animation foundations. POLYGON Apocalypse/Synty assets were used for environment and props. Unity AI Navigation supports NavMesh setup.

### Why did you postpone minimap/current-wave revive?
No ready-made JU TPS minimap was found, and a custom minimap would be risky late in the project. Revive-current-wave was feasible but touched wave reset, enemy cleanup, player resurrection, stats timing, and end-screen UI, so it was postponed to protect final stability.

### How does the release version relate to the video?
The video should be recorded from the final GitHub release version `[FINAL_RELEASE_TAG]`. If any changes are made after that release, create a newer release and update the video/PDF links.

### What would I improve with more time?
I would polish NavMesh edge cases, add a carefully scoped objective/minimap indicator if needed, improve HUD art styling, implement a fully validated revive-current-wave system, and continue balancing enemy difficulty through more playtests.
