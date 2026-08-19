---
Title: Code Setup / Enums
Game: As The Bell Tolls
Owner: Carey Clement Jr
Created: August, 2026
Updated: August, 2026
Version: 0.0.1
---

## Core Game

### GameStateType

- None (default)
- Gameplay
- Dialogue
- Menu
- Minigame
- Cinematic
- Festival
- Transition
- DayEnd
- Paused

### GameSceneType

- None (default)
- Opening
- Gameplay
- Festival
- Cinematic
- Sleep
- Transition

### GameplaySceneType

- None (default)
- Exterior
- Interior

### GameEnvironmentType

- None (default)
- Indoor
- Outdoor

### GameCameraZoomType

- Middle (default)
- Near
- Far

---

## Calendar & Time

### CalendarSeasonType

- None (default)
- Spring
- Summer
- Autumn
- Winter

### CalendarWeekDayType

- None (default)
- Monday
- Tuesday
- Wednesday
- Thursday
- Friday
- Saturday
- Sunday

### TimeDaylightType

- None (default)
- Dawn
- Day
- Dusk
- Night

### TimeClockFormatType

- TwelveHour (default)
- TwentyFourHour

---

## Weather

### WeatherType

- None (default)
- Clear
- Cloudy
- Rainy
- RainySevere
- Snowy
- SnowySevere
- Windy

---

## Restoration

### RestorationStageType

- Weathered (default)
- Rebuilding
- Recovering
- Renewed
- Growing
- Prospering
- Flourishing

### RestorationTargetType

- None (default)
- ManorRoom
- TownBuilding

---

## Items

### ItemType

- None (default)
- Artisanal
- Ingredient
- Interactable
- Quest
- Recipe
- Useful

### ArtisanalItemType

- None (default)
- Clothing
- Decor
- Furniture

### ArtisanalClothingType

- None (default)
- Outfit
- Top
- Bottom
- Footwear
- Headwear
- Accessory
- Hairstyle

### IngredientItemType

- None (default)
- Byproduct
- Flower
- FlowerSeed
- Forageable
- GardenCrop
- GardenSeed
- OrchardCrop
- OrchardSeed
- Gemstone
- Herb
- Fish
- Ore

### InteractableItemType

- None (default)
- ManMade
- Natural
- Gravemarker

### QuestItemType

- None (default)
- Recovery

### RecipeItemType

- None (default)
- Drink
- Fabrication
- Meal
- Tonic

### UsefulItemType

- None (default)
- LibraryBook
- MuseumArtifact
- RecordDisc
- Tool
- SpecialItem

### ItemQualityType

- Base (default)
- Copper
- Iron
- Silver
- Gold
- Cobalt

### ItemStorageType

- None (default)
- ToolBelt
- Satchel
- Pantry
- Mailbox
- ByproductBox
- StorageUnit

---

## Inventory

### InventorySortType

- None (default)
- Quality
- Name
- Count

---

## Tools

### ToolType

- None (default)
- Axe
- FishingNet
- FishingRod
- Hammer
- HerbKnife
- Hoe
- Pickaxe
- Scythe
- WateringCan

---

## Gathering / Interacting

### GatherableResourceType

- None (default)
- FlowerDeposit
- GemstoneDeposit
- Grass
- HerbBush
- LakeWater
- OreDeposit
- RiverWater
- RockDeposit
- Tree
- TreeTrunk

### InteractionType

- None (default)
- Talk
- Shop
- Gift
- Harvest
- Gather
- Fish
- Plant
- Water
- Till
- Chop
- Mine
- Cut
- Enter
- Exit
- Read
- Open
- Use
- Cook
- Craft
- MakeTonic
- Restore
- Customize
- Store
- Travel

---

## Farming

### FarmingPlantStateType

- None (default)
- Seeded
- Growing
- Harvestable

### FarmingSoilStateType

- Unavailable (default)
- Refueling
- Ready
- Tilled

### FarmingPlantType

- None (default)
- GardenCrop
- OrchardCrop
- Flower

---

## Husbandry

### AnimalType

- None (default)
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

### AnimalHousingType

- None (default)
- Apiary
- Barn
- Coop
- Manor
- Stable
- WeaverCradle

### AnimalLifeStageType

- Baby (default)
- Mature

---

## Fish

### FishHabitatType

- None (default)
- River
- Lake
- Universal

### FishSchoolingType

- Solitary (default)
- Schooling

---

## NPCs

### NpcRelationshipTierType

- Stranger (default)
- Acquaintance
- Friendly
- Friend
- Trusted
- Spouse
- SoulMate

### NpcMoodType

- Indifferent (default)
- Angry
- Sad
- Tired
- Content
- Happy
- InLove

### NpcMoodAffinityType

- Neutral (default)
- Disliked
- Liked

### NpcGiftPreferenceType

- None (default)
- Favorite
- Loved
- Liked
- Tolerated
- Disliked
- Hated

### NpcGiftContextType

- Normal (default)
- Birthday

### NpcDialogueLengthType

