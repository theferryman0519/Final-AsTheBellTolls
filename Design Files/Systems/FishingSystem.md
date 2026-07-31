---
Title: Characters / Systems / Fishing System
Game: As The Bell Tolls
Owner: Carey Clement Jr
Created: July, 2026
Updated: July, 2026
Version: 0.0.1
---

## Information

**<u>Activating Mechanics:</u>** When the player walks up and ends adjacent to the water, they can switch their Current Tool to be their Fishing Rod. The player will then be able to interact with the water, casting a line into the water (either into Graythorne Lake or Graythorne River) and start the Fishing Minigame. The player could also use a Fishing Net, which will automatically catch fish after 2 seconds.

**<u>Mechanic:</u>** After casting the line into the water, the player will wait for a random period of time (between 0.5 and 3.5 seconds). After the random time has passed, their is a chance that the Fishing Minigame will appear (that probability is determined by the quality of the Fishing Rod). The Fishing Minigame shows a timer bar below two gears. Each gear has a dial pointing out from the gear's central point. In order to complete the minigame, the player will need to rotate both gears so their dials are facing each other before the timer runs out.

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
