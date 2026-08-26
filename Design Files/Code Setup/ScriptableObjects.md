---
Title: Code Setup / Scriptable Objects
Game: As The Bell Tolls
Owner: Carey Clement Jr
Created: August, 2026
Updated: August, 2026
Version: 0.0.1
---

## Animals

* AnimalObject.cs:
    - DisplayName (string)
    - Id (string)
    - ByproductGrowthDuration (int)
    - MaturityGrowthDuration (int)
    - PurchasePrice (int)
    - SoldPriceBaby (int)
    - SoldPriceMature (int)
    - Housing (AnimalHousingType)
    - Byproducts (List of ItemObject)

---

## Animation

*None*

---

## Audio

* AudioObject.cs:
    - DisplayName (string)
    - Id (string)
    - Clip (AudioClip)
    - SpeechSet (AudioSpeechSetType)
    - Type (AudioType)

---

## Bond Events

*None*

---

## Calendar

*None*

---

## Camera

*None*

---

## Characters

* NpcObject.cs:
    - DisplayName (string)
    - Id (string)
    - FullName (string)
    - Nickname (string)
    - Profession (string)
    - Residence (string)
    - Workplace (string)
    - Quote (string)
    - BirthDate (int)
    - FirstAvailableDate (int)
    - FirstAvailableYear (int)
    - BasePitch (float)
    - PitchVariation (float)
    - Volume (float)
    - VolumeVariation (float)
    - MinimalInterval (float)
    - CanSendMail (bool)
    - CanEnterShowcase (bool)
    - IsMarriageCandidate (bool)
    - AgeRange (CharacterAgeRangeType)
    - BirthSeason (CalendarSeasonType)
    - Pronouns (CharacterPronounType)
    - FirstAvailableSeason (CalendarSeasonType)
    - MaritalStatus (CharacterMaritalStatusType)
    - BodySize (CharacterBodySizeType)
    - BodyType (CharacterBodyType)
    - EyeColor (CharacterEyeColorType)
    - HairColor (CharacterHairColorType)
    - Height (CharacterHeightType)
    - SkinTone (CharacterSkinToneType)
    - SpeakingTone (NpcSpeakingToneType)
    - MovementStyle (MovementStyleType)
    - SpeechSet (AudioSpeechSetType)
    - FavoriteGifts (List of ItemObject)
    - LovedGifts (List of ItemObject)
    - LikedGifts (List of ItemObject)
    - DislikedGifts (List of ItemObject)
    - HatedGifts (List of ItemObject)
    - ShowcaseInventions (List of InventionObject)
    - Personalities (List of NpcPersonalityModel)
    - WeatherMoods (List of NpcWeatherMoodModel)

---

## Commerce

*None*

---

## Crafting

*None*

---

## Dialogue

* DialogueObject.cs:
    - Id (string)
    - Dialogue (string)
    - OrderNumber (int)
    - Type (DialogueType)
    - Variant (DialogueVariantType)
    - Speaker (NpcObject)
    - Options (List of string)

---

## Economy

*None*

---

## Event

*None*

---

## Farming

*None*

---

## Festivals

* FestivalObject.cs:
    - DisplayName (string)
    - Id (string)
    - StartTime (int)
    - EndTime (int)
    - Type (FestivalEventType)
    - Weather (WeatherType)
    - Season (CalendarSeasonType)
    - Activity (FestivalActivityType)
    - Dates (List of int)
    - Rewards (List of ItemObject)
    - SoldItems (List of ItemObject)

## Fishing

* FishObject.cs:
    - DisplayName (string)
    - Id (string)
    - Description (string)
    - Type (ItemType)
    - PurchasePrice (int)
    - ValuePrice (int)
    - ReplenishAmount (int)
    - SpoilDuration (int)
    - CanBeGifted (bool)
    - CanBeInCooking (bool)
    - CanBeInCrafting (bool)
    - CanBeInInventions (bool)
    - CanBeInTonics (bool)
    - CanBeDifferentQualities (bool)
    - IngredientType (IngredientItemType)
    - HabitatType (FishHabitatType)
    - SchoolingType (FishSchoolingType)
    - LocationsSold (List of LocationObject)
    - SeasonsSold (List of CalendarSeasonType)
    - SeasonsFound (List of CalendarSeasonType)

