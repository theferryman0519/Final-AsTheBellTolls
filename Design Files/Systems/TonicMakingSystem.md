---
Title: Systems / Tonic Making System
Game: As The Bell Tolls
Owner: Carey Clement Jr
Created: July, 2026
Updated: August, 2026
Version: 0.0.1
---

## Information

**<u>Activating Mechanics:</u>** While the Manor's Laboratory is below its Renewed restoration stage, the Tonic Kit is not available. It only becomes available once the Laboratory has been restored to its Renewed stage. Once available, the player will need to walk up to the Tonic Kit and interact with it. While at Rain & Hale, the player will be able to have access to the Tonic Kit with Julian once reaching at least 3 Hearts with him.

**<u>Mechanic:</u>** After interacting with the Tonic Kit, the Tonic Making Menu will pop up. Here, the player will be able to choose from an available tonic recipe. Once that recipe has been selected, the Tonic Making Menu will show the player's stored Herbs (from their Inventory and any Storage Units). The player will be able to choose the appropriate Herbs available for the selected recipe to add to the Tonic Kit. Once all Herbs have been added, the Tonic Making Minigame will appear. The player will need to rotate a gear in order to open the valve and create a flame under the flask that holds the tonic. The tonic will start as a red color, turning orange, then yellow, then green due to the flame. Once green, the player will have a limited time to shut off the fire before the tonic burns. The successfully-crafted tonic will be added to the player's Inventory. If the player does not have the room in their Inventory, the tonic will be mailed to the player the next morning.

**<u>Fail/Cancel State:</u>** If the player cancels the Tonic Making Minigame at any time, or if the player fails to successfully make the tonic, the player still loses the Herbs added to the Tonic Kit. The player may continue making tonics as long as they have the necessary Herbs available.

---

## Tonic Buff Types

- Gathering Double: Creates a 25% probability that a foraged, harvested, or Tool-collected item is doubled in its count.
- Gathering Quality: Creates a 25% probability that a foraged, harvested, or Tool-collected item has its quality increased by one tier (max Cobalt).
- Gathering Type: Increases the probability for gathering rarer foraged, harvested, or Tool-collected items by 25%.
- Social Increase: Increases all earned NPC Relationship Points by 2.
- Speed Increase: Increases the player movement speed by 25%.
- Stamina Max: Increases the player's maximum stamina by 10.
- Stamina Slow: Reduces all stamina-consuming actions by 1.

---

## Example Flows

### Laboratory below Renewed Stage

- Player enters Laboratory in Pendrelle Manor.
- Laboratory is below Renewed restoration stage.
- Tonic Kit is not available to be used.

### Less than 3 Hearts with Julian Hale

- Player enters Rain & Hale.
- Player interacts with Julian Hale.
- The option to Make Tonic With Julian is not available.

### Unsuccessful Tonic Making in Manor

- Player enters Laboratory in Pendrelle Manor.
- Laboratory is at or above Renewed restoration stage.
- Player walks to end adjacent to Tonic Kit.
- Player interacts with Tonic Kit.
- Tonic Making Menu opens with available recipes sorted to top of scroll view.
- Player selects Endurance Tonic recipe.
- Tonic Making Menu opens with available Herbs.
- Player selects Sage.
- Tonic Making Minigame appears.
- Player opens valve with fire, but does not shut it off in time.
- Sad emote icon appears above the player to signify no tonic was made and Sage was lost.

### Successful Tonic Making in Manor

- Player enters Laboratory in Pendrelle Manor.
- Laboratory is at or above Renewed restoration stage.
- Player walks to end adjacent to Tonic Kit.
- Player interacts with Tonic Kit.
- Tonic Making Menu opens with available recipes sorted to top of scroll view.
- Player selects Endurance Tonic recipe.
- Tonic Making Menu opens with available Herbs.
- Player selects Sage.
- Tonic Making Minigame appears.
- Player opens valve with fire and successfully shuts it off in time.
- Alert emote icon appears above the player to signify tonic was made.
- Player receives tonic in their Inventory.
