---
Title: Code Setup / Scriptable Objects
Game: As The Bell Tolls
Owner: Carey Clement Jr
Created: August, 2026
Updated: August, 2026
Version: 0.0.1
---

## Information

- ScriptableObjects define static, designer-authored game content and configuration.
- ScriptableObjects should primarily describe what a piece of content is rather than what is currently true about it in a specific save file.
- ScriptableObjects are assets stored in the Unity project and may be referenced by Systems, Controllers, Services, Views, and other static definitions.
- ScriptableObjects should not become the authoritative owner of per-save runtime progression.
- Runtime gameplay state should remain in Systems and runtime Models.
- Persistent gameplay state should be exported to Save Data.
- Stable Data IDs should be used when ScriptableObject content must be referenced persistently.
- ScriptableObjects are well suited for definitions that designers may edit in the Unity Inspector.
- ScriptableObjects may reference other static assets when those relationships are part of the authored content.
- ScriptableObjects should avoid scene-specific GameObject references.
- ScriptableObjects should avoid storing mutable runtime state unless the asset is explicitly intended to act as runtime infrastructure.
- Content definitions should be grouped by dependency or domain rather than placed into one universal content asset.

---

# Purpose

ScriptableObjects are used to represent authored game definitions and configuration.

They answer questions such as:

- What is this Item?
- What is this NPC?
- What does this Crop require?
- What are this Quest's objectives?
- What gifts does this NPC like?
- What ingredients does this recipe require?
- What does this Festival contain?
- What does this Weather type look and sound like?
- What does this Building unlock at each Restoration state?
- What Dialogue entries belong to this condition?
- What Audio clip set belongs to this voice definition?

ScriptableObjects should generally not answer:

- How many of this Item does the player currently own?
- What is this NPC's current Friendship level?
- Has this Quest been completed?
- Is this Crop watered today?
- What Weather is currently active?
- Has this Building already been restored in this save?
- Has this Dialogue already been seen?
- Has this Festival reward already been claimed?

Those values belong to runtime Systems and Save Data.

---

# Core Principle

ScriptableObjects describe static content.

Runtime Models describe active state.

Save Data describes persistent state.

Example:

```text
ItemDefinition
    ↓
Describes Item

InventoryEntryModel
    ↓
Represents runtime possession

InventoryEntrySaveData
    ↓
Persists possession
```

Another example:

```text
NpcDefinition
    ↓
Describes NPC

FriendshipModel
    ↓
Represents current Friendship

FriendshipSaveData
    ↓
Persists Friendship
```

---

# ScriptableObject Responsibilities

ScriptableObjects may:

- Define content.
- Store stable IDs.
- Store display names.
- Store descriptions.
- Store icons.
- Store Prefab references.
- Store Audio references.
- Store static numerical values.
- Store static requirements.
- Store authored lists.
- Store authored relationships between definitions.
- Store designer configuration.
- Store static balancing values.
- Store references to other ScriptableObject definitions.
- Provide lightweight read-only helper properties.
- Provide editor-facing validation.

ScriptableObjects should not:

- Own per-save mutable progression.
- Track current Inventory quantities.
- Track current Friendship.
- Track current Quest progression.
- Track current Crop growth.
- Track current Weather.
- Track current Calendar date.
- Store live Player state.
- Store current NPC positions.
- Save files directly.
- Load scenes.
- Control UI.
- Play Audio directly as gameplay behavior.
- Subscribe permanently to runtime Event Channels unless explicitly acting as infrastructure.
- Depend on scene object instances.
- Become general-purpose managers.

---

# Definition Pattern

Most content ScriptableObjects should use a `Definition` suffix.

Examples:

- `ItemDefinition`
- `NpcDefinition`
- `CropDefinition`
- `QuestDefinition`
- `RecipeDefinition`
- `InventionDefinition`
- `FestivalDefinition`
- `BuildingDefinition`
- `LocationDefinition`
- `DialogueDefinition`
- `AudioDefinition`

Example:

```csharp
[CreateAssetMenu(
    fileName = "Item_",
    menuName = "As The Bell Tolls/Items/Item Definition")]
public sealed class ItemDefinition : ScriptableObject
{
    [SerializeField] private string _id;
    [SerializeField] private string _displayName;
    [SerializeField] private string _description;
    [SerializeField] private ItemCategory _category;
    [SerializeField] private Sprite _icon;
    [SerializeField] private int _baseSellValue;
    [SerializeField] private int _maximumStackSize;

    public string Id => _id;
    public string DisplayName => _displayName;
    public string Description => _description;
    public ItemCategory Category => _category;
    public Sprite Icon => _icon;
    public int BaseSellValue => _baseSellValue;
    public int MaximumStackSize => _maximumStackSize;
}
```

---

# Read-Only Runtime Access

Definition data should normally be exposed through read-only properties.

Prefer:

```csharp
[SerializeField] private string _displayName;

public string DisplayName => _displayName;
```

over:

```csharp
public string displayName;
```

for primary content definitions.

This provides:

- Inspector editing.
- Controlled runtime access.
- Clearer encapsulation.
- Easier future validation.

---

# Stable Data IDs

Every persistently referenced content definition should have a stable Data ID.

Example:

```csharp
[SerializeField] private string _id;
public string Id => _id;
```

Example IDs:

```text
item_herb_peppermint
npc_lockwood_adrian
quest_story_example
crop_strawberry
location_blackmere_town-square
building_town-hall
festival_example
invention_example
```

The ID should remain stable even if:

- Display Name changes.
- Asset file name changes.
- Asset moves folders.
- Art changes.
- Description changes.
- Localization changes.

Related Notes:

- Data IDs

---

# IDs vs Asset References

Direct ScriptableObject references are useful at runtime and during authoring.

Stable IDs are useful for:

- Save Data.
- Persistent world state.
- Serialized external data.
- Cross-version compatibility.
- Debug logs.
- Data lookup.

Example authored relationship:

```csharp
[SerializeField]
private ItemDefinition _requiredItem;
```

may be appropriate inside another ScriptableObject.

Example persistent save relationship:

```csharp
public string itemId;
```

should use the stable ID.

---

# Asset References

ScriptableObjects may reference other project assets when those references define static content.

Examples:

- Sprite.
- Texture.
- AudioClip.
- AnimationClip.
- Prefab.
- Material.
- Other ScriptableObjects.
- Timeline assets.
- Localization assets.

Example:

```csharp
[SerializeField] private Sprite _icon;
[SerializeField] private GameObject _worldPrefab;
[SerializeField] private AudioClip _pickupAudio;
```

These references belong in the definition when they are part of how that content is presented.

---

# Scene References

ScriptableObjects should not directly reference scene instances.

Avoid:

```csharp
[SerializeField]
private Transform _townSquareSpawn;
```

when the Transform belongs to a loaded Unity scene.

Scene-specific references should instead be handled through:

