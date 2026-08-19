---
Title: Code Setup / Services
Game: As The Bell Tolls
Owner: Carey Clement Jr
Created: August, 2026
Updated: August, 2026
Version: 0.0.1
---

## Information

- Services perform reusable operations that do not primarily own long-lived gameplay state.
- Services exist to separate reusable operations from Systems, Controllers, Views, and other architectural components.
- Services should have a clear responsibility and a well-defined input and output.
- Services may be used by multiple Systems or Controllers when the same operation is required in several places.
- Services should generally remain independent of Unity scene objects unless the Service specifically abstracts a Unity or platform capability.
- Services should not become alternate gameplay Systems.
- Services should not silently own persistent gameplay progression.
- Services may use interfaces when implementations may vary, require testing substitutes, or depend on platform-specific behavior.
- Services should generally be constructed or provided through explicit dependencies rather than accessed through global static locators.
- A Service should only be created when extracting the operation provides meaningful architectural value.

---

# Purpose

Services answer questions such as:

- How should this reusable calculation be performed?
- How should this data be formatted?
- How should this content be resolved?
- How should this platform-specific operation be performed?
- How should this data be serialized?
- How should this condition be evaluated?
- How should this reward be calculated?
- How should this path-independent utility operation be shared?

Examples may include:

- Price calculations.
- Gift reaction calculations.
- Dialogue condition evaluation.
- Requirement evaluation.
- Reward resolution.
- Save serialization.
- Platform storage.
- Localization lookup.
- Random selection.
- Data validation.
- Formatting.
- Mapping between data structures.

---

# Core Principle

A System owns gameplay state and rules.

A Service performs an operation.

Example:

```text
Economy System
    ↓
Owns player's Bells

Sell Price Service
    ↓
Calculates an Item's sell value
```

Another example:

```text
NPC Friendship System
    ↓
Owns Friendship progression

Gift Evaluation Service
    ↓
Determines authored gift preference and applicable gain
```

Another example:

```text
Save System
    ↓
Coordinates Save and Load

Save Serializer
    ↓
Converts Save Data to and from serialized text or bytes
```

---

# Service Responsibilities

Services may:

- Perform reusable calculations.
- Evaluate conditions.
- Validate data.
- Transform data.
- Map between structures.
- Resolve static content.
- Format information.
- Serialize and deserialize data.
- Abstract platform APIs.
- Abstract storage.
- Provide deterministic random selection when designed appropriately.
- Perform reusable queries over supplied data.
- Encapsulate external APIs.
- Encapsulate implementation details shared by several Systems.

Services should not:

- Own unrelated gameplay state.
- Become a substitute for Systems.
- Directly own player progression.
- Directly own Inventory.
- Directly own Friendship.
- Directly own Quests.
- Directly own Calendar state.
- Directly own Weather state.
- Directly control UI unless specifically a presentation infrastructure Service.
- Become a global dumping ground for helper methods.
- Hide circular dependencies.
- Mutate another System's private state.
- Raise gameplay Events for changes they do not own.
- Depend on arbitrary scene GameObjects unless that is the Service's explicit purpose.

---

# Systems vs Services

The distinction between Systems and Services should remain clear.

## System

A System generally:

- Owns authoritative runtime state.
- Implements gameplay rules around that state.
- Has a gameplay lifecycle.
- May participate in Save and Load.
- May publish Event Channels after state changes.
- May coordinate related gameplay operations.

Examples:

- `InventorySystem`
- `EconomySystem`
- `QuestSystem`
- `NpcFriendshipSystem`
- `WeatherSystem`
- `FarmingSystem`

## Service

A Service generally:

- Performs a reusable operation.
- Receives required information as input.
- Returns a result.
- Does not own the primary persistent state involved.
- May be stateless.
- May encapsulate an external or platform implementation.

Examples:

- `SellPriceService`
- `GiftEvaluationService`
- `ConditionEvaluationService`
- `SaveSerializer`
- `SaveStorageService`

---

# System or Service Decision

Use a System when the component answers:

"What is currently true?"

Examples:

- What Items does the player own?
- What is the current Weather?
- What Quests are active?
- How much Friendship does Adrian have?
- What Restoration state is Town Hall in?

Use a Service when the component answers:

"Given this input, what operation or calculation should be performed?"

Examples:

- What is this Item worth at Gold quality?
- Does this context satisfy these conditions?
- How should this Save Data be serialized?
- Which Dialogue entry best matches this supplied context?

---

# Services vs Controllers

Controllers coordinate Unity-facing behavior.

Services perform reusable operations.

Example:

```text
InventoryUiController
    ↓
Requests formatted Item information

ItemDisplayService
    ↓
Produces display-ready information
```

The Controller remains responsible for:

- UI interactions.
- Unity callbacks.
- View coordination.

The Service remains responsible for the reusable operation.

---

# Services vs Models

Models contain structured information.

Services operate on information.

Example:

```text
SellPriceRequest
    ↓
SellPriceService
    ↓
SellPriceCalculation
```

`SellPriceRequest` and `SellPriceCalculation` are Models.

`SellPriceService` performs the calculation.

Related Notes:

- Models

---

# Services vs ScriptableObjects

ScriptableObjects define static content and configuration.

Services interpret or operate on that data.

Example:

