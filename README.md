# affliction
COMP4501 Term Project - An immune system real-time strategy game made with Unity.

**The following details the current functionality of the game, much of which is temporary and subject to change very soon.**

## Controls
| Key          | Action                                      |
| ------------ | ------------------------------------------- |
| w            | Move camera up/forward                      |
| a            | Move camera left                            |
| s            | Move camera down/backward                   |
| d            | Move camera right                           |
| ESC          | Quit game (ignored in unity editor)         |
| LEFT MOUSE   | Select unit (drag to multi-select)          |
| RRIGHT MOUSE | Selected unit do action at mouse location   |
| 1            | If spawner selected, spawn red blood cell   |
| 2            | If spawner selected, spawn white blood cell |

## Mechanics
### Movement
Allied microorganisms (red/white blood cells) can be moved by selecting and then right clicking somewhere on the terrain.

### Spawning
Allied microorganisms can be spawned by first selecting the [spawner](#Spawner) then pressing 1 or the icon in the bottom right UI panel to spawn a red blood cell, or pressing 2 or the icon in the bottom right UI panel to spawn a white blood cell.

**NOTE**: Currently, spawning has no cost.

### Mining resources
To mine resources to be transferred to the spawner (and other buildings in the future), first select a [red blood cell](#Red-blood-cell), then right click a [miner building](#Miner), and then right click a spawner building. The red blood cell will continuously travel back and forth from the miner to the spawner, bringing resources that don't currently do anything.

## Identification
#### Red blood cell
-   Red sphere
-   Collects resources and delivers to building

#### White blood cell
-   White sphere
-   Attacks enemy units (NOT IMPLEMENTED)

#### Spore
-   Blue sphere
-   Builds infection buildings (NOT IMPLEMENTED)

#### Pathogen
-   Cyan sphere
-   Attacks allied units (NOT IMPLEMENTED)

#### Miner
-   Magenta cube
-   Produces resources (NOT IMPLEMENTED)

#### Spawner
-   Brown/orange cube
-   Spawns red and white blood cells

#### Pathogen
-   Black cube
-   Spawns spores and pathogens (NOT IMPLEMENTED)

## Notes
-   UI is incomplete, so the following functionality is all it contributes to:
    -   The "Menu" button in the top left of the game window simply exits the game, as does the ESCAPE key (this is all ignored in the editor)
    -   The bottom right panel is meant to be an action panel, but for the time being, it only displays the spawning options for spawner buildings
    -   None of the other UI elements are functional
-   Currently, to deselect any unit(s) it is necessary to drag select an empty space or terrain