- Scene Controllers.
- Stable spawn IDs.
- Location IDs.
- Runtime registration.
- Scene-specific lookup components.

Example:

```text
spawn_blackmere_town-square_south
```

can be resolved by the current scene.

---

# ScriptableObjects vs Models

## ScriptableObject

Defines static content.

Example:

```csharp
ItemDefinition
```

Contains:

- ID.
- Display Name.
- Description.
- Category.
- Icon.
- Base Sell Value.
- Maximum Stack Size.

## Runtime Model

Represents current runtime information.

Example:

```csharp
InventoryEntryModel
```

Contains:

- Item ID.
- Quantity.
- Quality.

The ScriptableObject answers:

"What is Peppermint?"

The Model answers:

"How much Peppermint does this Inventory currently contain?"

Related Notes:

- Models

---

# ScriptableObjects vs Save Data

ScriptableObjects are project assets.

Save Data belongs to a particular playthrough.

Example:

`NpcDefinition`

contains:

- NPC identity.
- Birthday.
- Profession.
- Gift preferences.
- Personality.
- Portrait.
- Dialogue references.

`NpcRelationshipSaveData`

contains:

- Friendship points.
- Connection progression.
- Relationship progression.
- Persistent relationship state.

Do not modify the ScriptableObject to reflect one player's progress.

---

# ScriptableObjects vs Systems

ScriptableObjects define content.

Systems apply gameplay rules to that content.

Example:

`CropDefinition`

may define:

- Crop ID.
- Season availability.
- Growth stages.
- Base sell value.
- Seed Item.
- Harvest Item.
- Water requirements.

`FarmingSystem`

determines:

- Whether planting is allowed.
- Current growth progress.
- Whether the Crop was watered.
- Whether growth advances.
- Whether harvesting succeeds.

The ScriptableObject should not become a replacement for the Farming System.

---

# ScriptableObjects vs Services

ScriptableObjects store configuration.

Services perform reusable operations.

Example:

`GiftPreferenceDefinition`

may contain authored gift preferences.

`GiftEvaluationService`

may calculate the reaction to a gift using:

- NPC Definition.
- Item Definition.
- Current context.

The ScriptableObject provides data.

The Service performs the operation.

---

# Static Data vs Runtime State

A useful rule:

If changing a value should affect every new and existing playthrough because the content itself changed, it may belong in a ScriptableObject.

If changing a value should affect only one active save, it generally belongs in runtime state and Save Data.

Example:

Base Sell Value:

```text
ItemDefinition
```

Current quantity owned:

```text
InventoryModel
```

Example:

NPC Birthday:

```text
NpcDefinition
```

Friendship Points:

```text
FriendshipModel
```

Example:

Crop total growth duration:

```text
CropDefinition
```

Current growth stage:

```text
CropStateModel
```

---

# Authoring vs Runtime

ScriptableObjects are authored before or during development.

Runtime Systems consume them.

General flow:

```text
Unity Inspector
    ↓
ScriptableObject Definition
    ↓
Data Registry
    ↓
Runtime System
    ↓
Runtime Models
```

Persistent state later references the content through:

```text
Stable Data ID
```

---

# Data Registries

Large numbers of definitions should generally be accessed through registries rather than manually referenced everywhere.

Example:

```csharp
public interface IItemRegistry
{
    bool TryGet(
        string itemId,
        out ItemDefinition definition);
}
```

Possible registry:

```csharp
[CreateAssetMenu(
    fileName = "ItemRegistry",
    menuName = "As The Bell Tolls/Registries/Item Registry")]
public sealed class ItemRegistry : ScriptableObject
{
    [SerializeField]
    private List<ItemDefinition> _items = new();

    private Dictionary<string, ItemDefinition> _lookup;

    public void Initialize()
    {
        _lookup = new Dictionary<string, ItemDefinition>();

        foreach (ItemDefinition item in _items)
        {
            _lookup.Add(item.Id, item);
        }
    }

    public bool TryGet(
        string itemId,
        out ItemDefinition definition)
    {
        return _lookup.TryGetValue(
            itemId,
            out definition);
    }
}
```

The exact registry implementation may vary.

---

# Registry Responsibilities

A Data Registry may:

- Hold references to content definitions.
- Build runtime ID lookups.
- Resolve stable IDs.
- Validate duplicate IDs.
- Provide enumerations of definitions.
- Support editor validation.

A Registry should not:

- Own per-save state.
- Track runtime progression.
- Become a general gameplay System.
- Change definitions dynamically for one save.

---

# Registry Categories

Recommended registries may include:

- Item Registry.
- NPC Registry.
- Quest Registry.
- Crop Registry.
- Recipe Registry.
- Invention Registry.
- Location Registry.
- Building Registry.
- Festival Registry.
- Dialogue Registry.
- Audio Registry.

Not every content type requires a dedicated registry.

A registry is most useful when content is frequently resolved by stable ID.

---

# Registry Initialization

Registries should validate their contents during initialization.

Validation should detect:

- Null entries.
- Empty IDs.
- Duplicate IDs.
- Invalid references.
- Missing required definitions.

Example:

```csharp
if (string.IsNullOrWhiteSpace(definition.Id))
{
    Debug.LogError(
        $"Definition {definition.name} has no Data ID.");
}
```

Duplicate IDs should be treated as errors.

---

# Definition Immutability at Runtime

Content definitions should generally be treated as read-only during normal gameplay.

Avoid:

```csharp
itemDefinition.BaseSellValue = 999;
```

during gameplay.

If temporary modifiers apply:

```text
Definition Base Value
        ↓
Runtime modifiers
        ↓
Calculated Final Value
```

Example:

```csharp
int finalSellValue =
    sellPriceService.Calculate(
        itemDefinition,
        quality,
        activeModifiers);
```

Do not modify the shared asset.

---

# Shared Asset Behavior

A ScriptableObject asset is shared.

Changing its runtime data may affect every System or GameObject referencing that same asset.

This makes mutable runtime state inside definitions dangerous.

Example:

Bad:

```csharp
public sealed class QuestDefinition : ScriptableObject
{
    public bool IsCompleted;
}
```

If changed at runtime, every reference sees the same asset state.

It also does not represent individual save slots correctly.

Preferred:

```text
QuestDefinition
    ↓
Static Quest information

QuestStateModel
    ↓
Per-playthrough state
```

---

# Enter Play Mode Considerations

Depending on Unity Editor settings, ScriptableObject runtime mutations may appear to reset or persist differently between Play Mode sessions.

The architecture should not depend on editor reset behavior.

Treat authored content assets as immutable during gameplay.

---

# CreateAssetMenu

Primary content definitions should generally use `CreateAssetMenu`.

Example:

```csharp
[CreateAssetMenu(
    fileName = "Npc_",
    menuName = "As The Bell Tolls/NPCs/NPC Definition")]
public sealed class NpcDefinition : ScriptableObject
{
}
```