```text
NpcDefinition
    ↓
Gift preferences

GiftEvaluationService
    ↓
Evaluates supplied gift
```

Another example:

```text
RecipeDefinition
    ↓
Ingredient requirements

RequirementEvaluationService
    ↓
Checks supplied Inventory information
```

ScriptableObjects should not be converted into Services merely because they contain configuration.

Related Notes:

- Scriptable Objects

---

# Services vs Event Channels

Services perform operations.

Event Channels announce that something happened.

Example:

```text
SellPriceService.Calculate(...)
```

returns a value directly.

It should not raise:

```text
SellPriceCalculated
```

unless there is a genuine independent notification requirement.

Prefer direct return values for Service operations.

Event Channels should remain focused on meaningful runtime notifications.

Related Notes:

- Event Channels

---

# Service Categories

Useful Service categories may include:

- Calculation Services
- Evaluation Services
- Validation Services
- Mapping Services
- Formatting Services
- Serialization Services
- Storage Services
- Content Lookup Services
- Selection Services
- Platform Services
- Presentation Infrastructure Services

Not every category requires a Service.

---

# Calculation Services

Calculation Services perform reusable calculations.

Examples:

- `SellPriceService`
- `FriendshipGainService`
- `StaminaCostService`
- `CropYieldService`
- `FishingProbabilityService`
- `GatheringDropService`
- `TonicEffectService`
- `CompletionPercentageService`

Example:

```csharp
public interface ISellPriceService
{
    int Calculate(
        ItemDefinition item,
        ItemQuality quality,
        float bonusMultiplier);
}
```

Implementation:

```csharp
public sealed class SellPriceService : ISellPriceService
{
    public int Calculate(
        ItemDefinition item,
        ItemQuality quality,
        float bonusMultiplier)
    {
        float qualityMultiplier =
            GetQualityMultiplier(quality);

        float value =
            item.BaseSellValue *
            qualityMultiplier *
            bonusMultiplier;

        return Mathf.Max(
            0,
            Mathf.RoundToInt(value));
    }

    private static float GetQualityMultiplier(
        ItemQuality quality)
    {
        return quality switch
        {
            ItemQuality.Base => 1f,
            ItemQuality.Copper => 1.1f,
            ItemQuality.Silver => 1.25f,
            ItemQuality.Gold => 1.5f,
            ItemQuality.Cobalt => 2f,
            _ => 1f
        };
    }
}
```

Exact balance values belong in the appropriate configuration when designer tuning is required.

---

# Evaluation Services

Evaluation Services determine whether supplied information meets defined criteria.

Examples:

- `ConditionEvaluationService`
- `DialogueConditionService`
- `QuestRequirementService`
- `GiftEvaluationService`
- `UnlockEvaluationService`
- `FestivalEligibilityService`

Example:

```csharp
public interface IConditionEvaluationService
{
    bool Evaluate(
        ConditionDefinition condition,
        GameplayContext context);
}
```

Evaluation Services should not silently mutate gameplay state.

They answer questions.

The owning System decides what to do with the answer.

---

# Validation Services

Validation Services validate structured data or complex operations.

Examples:

- `SaveValidationService`
- `DefinitionValidationService`
- `InventoryTransferValidationService`
- `CharacterNameValidationService`

Validation Services are useful when validation logic:

- Is reused.
- Is sufficiently complex.
- Does not belong exclusively to one Model.
- Benefits from isolated testing.

Simple local validation should remain near the owning code rather than being extracted unnecessarily.

---

# Mapping Services

Mapping Services convert one representation into another.

Examples:

- Runtime Model → Save Data.
- Runtime Model → Presentation Model.
- Definition + Runtime State → Display Model.
- Platform data → Internal Model.

Example:

```csharp
public interface IRelationshipDisplayMapper
{
    RelationshipDisplayModel Map(
        NpcDefinition npc,
        FriendshipSnapshot friendship);
}
```

Use a Mapper when conversion logic is substantial or repeated.

Do not create a Mapper for every trivial constructor call.

---

# Formatting Services

Formatting Services convert values into human-readable presentation.

Examples:

- `GameDateFormattingService`
- `GameTimeFormattingService`
- `CurrencyFormattingService`
- `PlaytimeFormattingService`
- `InputPromptFormattingService`

Example:

```csharp
public interface IGameTimeFormattingService
{
    string Format(GameTimeSnapshot time);
}
```

A formatting Service should not become the owner of the value being formatted.

---

# Serialization Services

Serialization Services convert Save Data into a storage representation.

Example:

```csharp
public interface ISaveSerializer
{
    string Serialize(GameSaveData saveData);

    GameSaveData Deserialize(string serializedData);
}
```

Possible implementation:

```csharp
public sealed class JsonSaveSerializer : ISaveSerializer
{
    public string Serialize(
        GameSaveData saveData)
    {
        return JsonUtility.ToJson(
            saveData,
            prettyPrint: false);
    }

    public GameSaveData Deserialize(
        string serializedData)
    {
        return JsonUtility.FromJson<GameSaveData>(
            serializedData);
    }
}
```

The final serializer may use a different JSON library or format.

The Save System should depend on the abstraction rather than hardcoding storage and serialization together.

---

# Storage Services

Storage Services abstract reading and writing persistent files.

Example:

```csharp
public interface ISaveStorageService
{
    bool Exists(string path);

    string Read(string path);

    void Write(
        string path,
        string contents);

    void Delete(string path);
}
```

