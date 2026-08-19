---
Title: Code Setup / Event Channels
Game: As The Bell Tolls
Owner: Carey Clement Jr
Created: August, 2026
Updated: August, 2026
Version: 0.0.1
---

## Information

- Event Channels provide decoupled communication between Systems, Controllers, UI, and other game features.
- Event Channels are primarily used when one dependency needs to announce that something happened without directly knowing every listener that may react.
- The System that owns gameplay data remains authoritative even when it raises an Event Channel.
- Event Channels should communicate changes, completed actions, presentation requests, or other cross-feature notifications.
- Event Channels should not replace normal method calls when one System clearly owns an action and the caller already depends on that System.
- Event Channels should not contain gameplay logic.
- Event Channels should not store persistent gameplay state.
- Event Channels should not be saved as part of Save Data.
- Listeners should query the authoritative System when they require information beyond the event payload.
- Event Channels should use strongly typed payloads when more information than a simple notification is required.
- Event Channels should be raised only after the authoritative change has successfully occurred unless the channel is explicitly a request channel.

---

# Event Channel Architecture

## Notification Channels

Notification Channels announce that something has already happened.

Examples:

- Game State changed.
- Time advanced.
- Weather changed.
- Inventory changed.
- Friendship changed.
- Quest progress changed.

Rules:

- Notification Channels are named using past-tense or changed-state terminology.
- Raising a Notification Channel does not transfer ownership of the underlying data.
- Listeners may update presentation, refresh cached Unity-facing information, or begin dependent behavior.
- A listener should not assume it is the only listener.
- Listener execution order should not determine gameplay correctness.

---

## Request Channels

Request Channels ask a decoupled Unity-facing feature to perform an action when a direct dependency would be undesirable.

Examples:

- Request a UI notification.
- Request a transition presentation.
- Request an audio cue.

Rules:

- Request Channels are used sparingly.
- Gameplay Systems should normally expose methods or interfaces for gameplay commands rather than receiving all commands through Event Channels.
- Request Channels should not be used when the caller requires an immediate return value.
- Request Channels should not be used for operations where execution order is critical unless the owning architecture explicitly coordinates that order.

---

## Namespace

`AsTheBellTolls.Events`

Event payloads that are specific to a dependency may remain within that dependency when appropriate, but reusable Event Channel infrastructure belongs to `AsTheBellTolls.Events`.

---

# Base Event Channels

## VoidEventChannel

Purpose:

Raises a notification that requires no payload.

Use When:

- The occurrence itself is sufficient information.
- Listeners can query the appropriate System if additional state is required.

Examples:

- Initialization completed.
- Day End completed.
- Save completed.

---

## TypedEventChannel<T>

Purpose:

Raises an event containing strongly typed event data.

Use When:

- Listeners need contextual information about what changed.
- Passing a payload prevents unnecessary repeated queries.
- The payload can remain small and immutable for the duration of the event.

Rules:

- Payloads should contain IDs, enums, primitive values, or small Models where possible.
- Payloads should avoid direct references to scene GameObjects unless the channel is explicitly Unity-facing.
- Payloads should not contain authoritative mutable collections owned by Systems.

---

# Core Game Flow Channels

## GameStateChanged

Asset:

`EC_GameStateChanged`

Payload:

`GameStateChangedEventData`

Contains:

- Previous Game State
- Current Game State

Raised By:

- Game State System

Used By:

- Game Controller
- Player Controller
- Player Movement Controller
- Player Interaction Controller
- NPC Controllers
- UI Controllers
- Camera Controller
- Audio Controller
- Time System

Purpose:

Notifies listeners that the high-level Game State has changed so Unity-facing behavior can be enabled, disabled, paused, or resumed appropriately.

---

## SceneTransitionStarted

Asset:

`EC_SceneTransitionStarted`

Payload:

`SceneTransitionEventData`

Contains:

- Previous Scene
- Destination Scene
- Destination Location ID when applicable

