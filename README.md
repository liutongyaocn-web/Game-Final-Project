# Game Final Project / Last Stand

Last Stand is the Coursework002 project for CMP-6056B / CMP-7042B Game and Mobile App Development. It is being developed in Unity 6.4 / 6000.4.4f1 as a 3D single-player zombie survival third-person shooter.

## Development Strategy

- JU TPS is used for the stable third-person shooter player/enemy foundation.
- POLYGON Apocalypse is used for the environment, map, buildings, props, cover, vehicles, barricades, and apocalypse atmosphere.
- Custom coursework systems are developed separately under `Assets/_LastStand`.

## JU TPS Integration Strategy

JU TPS is the gameplay foundation during core development. POLYGON Apocalypse is used for environment/map/prop work only, while Last Stand coursework systems stay under `Assets/_LastStand`. The project avoids actor skin replacement risk during core implementation so time stays focused on waves, AI integration, statistics, UI extensions, progression, and the final playable loop.

See `Docs/JUTPS_INTEGRATION_GUIDE.md` for the detailed rules future tasks should follow.

The final scene flow is:

- `Assets/_LastStand/Scenes/LS_MainMenu.unity`
- `Assets/_LastStand/Scenes/LS_Arena_01.unity`

## Implemented Custom Systems

- `GameFlowManager`
- `WaveManager`
- `SpawnDirector`
- `LastStandStatsManager`
- `LastStandHudController`
- `EndScreenController`
- `EnemyTargetBinder`
- `EnemyLifecycleReporter`
- `EnemyDeathDropper`
- `ExtractionObjective`
- `ExtractionMarkerController`

## Final Release Target

Recommended final release:

- Tag: `v1.0-coursework002`
- Title: `Last Stand Coursework002 Final Build`
- Video length target: 9-11 minutes, maximum 12 minutes.
- Final delivery checklist: `Docs/RELEASE_AND_DELIVERY_CHECKLIST.md`
- Video script: `Docs/VIDEO_DEMO_SCRIPT.md`

## Documentation

Coursework evidence and development records are maintained in `Docs` throughout development.

## Third-Party Asset Note

Paid third-party source assets are not authored by the student. They are used only as imported dependencies inside this private coursework project. Custom coursework code and documentation are kept separate under `Assets/_LastStand` and `Docs`.

## Git Workflow

- Use small verified commits.
- Do not commit broken code.
- Keep imported third-party source assets separate from custom coursework commits.
- Create a GitHub release before final submission.
