---
Title: Code Setup / Save Data
Game: As The Bell Tolls
Owner: Carey Clement Jr
Created: August, 2026
Updated: August, 2026
Version: 0.0.1
---

## Information

- Save Data represents the persistent state required to reconstruct a player's game session.
- Save Data is designed for serialization and persistence rather than active gameplay behavior.
- Runtime Systems remain the authoritative owners of gameplay state while a save is loaded.
- Save Data should contain values, IDs, and serializable structures required to restore those Systems.
- Controllers, Views, UI, and scene GameObjects should not directly edit Save Data.
- ScriptableObject definitions should not be duplicated into Save Data when stable Data IDs can reference them.
- Save Data should contain persistent gameplay state only.
- Temporary presentation state should generally not be saved.
- Every save file should contain an explicit save version.
- Save structures should be designed so older saves can be migrated when the data model changes.
- Save Data should remain independent from scene-specific object references.
- Each System that owns persistent state should define how that state is exported to Save Data and restored from Save Data.
- Saving should produce a snapshot of authoritative runtime state rather than making the save file itself the live gameplay state.

---

# Purpose

Save Data exists to answer:

"What information must be stored so this playthrough can be reconstructed later?"

Save Data may contain:

- Player identity.
- Current date.
- Current time.
- Current location.
- Inventory contents.
- Tool progression.
- Currency.
- Stamina progression.
- Skill progression.
- NPC Friendship progression.
- NPC Connection progression.
- Marriage state.
- Quest progression.
- Game Event progression.
- Restoration progression.
- Farming state.
- Gatherable world state.
- Mail state.
- Invention progression.
- Recipe unlocks.
- Ledger progression.
- Festival progression.
- Persistent world changes.
- Game Flags.
- Settings that are intentionally save-slot specific.
- Save metadata.

Save Data should not contain:

- Active Unity GameObject references.
- MonoBehaviour references.
- Transform references.
- Animator references.
- Scene instance IDs.
- Cached UI state that can be reconstructed.
- Static Item descriptions.
- Static NPC biographies.
- Static Quest text.
- Static recipe definitions.
- Static ScriptableObject content.
- Event Channel listeners.
- Runtime Services.
- System instances.
- Temporary animation state.
- Temporary Audio state.
- Calculated values that can safely be reconstructed from authoritative saved values.

---

# Core Principle

The save file is not the runtime source of truth.

During gameplay:

```text
Save Data
    ↓
Load
    ↓
Systems restore runtime state
    ↓
Systems become authoritative
```

While the player is actively playing:

```text
Gameplay
    ↓
Systems
    ↓
Runtime Models
```

When saving:

```text
Systems
    ↓
Export persistent state
    ↓
Save Data
    ↓
Serialization
    ↓
Save File
```

The save file should not be continuously modified as gameplay state changes.

---

# Save Data Responsibilities

Save Data may:

- Store persistent primitive values.
- Store stable Data IDs.
- Store serializable collections.
- Store persistent state for a specific System.
- Store save metadata.
- Store version information.
- Store timestamps.
- Store persistent world-state identifiers.
- Represent optional saved content.
- Act as an intermediate structure during migration.

Save Data should not:

- Apply gameplay rules.
- Decide whether an action is valid.
- Change Friendship.
- Spend currency.
- Complete Quests.
- Raise Event Channels.
- Find Unity GameObjects.
- Load scenes.
- Update UI.
- Play Audio.
- Calculate gameplay progression unless required solely for migration.
- Become a replacement for Runtime Models or Systems.

---

# Save File Structure

The top-level save should contain metadata and grouped persistent data.

Recommended structure:

```csharp
[Serializable]
public sealed class GameSaveData
{
    public SaveMetadataData metadata;

    public PlayerSaveData player;
    public TimeSaveData time;
    public CalendarSaveData calendar;
    public WeatherSaveData weather;
    public InventorySaveData inventory;
    public EconomySaveData economy;
    public ToolSaveData tools;
    public ProgressionSaveData progression;
    public RelationshipSaveData relationships;
    public QuestSaveData quests;
    public RestorationSaveData restoration;
    public FarmingSaveData farming;
    public WorldSaveData world;
    public MailSaveData mail;
    public InventionSaveData inventions;
    public LedgerSaveData ledger;
    public FestivalSaveData festivals;
    public GameFlagSaveData gameFlags;
}
```

The exact structure may evolve as implementation begins.

The important architectural rule is that Save Data should be grouped by responsibility rather than placed into one giant unstructured list of fields.

---

# Save Metadata

Every save should contain metadata used to identify and validate the file.

Recommended metadata:

```csharp
[Serializable]
public sealed class SaveMetadataData
{
    public int saveVersion;

    public string saveId;
    public string playerName;

    public string createdUtc;
    public string lastSavedUtc;

    public int playtimeSeconds;
}
```

Possible additional metadata:

- Save slot.
- Platform-independent save identifier.
- Current Year.
- Current Season.
- Current Day.
- Current location.
- Game build version.
- Content version.
- Screenshot reference.
- Save checksum or validation information.

Metadata should not duplicate large gameplay structures.

It exists primarily for:

- Save selection.
- Validation.
- Migration.
- Debugging.
- Displaying save information before loading the full file.

---

# Save Version

Every save file must contain an explicit Save Version.

Example:

```csharp
public int saveVersion;
```

The Save Version describes the schema expected by the game.

Example:

```text
Version 1
```

may contain:

```text
player
inventory
calendar
relationships
quests
```

A future:

```text
Version 2
```

may add:

```text
restoration
```

or change the format of an existing structure.

The current game should determine:

```text
Loaded Save Version
        ↓
Current Save Version?
        ↓
Yes → Continue loading

No
        ↓
Migration required
```

Related Notes:

- Save Versioning

---

# Save Data by Ownership

Persistent data should map clearly to the System that owns it at runtime.

