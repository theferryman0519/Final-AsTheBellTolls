---
Title: Systems / Game State System
Game: As The Bell Tolls
Owner: Carey Clement Jr
Created: August, 2026
Updated: August, 2026
Version: 0.0.1
---

## Information

- The Game State System determines the current high-level state of the game.
- Only one primary Game State can be active at a time.
- Changing the Game State determines which gameplay systems, inputs, and interfaces are currently active.
- Game States are separate from Input Maps. The current Game State determines the overall condition of the game while Input Maps determine which player inputs are available.
- Systems may respond to a Game State change by pausing, resuming, enabling, or disabling their normal behavior.

---

## Game States

| Game State | Information                                                                              |
|------------|------------------------------------------------------------------------------------------|
| Gameplay   | Normal player-controlled gameplay. Time advances and normal gameplay systems are active. |
| Dialogue   | The player is currently participating in dialogue with an NPC.                           |
| Menu       | The player has opened a gameplay or Player Menu interface.                               |
| Minigame   | The player is participating in a minigame such as Fishing or Tonic Making.               |
| Cinematic  | A scripted Game Event or cinematic sequence is currently playing.                        |
| Festival   | The player is currently participating in a festival.                                     |
| Transition | The game is transitioning between Gameplay Scenes or another major game state.           |
| Day End    | The current day has ended and end-of-day processing is occurring.                        |
| Paused     | Gameplay is manually paused.                                                             |

---

## State Rules

- Time advances during normal Gameplay.
- Time pauses when the player is not in the Gameplay Input Map.
- Player movement is disabled during Dialogue, Menu, Cinematic, Transition, Day End, and Paused states.
- NPCs involved in Dialogue or Cinematics will suspend their normal movement and routines as necessary.
- Gameplay interactions cannot begin while another incompatible Game State is active.
- The game returns to the appropriate previous or next Game State when the current state ends.