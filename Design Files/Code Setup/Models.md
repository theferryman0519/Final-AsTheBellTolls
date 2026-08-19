---
Title: Code Setup / Models
Game: As The Bell Tolls
Owner: Carey Clement Jr
Created: August, 2026
Updated: August, 2026
Version: 0.0.1
---

## Information

- Models represent structured runtime data used by Systems, Controllers, Services, UI, and other gameplay components.
- Models organize related values into meaningful objects rather than passing large groups of unrelated primitive values between classes.
- Models may represent runtime state, requests, results, calculated information, summaries, or transferable gameplay data.
- A Model does not automatically own the data it contains.
- The authoritative owner of a Model is determined by the System or architectural component responsible for that gameplay state.
- External classes should not directly mutate Models owned by another System unless the owning System explicitly allows it.
- Models should contain data and lightweight data-related behavior rather than large gameplay systems or Unity presentation logic.
- Models should remain independent of scene-specific GameObjects whenever possible.
- Models should generally be plain C# classes, structs, or records rather than MonoBehaviours or ScriptableObjects.
- Persistent Models may be converted into Save Data, but runtime Models and Save Data should remain conceptually separate.

---

# Purpose

Models are used to provide clearly structured representations of gameplay information.

Models may represent:

- Runtime state.
- Requests.
- Results.
- Read-only summaries.
- Temporary calculated information.
- Collections of related values.
- Data passed between Systems.
- Data passed from Systems to Controllers.
- Data passed from Systems to UI.
- Parsed or resolved static data.
- Runtime representations of persistent Save Data.

Models help prevent APIs such as:

```csharp
TryAddItem(
    string itemId,
    int quantity,
    int quality,
    bool discovered,
    bool notify,
    string source);
```

when those values logically belong together.

Instead:

```csharp
TryAddItem(InventoryAddRequest request);
```

---

# Model Responsibilities

Models may:

- Store related runtime values.
- Validate simple internal invariants.
- Provide calculated properties.
- Represent the current state of a gameplay concept.
- Represent information required to perform an operation.
- Represent the result of an operation.
- Provide immutable information to presentation code.
- Simplify communication between architectural layers.

Models should not:

- Become global managers.
- Own unrelated Systems.
- Directly control Unity GameObjects.
- Perform scene loading.
- Play Audio.
- Open UI screens.
- Directly Save files.
- Raise unrelated Event Channels.
- Perform large gameplay workflows.
- Duplicate authoritative state owned elsewhere.

---

# Model Categories

Models should generally be classified according to their purpose.

Primary categories:

- State Models
- Request Models
- Result Models
- Snapshot Models
- Presentation Models
- Calculation Models
- Context Models
- Entry Models
- Collection Models

Not every feature requires every category.

---

# State Models

State Models represent runtime gameplay state.

Examples:

- `InventoryEntryModel`
- `FriendshipModel`
- `QuestStateModel`
- `CropStateModel`
- `NpcRoutineStateModel`
- `RestorationStateModel`
- `MailStateModel`

Example:

```csharp
public sealed class FriendshipModel
{
    public string NpcId { get; private set; }
    public int FriendshipPoints { get; private set; }
    public int HeartLevel { get; private set; }

    public FriendshipModel(
        string npcId,
        int friendshipPoints,
        int heartLevel)
    {
        NpcId = npcId;
        FriendshipPoints = friendshipPoints;
        HeartLevel = heartLevel;
    }

    public void SetProgress(
        int friendshipPoints,
        int heartLevel)
    {
        FriendshipPoints = friendshipPoints;
        HeartLevel = heartLevel;
    }
}
```

The Model may contain the values, but the owning System remains responsible for deciding when those values change.

Example:

`NpcFriendshipSystem`

owns:

`FriendshipModel`

External code should request:

```csharp
friendshipSystem.AddFriendship(npcId, amount);
```

rather than:

```csharp
friendshipModel.FriendshipPoints += amount;
```

---

# Request Models

Request Models group information required to perform an operation.

Request Models are useful when:

- A method requires several related values.
- Additional request information may be added later.
- The request must pass through several architectural layers.
- The operation needs contextual information.

Examples:

- `InventoryAddRequest`
- `ShopPurchaseRequest`
- `CraftingRequest`
- `GiftRequest`
- `InteractionRequest`
- `ToolUseRequest`
- `DialogueRequest`
- `SceneTransitionRequest`

Example:

```csharp
public readonly struct ShopPurchaseRequest
{
    public string ShopId { get; }
    public string ItemId { get; }
    public int Quantity { get; }

    public ShopPurchaseRequest(
        string shopId,
        string itemId,
        int quantity)
    {
        ShopId = shopId;
        ItemId = itemId;
        Quantity = quantity;
    }
}
```

Request Models should describe what is being requested.

They should not determine whether the request succeeds.

That responsibility belongs to the receiving System or Service.

---

# Result Models

Result Models describe the outcome of an operation.

Result Models are recommended when an operation:

- May succeed or fail.
- Has multiple possible failure reasons.
- Produces useful output.
- Needs to report several pieces of information.
- Should avoid throwing exceptions for normal gameplay failures.

Examples:

- `CraftingResult`
- `PurchaseResult`
- `GiftResult`
- `InteractionResult`
- `ToolUseResult`
- `QuestStartResult`
- `InventoryTransferResult`

Example:

```csharp
public readonly struct PurchaseResult
{
    public bool Success { get; }
    public PurchaseFailureReason FailureReason { get; }
    public string ItemId { get; }
    public int Quantity { get; }

    public PurchaseResult(
        bool success,
        PurchaseFailureReason failureReason,
        string itemId,
        int quantity)
    {
        Success = success;
        FailureReason = failureReason;
        ItemId = itemId;
        Quantity = quantity;
    }
}
```

Possible use:

```csharp
PurchaseResult result = shopSystem.TryPurchase(request);

if (!result.Success)
{
    HandlePurchaseFailure(result.FailureReason);
}
```

Normal gameplay failures should be represented by Result Models or failure enums when practical.

Examples include:

- Insufficient Bellnotes.
- Inventory full.
- Missing ingredients.
- Incorrect tool.
- Insufficient Stamina.
- Quest requirements not met.
- Interaction currently unavailable.

These are expected gameplay outcomes and should generally not require exceptions.

---

# Snapshot Models

Snapshot Models represent a read-only view of current gameplay state at a particular moment.

They are useful when:

- UI needs several related values.
- External Systems need information without receiving mutable internal state.
- A System should expose data without exposing its internal Models.
- A temporary state representation is useful for calculations or presentation.

Examples:

- `InventorySnapshot`
- `PlayerStatusSnapshot`
- `CalendarSnapshot`
- `NpcRelationshipSnapshot`
- `RestorationSnapshot`

Example:

```csharp
public readonly struct PlayerStatusSnapshot
{
    public int CurrentStamina { get; }
    public int MaximumStamina { get; }
    public int Bells { get; }

    public PlayerStatusSnapshot(
        int currentStamina,
        int maximumStamina,
        int bells)
    {
        CurrentStamina = currentStamina;
        MaximumStamina = maximumStamina;
        Bells = bells;
    }
}
```

Snapshots should normally be immutable.

Snapshots should not become long-lived duplicate copies of authoritative System state.

---

# Presentation Models

Presentation Models contain information specifically prepared for UI or presentation code.

Presentation Models may combine data from multiple gameplay sources without becoming authoritative owners of that data.

Examples:

- `InventoryItemDisplayModel`
- `RelationshipDisplayModel`
- `QuestDisplayModel`
- `CalendarDayDisplayModel`
- `NotificationDisplayModel`
- `ShopItemDisplayModel`

Example:

```csharp
public readonly struct RelationshipDisplayModel
{
    public string NpcId { get; }
    public string DisplayName { get; }
    public int HeartLevel { get; }
    public float HeartProgress { get; }
    public RelationshipStatus Status { get; }

    public RelationshipDisplayModel(
        string npcId,
        string displayName,
        int heartLevel,
        float heartProgress,
        RelationshipStatus status)
    {
        NpcId = npcId;
        DisplayName = displayName;
        HeartLevel = heartLevel;
        HeartProgress = heartProgress;
        Status = status;
    }
}
```

Presentation Models should not contain gameplay mutation methods.

They exist to simplify presentation.

---

# Calculation Models

Calculation Models represent intermediate or calculated information.

Examples:

- `CropGrowthCalculation`
- `FishingCatchCalculation`
- `GiftReactionCalculation`
- `TonicEffectCalculation`
- `SellPriceCalculation`
- `RestorationRequirementCalculation`
- `RelationshipGainCalculation`

Example:

```csharp
public readonly struct SellPriceCalculation
{
    public int BaseValue { get; }
    public float QualityMultiplier { get; }
    public float BonusMultiplier { get; }
    public int FinalValue { get; }

    public SellPriceCalculation(
        int baseValue,
        float qualityMultiplier,
        float bonusMultiplier,
        int finalValue)
    {
        BaseValue = baseValue;
        QualityMultiplier = qualityMultiplier;
        BonusMultiplier = bonusMultiplier;
        FinalValue = finalValue;
    }
}
```

Calculation Models are useful when the result of a calculation needs to be inspected, displayed, tested, or reused.

---

# Context Models

Context Models provide environmental or gameplay context required to resolve another operation.

Examples:

- `DialogueContext`
- `InteractionContext`
- `GiftContext`
- `NpcScheduleContext`
- `SpawnContext`
- `WeatherSelectionContext`

Example:

```csharp
public readonly struct DialogueContext
{
    public string NpcId { get; }
    public int FriendshipLevel { get; }
    public DaylightState Daylight { get; }
    public WeatherType Weather { get; }
    public bool IsMarriedToPlayer { get; }

    public DialogueContext(
        string npcId,
        int friendshipLevel,
        DaylightState daylight,
        WeatherType weather,
        bool isMarriedToPlayer)
    {
        NpcId = npcId;
        FriendshipLevel = friendshipLevel;
        Daylight = daylight;
        Weather = weather;
        IsMarriedToPlayer = isMarriedToPlayer;
    }
}
```

A Context Model should contain only information relevant to the operation receiving it.

Avoid creating one giant global context containing every System's current state.

---

# Entry Models

Entry Models represent one element inside a larger collection.

Examples:

- `InventoryEntryModel`
- `StorageEntryModel`
- `ShopStockEntryModel`
- `QuestObjectiveEntryModel`
- `MailInboxEntryModel`
- `LedgerEntryModel`

Example:

```csharp
public sealed class InventoryEntryModel
{
    public string ItemId { get; private set; }
    public int Quantity { get; private set; }
    public ItemQuality Quality { get; private set; }

    public InventoryEntryModel(
        string itemId,
        int quantity,
        ItemQuality quality)
    {
        ItemId = itemId;
        Quantity = quantity;
        Quality = quality;
    }

    public void SetQuantity(int quantity)
    {
        Quantity = quantity;
    }
}
```

The collection-owning System should control adding, removing, merging, splitting, or reordering entries.

---

# Collection Models

Collection Models represent a logical group of related Models.

Examples:

- `InventoryModel`
- `StorageModel`
- `QuestLogModel`
- `RelationshipCollectionModel`
- `MailInboxModel`

Collection Models may contain:

- Collection-level metadata.
- Capacity.
- Ordering.
- Lookup helpers.
- Controlled internal entry access.

Example:

```csharp
public sealed class InventoryModel
{
    private readonly List<InventoryEntryModel> _entries = new();

    public int Capacity { get; }

    public IReadOnlyList<InventoryEntryModel> Entries => _entries;

    public InventoryModel(int capacity)
    {
        Capacity = capacity;
    }

    internal void AddEntry(InventoryEntryModel entry)
    {
        _entries.Add(entry);
    }

    internal void RemoveEntry(InventoryEntryModel entry)
    {
        _entries.Remove(entry);
    }
}
```

Collection mutation should remain controlled by the owning System.

---

# Models vs ScriptableObjects

Models and ScriptableObjects serve different purposes.

## ScriptableObject

Represents static designer-authored content.

Example:

`ItemDefinition`

May contain:

- Item ID
- Display Name
- Description
- Category
- Base Sell Value
- Maximum Stack Size
- Icon
- Prefab reference

## Model

Represents runtime information about that content.

Example:

`InventoryEntryModel`

May contain:

- Item ID
- Quantity
- Quality

The ScriptableObject answers:

"What is this Item?"

The Model answers:

"What is currently true about this Item in this gameplay context?"

---

# Models vs Save Data

Models and Save Data should remain conceptually separate.

## Runtime Model

Designed for active gameplay.

May contain:

- Calculated properties.
- Read-only collection access.
- Runtime-only state.
- Convenient lookup structures.
- Methods that maintain internal consistency.

## Save Data

Designed for serialization and persistence.

Should contain:

- Serializable values.
- Stable IDs.
- Primitive values.
- Serializable collections.
- Save version-compatible structures.

Example runtime Model:

```csharp
public sealed class InventoryEntryModel
{
    public string ItemId { get; private set; }
    public int Quantity { get; private set; }
    public ItemQuality Quality { get; private set; }
}
```

Example Save Data:

```csharp
[Serializable]
public sealed class InventoryEntrySaveData
{
    public string itemId;
    public int quantity;
    public int quality;
}
```

Conversion:

```text
Runtime Model
    ↓
Export
    ↓
Save Data
    ↓
Serialization
```

Loading:

```text
Serialized Save
    ↓
Save Data
    ↓
Validation / Migration
    ↓
Runtime Model
```

Save Data should not become the live gameplay Model used throughout the application.

---

# Models vs Controllers

Controllers coordinate Unity behavior.

Models contain structured information.

A Model should not:

- Process Input.
- Find GameObjects.
- Control animations.
- Subscribe to Unity Input Actions.
- Open menus.
- Move characters.
- Manage scene transitions.

Example:

`InteractionResult`

is a Model.

`PlayerInteractionController`

uses the result to determine presentation behavior.

---

# Models vs Systems

Systems own gameplay rules and authoritative state.

Models represent the data used by those Systems.

Example:

`InventorySystem`

owns and operates on:

`InventoryModel`

The Model should not become an `InventorySystem` in disguise.

The System remains responsible for:

- Validation.
- Gameplay rules.
- Cross-System communication.
- Event publication.
- Persistent state export.
- Gameplay operations.

---

# Models vs Services

Services perform reusable operations.

Models contain the data passed into or returned from those operations.

Example:

```text
SellPriceRequest
        ↓
SellPriceService
        ↓
SellPriceCalculation
```

The Request and Calculation are Models.

The Service performs the operation.

---

# Ownership

Every mutable State Model should have a clear owner.

Examples:

| Model | Owner |
|---|---|
| `InventoryModel` | Inventory System |
| `InventoryEntryModel` | Inventory System |
| `FriendshipModel` | NPC Friendship System |
| `ConnectionModel` | NPC Connection System |
| `QuestStateModel` | Quest System |
| `CropStateModel` | Farming System |
| `RestorationStateModel` | Restoration System |
| `MailStateModel` | Mail System |
| `NpcRoutineStateModel` | NPC Routine System |
| `ToolStateModel` | Tool System |

The owning System determines:

- Creation.
- Mutation.
- Validation.
- Removal.
- Persistence.
- Event publication.

---

# Mutability

Models should be immutable by default when mutation is not required.

Good candidates for immutable Models:

- Requests.
- Results.
- Snapshots.
- Context.
- Calculations.
- Presentation data.

Good candidates for controlled mutable Models:

- Internal System state.
- Collection entries.
- Long-lived runtime state.

Example immutable Model:

```csharp
public readonly struct GiftResult
{
    public bool Success { get; }
    public GiftPreference Preference { get; }
    public int FriendshipChange { get; }

    public GiftResult(
        bool success,
        GiftPreference preference,
        int friendshipChange)
    {
        Success = success;
        Preference = preference;
        FriendshipChange = friendshipChange;
    }
}
```

Example controlled mutable Model:

```csharp
public sealed class CropStateModel
{
    public string CropId { get; private set; }
    public int GrowthStage { get; private set; }
    public bool WateredToday { get; private set; }

    internal void SetGrowthStage(int growthStage)
    {
        GrowthStage = growthStage;
    }

    internal void SetWateredToday(bool watered)
    {
        WateredToday = watered;
    }
}
```

`private set` or `internal` mutation methods help prevent unrelated classes from editing System-owned state.

---

# Read-Only Exposure

Systems should avoid exposing mutable collections directly.

Avoid:

```csharp
public List<InventoryEntryModel> Entries { get; }
```

when external classes can modify the collection.

Prefer:

```csharp
public IReadOnlyList<InventoryEntryModel> Entries => _entries;
```

or return a Snapshot.

Avoid:

```csharp
inventorySystem.Entries.Clear();
```

Prefer:

```csharp
inventorySystem.ClearInventory();
```

when such an operation is intentionally supported.

---

# Validation

Models may perform lightweight validation related to their own structural consistency.

Examples:

- Quantity cannot be negative.
- Required ID cannot be empty.
- Maximum value cannot be lower than minimum value.
- Date values must represent a valid game date.

However, gameplay rule validation should normally remain with Systems or Services.

Example:

A `ShopPurchaseRequest` may verify that `Quantity > 0`.

The Shop System determines:

- Whether the Shop is open.
- Whether the Item is in stock.
- Whether the player has enough Bells.
- Whether Inventory has capacity.

---

# IDs Inside Models

Models should use stable Data IDs to reference static content when appropriate.

Examples:

```csharp
public string ItemId { get; }
public string NpcId { get; }
public string QuestId { get; }
public string LocationId { get; }
```

Examples of IDs:

```text
item_herb_peppermint
npc_lockwood_adrian
location_blackmere_town-square
quest_main_example
```

Runtime Models should avoid using Display Names as identifiers.

Avoid:

```csharp
NpcName = "Adrian"
```

as the authoritative reference.

Prefer:

```csharp
NpcId = "npc_lockwood_adrian"
```

Display information may then be resolved through the appropriate Data registry.

---

# Unity Dependencies

Models should generally avoid depending on:

- `MonoBehaviour`
- `GameObject`
- `Transform`
- `Animator`
- Scene-specific Components
- Unity lifecycle callbacks

Models may use Unity value types when useful and architecturally appropriate.

Examples:

- `Vector2`
- `Vector3`
- `Quaternion`
- `Color`

However, persistent or highly reusable Models should prefer custom serializable structures when Unity-specific types would create unnecessary coupling.

---

# Constructors

Constructors should leave Models in a valid initial state.

Prefer:

```csharp
public InventoryEntryModel(
    string itemId,
    int quantity,
    ItemQuality quality)
{
    ItemId = itemId;
    Quantity = quantity;
    Quality = quality;
}
```

over:

```csharp
var entry = new InventoryEntryModel();
entry.ItemId = itemId;
entry.Quantity = quantity;
entry.Quality = quality;
```

when partial initialization would create an invalid Model.

---

# Nullability

Models should minimize ambiguous null values.

Prefer:

- Required constructor parameters.
- Empty collections instead of null collections.
- Explicit optional types or flags where appropriate.
- Failure Results where expected gameplay operations may fail.

Avoid relying on null to communicate several unrelated meanings.

Example:

Avoid:

```csharp
ItemDefinition GetItem();
```

where null might mean:

- Missing Item.
- Locked Item.
- Invalid request.
- Empty slot.

Prefer explicit APIs or Results when the distinction matters.

---

# Enums in Models

Enums should be used for finite, stable state categories.

Examples:

- `ItemQuality`
- `RelationshipStatus`
- `WeatherType`
- `DaylightState`
- `QuestState`
- `RestorationState`
- `GiftPreference`

Example:

```csharp
public readonly struct GiftResult
{
    public GiftPreference Preference { get; }
}
```

Avoid using arbitrary strings for values already represented by an established enum.

Related Notes:

- Enums

---

# Model Events

Models should generally not publish global Event Channels themselves.

Preferred flow:

```text
System
    ↓
Changes owned Model
    ↓
Validates final state
    ↓
Raises Event Channel
```

Example:

```text
Inventory System
    ↓
Changes Inventory Model
    ↓
Raises InventoryChanged
```

This keeps event publication tied to the authoritative gameplay operation rather than raw data mutation.

---

# Model Conversion

Conversion between architectural data types should be explicit.

Common conversions:

```text
ScriptableObject Definition
        ↓
Runtime use

Save Data
        ↓
Runtime Model

Runtime Model
        ↓
Snapshot Model

Runtime Model
        ↓
Save Data

Multiple Runtime Models
        ↓
Presentation Model
```

Conversion logic may live in:

- The owning System.
- A dedicated Mapper.
- A Factory.
- A serialization or persistence Service.

The location should be consistent for each dependency.

---

# Factories

Factories may be useful when constructing a Model requires several dependencies or initialization rules.

Example:

```csharp
public sealed class CropModelFactory
{
    public CropStateModel Create(
        CropDefinition definition,
        GameDate plantedDate)
    {
        return new CropStateModel(
            definition.Id,
            plantedDate,
            growthStage: 0);
    }
}
```

Factories should not be introduced when a normal constructor is already clear and sufficient.

---

# Model Naming

Model names should describe the data they represent.

Recommended suffixes:

| Purpose | Suffix |
|---|---|
| General runtime data | `Model` |
| Operation input | `Request` |
| Operation output | `Result` |
| Read-only current state | `Snapshot` |
| UI-ready information | `DisplayModel` |
| Environmental input | `Context` |
| Calculation output | `Calculation` |
| Collection element | `EntryModel` |

Examples:

- `InventoryModel`
- `InventoryEntryModel`
- `InventoryAddRequest`
- `InventoryTransferResult`
- `InventorySnapshot`
- `InventoryItemDisplayModel`
- `DialogueContext`
- `SellPriceCalculation`

Avoid vague names such as:

- `Data`
- `Info`
- `Object`
- `Thing`
- `ManagerModel`

unless the term has a specific established meaning.

---

# Recommended Model Structure

A dependency may contain a `Models` folder when it has enough Models to justify one.

Example:

```text
Inventory/
|
|-- Systems/
|   |-- InventorySystem.cs
|
|-- Models/
|   |-- InventoryModel.cs
|   |-- InventoryEntryModel.cs
|   |-- InventoryAddRequest.cs
|   |-- InventoryTransferRequest.cs
|   |-- InventoryTransferResult.cs
|   |-- InventorySnapshot.cs
|
|-- Controllers/
|   |-- InventoryUiController.cs
```

For smaller dependencies, Models may remain directly inside the dependency folder if that is clearer.

---

# Suggested Models by Dependency

The following are examples of Models likely to be useful throughout the project.

They are not requirements to create every class immediately.

## Core / Game Flow

Possible Models:

- `GameStateSnapshot`
- `GamePauseRequest`
- `SceneTransitionRequest`
- `SceneTransitionResult`

---

## Time

Possible Models:

- `GameTimeModel`
- `GameTimeSnapshot`
- `TimeAdvanceResult`

Example values:

- Hour
- Minute
- Daylight State

---

## Calendar

Possible Models:

- `GameDateModel`
- `GameDateSnapshot`
- `CalendarDayModel`
- `CalendarEventEntryModel`

Example values:

- Weekday
- Day
- Season
- Year

---

## Weather

Possible Models:

- `WeatherStateModel`
- `WeatherForecastEntryModel`
- `WeatherSnapshot`
- `WeatherSelectionContext`

---

## Player

Possible Models:

- `PlayerStateModel`
- `PlayerStatusSnapshot`
- `PlayerMovementStateModel`
- `PlayerPositionModel`

---

## Stamina

Possible Models:

- `StaminaModel`
- `StaminaSnapshot`
- `StaminaConsumptionResult`

---

## Inventory

Possible Models:

- `InventoryModel`
- `InventoryEntryModel`
- `InventorySlotModel`
- `InventoryAddRequest`
- `InventoryRemoveRequest`
- `InventoryTransferRequest`
- `InventoryTransferResult`
- `InventorySnapshot`

---

## Economy

Possible Models:

- `WalletModel`
- `TransactionRequest`
- `TransactionResult`
- `SellPriceCalculation`
- `DayEndSaleEntryModel`
- `DayEndSaleSummaryModel`

---

## Tools

Possible Models:

- `ToolStateModel`
- `ToolUseRequest`
- `ToolUseResult`
- `ToolTargetContext`

---

## Interaction

Possible Models:

- `InteractionRequest`
- `InteractionResult`
- `InteractionContext`
- `AvailableInteractionModel`

---

## NPCs

Possible Models:

- `NpcRuntimeModel`
- `NpcLocationModel`
- `NpcRoutineStateModel`
- `NpcScheduleEntryModel`
- `NpcNavigationStateModel`
- `NpcAvailabilitySnapshot`

---

## Relationships

Possible Models:

- `FriendshipModel`
- `FriendshipSnapshot`
- `FriendshipChangeResult`
- `ConnectionModel`
- `ConnectionSnapshot`
- `GiftRequest`
- `GiftResult`
- `GiftReactionCalculation`
- `RelationshipDisplayModel`

---

## Dialogue

Possible Models:

- `DialogueRequest`
- `DialogueContext`
- `DialogueResult`
- `DialogueOptionModel`
- `DialogueDisplayModel`

---

## Quests

Possible Models:

- `QuestStateModel`
- `QuestObjectiveStateModel`
- `QuestStartRequest`
- `QuestStartResult`
- `QuestProgressResult`
- `QuestSnapshot`
- `QuestDisplayModel`

---

## Farming

Possible Models:

- `FarmTileStateModel`
- `CropStateModel`
- `PlantCropRequest`
- `WaterCropRequest`
- `HarvestCropRequest`
- `HarvestCropResult`
- `CropGrowthCalculation`

---

## Gathering

Possible Models:

- `ResourceNodeStateModel`
- `GatherRequest`
- `GatherResult`
- `GatheringDropModel`
- `GatheringCalculation`

---

## Fishing

Possible Models:

- `FishingStateModel`
- `FishingCatchRequest`
- `FishingCatchResult`
- `FishingCatchCalculation`

---

## Crafting / Fabrication

Possible Models:

- `CraftingRequest`
- `CraftingResult`
- `IngredientRequirementModel`
- `CraftingAvailabilitySnapshot`

