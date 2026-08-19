---
Title: Code Setup / Controllers
Game: As The Bell Tolls
Owner: Carey Clement Jr
Created: August, 2026
Updated: August, 2026
Version: 0.0.1
---

## Information

* Controllers coordinate Unity-facing behavior between the player, GameObjects, UI, and Gameplay Systems.
* Controllers do not own authoritative gameplay data when that data belongs to a System.
* Controllers may receive Input, detect Unity events, update GameObjects, update Views, and request actions from Gameplay Systems.
* Controllers should remain focused on a specific feature rather than becoming general-purpose managers.
* Gameplay rules should remain within the appropriate System or Service rather than being implemented directly inside Controllers.
* Controllers may subscribe to Event Channels when changes within another dependency need to affect their presentation or Unity behavior.
* Controllers should not directly modify Save Data.
* Controllers should avoid direct dependencies on unrelated Controllers.
* Controllers should communicate through Systems, Services, interfaces, requests, or Event Channels whenever possible.

---

# Core Controllers

## GameController

Namespace:

`AsTheBellTolls.GameFlow`

Purpose:

Coordinates the Unity-facing lifecycle of the active game session.

Responsibilities:

* Begins game initialization.
* Coordinates entering normal Gameplay.
* Responds to high-level Game State changes.
* Coordinates startup and shutdown behavior.
* Connects Unity lifecycle events to the appropriate Systems.
* Prevents gameplay Controllers from operating when the current Game State does not allow Gameplay.

Uses:

* Game State System
* Scene System
* Save System
* Initialization services
* Event Channels

Does Not:

* Own the current Game State.
* Own Save Data.
* Contain individual gameplay rules.
* Directly control every gameplay System.

---

## SceneController

Namespace:

`AsTheBellTolls.GameFlow`

Purpose:

Coordinates Unity scene transitions and scene-specific setup.

Responsibilities:

* Receives requests to change scenes.
* Coordinates transition presentation.
* Initializes scene-specific references after loading.
* Handles scene entry and exit hooks.
* Prevents interaction during active transitions.
* Reports completed Unity scene transitions to the Scene System.

Uses:

* Scene System
* Game State System
* Player Controller
* Camera Controller
* UI
* Audio

Does Not:

* Determine story progression.
* Determine whether a location is unlocked.
* Own the player's current location.
* Store persistent scene data.

---

## DayEndController

Namespace:

`AsTheBellTolls.GameFlow`

Purpose:

Coordinates presentation and Unity behavior during the Day End sequence.

Responsibilities:

* Begins the Day End presentation when requested.
* Prevents normal Gameplay controls during Day End.
* Presents Day End information in the required sequence.
* Advances between Day End screens.
* Reports when the Day End presentation has finished.

Presentation may include:

* Relationship changes
* Timing Progress
* End of Day Selling
* Tomorrow's Events

Uses:

* Day End System
* Game State System
* UI
* Input System

Does Not:

* Calculate Day End results.
* Advance the Calendar directly.
* Process Item sales directly.
* Save the game directly.

---

# Player Controllers

## PlayerController

Namespace:

`AsTheBellTolls.Player`

Purpose:

Acts as the primary Unity-facing Controller for the player character.

Responsibilities:

* Holds references to player GameObjects and components.
* Coordinates player-facing Controllers.
* Enables or disables player behavior according to Game State.
* Provides the player's Unity Transform when required by other Unity-facing features.
* Coordinates mounting and dismounting when appropriate.

Uses:

* Player Movement Controller
* Player Interaction Controller
* Player Customization Controller
* Tool Controller
* Game State System

Does Not:

* Own gameplay progression.
* Own Inventory.
* Own Stamina.
* Process Tool effects.
* Process interactions.

---

## PlayerMovementController

Namespace:

`AsTheBellTolls.Player`

Purpose:

Translates movement Input into physical player movement.

Responsibilities:

* Reads movement Input.
* Determines the requested movement direction.
* Moves the player GameObject.
* Updates facing direction.
* Applies the currently calculated movement speed.
* Coordinates movement animations.
* Stops movement when Gameplay movement is disabled.
* Handles mounted movement presentation when riding the Horse.

Uses:

* Input System
* Player Movement System
* Game State System
* Animation components
* Physics components

Does Not:

* Determine permanent movement bonuses.
* Determine tonic movement bonuses.
* Determine Horse unlock requirements.
* Determine Butler Tunnel availability.

---

## PlayerInteractionController

Namespace:

`AsTheBellTolls.Interaction`

Purpose:

Determines which nearby Unity object the player is currently able to interact with.

Responsibilities:

* Detects nearby interactable GameObjects.
* Determines which interactable the player is facing.
* Tracks the current interaction target.
* Requests the available interaction from the Interaction System.
* Responds to the Interact Input.
* Provides interaction information to the HUD.
* Clears the current interaction when the target is no longer valid.

Uses:

* Input System
* Interaction System
* Tool System
* HUD Controller

Does Not:

* Determine the gameplay result of the interaction.
* Remove Items.
* Award Items.
* Change relationships.
* Process Tool effects directly.

---

## PlayerCustomizationController

Namespace:

`AsTheBellTolls.Player`

Purpose:

Coordinates player avatar customization with the Unity character presentation.

Responsibilities:

* Applies selected appearance options to the player avatar.
* Updates skin tone.
* Updates hairstyle.
* Updates hair color.
* Updates eye color.
* Updates clothing.
* Updates body presentation.
* Previews customization changes.
* Applies confirmed customization choices.

Uses:

* Player Customization System
* Item System
* UI

Does Not:

* Determine which customization options are unlocked.
* Own the player's customization data.
* Process clothing purchases.

---

# Character Controllers

## NpcController

Namespace:

`AsTheBellTolls.Characters`

Purpose:

Acts as the Unity-facing representation of an NPC.

Responsibilities:

* Associates an NPC GameObject with its NPC ID.
* Provides NPC Transform information.
* Coordinates NPC navigation.
* Coordinates NPC animation.
* Coordinates NPC interaction availability.
* Applies visible NPC state.
* Responds when an NPC enters or leaves an active Game Event.
* Coordinates temporary visual behavior such as facing the player during Dialogue.

Uses:

* NPC Navigation System
* NPC Routine System
* NPC Mood System
* Dialogue System
* Game Event System

Does Not:

* Own Friendship.
* Own Connection progression.
* Select permanent NPC routines.
* Select Dialogue.
* Determine NPC schedule logic.

---

## NpcNavigationController

Namespace:

`AsTheBellTolls.Characters.Navigation`

Purpose:

Applies NPC Navigation System instructions to NPC GameObjects.

Responsibilities:

* Moves NPCs toward assigned destinations.
* Updates NPC facing direction.
* Coordinates walking animations.
* Handles arrival at destinations.
* Stops NPC movement during Dialogue.
* Stops or overrides movement during Game Events.
* Handles local movement between routine positions.

Uses:

* NPC Navigation System
* NPC Routine System
* Game Event System
* Unity navigation or movement components

Does Not:

* Select the NPC's Routine.
* Determine where an NPC should be according to the Calendar.
* Determine Festival attendance.
* Determine Quest behavior.

---

## NpcAnimationController

Namespace:

`AsTheBellTolls.Characters`

Purpose:

Controls visual animation state for NPC characters.

Responsibilities:

* Applies idle animations.
* Applies walking animations.
* Applies contextual animations.
* Updates facing direction.
* Responds to Dialogue state.
* Responds to Game Event animation instructions.

Uses:

* NPC Controller
* Game Event System
* Animation components

Does Not:

* Determine NPC gameplay behavior.
* Determine Dialogue.
* Determine Routine selection.

---

# Animal Controllers

## AnimalController

Namespace:

`AsTheBellTolls.Animals`

Purpose:

Acts as the Unity-facing representation of a livestock animal or pet.

Responsibilities:

* Associates an Animal GameObject with its Animal ID.
* Applies current animal presentation.
* Coordinates animal movement.
* Coordinates animation.
* Handles player-facing interaction detection.
* Displays appropriate visual state.
* Responds to animal state changes.

Uses:

* Husbandry System
* Interaction System
* Animal data

Does Not:

* Own animal Hearts.
* Determine animal maturity.
* Calculate byproduct production.
* Determine Husbandry progression.

---

# Interaction Controllers

## InteractableController

Namespace:

`AsTheBellTolls.Interaction`

Purpose:

Provides the common Unity-facing component used by interactable world objects.

Responsibilities:

* Identifies the Interactable.
* Provides Interaction ID or interaction definition.
* Provides interaction position.
* Reports whether the object is currently physically interactable.
* Supplies presentation information for the Interact Panel.
* Forwards interaction requests to the appropriate gameplay feature.

Examples:

* NPCs
* Crops
* Flowers
* Soil
* Trees
* Rock Deposits
* Bushes
* Tall Grass
* Water
* Doors
* Workbenches
* Stove
* Tonic Station
* Shops
* Grandfather Clock
* Gravemarkers

Does Not:

* Implement the complete gameplay logic of every interaction.

---

## DoorController

Namespace:

`AsTheBellTolls.World`

Purpose:

Coordinates Unity door interactions and transitions between locations.

Responsibilities:

* Detects Door interaction.
* Requests destination validation.
* Begins the appropriate transition.
* Places the player at the destination entry point.

Uses:

* Interaction System
* Scene System
* World / Location data

Does Not:

* Determine progression unlock conditions independently.
* Own location data.

---

## FastTravelController

Namespace:

`AsTheBellTolls.World`

Purpose:

Coordinates physical fast-travel interactions such as the Butler Tunnels.

Responsibilities:

* Presents available destinations.
* Receives the player's selected destination.
* Requests travel.
* Coordinates the resulting transition.

Uses:

* World services
* Restoration System
* Scene System
* UI

Does Not:

* Determine whether Butler Tunnels have been unlocked.
* Own discovered-location data.

---

# Tool Controllers

## ToolController

Namespace:

`AsTheBellTolls.Tools`

Purpose:

Coordinates Tool selection and Tool-use presentation for the player.

Responsibilities:

* Tracks which Tool is currently presented as equipped.
* Responds to Tool selection Input.
* Plays Tool-use animations.
* Coordinates Tool targeting.
* Sends Tool-use requests to the Tool System.
* Updates Tool presentation following upgrades.

Uses:

* Tool System
* Inventory System
* Input System
* Interaction System
* Player animation

Does Not:

* Own Tools.
* Determine upgrade requirements.
* Deduct Stamina directly.
* Harvest resources directly.
* Modify soil directly.

---

# World Gameplay Controllers

## FarmingController

Namespace:

`AsTheBellTolls.Farming`

Purpose:

Coordinates Unity presentation and interactions for farmable soil, crops, and flowers.

Responsibilities:

* Presents Soil state.
* Presents planted Seeds.
* Updates visible crop and flower growth stages.
* Handles targeting of farmable squares.
* Sends tilling requests.
* Sends planting requests.
* Sends watering requests.
* Sends harvesting requests.
* Refreshes farm visuals after Farming state changes.

Uses:

* Farming System
* Tool System
* Inventory System
* Interaction System

Does Not:

* Calculate growth.
* Consume Seeds directly.
* Award harvested Items directly.
* Determine whether Weather waters crops.

---

## FishingController

Namespace:

`AsTheBellTolls.Fishing`

Purpose:

Coordinates fishing interactions and Fishing Minigame presentation.

Responsibilities:

* Begins fishing presentation.
* Coordinates casting.
* Displays Fishing Minigame UI.
* Receives Fishing Input.
* Updates visible minigame elements.
* Reports minigame outcomes to the Fishing System.
* Ends fishing presentation.
* Coordinates Fishing Net presentation.

Uses:

* Fishing System
* Input System
* Tool System
* UI
* Inventory System

Does Not:

* Select Fish independently.
* Calculate Fish probability.
* Award caught Fish directly.
* Determine Fish availability.

---

## GatheringController

Namespace:

`AsTheBellTolls.Gathering`

Purpose:

Coordinates world-resource gathering presentation.

Responsibilities:

* Handles gathering interactions with eligible resource GameObjects.
* Plays gathering animations and effects.
* Requests gathering results.
* Refreshes or removes depleted resource visuals.

Examples:

* Herbs
* Trees
* Tall Grass
* Rock Deposits
* Gemstone Deposits
* Forageables

Uses:

* Gathering Probability System
* Interaction System
* Tool System
* Inventory System
* Resource Respawn System

Does Not:

* Calculate gathering probabilities independently.
* Add Items directly to Inventory.
* Determine resource respawn timing.

---

## ResourceController

Namespace:

`AsTheBellTolls.World`

Purpose:

Represents a gatherable or destructible resource within a Unity scene.

Responsibilities:

* Associates the GameObject with its resource definition.
* Displays current resource state.
* Reports resource interaction points.
* Hides or destroys presentation when depleted.
* Restores presentation when respawned.

Uses:

* Resource Respawn System
* Gathering features

Does Not:

* Own resource respawn state.
* Calculate gathering rewards.

---

## HusbandryController

Namespace:

`AsTheBellTolls.Animals`

Purpose:

Coordinates player interactions with livestock, pets, Bees, and Silkworms.

Responsibilities:

* Presents available Husbandry interactions.
* Coordinates petting and care animations.
* Coordinates collection of animal byproducts.
* Refreshes animal presentation following state changes.

Uses:

* Husbandry System
* Animal Controller
* Inventory System
* Interaction System

Does Not:

* Calculate Hearts.
* Determine maturity.
* Determine byproduct availability.
* Own animal data.

---

# Production Controllers

## CraftingController

Namespace:

`AsTheBellTolls.Crafting`

Purpose:

Coordinates the Workbench and Fabrication interface.

Responsibilities:

* Opens the Crafting Menu.
* Displays available Fabrications.
* Displays ingredient requirements.
* Allows recipe selection.
* Sends Crafting requests.
* Presents success or failure results.
* Refreshes available recipes and Inventory presentation.

Uses:

* Crafting System
* Inventory System
* Item System
* UI

Does Not:

* Consume Ingredients directly.
* Create fabricated Items directly.
* Determine recipe unlocks independently.

---

## CookingController

Namespace:

`AsTheBellTolls.Crafting.Cooking`

Purpose:

Coordinates Stove interactions and Cooking UI.

Responsibilities:

* Opens the Cooking Menu.
* Displays available Meals and Drinks.
* Displays Ingredient requirements.
* Receives recipe selection.
* Sends Cooking requests.
* Presents completed Food.

Uses:

* Cooking System
* Inventory System
* Item System
* UI

Does Not:

* Remove Ingredients directly.
* Add Meals or Drinks directly.
* Determine unlocked recipes independently.

---

## TonicMakingController

Namespace:

`AsTheBellTolls.Crafting.Tonics`

Purpose:

Coordinates the Tonic Making Minigame.

Responsibilities:

* Begins the Tonic Making Minigame.
* Displays Flask and heat presentation.
* Receives Flask rotation Input.
* Receives flame Input.
* Updates minigame visual state.
* Reports minigame results.
* Ends the minigame.

Uses:

* Tonic Making System
* Input System
* Inventory System
* UI

Does Not:

* Determine final Tonic results independently.
* Remove Herbs directly.
* Add Tonics directly.

---

## InventionController

Namespace:

`AsTheBellTolls.Inventions`

Purpose:

Coordinates invention construction, testing, and presentation.

Responsibilities:

* Presents available Inventions.
* Displays Invention requirements.
* Receives construction requests.
* Presents active invention timers.
* Coordinates invention testing presentation.
* Refreshes unlocked Blueprint and Invention information.

Uses:

* Invention System
* Inventory System
* Progression
* UI

Does Not:

* Determine Invention unlocks independently.
* Consume Items directly.
* Own invention completion state.

---

## TimeManipulationController

Namespace:

`AsTheBellTolls.Time`

Purpose:

Coordinates interaction with the Pendrelle grandfather clock and the Time Manipulation Menu.

Responsibilities:

* Opens the Time Manipulation Menu.
* Displays available timed activities.
* Displays remaining Chimes.
* Allows an activity to be selected.
* Requests Time Manipulation.
* Presents successful or unsuccessful results.
* Refreshes duration information following manipulation.

Uses:

* Time Manipulation System
* Time System
* UI

Does Not:

* Own Chimes.
* Calculate Time Manipulation effects independently.
* Directly alter timed gameplay data.

---

# Dialogue and Relationship Controllers

## DialogueController

Namespace:

`AsTheBellTolls.Dialogue`

Purpose:

Coordinates active Dialogue presentation.

Responsibilities:

* Opens and closes the Dialogue Panel.
* Displays speaker information.
* Displays Dialogue text.
* Advances Dialogue.
* Displays player response options.
* Receives response selection.
* Coordinates NPC facing and temporary movement suspension.
* Coordinates speech audio playback.
* Reports Dialogue completion.

Uses:

* Dialogue System
* Input System
* Audio System
* NPC Controller
* UI

Does Not:

* Select Dialogue independently.
* Award Friendship points directly.
* Process Gifts directly.
* Update Quest progression directly.

---

## GiftController

Namespace:

`AsTheBellTolls.Relationships`

Purpose:

Coordinates the player-facing process of giving an Item to an NPC.

Responsibilities:

* Presents eligible Items.
* Receives the selected Gift.
* Requests Gift processing.
* Displays the resulting NPC reaction.
* Refreshes Inventory presentation.

Uses:

* NPC Friendship System
* Inventory System
* Dialogue System
* Item System

Does Not:

* Calculate Friendship rewards independently.
* Remove Items directly.
* Select Gift Dialogue independently.

---

## MarriageController

Namespace:

`AsTheBellTolls.Relationships.Marriage`

Purpose:

Coordinates player-facing marriage interactions and wedding presentation.

Responsibilities:

* Coordinates proposal presentation.
* Displays marriage-related confirmation UI.
* Coordinates Player Wedding presentation where appropriate.
* Applies marriage-related visual changes after confirmed System state changes.

Uses:

* Marriage System
* Family System
* Festival System
* Dialogue System
* Game Event System

Does Not:

* Determine marriage eligibility.
* Own spouse state.
* Determine Relationship progression.

---

# Quest and Event Controllers

## QuestController

Namespace:

`AsTheBellTolls.Quests`

Purpose:

Coordinates player-facing Quest interactions and Quest presentation.

Responsibilities:

* Presents Quest offers.
* Displays Quest acceptance choices.
* Presents Quest updates.
* Presents Quest completion.
* Refreshes Quest UI.
* Coordinates Quest markers where applicable.

Uses:

* Quest System
* Dialogue System
* UI
* Event Channels

Does Not:

* Own Quest progression.
* Determine Quest eligibility independently.
* Grant Quest rewards directly.

---

## GameEventController

Namespace:

`AsTheBellTolls.GameFlow`

Purpose:

Executes the Unity-facing presentation of scripted Game Events.

Responsibilities:

* Receives an eligible Game Event from the Game Event System.
* Begins cinematic presentation.
* Coordinates scripted NPC movement.
* Coordinates scripted player movement.
* Coordinates Dialogue.
* Coordinates animations.
* Coordinates Camera instructions.
* Coordinates Audio instructions.
* Coordinates waits and timing.
* Coordinates visual effects.
* Reports completion of event presentation.

Uses:

* Game Event System
* Game State System
* Dialogue Controller
* NPC Controllers
* Player Controller
* Camera Controller
* Audio System
* UI

Does Not:

* Determine whether the Game Event is eligible.
* Directly grant rewards.
* Directly update Relationships.
* Directly update Quests.
* Own Game Event completion state.