Recommended menu hierarchy:

```text
As The Bell Tolls/
```

followed by domain.

Examples:

```text
As The Bell Tolls/Items/Item Definition
As The Bell Tolls/NPCs/NPC Definition
As The Bell Tolls/Quests/Quest Definition
As The Bell Tolls/Crops/Crop Definition
As The Bell Tolls/Recipes/Recipe Definition
As The Bell Tolls/Inventions/Invention Definition
As The Bell Tolls/Festivals/Festival Definition
As The Bell Tolls/Locations/Location Definition
```

This keeps custom asset creation organized.

---

# Asset Naming

Asset file names should be predictable and searchable.

Recommended pattern:

```text
<Type>_<ReadableName>
```

Examples:

```text
Item_Peppermint
Npc_AdrianLockwood
Quest_RepairTownHall
Crop_Strawberry
Recipe_StrawberryJam
Location_TownSquare
Building_TownHall
Festival_HarvestFestival
```

The asset name is not the persistent Data ID.

Renaming the asset should not alter save compatibility.

---

# Definition Naming

Class names should use:

```text
<Content>Definition
```

Examples:

- `ItemDefinition`
- `NpcDefinition`
- `QuestDefinition`
- `CropDefinition`

Specific specialized definitions may use:

- `ToolDefinition`
- `MealDefinition`
- `TonicDefinition`
- `FishingDefinition`
- `WeatherDefinition`
- `VoiceDefinition`

Avoid vague names such as:

- `ItemData`
- `NPCData`
- `QuestInfo`

when the class represents a static content definition.

The `Definition` suffix makes its architectural role explicit.

---

# Serialized Fields

Prefer private serialized fields with public read-only properties.

Example:

```csharp
[SerializeField]
private int _baseValue;

public int BaseValue => _baseValue;
```

Benefits:

- Inspector compatibility.
- Encapsulation.
- Easier validation.
- Prevents accidental runtime mutation.

---

# Inspector Organization

Definitions with many fields should be organized for authoring clarity.

Possible techniques:

- `[Header]`
- `[Tooltip]`
- Custom Editors.
- Property Drawers.
- Nested serializable structures.

Example:

```csharp
[Header("Identity")]
[SerializeField] private string _id;
[SerializeField] private string _displayName;

[Header("Economy")]
[SerializeField] private int _baseSellValue;

[Header("Presentation")]
[SerializeField] private Sprite _icon;
```

Avoid excessive Inspector decoration that makes fields harder to scan.

---

# Tooltips

Use Tooltips where a field's meaning is not obvious.

Example:

```csharp
[Tooltip("Stable ID used by Save Data. Do not change after release.")]
[SerializeField]
private string _id;
```

Tooltips are especially useful for:

- Stable IDs.
- Value ranges.
- Designer assumptions.
- Special restrictions.

---

# Validation with OnValidate

`OnValidate()` may be used for editor-time validation.

Example:

```csharp
private void OnValidate()
{
    _baseSellValue = Mathf.Max(0, _baseSellValue);
    _maximumStackSize = Mathf.Max(1, _maximumStackSize);
}
```

Use it for simple local invariants.

Do not place major gameplay logic inside `OnValidate()`.

---

# Validation Rules

Definitions should validate:

- Required ID.
- Required Display Name.
- Required references.
- Non-negative values.
- Valid ranges.
- Duplicate child entries.
- Impossible authored configurations.

Cross-asset validation may be handled by:

- Registries.
- Editor tooling.
- Validation tests.

---

# Runtime Exceptions

Definitions should be assumed valid by normal runtime gameplay after startup validation.

Invalid authored content should ideally be caught during development rather than discovered after release.

---

# Nested Serializable Definitions

Not every piece of authored static data needs its own ScriptableObject asset.

Small structures may be nested serializable classes or structs.

Example:

```csharp
[Serializable]
public sealed class IngredientRequirement
{
    [SerializeField] private ItemDefinition _item;
    [SerializeField] private int _quantity;

    public ItemDefinition Item => _item;
    public int Quantity => _quantity;
}
```

Used by:

```csharp
public sealed class RecipeDefinition : ScriptableObject
{
    [SerializeField]
    private List<IngredientRequirement> _ingredients;
}
```

This avoids creating unnecessary assets for tiny subordinate structures.

---

# When to Use a Separate ScriptableObject

Create a separate asset when the data:

- Has its own stable identity.
- Is referenced by many definitions.
- Needs independent editing.
- Needs its own registry.
- Has meaningful reusable content.
- May be referenced by Save Data.
- Represents a standalone piece of game content.

Keep data nested when it:

- Only belongs to one parent definition.
- Has no stable identity.
- Is not reused.
- Is small and simple.

---

# Composition

Definitions should prefer composition over giant inheritance trees.

Example:

```text
NpcDefinition
    ↓
VoiceDefinition
GiftPreferenceDefinition
ScheduleDefinition
```

may be clearer than:

```text
MarriageCandidateNpcDefinition
    ↓
AdultNpcDefinition
    ↓
HumanNpcDefinition
    ↓
NpcDefinition
```

Inheritance may still be appropriate when there is a genuine behavioral or data relationship, but it should not be used solely to organize Inspector fields.

---

# Definition References

A definition may reference other definitions.

Example:

```csharp
public sealed class RecipeDefinition : ScriptableObject
{
    [SerializeField]
    private ItemDefinition _result;

    [SerializeField]
    private List<IngredientRequirement> _ingredients;
}
```

This is appropriate because the recipe is static authored content.

Save Data would still store stable IDs.

---

# Circular Definition References

Avoid unnecessary circular references.

Example:

```text
NpcDefinition
    ↓
Residence LocationDefinition

LocationDefinition
    ↓
List of every resident NpcDefinition
```

This may be acceptable in limited cases but can create maintenance complexity.

Prefer one authoritative direction where possible.

Example:

`NpcDefinition` owns Residence ID/reference.

The location's resident list can be derived by registry lookup if needed.

---

# Item Definitions

Suggested responsibilities:

```csharp
public sealed class ItemDefinition : ScriptableObject
```

Possible fields:

- ID.
- Display Name.
- Description.
- Item Category.
- Icon.
- World Prefab.
- Base Sell Value.
- Stack Size.
- Quality eligibility.
- Gift category.
- Ledger category.
- Tags.

Runtime state such as quantity and quality belongs elsewhere.

---

# Tool Definitions

Possible fields:

- Tool ID.
- Display Name.
- Description.
- Icon.
- Tool Type.
- Base Stamina Cost.
- Animation information.
- Audio cues.
- Upgrade definitions.

Runtime state:

- Unlocked.
- Current upgrade.
- Equipped.

belongs to the Tool System and Save Data.

---

# Crop Definitions

Possible fields:

- Crop ID.
- Display Name.
- Seed Item.
- Harvest Item.
- Valid Seasons.
- Growth stages.
- Days per stage.
- Harvest quantity.
- Regrowth behavior.
- Base presentation references.

