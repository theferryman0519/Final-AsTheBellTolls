---
Title: Code Setup / Dependencies
Game: As The Bell Tolls
Owner: Carey Clement Jr
Created: August, 2026
Updated: August, 2026
Version: 0.0.1
---

# Dependencies

The following dependencies represent the primary code domains used throughout *As The Bell Tolls*.

Dependencies should represent major areas of responsibility within the game rather than individual gameplay systems.

Individual Systems, Controllers, Models, Services, ScriptableObjects, and related classes should exist within the appropriate dependency namespace.

---

## Core

Namespace:

`AsTheBellTolls.Core`

Purpose:

Contains foundational code that can be used throughout the entire game without depending on gameplay-specific systems.

Includes:

* Common interfaces
* Base classes
* Common utilities
* Shared value types
* Generic helpers
* Constants
* Common result types
* Shared validation
* Application-wide abstractions

Examples:

* `IInitializable`
* `ITickable`
* `IResettable`
* `GameConstants`
* Generic ID wrappers
* Common extension methods

---

## Data

Namespace:

`AsTheBellTolls.Data`

Purpose:

Contains shared data definitions and structures used to identify and describe game content.

Includes:

* Data IDs
* Shared enums
* Data references
* Shared Models
* Data validation
* Data registries
* ScriptableObject foundations

Related Notes:

* Data IDs
* Enums
* Models
* Scriptable Objects

---

## Events

Namespace:

`AsTheBellTolls.Events`

Purpose:

Provides communication between otherwise independent areas of the game.

Includes:

* Event Channels
* Gameplay events
* Notifications between systems
* Requests
* Responses
* Event payloads

Related Notes:

* Event Channels
* System Interaction Rules

This namespace represents **code events**, rather than festivals or story events.

---

## GameFlow

Namespace:

`AsTheBellTolls.GameFlow`

Purpose:

Controls the high-level lifecycle and state of the game.

Includes:

* Game State
* Game initialization
* Initialization order
* Scene transitions
* Day End processing
* Game Event execution
* Tutorial flow
* Application-level orchestration

Systems:

* Game State System
* Game Event System
* Scene System
* Day End System
* Tutorial System
* Admin System

---

## Save

Namespace:

`AsTheBellTolls.Save`

Purpose:

Handles persistent game data.

Includes:

* Save files
* Save Data
* Save serialization
* Save loading
* Save migrations
* Save versioning
* Autosaves
* Manual saves

Systems:

* Save System

Related Notes:

* Save Data
* Save Versioning

---

## Time

Namespace:

`AsTheBellTolls.Time`

Purpose:

Owns the passage and manipulation of game time.

Includes:

* Current game time
* Time progression
* Time ticks
* Daylight periods
* Time pausing
* Time manipulation
* Time-related calculations

Systems:

* Time System
* Time Manipulation System

---

## Calendar

Namespace:

`AsTheBellTolls.Calendar`

Purpose:

Owns dates and calendar-based scheduling.

Includes:

* Weekdays
* Days
* Seasons
* Years
* Calendar dates
* Seasonal transitions
* Scheduled dates
* Calendar queries

Systems:

* Calendar System

---

## Weather

Namespace:

`AsTheBellTolls.Weather`

Purpose:

Controls current and future environmental weather conditions.

Includes:

* Weather generation
* Current Weather
* Weather transitions
* Weather Forecasts
* Weather effects
* Seasonal weather probabilities

Systems:

* Weather System
* Weather Forecast System

---

## World

Namespace:

`AsTheBellTolls.World`

Purpose:

Represents Blackmere, Morvanya, Pendrelle Manor, and the physical game world.

Includes:

* Locations
* Location IDs
* World areas
* Buildings
* Interiors
* Scene-location relationships
* Resource spawning
* World interactables
* Cemetery data
* Maps

Systems:

* Resource Respawn System
* Cemetery System
* Map System

Content:

* Blackmere
* Morvanya
* Pendrelle Manor
* Resident Lane
* Town buildings
* Farms
* Mines
* Rivers
* Memorial Hill
* Other locations

---

## Player

Namespace:

`AsTheBellTolls.Player`

Purpose:

Contains functionality specifically owned by the player character.

Includes:

* Player state
* Player movement
* Player customization
* Avatar information
* Player statistics
* Stamina
* Player-specific runtime data

Systems:

* Player Movement System
* Player Customization System
* Stamina System

---

## Characters

Namespace:

`AsTheBellTolls.Characters`

Purpose:

Contains shared character and NPC functionality.

Includes:

