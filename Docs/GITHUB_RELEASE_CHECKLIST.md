# GitHub Release Checklist

## Before Each Commit
- [ ] Check git status.
- [ ] Confirm change is a coherent small unit.
- [ ] Confirm no imported third-party source assets were edited accidentally.
- [ ] Confirm Unity is not compiling.
- [ ] Check Unity Console for red errors through MCP where possible.
- [ ] Update relevant files in Docs.
- [ ] Use a readable Conventional Commit message.

## Before Push
- [ ] Project opens in Unity 6000.4.4f1.
- [ ] Last Stand scene runs locally.
- [ ] Console has no red errors.
- [ ] Core gameplay loop has been manually tested.
- [ ] Documentation reflects current implementation.

## Before Release
- [ ] Version/tag chosen.
- [ ] Build settings point to final Last Stand scene(s), not JU TPS demo scenes.
- [ ] Windows build created and smoke-tested.
- [ ] GitHub release notes mention gameplay features, controls, known limitations, and asset credits.
- [ ] Report/video evidence files are ready or linked.