Runtime state:

- Planted date.
- Current stage.
- Watered today.
- Tile location.
- Current growth progress.

belongs to Farming runtime state.

---

# NPC Definitions

Possible fields:

- NPC ID.
- Display Name.
- Full Name.
- Pronouns.
- Pronunciation.
- Birthday.
- Profession.
- Residence.
- Workplace.
- Core traits.
- Gift preferences.
- Personality information.
- Portraits.
- Model/prefab references.
- Voice definition.
- Dialogue references.
- Routine definitions.

Runtime state:

- Friendship.
- Connection progression.
- Current location.
- Current routine.
- Marriage state.
- Current event state.

belongs to gameplay Systems.

---

# Gift Preference Definitions

Gift preferences may be stored directly inside `NpcDefinition` or separated into reusable data structures.

Possible authored categories:

- Favorite.
- Loved.
- Liked.
- Disliked.
- Hated.

Values should reference stable Item definitions or IDs.

Runtime Friendship effects remain part of Relationship gameplay logic.

---

# Dialogue Definitions

Dialogue ScriptableObjects may contain:

- Dialogue ID.
- Speaker.
- Dialogue text or localization key.
- Conditions.
- Priority.
- Availability category.
- Response options.
- Follow-up references.
- Event linkage.

Runtime state such as:

- Already seen.
- Current Dialogue node.
- Current conversation session.
- One-time Dialogue completion.

belongs outside the definition.

---

# Quest Definitions

Possible fields:

- Quest ID.
- Display Name.
- Description.
- Giver.
- Requirements.
- Objectives.
- Rewards.
- Prerequisite Quests.
- Expiration rules.
- Quest category.

Runtime state:

- Locked.
- Available.
- Active.
- Objective progress.
- Completed.
- Reward claimed.

belongs to the Quest System.

---

# Quest Objective Definitions

Quest objectives may be nested inside `QuestDefinition`.

Example:

```csharp
[Serializable]
public sealed class QuestObjectiveDefinition
{
    [SerializeField] private string _objectiveId;
    [SerializeField] private QuestObjectiveType _type;
    [SerializeField] private string _targetId;
    [SerializeField] private int _requiredAmount;
}
```

Each objective should have a stable local identifier when Save Data needs to persist progress for it.

Example:

```text
collect_wood
repair_door
talk_to_edward
```

---

# Recipe Definitions

Possible fields:

- Recipe ID.
- Display Name.
- Result Item.
- Result quantity.
- Ingredients.
- Required station.
- Unlock conditions.
- Category.

Runtime state:

- Unlocked.
- Crafted before.
- Current crafting operation.

belongs elsewhere.

---

# Invention Definitions

Possible fields:

- Invention ID.
- Display Name.
- Description.
- Invention type.
- Ingredients.
- Fabrications.
- Requirements.
- Unlock prerequisites.
- Icon.
- Prefab.
- Effects.

Runtime state:

- Unlocked.
- Built.
- Progress.
- Claimed reward.

belongs to the Invention System.

---

# Restoration Definitions

Possible fields:

- Restoration target ID.
- Display Name.
- Target type.
- Restoration stages.
- Stage requirements.
- Stage rewards.
- Unlocks.
- Presentation references.

Runtime state:

- Current Restoration state.
- Current progress.
- Completed stages.

belongs to the Restoration System.

---

# Building Definitions

Possible fields:

- Building ID.
- Display Name.
- Location.
- Owner.
- Shop definition.
- Restoration definition.
- Opening schedule.
- Icon.
- Scene/presentation identifiers.

Runtime state:

- Current Restoration stage.
- Special closures.
- Temporary availability.

belongs to runtime Systems.

---

# Location Definitions

Possible fields:

- Location ID.
- Display Name.
- Region.
- Scene identifier.
- Map icon.
- Fast travel rules.
- Default Audio profile.
- Weather exposure.
- Spawn IDs.

Avoid direct references to runtime scene instances.

---

# Weather Definitions

Possible fields:

- Weather Type.
- Display Name.
- UI icon.
- Ambient Audio.
- Particle Prefab.
- Lighting configuration.
- Gameplay modifiers.
- Seasonal availability.

Current Weather is runtime state.

---

# Festival Definitions

Possible fields:

- Festival ID.
- Display Name.
- Calendar date.
- Location.
- Start time.
- End time.
- Requirements.
- Activities.
- Rewards.
- NPC participation.
- Dialogue references.

Runtime state:

- Attended.
- Score.
- Rewards claimed.
- One-time progression.

belongs outside the definition.

---

# Audio Definitions

Possible definitions:

- Music Definition.
- Ambient Audio Definition.
- Speech Voice Definition.
- Weather Audio Definition.
- Sound Effect Definition.

Example:

```csharp
public sealed class VoiceDefinition : ScriptableObject
{
    [SerializeField] private float _basePitch;
    [SerializeField] private float _pitchVariation;
    [SerializeField] private float _volume;
    [SerializeField] private float _volumeVariation;
    [SerializeField] private float _minimumInterval;
    [SerializeField] private AudioClip[] _clips;
}
```

Runtime Audio playback belongs to Audio Systems or Controllers.

---

# Configuration ScriptableObjects

ScriptableObjects may also represent static system configuration rather than content.

Examples:

- Time Configuration.
- Stamina Configuration.
- Relationship Configuration.
- Economy Configuration.
- Farming Configuration.
- Fishing Configuration.
- Camera Configuration.

Example:

```csharp
[CreateAssetMenu(
    fileName = "TimeConfig",
    menuName = "As The Bell Tolls/Configuration/Time")]
public sealed class TimeConfiguration : ScriptableObject
{
    [SerializeField] private int _minutesPerTick = 10;
    [SerializeField] private float _realSecondsPerTick = 7f;

    public int MinutesPerTick => _minutesPerTick;
    public float RealSecondsPerTick => _realSecondsPerTick;
}
```

Configuration values should still be treated as authored static data.

---

# Configuration vs Constants

Use configuration assets when values:

- Need designer tuning.
- May vary by game mode.
- Are shared across multiple components.
- Benefit from Inspector editing.

Use code constants when values:

- Are architectural invariants.
- Should never be designer-edited.
- Represent fixed implementation limits.

Avoid moving every numeric literal into a ScriptableObject merely because it can be edited.

---

# Event Channels as ScriptableObjects

Event Channels may themselves be ScriptableObject assets.

This is a specialized infrastructure use of ScriptableObjects.

Example:

```csharp
[CreateAssetMenu(
    fileName = "Event_",
    menuName = "As The Bell Tolls/Events/Void Event Channel")]
public sealed class VoidEventChannel : ScriptableObject
{
    public event Action Raised;

    public void Raise()
    {
        Raised?.Invoke();
    }
}
```

These are not content definitions.

They should be stored separately from content ScriptableObjects.