---

## FestivalController

Namespace:

`AsTheBellTolls.Festivals`

Purpose:

Coordinates Unity-facing Festival presentation and Festival activities.

Responsibilities:

* Initializes the active Festival scene or area.
* Applies Festival decorations.
* Coordinates Festival-specific NPC presentation.
* Presents Festival activities.
* Coordinates competitions and minigames.
* Coordinates Festival shops.
* Restores normal presentation when a Festival ends.

Uses:

* Festival System
* NPC Routine System
* Game Event System
* Dialogue System
* Audio System
* UI

Does Not:

* Determine Festival dates.
* Determine attendance independently.
* Own Festival completion data.

---

## TutorialController

Namespace:

`AsTheBellTolls.GameFlow`

Purpose:

Coordinates presentation of non-Quest tutorials.

Responsibilities:

* Opens Tutorial Menus.
* Displays tutorial instructions.
* Advances tutorial pages.
* Highlights relevant UI or controls.
* Reports tutorial completion.

Uses:

* Tutorial System
* Input System
* UI

Does Not:

* Own Tutorial progression.
* Process Tutorial Quest progression.

---

# Commerce Controllers

## ShopController

Namespace:

`AsTheBellTolls.Commerce`

Purpose:

Coordinates Shop and merchant interfaces.

Responsibilities:

* Opens Shop UI.
* Displays available stock.
* Displays purchase prices.
* Displays eligible Items for selling when supported.
* Receives purchase and sale selections.
* Requests transactions.
* Presents transaction results.
* Refreshes Shop and Inventory presentation.

Uses:

* Commerce
* Economy System
* Inventory System
* Item System
* UI

Does Not:

* Modify Bellnotes directly.
* Modify Inventory directly.
* Determine Shop stock independently.

---

## LoanController

Namespace:

`AsTheBellTolls.Economy`

Purpose:

Coordinates player-facing Loan interactions.

Responsibilities:

* Displays available Loan information.
* Displays current Loan balance.
* Receives Loan requests.
* Receives repayment requests.
* Presents transaction results.

Uses:

* Loan System
* Economy System
* UI

Does Not:

* Own Loan balances.
* Modify Bellnotes directly.
* Determine Loan eligibility independently.

---

# Restoration Controllers

## RestorationController

Namespace:

`AsTheBellTolls.Restoration`

Purpose:

Coordinates restoration interactions and presentation for Manor rooms and Town buildings.

Responsibilities:

* Displays current Restoration stage.
* Displays Restoration requirements.
* Receives Restoration requests.
* Plays restoration presentation.
* Refreshes building visuals.
* Applies the correct visual appearance for the current Restoration stage.

Uses:

* Restoration System
* Inventory System
* Tool System
* World
* UI

Does Not:

* Determine Restoration requirements independently.
* Consume Items directly.
* Own Restoration progress.
* Unlock features directly.

---

# UI Controllers

## UiController

Namespace:

`AsTheBellTolls.UI`

Purpose:

Coordinates the highest-level runtime UI state.

Responsibilities:

* Tracks which major UI layer is currently presented.
* Coordinates opening and closing UI.
* Handles modal UI presentation.
* Coordinates UI focus.
* Responds to Game State changes.
* Ensures incompatible UI layers are not active simultaneously.

Uses:

* UI System
* Game State System
* Input System

Does Not:

* Own gameplay data.
* Implement individual menu functionality.

---

## HudController

Namespace:

`AsTheBellTolls.UI.HUD`

Purpose:

Coordinates runtime HUD presentation.

Responsibilities:

* Updates Date display.
* Updates Time display.
* Updates Bellnotes display.
* Updates Stamina display.
* Updates Daylight presentation.
* Updates Buff presentation.
* Shows and hides the Interact Panel.
* Displays the current bound Input for interactions.

Uses:

* HUD System
* Time System
* Calendar System
* Economy System
* Stamina System
* Input System
* Interaction System

Does Not:

* Own any displayed gameplay values.
* Process gameplay actions.

---

