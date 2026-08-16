---
Title: Systems / UI System
Game: As The Bell Tolls
Owner: Carey Clement Jr
Created: August, 2026
Updated: August, 2026
Version: 0.0.1
---

## Information

- The UI System manages the interfaces displayed to the player throughout the game.
- UI will support Keyboard & Mouse, Xbox Controller, PlayStation Controller, and Nintendo Switch Controller inputs.
- UI input prompts will change based on the player's currently active input device.
- UI elements will be designed for a base resolution of 1280x720 with appropriate scaling for other supported resolutions.
- Interfaces may pause gameplay depending on the type of UI currently open.
- UI elements will display current gameplay data received from their associated systems.
- Only appropriate UI elements for the current Game State will be displayed.

---

## UI Types

| UI Type       | Information                                                                                       |
|---------------|---------------------------------------------------------------------------------------------------|
| HUD           | Displays information needed during normal Gameplay.                                               |
| Interaction   | Displays available interactions with nearby NPCs and objects.                                     |
| Dialogue      | Displays NPC dialogue and available player responses or actions.                                  |
| Gameplay Menu | Displays contextual interfaces such as shops, storage, crafting, cooking, and other interactions. |
| Player Menu   | Displays the Grandfather Clock Menu and Player Menu pages.                                        |
| Minigame UI   | Displays information and controls needed during a minigame.                                       |
| Cinematic UI  | Displays dialogue, subtitles, and other information during cinematics.                            |
| System UI     | Displays confirmations, settings, save/load interfaces, and other system-level information.       |

---

## UI Navigation

- Only one primary menu interface can be active at a time.
- Modal panels and confirmation panels may appear above an active menu.
- Back or Cancel closes the highest active UI layer first.
- UI navigation will always provide a selected element when using a controller.
- Closing the final active menu returns control to the appropriate Gameplay Input Map.