Platform-specific implementations may differ.

Example:

```text
PCSaveStorageService
ConsoleSaveStorageService
```

Gameplay Systems should not directly use platform file APIs.

---

# Platform Services

Platform Services isolate platform-specific functionality.

Examples:

- Save storage.
- Achievements.
- Platform user identity.
- Controller platform detection.
- Platform-specific file paths.
- Cloud save integration.

General flow:

```text
Gameplay / Infrastructure
        ↓
Platform Interface
        ↓
Platform Implementation
```

This allows gameplay code to remain platform-independent.

---

# Content Lookup Services

Most static content lookup should be handled by Data Registries.

A Service may be appropriate when lookup requires additional behavior.

Example:

```text
Item Registry
    ↓
Direct ID → ItemDefinition lookup
```

versus:

```text
ItemSearchService
    ↓
Searches definitions by multiple tags,
categories, availability rules, or criteria
```

Do not wrap every Registry method in a Service without additional value.

---

# Selection Services

Selection Services choose one result from supplied candidates.

Examples:

- `DialogueSelectionService`
- `WeatherSelectionService`
- `LootSelectionService`
- `FishingSelectionService`
- `NpcRoutineSelectionService`

Selection may depend on:

- Weights.
- Conditions.
- Priority.
- Randomness.
- Current context.

Example:

```csharp
public interface IDialogueSelectionService
{
    DialogueDefinition Select(
        IReadOnlyList<DialogueDefinition> candidates,
        DialogueContext context);
}
```

If selection changes authoritative state, the owning System should apply that change after receiving the result.

---

# Random Services

Randomness should be abstracted when deterministic behavior or testing matters.

Example:

```csharp
public interface IRandomService
{
    int Range(
        int minimumInclusive,
        int maximumExclusive);

    float Value();
}
```

Implementation may wrap Unity randomness.

A seeded implementation may support deterministic gameplay where required.

Benefits:

- Easier tests.
- Reproducible bugs.
- Deterministic systems.
- Reduced direct dependency on `UnityEngine.Random`.

---

# Randomness Ownership

A Random Service generates random values.

It should not decide entire gameplay outcomes unless that selection logic is its explicit responsibility.

Example:

```text
Random Service
    ↓
Provides random value

Fishing Selection Service
    ↓
Applies fishing weights and rules

Fishing System
    ↓
Owns active fishing gameplay state
```

---

# Localization Services

Localization should be abstracted when the project begins supporting localized content.

Example:

```csharp
public interface ILocalizationService
{
    string Get(string localizationKey);
}
```

ScriptableObjects may store localization keys.

Presentation code requests resolved text.

Stable Data IDs remain separate from localization keys.

---

# Audio Services

Audio architecture may use Services for reusable playback infrastructure.

Possible examples:

- `AudioPlaybackService`
- `MusicTransitionService`
- `AudioClipSelectionService`

However, gameplay Systems should not depend heavily on concrete Audio playback.

Preferred:

```text
Gameplay state changes
    ↓
Event / Controller
    ↓
Audio presentation infrastructure
```

Audio Services should not own gameplay progression.

---

# Input Services

Input itself may be exposed through an Input System or Service depending on the final architecture.

Platform input-label resolution may be a Service.

Example:

```csharp
public interface IInputPromptService
{
    InputPromptModel GetPrompt(
        GameplayAction action);
}
```

This may resolve:

- Keyboard prompt.
- Xbox prompt.
- PlayStation prompt.
- Nintendo Switch prompt.

The HUD displays the result.

The Service does not decide whether the gameplay action is valid.

---

# Condition Evaluation

Conditions should be declarative where practical.

Example definition:

```text
Condition:
Friendship with Adrian >= 4 Hearts
Season = Autumn
Weather != Stormy
```

Runtime flow:

```text
Definition
    ↓
ConditionEvaluationService
    ↓
GameplayContext
    ↓
true / false
```

The Service should query supplied context or explicit read-only dependencies.

Avoid allowing arbitrary condition definitions to mutate state.

---

# Requirement Services

Requirement evaluation may be useful across:

- Quests.
- Crafting.
- Restoration.
- Inventions.
- Festivals.
- Dialogue.
- Shops.

Example:

```csharp
public interface IRequirementEvaluationService
{
    RequirementResult Evaluate(
        IReadOnlyList<RequirementDefinition> requirements,
        RequirementContext context);
}
```

The Result may identify:

- Success.
- Missing Items.
- Missing Bells.
- Missing Friendship.
- Missing Restoration state.
- Missing prerequisite.

---

# Reward Services

A distinction should be made between:

```text
Reward Calculation
```

and:

```text
Reward Application
```

A Service may calculate or resolve a reward.

Authoritative mutation should remain with owning Systems.

Example:

```text
Quest System
    ↓
Reward Definition
    ↓
Reward Resolution Service
    ↓
Resolved Rewards
    ↓
Quest System coordinates:
        Economy System
        Inventory System
        Unlock System
```

Avoid a Reward Service that reaches into every System and silently mutates unrelated state unless it is explicitly designed as a high-level orchestration component.

In many cases, reward application belongs to the owning System or a dedicated System rather than a stateless Service.

---

# Stateless Services

Prefer stateless Services when possible.