| Save Data | Runtime Owner |
|---|---|
| `TimeSaveData` | Time System |
| `CalendarSaveData` | Calendar System |
| `WeatherSaveData` | Weather System |
| `PlayerSaveData` | Player-related Systems |
| `InventorySaveData` | Inventory System |
| `EconomySaveData` | Economy System |
| `ToolSaveData` | Tool System |
| `RelationshipSaveData` | Relationship Systems |
| `QuestSaveData` | Quest System |
| `RestorationSaveData` | Restoration System |
| `FarmingSaveData` | Farming System |
| `WorldSaveData` | World-related Systems |
| `MailSaveData` | Mail System |
| `InventionSaveData` | Invention System |
| `LedgerSaveData` | Ledger System |
| `FestivalSaveData` | Festival System |
| `GameFlagSaveData` | Game Flag System |

A System may own more than one Save Data type when the data is sufficiently complex.

---

# Exporting Runtime State

Each persistent System should provide a controlled method for exporting its persistent state.

Example:

```csharp
public InventorySaveData CreateSaveData()
{
    var data = new InventorySaveData();

    foreach (InventoryEntryModel entry in _inventory.Entries)
    {
        data.entries.Add(new InventoryEntrySaveData
        {
            itemId = entry.ItemId,
            quantity = entry.Quantity,
            quality = (int)entry.Quality
        });
    }

    return data;
}
```

The Save System should not reach inside another System and directly copy private state.

Avoid:

```csharp
save.inventory.entries = inventorySystem._entries;
```

Prefer:

```csharp
save.inventory = inventorySystem.CreateSaveData();
```

---

# Restoring Runtime State

Each persistent System should control how its state is restored.

Example:

```csharp
public void RestoreFromSave(InventorySaveData data)
{
    _inventory.ClearInternal();

    foreach (InventoryEntrySaveData savedEntry in data.entries)
    {
        _inventory.AddInternal(
            new InventoryEntryModel(
                savedEntry.itemId,
                savedEntry.quantity,
                (ItemQuality)savedEntry.quality));
    }
}
```

Restoration should:

- Validate required values.
- Resolve IDs.
- Apply defaults where appropriate.
- Reject or repair invalid state where safely possible.
- Rebuild runtime-only lookup structures.
- Avoid publishing normal gameplay Events during partial restoration unless intentionally required.
- Leave the System in a valid runtime state.

---

# Loading Sequence

Recommended high-level load sequence:

1. Read the save file.
2. Deserialize into Save Data.
3. Validate top-level structure.
4. Read Save Version.
5. Migrate older Save Data if required.
6. Validate stable IDs where necessary.
7. Initialize runtime Systems.
8. Restore persistent System state.
9. Rebuild runtime-only derived data.
10. Load the required Unity scene.
11. Restore scene-facing world presentation.
12. Position the player.
13. Restore NPC presentation.
14. Synchronize UI.
15. Resume gameplay.
16. Raise a high-level load-complete notification if required.

Normal gameplay Event Channels should generally not fire repeatedly while Systems are only partially restored.

A dedicated synchronization step should occur after loading.

---

# Saving Sequence

Recommended high-level save sequence:

1. Confirm that saving is currently allowed.
2. Enter an appropriate Saving state if required.
3. Request persistent state from each owning System.
4. Build the top-level `GameSaveData`.
5. Assign the current Save Version.
6. Update save metadata.
7. Validate the generated Save Data.
8. Serialize the data.
9. Write safely to temporary storage.
10. Confirm the write completed.
11. Replace the previous save file.
12. Update backup data if supported.
13. Leave the Saving state.
14. Publish a Save Completed notification if required.

The game should avoid writing directly over the only valid save file before a new serialized file has been successfully created.

---

# Save Atomicity

Save operations should be designed to reduce the chance of corrupted files.

Preferred flow:

```text
Current Save
      ↓
Create New Save Data
      ↓
Serialize
      ↓
Write Temporary File
      ↓
Validate Write
      ↓
Replace Existing File
```

Optional backup flow:

```text
Existing Save
      ↓
Backup
      ↓
Replace with New Save
```

This is safer than writing directly into the only valid copy of the save file.

---

# Stable IDs

Persistent content references should use stable Data IDs.

Examples:

```text
npc_lockwood_adrian
item_herb_peppermint
location_blackmere_town-square
quest_example
building_town-hall
invention_example
```

Example:

```csharp
[Serializable]
public sealed class InventoryEntrySaveData
{
    public string itemId;
    public int quantity;
    public int quality;
}
```

Do not store:

```csharp
public ItemDefinition itemDefinition;
```

as the core persistent identifier.

The save should store:

```csharp
public string itemId;
```

The Data Registry resolves:

```text
itemId
    ↓
ItemDefinition
```

during runtime.

Related Notes:

- Data IDs

---

# IDs Must Be Stable

Once content has shipped in a public build, changing a Data ID may break old saves.

Avoid changing:

```text
npc_lockwood_adrian
```

to:

```text
npc_adrian
```

without migration support.

If an ID must change:

```text
Old ID
    ↓
Migration
    ↓
New ID
```

Example mapping:

```csharp
if (entry.itemId == "item_old_peppermint")
{
    entry.itemId = "item_herb_peppermint";
}
```

---

# Save Data vs Runtime Models

Save Data and Runtime Models serve different purposes.

## Runtime Model

Optimized for active gameplay.

May contain:

- Private mutation methods.
- Read-only collection views.
- Calculated properties.
- Lookup dictionaries.
- Cached runtime state.
- References to static definitions.
- Runtime-only helper state.

## Save Data

Optimized for serialization.

Should contain:

- Primitive values.
- Enums represented safely.
- Stable IDs.
- Serializable collections.
- Simple nested Save Data structures.

Example:

Runtime:

```csharp
public sealed class FriendshipModel
{
    public string NpcId { get; private set; }
    public int FriendshipPoints { get; private set; }
    public int HeartLevel { get; private set; }
}
```

Save:

```csharp
[Serializable]
public sealed class FriendshipEntrySaveData
{
    public string npcId;
    public int friendshipPoints;
}
```

`heartLevel` may not need to be saved if it can be deterministically recalculated from `friendshipPoints`.

Save only the authoritative value when practical.

Related Notes:

- Models

---

# Save Authoritative Values

Prefer saving values that are authoritative rather than values derived from them.

Example:

If:

```text
Friendship Points
```

determine:

```text
Heart Level
```

save:

```text
Friendship Points
```

and recalculate:

```text
Heart Level
```

during restoration.

Similarly:

If current season/day/year determine a formatted date string, do not save the formatted string.

Avoid:

```csharp
public string displayDate = "Sun. 1 of Spring";
```

Prefer:

```csharp
public int weekday;
public int day;
public int season;
public int year;
```

and reconstruct the display.

---

# Save Derived Values Only When Necessary

A derived value may be saved when:

- Reconstruction would be expensive.
- Reconstruction is nondeterministic.
- Historical state matters.
- The value represents a chosen outcome rather than a calculation.
- Recalculation rules may change and preserving the original result is important.

Otherwise, prefer rebuilding it from authoritative values.

---

# Player Save Data

Player Save Data may contain:

```csharp
[Serializable]
public sealed class PlayerSaveData
{
    public string playerName;
    public int pronounSelection;

    public string currentLocationId;

    public PlayerPositionSaveData position;

    public int currentStamina;
    public int maximumStamina;
}
```

Possible additional values:

- Appearance selections.
- Hairstyle ID.
- Hair color.
- Skin tone.
- Clothing selections.
- Permanent player upgrades.
- Player-specific unlocks.

Avoid storing static appearance definitions when IDs can reference them.

---

# Player Position Save Data

Example:

```csharp
[Serializable]
public sealed class PlayerPositionSaveData
{
    public float x;
    public float y;
    public float z;

    public float facingX;
    public float facingY;
}
```

Alternatively, use a logical spawn identifier when exact coordinates are unnecessary.

Example:

```text
location_blackmere_town-square
spawn_town-square_south
```

Logical spawn IDs may be safer when scene geometry changes between versions.

---

# Time Save Data

Example:

```csharp
[Serializable]
public sealed class TimeSaveData
{
    public int hour;
    public int minute;
}
```

Values that can be reconstructed should not be duplicated unnecessarily.

Example:

`DaylightState`

may be recalculated from:

- Time.
- Season.
- Daylight rules.

---

# Calendar Save Data

Example:

```csharp
[Serializable]
public sealed class CalendarSaveData
{
    public int weekday;
    public int day;
    public int season;
    public int year;
}
```

Calendar Save Data should represent the current game date.

Static Festival definitions do not belong here.

---

# Weather Save Data

Weather Save Data should contain enough information to preserve deterministic current and future Weather behavior.

Possible structure:

```csharp
[Serializable]
public sealed class WeatherSaveData
{
    public int currentWeather;

    public List<int> forecast = new();
}
```

If Weather generation uses a deterministic seed, the save may instead store:

- Current Weather.
- Weather seed.
- Required generation state.

The design should guarantee that loading a save does not unexpectedly reroll Weather that was already established.

---

# Inventory Save Data

Example:

```csharp
[Serializable]
public sealed class InventorySaveData
{
    public List<InventoryEntrySaveData> entries = new();
}
```

Entry:

```csharp
[Serializable]
public sealed class InventoryEntrySaveData
{
    public string itemId;
    public int quantity;
    public int quality;
}
```

Possible additional values:

- Slot index.
- Selected Tool Belt slot.
- Inventory capacity upgrades.

Save slot location only when placement/order matters.

---

# Storage Save Data

Storage containers should have stable world IDs.

Example:

```csharp
[Serializable]
public sealed class StorageContainerSaveData
{
    public string storageId;
    public List<InventoryEntrySaveData> entries = new();
}
```

Example stable ID:

```text
storage_pendrelle-manor_bedroom_001
```

This allows the save to restore contents to the correct container.

---

# Economy Save Data

Example:

```csharp
[Serializable]
public sealed class EconomySaveData
{
    public int bells;
}
```

If transaction history is not required for gameplay, it should not be saved indefinitely.

Day End selling data should only be persisted when necessary to survive saving/loading before its processing is complete.

---

# Stamina Save Data

Possible values:

```csharp
[Serializable]
public sealed class StaminaSaveData
{
    public int current;
    public int maximum;
}
```

If `maximum` is fully derived from permanent progression, consider saving the progression source instead and recalculating the maximum.

---

# Tool Save Data

Example:

```csharp
[Serializable]
public sealed class ToolSaveData
{
    public List<ToolEntrySaveData> tools = new();
    public string equippedToolId;
}
```

Entry:

```csharp
[Serializable]
public sealed class ToolEntrySaveData
{
    public string toolId;
    public int upgradeLevel;
    public bool unlocked;
}
```

Static Tool descriptions and icons remain in Tool definitions.

---

# Relationship Save Data

Relationship persistence should be organized by NPC ID.

Example:

```csharp
[Serializable]
public sealed class RelationshipSaveData
{
    public List<NpcRelationshipEntrySaveData> entries = new();
}
```

Entry:

```csharp
[Serializable]
public sealed class NpcRelationshipEntrySaveData
{
    public string npcId;

    public int friendshipPoints;
    public int connectionProgress;

    public bool hasBeenIntroduced;
    public bool isDating;
    public bool isMarried;
}
```

The exact fields should reflect the final Relationship Systems.

Do not save calculated display status if it can be reconstructed.

Example:

```text
Friendship Points
    ↓
Relationship Status
```

The status may be calculated instead of duplicated.

---

# Marriage Save Data

Marriage state may be contained inside Relationship Save Data or separated if the system becomes complex.

Possible values:

- Spouse NPC ID.
- Marriage date.
- Marriage state.
- Marriage-specific progression.
- Persistent spouse-related event completion.

Only one System should remain authoritative for marriage state.

---

# Quest Save Data

Example:

```csharp
[Serializable]
public sealed class QuestSaveData
{
    public List<QuestEntrySaveData> entries = new();
}
```

Entry:

```csharp
[Serializable]
public sealed class QuestEntrySaveData
{
    public string questId;
    public int state;

    public List<QuestObjectiveSaveData> objectives = new();
}
```

Objective:

```csharp
[Serializable]
public sealed class QuestObjectiveSaveData
{
    public string objectiveId;
    public int progress;
    public bool completed;
}
```

