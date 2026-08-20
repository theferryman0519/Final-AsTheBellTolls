---
Title: Table of Contents
Game: As The Bell Tolls
Owner: Carey Clement Jr
Created: August, 2026
Updated: August, 2026
Version: 0.0.1
---

# Design Files

- Game Design Document
- Table of Contents

---

## Audio

- Ambiance
- Footsteps
- Music
- Sound Effects
- Speech
- Weather

---

## Characters

### Children

- Anya Thorne
- Child Pendrelle (Boy)
- Child Pendrelle (Girl)
- Felix Holt
- Kira Mercer
- Milo Mercer

### Deceased

- Erika Ashcroft
- Ian Thorne
- James Calder
- Myrtle Pendrelle
- Opal Pierce

### Marriage Candidates

- Adrian Lockwood
- Clara Weiss
- Dante Menici
- Leo Finch
- Madison Remington
- Mara Klein
- Marcus Rowan
- Nora Reed
- Sabrina Fairchild
- Victor Cross

### Non-Marriage Candidates

- Agatha Whitmore
- Beatrice Whitmore
- Claudia Mercer
- Edward Ashcroft
- Elise Moreau
- Frederick Holt
- Helen Holt
- Irene Calder
- Jasmine Mercer
- Jonas Mercer
- Julian Hale
- Lena Thorne
- Martin Ashcroft
- Roland Whitmore
- Theo Bennett
- Walter Pierce

### Pets & Animals

- Bee
- Cat
- Chicken
- Cow
- Dog
- Duck
- Goat
- Goose
- Horse
- Pig
- Sheep
- Silkworm

### Politicians

- Rupert Munro

### Rivals

- Lucian/Vivian Darrow

### Traders

- Kai Vale
- Kay Vale
- Kegan Vale
- Kiki Vale

---

## Code Setup

- Controllers
- Data IDs
- Dependencies
- Enums
- Event Channels
- Game Architecture
- Game Flags
- Initialization Order
- Models
- Save Data
- Save Versioning
- Scriptable Objects
- Services

---

## Dialogue

- Bond Events
- Disliked Gifts
- Edward Help
- Favorite Gifts
- Festivals
- First Meet
- Generic Connection Level
- Generic Daylight
- Generic Friendship Level
- Generic Memorial
- Generic Profession
- Generic Season
- Generic Tip
- Generic Weather
- Greetings
- Hated Gifts
- Intro
- Liked Gifts
- Loved Gifts
- Married Life
- Proposals
- Quests (Mid)
- Quests (Post)
- Quests (Pre)
- Shopping
- Tolerated Gifts

---

## Events

### Main Festivals

- Blackmere Trade Festival
- Eve of the White Doe
- Grand Showcase
- Harvest Supper
- Midsummer Splash
- River Remembrance Day
- Seedwake Brunch
- Snow Bell's Eve
- Toll of Hearths

### Mini Festivals

- Aurora Watch
- Cooking with the Twins
- Player Wedding
- Salmon Run
- Trout Trials

### Ongoing Events

- Breakfast at the Inn
- International Trade Cart
- Supper with the Mercers

### Seasonal Data

- Autumn
- Spring
- Summer
- Winter

---

## Inventions

- Cobalt Tier Inventions
- Copper Tier Inventions
- Gold Tier Inventions
- Grand Showcase Inventions
- Iron Tier Inventions
- NPC Quest Inventions
- Silver Tier Inventions

---

## Items

### Clothing Items

- Accessories
- Bottoms
- Footwear
- Hairstyles
- Headwear
- Outfits
- Tops

### Ingredient Items

- Byproducts
- Flowers
- Flower Seeds
- Forageables
- Garden Crops
- Garden Seeds
- Gemstones
- Herbs
- Lake Fish
- Orchard Crops
- Orchard Seeds
- Ore
- River Fish
- Universal Fish

### Interactable Items

- Gravemarkers
- Man Made
- Natural

### Mail Items

- Community Board Requests
- Event Reminders
- Event Requests
- Humor Notes
- Overflow Item
- Quest Requests

### Quest Items

- Recovery

### Recipe Items

- Drinks
- Fabrications
- Meals
- Tonics

### Useful Items

- Dove Timepiece
- Library Books
- Museum Artifacts
- Record Discs
- Statues
- Tools

