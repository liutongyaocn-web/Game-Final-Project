# Game Final Project / Last Stand

Last Stand is the Coursework002 project for CMP-6056B / CMP-7042B Game and Mobile App Development. It is being developed in Unity 6.4 / 6000.4.4f1 as a 3D single-player zombie survival third-person shooter.

## Development Strategy

- JU TPS is used for the stable third-person shooter player/enemy foundation.
- POLYGON Apocalypse is used for the environment, map, buildings, props, cover, vehicles, barricades, and apocalypse atmosphere.
- Custom coursework systems are developed separately under `Assets/_LastStand`.

The main custom scene planned for the final vertical slice is:

- `Assets/_LastStand/Scenes/LS_Arena_01.unity`

## Planned Custom Systems

- `GameFlowManager`
- `WaveManager`
- `SpawnDirector`
- `StatsManager`
- `HUDPresenter`
- Pickup system
- Enemy integration/reporting

## Documentation

Coursework evidence and development records are maintained in `Docs` throughout development.

## Third-Party Asset Note

Paid third-party source assets are not authored by the student. They are used only as imported dependencies inside this private coursework project. Custom coursework code and documentation are kept separate under `Assets/_LastStand` and `Docs`.

## Git Workflow

- Use small verified commits.
- Do not commit broken code.
- Keep imported third-party source assets separate from custom coursework commits.
- Create a GitHub release before final submission.