## GameplayMenuController

Namespace:

`AsTheBellTolls.UI.Menus`

Purpose:

Coordinates contextual Gameplay Menus opened through world interactions.

Examples:

* Shop
* Cooking
* Crafting
* Tonic Making
* Storage
* Time Manipulation
* Restoration

Responsibilities:

* Opens the requested Gameplay Menu.
* Closes the active Gameplay Menu.
* Coordinates UI navigation.
* Applies appropriate Game State behavior.
* Returns control to Gameplay when the menu closes.

Uses:

* UI Menu System
* Input System
* Game State System

Does Not:

* Process the gameplay feature represented by the Menu.

---

## PlayerMenuController

Namespace:

`AsTheBellTolls.UI.Menus`

Purpose:

Coordinates the Grandfather Clock Player Menu.

Responsibilities:

* Opens and closes the Player Menu.
* Coordinates Clock Wheel navigation.
* Opens selected Player Menu pages.
* Handles page-to-page navigation.
* Maintains currently selected page during the active Menu session.

Pages:

* Player
* Inventory
* Inventions
* Skill Tree
* Relationships
* Town Progress
* Quests
* Map
* Calendar
* Ledger
* Settings
* Quit

Uses:

* UI Player Menu System
* Input System
* Game State System

Does Not:

* Own the gameplay data displayed on individual pages.

---

## InventoryUiController

Namespace:

`AsTheBellTolls.UI.Menus`

Purpose:

Coordinates Inventory page presentation and Item manipulation within UI.

Responsibilities:

* Displays Tool Belt.
* Displays Satchel.
* Displays Item stacks.
* Displays Item Quality.
* Displays Item details.
* Handles Item selection.
* Handles manual Item arrangement.
* Sends sorting requests.
* Coordinates Item transfers when applicable.

Uses:

* Inventory System
* Item System
* Input System

Does Not:

* Own Inventory.
* Modify Item stacks independently.

---

## RelationshipsUiController

Namespace:

`AsTheBellTolls.UI.Menus`

Purpose:

Coordinates the Relationships page.

Responsibilities:

* Displays NPC list.
* Displays Relationship Status.
* Displays Friendship Hearts.
* Displays progress to the next Heart.
* Displays known Gift preferences.
* Displays profession and residence information.
* Displays spouse information when applicable.

Uses:

* NPC Friendship System
* NPC Connection System
* Marriage System
* Character data

Does Not:

* Own Relationship progression.

---

## QuestUiController

Namespace:

`AsTheBellTolls.UI.Menus`

Purpose:

Coordinates the Quests page and quick Quest display.

Responsibilities:

* Displays Active Quests.
* Displays Completed Quests.
* Displays Quest objectives.
* Displays Quest requirements.
* Displays Quest rewards.
* Displays Quest giver information.

Uses:

* Quest System

Does Not:

* Own Quest progression.

---

## MapController

Namespace:

`AsTheBellTolls.UI.Menus`

Purpose:

Coordinates the interactive Map.

Responsibilities:

* Displays the world Map.
* Displays known building and location icons.
* Handles location selection.
* Opens location detail panels.
* Displays relevant location information.

Uses:

* Map System
* World data
* Restoration System

Does Not:

* Own location progression.
* Determine Fast Travel rules unless explicitly requested through the appropriate World feature.

---

## CalendarUiController

Namespace:

`AsTheBellTolls.UI.Menus`

Purpose:

Coordinates the Calendar page.

Responsibilities:

* Displays the current Season.
* Displays days of the Season.
* Displays Festivals.
* Displays birthdays.
* Displays relevant scheduled events.

Uses:

* Calendar System
* Festival System
* Character data

Does Not:

* Own Calendar data.
* Advance dates.

---

## SettingsController

Namespace:

`AsTheBellTolls.UI.Menus`

Purpose:

Coordinates Settings Menu presentation and user setting changes.

Responsibilities:

* Displays current Settings.
* Receives setting changes.
* Updates Audio settings.
* Updates control settings.
* Updates display settings.
* Coordinates rebinding UI where supported.