Raised By:

- Scene System

Used By:

- Scene Controller
- Player Controller
- UI Controller
- Camera Controller
- Audio Controller

Purpose:

Notifies Unity-facing features that a scene transition has begun.

---

## SceneTransitionCompleted

Asset:

`EC_SceneTransitionCompleted`

Payload:

`SceneTransitionEventData`

Contains:

- Previous Scene
- Current Scene
- Current Location ID when applicable

Raised By:

- Scene System

Used By:

- Game Controller
- Scene Controller
- Player Controller
- NPC Controllers
- Camera Controller
- Audio Controller
- UI Controller

Purpose:

Notifies listeners that the new Gameplay Scene has finished loading and normal scene-specific setup may occur.

---

## InitializationCompleted

Asset:

`EC_InitializationCompleted`

Payload:

None

Raised By:

- Game initialization flow

Used By:

- Game Controller
- UI Controller
- Audio Controller
- Other features that must wait until initialization is complete

Purpose:

Notifies listeners that required game initialization has completed.

---

# Time & Calendar Channels

## TimeChanged

Asset:

`EC_TimeChanged`

Payload:

`TimeChangedEventData`

Contains:

- Previous game time
- Current game time
- Current Daylight Type

Raised By:

- Time System

Used By:

- HUD System
- NPC Routine System
- NPC Navigation System
- Farming System
- Resource Respawn System when applicable
- Audio System
- Lighting or environment presentation

Purpose:

Notifies listeners when the displayed or gameplay-relevant game time changes.

---

## DaylightChanged

Asset:

`EC_DaylightChanged`

Payload:

`DaylightChangedEventData`

Contains:

- Previous Daylight Type
- Current Daylight Type

Raised By:

- Time System

Used By:

- HUD System
- Audio System
- NPC Routine System
- World lighting presentation
- Dialogue System

Purpose:

Notifies listeners when the current Dawn, Day, Dusk, or Night period changes.

---

## DateChanged

Asset:

`EC_DateChanged`

Payload:

`DateChangedEventData`

Contains:

- Previous Calendar Date
- Current Calendar Date

Raised By:

- Calendar System

Used By:

- HUD System
- Calendar UI Controller
- Festival System
- NPC Routine System
- Quest System
- Mail System
- Resource Respawn System

Purpose:

Notifies listeners that the active calendar date has changed.

---

## SeasonChanged

Asset:

`EC_SeasonChanged`

Payload:

`SeasonChangedEventData`

Contains:

- Previous Season
- Current Season
- Current Year

Raised By:

- Calendar System

Used By:

- Farming System
- Weather System
- Resource Respawn System
- NPC Routine System
- Audio System
- World environment presentation
- Calendar UI Controller

Purpose:

Notifies listeners that a new season has begun.

---

## DayEndStarted

Asset:

`EC_DayEndStarted`

Payload:

None

Raised By:

- Day End System

Used By:

- Day End Controller
- Game Controller
- UI Controller
- Player Controller

Purpose:

Notifies Unity-facing features that normal Gameplay has ended and the Day End sequence is beginning.

---

## DayEndCompleted

Asset:

`EC_DayEndCompleted`

Payload:

None

Raised By:

- Day End System

Used By:

- Day End Controller
- Game Controller
- Save Controller

Purpose:

Notifies listeners that Day End processing and presentation have completed.

---

# Weather Channels

## WeatherChanged

Asset:

`EC_WeatherChanged`

Payload:

`WeatherChangedEventData`

Contains:

- Previous Weather Type
- Current Weather Type

Raised By:

- Weather System

Used By:

- Farming System
- NPC Routine System
- Dialogue System
- Audio System
- World environment presentation
- HUD or Calendar UI when weather information is displayed

Purpose:

Notifies listeners that the currently active Weather has changed.

---

## WeatherForecastChanged

Asset:

`EC_WeatherForecastChanged`

