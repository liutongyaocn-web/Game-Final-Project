# Release and Delivery Checklist

## Repository
- GitHub repository URL: https://github.com/liutongyaocn-web/Game-Final-Project
- Branch: `main`
- Latest checked commit before this checklist: `08ff41c1c81893b3e34d7a894597901f4bfd20b9`
- Confirm the repository is private or that access is handled appropriately for marking.
- Confirm commit history shows staged development over time through small scoped commits.

## Final Scenes
- Main menu scene: `Assets/_LastStand/Scenes/LS_MainMenu.unity`
- Gameplay scene: `Assets/_LastStand/Scenes/LS_Arena_01.unity`

Build Settings should include:
1. `LS_MainMenu`
2. `LS_Arena_01`

## Final Validation Checklist
Before release:
- [ ] Unity opens without red compile errors.
- [ ] Start Game loads `LS_Arena_01`.
- [ ] Wave 1 auto-starts.
- [ ] HUD displays correctly.
- [ ] Enemies spawn and target player.
- [ ] Kills/score update.
- [ ] Health/ammo drops work.
- [ ] Player death shows `You Died`.
- [ ] Extraction marker appears after final wave or debug unlock.
- [ ] `Extraction Complete` screen works.
- [ ] `R` restart works.
- [ ] Pause menu works.
- [ ] Pause -> Menu returns to `LS_MainMenu`.
- [ ] Console has no Last Stand gameplay red errors.

## Release Version
Recommended GitHub release:
- Tag: `v1.0.1`
- Release title: `Last Stand Coursework002 Final Build`

Release notes should include:
- Final vertical slice.
- Unity version: `6000.4.4f1`.
- Main scene flow: `LS_MainMenu -> LS_Arena_01`.
- Key systems implemented:
  - main menu and controls panel
  - JU TPS player/camera/combat foundation
  - POLYGON Apocalypse arena
  - three enemy variants
  - five-wave progression
  - dynamic spawning
  - enemy target binding and lifecycle reporting
  - stats manager and HUD
  - enemy death health/ammo drops
  - player death and failed state
  - extraction marker and victory flow
  - end screen and restart
- Known limitations:
  - HUD is functional and clear but visually simple
  - occasional NavMesh edge cases may occur in detailed city props
  - minimap was audited but postponed
  - revive-current-wave was audited but postponed
  - third-party assets are required dependencies
- Third-party assets used:
  - JU TPS
  - JUTPS Addons if required by the imported project setup
  - POLYGON Apocalypse Pack - Art by Synty
  - Unity AI Navigation package

## Build / Package Delivery
### Option 1: Playable Build
If submitting a playable build:
- Windows build zip is recommended.
- Suggested zip name: `LastStand_Coursework002_Windows.zip`
- Include the executable and matching data folder.
- Test the build before sharing.
- Confirm Start Game, Wave 1 auto-start, enemy spawning, HUD, failure, extraction/victory, pause/menu, and restart.

### Option 2: Unity Project
If sharing the Unity project:
- Third-party paid assets are dependencies.
- Do not redistribute raw paid assets publicly.
- If required for marker access, provide private/controlled access or a OneDrive link.
- README should explain required assets:
  - JU TPS
  - JUTPS Addons if used by the local project
  - POLYGON Apocalypse Pack

## Asset Attribution
- JU TPS: third-person shooter player/camera/combat/AI foundation.
- JUTPS Addons: imported support/addon dependency if required by the project setup.
- POLYGON Apocalypse Pack - Art by Synty: environment, city props, vehicles, barricades, and apocalypse visuals.
- Unity AI Navigation package: NavMesh baking/runtime navigation support.

## Video Upload
- Upload final video to YouTube or OneDrive.
- If YouTube, use Unlisted.
- Confirm the link works in a private/incognito browser.
- Recommended video length: 9-11 minutes.
- Maximum video length: 12 minutes.
- Video should use release version `v1.0.1`.

## Final Submission PDF
The PDF should contain:
- Student name / ID if required.
- Game title: `Last Stand`.
- GitHub repository link: https://github.com/liutongyaocn-web/Game-Final-Project
- GitHub release link for `v1.0.1`.
- Video demo link.
- Optional playable build link.
- Short note: "The video demonstration and live demo are based on release v1.0.1."

## Known Limitations
- HUD is functional and clear but visually simple.
- Occasional NavMesh edge cases may occur in complex city props.
- Minimap was audited but postponed because no ready-made JU TPS minimap was found.
- Current-wave revive system was audited but postponed due implementation risk.
- Third-party assets are used as dependencies and should not be publicly redistributed as raw paid asset source.

## Final Pre-Submission Checklist
- [ ] GitHub release exists.
- [ ] Video link works.
- [ ] PDF contains GitHub and video links.
- [ ] Release/build link works.
- [ ] Blackboard PDF opens correctly.
- [ ] No local-only paths in submitted PDF.
- [ ] YouTube/OneDrive permissions are correct.
- [ ] Release notes mention third-party dependencies.
- [ ] Demo/video is based on release `v1.0.1`.
