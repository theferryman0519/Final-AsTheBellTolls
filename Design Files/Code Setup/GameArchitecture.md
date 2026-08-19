---
Title: Code Setup / Game Architecture
Game: As The Bell Tolls
Owner: Carey Clement Jr
Created: August, 2026
Updated: August, 2026
Version: 0.0.1
---

## Information

- *As The Bell Tolls* uses a modular, system-driven architecture designed around clear ownership of gameplay data and responsibilities.
- Gameplay rules and authoritative runtime state are primarily owned by Systems.
- Controllers connect Unity-facing behavior to Systems without becoming the source of gameplay rules or persistent state.
- Services perform reusable operations that do not need to own long-lived gameplay state.
- ScriptableObjects define static game content and configuration.
- Models represent structured runtime or transferable data.
- Event Channels allow independent areas of the game to react to important changes without unnecessary direct dependencies.
- Save Data represents the persistent state required to reconstruct a game session.
- Dependencies organize code into major responsibility domains and establish allowed architectural relationships.
- Each major gameplay concept should have one clear authoritative owner.
- Different architectural layers should communicate through defined interfaces, methods, requests, results, or Event Channels rather than directly manipulating one another's internal data.

---

# Architectural Goals

The architecture should prioritize:

- Clear ownership of gameplay state.
- Separation between gameplay logic and Unity presentation.
- Low coupling between unrelated gameplay features.
- Predictable dependency direction.
- Testable gameplay logic.
- Replaceable or extendable systems.
- Data-driven content.
- Controlled communication between systems.
- Reliable Save and Load behavior.
- Explicit initialization.
- Minimal use of global mutable state.
- Minimal duplication of gameplay data.
- Easy debugging of gameplay state changes.
- Support for future content without requiring major architectural rewrites.

---

# High-Level Architecture

The project is divided conceptually into the following layers:

1. Data Definitions
2. Runtime Gameplay
3. Coordination
4. Unity Presentation
5. Cross-System Communication
6. Persistence
7. Infrastructure

General flow:

`Input / Unity Event`

↓

`Controller`

↓

`System or Service`

↓

`Authoritative Runtime State`

↓

`Event Channel`

↓

`Other Systems / Controllers / UI`

Persistent state is converted to and from:

`Save Data`

Static content is provided by:

`ScriptableObjects / Data Registries`

---

# Architecture Layers

## Data Definitions

Purpose:

Defines the static content, identifiers, structures, and configuration used by gameplay code.

Includes:

- Data IDs
- Enums
- Models
- ScriptableObjects
- Data registries
- Configuration data
- Definitions
- Shared value types

Examples:

- Item definitions
- NPC definitions
- Quest definitions
- Festival definitions
- Crop definitions
- Invention definitions
- Dialogue definitions
- Location definitions
- Weather configuration
- Restoration definitions

Primary Dependency:

`AsTheBellTolls.Data`

Rules:

- Static content should generally be represented by ScriptableObjects or equivalent definitions.
- Data definitions should not contain active gameplay state.
- Data definitions should not directly control GameObjects.
- Runtime gameplay Systems may reference data definitions.
- Save Data should store stable IDs rather than direct ScriptableObject references whenever persistent identification is required.

---

## Runtime Gameplay

Purpose:

Owns authoritative gameplay state and implements gameplay rules.

Primary Components:

- Systems

Examples:

- Time System
- Calendar System
- Weather System
- Inventory System
- Economy System
- Stamina System
- Farming System
- Fishing System
- Quest System
- NPC Friendship System
- NPC Connection System
- Restoration System
- Game Event System

Rules:

- Every authoritative gameplay value should have one primary owning System.
- Systems may query other Systems when required by gameplay rules.
- Systems may request actions from other Systems when ownership requires it.
- Systems should expose controlled methods rather than allowing external code to directly modify their internal state.
- Systems should publish Event Channels after meaningful state changes when other areas need to react.
- Systems should not directly manipulate UI.
- Systems should not require scene-specific GameObject references unless the System specifically owns Unity-facing runtime behavior.
- Systems should be designed so gameplay state can be reconstructed from Save Data.