- None (default)
- Short
- Mid
- Long
- Excruciating

### NpcSpeakingToneType

- None (default)
- Gentle
- Formal
- Dry
- Reflective
- Compassionate
- Guarded
- Energetic
- Playful

### NpcRoutineType

- Standard (default)
- Seasonal
- Weather
- Special
- Quest
- Festival
- GameEvent

### NpcIdleType

- None (default)
- Stand
- Wander
- Service

### NpcActivityType

- None (default)
- Sleep
- Idle
- Travel
- Work
- Shopkeeping
- Eat
- Socialize
- Fish
- Read
- Study
- Explore
- Festival
- Quest
- Special

---

## Character

### CharacterHeightType

- Middle (default)
- Short
- Tall

### CharacterBodySizeType

- Normal (default)
- Skinny
- Slim
- Fit
- Curvy

### CharacterBodyType

- Androgynous (default)
- Feminine
- Masculine

### CharacterSkinToneType

- Olive (default)
- Deep
- Espresso
- Bronze
- Fair
- Pale

### CharacterHairColorType

- Brown (default)
- Black
- Blonde
- Blue
- Green
- Purple
- Red
- White

### CharacterEyeColorType

- Brown (default)
- Amber
- Blue
- Green
- Hazel

### CharacterMovementStyleType

- Normal (default)
- Purposeful
- Hesitant
- Energetic
- Heavy
- Graceful

### CharacterPronounType

- TheyThem (default)
- HeHim
- SheHer

### CharacterAgeRangeType

- Twenties (default)
- Child
- Teens
- Thirties
- Forties
- Fifties
- Sixties
- Seventies

### CharacterMaritalStatusType

- Single (default)
- Married
- Child
- Widowed

---

## Quests

### QuestType

- None (default)
- MainStory
- Achievement
- Connection
- Daily
- Friendship
- Tutorial

### QuestStateType

- Inactive (default)
- Available
- Active
- Completed
- Failed
- Expired

### QuestObjectiveType

- None (default)
- Talk
- Collect
- Deliver
- Craft
- Cook
- Fish
- Restore
- Purchase
- ReachLocation
- ReachFriendship
- ReachConnection
- CompleteEvent
- Custom

---

## Bond Events

### BondEventType

- None (default)
- MainStory
- Friendship
- Connection
- Tutorial
- Restoration
- Other

### BondEventStateType

- Inactive (default)
- Eligible
- Playing
- Completed

### BondEventRequirementType

- None (default)
- MainStoryProgress
- FriendshipLevel
- ConnectionLevel
- QuestState
- PreviousEvent
- ManorRestoration
- TownRestoration
- Season
- Day
- DayOfWeek
- Time
- Daylight
- Weather
- Location
- NpcAvailability
- RelationshipStatus
- MarriageStatus
- ItemPossession
- GameFlag

---

## Festivals & Communal Events

### FestivalEventType

- None (default)
- MainFestival
- MiniFestival
- OngoingEvent

### FestivalActivityType

- None (default)
- SharedMeal
- Fishing
- Market
- Cooking
- Stargazing
- Wedding
- OrbHunt
- Memorial
- Dance
- JudgeShow
- WaterSplash

---

## Inventions

### InventionType

- None (default)
- Blueprint
- Quest
- GrandShowcase

### InventionTierType

- None (default)
- Copper
- Iron
- Silver
- Gold
- Cobalt

### InventionStateType

- Locked (default)
- Available
- InProgress
- Completed

---

## Tonics

### TonicBuffType

- None (default)
- GatheringDouble
- GatheringQuality
- GatheringType
- SocialIncrease
- SpeedIncrease
- StaminaMax
- StaminaSlow

---

## Audio

### AudioType

- None (default)
- Music
- Ambiance
- SoundEffect
- Weather
- Footstep
- Speech

### AudioSpeechSetType

- None (default)
- FemaleA
- FemaleB
- MaleA
- MaleB

---

## Input

### InputMapType

- None (default)
- Gameplay
- UI
- Dialogue
- Fishing
- TonicMaking

### InputDeviceType

- Unknown (default)
- KeyboardMouse
- Xbox
- PlayStation
- Switch

---

## Minigame

### MinigameType

- None (default)
- Fishing
- TonicMaking

### MinigameResultType

- None (default)
- Success
- Failure
- Cancelled

### MinigameFishingStateType

- Idle (default)
- Casting
- Waiting
- Hooked
- Reeling
- Completing

### MinigameTonicMakingStateType

- Idle (default)
- Adding
- Mixing
- Boiling
- Completing

---

## Mail

### MailType

- None (default)
- HumorNote
- EventReminder
- QuestRequest
- EventRequest
- OverflowItem

---

## Dialogue

### DialogueType

- None (default)
- Generic
- Festival
- Gift
- Quest
- BondEvent
- ConnectionEvent
- EdwardHelp
- Shopping
- FirstMeet
- Proposal
- Greeting
- MarriedLife
- Intro

### DialogueVariantType

- None (default)
- VariantA
- VariantB
- VariantC