Payload:

None

Raised By:

- Weather Forecast System

Used By:

- Calendar UI Controller
- Other forecast presentation

Purpose:

Notifies presentation features that forecast data should be refreshed from the Weather Forecast System.

---

# Player Channels

## StaminaChanged

Asset:

`EC_StaminaChanged`

Payload:

`StaminaChangedEventData`

Contains:

- Previous Stamina
- Current Stamina
- Maximum Stamina

Raised By:

- Stamina System

Used By:

- HUD System
- Player Controller when exhaustion behavior is required

Purpose:

Notifies listeners that the player's Stamina has changed.

---

## PlayerCustomizationChanged

Asset:

`EC_PlayerCustomizationChanged`

Payload:

None

Raised By:

- Player Customization System

Used By:

- Player Customization Controller
- Player Controller
- UI presentation using the player avatar

Purpose:

Notifies Unity-facing presentation that the player's appearance information has changed and should be refreshed.

---

## MovementSpeedChanged

Asset:

`EC_MovementSpeedChanged`

Payload:

`MovementSpeedChangedEventData`

Contains:

- Previous movement speed
- Current movement speed

Raised By:

- Player Movement System

Used By:

- Player Movement Controller

Purpose:

Notifies the Unity-facing movement Controller that the calculated player movement speed has changed.

---

# Inventory & Item Channels

## InventoryChanged

Asset:

`EC_InventoryChanged`

Payload:

`InventoryChangedEventData`

Contains:

- Inventory Storage Type
- Item ID when applicable
- Previous quantity when applicable
- Current quantity when applicable
- Change reason when useful

Raised By:

- Inventory System

Used By:

- Inventory UI Controller
- HUD System
- Gameplay Menu Controller
- Crafting interfaces
- Quest System when Item ownership affects objectives

Purpose:

Notifies listeners that the player's Inventory contents have changed.

---

## ItemObtained

Asset:

`EC_ItemObtained`

Payload:

`ItemObtainedEventData`

Contains:

- Item ID
- Quantity
- Item Quality
- Acquisition source when useful

Raised By:

- Inventory System after a successful Item addition

Used By:

- HUD notification presentation
- Achievement Ledger progression
- Quest System when acquisition itself is relevant
- Tutorial System

Purpose:

Announces a successful Item acquisition when the occurrence matters independently from the resulting Inventory total.

Notes:

- `InventoryChanged` remains the general Inventory refresh event.
- `ItemObtained` should not be raised for internal Inventory movement that does not represent acquiring a new Item.

---

## ItemRemoved

Asset:

`EC_ItemRemoved`

Payload:

`ItemRemovedEventData`

Contains:

- Item ID
- Quantity
- Removal reason when useful

Raised By:

- Inventory System after a successful Item removal

Used By:

- Quest System when possession affects objectives
- Tutorial System when relevant

Purpose:

Announces that Items left the player's owned Inventory when the removal occurrence matters independently from the resulting Inventory total.

---

## EquippedToolChanged

Asset:

`EC_EquippedToolChanged`

Payload:

`EquippedToolChangedEventData`

Contains:

- Previous Tool Type
- Current Tool Type

Raised By:

- Tool System

Used By:

- Tool Controller
- Player Interaction Controller
- HUD System
- Player animation presentation

Purpose:

Notifies listeners that the player's currently equipped Tool has changed.

---

# Economy Channels

## BellnotesChanged

Asset:

`EC_BellnotesChanged`

Payload:

`BellnotesChangedEventData`

Contains:

- Previous balance
- Current balance
- Change amount
- Change reason when useful

Raised By:

- Economy System

Used By:

- HUD System
- Shop Controller
- Loan Controller
- Gameplay Menus displaying Bellnotes

Purpose:

Notifies listeners that the player's Bellnote balance has changed.

---

## LoanChanged

Asset:

`EC_LoanChanged`

Payload:

`LoanChangedEventData`

Contains:

- Previous loan balance
- Current loan balance
- Loan state when applicable

Raised By:

- Loan System

Used By:

- Loan Controller
- UI presentation
- Quest System when loan milestones matter

Purpose:

Notifies listeners that the player's active loan data has changed.

---

# Interaction Channels

## InteractionTargetChanged

Asset:

`EC_InteractionTargetChanged`

Payload:

`InteractionTargetChangedEventData`

Contains:

- Interaction target ID when applicable
- Interaction action
- Required Input action
- Whether an interaction is currently available

Raised By:

- Player Interaction Controller or Interaction System according to final ownership of target selection

Used By:

- HUD System

Purpose:

Updates the HUD interaction prompt when the player's current interactable target changes.

Notes:

- This is intentionally Unity-facing because nearby target detection is performed by the Player Interaction Controller.
- The payload should avoid storing a scene GameObject when an ID or presentation model is sufficient.

---

## InteractionCompleted

Asset:

`EC_InteractionCompleted`

Payload:

`InteractionCompletedEventData`

Contains:

- Interaction target ID
- Interaction action
- Player-facing result when required

Raised By:

- Interaction System

Used By:

- Tutorial System
- Quest System when an interaction objective is active
- HUD notification presentation when required

Purpose:

Announces that a valid interaction has successfully completed.

---

# Character & Relationship Channels

## NpcRoutineChanged

Asset:

`EC_NpcRoutineChanged`

Payload:

`NpcRoutineChangedEventData`

Contains:

- NPC ID
- Previous Routine ID when applicable
- Current Routine ID

Raised By:

- NPC Routine System

Used By:

- NPC Controller
- NPC Navigation Controller

Purpose:

Notifies Unity-facing NPC behavior that the selected routine for an NPC has changed.

---

## NpcMoodChanged

Asset:

`EC_NpcMoodChanged`

Payload:

`NpcMoodChangedEventData`

Contains:

- NPC ID
- Previous mood
- Current mood

Raised By:

- NPC Mood System

Used By:

- Dialogue System
- NPC animation or presentation when mood affects presentation

Purpose:

Notifies listeners that an NPC's active mood has changed.

---

## FriendshipChanged

Asset:

`EC_FriendshipChanged`

Payload:

`FriendshipChangedEventData`

Contains:

- NPC ID
- Previous Friendship value
- Current Friendship value
- Previous Heart Level
- Current Heart Level

Raised By:

- NPC Friendship System

Used By:

- Relationships UI Controller
- Game Event System
- Dialogue System
- Quest System when Friendship requirements are relevant

Purpose:

Notifies listeners that Friendship progression with an NPC has changed.

---

## ConnectionChanged

Asset:

`EC_ConnectionChanged`

Payload:

`ConnectionChangedEventData`

Contains:

- NPC ID
- Previous Connection state or value
- Current Connection state or value
- Newly unlocked Connection Key when applicable

Raised By:

- NPC Connection System

Used By:

- Relationships UI Controller
- Game Event System
- Dialogue System
- Quest System

Purpose:

Notifies listeners that Connection progression with an NPC has changed.

---

## RelationshipStatusChanged

Asset:

`EC_RelationshipStatusChanged`

Payload:

`RelationshipStatusChangedEventData`

Contains:

- NPC ID
- Previous Relationship Status
- Current Relationship Status

Raised By:

- Relationship-owning System responsible for derived Relationship Status

Used By:

- Relationships UI Controller
- Dialogue System
- Marriage System
- Game Event System

Purpose:

Notifies listeners when an NPC relationship crosses into a new named Relationship Status.

---

## MarriageChanged

Asset:

`EC_MarriageChanged`

Payload:

`MarriageChangedEventData`

Contains:

- NPC ID
- Previous marriage state
- Current marriage state

Raised By:

- Marriage System

Used By:

- Relationships UI Controller
- Dialogue System
- NPC Routine System
- Family System
- Game Event System

