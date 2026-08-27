---
Title: Code Setup / Models
Game: As The Bell Tolls
Owner: Carey Clement Jr
Created: August, 2026
Updated: August, 2026
Version: 0.0.1
---

## Animals

- Animal.cs:
    - InstanceId (string)
    - CustomName (string)
    - MatureDay (int)
    - ByproductDay (int)
    - ReceivedByproductToday (bool)
    - LifeStage (AnimalLifeStageType)
    - AnimalObject (AnimalObject)

---

## Animation

*None*

---

## Audio

*None*

---

## Bond Events

- BondEventProgress.cs:
    - Id (string)
    - State (BondEventStateType)
    - HasPlayed (bool)

---

## Calendar

- CalendarDate.cs:
    - DateNumber (int)
    - Year (int)
    - WeekDay (CalendarDayType)
    - Season (CalendarSeasonType)
    - GridCell (Vector2Int)
    - Festivals (List of FestivalObject)
    - Birthdays (List of BirthdayObject)

---

## Camera

*None*

---

## Characters

- Npc.cs:
    - GaveDailyGift (bool)
    - GaveDailyTalk (bool)
    - MetFirstTime (bool)
    - NpcObject (NpcObject)
    - CurrentActivity (NpcActivityType)
    - CurrentLocation (LocationObject)
    - CurrentMood (NpcMoodType)
    - RecentDialogues (List of string)

- NpcPersonalityModel.cs:
    - Type (NpcPersonalityType)
    - Amount (int)

- NpcWeatherMoodModel.cs:
    - Weather (WeatherType)
    - Affinity (NpcMoodAffinityType)

---

## Commerce

- ShopStockModel.cs:
    - Item (ItemObject)
    - CurrentAmount (int)
    - MaximumAmount (int)

---

## Crafting

- CraftingIngredientModel.cs:
    - Item (ItemObject)
    - RequiredAmount (int)
    - SelectedAmount (int)

---

## Dialogue

- Dialogue.cs:
    - CurrentOrder (int)
    - CurrentSpeaker (NpcObject)
    - CurrentVariant (DialogueVariantType)
    - DialogueSet (List of DialogueObject)

---

## Economy

- Economy.cs:
    - CurrentAmount (int)
    - TotalEarned (int)
    - CurrentLoanAmount (int)
    - CurrentlyHaveLoan (bool)

---

## Event

*None*

---

## Farming

- GardenSoil.cs:
    - SoilSquare (int)
    - IsBeingUsed (bool)
    - IsWateredToday (bool)
    - CurrentState (FarmingSoilStateType)
    - Plant (FarmingPlant)

- OrchardSoil.cs:
    - SoilSquare (int)
    - IsBeingUsed (bool)
    - IsWateredToday (bool)
    - CurrentState (FarmingSoilStateType)
    - Plant (FarmingPlant)

- FarmingPlant.cs:
    - Item (IngredientItemObject)
    - PlantType (FarmingPlantType)
    - CurrentState (FarmingPlantStateType)
    - DaysRemaining (int)

---

## Festivals

*None*

---

## Fishing

- FishingAttempt.cs:
    - CurrentState (FishingStateType)
    - FishOnLine (FishObject)

---

## Game Flow

*None*

---

## Gathering

- GatherableResource.cs:
    - ResourceId (string)
    - ResourceType (GatherableResourceType)
    - IsAvailable (bool)
    - RemainingUses (int)
    - RespawnDaysRemaining (int)

---

## Input

*None*

---

## Interactions

*None*

---

## Inventions

- Invention.cs:
    - InventionObject (InventionObject)
    - CurrentState (InventionStateType)
    - IsDiscovered (bool)
    - RemainingCraftingDuration (int)

---

## Inventory

- InventorySlot.cs:
    - SlotNumber (int)
    - StorageType (ItemStorageType)
    - StorageId (string)
    - ItemStack (ItemStack)

---

## Items

- ItemStack.cs:
    - ItemObject (ItemObject)
    - Amount (int)
    - Quality (ItemQualityType)
    - RemainingSpoilDays (int)
    - IsSpoiled (bool)

---

## Mail

- Mail.cs:
    - MailObject (MailObject)
    - State (MailStateType)
    - ReceivedDate (CalendarDate)
    - Attachments (List of ItemStack)
    - AttachmentsCollected (bool)

---

## Movement

*None*

---

## Player

- Player.cs:
    - DisplayName (string)
    - Pronouns (CharacterPronounType)
    - BodySize (CharacterBodySizeType)
    - BodyType (CharacterBodyType)
    - EyeColor (CharacterEyeColorType)
    - HairColor (CharacterHairColorType)
    - Height (CharacterHeightType)
    - SkinTone (CharacterSkinToneType)
    - CurrentStamina (int)
    - MaximumStamina (int)
    - CurrentLocation (LocationObject)
    - Outfit (List of ArtisanalItemObject)

---

## Progression

- AchievementProgress.cs:
    - QuestObject (QuestObject)
    - CurrentAmount (int)
    - RequiredAmount (int)
    - IsCompleted (bool)

- UnlockProgress.cs:
    - Id (string)
    - IsUnlocked (bool)

---

## Quests

- Quest.cs:
    - QuestObject (QuestObject)
    - CurrentState (QuestStateType)
    - CurrentObjectiveAmount (int)
    - RequiredObjectiveAmount (int)
    - AcceptedDate (CalendarDate)
    - ExpirationDate (CalendarDate)

---

## Relationships

- NpcRelationship.cs:
    - NpcObject (NpcObject)
    - FriendshipPoints (int)
    - FriendshipHearts (int)
    - HighestFriendshipHearts (int)
    - ConnectionPoints (int)
    - ConnectionKeys (int)
    - HighestConnectionKeys (int)
    - ConnectionUnlocked (bool)
    - RelationshipTier (NpcRelationshipTierType)

---

## Restoration

- RestorationProgress.cs:
    - TargetId (string)
    - TargetType (RestorationTargetType)
    - CurrentStage (RestorationStageType)
    - IsRestoring (bool)
    - DaysRemaining (int)

---

## Save

- SaveMetadata.cs:
    - SaveSlot (int)
    - SaveVersion (string)
    - PlayerName (string)
    - CurrentDate (CalendarDate)
    - LastSavedUtc (string)


---

## Time

- GameTime.cs:
    - Hour (int)
    - Minute (int)
    - Daylight (TimeDaylightType)
    - ClockFormat (TimeClockFormatType)

---

## Time Manipulation

- TimeManipulation.cs:
    - CurrentChimes (int)
    - MaximumChimes (int)
    - CurrentState (TimeManipulationStateType)
    - UnlockedTypes (List of TimeManipulationType)

---

## Tonics

- TonicMakingAttempt.cs:
    - TonicObject (TonicObject)
    - CurrentState (TonicMakingStateType)
    - SelectedHerbs (List of ItemStack)

- ActiveTonicBuff.cs:
    - TonicObject (TonicObject)
    - BuffType (TonicBuffType)
    - RemainingDuration (int)

---

## Tools

- Tool.cs:
    - ToolObject (ToolObject)
    - Quality (ItemQualityType)
    - IsUnlocked (bool)

---

## UI

*None*

---

## Weather

- WeatherDay.cs:
    - Date (CalendarDate)
    - Weather (WeatherType)

- WeatherForecast.cs:
    - UpcomingWeather (List of WeatherDay)

---

## World

*None*