Related Notes:

- Event Channels

---

# Runtime Sets as ScriptableObjects

Runtime Set ScriptableObjects can be useful in some architectures for tracking active objects.

However, they introduce shared mutable runtime asset state and should be used carefully.

For this project, prefer:

- Systems.
- Registries.
- Runtime collections.

unless a Runtime Set provides a clear architectural advantage.

Do not use Runtime Sets as a substitute for authoritative gameplay Systems.

---

# ScriptableObject Variables

ScriptableObject variable assets such as:

```text
FloatVariable
IntVariable
BoolVariable
```

can be useful in some Unity architectures.

However, widespread use may obscure ownership of gameplay state.

For this architecture:

- Persistent gameplay state should remain in Systems.
- ScriptableObject variable assets should not become alternate sources of truth.
- Use them only for clearly justified infrastructure or presentation cases.

---

# Inspector Editing Safety

Because ScriptableObjects are project assets, accidental edits may modify source content.

Important content should be protected through:

- Version control.
- Validation.
- Clear naming.
- Organized folders.
- Custom Editors where useful.

Avoid runtime code that modifies authored assets and unintentionally leaves changed values in development workflows.

---

# Editor-Only Utilities

Editor scripts may be created to:

- Generate definitions.
- Bulk assign IDs.
- Validate IDs.
- Search duplicate IDs.
- Audit missing references.
- Sort registries.
- Generate registry contents.
- Verify asset naming.
- Build content reports.

Editor-only code should remain inside an `Editor` folder or Editor-only assembly.

---

# Automatic ID Generation

IDs may be automatically generated during content creation, but should not automatically change afterward.

Possible flow:

```text
Create Asset
    ↓
Generate Initial Stable ID
    ↓
Designer may verify
    ↓
ID becomes permanent
```

Avoid regenerating IDs based on asset file name every time the asset is renamed.

---

# Duplicate ID Detection

Duplicate IDs must be treated as invalid.

Example:

Two assets both contain:

```text
item_herb_peppermint
```

This should produce an editor or initialization error.

Otherwise Save Data lookup becomes ambiguous.

---

# Missing ID Detection

Persistently referenced definitions must not have empty IDs.

Validation should catch:

```text
null
""
" "
```

before runtime.

---

# Data ID Validation

IDs should follow the project's Data ID rules.

Example pattern:

```text
category_subcategory_name
```

or the established project-specific format.

Examples:

```text
npc_lockwood_adrian
item_herb_peppermint
audio_weather_light-wind
```

Related Notes:

- Data IDs

---

# Localization

Display text may eventually use localization keys rather than hardcoded strings.

Example:

```csharp
[SerializeField]
private string _displayNameKey;
```

or a Unity Localization reference.

Stable Data IDs should remain separate from localization keys.

Example:

```text
Data ID:
npc_lockwood_adrian

Localization Key:
npc.lockwood_adrian.display_name
```

Changing localized text should never affect persistence.

---

# Addressables

If the project later uses Unity Addressables, ScriptableObject definitions may be loaded through Addressables.

Architecture should still preserve:

```text
Stable Data ID
```

as gameplay identity.

Addressable address or GUID should not automatically replace the intentional Data ID unless that is a deliberate project-wide decision.

---

# Lazy Loading

Large content libraries may eventually be loaded lazily.

Systems should depend on registry interfaces or content providers rather than assuming every ScriptableObject asset is always loaded.

This can be introduced later if required.

The initial architecture can remain simpler while preserving clear boundaries.

---

# Asset Bundles and Platform Builds

ScriptableObject definitions included in builds must have valid dependency references.

Platform-specific presentation assets may be separated when needed.

Gameplay identity should remain platform-independent.

Example:

```text
item_herb_peppermint
```

should remain the same on:

- PC.
- macOS.
- Nintendo Switch.
- Xbox.
- PlayStation.

---

# ScriptableObject Inheritance

Inheritance may be used when several definitions genuinely share a common contract.

Example:

```csharp
public abstract class ItemDefinitionBase : ScriptableObject
{
    public abstract string Id { get; }
}
```

Possible subclasses:

```text
ToolDefinition
SeedDefinition
MealDefinition
```

However, deep inheritance hierarchies should be avoided.

Prefer:

- Shared interfaces.
- Composition.
- Nested serializable data.

when those produce clearer authoring and runtime behavior.

---

# Interfaces

Definitions may implement lightweight interfaces for shared read-only contracts.

Example:

```csharp
public interface IIdentifiableDefinition
{
    string Id { get; }
}
```

Used by:

```csharp
public sealed class ItemDefinition :
    ScriptableObject,
    IIdentifiableDefinition
{
}
```

Interfaces may simplify:

- Registry validation.
- Editor tooling.
- Generic lookup.
- Testing.

---

# Base Definition

A small shared base definition may be useful.

Example:

```csharp
public abstract class DefinitionBase : ScriptableObject
{
    [SerializeField]
    private string _id;

    public string Id => _id;
}
```

Possible derived classes:

```text
ItemDefinition
NpcDefinition
QuestDefinition
CropDefinition
```

Use a base class only if the shared fields and behavior are truly universal.

Do not force unrelated definitions into a base type solely for convenience.

---

# Display Definition Base

If many content definitions share:

- ID.
- Display Name.
- Description.
- Icon.

a shared base may be appropriate.

Example:

```csharp
public abstract class DisplayDefinitionBase : DefinitionBase
{
    [SerializeField] private string _displayName;
    [SerializeField] private string _description;
    [SerializeField] private Sprite _icon;

    public string DisplayName => _displayName;
    public string Description => _description;
    public Sprite Icon => _icon;
}
```

However, avoid assuming every definition needs all display fields.

---

# Tags

Definitions may use authored tags when content needs flexible categorization.

Example:

```text
Herb
Flower
Mineral
Fish
Giftable
CookingIngredient
```

Prefer established enums when categories are finite and stable.

Use tags when:

- Multiple overlapping categories are needed.
- Designers need flexible classification.
- New tags should not require modifying code.

Avoid arbitrary tags for core gameplay state where a strongly typed enum is clearer.

---

# Definition Conditions

Definitions may contain declarative conditions.

Example Quest condition:

```text
Friendship ≥ 4
Town Hall Restoration ≥ Recovering
Season = Autumn
```

The definition should describe the condition.

A runtime Condition Service or System evaluates it.

Avoid embedding large gameplay execution logic directly inside the ScriptableObject.

---

# Definition Rewards

Definitions may contain declarative rewards.

Example:

```text
Reward:
- 500 Bells
- Recipe Unlock
- Friendship Gain
```

A runtime Reward System or owning gameplay System applies them.

The definition describes the reward.

It does not directly mutate runtime state.

---

# Polymorphic Authored Data

Some content may require different condition, objective, or reward types.

Potential approaches include:

- Serializable nested types.
- ScriptableObject sub-assets.
- Managed references.
- Separate definition assets.

Choose the simplest approach that remains:

- Inspectable.
- Testable.
- Serializable.
- Maintainable.

Do not introduce complex polymorphic authoring before the content requires it.

---

# Sub-Assets

Sub-assets may be useful when one definition owns several structured authored objects.

Examples:

- Complex Quest objectives.
- Dialogue nodes.
- Festival activities.

They can reduce top-level asset clutter.

However, sub-assets may be harder to manage manually.

Use them only when they improve authoring workflow.

---

# Content Dependencies

Definitions may depend on other definitions.

Example:

```text
RecipeDefinition
    ↓
ItemDefinition

QuestDefinition
    ↓
NpcDefinition
    ↓
ItemDefinition

NpcDefinition
    ↓
LocationDefinition
    ↓
VoiceDefinition
```

Dependencies should represent authored relationships, not runtime ownership.

---

# Dependency Validation

Validation should detect missing required references.

Example:

A Recipe with:

```text
Result Item = null
```

should be invalid.

A Quest objective that references a missing target should be invalid.

A Crop without a harvest Item should be invalid unless the design intentionally supports it.

---

# ScriptableObject Lifetime

ScriptableObject assets generally live independently of loaded scenes.

This makes them useful for static definitions.

However, their asset lifetime does not mean they should own gameplay session state.

Asset lifetime and gameplay ownership are separate concepts.

---

# Loading Definitions

Definitions should be loaded before Systems that require them are initialized.

High-level flow:

```text
Core Infrastructure
    ↓
Definition Assets
    ↓
Registries
    ↓
Registry Validation
    ↓
Gameplay Systems
    ↓
Save Restoration
```

Related Notes:

- Initialization Order

---

# Definitions and Save Restoration

During Save restoration:

```text
Save Data Item ID
    ↓
Item Registry
    ↓
ItemDefinition
```

If the ID cannot be resolved:

```text
Validation / Migration / Recovery
```

should determine how to proceed.

The Save file should not need to serialize the entire Item definition.

---

# Content Removal

Removing a shipped ScriptableObject may affect existing Save Data.

Before deleting content:

1. Determine whether its ID may exist in Save Data.
2. Determine whether old saves reference it.
3. Add migration or fallback handling if necessary.
4. Remove the content only when compatibility is safe.

---

# Content Renaming

Renaming:

```text
Item_Peppermint.asset
```

is safe when:

```text
item_herb_peppermint
```

remains unchanged.

Renaming the Data ID is a persistence change.

---

# Content Rebalancing

Changing authored values such as:

- Sell Value.
- Growth duration.
- Stamina cost.
- Gift preference.
- Reward amount.

changes the content definition for all loaded saves after the new build.

This is usually desirable for balancing changes.

If historical values must remain locked for existing saves, that specific result should be persisted when the gameplay outcome occurs.

---

# Authoritative Historical Outcomes

Example:

A Quest reward changes from:

```text
500 Bells
```

to:

```text
750 Bells
```

Players who already completed the Quest should not receive the reward again.

Persist:

```text
Quest completed
Reward claimed
```

The current definition may contain the new reward for future completions.

---

# Versioning Definitions

Individual ScriptableObject definitions generally do not need schema versions inside every asset.

Save compatibility is primarily handled through:

- Stable IDs.
- Save Versioning.
- Migration.

Definition schema changes are handled through Unity serialization and code migration as needed.

---

# Testing Definitions

Content definitions should be tested or validated for:

- Duplicate IDs.
- Missing IDs.
- Missing required references.
- Invalid ranges.
- Invalid enum values.
- Impossible requirements.
- Broken prerequisite references.
- Circular dependencies where prohibited.
- Duplicate objective IDs.
- Duplicate reward IDs where relevant.
- Invalid registry entries.
- Missing presentation assets where required.

---

# Content Audit Tools

Editor validation may provide reports such as:

```text
Items
- 120 valid
- 2 missing IDs
- 1 duplicate ID

NPCs
- 31 valid
- 0 missing IDs

Quests
- 48 valid
- 3 missing prerequisite references
```

These tools can make large content libraries significantly safer to maintain.

---

# ScriptableObject Folder Structure

Recommended project structure:

```text
Assets/
|
|-- AsTheBellTolls/
|   |
|   |-- Data/
|   |   |
|   |   |-- ScriptableObjects/
|   |   |   |
|   |   |   |-- Items/
|   |   |   |-- Tools/
|   |   |   |-- Crops/
|   |   |   |-- NPCs/
|   |   |   |-- Dialogue/
|   |   |   |-- Quests/
|   |   |   |-- Recipes/
|   |   |   |-- Inventions/
|   |   |   |-- Restoration/
|   |   |   |-- Buildings/
|   |   |   |-- Locations/
|   |   |   |-- Weather/
|   |   |   |-- Festivals/
|   |   |   |-- Audio/
|   |   |   |-- Configuration/
|   |   |
|   |   |-- Registries/
|   |
|   |-- Events/
|   |   |-- Channels/
```

Event Channel ScriptableObjects should remain separate from content definitions.

---

# Definition Asset Folder Structure

Actual assets may be organized by content type.

Example:

```text
ScriptableObjects/
|
|-- Items/
|   |-- Herbs/
|   |-- Flowers/
|   |-- Minerals/
|   |-- Fish/
|
|-- NPCs/
|   |-- MarriageCandidates/
|   |-- Townspeople/
|   |-- Children/
|
|-- Quests/
|   |-- Story/
|   |-- Friendship/
|   |-- Connection/
|   |-- Restoration/
|
|-- Audio/
|   |-- Music/
|   |-- Speech/
|   |-- Weather/
```

Organization should optimize editor navigation rather than runtime behavior.

---

# Namespaces

ScriptableObject classes should live inside the dependency namespace that owns the definition.

Examples:

```csharp
AsTheBellTolls.Items
AsTheBellTolls.Characters
AsTheBellTolls.Quests
AsTheBellTolls.Farming
AsTheBellTolls.Audio
```

Shared registry interfaces may live in:

```csharp
AsTheBellTolls.Data
```

or another established foundational namespace.

Related Notes:

- Dependencies

---

# Assemblies

If Assembly Definitions are used, ScriptableObject dependencies should follow the same dependency rules as the rest of the architecture.

Example:

```text
Data
    ↓
Items
    ↓
Inventory
```

Avoid making foundational Data assemblies depend on high-level runtime Systems.

---

# Addressing ScriptableObject References in Tests

Tests may:

- Create temporary ScriptableObject instances with `ScriptableObject.CreateInstance<T>()`.
- Use test fixture assets.
- Mock registry interfaces.

Gameplay Systems should not require AssetDatabase access at runtime.

`AssetDatabase` belongs to Editor tooling only.

---

# AssetDatabase

Never depend on:

```csharp
UnityEditor.AssetDatabase
```