* NPC definitions
* Character data
* Character identities
* Character attributes
* NPC runtime state
* Personality information
* NPC Mood
* NPC navigation
* NPC routines

Systems:

* NPC Mood System
* NPC Navigation System
* NPC Routine System

Content:

* Marriage Candidates
* Non-Marriage Candidates
* Children
* Rivals
* Traders
* Politicians
* Deceased Characters

Possible child namespaces:

* `AsTheBellTolls.Characters.Data`
* `AsTheBellTolls.Characters.Routines`
* `AsTheBellTolls.Characters.Navigation`

---

## Animals

Namespace:

`AsTheBellTolls.Animals`

Purpose:

Contains animals, livestock, pets, and animal-related gameplay.

Includes:

* Livestock
* Pets
* Animal definitions
* Animal behavior
* Animal products
* Animal care
* Husbandry

Systems:

* Husbandry System

Content:

* Bee
* Cat
* Chicken
* Cow
* Dog
* Duck
* Goat
* Goose
* Horse
* Pig
* Sheep
* Silkworm

---

## Relationships

Namespace:

`AsTheBellTolls.Relationships`

Purpose:

Owns social progression between the player and NPCs.

Includes:

* Friendship
* Connections
* Hearts
* Keys
* Relationship progression
* Relationship thresholds
* Relationship rewards
* Marriage eligibility
* Family relationships
* NPC social progression

Systems:

* NPC Friendship System
* NPC Connection System
* Marriage System
* Family System

Related Progression:

* NPC Friendship Progression
* NPC Connection Progression

Possible child namespaces:

* `AsTheBellTolls.Relationships.Friendship`
* `AsTheBellTolls.Relationships.Connection`
* `AsTheBellTolls.Relationships.Marriage`
* `AsTheBellTolls.Relationships.Family`

---

## Dialogue

Namespace:

`AsTheBellTolls.Dialogue`

Purpose:

Owns conversations and NPC dialogue selection.

Includes:

* Dialogue entries
* Dialogue conditions
* Dialogue selection
* Dialogue responses
* Generic dialogue
* Relationship dialogue
* Quest dialogue
* Festival dialogue
* Gift dialogue
* Marriage dialogue
* Introductory dialogue

Systems:

* Dialogue System

Content includes:

* First Meet
* Greetings
* Daylight
* Weather
* Season
* Profession
* Memorial
* Friendship Level
* Connection Level
* Gifts
* Shopping
* Quests
* Bond Events
* Proposals
* Married Life
* Festivals

---

## Interaction

Namespace:

`AsTheBellTolls.Interaction`

Purpose:

Controls how the player interacts with NPCs, objects, resources, and the environment.

Includes:

* Interactable interfaces
* Interaction detection
* Interaction requirements
* Interaction prompts
* Context actions
* Interaction requests
* Interaction targeting

Systems:

* Interaction System

Examples:

* Talk
* Examine
* Harvest
* Cut
* Mine
* Fish
* Open
* Shop
* Use
* Repair

---

## Items

Namespace:

`AsTheBellTolls.Items`

Purpose:

Defines all item content and common item behavior.

Includes:

* Item definitions
* Item IDs
* Item categories
* Item qualities
* Stack information
* Item metadata
* Ingredient data
* Quest Items
* Useful Items
* Clothing
* Mail Items
* Recipe products

Systems:

* Item System

Content categories:

* Clothing Items
* Ingredient Items
* Interactable Items
* Mail Items
* Quest Items
* Recipe Items
* Useful Items

Possible child namespaces:

* `AsTheBellTolls.Items.Data`
* `AsTheBellTolls.Items.Equipment`
* `AsTheBellTolls.Items.Recipes`

---

## Inventory

Namespace:

`AsTheBellTolls.Inventory`

Purpose:

Owns storage and movement of item instances.

Includes:

* Player Inventory
* Tool Belt
* Satchel
* Item stacks
* Item transfer
* Storage containers
* Inventory capacity
* Adding Items
* Removing Items

Systems:

* Inventory System

Items define **what an Item is**.

Inventory defines **where owned Items are stored**.

---

## Tools

Namespace:

`AsTheBellTolls.Tools`

Purpose:

Owns usable player tools and their shared behavior.

Includes:

* Tool definitions
* Tool selection
* Tool usage
* Tool requirements
* Tool targeting
* Tool upgrades
* Tool actions

Systems:

* Tool System

Tools:

* Axe
* Fishing Rod
* Hammer
* Herb Knife
* Hoe
* Pickaxe
* Scythe
* Watering Can

---

## Gathering

Namespace:

`AsTheBellTolls.Gathering`

Purpose:

Owns resources collected directly from the world.

Includes:

* Foraging
* Resource gathering
* Gather probabilities
* Gather quality
* Gather quantities
* Harvestable world resources

Systems:

* Gathering Probability System

---

## Farming

Namespace:

`AsTheBellTolls.Farming`

Purpose:

Owns crop, flower, and cultivated-soil gameplay.

Includes:

* Soil
* Tilling
* Planting
* Watering
* Crop growth
* Flower growth
* Harvesting
* Crop quality
* Seasonal growing rules

Systems:

* Farming System

---

## Fishing

Namespace:

`AsTheBellTolls.Fishing`

Purpose:

Owns fishing gameplay.

Includes:

* Fishing locations
* Fish availability
* Fishing probabilities
* Fishing minigame
* Fish quality
* Catch calculations
* Seasonal fish rules

Systems:

* Fishing System

---

## Crafting

Namespace:

`AsTheBellTolls.Crafting`

Purpose:

Owns transforming Items into new Items using recipes.

Includes:

* Crafting recipes
* Fabrications
* Cooking
* Drinks
* Meals
* Tonic Making
* Ingredient requirements
* Recipe validation
* Production results

Systems:

* Crafting System
* Cooking System
* Tonic Making System

Possible child namespaces:

* `AsTheBellTolls.Crafting.Fabrication`
* `AsTheBellTolls.Crafting.Cooking`
* `AsTheBellTolls.Crafting.Tonics`

---

## Inventions

Namespace:

`AsTheBellTolls.Inventions`

Purpose:

Owns the player's invention gameplay and invention progression.

Includes:

* Blueprint Book inventions
* Invention tiers
* Quest inventions
* Grand Showcase inventions
* Invention requirements
* Invention unlocking
* Invention completion

Systems:

* Invention System

Content:

* Copper Tier
* Iron Tier
* Silver Tier
* Gold Tier
* Cobalt Tier
* NPC Quest Inventions
* Grand Showcase Inventions

---

## Economy

Namespace:

`AsTheBellTolls.Economy`

Purpose:

Owns money, purchasing, selling, financial transactions, and related economic gameplay.

Includes:

* Bellnotes
* Prices
* Purchasing
* Selling
* Shop transactions
* Loans
* End-of-day sales
* Financial calculations

Systems:

* Economy System
* Loan System
* Day End Selling System

---

## Commerce

Namespace:

`AsTheBellTolls.Commerce`

Purpose:

Owns shops, merchants, inventories offered for sale, and trade gameplay.

Includes:

* Shops
* Shop inventories
* Vendors
* Traders
* Purchase availability
* Seasonal stock
* Trade carts
* Merchant schedules

Economy determines **financial transactions**.

Commerce determines **what can be bought or sold and from whom**.

---

## Quests

Namespace:

`AsTheBellTolls.Quests`

Purpose:

Owns quest definitions and quest progression.

Includes:

* Quest requirements
* Quest objectives
* Quest states
* Quest rewards
* Quest completion
* Quest availability

Systems:

* Quest System

Quest categories:

* Tutorial Quests
* Connection Quests
* Friendship Quests
* Achievement Quests
* Daily Quests

---

## Progression

Namespace:

`AsTheBellTolls.Progression`

Purpose:

Owns broad player and world progression rules that span multiple gameplay domains.

Includes:

* Main Story Progression
* Unlock Progression
* Blueprint Book Progression
* Achievement Ledger
* Progress gates
* Unlock requirements
* Completion tracking

Content:

* Main Story Progression
* Blueprint Book Progression
* Unlock Progression
* Achievement Ledger

Relationship-specific progression remains under `Relationships`.

---

## Restoration

Namespace:

`AsTheBellTolls.Restoration`

Purpose:

Owns the recovery and improvement of Pendrelle Manor and Blackmere.

Includes:

* Building restoration
* Manor restoration
* Town restoration
* Restoration states
* Restoration requirements
* Restoration rewards
* Feature unlocks caused by restoration

Systems:

* Restoration System

Progression States:

* Weathered
* Rebuilding
* Recovering
* Renewed
* Growing
* Prospering
* Flourishing

---

## Festivals

Namespace:

`AsTheBellTolls.Festivals`

Purpose:

Owns scheduled festivals and special community events.

Includes:

* Festival definitions
* Festival schedules
* Festival participation
* Festival routines
* Festival-specific gameplay
* Mini Festivals
* Main Festivals

Systems:

* Festival System

Content includes:

* Seedwake Brunch
* River Remembrance Day
* Fun in the Sun Festival
* Blackmere Trade Fair
* Harvest Supper
* Hollow Moon Night
* Grand Showcase
* Toll of Hearths
* Snow Bells Eve
* Aurora Watch
* Salmon Run
* Trout Trials
* Player Wedding
* Cooking With the Twins

---

## Activities

Namespace:

`AsTheBellTolls.Activities`

Purpose:

Owns recurring structured activities that do not warrant an independent major gameplay domain.

Includes:

* Library interaction
* Museum interaction
* Cemetery activities
* Recurring community activities
* Location-specific gameplay services

Systems:

* Library System
* Museum System

Individual large gameplay mechanics such as Farming and Fishing remain independent namespaces.

---

## Mail

Namespace:

`AsTheBellTolls.Mail`

Purpose:

Owns incoming game mail and mailbox behavior.

Includes:

* Letters
* Mail delivery
* Mail eligibility
* Mail read state
* Mail attachments
* Story mail
* Reward mail

Systems:

* Mail System

---

## Audio

Namespace:

`AsTheBellTolls.Audio`

Purpose:

Owns runtime audio behavior.

Includes:

* Music
* Sound Effects
* Ambiance
* Footsteps
* Speech
* Weather Audio
* Audio playback
* Audio transitions
* Audio settings

Systems:

* Audio System

Content:

* Music
* Sound Effects
* Ambiance
* Footsteps
* Speech
* Weather

---

## Input

Namespace:

`AsTheBellTolls.Input`

Purpose:

Owns player input abstraction and control-device handling.

Includes:

* Input Actions
* Input bindings
* Input device detection
* Control schemes
* Rebinding
* Contextual actions
* Platform-specific input prompts

Systems:

* Input System

Platforms include:

* Keyboard / Mouse
* Xbox
* PlayStation
* Nintendo Switch / Switch 2

---

## Camera

Namespace:

`AsTheBellTolls.Camera`

Purpose:

Owns runtime camera behavior.

Includes:

* Camera movement
* Camera following
* Camera bounds
* Camera transitions
* Cinematic camera behavior
* Orthographic camera configuration

Systems:

* Camera System

---

## UI

Namespace:

`AsTheBellTolls.UI`

Purpose:

Owns presentation and player-facing interface behavior.

Includes:

* HUD
* Gameplay menus
* Player Menu
* Panels
* Popups
* Notifications
* Input prompts
* Menu navigation
* UI state
* UI presentation models

Systems:

* UI System
* HUD System
* UI Menu System
* UI Player Menu System

Possible child namespaces:

* `AsTheBellTolls.UI.HUD`
* `AsTheBellTolls.UI.Menus`
* `AsTheBellTolls.UI.Components`
* `AsTheBellTolls.UI.Presentation`

---

# Recommended Dependency List

The primary dependencies for the project are:

* `Core`
* `Data`
* `Events`
* `GameFlow`
* `Save`
* `Time`
* `Calendar`
* `Weather`
* `World`
* `Player`
* `Characters`
* `Animals`
* `Relationships`
* `Dialogue`
* `Interaction`
* `Items`
* `Inventory`
* `Tools`
* `Gathering`
* `Farming`
* `Fishing`
* `Crafting`
* `Inventions`
* `Economy`
* `Commerce`
* `Quests`
* `Progression`
* `Restoration`
* `Festivals`
* `Activities`
* `Mail`
* `Audio`
* `Input`
* `Camera`
* `UI`

---

# Dependency Rules

* `Core` should not depend on any gameplay dependency.
* `Data` should depend only on `Core` wherever possible.
* Gameplay dependencies may depend on `Core`, `Data`, and `Events`.
* A gameplay dependency should own its own authoritative gameplay state.
* Systems should communicate through Events, Services, interfaces, requests, or shared read-only data when possible.
* Avoid circular dependency relationships between gameplay domains.
* `UI` may observe and request actions from gameplay dependencies but should not own gameplay state.
* `Save` may serialize data belonging to other dependencies but should not become the owner of that gameplay data.
* `GameFlow` may coordinate multiple dependencies without absorbing their individual responsibilities.
* `Progression` may evaluate information from multiple gameplay domains but should not duplicate the state owned by those domains.
* Content ScriptableObjects should live with the domain that owns their meaning rather than inside one universal ScriptableObjects namespace.
* Models should live with the domain that owns them rather than inside one universal Models namespace.
* Controllers should live with the feature they control rather than inside one universal Controllers namespace.
* Services and interfaces should normally live with the domain that defines the responsibility they represent.

---
