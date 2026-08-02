---
Title: Systems / Cooking System
Game: As The Bell Tolls
Owner: Carey Clement Jr
Created: July, 2026
Updated: July, 2026
Version: 0.0.1
---

## Information

**<u>Activating Mechanics:</u>** While the Manor's Kitchen is in its Weathered restoration stage, only the Pantry is available. The Stove and Blender do not become available until the Kitchen has been restored to its Rebuilding stage. Once available, the player will need to walk up to the Stove or Blender and interact with it. While at the Winding Banks Inn, the player will be able to have access to the Stove and Blender with Helen or with Theo once reaching at least 3 Hearts with one of them.

**<u>Mechanic:</u>** After interacting with the Stove or Blender, the Cooking Menu will pop up. Here, the player will be able to choose from an available recipe. Once that recipe has been selected, the Cooking Menu will show the player's stored consumable ingredient items (from their Inventory, Pantry, and any Storage Units). The player will be able to choose the appropriate ingredients available for the selected recipe to add to the Stove or Blender. Once all ingredients has been added, the Meal or Drink (depending if using the Stove or Blender) will be added to the player's Inventory. If the player does not have room in their Inventory, the meal will be added to their Pantry.

**<u>Other Information:</u>** Cooking does not take any amount of in-game time. Time pauses when the Cooking Menu is open and does not restart until after the Meal or Drink has completed. Recipes are only shown as available if they are both discovered and if the player has all necessary ingredient items. The player cannot cancel once confirming all ingredients added to the Stove or Blender.

---

## Example Flows

### Kitchen in Weathered Restoration Stage

- Player enters Kitchen in Pendrelle Manor.
- Kitchen is at Weathered restoration stage.
- Stove and Blender are not available to be used.

### Less than 3 Hearts with both Helen Holt and Theo Bennett

- Player enters Winding Banks Inn.
- Player interacts with Helen Holt.
- The option to Cook With Helen is not available.
- Player interacts with Theo Bennett.
- The option to Cook With Theo is not available.

### Cooking Fried Egg in Manor

- Player enters Kitchen in Pendrelle Manor.
- Kitchen is at or above Rebuilding restoration stage.
- Player walks to end adjacent to Stove.
- Player interacts with Stove.
- Cooking Menu opens with available recipes sorted to top of scroll view.
- Player selects Fried Egg recipe.
- Cooking Menu opens with available consumable ingredient items.
- Player selects Chicken Egg.
- Fried Egg is successfully created and added to the player's Inventory or Pantry (if Inventory is full).

### Making Cherry Cream Drink in Manor

- Player enters Kitchen in Pendrelle Manor.
- Kitchen is at or above Rebuilding restoration stage.
- Player walks to end adjacent to Blender.
- Player interacts with Blender.
- Cooking Menu opens with available recipes sorted to top of scroll view.
- Player selects Cherry Cream Drink recipe.
- Cooking Menu opens with available consumable ingredient items.
- Player selects Cherry, then selects Cow Milk.
- Cherry Cream Drink is successfully created and added to the player's Inventory or Pantry (if Inventory is full).

### Cooking Catfish Chowder with Helen Holt

- Player enters Winding Banks Inn and navigates to Helen Holt.
- Player speaks to Helen and is at or above 3 Hearts with her.
- After speaking, player selects Cook With Helen option.
- Cooking Menu opens with available recipes sorted to top of scroll view.
- Player selects Catfish Chowder recipe.
- Cooking Menu opens with available consumable ingredient items.
- Player selects Catfish, then selects Cow Milk, then selects Potato.
- Catfish Chowder is successfully created and added to the player's Inventory or Pantry (if Inventory is full).
- Player gains Friendship Points with Helen.

### Cooking Catfish Chowder with Theo Bennett

- Player enters Winding Banks Inn and navigates to Theo Bennett.
- Player speaks to Theo and is at or above 3 Hearts with him.
- After speaking, player selects Cook With Theo option.
- Cooking Menu opens with available recipes sorted to top of scroll view.
- Player selects Catfish Chowder recipe.
- Cooking Menu opens with available consumable ingredient items.
- Player selects Catfish, then selects Cow Milk, then selects Potato.
- Catfish Chowder is successfully created and added to the player's Inventory or Pantry (if Inventory is full).
- Player gains Friendship Points with Theo.
