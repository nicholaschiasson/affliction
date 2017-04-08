# affliction
COMP4501 Term Project - An immune system real-time strategy game made with Unity.

## Contributors
- [Nicholas Chiasson](https://github.com/nicholaschiasson)
- [Luke Harper](https://github.com/Lharp5)

## Controls
| Key          | Action                                                                           |
| ------------ | -------------------------------------------------------------------------------- |
| w            | Move camera up/forward                                                           |
| a            | Move camera left                                                                 |
| s            | Move camera down/backward                                                        |
| d            | Move camera right                                                                |
| ESC          | Quit game (ignored in unity editor)                                              |
| LEFT MOUSE   | Select unit (drag to multi-select)<br/>Deslects units if click/drag over terrain |
| RIGHT MOUSE  | Selected unit do action at mouse location                                        |
| SCROLL WHEEL | Zoom camera in/out                                                               |
| 1            | If spawner selected, spawn red blood cell                                        |
| 2            | If spawner selected, spawn white blood cell                                      |
| SPACE        | Hold to view camera quick go to menu                                             |
| SPACE + 1    | Jump camera to heart                                                             |
| SPACE + 2    | Jump camera to brain                                                             |
| SPACE + 3    | Jump camera to lungs                                                             |
| SPACE + 4    | Jump camera to stomache                                                          |
| SPACE + 5    | Jump camera to left kindey                                                       |
| SPACE + 6    | Jump camera to right kindey                                                      |


## Mechanics
### Movement
Allied microorganisms (red/white blood cells) can be moved by selecting them and then right clicking somewhere on the terrain. A trail particle effect will appear behind the unit as they move.

### Spawning
Allied microorganisms can be spawned by first selecting the [spawner](#spawners) then pressing 1 or the icon in the bottom right UI panel to spawn a red blood cell, or pressing 2 or the icon in the bottom right UI panel to spawn a white blood cell. A spawning animation particle effect will appear.

### Mining resources
To mine resources to be transferred to the spawner, first select a [red blood cell](#red-blood-cell), then right click a [miner organ](#miners), and then right click a spawner organ. The red blood cell will continuously travel back and forth from the miner to the spawner, bringing resources.

### Attacking Enemies
To Attack an enemy select a [white blood cell](#white-blood-cell), then right click on an enemy [pathogen](#pathogen) or [spore](#spore) or enemy Infection. When two units collide, the units use RigidBody physics to push the enemy units while dealing damage. A damage particle effect will sparkle around the enemy unit. When a unit dies it is removed from the game.

## Identification
### Microorganisms
#### Red blood cell
-   Red blood cell model
-   Ally affiliation
-   Collects resources from a miner and delivers to a spawner

#### White blood cell
-   White blood cell model
-   Ally affiliation
-   Attacks enemy units

#### Spore
-   Blue model identical to white blood cell model
-   Enemy affiliation
-   Builds [infections](#infection)

#### Pathogen
-   Skeletal animated model 
-   Enemy affiliation
-   Attacks allied units

### Miners
#### Lungs
-   Lungs model
-   Ally affiliation
-   Produces oxygen

#### Stomach
-   Stomach (actually liver) model
-   Ally affiliation
-   Produces protein

#### Kidney
-   Kidney model
-   Ally affiliation
-   Produces erythropoietin

### Spawners
#### Heart
-   Heart model
-   Ally affiliation
-   Spawns red and white blood cells

#### Infection
-   Black cube
-   Enemy affiliation
-   Spawns spores and pathogens

## Notes
-   UI is incomplete, so the following functionality is all it contributes to:
    -   The "Menu" button in the top left of the game window simply exits the game, as does the ESCAPE key (this is all ignored in the editor)
    -   More UI elements are still to be added in the future
-   Currently spawning animation plays on first launch
-   A bug exists when a unit is attacking another unit but not moving, the moving trail still is emitting and if the unit dies, the allied unit stops by the trail keeps emitting

## ChangeLog
- Added models to all units
- Added skeletal animation to Pathogen
- Added particle effects
- Removed collision to same type of units
- Added combat from player units to enemies
- Added Kidney, Stomache, Lungs
- UI Improvements
- Added brain and research functionality for organ upgrading
- Updated minimap to show entire map
- Added camera zoom and camera boundaries
- Added unity terrain
- Added Unity pathfinding using NavMesh system
- Added white blood cell AI, which in turn is responsible for pathogen AI
- Added spore AI
- Added infection AI
- Placed level components in a more suitable startup configuration
