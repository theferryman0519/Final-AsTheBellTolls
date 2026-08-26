---
Title: Code Setup / Initialization Order
Game: As The Bell Tolls
Owner: Carey Clement Jr
Created: August, 2026
Updated: August, 2026
Version: 0.0.1
---

## Information

- Game initialization occurs in a controlled order before normal Gameplay begins.
- Systems should not depend on Unity Script Execution Order for project-wide initialization.
- Systems that require another system must initialize only after that dependency is ready.
- Player input remains disabled until required initialization and scene loading are complete.

---

## Initialization Order

### 1. Bootstrap

- Create or locate persistent core systems.
- Set the Game State to Transition.
- Disable normal Gameplay input.
- Begin a New Game or Load Game request.

### 2. Data Registry

- Load authored Scriptable Objects.
- Build Data ID lookup dictionaries.
- Validate duplicate and missing IDs.
- Mark the Data Registry as ready.

### 3. Save

- Read save-slot metadata when loading an existing game.
- Read GameSaveData.
- Validate Save Version.
- Apply required Save Migrations.
- Keep Save Data available for domain reconstruction.

### 4. Core State

- Initialize Game Flow state.
- Initialize Calendar.
- Initialize Time.
- Initialize Weather and forecast state.
- Initialize Economy.

### 5. Persistent Progression

- Initialize Progression and unlocks.
- Initialize Restoration progress.
- Initialize Quest progress.
- Initialize Game Event and Bond Event progress.
- Initialize Time Manipulation progress.

### 6. Player Data

- Initialize Player state.
- Initialize Inventory.
- Initialize Tools.
- Initialize active Tonic buffs.
- Restore player customization.

### 7. World Data

- Load the required Gameplay Scene.
- Initialize persistent World flags and resource state.
- Initialize Farming state.
- Initialize Gathering resource state.
- Initialize Animals.
- Initialize Inventions.
- Initialize Mail.

### 8. NPC Data

- Initialize NPC runtime Models.
- Initialize Relationships.
- Resolve current NPC locations.
- Select NPC Routines.
- Initialize NPC Navigation after the scene is ready.

### 9. Presentation

- Initialize Audio for the active location, time, and weather.
- Initialize Camera state.
- Initialize HUD and UI presentation.
- Refresh visible gameplay information from current runtime state.

### 10. Gameplay Ready

- Validate that required systems report ready.
- Set the appropriate Game State.
- Enable the appropriate Input Map.
- Publish a Gameplay Ready event if required.
- Begin normal time progression and gameplay behavior.

---

## New Game Initialization

When starting a New Game:

1. Initialize authored Data Registry.
2. Create GameSaveData using current defaults.
3. Create runtime Models from authored defaults.
4. Apply starting Player, Calendar, Time, Inventory, Tool, progression, and world values.
5. Load the starting Gameplay Scene.
6. Initialize NPCs and world systems.
7. Initialize presentation.
8. Enter Gameplay when the opening flow permits it.

---

## Load Game Initialization

When loading an existing game:

1. Initialize authored Data Registry.
2. Read the selected save slot.
3. Validate and migrate Save Data.
4. Resolve Data IDs through the Data Registry.
5. Reconstruct runtime Models.
6. Load the saved Gameplay Scene or required starting scene.
7. Restore world and NPC state.
8. Initialize presentation.
9. Enter Gameplay only after loading is complete.

---

## Scene Transition Order

Scene transitions do not repeat full game initialization.

1. Set Game State to Transition.
2. Disable normal Gameplay input.
3. Preserve persistent runtime Models.
4. Unload or leave the current Gameplay Scene.
5. Load the destination Gameplay Scene.
6. Resolve scene-specific world references.
7. Reposition Player, NPCs, and required world objects.
8. Refresh Camera, Audio, and UI state.
9. Set the appropriate Game State.
10. Re-enable the appropriate Input Map.

---

## Day End Order

1. Set Game State to Day End.
2. Prevent new normal gameplay interactions.
3. Process relationship changes.
4. Process timing progression.
5. Process End of Day Selling.
6. Prepare Tomorrow's Events.
7. Advance Calendar and required daily state.
8. Generate or advance Weather forecast data.
9. Reset daily NPC and player interaction flags.
10. Create current GameSaveData.
11. Perform the automatic Save.
12. Load or restore the player for the new morning.
13. Enter Gameplay.

---

## Initialization Rules

- Data Registry must be ready before resolving saved Data IDs.
- Save migration must finish before runtime Models are reconstructed.
- Calendar, Time, and Weather should be ready before NPC Routines are selected.
- World scene references must be ready before NPC Navigation begins.
- Inventory must be ready before systems attempt to grant or remove Items.
- Relationships and Quests must be ready before Game Event eligibility is evaluated.
- UI should initialize after the gameplay state it displays is available.
- Gameplay input must not activate before initialization is complete.