---

## Locations

### Blackmere

- 1 Resident Lane
- 2 Resident Lane
- 3 Resident Lane
- 4 Resident Lane
- 5 Resident Lane
- 6 Resident Lane
- A. Whitmore Public Library
- A. Whitmore Public School
- Blackmere Bank & Exchange
- Blackmere Bell Tower
- Blackmere General Store
- Blackmere Town Hall
- Caravan Stall
- Hall of Wonder
- Ironveil Forge
- Klein Woodworks
- Rain & Hale
- Riverbend Fishery
- Thread & Thimble
- Weiss Design Studio
- Winding Banks Inn

### Morvanya

- Ashfall Mines
- Gloamwood Depths
- Gloamwood Forest
- Graythorne Lake
- Ironveil Peak
- Memorial Hill
- Mercer Farms
- Steelridge Watch

### Pendrelle Manor

- Barn
- Butler Quarters
- Central Room
- Coop
- Dining Hall
- Garden
- Kitchen
- Laboratory
- Main Bedroom
- Nursery
- Orchard
- Stables

---

## Progression

- Achievement Ledger
- Blueprint Book Progression
- Main Story Progression
- NPC Connection Progression
- NPC Friendship Progression
- Unlock Progression
- Version Roadmap

---

## Quests

- Achievement Quests
- Connection Quests
- Daily Quests
- Friendship Quests
- Tutorial Quests

---

## Systems

- Admin System
- Audio System
- Calendar System
- Camera System
- Cemetery System
- Cooking System
- Crafting System
- Day End Selling System
- Day End System
- Dialogue System
- Economy System
- Family System
- Farming System
- Festival System
- Fishing System
- Game Event System
- Game State System
- Gathering Probability System
- HUD System
- Husbandry System
- Input System
- Interaction System
- Invention System
- Inventory System
- Item System
- Library System
- Loan System
- Mail System
- Map System
- Marriage System
- Museum System
- NPC Connection System
- NPC Friendship System
- NPC Mood System
- NPC Navigation System
- NPC Routine System
- Player Customization System
- Player Movement System
- Quest System
- Resource Respawn System
- Restoration System
- Save System
- Scene System
- Stamina System
- System Interaction Rules
- Time System
- Time Manipulation System
- Tonic Making System
- Tool System
- Tutorial System
- UI Menu System
- UI Player Menu System
- UI System
- Weather System
- Weather Forecast System

---

# Scripts

Notes:
* Controller: Coordinates when things should happen.
* Data: Supporting constant or persistent information.
* Enums: Custom variable options.
* Events: Triggers what just happened.
* Interfaces: Allows other domains to access this domain.
* Models: Current state of pieces of information during gametime.
* Save Data: Converted pieces of information for end of day saving.
* Scriptable Objects: Authored, static, individual pieces of information for models.
* Services: How things should happen.

---

## Activities

- Controller / ActivitiesController.cs
- Data / MinigameFishingConstants.cs
- Data / MinigameTonicMakingConstants.cs
- Enums / MinigameFishingStateType.cs
- Enums / MinigameResultType.cs
- Enums / MinigameTonicMakingStateType.cs
- Enums / MinigameType.cs

---

## Animals

- Controller / AnimalsController.cs
- Enums / AnimalHousingType.cs
- Enums / AnimalLifeStageType.cs
- Enums / AnimalType.cs

---

## Animation

- Controller / AnimationController.cs

## Audio

- Controller / AudioController.cs
- Enums / AudioSpeechSetType.cs
- Enums / AudioType.cs

---

## Bond Events

- Controller / BondEventsController.cs
- Enums / BondEventRequirementType.cs
- Enums / BondEventStateType.cs
- Enums / BondEventType.cs

---

## Calendar

- Controller / CalendarController.cs
- Enums / CalendarSeasonType.cs
- Enums / CalendarWeekDayType.cs

---

## Camera

- Controller / CameraController.cs
- Enums / CameraZoomType.cs

---

## Characters

