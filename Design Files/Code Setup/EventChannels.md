---
Title: Code Setup / Event Channels
Game: As The Bell Tolls
Owner: Carey Clement Jr
Created: August, 2026
Updated: August, 2026
Version: 0.0.1
---

## Game Flow

- GameStateChangedEvent:
    - PreviousState (GameStateType)
    - CurrentState (GameStateType)

- SceneTransitionStartedEvent:
    - DestinationId (string)

- SceneTransitionCompletedEvent:
    - LocationId (string)

---

## Calendar and Time

- TimeChangedEvent:
    - Hour (int)
    - Minute (int)

- DayStartedEvent:
    - Date (CalendarDate)

- DayEndingEvent:
    - Date (CalendarDate)

- DayEndedEvent:
    - Date (CalendarDate)

- DateChangedEvent:
    - PreviousDate (CalendarDate)
    - CurrentDate (CalendarDate)

---

## Weather

- WeatherChangedEvent:
    - PreviousWeather (WeatherType)
    - CurrentWeather (WeatherType)

- ForecastChangedEvent:
    - Forecast (WeatherForecast)

---

## Inventory and Items

- ItemAddedEvent:
    - ItemId (string)
    - Amount (int)
    - Quality (ItemQualityType)

- ItemRemovedEvent:
    - ItemId (string)
    - Amount (int)
    - Quality (ItemQualityType)

- ItemSoldEvent:
    - ItemId (string)
    - Amount (int)
    - Quality (ItemQualityType)
    - BellnotesReceived (int)

---

## Economy

- BellnotesChangedEvent:
    - PreviousAmount (int)
    - CurrentAmount (int)
    - Difference (int)

- LoanChangedEvent:
    - PreviousAmount (int)
    - CurrentAmount (int)

---

## Quests and Progression

- QuestStartedEvent:
    - QuestId (string)

- QuestUpdatedEvent:
    - QuestId (string)
    - CurrentAmount (int)
    - RequiredAmount (int)

- QuestCompletedEvent:
    - QuestId (string)

- QuestFailedEvent:
    - QuestId (string)

- UnlockChangedEvent:
    - Id (string)
    - IsUnlocked (bool)

- AchievementProgressChangedEvent:
    - AchievementId (string)
    - CurrentAmount (int)
    - RequiredAmount (int)

---

## Relationships

- FriendshipChangedEvent:
    - NpcId (string)
    - PreviousPoints (int)
    - CurrentPoints (int)
    - PreviousHearts (int)
    - CurrentHearts (int)

- ConnectionChangedEvent:
    - NpcId (string)
    - PreviousPoints (int)
    - CurrentPoints (int)
    - PreviousKeys (int)
    - CurrentKeys (int)

- RelationshipTierChangedEvent:
    - NpcId (string)
    - PreviousTier (NpcRelationshipTierType)
    - CurrentTier (NpcRelationshipTierType)

---

## Restoration and Inventions

- RestorationStartedEvent:
    - TargetId (string)
    - Stage (RestorationStageType)

- RestorationCompletedEvent:
    - TargetId (string)
    - Stage (RestorationStageType)

- InventionStateChangedEvent:
    - InventionId (string)
    - PreviousState (InventionStateType)
    - CurrentState (InventionStateType)

- InventionCompletedEvent:
    - InventionId (string)

---

## Player and Tools

- StaminaChangedEvent:
    - PreviousAmount (int)
    - CurrentAmount (int)

- ToolEquippedEvent:
    - ToolId (string)

- ToolUpgradedEvent:
    - ToolId (string)
    - Quality (ItemQualityType)

- LocationChangedEvent:
    - PreviousLocationId (string)
    - CurrentLocationId (string)

---

## Game Events

- GameEventEligibleEvent:
    - EventId (string)

- GameEventStartedEvent:
    - EventId (string)

- GameEventCompletedEvent:
    - EventId (string)