Purpose:

Notifies listeners that the player's marriage state has changed.

---

# Quest & Game Event Channels

## QuestStarted

Asset:

`EC_QuestStarted`

Payload:

`QuestEventData`

Contains:

- Quest ID

Raised By:

- Quest System

Used By:

- Quest Controller
- Quest UI Controller
- HUD notification presentation
- Game Event System

Purpose:

Announces that a Quest has entered the active state.

---

## QuestProgressChanged

Asset:

`EC_QuestProgressChanged`

Payload:

`QuestProgressChangedEventData`

Contains:

- Quest ID
- Objective ID when applicable
- Previous progress
- Current progress

Raised By:

- Quest System

Used By:

- Quest Controller
- Quest UI Controller
- HUD notification presentation when required

Purpose:

Notifies listeners that an active Quest or one of its objectives has progressed.

---

## QuestCompleted

Asset:

`EC_QuestCompleted`

Payload:

`QuestEventData`

Contains:

- Quest ID

Raised By:

- Quest System

Used By:

- Quest Controller
- Quest UI Controller
- HUD notification presentation
- Game Event System
- Progression-related features

Purpose:

Announces that a Quest has been completed.

---

## GameEventStarted

Asset:

`EC_GameEventStarted`

Payload:

`GameEventEventData`

Contains:

- Game Event ID
- Participating NPC IDs when required

Raised By:

- Game Event System

Used By:

- Game Event Controller
- Game State System
- NPC Routine System
- Dialogue Controller
- Camera Controller
- Audio Controller

Purpose:

Announces that a scripted Game Event has begun.

---

## GameEventCompleted

Asset:

`EC_GameEventCompleted`

Payload:

`GameEventEventData`

Contains:

- Game Event ID
- Participating NPC IDs when required

Raised By:

- Game Event System

Used By:

- Game Event Controller
- Game State System
- NPC Routine System
- Quest System
- Save-related dirty-state tracking when applicable

Purpose:

Announces that a scripted Game Event has successfully completed.

---

# Festival Channels

## FestivalStarted

Asset:

`EC_FestivalStarted`

Payload:

`FestivalEventData`

Contains:

- Festival ID

Raised By:

- Festival System

Used By:

- Festival Controller
- Game State System
- NPC Routine System
- Audio Controller
- UI Controller

Purpose:

Announces that the active Festival has begun.

---

## FestivalCompleted

Asset:

`EC_FestivalCompleted`

Payload:

`FestivalEventData`

Contains:

- Festival ID

Raised By:

- Festival System

Used By:

- Festival Controller
- Game State System
- NPC Routine System
- Quest System when applicable

Purpose:

Announces that the active Festival has ended.

---

# Restoration & Progression Channels

## RestorationChanged

Asset:

`EC_RestorationChanged`

Payload:

`RestorationChangedEventData`

Contains:

- Restoration Target Type
- Restoration Target ID
- Previous Restoration Stage
- Current Restoration Stage
- Current restoration progress when applicable

Raised By:

- Restoration System

Used By:

- Restoration Controller
- UI presentation
- Quest System
- Game Event System
- NPC Dialogue System
- World presentation

Purpose:

Notifies listeners that a Manor room or Town building has changed restoration progress or stage.

---

## UnlockChanged

Asset:

`EC_UnlockChanged`

Payload:

`UnlockChangedEventData`

Contains:

- Unlock ID
- Whether the content is now unlocked

Raised By:

- System responsible for the authoritative unlock

Used By:

- UI presentation
- Map System
- Quest System
- Tutorial System
- Relevant gameplay Controllers

Purpose:

Notifies listeners that a persistent gameplay feature, location, recipe, ability, or other unlockable content has changed availability.

Notes:

- The authoritative unlock should remain with the System that owns that feature or its progression data.
- This channel is a notification and should not become a second unlock registry.

---