---

## Inventions

Possible Models:

- `InventionStateModel`
- `InventionBuildRequest`
- `InventionBuildResult`
- `InventionProgressSnapshot`

---

## Cooking

Possible Models:

- `CookingRequest`
- `CookingResult`
- `RecipeRequirementModel`

---

## Tonics

Possible Models:

- `TonicCraftRequest`
- `TonicCraftResult`
- `TonicEffectModel`
- `TonicEffectCalculation`

---

## Restoration

Possible Models:

- `RestorationStateModel`
- `RestorationRequirementModel`
- `RestorationRequest`
- `RestorationResult`
- `RestorationSnapshot`
- `RestorationDisplayModel`

---

## Shops

Possible Models:

- `ShopStateModel`
- `ShopStockEntryModel`
- `ShopPurchaseRequest`
- `PurchaseResult`
- `ShopSellRequest`
- `ShopSellResult`
- `ShopItemDisplayModel`

---

## Mail

Possible Models:

- `MailStateModel`
- `MailInboxEntryModel`
- `MailAttachmentModel`
- `MailReadResult`
- `MailClaimResult`

---

## Festivals

Possible Models:

- `FestivalStateModel`
- `FestivalParticipantModel`
- `FestivalScoreModel`
- `FestivalResultModel`

---

## Activities

Possible Models:

- `ActivityStateModel`
- `ActivityStartRequest`
- `ActivityResult`
- `ActivityScoreModel`

---

## Ledger

Possible Models:

- `LedgerEntryModel`
- `LedgerCategoryProgressModel`
- `LedgerSnapshot`
- `CompletionCalculation`

---

## UI

Possible Models:

- `NotificationDisplayModel`
- `InventoryItemDisplayModel`
- `QuestDisplayModel`
- `RelationshipDisplayModel`
- `CalendarDayDisplayModel`
- `ShopItemDisplayModel`

UI Models should be derived from authoritative gameplay state rather than independently persisted.

---

# Example Inventory Flow

Static content:

```text
ItemDefinition
```

Runtime state:

```text
InventoryModel
    ↓
InventoryEntryModel
```

Gameplay ownership:

```text
InventorySystem
```

Operation:

```text
InventoryAddRequest
    ↓
InventorySystem.TryAddItem()
    ↓
InventoryTransferResult
```

Notification:

```text
InventoryChanged Event Channel
```

UI:

```text
InventorySnapshot
    ↓
InventoryItemDisplayModel
    ↓
Inventory UI
```

Persistence:

```text
InventoryModel
    ↓
InventorySaveData
    ↓
Save File
```

---

# Example Relationship Flow

Static NPC content:

```text
NpcDefinition
```

Runtime state:

```text
FriendshipModel
```

Gameplay ownership:

```text
NpcFriendshipSystem
```

Operation:

```text
GiftRequest
    ↓
Gift System
    ↓
GiftResult
    ↓
NpcFriendshipSystem
```

State change:

```text
FriendshipModel updated
```

Notification:

```text
FriendshipChanged Event Channel
```

UI:

```text
FriendshipSnapshot
    ↓
RelationshipDisplayModel
    ↓
Relationships Menu
```

Persistence:

```text
FriendshipModel
    ↓
FriendshipSaveData
```

---

# Example Quest Flow

Static content:

```text
QuestDefinition
```

Runtime state:

```text
QuestStateModel
    ↓
QuestObjectiveStateModel
```

Gameplay ownership:

```text
QuestSystem
```

Operation:

```text
QuestStartRequest
    ↓
QuestSystem.TryStartQuest()
    ↓
QuestStartResult
```

Progression:

```text
QuestSystem
    ↓
updates QuestStateModel
```

Notification:

```text
QuestUpdated
QuestCompleted
```

UI:

```text
QuestSnapshot
    ↓
QuestDisplayModel
```

Persistence:

```text
QuestStateModel
    ↓
QuestSaveData
```

---

# Anti-Patterns

## Models as Systems

Avoid putting entire gameplay workflows inside Models.

Bad:

```csharp
public class InventoryModel
{
    public void BuyItem(...)
    {
        // Checks shop.
        // Removes money.
        // Adds inventory.
        // Updates quests.
        // Plays audio.
        // Raises UI.
    }
}
```

Preferred:

`ShopSystem` coordinates the transaction while `InventoryModel` represents Inventory state.

---

## Public Mutable Collections

Avoid:

```csharp
public List<InventoryEntryModel> Entries;
```

This allows any caller to bypass Inventory rules.

Prefer:

```csharp
public IReadOnlyList<InventoryEntryModel> Entries => _entries;
```

---

## Models as GameObjects

Avoid deriving ordinary gameplay Models from:

```csharp
MonoBehaviour
```

unless the class is actually a Unity scene component and should therefore probably be categorized as a Controller or View instead.

---

## Models as ScriptableObjects

Avoid using ScriptableObjects for ordinary per-save mutable state solely because they are convenient Unity assets.

Static definition:

`NpcDefinition`

Runtime state:

`NpcRuntimeModel`

These are separate concepts.

---

## Models as Save Files

Avoid using serialization-focused Save Data as the live runtime representation for every gameplay feature.

Runtime state should be shaped for gameplay.

Save Data should be shaped for persistence.

---

## Duplicate Models

Avoid creating multiple Models that represent the same authoritative state in different Systems.

Example:

Do not independently maintain:

- `PlayerMoneyModel`
- `ShopMoneyModel`
- `HudMoneyModel`

when one `EconomySystem` owns the player's Bells.

The UI should display a Snapshot or presentation representation derived from that source.

---

## Giant Models

Avoid creating one model such as:

`GameModel`

containing:

- Inventory.
- NPCs.
- Quests.
- Farming.
- Weather.
- Calendar.
- Economy.
- Restoration.
- Mail.

Separate Models should align with architectural ownership.

---

## Boolean Explosion

Avoid Models filled with many loosely related booleans.

Example:

```csharp
bool isAvailable;
bool isLocked;
bool isStarted;
bool isFinished;
bool isFailed;
bool isHidden;
```

when a clear enum may better represent mutually exclusive state.

Prefer:

```csharp
QuestState State;
```

where appropriate.

---

## Primitive Obsession

Avoid repeatedly passing large groups of primitives when they represent one coherent concept.

Bad:

```csharp
StartDialogue(
    npcId,
    friendship,
    hour,
    weather,
    season,
    married,
    festival,
    questId);
```

Prefer:

```csharp
StartDialogue(DialogueContext context);
```

---

## Event Logic Inside Models

Avoid having Models decide when global gameplay Events should occur.

Event publication should generally remain with the authoritative System after it has completed the operation.

---

# Model Design Checklist

Before creating a Model, determine:

1. What information does the Model represent?
2. Who owns this information?
3. Is the information runtime state, static data, or persistent Save Data?
4. Does the Model need to be mutable?
5. Who is allowed to mutate it?
6. Can the Model be immutable?
7. Is this actually a Request?
8. Is this actually a Result?
9. Is this actually a Snapshot?
10. Is this only presentation data?
11. Does this Model duplicate an existing authoritative state?
12. Does the Model require a stable Data ID?
13. Does it need Unity-specific types?
14. Should collections be exposed read-only?
15. Does the Model need to be serialized?
16. Should Save Data be a separate structure?
17. Does a simple struct make more sense than a class?
18. Would an enum better represent part of the state?
19. Is gameplay logic accidentally being moved into the Model?
20. Can this Model be tested independently?

---

# Model Rules

- Use Models to represent structured runtime information.
- Give every mutable State Model a clear owner.
- Prefer immutable Models for Requests, Results, Snapshots, Contexts, Calculations, and presentation data.
- Keep gameplay rule ownership inside Systems.
- Keep static designer-authored content inside ScriptableObjects or Definitions.
- Keep serialization-specific structures inside Save Data.
- Do not expose mutable System collections unnecessarily.
- Use stable Data IDs for persistent content references.
- Prefer explicit Request and Result Models for complex gameplay operations.
- Prefer Snapshots when external code needs safe read-only state.
- Prefer Presentation Models when UI requires combined or reformatted information.
- Keep Models independent of GameObjects whenever practical.
- Avoid giant Models spanning unrelated gameplay dependencies.
- Avoid duplicate authoritative state.
- Avoid global mutable Models.
- Avoid using Models as Event broadcasters.
- Keep constructors valid and intentional.
- Keep naming tied to the Model's purpose.
- Only create Models that provide meaningful structure or architectural value.

---

# Related Code Setup Notes

- Controllers
- Data IDs
- Dependencies
- Enums
- Event Channels
- Game Architecture
- Game Flags
- Initialization Order
- Save Data
- Save Versioning
- Scriptable Objects
- Services

---

# Related System Notes

- System Interaction Rules
- Individual System documentation