Uses:

* Audio System
* Input System
* Save System
* UI

Does Not:

* Implement Audio playback.
* Implement Input handling.

---

# Camera and Audio Controllers

## CameraController

Namespace:

`AsTheBellTolls.Camera`

Purpose:

Applies Camera System state to the active Unity Camera.

Responsibilities:

* Follows the player during normal Gameplay.
* Applies Camera bounds.
* Changes targets.
* Performs scripted Camera movement.
* Performs cinematic framing.
* Coordinates Camera transitions.
* Restores Gameplay Camera state after Game Events.

Uses:

* Camera System
* Game Event Controller
* Player Controller

Does Not:

* Determine Game Event eligibility.
* Own the player's position.

---

## AudioController

Namespace:

`AsTheBellTolls.Audio`

Purpose:

Coordinates Unity AudioSources and runtime playback.

Responsibilities:

* Plays Music.
* Plays Ambiance.
* Plays Sound Effects.
* Plays Weather Audio.
* Plays Footsteps.
* Plays Speech clips.
* Crossfades appropriate audio.
* Applies Audio settings.

Uses:

* Audio System
* Weather System
* World / Location information
* Game Event System

Does Not:

* Determine Weather.
* Determine player progression.
* Own Audio settings data.

---

# Save Controllers

## SaveController

Namespace:

`AsTheBellTolls.Save`

Purpose:

Coordinates Unity-facing Save and Load requests.

Responsibilities:

* Receives manual Save requests.
* Receives Load requests.
* Presents Save slots.
* Presents Save status.
* Presents Load failures.
* Coordinates Save confirmation UI.
* Reports Unity application lifecycle Save opportunities when required.

Uses:

* Save System
* UI
* Game State System

Does Not:

* Serialize gameplay data itself.
* Own Save Data.
* Modify gameplay state directly.

---

# Recommended Controller List

The primary Controllers are:

* `GameController`
* `SceneController`
* `DayEndController`
* `PlayerController`
* `PlayerMovementController`
* `PlayerInteractionController`
* `PlayerCustomizationController`
* `NpcController`
* `NpcNavigationController`
* `NpcAnimationController`
* `AnimalController`
* `InteractableController`
* `DoorController`
* `FastTravelController`
* `ToolController`
* `FarmingController`
* `FishingController`
* `GatheringController`
* `ResourceController`
* `HusbandryController`
* `CraftingController`
* `CookingController`
* `TonicMakingController`
* `InventionController`
* `TimeManipulationController`
* `DialogueController`
* `GiftController`
* `MarriageController`
* `QuestController`
* `GameEventController`
* `FestivalController`
* `TutorialController`
* `ShopController`
* `LoanController`
* `RestorationController`
* `UiController`
* `HudController`
* `GameplayMenuController`
* `PlayerMenuController`
* `InventoryUiController`
* `RelationshipsUiController`
* `QuestUiController`
* `MapController`
* `CalendarUiController`
* `SettingsController`
* `CameraController`
* `AudioController`
* `SaveController`

---

# Controller Rules

* Controllers should not be treated as another name for Systems.
* Systems own gameplay state and gameplay rules.
* Controllers coordinate Unity GameObjects, Input, animations, Views, and presentation.
* A Controller may request a System action but should not recreate that System's rules.
* Controllers should not directly manipulate another dependency's authoritative data.
* Controllers should not directly access Save Data to alter gameplay values.
* Controllers should not communicate through static global references where an interface, Service, Event Channel, or dependency reference can be used.
* Controllers should generally not call unrelated Controllers directly.
* A parent Controller may coordinate smaller Controllers within the same feature.
* Feature-specific Controllers should remain within the namespace of the feature they represent.
* UI Controllers may read presentation data and issue requests but should not own gameplay state.
* Scene-bound MonoBehaviours are appropriate Controllers when they coordinate Unity objects with gameplay logic.
* Pure C# gameplay logic should normally exist in Systems, Services, Models, or other domain classes instead of Controllers.

---