# Farming, Gathering & Husbandry Channels

## FarmingPlotChanged

Asset:

`EC_FarmingPlotChanged`

Payload:

`FarmingPlotChangedEventData`

Contains:

- Plot ID
- Change Type

Raised By:

- Farming System

Used By:

- Farming Controller
- World presentation
- Tutorial System when applicable

Purpose:

Notifies Unity-facing farming presentation that a plot's state has changed and should be refreshed.

---

## ResourceChanged

Asset:

`EC_ResourceChanged`

Payload:

`ResourceChangedEventData`

Contains:

- Resource ID
- Resource state

Raised By:

- Resource Respawn System or owning resource gameplay feature

Used By:

- Resource Controller
- World presentation

Purpose:

Notifies Unity-facing resource objects when a persistent or managed resource state changes.

---

## AnimalStateChanged

Asset:

`EC_AnimalStateChanged`

Payload:

`AnimalStateChangedEventData`

Contains:

- Animal ID
- Changed state category

Raised By:

- Husbandry System

Used By:

- Animal Controller
- Husbandry Controller
- UI presentation

Purpose:

Notifies listeners that gameplay state for an owned animal has changed.

---

# Crafting & Invention Channels

## RecipeUnlocked

Asset:

`EC_RecipeUnlocked`

Payload:

`RecipeUnlockedEventData`

Contains:

- Recipe ID
- Recipe Type

Raised By:

- Owning recipe progression feature

Used By:

- Crafting Controller
- Cooking Controller
- Tonic Making Controller
- UI presentation
- HUD notification presentation

Purpose:

Announces that the player has permanently unlocked a new recipe.

---

## InventionUnlocked

Asset:

`EC_InventionUnlocked`

Payload:

`InventionEventData`

Contains:

- Invention ID

Raised By:

- Invention System

Used By:

- Invention Controller
- Player Menu presentation
- HUD notification presentation

Purpose:

Announces that an Invention has become available to the player.

---

## InventionCompleted

Asset:

`EC_InventionCompleted`

Payload:

`InventionEventData`

Contains:

- Invention ID

Raised By:

- Invention System

Used By:

- Invention Controller
- Quest System
- Progression features
- HUD notification presentation

Purpose:

Announces that the player has completed an Invention.

---

# Activities Channels

## LibraryCollectionChanged

Asset:

`EC_LibraryCollectionChanged`

Payload:

None

Raised By:

- Library System

Used By:

- Library-related UI
- Achievement Ledger presentation

Purpose:

Notifies listeners that the player's Library-related collection progress has changed.

---

## MuseumCollectionChanged

Asset:

`EC_MuseumCollectionChanged`

Payload:

None

Raised By:

- Museum System

Used By:

- Museum-related UI
- Achievement Ledger presentation

Purpose:

Notifies listeners that Museum collection progress has changed.

---

# Mail Channels

## MailReceived

Asset:

`EC_MailReceived`

Payload:

`MailEventData`

Contains:

- Mail ID

Raised By:

- Mail System

Used By:

- HUD notification presentation
- Mailbox presentation
- Tutorial System when applicable

Purpose:

Announces that new Mail has become available to the player.

---

## MailStateChanged

Asset:

`EC_MailStateChanged`

Payload:

`MailEventData`

Contains:

- Mail ID

Raised By:

- Mail System

Used By:

- Mailbox presentation
- Related UI

Purpose:

Notifies listeners that the state of an existing Mail entry has changed.

---

# Save Channels

## SaveStarted

Asset:

`EC_SaveStarted`

Payload:

`SaveEventData`

Contains:

- Save Type
- Save Slot when applicable

Raised By:

- Save System

Used By:

- Save Controller
- UI presentation

Purpose:

Notifies presentation that a Save operation has begun.

---

## SaveCompleted

Asset:

`EC_SaveCompleted`

Payload:

`SaveEventData`

Contains:

- Save Type
- Save Slot when applicable