Example:

```csharp
public sealed class GameDateFormattingService
{
    public string Format(
        GameDateSnapshot date)
    {
        ...
    }
}
```

Benefits:

- Easy testing.
- Predictable behavior.
- Simple lifetime.
- Minimal initialization complexity.

---

# Stateful Services

Some Services may require internal state.

Examples:

- Platform connection.
- Cached localization data.
- File handles.
- HTTP client configuration.
- Random number generator seed.
- Audio playback infrastructure.

Stateful Services are acceptable when the state supports the Service operation rather than becoming authoritative gameplay progression.

---

# Service Lifetime

Common lifetimes:

## Application Lifetime

Examples:

- Localization Service.
- Platform Service.
- Save Storage Service.
- Serializer.
- Random Service.

## Game Session Lifetime

Examples may include:

- Session-specific selection Services.
- Services using a deterministic playthrough seed.

## Scene Lifetime

Use sparingly.

A scene-specific reusable Unity operation may justify a scene Service, but many scene-facing responsibilities belong to Controllers.

---

# Service Interfaces

Interfaces are useful when:

- Multiple implementations are possible.
- Platform implementations differ.
- Tests require substitutes.
- External APIs are abstracted.
- The implementation may change.
- Dependency boundaries benefit from a contract.

Example:

```csharp
public interface ISaveSerializer
{
    string Serialize(GameSaveData data);
    GameSaveData Deserialize(string value);
}
```

Implementation:

```csharp
public sealed class JsonSaveSerializer :
    ISaveSerializer
{
}
```

---

# When an Interface Is Not Necessary

Do not create an interface automatically for every Service.

An interface may be unnecessary when:

- There will realistically be one simple implementation.
- The class is internal to one feature.
- It has no external dependency.
- Testing does not require substitution.
- The abstraction adds no meaningful boundary.

Avoid:

```text
IThingService
ThingService
```

for every tiny helper solely as ceremony.

---

# Dependency Injection

Services should generally be supplied explicitly to the classes that require them.

Constructor injection is preferred for plain C# classes.

Example:

```csharp
public sealed class ShopSystem
{
    private readonly ISellPriceService _sellPriceService;

    public ShopSystem(
        ISellPriceService sellPriceService)
    {
        _sellPriceService = sellPriceService;
    }
}
```

This makes dependencies visible.

---

# Unity Component Injection

MonoBehaviours cannot always use normal constructor injection.

Dependencies may be provided through:

- Initialization methods.
- Serialized references where appropriate.
- Composition root wiring.
- Dependency injection framework if intentionally adopted.

Example:

```csharp
public void Initialize(
    IInventorySystem inventorySystem)
{
    _inventorySystem = inventorySystem;
}
```

Avoid repeated global lookups.

---

# Service Locator

Avoid a global Service Locator as the default architecture.

Bad:

```csharp
ServiceLocator.Get<IInventorySystem>();
ServiceLocator.Get<ISellPriceService>();
ServiceLocator.Get<ISaveSerializer>();
```

throughout the project.

Problems:

- Dependencies become hidden.
- Testing becomes harder.
- Initialization order becomes less clear.
- Any class can access anything.

Prefer explicit dependencies.

---

# Static Services

Pure utility functions may be static when:

- They have no dependencies.
- They have no mutable state.
- They represent universal deterministic operations.
- Mocking is unnecessary.

Example:

```csharp
public static class MathUtility
{
    public static int ClampQuantity(
        int value,
        int maximum)
    {
        return Mathf.Clamp(
            value,
            0,
            maximum);
    }
}
```

However, do not make a Service static solely for convenient global access.

---

# Utility vs Service

Use a Utility when the operation is:

- Very small.
- Pure.
- General.
- Dependency-free.
- Not domain-owned.

Use a Service when the operation:

- Represents a meaningful domain operation.
- Has dependencies.
- Has multiple implementations.
- Benefits from an interface.
- Requires configuration.
- Needs testing substitution.

---

# Domain Services

Services should usually belong to the domain they support.

Examples:

```text
Economy/
    Services/
        SellPriceService.cs

Relationships/
    Services/
        GiftEvaluationService.cs

Dialogue/
    Services/
        DialogueSelectionService.cs

Save/
    Serialization/
        JsonSaveSerializer.cs
```

Avoid one enormous:

```text
Services/
```

folder containing every Service in the game without domain organization.

---

# Cross-Domain Services

Truly shared Services may live in foundational dependencies.

Examples:

- Random Service.
- Localization Service.
- Serialization abstraction.
- General formatting infrastructure.

A Service should only move into shared infrastructure when multiple unrelated domains genuinely depend on it.

---

# Service Dependencies

A Service may depend on:

- Configuration.
- Registries.
- Other narrowly scoped Services.
- Platform APIs through abstractions.
- Read-only data providers.

Avoid Service dependency chains such as:

```text
Service A
    ↓
Service B
    ↓
Service C
    ↓
Service D
    ↓
System E
```

when the operation becomes difficult to understand.

---

# Services Depending on Systems

A Service should generally avoid owning broad direct dependencies on mutable gameplay Systems.

Prefer passing the required context into the Service.

Instead of:

```csharp
public sealed class GiftEvaluationService
{
    private readonly InventorySystem _inventory;
    private readonly CalendarSystem _calendar;
    private readonly WeatherSystem _weather;
    private readonly NpcFriendshipSystem _friendship;
}
```

