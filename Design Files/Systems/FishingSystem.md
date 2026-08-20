---
Title: Systems / Fishing System
Game: As The Bell Tolls
Owner: Carey Clement Jr
Created: July, 2026
Updated: August, 2026
Version: 0.0.1
---

## Information

**<u>Activating Mechanics:</u>** When the player walks up and ends adjacent to the water, they can switch their Current Tool to be their Fishing Rod. The player will then be able to interact with the water, casting a line into the water (either into Graythorne Lake or Graythorne River) and start the Fishing Minigame. The player could also use a Fishing Net, which will automatically catch fish after 2 seconds.

**<u>Mechanic:</u>** After casting the line into the water, the player will wait for a random period of time (between 0.5 and 3.5 seconds). After the random time has passed, their is a chance that the Fishing Minigame will appear (that probability is determined by the quality of the Fishing Rod). The Fishing Minigame shows a timer bar below a clock face. The clock face has a dial and the fish is at a random position on the border of the clock face. In order to successfully catch the fish, the player will need to turn the dial to the success area on the clock. Upgrading the Fishing Rod will increase the success area on the clock.

**<u>Fail/Cancel State:</u>** If the player cancels the Fishing Minigame at any time, or if the player fails to successfully catch a fish, the player still loses the stamina consumed by using the Fishing Rod. The amount of stamina lost is determined by the quality of the Fishing Rod. The player is free to attempt to fish as long as they have the stamina available.

---

## Upgrade Success Area

| Fishing Rod Quality | Degrees of Success Area |
|---------------------|-------------------------|
| Base                | 5 Degrees               |
| Copper              | 8 Degrees               |
| Iron                | 12 Degrees              |
| Silver              | 15 Degrees              |
| Gold                | 20 Degrees              |
| Cobalt              | 30 Degrees              |

---

## Example Flows

### Using Fishing Net in Graythorne Lake

- Player walks up to the water's edge, facing it.
- Player opens their Inventory and selects the Fishing Net from the Satchel, then selects Cast Net.
- Fishing Net is cast into the water.
- 2 seconds go by and the player gets a notice to interact with the Fishing Net, pulling it from the water.
- Player receives fish from the lake, all at Basic Quality (Fishing Net only catches Basic Quality fish).

### Using Fishing Rod with No Catch

- Player walks up to the water's edge, facing it.
- Player opens Tool Wheel and selects Fishing Rod.
- Player interacts with the water and casts the Fishing Rod.
- A random amount of time between 0.5 and 3.5 seconds passes.
- Player gets a notice to interact with the Fishing Rod, pulling in the line.
- Sad emote icon appears above the player to signify no catch was made.

### Using Fishing Rod with Unsuccessful Minigame

- Player walks up to the water's edge, facing it.
- Player opens Tool Wheel and selects Fishing Rod.
- Player interacts with the water and casts the Fishing Rod.
- A random amount of time between 0.5 and 3.5 seconds passes.
- Player gets a notice to interact with the Fishing Rod, pulling in the line.
- Alert emote icon appears above the player to signify catch was made.
- Fishing Minigame appears and timer starts.
- Player does not complete minigame in time, and closes.
- Sad emote icon appears above the player to signify no catch was made.

### Using Fishing Rod with Successful Minigame

- Player walks up to the water's edge, facing it.
- Player opens Tool Wheel and selects Fishing Rod.
- Player interacts with the water and casts the Fishing Rod.
- A random amount of time between 0.5 and 3.5 seconds passes.
- Player gets a notice to interact with the Fishing Rod, pulling in the line.
- Alert emote icon appears above the player to signify catch was made.
- Fishing Minigame appears and timer starts.
- Player successfully completes minigame in time, and closes.
- Player receives fish from the water at the quality of the Fishing Rod.