inside runtime gameplay code.

AssetDatabase is Editor-only.

Runtime builds should receive definitions through:

- Serialized references.
- Registries.
- Addressables.
- Resources when intentionally used.
- Dependency injection or initialization.

---

# Resources Folder

Avoid placing all ScriptableObjects inside Unity `Resources` solely for convenient global lookup.

`Resources.Load()` can be useful in limited cases but should not become the main architecture for content access.

Prefer explicit registries or a deliberate content-loading strategy.

---

# Singleton ScriptableObjects

Avoid treating content ScriptableObjects as mutable global singletons.

Configuration assets may be globally shared when they are read-only.

Example:

```text
TimeConfiguration
```

may be one project-wide asset.

But:

```text
PlayerRuntimeState
```

should not be a mutable ScriptableObject singleton.

---

# Runtime Cloning

In rare cases, runtime code may instantiate a copy of a ScriptableObject.

Example:

```csharp
Instantiate(definition);
```

This creates runtime asset state separate from the authored asset.

However, if the data is truly runtime state, a plain C# Model is usually clearer.

Prefer Models unless ScriptableObject behavior is specifically required.

---

# Editor Asset Changes During Play Mode

Developers should be cautious when changing ScriptableObject fields during Play Mode.

Depending on workflow, changes may affect project assets or create misleading testing conditions.

Gameplay state should be changed through Systems rather than editing content assets during runtime tests.

---

# Serialized Reference Safety

A definition reference may become null if an asset is deleted or moved incorrectly.

Unity usually preserves references through GUIDs when assets are moved inside the Editor.

Stable Data IDs provide a separate gameplay identity layer.

Both serve different purposes:

```text
Unity GUID
    ↓
Asset reference

Data ID
    ↓
Gameplay / persistence identity
```

---

# Unity GUID vs Data ID

Unity GUID:

- Managed by Unity.
- Identifies an asset internally.
- Used for project asset references.
- Not intended as the player's gameplay-facing identity layer.

Data ID:

- Defined by the project.
- Human-readable.
- Stable across runtime systems.
- Stored in Save Data.
- Used by registries and gameplay code.

Do not assume they are interchangeable.

---

# ScriptableObject Data Example

Example Item Definition:

```csharp
[CreateAssetMenu(
    fileName = "Item_",
    menuName = "As The Bell Tolls/Items/Item Definition")]
public sealed class ItemDefinition : ScriptableObject
{
    [Header("Identity")]
    [SerializeField] private string _id;
    [SerializeField] private string _displayName;
    [TextArea]
    [SerializeField] private string _description;

    [Header("Classification")]
    [SerializeField] private ItemCategory _category;

    [Header("Economy")]
    [Min(0)]
    [SerializeField] private int _baseSellValue;

    [Header("Inventory")]
    [Min(1)]
    [SerializeField] private int _maximumStackSize = 999;

    [Header("Presentation")]
    [SerializeField] private Sprite _icon;
    [SerializeField] private GameObject _worldPrefab;

    public string Id => _id;
    public string DisplayName => _displayName;
    public string Description => _description;
    public ItemCategory Category => _category;
    public int BaseSellValue => _baseSellValue;
    public int MaximumStackSize => _maximumStackSize;
    public Sprite Icon => _icon;
    public GameObject WorldPrefab => _worldPrefab;
}
```

---

# NPC Definition Example

```csharp
[CreateAssetMenu(
    fileName = "Npc_",
    menuName = "As The Bell Tolls/NPCs/NPC Definition")]
public sealed class NpcDefinition : ScriptableObject
{
    [Header("Identity")]
    [SerializeField] private string _id;
    [SerializeField] private string _displayName;
    [SerializeField] private string _fullName;

    [Header("Life")]
    [SerializeField] private ProfessionType _profession;
    [SerializeField] private LocationDefinition _residence;
    [SerializeField] private LocationDefinition _workplace;

    [Header("Birthday")]
    [SerializeField] private Season _birthdaySeason;
    [SerializeField] private int _birthdayDay;

    [Header("Presentation")]
    [SerializeField] private Sprite _portrait;
    [SerializeField] private GameObject _prefab;

    [Header("Audio")]
    [SerializeField] private VoiceDefinition _voice;

    public string Id => _id;
    public string DisplayName => _displayName;
    public string FullName => _fullName;
    public ProfessionType Profession => _profession;
    public LocationDefinition Residence => _residence;
    public LocationDefinition Workplace => _workplace;
    public Season BirthdaySeason => _birthdaySeason;
    public int BirthdayDay => _birthdayDay;
    public Sprite Portrait => _portrait;
    public GameObject Prefab => _prefab;
    public VoiceDefinition Voice => _voice;
}
```

Relationship and routine state do not belong inside this asset.

---

# Quest Definition Example

```csharp
[CreateAssetMenu(
    fileName = "Quest_",
    menuName = "As The Bell Tolls/Quests/Quest Definition")]
public sealed class QuestDefinition : ScriptableObject
{
    [SerializeField] private string _id;
    [SerializeField] private string _displayName;

    [TextArea]
    [SerializeField] private string _description;

    [SerializeField] private NpcDefinition _giver;

    [SerializeField]
    private List<QuestObjectiveDefinition> _objectives = new();

    [SerializeField]
    private List<RewardDefinition> _rewards = new();

    public string Id => _id;
    public string DisplayName => _displayName;
    public string Description => _description;
    public NpcDefinition Giver => _giver;
    public IReadOnlyList<QuestObjectiveDefinition> Objectives => _objectives;
    public IReadOnlyList<RewardDefinition> Rewards => _rewards;
}
```

Runtime Quest state remains separate.

---

# Recipe Definition Example

```csharp
[CreateAssetMenu(
    fileName = "Recipe_",
    menuName = "As The Bell Tolls/Recipes/Recipe Definition")]
public sealed class RecipeDefinition : ScriptableObject
{
    [SerializeField] private string _id;
    [SerializeField] private string _displayName;

    [SerializeField]
    private ItemDefinition _resultItem;

    [SerializeField]
    private int _resultQuantity = 1;

    [SerializeField]
    private List<IngredientRequirement> _ingredients = new();

    public string Id => _id;
    public string DisplayName => _displayName;
    public ItemDefinition ResultItem => _resultItem;
    public int ResultQuantity => _resultQuantity;
    public IReadOnlyList<IngredientRequirement> Ingredients => _ingredients;
}
```

---

# Definition Lookup Example

```csharp
public sealed class InventorySystem
{
    private readonly IItemRegistry _itemRegistry;

    public InventorySystem(
        IItemRegistry itemRegistry)
    {
        _itemRegistry = itemRegistry;
    }

    public ItemDefinition GetDefinition(
        string itemId)
    {
        if (!_itemRegistry.TryGet(
            itemId,
            out ItemDefinition definition))
        {
            throw new InvalidOperationException(
                $"Unknown Item ID: {itemId}");
        }

        return definition;
    }
}
```