Static Quest text, descriptions, rewards, and requirements remain in `QuestDefinition`.

---

# Quest Persistence Rules

A Quest save entry should exist when persistent state needs to be remembered.

Possible persistent states:

- Active.
- Completed.
- Failed.
- Abandoned when relevant.
- Objective progress.
- Reward claimed.
- One-time Quest seen.

Locked Quests do not necessarily require individual save entries if their availability can be reconstructed from other progression.

---

# Game Event Save Data

Game Events may require persistence when they can only occur once or have progression stages.

Example:

```csharp
[Serializable]
public sealed class GameEventEntrySaveData
{
    public string eventId;
    public int state;
    public bool completed;
}
```

Examples:

- NPC heart events.
- Story events.
- Introduction events.
- Restoration cinematics.
- One-time world events.

Static event definitions remain outside the save file.

---

# Restoration Save Data

Example:

```csharp
[Serializable]
public sealed class RestorationSaveData
{
    public List<RestorationEntrySaveData> entries = new();
}
```

Entry:

```csharp
[Serializable]
public sealed class RestorationEntrySaveData
{
    public string targetId;
    public int restorationState;
    public int progress;
}
```

Possible targets:

- Pendrelle Manor rooms.
- Town buildings.
- Public areas.
- Special restoration projects.

If Restoration State is fully derived from progress, save the authoritative progress and calculate the state.

---

# Farming Save Data

Farming Save Data may become one of the larger persistent structures.

Possible structure:

```csharp
[Serializable]
public sealed class FarmingSaveData
{
    public List<FarmTileSaveData> tiles = new();
}
```

Tile:

```csharp
[Serializable]
public sealed class FarmTileSaveData
{
    public string tileId;

    public bool tilled;
    public bool watered;

    public string cropId;
    public int growthStage;
    public int daysInStage;

    public int plantedYear;
    public int plantedSeason;
    public int plantedDay;
}
```

The exact data should be selected based on the final Crop growth algorithm.

Avoid saving both:

```text
growthStage
```

and several values that deterministically produce the same `growthStage` unless they are independently required.

---

# Persistent Farm Tile IDs

Farm tiles that require persistent state should have stable identifiers.

Example:

```text
farm_pendrelle_field_a_001
farm_pendrelle_field_a_002
```

Alternative:

Use stable grid coordinates relative to a known farm region.

Example:

```csharp
public int gridX;
public int gridY;
```

Whichever approach is selected must remain stable across save versions.

---

# World Save Data

World Save Data stores persistent changes to world entities.

Examples:

- Resource nodes.
- Opened treasure objects.
- Cleared obstacles.
- Permanent switches.
- Destroyed persistent objects.
- Unlocked shortcuts.
- Discovered areas.

Example:

```csharp
[Serializable]
public sealed class WorldSaveData
{
    public List<WorldObjectSaveData> objects = new();
}
```

Entry:

```csharp
[Serializable]
public sealed class WorldObjectSaveData
{
    public string objectId;
    public int state;
}
```

World object IDs must remain stable.

---

# Resource Respawn Save Data

Do not save every temporary resource node if the state can be reconstructed from:

- Last gathered date.
- Respawn rules.
- Deterministic generation state.

Example:

```csharp
[Serializable]
public sealed class ResourceNodeSaveData
{
    public string resourceNodeId;

    public bool available;

    public int lastGatheredYear;
    public int lastGatheredSeason;
    public int lastGatheredDay;
}
```

The exact representation depends on the final respawn architecture.

---

# NPC World State

NPC routine locations generally should not need to be permanently saved if they are deterministic from:

- Current date.
- Current time.
- Weather.
- Festival state.
- Quest state.
- Relationship state.
- Routine definitions.

However, save state may be necessary for:

- Temporary scripted event state.
- NPC-specific persistent absence.
- Travel.
- Story changes.
- Marriage residence changes.
- Exceptional state not derivable from normal systems.

Avoid saving every NPC's position every few seconds unless the design specifically requires exact positional persistence.

---

# Mail Save Data

Example:

```csharp
[Serializable]
public sealed class MailSaveData
{
    public List<MailEntrySaveData> entries = new();
}
```

Entry:

```csharp
[Serializable]
public sealed class MailEntrySaveData
{
    public string mailId;

    public bool received;
    public bool read;
    public bool attachmentClaimed;
}
```

Static letter text remains in Mail definitions.

---

# Invention Save Data

Example:

```csharp
[Serializable]
public sealed class InventionSaveData
{
    public List<InventionEntrySaveData> entries = new();
}
```

Entry:

```csharp
[Serializable]
public sealed class InventionEntrySaveData
{
    public string inventionId;

    public bool unlocked;
    public bool built;
}
```

Additional progress may be added if an Invention can be partially completed.

---

# Recipe and Unlock Save Data

Persistent unlocks may be stored by stable IDs.

Example:

```csharp
[Serializable]
public sealed class UnlockSaveData
{
    public List<string> unlockedRecipeIds = new();
    public List<string> unlockedInventionIds = new();
    public List<string> unlockedAreaIds = new();
}
```

Avoid saving static lists of every possible locked item.

Save only meaningful persistent changes when possible.

---

# Ledger Save Data

Ledger progression should store persistent discoveries and counts required for completion tracking.

Possible values:

- Item discovered.
- Item sold.
- Fish caught.
- Crop harvested.
- Recipe completed.
- Invention built.
- Required collection count.

Example:

```csharp
[Serializable]
public sealed class LedgerEntrySaveData
{
    public string entryId;

    public bool discovered;
    public int count;
}
```

Only save values needed to reconstruct Ledger completion.

---

# Festival Save Data

Festival Save Data may include:

- Festivals attended.
- One-time Festival rewards claimed.
- Festival scores.
- Festival progression.
- Competition results.
- Persistent Festival-specific unlocks.

Do not save the static Festival schedule itself.

---

# Game Flags Save Data

Example:

```csharp
[Serializable]
public sealed class GameFlagSaveData
{
    public List<GameFlagEntrySaveData> flags = new();
}
```

Entry:

```csharp
[Serializable]
public sealed class GameFlagEntrySaveData
{
    public string flagId;
    public bool value;
}
```

If future Flags require multiple value types, use explicitly separated structures.

Example:

```text
Bool Flags
Int Flags
String Flags
```

rather than storing all values as arbitrary strings.

Related Notes:

- Game Flags

---

# Settings Save Data

Not all settings belong inside a game save.

## Account / Application Settings

Examples:

- Master volume.
- Music volume.
- SFX volume.
- Display resolution.
- Fullscreen mode.
- Accessibility.
- Controller vibration.
- Input bindings.

These should generally be stored separately from individual save slots.

## Save-Specific Settings

Examples might include:

- Difficulty options that affect this playthrough.
- Player-specific gameplay preferences intentionally tied to the save.

Only save-slot-specific settings belong inside `GameSaveData`.

---

# Input Bindings

Input bindings should generally not be stored inside each game save.

They are application-level player preferences.

This allows the same bindings to apply across multiple save files.

---

# Save Slots

Each save slot should have its own independent `GameSaveData`.

Example:

```text
Save Slot 1
    ↓
GameSaveData

Save Slot 2
    ↓
GameSaveData

Save Slot 3
    ↓
GameSaveData
```

Save slot management should be handled by Save infrastructure rather than by gameplay Systems.

---

# Save IDs

Every created save may have a globally unique or sufficiently unique Save ID.

Example:

```csharp
public string saveId;
```

The Save ID is separate from:

- Save slot number.
- Player Name.
- File name.

This allows save metadata and backups to identify the playthrough reliably.

---

# New Game Data

Starting a new game should not require loading a fake save file.

Preferred:

```text
New Game Requested
      ↓
Create default runtime state
      ↓
Apply selected character creation data
      ↓
Begin Intro
```

A Save Data snapshot can then be created whenever the first save occurs.

Alternatively, a clean initial `GameSaveData` may be generated and passed through the same restore pipeline if this simplifies implementation.

Whichever approach is used should produce the same valid starting state.

---

# Default Values

Every Save Data structure should have safe defaults.

Example:

```csharp
public List<InventoryEntrySaveData> entries = new();
```

rather than:

```csharp
public List<InventoryEntrySaveData> entries;
```

when null offers no useful semantic meaning.

Defaults help:

- New games.
- Older save migrations.
- Missing optional fields.
- Corruption recovery.
- Testing.

---

# Optional Data

Newer versions of the game may contain Save Data sections that older saves do not.

Example:

Version 1:

```text
No Ledger Data
```

Version 2:

```text
Ledger Data added
```

Migration may create:

```csharp
save.ledger = new LedgerSaveData();
```

with defaults derived from existing progression where possible.

---

# Serialization

Save Data should be designed around the selected serializer.

Possible serialization formats include:

- JSON.
- Binary.
- Custom format.

The architecture should not depend heavily on one serializer unless intentionally chosen.

Save Data should avoid unsupported runtime structures such as:

- Delegates.
- Interfaces without explicit serialization support.
- Unity scene references.
- Event listeners.
- Complex circular object graphs.

---

# Serialization DTO Style

Save Data classes may intentionally use simple public fields.

Example:

```csharp
[Serializable]
public sealed class InventoryEntrySaveData
{
    public string itemId;
    public int quantity;
    public int quality;
}
```

This is acceptable because Save Data is a serialization structure rather than an authoritative runtime object.

Runtime Models should generally maintain stronger encapsulation.

---

# Enum Persistence

Enums should be handled carefully.

Example runtime enum:

```csharp
public enum ItemQuality
{
    Base,
    Copper,
    Silver,
    Gold,
    Cobalt
}
```

Saving the integer value is compact:

```csharp
public int quality;
```

but changing enum order can break old saves.

If integer persistence is used:

- Explicit enum numeric values should be assigned.
- Existing numeric values should never be reused for different meanings.

Example:

```csharp
public enum ItemQuality
{
    Base = 0,
    Copper = 1,
    Silver = 2,
    Gold = 3,
    Cobalt = 4
}
```

Alternatively, string persistence may be used when appropriate.

---

# Date Persistence

Real-world timestamps should use an unambiguous format.

Preferred:

```text
UTC
```

Example:

```text
2026-08-19T16:30:00Z
```

Game Calendar dates should use game-specific numeric fields rather than formatted strings.

---

# Playtime

Playtime should generally be stored as a numeric duration.

Example:

```csharp
public int playtimeSeconds;
```

UI may convert this to:

```text
48h 13m
```

for display.

Do not store only the formatted display string.

---

# Validation

Loaded Save Data should be validated before entering normal gameplay.

Validation examples:

- Save Version is recognized.
- Required top-level structures exist.
- Quantities are non-negative.
- Current Stamina does not exceed valid limits.
- Stable IDs resolve where required.
- Calendar values are valid.
- Inventory quantities respect supported ranges.
- Relationship points are valid.
- Quest states are recognized.
- World object states are recognized.

Validation should distinguish between:

- Recoverable problems.
- Migratable problems.
- Unrecoverable corruption.

---

# Missing Content IDs

A save may contain an ID no longer available in current content.

Example:

```text
item_removed_content
```

Possible strategies:

- Map the ID during migration.
- Remove the entry when safe.
- Replace with a fallback.
- Preserve unknown data where forward/backward compatibility requires it.
- Reject the save when continued loading would be unsafe.

The strategy should depend on the content type.

A missing cosmetic item may be recoverable.

A missing core Quest definition may require more careful migration.

---

# Corruption Handling

The Save System should fail safely when data cannot be loaded.

Possible behavior:

1. Detect invalid or unreadable save.
2. Do not overwrite it.
3. Attempt backup recovery if available.
4. Report a clear error to the player.
5. Preserve the corrupted file for debugging or recovery where appropriate.

Never automatically replace a corrupted save with a blank game without informing the player.

---

# Backup Saves

Recommended backup strategy:

```text
save_01.json
save_01.backup.json
```

When writing a new save:

1. Validate the existing save if available.
2. Preserve the previous valid save as backup.
3. Write the new temporary save.
4. Validate the new file.
5. Promote the new file to primary.