- Controller / CharactersController.cs
- Enums / CharacterAgeRangeType.cs
- Enums / CharacterBodySizeType.cs
- Enums / CharacterBodyType.cs
- Enums / CharacterEyeColorType.cs
- Enums / CharacterHairColorType.cs
- Enums / CharacterHeightType.cs
- Enums / CharacterMaritalStatusType.cs
- Enums / CharacterMovementStyleType.cs
- Enums / CharacterPronounType.cs
- Enums / CharacterSkinToneType.cs
- Enums / NpcActivityType.cs
- Enums / NpcDialogueLengthType.cs
- Enums / NpcGiftContextType.cs
- Enums / NpcGiftPreferenceType.cs
- Enums / NpcIdelType.cs
- Enums / NpcMoodAffinityType.cs
- Enums / NpcMoodType.cs
- Enums / NpcRelationshipTierType.cs
- Enums / NpcRoutineType.cs
- Enums / NpcSpeakingToneType.cs

---

## Commerce

- Controller / CommerceController.cs

---

## Core

- Controller / CoreController.cs
- Enums / GameEnvironmentType.cs
- Enums / GameStateType.cs
- Services / Singleton.cs

---

## Crafting

- Controller / CraftingController.cs

---

## Data

- Controller / DataController.cs

---

## Dialogue

- Controller / DialogueController.cs
- Enums / DialogueType.cs
- Enums / DialogueVariantType.cs

---

## Economy

- Controller / EconomyController.cs

---

## Event

- Controller / EventController.cs

---

## Farming

- Controller / FarmingController.cs
- Enums / FarmingPlantStateType.cs
- Enums / FarmingPlantType.cs
- Enums / FarmingSoilStateType.cs

---

## Festivals

- Controller / FestivalsController.cs
- Enums / FestivalActivityType.cs
- Enums / FestivalEventType.cs

---

## Fishing

- Controller / FishingController.cs
- Enums / FishHabitatType.cs
- Enums / FishSchoolingType.cs

---

## Game Flow

- Controller / GameFlowController.cs
- Enums / GameFlowSceneType.cs
- Enums / GameplaySceneType.cs

---

## Gathering

- Controller / GatheringController.cs
- Enums / GatherableResourceType.cs

---

## Input

- Controller / InputController.cs
- Enums / InputDeviceType.cs
- Enums / InputMapType.cs

---

## Interaction

- Controller / InteractionController.cs
- Enums / InteractionType.cs

---

## Inventions

- Controller / InventionsController.cs
- Enums / InventionStateType.cs
- Enums / InventionTierType.cs
- Enums / InventionType.cs

---

## Inventory

- Controller / InventoryController.cs
- Enums / InventorySortType.cs

---

## Items

- Controller / ItemsController.cs
- Enums / ArtisanalClothingType.cs
- Enums / ArtisanalItemType.cs
- Enums / IngredientItemType.cs
- Enums / InteractableItemType.cs
- Enums / ItemQualityType.cs
- Enums / ItemStorageType.cs
- Enums / ItemType.cs
- Enums / QuestItemType.cs
- Enums / RecipeItemType.cs
- Enums / UsefulItemType.cs

---

## Mail

- Controller / MailController.cs
- Enums / MailStateType.cs
- Enums / MailType.cs

---

## Player

- Controller / PlayerController.cs

---

## Progression

- Controller / ProgressionController.cs

---

## Quests

- Controller / QuestsController.cs
- Enums / QuestObjectiveType.cs
- Enums / QuestStateType.cs
- Enums / QuestType.cs

---

## Relationships

- Controller / RelationshipsController.cs

---

## Restoration

- Controller / RestorationController.cs
- Enums / RestorationStageType.cs
- Enums / RestorationTargetType.cs

---

## Save

- Controller / SaveController.cs

---

## Templates

- ControllersTemplate.cs
- DataTemplate.cs
- EnumsTemplate.cs
- EventsTemplate.cs
- InterfacesTemplate.cs
- ModelsTemplate.cs
- SaveDataTemplate.cs
- ScriptableObjectsTemplate.cs
- ServicesTemplate.cs

---

## Time

- Controller / TimeController.cs
- Enums / TimeClockFormatType.cs
- Enums / TimeDaylightType.cs

---

## Tonics

- Controller / TonicsController.cs
- Enums / TonicBuffType.cs

---

## Tools

- Controller / ToolsController.cs
- Enums / ToolType.cs

---

## UI

- Controller / UiController.cs

---

## Weather

- Controller / WeatherController.cs
- Enums / WeatherType.cs

---

## World

- Controller / WorldController.cs

---