Gameplay code does not need to search Unity assets manually.

---

# Save Restoration Example

Save entry:

```csharp
public sealed class InventoryEntrySaveData
{
    public string itemId;
    public int quantity;
}
```

Loading:

```text
InventoryEntrySaveData.itemId
        ↓
Item Registry
        ↓
ItemDefinition
        ↓
Inventory System restores runtime Model
```

If the definition is missing, Save validation or migration handles the problem.

---

# Content Relationship Example

An NPC's Favorite Gift may be authored as:

```csharp
[SerializeField]
private ItemDefinition _favoriteGift;
```

Runtime systems can access:

```csharp
npcDefinition.FavoriteGift.Id
```

Save Data should not serialize the asset reference.

---

# Data-Driven Gameplay

ScriptableObjects support data-driven gameplay by allowing content changes without rewriting gameplay classes.

Example:

`GiftSystem`

does not need:

```csharp
if (npcId == "npc_lockwood_adrian")
{
    if (itemId == "item_herb_peppermint")
    {
        ...
    }
}
```

Instead:

```text
NpcDefinition
    ↓
Gift preferences
    ↓
Gift System
```

The System remains generic.

The authored definition supplies content-specific values.

---

# Data-Driven Benefits

Benefits include:

- Faster content authoring.
- Less hardcoded gameplay data.
- Easier balancing.
- Easier expansion.
- Clearer separation of content and code.
- Better reuse.
- Easier editor validation.
- Easier localization.
- Less risk of huge switch statements.

---

# Over-Data-Driving

Not everything needs to be a ScriptableObject.

Avoid creating assets for:

- One constant used once.
- Tiny implementation details.
- Temporary state.
- Values that should be code invariants.
- Runtime calculations.
- Private helper configuration that never needs designer editing.

Use ScriptableObjects where they improve content authoring or architecture.

---

# ScriptableObject Anti-Patterns

## Runtime Save State

Bad:

```csharp
public sealed class NpcDefinition : ScriptableObject
{
    public int FriendshipPoints;
}
```

Preferred:

```text
NpcDefinition
    ↓
Static data

FriendshipModel
    ↓
Runtime state
```

---

## Quest Completion in Definition

Bad:

```csharp
questDefinition.IsCompleted = true;
```

Preferred:

```csharp
questSystem.CompleteQuest(
    questDefinition.Id);
```

---

## Inventory Quantity in Item Definition

Bad:

```csharp
itemDefinition.QuantityOwned++;
```

Preferred:

```csharp
inventorySystem.AddItem(
    itemDefinition.Id,
    1);
```

---

## Mutable Global Configuration

Avoid changing shared configuration assets as a way to store current gameplay values.

Example:

Bad:

```csharp
timeConfiguration.CurrentHour = 14;
```

Preferred:

```text
TimeConfiguration
    ↓
Static timing rules

TimeSystem
    ↓
Current runtime time
```

---

## Scene GameObject References

Avoid scene instance references inside content definitions.

Use stable IDs or scene registration.

---

## Asset Name as ID

Avoid:

```csharp
string id = definition.name;
```

as persistence identity.

Use an explicit stable Data ID.

---

## Display Name as ID

Avoid using:

```text
Peppermint
Adrian Lockwood
Town Hall
```

as stable identifiers.

Display Names may change or be localized.

---

## Runtime Asset Mutation

Avoid changing definition lists or values during normal gameplay.

Treat authored definition assets as read-only.

---

## Giant Definition

Avoid one asset such as:

```text
AllGameData
```

containing every:

- NPC.
- Item.
- Quest.
- Crop.
- Festival.
- Recipe.
- Building.
- Dialogue.

Use domain-specific definitions and registries.

---

## Excessive Inheritance

Avoid deep ScriptableObject inheritance trees that exist primarily to organize fields.

Prefer composition.

---

## Event Channel Confusion

Do not treat Event Channel ScriptableObjects as static game definitions.

They are infrastructure assets and should be documented and organized separately.

---

## Save Data References

Do not put direct ScriptableObject references into Save Data as the persistent source of identity.

Use stable Data IDs.

---

# ScriptableObject Design Checklist

Before creating a ScriptableObject, determine:

1. Is this static designer-authored data?
2. Does this content have a stable identity?
3. Is it reused by multiple runtime objects?
4. Does it benefit from Inspector editing?
5. Is it configuration rather than runtime state?
6. Should it have a stable Data ID?
7. Will Save Data ever reference it?
8. Does it need its own registry?
9. Could this instead be a nested serializable structure?
10. Does it need its own standalone asset?
11. Does it reference another definition?
12. Can those references create unnecessary cycles?
13. Does it contain any per-save mutable state?
14. Does it contain any scene references?
15. Are all runtime-facing properties read-only?
16. Are required fields validated?
17. Can duplicate IDs be detected?
18. Does it need a `CreateAssetMenu` entry?
19. Is its asset naming predictable?
20. Does it belong in the correct dependency namespace?
21. Will deleting or renaming its ID affect existing saves?
22. Is a runtime System responsible for applying its rules?
23. Is a Model responsible for active state?
24. Is Save Data responsible for persistence?

---

# ScriptableObject Rules

- Use ScriptableObjects for static designer-authored content and configuration.
- Treat content definitions as read-only during normal gameplay.
- Use the `Definition` suffix for static content assets.
- Use explicit stable Data IDs for persistently referenced content.
- Never use Display Names as persistent identifiers.
- Keep runtime gameplay state inside Systems and Models.
- Keep persistent gameplay state inside Save Data.
- Avoid scene-specific object references.
- Use read-only properties for runtime access.
- Use Data Registries for frequently resolved definition types.
- Validate missing and duplicate IDs.
- Validate required references.
- Use nested serializable structures for small subordinate authored data.
- Use separate ScriptableObjects when content has independent identity or reuse.
- Prefer composition over deep inheritance.
- Keep Event Channel ScriptableObjects separate from content definitions.
- Avoid mutable ScriptableObject variable assets as alternate gameplay sources of truth.
- Avoid giant all-content assets.
- Avoid unnecessary use of `Resources.Load`.
- Keep `AssetDatabase` inside Editor-only code.
- Separate Unity GUID identity from gameplay Data ID identity.
- Preserve shipped Data IDs or provide migration.
- Design content changes so Save compatibility remains intentional.
- Keep platform-specific storage and runtime state outside definitions.
- Use editor validation and audit tooling as the content library grows.
- Let Systems interpret and apply ScriptableObject data rather than placing full gameplay workflows inside assets.

---

# Related Code Setup Notes

- Data IDs
- Dependencies
- Enums
- Event Channels
- Game Architecture
- Initialization Order
- Models
- Save Data
- Save Versioning
- Services

---

# Related System Notes

- Data Registry Systems
- System Interaction Rules
- Individual gameplay System documentation