---

## Coordination

Purpose:

Coordinates operations that require several gameplay or Unity components without transferring ownership of their underlying state.

Primary Components:

- Controllers
- High-level orchestration
- Game Flow code

Examples:

- Game Controller
- Scene Controller
- Day End Controller
- Farming Controller
- Dialogue Controller
- Festival Controller
- Save Controller

Rules:

- Controllers coordinate actions but do not become authoritative owners of gameplay data.
- Controllers may receive player Input or Unity callbacks.
- Controllers may request actions from Systems.
- Controllers may invoke Services.
- Controllers may update Unity presentation.
- Controllers may subscribe to Event Channels.
- Controllers should not duplicate rules already owned by Systems.
- Controllers should not become general-purpose managers containing unrelated functionality.

---

## Unity Presentation

Purpose:

Represents and displays gameplay state through Unity GameObjects, UI, animation, audio, visual effects, and other scene-facing components.

Includes:

- Views
- UI
- HUD
- Animators
- GameObject presentation
- Visual effects
- Audio presentation
- Scene presentation

Examples:

- Player character GameObject
- NPC GameObjects
- Crop visuals
- Resource visuals
- HUD
- Inventory Menu
- Relationships Menu
- Dialogue UI
- Quest UI
- Calendar UI
- Day End screens

Rules:

- Presentation should display state rather than own authoritative gameplay state.
- Presentation may cache temporary visual state when required.
- UI should request gameplay actions through Controllers, Systems, or appropriate Services.
- UI should not directly edit gameplay Models belonging to Systems.
- Presentation should respond to Event Channels or controlled state queries when gameplay data changes.
- Destroying or reloading a presentation object should not destroy persistent gameplay state.

---

## Cross-System Communication

Purpose:

Allows separate areas of the architecture to communicate without unnecessary coupling.

Primary Components:

- Event Channels
- Requests
- Results
- Interfaces
- Controlled direct System calls

Rules:

- Direct calls should be used when one component explicitly requires an action or answer from another component.
- Event Channels should be used when a completed change needs to be announced to potentially multiple listeners.
- Event Channels should not replace normal method calls for required gameplay operations.
- Events should describe what happened rather than who should respond.
- Event payloads should contain only the information required for listeners to react.
- Event Channels should not become alternate owners of gameplay state.
- Important gameplay behavior should not depend on an undocumented chain of events.

Example direct action:

`Crafting System`

↓

`Inventory System.TryConsumeItems(...)`

↓

`Crafting System creates crafted result`

↓

`Inventory System.AddItem(...)`

Example notification:

`Inventory System`

↓

`InventoryChanged Event Channel`

↓

`HUD / Inventory UI / Quest System`

---

## Persistence

Purpose:

Stores and restores the state required for persistent game progression.

Primary Components:

- Save System
- Save Data
- Save Versioning
- Save migration
- Serialization

Rules:

- Save Data represents persistent state rather than live gameplay behavior.
- Systems remain the authoritative runtime owners of their gameplay state.
- Systems export their persistent state when saving.
- Systems restore their runtime state from loaded Save Data.
- Controllers should not directly modify Save Data.
- ScriptableObject definitions should not be duplicated into Save Data when stable IDs can reference them.
- Temporary presentation state should not be saved unless it affects gameplay after loading.
- Save files should include an explicit Save Version.
- Older Save Data should be migrated when necessary.

General flow when saving:

`Systems`

↓

`Persistent State Snapshot`

↓

`Save Data`

↓

`Serialization`

↓

`Save File`

General flow when loading:

`Save File`

↓

`Deserialization`

↓

`Save Data`

↓

`Migration if required`

↓

`Systems restore runtime state`

↓

`Presentation refreshes`

---

## Infrastructure

Purpose:

Provides foundational functionality used by multiple gameplay areas without representing specific game mechanics.

Includes:

- Initialization
- Input
- Scene loading
- Audio infrastructure
- Camera infrastructure
- Data registries
- Common utilities
- Validation
- Shared interfaces
- Dependency wiring

Rules:

- Infrastructure should remain as gameplay-independent as reasonably possible.
- Infrastructure should not become the owner of unrelated gameplay state.
- Infrastructure dependencies should generally point toward Core and Data rather than individual high-level gameplay features.

---

# Primary Architectural Components

## Systems

Systems are the primary owners of gameplay behavior and authoritative runtime state.

A System should generally exist when a feature:

- Has meaningful gameplay rules.
- Owns persistent or runtime state.
- Must coordinate several related operations.
- Needs to be queried by other gameplay features.
- Must continue existing independently of one specific GameObject or UI screen.

Examples:

- `TimeSystem`
- `InventorySystem`
- `QuestSystem`
- `NpcFriendshipSystem`
- `RestorationSystem`

Systems should answer questions such as:

- What is the current value?
- Is this action allowed?
- What happens when this action succeeds?
- How does this gameplay state change?
- What persistent data belongs to this feature?

---

## Controllers

Controllers connect Unity-facing actions and presentation to gameplay logic.

A Controller should generally exist when a feature:

- Receives Unity callbacks.
- Receives player Input.
- Coordinates GameObjects.
- Coordinates UI.
- Bridges a gameplay System with scene behavior.
- Coordinates several presentation elements around a gameplay action.

Examples:

- `PlayerController`
- `FarmingController`
- `DialogueController`
- `FestivalController`
- `InventoryUiController`

Controllers should answer questions such as:

- What Unity behavior should happen now?
- What gameplay request should be sent?
- Which presentation should be updated?
- Which GameObject should respond to this gameplay result?

Controllers should not answer questions such as:

- How much Friendship does this NPC have?
- Does the player own this Item?
- Is this Quest completed?
- What is the current Weather?
- How much Stamina remains?

Those values belong to their owning Systems.

---

## Services

Services provide reusable operations that generally do not own long-lived gameplay state.

A Service should generally exist when:

- An operation is reusable across several features.
- The operation has a clear input and output.
- The operation does not need to own a persistent gameplay lifecycle.
- Extracting the operation reduces duplicated logic.

Examples may include:

- Validation
- Formatting
- Path calculations
- Probability calculations
- Data lookup
- Serialization helpers
- Localization lookup
- Platform-specific operations

Services should generally be accessed through interfaces when the implementation may need to change or be replaced.

A Service should not be created merely to rename a System or hide every method call behind another class.

---

## Models

Models represent structured runtime information.

Models may represent:

- Runtime state.
- Request data.
- Result data.
- Temporary calculated data.
- Data transferred between architectural layers.

Examples:

- Inventory entry
- Calendar date
- Friendship state
- Quest state
- Shop transaction result
- Interaction result

Models should not automatically become globally shared mutable objects.

When a System owns a Model, external code should not modify that Model without going through the owning System.

---

## ScriptableObjects

ScriptableObjects define static or designer-authored game content.

ScriptableObjects are appropriate for:

- Items
- Crops
- NPC definitions
- Dialogue definitions
- Quests
- Festivals
- Inventions
- Recipes
- Buildings
- Locations
- Audio definitions
- Configuration values

ScriptableObjects should generally answer:

"What is this piece of content?"

They should generally not answer:

"What is the player's current state for this content?"

Example:

`QuestDefinition`

May contain:

- Quest ID
- Display Name
- Description
- Requirements
- Rewards
- Definition of objectives

`QuestSystem`

Owns:

- Whether the Quest is available.
- Whether the Quest is active.
- Current objective progress.
- Whether the Quest is completed.

---

## Event Channels

Event Channels broadcast meaningful changes or presentation requests between independent components.

Examples:

- `TimeChanged`
- `DayChanged`
- `WeatherChanged`
- `InventoryChanged`
- `FriendshipChanged`
- `QuestCompleted`
- `RestorationStateChanged`

Event Channels are primarily used after an authoritative change has already occurred.

Example:

1. `InventorySystem.AddItem()` validates the request.
2. `InventorySystem` changes its authoritative Inventory state.
3. `InventorySystem` raises `InventoryChanged`.
4. Interested listeners refresh or react.

The Event Channel does not own the Inventory and does not determine whether the Item may be added.

Related Notes:

- Event Channels
- System Interaction Rules

---

## Save Data

Save Data contains serializable persistent values required to restore the player's game.

Examples:

- Current date
- Current time
- Inventory contents
- Bellnotes
- Stamina
- Friendship progression
- Connection progression
- Quest progression
- Restoration progression
- Unlocked content
- Game Flags
- Mail state
- Farming state

Save Data should store identifiers and values rather than gameplay behavior.

Related Notes:

- Save Data
- Save Versioning
- Data IDs
- Game Flags

---

# Static Data vs Runtime State

Static Data describes content that exists in the game.

Runtime State describes what is currently true during a specific playthrough.

Example:

Static Item Data:

`ItemDefinition`

- ID
- Display Name
- Description
- Category
- Sell Value
- Icon
- Maximum Stack Size

Runtime Inventory State:

`InventoryEntryModel`

- Item ID
- Quantity
- Quality

---

Example:

Static NPC Data:

`NpcDefinition`

- NPC ID
- Display Name
- Birthday
- Profession
- Residence
- Gift preferences
- Personality data

Runtime NPC State:

- Friendship points
- Connection progression
- Current mood
- Current routine
- Current location
- Marriage state

---

Example:

Static Quest Data:

`QuestDefinition`

- Quest ID
- Requirements
- Objectives
- Rewards

Runtime Quest State:

- Locked
- Available
- Active
- Objective progress
- Completed

---

# Source of Truth

Each important gameplay value must have one authoritative source.

Examples:

| Gameplay State | Authoritative Owner |
|---|---|
| Current Game State | Game State System |
| Current Time | Time System |
| Current Date | Calendar System |
| Current Weather | Weather System |
| Forecast Weather | Weather Forecast System |
| Player Stamina | Stamina System |
| Player Inventory | Inventory System |
| Player Bellnotes | Economy System |
| Quest Progress | Quest System |
| Friendship Progress | NPC Friendship System |
| Connection Progress | NPC Connection System |
| NPC Routine Selection | NPC Routine System |
| NPC Navigation | NPC Navigation System |
| Restoration Progress | Restoration System |
| Current Tool State | Tool System |
| Farming State | Farming System |
| Marriage State | Marriage System |
| Game Event Progress | Game Event System |
| Current Scene State | Scene System |

Other components may display, cache, or react to these values, but should not independently own competing copies of them.

---

# Dependency Direction

Dependencies should generally flow from more specific code toward more foundational abstractions.

General direction:

`Presentation`

↓

`Controllers`

↓

`Systems / Services`

↓

`Data / Core`

Event Channels may allow communication across dependency boundaries when direct coupling is undesirable.

Persistence operates alongside runtime Systems:

`Save System ↔ Runtime Systems`

Static content is supplied to runtime Systems:

`ScriptableObjects / Registries → Systems`

---

# Dependency Rules

- Core must not depend on gameplay-specific dependencies.
- Data should avoid depending on high-level gameplay implementations.
- Systems may depend on Core, Data, appropriate Services, and explicitly related Systems.
- Controllers may depend on the Systems they coordinate.
- UI may depend on Controllers, presentation Models, interfaces, or read-only gameplay queries.
- Systems should not depend on UI.
- Systems should not require a specific UI screen to exist.
- Systems should not depend on unrelated Controllers.
- Services should not create circular dependencies between Systems.
- Event Channels should not be used solely to hide an otherwise appropriate direct dependency.
- Circular dependencies should be avoided.
- Shared functionality should be moved into an appropriate lower-level abstraction when two components would otherwise require circular references.

Related Notes:

- Dependencies

---

# Communication Rules

## Use Direct Calls When

Use a direct call when:

- A response is immediately required.
- Validation must occur before continuing.
- One System explicitly owns the requested action.
- Failure must be returned to the caller.
- Execution order is important.

Examples:

`InventorySystem.HasItem()`

`InventorySystem.TryRemoveItem()`

`EconomySystem.TrySpend()`

`StaminaSystem.TryConsume()`

`QuestSystem.CanStartQuest()`

---

## Use Event Channels When

Use an Event Channel when:

- A state change has already happened.
- Multiple independent listeners may care.
- The sender should not need to know the listeners.
- The message represents a notification rather than authoritative execution.

Examples:

`InventoryChanged`

`WeatherChanged`

`QuestCompleted`

`FriendshipLevelChanged`

`RestorationStateChanged`

---

## Use Services When

Use a Service when:

- A reusable operation does not need to own gameplay state.
- Several features require the same operation.
- An external or platform-specific operation needs an abstraction.

---

## Use Controllers When

Use a Controller when:

- Unity-facing behavior must be coordinated.
- Input needs to trigger gameplay behavior.
- GameObjects need to react to gameplay state.
- UI interaction must be translated into gameplay requests.

---

# Request and Result Pattern

Gameplay actions that may succeed or fail should return an explicit result when useful.

Example:

`CraftingController`

↓

Requests:

`CraftingSystem.TryCraft(recipeId)`

The Crafting System may:

1. Find the recipe definition.
2. Validate whether the recipe is unlocked.
3. Validate required ingredients.
4. Request ingredient consumption from Inventory.
5. Produce the crafted Item.
6. Add the Item to Inventory.
7. Update any relevant progression.
8. Return a result.

Possible result information:

- Success
- Failure reason
- Crafted Item ID
- Quantity
- Quality
- Consumed ingredients

The Controller may then determine which Unity presentation should occur.

---

# Gameplay Action Example

## Farming Action

Player Input:

`Use Tool`

↓

`PlayerInteractionController`

↓

`FarmingController`

↓

`FarmingSystem`

The Farming System may query:

- Tool System
- Stamina System
- Inventory System
- Weather or Calendar data when required

If the action succeeds:

- Farming System updates farming state.
- Required Stamina is consumed.
- Required Item may be consumed.
- Relevant Event Channels are raised.
- Farming Controller updates scene presentation.
- HUD responds to Stamina or Inventory changes.

No UI element directly modifies Farming, Inventory, or Stamina state.

---

# Interaction Example

## NPC Interaction

Player enters interaction range.

↓

`PlayerInteractionController`

identifies:

`NpcController`

↓

Interaction request reaches:

`InteractionSystem`

↓

Interaction System determines available interaction.

↓

`DialogueController`

requests appropriate dialogue from:

`DialogueSystem`

Dialogue System may query:

- NPC Friendship System
- NPC Connection System
- Calendar System
- Time System
- Weather System
- Quest System
- Game Flags
- NPC data definitions

↓

Dialogue result is presented through UI.

The Dialogue UI displays the result but does not determine NPC Friendship, Quest, Calendar, or Weather state.

---

# Quest Progression Example

Gameplay action occurs.

↓

Owning Gameplay System completes the action.

↓

Relevant Quest progress is reported or observed.

↓

`QuestSystem`

validates active Quest objectives.

↓

Quest progress changes.

↓

`QuestUpdated`

or:

`QuestCompleted`

is raised.

↓

Possible listeners:

- Quest UI
- HUD
- Game Event System
- Tutorial System
- Audio
- Progression-related presentation

Quest System remains the authoritative owner of Quest progression.

---

# Day End Architecture

Day End is a coordinated multi-system process rather than ownership by one giant manager.

Primary orchestrator:

`DayEndSystem`

General sequence:

1. Time System reaches midnight.
2. Day End is requested.
3. Game State changes to Day End.
4. Normal gameplay interaction is stopped.
5. End-of-day Systems process required state.
6. Relationship changes are prepared.
7. Timing Progress is prepared.
8. Day End Selling is processed.
9. Tomorrow's events are prepared.
10. Day End presentation is displayed.
11. Calendar advances.
12. Daily gameplay state resets.
13. Weather advances.
14. NPC routines are prepared for the new day.
15. Save System performs the automatic Save.
16. Game State returns to normal Gameplay.
17. The new day begins.

The Day End System coordinates this process but does not take ownership of Inventory, Economy, Relationships, Calendar, Weather, or other participating System data.

Related Notes:

- System Interaction Rules
- Initialization Order
- Save Data

---

# Scene Architecture