prefer:

```csharp
GiftResult Evaluate(
    GiftRequest request,
    GiftContext context);
```

when practical.

This keeps the Service reusable and easier to test.

---

# Read-Only System Dependencies

Some Services may reasonably query read-only System interfaces when building context externally would create excessive duplication.

If used:

- Keep dependencies narrow.
- Depend on interfaces.
- Do not mutate the Systems.
- Avoid creating circular dependencies.

---

# Service Results

Complex Service operations should return explicit Result or Calculation Models.

Example:

```csharp
public GiftEvaluationResult Evaluate(...);
```

instead of:

```csharp
public bool Evaluate(...);
```

when callers need:

- Preference.
- Friendship gain.
- Bonus.
- Failure reason.
- Context information.

Related Notes:

- Models

---

# Failure Handling

Expected failures should normally be returned explicitly.

Example:

```csharp
public SaveDeserializeResult Deserialize(...);
```

may contain:

- Success.
- Error type.
- Parsed Save Data.
- Error message for logging.

Exceptions should generally represent exceptional conditions rather than ordinary gameplay outcomes.

---

# Null Handling

Services should define clear behavior for missing data.

Avoid ambiguous null results when possible.

Example:

Instead of:

```csharp
ItemDefinition Find(string id);
```

where `null` may have several meanings, use:

```csharp
bool TryGet(
    string id,
    out ItemDefinition definition);
```

or a Result Model.

---

# Async Services

Some infrastructure Services may require asynchronous operations.

Examples:

- Cloud saves.
- Platform storage.
- Online platform APIs.
- Addressable content loading.

Possible API:

```csharp
Task<SaveWriteResult> WriteAsync(...);
```

Gameplay calculations generally should remain synchronous unless there is a real asynchronous dependency.

---

# Cancellation

Long-running asynchronous Services should support cancellation when appropriate.

Examples:

- Cloud synchronization.
- Remote data loading.
- Addressable loading.

Do not add cancellation complexity to simple synchronous gameplay Services.

---

# Threading

Services that perform background work must respect Unity's main-thread restrictions.

Background operations may handle:

- Serialization.
- File processing.
- Data calculations.

Unity API calls involving many UnityEngine objects must remain on the main thread unless explicitly documented as thread-safe.

Keep threading concerns inside infrastructure rather than spreading them through gameplay Systems.

---

# Caching

Services may cache expensive reusable results when appropriate.

Examples:

- Localization lookup.
- Parsed content lookup.
- Compiled condition structures.

Caches should:

- Be invalidated intentionally.
- Not become alternate authoritative gameplay state.
- Be reconstructable.
- Generally not require Save Data.

---

# Determinism

Services involved in:

- Weather generation.
- Fishing selection.
- Gathering drops.
- Random Dialogue selection.

should consider whether results need deterministic behavior.

If deterministic:

- Use explicit seeds.
- Avoid uncontrolled global randomness.
- Persist required seed/state when necessary.

The owning System determines what random outcome becomes authoritative state.

---

# Testing Services

Services are strong candidates for isolated unit tests.

Examples:

- Sell price calculations.
- Friendship gain calculations.
- Gift evaluation.
- Requirement evaluation.
- Condition evaluation.
- Dialogue selection.
- Save serialization.
- Save validation.
- Formatting.
- Random weighted selection.

A Service with many Unity scene dependencies may indicate that the responsibility belongs elsewhere.

---

# Pure Function Testing

Stateless Services should ideally support tests such as:

```text
Given Input A
    ↓
Service
    ↓
Expected Output B
```

without requiring:

- Scene loading.
- GameObjects.
- UI.
- Save files.
- Player input.

---

# Mock Services

Interfaces allow tests to substitute deterministic implementations.

Example:

```csharp
public sealed class FixedRandomService :
    IRandomService
{
    private readonly float _value;

    public FixedRandomService(float value)
    {
        _value = value;
    }

    public float Value()
    {
        return _value;
    }
}
```

This allows predictable tests for random selection logic.

---

# Service Naming

Use names that describe the operation or capability.

Recommended:

- `SellPriceService`
- `GiftEvaluationService`
- `DialogueSelectionService`
- `SaveValidationService`
- `GameDateFormattingService`

Avoid vague names:

- `HelperService`
- `GeneralService`
- `GameService`
- `UtilityService`
- `DataService`

unless the name accurately describes a specific established responsibility.

---

# Interface Naming

Interfaces should use the standard `I` prefix.

Examples:

- `ISellPriceService`
- `IRandomService`
- `ISaveSerializer`
- `ISaveStorageService`
- `ILocalizationService`

The interface name should describe the capability rather than the implementation.

---

# Implementation Naming

Implementation names should identify meaningful implementation differences.

Examples:

```text
ISaveSerializer
    ↓
JsonSaveSerializer

ISaveStorageService
    ↓
LocalSaveStorageService
    ↓
ConsoleSaveStorageService
```

If only one implementation exists and no interface is needed:

```text
SellPriceService
```

is sufficient.

---

# Service Folder Structure

Recommended per-domain structure:

```text
Economy/
|
|-- Systems/
|   |-- EconomySystem.cs
|
|-- Services/
|   |-- SellPriceService.cs
|
|-- Models/
|   |-- SellPriceCalculation.cs
```