---

## Game Flow

*None*

---

## Gathering

*None*

---

## Input

*None*

---

## Interactions

*None*

---

## Inventions

* InventionObject.cs:
    - DisplayName (string)
    - Id (string)
    - Description (string)
    - Usage (string)
    - CraftingDuration (int)
    - MinShowcaseScore (float)
    - MaxShowcaseScore (float)
    - Type (InventionType)
    - TierType (InventionTierType)
    - Gemstone (ItemObject)
    - RequiredItems (List of ItemObject)

---

## Inventory

*None*

---

## Items

* ItemObject.cs:
    - DisplayName (string)
    - Id (string)
    - Description (string)
    - Type (ItemType)
    - PurchasePrice (int)
    - ValuePrice (int)
    - ReplenishAmount (int)
    - CanBeGifted (bool)
    - CanBeDifferentQualities (bool)
    - LocationsSold (List of LocationObject)
    - SeasonsSold (List of CalendarSeasonType)
    - SeasonsFound (List of CalendarSeasonType)

* ArtisanalItemObject.cs (ItemObject):
    - AvailableColors (List of CharacterHairColorType)
    - ArtisanalType (ArtisanalItemType)
    - ClothingType (ArtisanalClothingType)

* IngredientItemObject.cs (ItemObject):
    - GrowthDuration (int)
    - SpoilDuration (int)
    - CanBeInCooking (bool)
    - CanBeInCrafting (bool)
    - CanBeInInventions (bool)
    - CanBeInTonics (bool)
    - IngredientType (IngredientItemType)

* InteractableItemObject.cs (ItemObject):
    - InteractableType (InteractableItemType)

* GravemarkerItemObject.cs (InteractableItemObject):
    - Text (string)
    - Friendships (List of NpcObject)

* QuestItemObject.cs (ItemObject):
    - QuestType (QuestItemType)
    - BelongsTo (NpcObject)
    - Quest (QuestObject)

* RecipeItemObject.cs (ItemObject):
    - RecipeType (RecipeItemType)
    - ItemsNeeded (List of ItemObject)

* UsefulItemObject.cs (ItemObject):
    - UsefulType (UsefulItemType)

* LibraryBookItemObject.cs (UsefulItemObject):
    - Author (string)
    - BodyText (string)
    - CategoryType (LibraryBookType)

* MuseumArtifactItemObject.cs (UsefulItemObject):
    - ArtifactType (MuseumArtifactType)

* RecordDiscItemObject.cs (UsefulItemObject):
    - SongPlayed (AudioObject)

---

## Mail

* MailObject.cs:
    - DisplayName (string)
    - Id (string)
    - BodyText (string)
    - MailType (MailType)
    - Sender (NpcObject)

---

## Movement

*None*

---

## Player

*None*

---

## Progression

*None*

---

## Quests

* QuestObject.cs:
    - DisplayName (string)
    - Id (string)
    - Backstory (string)
    - Prerequisites (string)
    - Objective (string)
    - Reward (string)
    - Owners (List of NpcObject)

---

## Relationships

*None*

---

## Restoration

*None*

---

## Save

*None*

---

## Time

*None*

---

## Time Manipulation

*None*

---

## Tonics

* TonicObject.cs:
    - DisplayName (string)
    - Id (string)
    - Description (string)
    - BuffDuration (int)
    - ValuePrice (int)
    - Buff (TonicBuffType)
    - HerbsNeeded (List of ItemObject)

---

## Tools

* ToolObject.cs:
    - DisplayName (string)
    - Id (string)
    - Description (string)
    - Usage (string)
    - CanBeUpgraded (bool)
    - Type (ToolType)

---

## UI

*None*

---

## Weather

*None*

---

## World

* LocationObject.cs:
    - DisplayName (string)
    - Description (string)
    - FirstAvailableDate (int)
    - FirstAvailableYear (int)
    - OpeningHour (int)
    - ClosingHour (int)
    - FirstAvailableSeason (CalendarSeasonType)
    - ClosedDays (List of CalendarDayType)
    - ClosedWeatherTypes (List of WeatherType)