The exact platform implementation may vary.

---

# Autosave

Autosave should use the same Save Data architecture as manual saving.

Examples of appropriate autosave points:

- End of day.
- Major progression checkpoints.
- Safe scene transitions if desired.

The game should avoid saving halfway through operations that cannot be safely reconstructed.

Example:

Avoid autosaving between:

```text
Currency removed
```

and:

```text
Purchased Item added
```

Treat transactions atomically at the gameplay level before saving.

---

# Manual Save

If manual saving is supported, it should only occur while Systems are in a consistent state.

The Save System may determine:

```csharp
bool CanSave();
```

Potential save restrictions:

- During scene loading.
- During a cinematic.
- During a transactional operation.
- During minigame resolution.
- During Day End processing.
- During another Save or Load operation.

These restrictions should be explicit rather than accidental.

---

# Day End Saving

The normal Day End autosave should occur after all persistent Day End changes are complete.

Recommended sequence:

```text
Day End begins
    ↓
Process selling
    ↓
Process progression
    ↓
Advance Calendar
    ↓
Advance Weather
    ↓
Reset daily Systems
    ↓
Prepare new-day state
    ↓
Autosave
    ↓
Begin new day
```

The exact sequence should match the final Initialization and Day End architecture.

The save should represent a coherent point in time.

---

# Event Channels and Saving

Event Channels should not be serialized.

Do not save:

- Event listeners.
- Event subscriptions.
- Event invocation state.

After loading, runtime objects subscribe again through their normal initialization lifecycle.

Possible Save-related Event Channels:

```text
SaveStarted
SaveCompleted
SaveFailed
LoadStarted
LoadCompleted
LoadFailed
```

These are runtime notifications only.

---

# Saving Event-Driven State

Do not assume an Event can reconstruct persistent state.

Example:

Do not rely on:

```text
QuestCompleted event happened previously
```

to know that a Quest is completed after loading.

Persist:

```text
Quest State = Completed
```

Events describe runtime occurrences.

Save Data stores persistent facts.

---

# Runtime-Only State

Some state should intentionally not be saved.

Examples:

- UI scroll position.
- Open menu tab.
- Hovered Item.
- Current animation frame.
- Temporary Audio cue.
- Current particle effect.
- Controller rumble.
- Temporary Event listeners.
- Cached presentation Models.
- Pathfinding cache.
- Temporary navigation path.
- Calculated UI strings.

These should be reconstructed.

---

# Conditional Runtime State

Some temporary gameplay state may or may not require persistence depending on save rules.

Examples:

- Active minigame.
- Active dialogue.
- Active cinematic.
- Half-completed interaction.
- Current NPC navigation route.

If saving is prohibited during these states, they do not need persistence.

This is often preferable to building complex serialization for transient operations.

---

# Save Boundaries

Before adding a field to Save Data, ask:

"If this field were missing after loading, could the game correctly reconstruct it?"

If yes:

It may not need to be saved.

If no:

Determine whether it represents:

- Authoritative persistent state.
- Historical outcome.
- Player choice.
- Progression.
- World change.

If so, it likely belongs in Save Data.

---

# Save Data Granularity

Avoid one field per tiny temporary fact when a clearer authoritative value exists.

Bad:

```text
cropWasPlanted
cropHasSprouted
cropHasSecondStage
cropHasThirdStage
cropReady
```

Preferred:

```csharp
public int growthStage;
```

when those states are mutually exclusive.

Likewise avoid excessively generic storage such as:

```csharp
Dictionary<string, string>
```

for all game state.

Typed Save Data is easier to:

- Validate.
- Migrate.
- Debug.
- Refactor.
- Document.

---

# Dictionaries

Dictionaries may not serialize cleanly with every Unity serialization approach.

Where portability matters, prefer serializable lists.

Instead of:

```csharp
Dictionary<string, int> friendship;
```

use:

```csharp
public List<FriendshipEntrySaveData> entries;
```

Runtime Systems may rebuild a dictionary after loading.

Example:

```text
Serialized List
    ↓
Load
    ↓
Runtime Dictionary
```

---

# Runtime Lookup Reconstruction

Save Data should not duplicate runtime optimization structures.

Example:

Inventory runtime may maintain:

```csharp
Dictionary<string, InventoryEntryModel>
```

for fast lookup.

The save can contain:

```csharp
List<InventoryEntrySaveData>
```

After load:

```text
Save List
    ↓
Restore
    ↓
Rebuild Dictionary
```

---

# Save Data Factories

A dedicated factory may create a complete Save Data snapshot.

Example:

```csharp
public sealed class GameSaveDataFactory
{
    public GameSaveData Create(
        ITimeSystem timeSystem,
        ICalendarSystem calendarSystem,
        IInventorySystem inventorySystem)
    {
        return new GameSaveData
        {
            time = timeSystem.CreateSaveData(),
            calendar = calendarSystem.CreateSaveData(),
            inventory = inventorySystem.CreateSaveData()
        };
    }
}
```

This can keep the Save System focused on persistence operations rather than knowing every conversion detail.

Whether a factory is needed depends on implementation complexity.

---

# Save System Responsibilities

The Save System should generally be responsible for:

- Selecting save paths.
- Creating save snapshots.
- Coordinating System export.
- Serialization.
- File writing.
- File reading.
- Save slot management.
- Metadata.
- Validation orchestration.
- Migration orchestration.
- Backup handling.
- Save/load state.
- Save/load notifications.

The Save System should not own the gameplay state being persisted.

---

# System Save Contracts

Persistent Systems may implement a common contract when useful.

Example:

```csharp
public interface ISaveParticipant<TSaveData>
{
    TSaveData CreateSaveData();
    void RestoreFromSave(TSaveData saveData);
}
```

Or separate contracts:

```csharp
public interface ISaveDataProvider<TSaveData>
{
    TSaveData CreateSaveData();
}

public interface ISaveDataRestorer<TSaveData>
{
    void RestoreFromSave(TSaveData saveData);
}
```

Do not force every System into a generic interface if it makes initialization or type safety less clear.

Explicit methods are acceptable.