Unity scenes should contain presentation and scene-specific objects rather than becoming the authoritative storage location for gameplay progression.

Persistent gameplay state should survive scene transitions through the appropriate Systems.

Scene changes should generally follow:

1. A scene transition is requested.
2. Scene System validates or begins the transition.
3. Game State prevents normal interaction.
4. Scene Controller begins transition presentation.
5. Unity scene loading occurs.
6. Scene-specific references are initialized.
7. Player placement is resolved.
8. NPC and world presentation are synchronized with current gameplay state.
9. Camera and Audio are configured.
10. Transition presentation ends.
11. Normal gameplay resumes.

Scene reloads should reconstruct presentation from authoritative runtime state rather than relying on the previous scene instance.

---

# GameObject Architecture

GameObjects should represent Unity-facing objects rather than act as persistent gameplay databases.

GameObjects may contain:

- Controllers
- Views
- Colliders
- Animators
- Renderers
- Audio sources
- Unity interaction components
- Scene references

GameObjects should generally not contain:

- Independent copies of persistent Inventory data.
- Independent Friendship progression.
- Independent Quest progression.
- Independent Economy state.
- Independent Calendar progression.
- Permanent unlock state that belongs to a System.

A scene object may contain a stable Data ID so the appropriate System can determine its current gameplay state.

Example:

A resource node GameObject contains:

`resource_node_blackmere_forest_001`

The Resource Respawn System determines whether that resource is currently available.

The GameObject displays the resulting state.

---

# UI Architecture

UI should follow a presentation-oriented architecture.

General flow:

`Player Input`

↓

`UI Controller`

↓

`System request`

↓

`System result or state change`

↓

`UI refresh`

UI may query read-only state for display.

UI should not directly mutate authoritative gameplay state.

Example:

Inventory Menu:

`InventoryUiController`

queries:

`InventorySystem`

and displays:

- Item entries
- Quantities
- Quality
- Capacity
- Selected Item information

If the player performs an action:

`InventoryUiController`

requests the action through the appropriate gameplay API.

The UI does not directly modify the Inventory collection.

---

# Input Architecture

Input should be interpreted separately from gameplay rules.

General flow:

`Unity Input System`

↓

`Input System`

↓

`Controller`

↓

`Gameplay System`

Examples:

Movement Input:

`Input System`

↓

`PlayerMovementController`

↓

`PlayerMovementSystem`

Interaction Input:

`Input System`

↓

`PlayerInteractionController`

↓

`Interaction System`

Tool Input:

`Input System`

↓

`ToolController`

↓

`Tool System / Appropriate Gameplay System`

This separation allows:

- Keyboard controls
- Xbox controls
- PlayStation controls
- Nintendo Switch controls
- Input rebinding
- Dynamic HUD prompts

to share the same gameplay behavior.

---

# Initialization Architecture

Game initialization must occur in a controlled order so Systems do not operate before required dependencies and data are available.

High-level initialization:

1. Core infrastructure becomes available.
2. Static game data and registries are loaded.
3. Event Channels become available.
4. Required Services are initialized.
5. Gameplay Systems are constructed or initialized.
6. Save Data is loaded or a new game state is created.
7. Systems restore their runtime state.
8. Scene-specific presentation is initialized.
9. Controllers receive required references.
10. Event listeners subscribe.
11. UI synchronizes with current gameplay state.
12. Game State enters the appropriate playable state.

Detailed ordering belongs in:

- Initialization Order

---

# Lifetime Categories

Different objects may have different lifetimes.

## Application Lifetime

Exists for the duration of the running application.

Examples may include:

- Core services
- Data registries
- Save infrastructure
- Global configuration

---

## Game Session Lifetime

Exists while a Save File is active.

Examples may include:

- Time System
- Calendar System
- Inventory System
- Economy System
- Quest System
- Friendship System
- Restoration System
- Game State System

These contain runtime state belonging to the active playthrough.

---

## Scene Lifetime

Exists only while a particular Unity scene is loaded.

Examples:

- Scene-specific Controllers
- NPC GameObjects
- Resource GameObjects
- Door GameObjects
- Local Audio Sources
- Scene cameras
- World presentation objects

---