Raised By:

- Save System

Used By:

- Save Controller
- UI presentation

Purpose:

Notifies presentation that a Save operation completed successfully.

---

## SaveFailed

Asset:

`EC_SaveFailed`

Payload:

`SaveFailedEventData`

Contains:

- Save Type
- Save Slot when applicable
- Failure reason suitable for handling or logging

Raised By:

- Save System

Used By:

- Save Controller
- UI error presentation
- Logging

Purpose:

Notifies listeners that a Save operation failed.

---

# Presentation Request Channels

## NotificationRequested

Asset:

`EC_NotificationRequested`

Payload:

`NotificationRequestEventData`

Contains:

- Notification Type
- Localization or content ID
- Optional related Item, Quest, NPC, or feature ID
- Optional quantity or display value

Raised By:

- Systems or Controllers that need a non-blocking player notification

Used By:

- HUD System
- UI Controller

Purpose:

Requests a non-modal HUD notification without requiring gameplay features to directly depend on HUD implementation.

---

## AudioCueRequested

Asset:

`EC_AudioCueRequested`

Payload:

`AudioCueRequestEventData`

Contains:

- Audio ID
- Audio category when required
- Optional world position when required

Raised By:

- Systems and Controllers requiring decoupled audio feedback

Used By:

- Audio System
- Audio Controller

Purpose:

Requests audio playback when the requesting feature should not directly depend on Audio implementation.

Notes:

- Repeated or continuously controlled audio should use direct Audio System APIs rather than an Event Channel when lifecycle control is required.

---

# Event Payload Guidelines

- Event payloads should be small.
- Event payloads should describe the change rather than duplicate the entire owning System state.
- IDs should be preferred over direct content asset references when the listener can resolve the data through the appropriate registry or System.
- Previous and current values should be included when listeners commonly need to compare the change.
- A reason or source value should only be included when gameplay or presentation behavior actually depends on why the change occurred.
- Scene GameObject references should normally remain out of globally shared Event Channels.
- Payloads should not expose mutable internal collections owned by Systems.
- Payloads should not contain Save Data objects for listeners to modify.

---

# Subscription Rules

- Subscribers register when they become active and unregister when they become inactive or are destroyed.
- Unity-facing listeners normally subscribe during `OnEnable` and unsubscribe during `OnDisable` when appropriate for the object's lifecycle.
- Plain C# Systems should subscribe and unsubscribe through their initialization and shutdown lifecycle.
- A listener must not remain subscribed after it is no longer valid.
- Event Channels should tolerate having zero listeners.
- Raising an Event Channel should not require a listener to exist.
- Listeners should avoid raising the same Event Channel recursively unless the behavior is explicitly designed and guarded.

---

# Event Ordering Rules

- The owning System validates and applies its authoritative state change first.
- The owning System raises the appropriate Notification Channel after the change succeeds.
- Listeners respond to the completed change.
- Event listener order should not be used to sequence authoritative gameplay logic.
- Multi-step gameplay operations that require deterministic ordering should be coordinated by the owning System, an orchestration System, or a Controller rather than by assuming Event Channel listener order.
- Day End processing follows the explicit Day End processing order rather than relying on unordered Event Channel reactions.
- Save operations read authoritative System state after gameplay processing has completed.

---

# Direct Call vs. Event Channel

Use a direct System or Service call when:

- A feature is requesting an action from the System that owns that action.
- The caller requires an immediate result.
- The operation requires validation before succeeding.
- Execution order is important.
- The caller already has an appropriate dependency on the target System.

Use an Event Channel when:

- Something has already happened and multiple unrelated listeners may need to react.
- A Unity-facing Controller or UI needs to react to System state without the System depending on presentation code.
- A cross-feature notification should remain decoupled.
- A presentation request such as a HUD notification or one-shot audio cue should not create an unnecessary dependency.

Example:

- The Crafting System calls the Inventory System directly to validate and consume required Items.
- After the Inventory changes, the Inventory System raises `InventoryChanged`.
- The Inventory UI Controller refreshes its visible Inventory.
- The HUD may independently react if necessary.
- The Crafting System does not raise an event and hope that another listener removes the Items.

---

# Events That Should Not Become Channels

Avoid creating Event Channels for:

- Every method call between Systems.
- Every frame update.
- Raw movement Input.
- Continuous physics information.
- Internal helper operations that no external dependency needs to observe.
- Validation queries such as whether the player can afford an Item.
- Queries that require return values.
- Data that can be read directly from the authoritative System when no change notification is required.

Examples that should normally remain direct calls or queries:

- `InventorySystem.HasItem(...)`
- `EconomySystem.CanAfford(...)`
- `ToolSystem.CanUseTool(...)`
- `QuestSystem.IsQuestActive(...)`
- `CalendarSystem.GetCurrentDate()`
- `WeatherForecastSystem.GetForecast(...)`
- `NpcFriendshipSystem.GetFriendship(...)`

---

# Recommended Event Channel Assets

## Core

- `EC_InitializationCompleted`
- `EC_GameStateChanged`
- `EC_SceneTransitionStarted`
- `EC_SceneTransitionCompleted`

## Time & Calendar

- `EC_TimeChanged`
- `EC_DaylightChanged`
- `EC_DateChanged`
- `EC_SeasonChanged`
- `EC_DayEndStarted`
- `EC_DayEndCompleted`

## Weather

- `EC_WeatherChanged`
- `EC_WeatherForecastChanged`

## Player

- `EC_StaminaChanged`
- `EC_PlayerCustomizationChanged`
- `EC_MovementSpeedChanged`

## Inventory & Items

- `EC_InventoryChanged`
- `EC_ItemObtained`
- `EC_ItemRemoved`
- `EC_EquippedToolChanged`

## Economy

- `EC_BellnotesChanged`
- `EC_LoanChanged`

## Interaction

- `EC_InteractionTargetChanged`
- `EC_InteractionCompleted`

## Characters & Relationships

- `EC_NpcRoutineChanged`
- `EC_NpcMoodChanged`
- `EC_FriendshipChanged`
- `EC_ConnectionChanged`
- `EC_RelationshipStatusChanged`
- `EC_MarriageChanged`

## Quests & Game Events

- `EC_QuestStarted`
- `EC_QuestProgressChanged`
- `EC_QuestCompleted`
- `EC_GameEventStarted`
- `EC_GameEventCompleted`

## Festivals

- `EC_FestivalStarted`
- `EC_FestivalCompleted`

## Restoration & Progression

- `EC_RestorationChanged`
- `EC_UnlockChanged`

## Farming, Resources & Animals

- `EC_FarmingPlotChanged`
- `EC_ResourceChanged`
- `EC_AnimalStateChanged`

## Crafting & Inventions

- `EC_RecipeUnlocked`
- `EC_InventionUnlocked`
- `EC_InventionCompleted`

## Activities

- `EC_LibraryCollectionChanged`
- `EC_MuseumCollectionChanged`

## Mail

- `EC_MailReceived`
- `EC_MailStateChanged`

## Save

- `EC_SaveStarted`
- `EC_SaveCompleted`
- `EC_SaveFailed`

## Presentation Requests

- `EC_NotificationRequested`
- `EC_AudioCueRequested`

---

# Final Rules

- Systems own gameplay state.
- Controllers own Unity-facing coordination.
- Event Channels communicate changes without transferring ownership.
- Direct calls remain the preferred way to request authoritative gameplay actions.
- Event Channels are preferred for one-to-many notifications and decoupled presentation reactions.
- Event payloads remain small and strongly typed.
- Event listener order must not determine gameplay correctness.
- Event Channels never become persistent Save Data.
- New Event Channels should only be added when a real cross-dependency communication need exists.
