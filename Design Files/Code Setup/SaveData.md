---
Title: Code Setup / Save Data
Game: As The Bell Tolls
Owner: Carey Clement Jr
Created: August, 2026
Updated: September, 2026
Version: 0.0.1
---

## Admin

- AdminSaveData.cs:
    - Metadata (SaveMetadataData)
    - Player (PlayerSaveData)
    - Calendar (CalendarSaveData)
    - Time (TimeSaveData)
    - Dialogue (DialogueSaveData)
    - Economy (EconomySaveData)
    - Inventory (InventorySaveData)
    - Tools (List of ToolSaveData)
    - Animals (List of AnimalSaveData)
    - Farming (FarmingSaveData)
    - Gathering (List of GatherableResourceSaveData)
    - Npcs (List of NpcSaveData)
    - Relationships (List of RelationshipSaveData)
    - Quests (List of QuestSaveData)
    - BondEvents (List of BondEventSaveData)
    - Restoration (List of RestorationSaveData)
    - Inventions (List of InventionSaveData)
    - Mail (List of MailSaveData)
    - Progression (ProgressionSaveData)
    - TimeManipulation (TimeManipulationSaveData)
    - Tonics (TonicSaveData)
    - Weather (WeatherSaveData)
    - World (WorldSaveData)

---

## Root Save Data

- GameSaveData.cs:
    - Metadata (SaveMetadataData)
    - Player (PlayerSaveData)
    - Calendar (CalendarSaveData)
    - Time (TimeSaveData)
    - Dialogue (DialogueSaveData)
    - Economy (EconomySaveData)
    - Inventory (InventorySaveData)
    - Tools (List of ToolSaveData)
    - Animals (List of AnimalSaveData)
    - Farming (FarmingSaveData)
    - Gathering (List of GatherableResourceSaveData)
    - Npcs (List of NpcSaveData)
    - Relationships (List of RelationshipSaveData)
    - Quests (List of QuestSaveData)
    - BondEvents (List of BondEventSaveData)
    - Restoration (List of RestorationSaveData)
    - Inventions (List of InventionSaveData)
    - Mail (List of MailSaveData)
    - Progression (ProgressionSaveData)
    - TimeManipulation (TimeManipulationSaveData)
    - Tonics (TonicSaveData)
    - Weather (WeatherSaveData)
    - World (WorldSaveData)

---

## Metadata

- SaveMetadataData.cs:
    - SaveSlot (int)
    - SaveVersion (string)
    - PlayerName (string)
    - CurrentDay (int)
    - CurrentSeason (CalendarSeasonType)
    - CurrentYear (int)
    - LastSavedUtc (string)

---

## Player

- PlayerSaveData.cs:
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
    - CurrentLocationId (string)
    - OutfitItemIds (List of string)

---

## Calendar and Time

- CalendarSaveData.cs:
    - DateNumber (int)
    - Year (int)
    - WeekDay (CalendarDayType)
    - Season (CalendarSeasonType)

- TimeSaveData.cs:
    - Hour (int)
    - Minute (int)
    - Daylight (TimeDaylightType)
    - ClockFormat (TimeClockFormatType)

---

## Dialogue

- DialogueSaveData.cs:
    - DialogueSets (List of DialogueSaveData)

- DialogueSetSaveData.cs:
    - DialogueIds (List of string)
    - LastTriggeredDay (int)
    - LastTriggeredSeason (CalendarSeasonType)
    - LastPlayedDay (int)
    - LastPlayedSeason (CalendarSeasonType)
    - IsInCooldown (bool)

---

## Economy

- EconomySaveData.cs:
    - CurrentAmount (int)
    - TotalEarned (int)
    - CurrentLoanAmount (int)
    - CurrentlyHaveLoan (bool)

---

## Items and Inventory

- ItemStackSaveData.cs:
    - ItemId (string)
    - Amount (int)
    - Quality (ItemQualityType)
    - RemainingSpoilDays (int)
    - IsSpoiled (bool)

- InventorySlotSaveData.cs:
    - SlotNumber (int)
    - StorageType (ItemStorageType)
    - StorageId (string)
    - ItemStack (ItemStackSaveData)

- InventorySaveData.cs:
    - Slots (List of InventorySlotSaveData)

---

## Tools