## UI Lifetime

May exist globally or only while a screen is active.

Examples:

- HUD
- Inventory Menu
- Relationships Menu
- Dialogue window
- Day End presentation

UI lifetime must not determine the lifetime of authoritative gameplay state.

---

# Data ID Architecture

Stable string IDs identify designer-authored content and persistent world entities.

Examples:

`npc_lockwood_adrian`

`item_herb_peppermint`

`quest_tutorial_example`

`location_blackmere_town-square`

IDs allow:

- Save Data to reference content.
- Systems to communicate without direct asset references.
- Data registries to resolve definitions.
- Content to remain identifiable across sessions.
- Save migrations to map old content when required.

Runtime code should avoid using Display Names as persistent identifiers.

Related Notes:

- Data IDs

---

# Game Flag Architecture

Game Flags represent discrete persistent facts that are needed across otherwise independent gameplay areas.

Examples may include:

- Story milestones.
- One-time introductions.
- Important world changes.
- Special NPC states.
- Tutorial milestones.
- Unlock conditions not already owned by a more specialized System.

Game Flags should not replace structured Systems.

Example:

Use:

`NpcFriendshipSystem.GetFriendshipLevel(npcId)`

Rather than creating:

`adrian_friendship_level_5`

unless a separate one-time fact specifically needs to be recorded.

Related Notes:

- Game Flags

---

# Architectural Ownership Rules

- Every persistent gameplay value must have an identifiable owner.
- Only the owning System should directly modify its authoritative state.
- Other Systems should request changes through the owner.
- Controllers coordinate but do not become substitute Systems.
- ScriptableObjects describe content but do not become per-save runtime state.
- Models structure data but do not automatically own that data.
- Services perform operations but should not silently become gameplay state owners.
- Event Channels announce changes but do not own those changes.
- Save Data stores persistent state but is not the live authority during gameplay.
- UI displays and requests actions but does not own gameplay state.
- Scene GameObjects present world state but should not independently decide persistent progression.

---

# Anti-Patterns

The following patterns should be avoided.

## God Managers

Avoid classes such as:

`GameManager`

containing:

- Inventory
- Quests
- Time
- Weather
- Friendship
- Farming
- Saving
- UI
- Audio

A high-level Game Controller may coordinate application flow, but gameplay responsibilities should remain separated into their appropriate Systems.

---

## Duplicate State

Avoid storing the same authoritative value in multiple places.

Bad example:

- Inventory System stores Bellnotes.
- Economy System stores Bellnotes.
- HUD stores Bellnotes.
- Save Controller stores Bellnotes.

Preferred:

`EconomySystem`

owns Bellnotes.

Other components query or react to it.

---

## UI Gameplay Logic

Avoid:

`InventoryButton.OnClick()`

directly editing an Inventory list.

Preferred:

`InventoryButton`

↓

`InventoryUiController`

↓

`InventorySystem`

---

## ScriptableObject Runtime Save State

Avoid changing shared ScriptableObject definitions to represent one Save File's progression.

Bad example:

`QuestDefinition.IsCompleted = true`

Preferred:

`QuestSystem`

stores completion state for the Quest ID.

---

## Event-Driven Everything

Avoid replacing all direct communication with events.

Bad:

`CraftRequested`

↓

unknown listener performs crafting

↓

`IngredientsRequested`

↓

unknown listener removes items

↓

`ItemCreationRequested`

Preferred:

`CraftingSystem.TryCraft()`

uses explicit required dependencies and publishes an event only after meaningful state changes.

---

## Controller-to-Controller Chains

Avoid long chains such as:

`PlayerController`

↓

`FarmingController`

↓

`InventoryController`

↓

`HudController`

Preferred gameplay communication should pass through authoritative Systems, Services, or appropriate Event Channels.

---

## Static Global State

Avoid globally mutable static classes for gameplay progression when the state belongs to the active Save File.

Static utility functions may be appropriate when they contain no mutable gameplay state.

---

## Scene-Owned Persistent State

Avoid requiring the same scene GameObject instance to survive forever in order to preserve gameplay state.

Persistent state belongs to Systems and Save Data.
