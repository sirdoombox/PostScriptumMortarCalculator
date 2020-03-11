
# Post Scriptum Mortar Calculator

### [Get It Here](/releases/latest)

![Screenshot](https://i.imgur.com/UH5S0zZ.png)

A desktop app for mortar calculations in Post Scriptum, using additionally gathered data for ranges beyond those listed on the in-game tables, and for innate dispersion. Requires no installation and the high-res map image files are included in the executable.

## Information for Users
### Features
- Pan/Zoom high res map images with:
	- Mortar min/max range indictators
	- Line to target
	- Area displaying the innate spread of the mortar fire (which changes over distance)
- Selectable themes/colours
### Planned features
- Rebindable mouse buttons/keys
- Change colours of the map indicators.
- Overlay grid for maps
- Personal map landmarks for saving good mortar positions for future sessions
### Help - How Do I...
`Menu` -> `Help` should provide you with all you need to know to use the app as well as some useful general hints.
### Contacting me
If you find a bug or want to request a feature and don't have a github account, you're more than welcome to @ me on the Post Scriptum discord - Doombox#1389

## Information for Developers/Contributors
### Contributing
- **Always pull and work on the [Develop](https://github.com/sirdoombox/PostScriptumMortarCalculator/tree/Develop) branch!**
- Updating the tool when maps/mortars are changed/added is done very simply by editing the assets found [**here**](PostScriptumMortarCalculator/Assets) and can be done by anyone without any specific programming experience.
- Unit tests are a little overkill, but they do serve a purpose - Before submitting a PR ensure that
	- Changes to existing code results in all existing unit tests passing.
	- Additional code is tested.
- Any PRs that purely improve test coverage would be greatly appreciated.
-  Currently the tool I use to gather data from Post Scriptum for mortars is very labour intensive, but it is available upon request, at some point it will be made at least partially automatic and the source will be provided as part of this repo.