- ToolSaveData.cs:
    - ToolId (string)
    - Quality (ItemQualityType)
    - IsUnlocked (bool)

---

## Animals

- AnimalSaveData.cs:
    - InstanceId (string)
    - AnimalId (string)
    - CustomName (string)
    - MatureDay (int)
    - ByproductDay (int)
    - ReceivedByproductToday (bool)
    - LifeStage (AnimalLifeStageType)

---

## Farming

- FarmingPlantSaveData.cs:
    - ItemId (string)
    - PlantType (FarmingPlantType)
    - CurrentState (FarmingPlantStateType)
    - DaysRemaining (int)

- SoilSaveData.cs:
    - SoilSquare (int)
    - IsBeingUsed (bool)
    - IsWateredToday (bool)
    - CurrentState (FarmingSoilStateType)
    - Plant (FarmingPlantSaveData)

- FarmingSaveData.cs:
    - GardenSoil (List of SoilSaveData)
    - OrchardSoil (List of SoilSaveData)

---

## Gathering

- GatherableResourceSaveData.cs:
    - ResourceId (string)
    - ResourceType (GatherableResourceType)
    - IsAvailable (bool)
    - RemainingUses (int)
    - RespawnDaysRemaining (int)

---

## Characters

- NpcSaveData.cs:
    - NpcId (string)
    - GaveDailyGift (bool)
    - GaveDailyTalk (bool)
    - MetFirstTime (bool)
    - CurrentActivity (NpcActivityType)
    - CurrentLocationId (string)
    - CurrentMood (NpcMoodType)

---

## Relationships

- RelationshipSaveData.cs:
    - NpcId (string)
    - FriendshipPoints (int)
    - FriendshipHearts (int)
    - HighestFriendshipHearts (int)
    - ConnectionPoints (int)
    - ConnectionKeys (int)
    - HighestConnectionKeys (int)
    - ConnectionUnlocked (bool)
    - RelationshipTier (NpcRelationshipTierType)

---

## Quests

- QuestSaveData.cs:
    - QuestId (string)
    - CurrentState (QuestStateType)
    - CurrentObjectiveAmount (int)
    - RequiredObjectiveAmount (int)
    - AcceptedDate (CalendarSaveData)
    - ExpirationDate (CalendarSaveData)

---

## Bond Events

- BondEventSaveData.cs:
    - Id (string)
    - State (BondEventStateType)
    - HasPlayed (bool)

---

## Restoration

- RestorationSaveData.cs:
    - TargetId (string)
    - TargetType (RestorationTargetType)
    - CurrentStage (RestorationStageType)
    - IsRestoring (bool)
    - DaysRemaining (int)

---

## Inventions

- InventionSaveData.cs:
    - InventionId (string)
    - CurrentState (InventionStateType)
    - IsDiscovered (bool)
    - RemainingCraftingDuration (int)

---

## Mail

- MailSaveData.cs:
    - MailId (string)
    - State (MailStateType)
    - ReceivedDate (CalendarSaveData)
    - Attachments (List of ItemStackSaveData)
    - AttachmentsCollected (bool)

---

## Progression

- AchievementSaveData.cs:
    - QuestId (string)
    - CurrentAmount (int)
    - RequiredAmount (int)
    - IsCompleted (bool)

- UnlockSaveData.cs:
    - Id (string)
    - IsUnlocked (bool)

- ProgressionSaveData.cs:
    - Achievements (List of AchievementSaveData)
    - Unlocks (List of UnlockSaveData)

---

## Time Manipulation

- TimeManipulationSaveData.cs:
    - CurrentChimes (int)
    - MaximumChimes (int)
    - CurrentState (TimeManipulationStateType)
    - UnlockedTypes (List of TimeManipulationType)

---

## Tonics

- ActiveTonicBuffSaveData.cs:
    - TonicId (string)
    - BuffType (TonicBuffType)
    - RemainingDuration (int)

- TonicSaveData.cs:
    - ActiveBuffs (List of ActiveTonicBuffSaveData)

---

## Weather

- WeatherDaySaveData.cs:
    - Date (CalendarSaveData)
    - Weather (WeatherType)

- WeatherSaveData.cs:
    - CurrentWeather (WeatherType)
    - UpcomingWeather (List of WeatherDaySaveData)

---

## World

- WorldSaveData.cs:
    - GameFlags (List of string)