Another example:

```text
Relationships/
|
|-- Systems/
|   |-- NpcFriendshipSystem.cs
|
|-- Services/
|   |-- GiftEvaluationService.cs
|   |-- FriendshipGainService.cs
|
|-- Models/
```

Infrastructure example:

```text
Save/
|
|-- Systems/
|   |-- SaveSystem.cs
|
|-- Serialization/
|   |-- ISaveSerializer.cs
|   |-- JsonSaveSerializer.cs
|
|-- Storage/
|   |-- ISaveStorageService.cs
|   |-- LocalSaveStorageService.cs
|
|-- Validation/
|   |-- SaveValidationService.cs
```

---

# Suggested Services by Dependency

The following are candidates, not requirements.

Only create a Service when the implementation actually benefits from one.

---

# Core

Possible Services:

- `RandomService`
- `IdValidationService`

---

# Time / Calendar

Possible Services:

- `GameTimeFormattingService`
- `GameDateFormattingService`
- `DaylightCalculationService`

If Daylight is central authoritative gameplay state rather than a pure calculation, ownership may remain with the appropriate System.

---

# Weather

Possible Services:

- `WeatherSelectionService`
- `WeatherForecastGenerationService`

Weather System remains authoritative for current and established forecast state.

---

# Player

Possible Services:

- `PlayerAppearanceValidationService`

Most Player behavior belongs to Systems and Controllers rather than Services.

---

# Inventory

Possible Services:

- `InventorySortingService`
- `InventoryCapacityCalculationService`

Inventory System remains authoritative for Inventory contents.

---

# Economy

Possible Services:

- `SellPriceService`
- `PurchasePriceService`
- `DayEndSaleCalculationService`

Economy System remains authoritative for Bells and completed transactions.

---

# Relationships

Possible Services:

- `GiftEvaluationService`
- `FriendshipGainService`
- `RelationshipStatusCalculationService`

Relationship Systems remain authoritative for progression.

---

# Dialogue

Possible Services:

- `DialogueConditionService`
- `DialogueSelectionService`
- `DialoguePriorityService`

Dialogue System remains responsible for the active conversation and gameplay coordination.

---

# Quests

Possible Services:

- `QuestRequirementService`
- `QuestObjectiveEvaluationService`
- `QuestRewardResolutionService`

Quest System remains authoritative for Quest state.

---

# Farming

Possible Services:

- `CropGrowthCalculationService`
- `HarvestYieldService`
- `CropSeasonValidationService`

Farming System remains authoritative for Farm state.

---

# Gathering

Possible Services:

- `GatheringDropService`
- `ResourceRespawnCalculationService`

Resource Systems remain authoritative for world node state.

---

# Fishing

Possible Services:

- `FishAvailabilityService`
- `FishingCatchSelectionService`
- `FishingQualityService`

Fishing System remains authoritative for the active fishing operation.

---

# Crafting

Possible Services:

- `CraftingRequirementService`
- `CraftingOutputService`

Crafting System coordinates the actual operation.

---

# Cooking

Possible Services:

- `CookingRequirementService`
- `MealQualityService`

---

# Tonics

Possible Services:

- `TonicEffectService`
- `TonicRecipeValidationService`

---

# Inventions

Possible Services:

- `InventionRequirementService`
- `InventionUnlockEvaluationService`

Invention System remains authoritative for progression.

---

# Restoration

Possible Services:

- `RestorationRequirementService`
- `RestorationProgressCalculationService`

Restoration System remains authoritative for Restoration state.

---

# Shops

Possible Services:

- `ShopAvailabilityService`
- `ShopStockSelectionService`
- `ShopPriceService`

Shop/Economy Systems coordinate transactions.

---

# NPC Routines

Possible Services:

- `NpcRoutineSelectionService`
- `NpcScheduleEvaluationService`

NPC Routine System remains authoritative for selected runtime routine state.

---

# Mail

Possible Services:

- `MailEligibilityService`

Mail System remains authoritative for received/read/claimed state.

---

# Festivals

Possible Services:

- `FestivalEligibilityService`
- `FestivalScoreCalculationService`

Festival System remains authoritative for active Festival state and persistent results.

---

# Ledger

Possible Services:

- `CompletionPercentageService`
- `LedgerCategoryCalculationService`

Ledger System remains authoritative for persistent discovery/progress state.

---

# Save

Recommended infrastructure Services:

- `ISaveSerializer`
- `ISaveStorageService`
- `SaveValidationService`
- `SaveBackupService`
- Save migration components

Save System coordinates the full Save and Load operation.

---

# Audio

Possible Services:

- `AudioClipSelectionService`
- `AudioPlaybackService`

Audio presentation ownership should remain clearly separated from gameplay state.

---

# Input

Possible Services:

- `InputPromptService`
- `InputDeviceClassificationService`

These support dynamic platform-specific HUD prompts.

---

# Localization

Possible Service:

- `LocalizationService`

This may later wrap Unity Localization or another localization implementation.

---

# Service Composition Example

Crafting flow:

```text
Crafting Controller
    ↓
Crafting System
    ↓
Crafting Requirement Service
    ↓
Requirement Result
```

If valid:

```text
Crafting System
    ↓
Inventory System.TryConsume(...)
    ↓
Inventory System.AddItem(...)
    ↓
Crafting System updates progression
    ↓
Event Channels
```

The Requirement Service does not remove Items itself.

---

# Gift Service Example

Flow:

```text
Gift Request
    ↓
Gift System / Relationship coordination
    ↓
Gift Evaluation Service
```

Inputs:

- NPC Definition.
- Item Definition.
- Current relationship context.
- Relevant bonuses.

Output:

```text
GiftEvaluationResult
```

Possible fields:

- Gift Preference.
- Base Friendship Gain.
- Birthday multiplier.
- Final Friendship Gain.
- Dialogue category.

Then:

```text
NPC Friendship System
    ↓
Applies authoritative Friendship change
```

---

# Dialogue Service Example

Flow:

```text
Dialogue System
    ↓
Build DialogueContext
    ↓
Dialogue Condition Service
    ↓
Eligible Dialogue
    ↓
Dialogue Selection Service
    ↓
Selected DialogueDefinition
```

Dialogue System then begins the conversation.

The selection Service does not directly open the UI.

---

# Save Service Example

Flow:

```text
Save System
    ↓
Collect Save Data
    ↓
Save Validation Service
    ↓
Save Serializer
    ↓
Save Storage Service
```

This keeps:

- Gameplay state collection.
- Validation.
- Serialization.
- Physical storage.

as separate responsibilities.

---

# Platform Abstraction Example

```csharp
public interface IPlatformStorageService
{
    Task<StorageWriteResult> WriteAsync(
        string saveId,
        byte[] data);

    Task<StorageReadResult> ReadAsync(
        string saveId);
}
```

Possible implementations:

```text
DesktopPlatformStorageService
NintendoPlatformStorageService
XboxPlatformStorageService
PlayStationPlatformStorageService
```

The rest of the game depends on the interface.

---

# Service Configuration

Services may receive ScriptableObject configuration.

Example:

```csharp
public sealed class FriendshipGainService
{
    private readonly RelationshipConfiguration _configuration;

    public FriendshipGainService(
        RelationshipConfiguration configuration)
    {
        _configuration = configuration;
    }
}
```

The Service reads configuration.

It should not mutate the configuration asset.

---

# Services and Save Data

Services generally should not have their own Save Data unless they actually own meaningful persistent state.

If a component requires persistent gameplay state, reconsider whether it is really a System.

Infrastructure Services may have non-gameplay persistence such as:

- Cached platform tokens.
- Application preferences.
- Localization settings.

Those are separate from gameplay Save Data.

---

# Services and Event Channels

A Service should generally return its result directly to the caller.

Preferred:

```csharp
GiftEvaluationResult result =
    giftEvaluationService.Evaluate(...);
```

Avoid:

```text
Gift Evaluation Service
    ↓
Raises GiftEvaluated Event
```

when the caller requires the result immediately.

Events are appropriate only when independent listeners genuinely need notification.

---

# Services and Initialization

Services should be initialized before Systems that depend on them.

General order:

```text
Core
    ↓
Static Data
    ↓
Registries
    ↓
Services
    ↓
Gameplay Systems
    ↓
Save Restoration
    ↓
Controllers / Presentation
```

Stateful platform Services may require earlier initialization.

Related Notes:

- Initialization Order

---

# Services and Dependencies

Services should respect dependency direction.

A lower-level shared Service should not depend on a high-level gameplay feature.

Example:

Bad:

```text
Core Random Service
    ↓
Quest System
```

Preferred:

```text
Quest System
    ↓
Random Service
```

Domain Services may depend on lower-level Data and Core abstractions.

Related Notes:

- Dependencies

---

# Circular Dependencies

Services should not be introduced merely to disguise circular dependencies.

Bad:

```text
Quest System
    ↓
Relationship Service
    ↓
Relationship System
    ↓
Quest Service
    ↓
Quest System
```

Instead:

- Reconsider ownership.
- Pass explicit context.
- Extract a lower-level calculation.
- Use a completed-change Event where appropriate.
- Restructure dependency direction.

---

# God Services

Avoid large Services such as:

```text
GameplayService
```

containing:

- Inventory calculations.
- Quest validation.
- Friendship.
- Farming.
- Fishing.
- Save helpers.
- Audio.
- UI formatting.

Services should remain narrowly focused.

---

# Helper Service Anti-Pattern

Avoid creating:

```text
HelperService
```

or:

```text
CommonService
```

as a dumping ground for unrelated methods.

If methods are unrelated, they belong in different domains or may simply remain private helpers.

---

# One-Method Services

A one-method Service is not automatically bad.

It is useful when that method represents:

- A meaningful abstraction.
- A platform boundary.
- A complex calculation.
- A replaceable implementation.
- A highly reusable operation.

It is unnecessary when it only wraps another one-line call without adding architectural value.

---

# Service Over-Abstraction

Avoid:

```text
InventorySystem
    ↓
InventoryService
    ↓
InventoryRepository
    ↓
InventoryProvider
    ↓
InventoryModel
```

when the Inventory System can clearly own and operate on its Model directly.

The architecture should remain understandable.

---

# Repository Pattern

Repositories may be useful for external persistence or complex data sources.

For static game content, Data Registries are generally sufficient.

For runtime gameplay state, Systems own state.

Do not introduce repositories solely because they are common in web application architecture.

