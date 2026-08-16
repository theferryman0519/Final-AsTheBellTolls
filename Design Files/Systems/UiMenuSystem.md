---
Title: Systems / UI Menu System
Game: As The Bell Tolls
Owner: Carey Clement Jr
Created: August, 2026
Updated: August, 2026
Version: 0.0.1
---

## Information

- Gameplay Menus are contextual interfaces opened through interactions with NPCs, objects, workstations, and other gameplay features.
- Gameplay Menus are separate from the Player Menu.
- Opening a Gameplay Menu changes the active Input Map from Gameplay to the appropriate Menu Input Map.
- Game time pauses while a Gameplay Menu is open.
- Player movement is disabled while a Gameplay Menu is open.
- Only one primary Gameplay Menu can be open at a time.
- Closing a Gameplay Menu returns the player to Gameplay unless another UI layer remains active.

---

## Gameplay Menus

Gameplay Menus include interfaces such as:

- Shops
- Item Storage
- Cooking
- Crafting
- Tonic Making
- Restoration
- Banking and Loans
- Character Customization
- Other contextual gameplay interfaces

---

## Menu Navigation

- Menu navigation supports Keyboard & Mouse and Controller input.
- Back or Cancel closes the current secondary panel before closing the primary Gameplay Menu.
- Confirmation panels may appear above a Gameplay Menu.
- Confirmation panels must be resolved or cancelled before interacting with the menu underneath them.
- Closing the primary Gameplay Menu returns control to the player.