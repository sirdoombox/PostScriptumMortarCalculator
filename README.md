
# Post Scriptum Mortar Calculator

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

## Information for Developers/Contributors
### Contributing
- Updating the tool when maps/mortars are changed/added is done very simply by editing the assets found [**here**](PostScriptumMortarCalculator/Assets) and can be done by anyone without any specific programming experience.
- Unit tests are exhaustive and massively overkill, but they do the job - Before submitting a PR ensure that
	- Changes to existing code results in all existing unit tests passing.
	- Additional code is tested.
-  Currently the tool I use to gather data from Post Scriptum for mortars is very labour intensive, but it is available upon request, at some point it will be made at least partially automatic and the source will be provided as part of this repo.