---

# Manager vs Service

Avoid the generic `Manager` suffix when a more precise architectural role exists.

Instead of:

```text
SaveManager
```

prefer:

```text
SaveSystem
SaveStorageService
SaveBackupService
```

depending on responsibility.

Instead of:

```text
AudioManager
```

determine whether the component is:

- Audio System.
- Audio Controller.
- Audio Playback Service.

Names should communicate ownership.

---

# Service API Design

Service methods should:

- Have clear names.
- Accept only required inputs.
- Return explicit outputs.
- Avoid hidden global state.
- Avoid surprising side effects.
- Document important assumptions.
- Use domain Models when parameters become complex.

Example:

Prefer:

```csharp
SellPriceCalculation Calculate(
    SellPriceRequest request);
```

over:

```csharp
int DoThing(
    string id,
    int a,
    int b,
    bool c,
    float d);
```

---

# Side Effects

Calculation and evaluation Services should ideally be side-effect free.

Infrastructure Services may intentionally have side effects.

Examples:

```text
SaveStorageService.Write()
AudioPlaybackService.Play()
```

The side effect should be obvious from the Service's purpose and method name.

---

# Idempotency

Where appropriate, Services should have predictable repeated behavior.

Example:

```text
Formatting same date twice
```

should produce the same output.

```text
Validating same Save Data twice
```

should produce the same validation result.

Operations such as file writing are inherently side-effecting and should document behavior accordingly.

---

# Logging

Services may use a logging abstraction for diagnostics.

Avoid excessive logs from frequently called pure calculations.

Useful logs include:

- Failed serialization.
- Missing platform capability.
- Invalid definition data.
- Storage errors.
- Migration errors.

Gameplay-facing messages should not be produced directly from low-level Services unless that is their explicit presentation responsibility.

---

# Error Messages

Infrastructure Service errors should provide enough context for debugging.

Example:

```text
Failed to deserialize save slot 2:
Unexpected token at ...
```

Avoid exposing raw technical errors directly to players.

The Controller or UI layer may translate failures into player-facing messages.

---

# Service Documentation

Each non-trivial Service should document:

- Purpose.
- Inputs.
- Outputs.
- Dependencies.
- Side effects.
- Ownership boundaries.
- Failure behavior.
- Whether deterministic.
- Whether thread-safe if relevant.

This can be done in code comments and the relevant System/Code Setup notes.

---

# Service Design Checklist

Before creating a Service, determine:

1. What reusable operation does it perform?
2. Does it own authoritative gameplay state?
3. If yes, should it actually be a System?
4. Is this operation reused?
5. Is the operation complex enough to justify extraction?
6. Could this remain a private method?
7. Does it require configuration?
8. Does it require a platform abstraction?
9. Does it need an interface?
10. Will multiple implementations exist?
11. Will tests need a substitute implementation?
12. Can it be stateless?
13. What is its lifetime?
14. What are its inputs?
15. What is its output?
16. Does it need a Request or Result Model?
17. Does it have side effects?
18. Are those side effects obvious?
19. Does it depend on mutable gameplay Systems?
20. Could required context be passed explicitly instead?
21. Does it introduce a circular dependency?
22. Is it hiding another component's ownership?
23. Should it return a result directly instead of raising an Event?
24. Does it belong to a specific gameplay domain?
25. Is it genuinely shared infrastructure?
26. Can it be tested independently?
27. Does it depend on Unity scene objects?
28. Would a Controller be more appropriate?
29. Would a static Utility be simpler?
30. Is the Service making the architecture clearer rather than merely adding another layer?

---

# Service Rules

- Use Services for reusable operations that do not primarily own long-lived gameplay state.
- Keep authoritative gameplay state inside Systems.
- Keep Services narrowly focused.
- Prefer stateless Services when practical.
- Give Services explicit inputs and outputs.
- Use Request and Result Models for complex operations.
- Return required results directly rather than communicating through Events.
- Use interfaces when implementations may vary or substitution provides real value.
- Do not create interfaces solely as ceremony.
- Prefer explicit dependency injection.
- Avoid global Service Locators.
- Avoid mutable static Services.
- Avoid God Services and generic Helper Services.
- Keep Services in the domain that owns the operation.
- Move a Service into shared infrastructure only when multiple unrelated domains genuinely require it.
- Prefer passing context to calculation Services instead of giving them broad access to mutable Systems.
- Keep platform-specific implementations behind interfaces.
- Keep serialization separate from physical storage.
- Keep static content in ScriptableObjects and Registries.
- Keep runtime state in Models and Systems.
- Keep persistent gameplay state in Save Data.
- Avoid using Services to disguise circular dependencies.
- Avoid unnecessary abstraction layers.
- Use deterministic Random Services where reproducibility matters.
- Keep expected failures explicit through Results where appropriate.
- Keep scene-specific coordination in Controllers.
- Keep Service side effects intentional and obvious.
- Test reusable calculation and evaluation Services independently.
- Only create a Service when it makes responsibility, reuse, testing, or dependency boundaries clearer.

---

# Related Code Setup Notes

- Controllers
- Data IDs
- Dependencies
- Enums
- Event Channels
- Game Architecture
- Initialization Order
- Models
- Save Data
- Scriptable Objects

---

# Related System Notes

- System Interaction Rules
- Individual System documentation