---

# Save Registration

If Systems are registered dynamically as Save participants, registration must remain deterministic and testable.

Avoid architectures where important Save Data is silently omitted because a scene object failed to register.

Core persistent Systems should have explicit Save participation.

---

# Save Ordering

Most Systems should export independently.

Loading may require ordering when one System relies on another.

Example:

```text
Data Registries
    ↓
Calendar
    ↓
Weather
    ↓
World
    ↓
NPC routines
```

Detailed restoration order belongs in:

- Initialization Order

Save creation should avoid hidden dependency chains.

---

# No Gameplay Events During Partial Load

Avoid normal gameplay reactions while only part of the save has been restored.

Example problem:

```text
Inventory restores
    ↓
InventoryChanged raised
    ↓
Quest System reacts
    ↓
Quest System has not been restored yet
```

Preferred:

```text
Begin Load
    ↓
Suppress normal change notifications
    ↓
Restore Systems
    ↓
Finalize Load
    ↓
Refresh presentation
    ↓
Raise LoadCompleted
```

Systems may expose a specific silent restore API.

---

# Load Synchronization

After all Systems have restored:

- Recalculate derived values.
- Rebuild lookup collections.
- Resolve references.
- Synchronize scene state.
- Synchronize NPC routines.
- Refresh UI.
- Publish a high-level Load Completed event.

This establishes one clear boundary between:

```text
Restoration
```

and:

```text
Normal Gameplay
```

---

# Save Migration

Migration converts older Save Data into the current schema.

Example:

```text
Version 1
    ↓
Migrate to Version 2
    ↓
Migrate to Version 3
    ↓
Current Version
```

Avoid requiring one giant migration directly from every historic version to the newest version.

Incremental migrations are usually easier to reason about.

Related Notes:

- Save Versioning

---

# Migration Responsibilities

Migration may:

- Rename fields.
- Add missing structures.
- Convert old enums.
- Replace old IDs.
- Split one structure into several.
- Merge structures.
- Supply new default values.
- Recalculate newly introduced persistent values where required.

Migration should not:

- Arbitrarily change player progression.
- Re-run gameplay rewards.
- Trigger normal gameplay Events.
- Depend on scene objects.
- Require UI.

---

# Save Data Tests

Save architecture should be tested independently where practical.

Important tests include:

- New save serialization.
- Save → Load round trip.
- Empty Inventory.
- Full Inventory.
- Relationship persistence.
- Quest objective persistence.
- Restoration persistence.
- Farm tile persistence.
- World object persistence.
- Mail persistence.
- Game Flags.
- Enum stability.
- Missing optional fields.
- Old save migration.
- Invalid ID handling.
- Corrupted file handling.
- Backup recovery.
- Save overwrite safety.
- Multiple save slots.
- Application restart simulation.

---

# Round-Trip Testing

A round-trip test verifies:

```text
Runtime State A
    ↓
Save
    ↓
Serialize
    ↓
Deserialize
    ↓
Restore
    ↓
Runtime State B
```

Expected:

```text
Persistent State A == Persistent State B
```

Runtime-only caches do not need to match as long as they are correctly reconstructed.

---

# Debugging Save Data

JSON or another human-readable development format may be useful during development.

Benefits:

- Inspect IDs.
- Inspect progression.
- Compare save versions.
- Diagnose migration issues.
- Verify missing fields.
- Reproduce bugs.

Production format may remain the same or change depending on performance, security, platform, and compatibility requirements.

---

# Security and Tampering

Save architecture should not treat local save files as secure.

Players may modify local files.

For a primarily single-player game, priorities should generally be:

- Reliability.
- Compatibility.
- Corruption resistance.
- Debuggability.

Anti-tamper measures should not make save recovery or migration significantly more fragile unless there is a strong design reason.

---

# Platform Storage

The Save System should abstract platform-specific storage behavior.

Possible platforms include:

- Windows.
- macOS.
- Nintendo Switch.
- Nintendo Switch 2.
- Xbox.
- PlayStation.

Gameplay Systems should not know platform file paths.

Preferred:

```text
Gameplay Systems
      ↓
Save System
      ↓
Storage Service
      ↓
Platform storage
```

---

# Cloud Saves

If cloud saves are supported later, the runtime Save Data structure should remain independent of the cloud provider.

Potential flow:

```text
GameSaveData
    ↓
Serialization
    ↓
Local Save
    ↓
Cloud Sync Service
```

Conflict resolution belongs to Save/Platform infrastructure rather than individual gameplay Systems.

---

# Save File Naming

File names should not depend solely on Player Name.

Example:

```text
save_01.json
save_02.json
save_03.json
```

or:

```text
save_<saveId>.json
```

Player Name may contain:

- Spaces.
- Unicode.
- Invalid file-name characters.
- Duplicate names.

Save ID or slot ID is safer.

---

# Recommended Folder Structure

```text
Save/
|
|-- Systems/
|   |-- SaveSystem.cs
|
|-- Data/
|   |-- GameSaveData.cs
|   |-- SaveMetadataData.cs
|   |-- PlayerSaveData.cs
|   |-- TimeSaveData.cs
|   |-- CalendarSaveData.cs
|   |-- WeatherSaveData.cs
|   |-- InventorySaveData.cs
|   |-- RelationshipSaveData.cs
|   |-- QuestSaveData.cs
|   |-- RestorationSaveData.cs
|   |-- FarmingSaveData.cs
|   |-- WorldSaveData.cs
|   |-- MailSaveData.cs
|   |-- InventionSaveData.cs
|   |-- LedgerSaveData.cs
|   |-- FestivalSaveData.cs
|   |-- GameFlagSaveData.cs
|
|-- Serialization/
|   |-- ISaveSerializer.cs
|   |-- JsonSaveSerializer.cs
|
|-- Storage/
|   |-- ISaveStorageService.cs
|
|-- Migration/
|   |-- ISaveMigration.cs
|   |-- SaveMigrationV1ToV2.cs
|
|-- Validation/
|   |-- SaveValidator.cs
|
|-- Backup/
|   |-- SaveBackupService.cs
```

The final organization may be simplified if some categories remain small.

---

# Recommended Save Data Naming

Use:

```text
<Feature>SaveData
```

for serialized structures.

Examples:

- `GameSaveData`
- `InventorySaveData`
- `InventoryEntrySaveData`
- `QuestSaveData`
- `QuestEntrySaveData`
- `CropSaveData`
- `NpcRelationshipSaveData`

Avoid calling runtime Models:

```text
SaveData
```

when they are not actually serialization structures.

---

# Example Save Snapshot

A simplified save may resemble:

```json
{
  "metadata": {
    "saveVersion": 1,
    "saveId": "save-example",
    "playerName": "Carey",
    "createdUtc": "2026-08-19T16:00:00Z",
    "lastSavedUtc": "2026-08-19T16:30:00Z",
    "playtimeSeconds": 4200
  },
  "calendar": {
    "weekday": 0,
    "day": 1,
    "season": 0,
    "year": 1
  },
  "time": {
    "hour": 6,
    "minute": 0
  },
  "economy": {
    "bells": 500
  },
  "inventory": {
    "entries": [
      {
        "itemId": "item_herb_peppermint",
        "quantity": 3,
        "quality": 0
      }
    ]
  }
}
```

This is illustrative only.

The final schema should be based on actual implemented Systems.

---

# What Should Be Saved?

## Save

Save when the value represents persistent player or world state.

Examples:

- Player Name.
- Current game date.
- Current game time.
- Bells.
- Inventory.
- Friendship points.
- Quest state.
- Restoration progress.
- Crop state.
- Unlocked recipes.
- Built inventions.
- Mail read state.
- Persistent Game Flags.

---

# What Should Usually Not Be Saved?

Do not save information that is purely presentation or can safely be reconstructed.

Examples:

- HUD text.
- Date display string.
- Current menu page.
- Animation state.
- UI selections.
- Item icons.
- NPC portraits.
- Static dialogue text.
- Static Quest descriptions.
- Item descriptions.
- Relationship status if derived entirely from Friendship points.
- Daylight state if derived entirely from date and time.
- Static Shop inventory definitions.

---

# Save Decision Checklist

Before adding a field to Save Data, determine:

1. Is this value persistent?
2. Which Runtime System owns it?
3. Is it authoritative or derived?
4. Can it be reconstructed after loading?
5. Does it reference static content?
6. Can that content be referenced by a stable ID?
7. Does the value belong to this save slot or application settings?
8. Does it need exact historical preservation?
9. Will changing this structure require migration?
10. Is the field safe to default when loading an older save?
11. Does it require validation?
12. Is this structure serializable by the chosen serializer?
13. Does it contain any Unity scene references?
14. Is it duplicating data already saved elsewhere?
15. Does it need a stable world object ID?
16. Is this temporary state better handled by preventing saving during that operation?
17. Can the owning System export and restore it cleanly?
18. Will this field remain understandable when debugging a save months later?

---

# Anti-Patterns

## Save Data as Runtime State

Avoid:

```csharp
gameSaveData.inventory.entries.Add(...);
```

during normal gameplay.

Preferred:

```csharp
inventorySystem.AddItem(...);
```

Then create Save Data when saving.

---

## Controllers Editing Save Data

Avoid:

```csharp
inventoryUiController.SaveData.inventory...
```

Controllers should interact with gameplay Systems.

---

## Saving ScriptableObjects Directly

Avoid relying on direct asset references for persistent identity.

Prefer stable IDs.

---

## Saving Display Names

Avoid:

```text
"Adrian"
```

as the NPC identifier.

Prefer:

```text
npc_lockwood_adrian
```

---

## Saving Derived State Everywhere

Avoid storing:

```text
Friendship Points
Heart Level
Relationship Status
Heart Progress
```

when the last three can all be derived from Friendship Points.

---

## Giant Generic Dictionary

Avoid:

```csharp
Dictionary<string, object> allGameState;
```

Typed structures provide better validation and migration.

---

## Scene Instance References

Never persist:

- Runtime Instance IDs.
- GameObject references.
- Transform references.

Use stable Data IDs or world IDs.

---

## Silent ID Changes

Never rename shipped IDs without migration support.

---

## Saving Event History Instead of State

Do not assume replaying Events can reconstruct the game.

Persist the resulting state.

---

## Partial Transaction Saves

Avoid saving halfway through a required multi-System transaction.

The gameplay operation should finish before persistence occurs.

---

## No Save Version

Never release save files without schema version information.

---

## Destructive Failed Load

A failed load should never automatically overwrite the file that failed to load.

---

# Save Data Rules

- Runtime Systems remain authoritative while gameplay is active.
- Save Data is a serialized snapshot of persistent runtime state.
- Every Save file must contain a Save Version.
- Use stable Data IDs for persistent content references.
- Keep ScriptableObject definitions outside Save Data.
- Save authoritative values rather than unnecessary derived values.
- Keep Runtime Models and Save Data conceptually separate.
- Let owning Systems export and restore their state.
- Do not allow UI or Controllers to directly mutate Save Data.
- Do not serialize Event Channels or listeners.
- Do not serialize scene GameObjects or runtime Unity references.
- Use typed Save Data structures rather than one generic state dictionary.
- Provide safe defaults for new fields.
- Validate loaded data.
- Migrate older save versions.
- Preserve old IDs through migration when content changes.
- Rebuild runtime-only caches after loading.
- Suppress normal gameplay reactions while the save is only partially restored.
- Perform one final synchronization after all Systems are restored.
- Use safe write behavior to reduce corruption risk.
- Keep backup support where practical.
- Keep application settings separate from save-slot gameplay data.
- Keep platform storage details outside gameplay Systems.
- Test Save → Load round trips for all persistent Systems.
- Treat shipped Save Data as a long-term compatibility contract.

---

# Related Code Setup Notes

- Data IDs
- Dependencies
- Enums
- Event Channels
- Game Architecture
- Game Flags
- Initialization Order
- Models
- Save Versioning
- Scriptable Objects
- Services

---

# Related System Notes

- Save System
- System Interaction Rules
- Day End System
- Game State System
- Individual persistent System documentation
