---
Title: Code Setup / Save Versioning
Game: As The Bell Tolls
Owner: Carey Clement Jr
Created: August, 2026
Updated: August, 2026
Version: 0.0.1
---

## Information

- Save Versioning protects existing player saves when the Save Data schema changes between game versions.
- Every gameplay Save file should contain an explicit Save Format Version.
- Save Format Version is separate from the public Game Version.
- Save migration should transform older Save Data into the current Save Data structure before normal gameplay restoration begins.
- Migration should be sequential, deterministic, testable, and non-destructive.
- Existing Save files should never be overwritten until migration and validation succeed.
- Stable Data IDs are a major part of Save compatibility.
- Changes to ScriptableObject asset names should not require migration when stable Data IDs remain unchanged.
- Changes to persistent Data IDs may require explicit migration.
- New fields should receive intentional defaults when loading older saves.
- Removed or restructured fields should be handled by migration rather than scattered compatibility checks throughout gameplay Systems.
- The runtime game should ideally operate only on the current Save Data schema after migration completes.

---

# Purpose

Save Versioning answers:

- Which Save Data schema created this file?
- Can the current game load this Save?
- Does this Save need migration?
- Which migrations must run?
- What defaults should newly introduced fields receive?
- What happens when a persistent Data ID changes?
- How should removed content be handled?
- How should corrupted or unsupported Saves be treated?
- How can old Saves remain compatible after game updates?

The goal is:

```text
Old Save
    ↓
Version Detection
    ↓
Migration
    ↓
Validation
    ↓
Current Save Data
    ↓
Runtime Restoration
```

---

# Core Principle

Gameplay Systems should not contain years of old Save compatibility logic.

Instead:

```text
Old Save Format
    ↓
Migration Pipeline
    ↓
Current Save Format
    ↓
Gameplay Systems
```

Once migration succeeds, the rest of the game should work with the current schema.

---

# Save Format Version

Every Save should contain a dedicated integer Save Format Version.

Example:

```csharp
[Serializable]
public sealed class GameSaveData
{
    public int saveFormatVersion;

    public SaveMetadataData metadata;

    public PlayerSaveData player;
    public InventorySaveData inventory;
    public EconomySaveData economy;
    public CalendarSaveData calendar;
    public RelationshipSaveData relationships;
    public QuestSaveData quests;
    public WorldSaveData world;
}
```

Example serialized value:

```text
saveFormatVersion = 4
```

---

# Current Save Format Version

The game should define one authoritative current Save Format Version.

Example:

```csharp
public static class SaveVersion
{
    public const int Current = 4;
}
```

Do not scatter:

```csharp
4
```

through migration and Save code.

Use the central constant.

---

# Save Format Version vs Game Version

These are different concepts.

## Game Version

Examples:

```text
0.0.1
0.5.0
1.0.0
1.1.2
```

Game Version describes the released game build.

## Save Format Version

Examples:

```text
1
2
3
4
```

Save Format Version describes the persistent Save Data schema.

A game update does not automatically require a Save Format increment.

Example:

```text
Game 1.0.0
Save Format 12
```

A bug-fix release:

```text
Game 1.0.1
Save Format 12
```

If no persistent schema changed, the Save Format remains `12`.

---

# Why Use an Integer

Save Format Versions should normally use sequential integers.

Recommended:

```text
1
2
3
4
5
```

Avoid:

```text
0.1.4
1.2.7
```

for the schema version itself.

Sequential integers make migration ordering simple and explicit.

---

# Initial Version

The first implementation may begin with:

```csharp
public const int Current = 1;
```

Even during development, introducing Save Versioning early is useful.

It establishes the migration architecture before persistent test Saves become important.

---

# When to Increment the Save Format Version

Increment the Save Format Version when an existing Save may require transformation to load correctly.

Examples:

- A persistent field is renamed.
- A persistent field changes type.
- A nested Save structure changes.
- Data moves between Save objects.
- One field is split into several fields.
- Several fields are merged.
- A persistent Data ID changes.
- Saved enum representation changes incompatibly.
- A new required field cannot safely use automatic default behavior.
- World-state representation changes.
- Inventory representation changes.
- Relationship progression representation changes.
- Quest objective persistence changes.
- Restoration state persistence changes.

---

# When Not to Increment

A Save Format increment is generally unnecessary when:

- UI changes.
- Art changes.
- Audio changes.
- A ScriptableObject asset file is renamed but its stable Data ID remains unchanged.
- A description changes.
- Dialogue text changes.
- A non-persistent runtime Model changes.
- A Controller changes.
- An Event Channel changes.
- A calculation changes but stored data remains compatible.
- A new optional Save field can safely default without transformation.
- Internal code is refactored without changing serialized Save structure.

Use judgment.

The question is:

```text
Does an existing Save require intentional conversion?
```

---

# Save Metadata

Save metadata may include both Save Format Version and Game Version.

Example:

```csharp
[Serializable]
public sealed class SaveMetadataData
{
    public string gameVersion;
    public long createdUtcTicks;
    public long lastSavedUtcTicks;
    public string saveSlotId;
}
```

Top-level Save:

```csharp
[Serializable]
public sealed class GameSaveData
{
    public int saveFormatVersion;
    public SaveMetadataData metadata;
}
```

This allows diagnostics such as:

```text
Save Format: 8
Created By Game Version: 0.7.2
Last Saved By Game Version: 1.0.1
```

---

# Migration Direction

Migrations should normally move forward only.

Example:

```text
V1
 ↓
V2
 ↓
V3
 ↓
V4
```

The current game loads an old Save by applying every required forward migration.

Downgrading Saves is generally not supported.

---

# Sequential Migration

Prefer sequential migrations.

If a Save is Version 2 and the current format is Version 5:

```text
V2 → V3
V3 → V4
V4 → V5
```

Do not create every possible direct path:

```text
V1 → V5
V2 → V5
V3 → V5
V4 → V5
```

Sequential migration keeps each transformation small and understandable.

---

# Migration Interface

A migration may use an interface such as:

```csharp
public interface ISaveMigration
{
    int FromVersion { get; }

    int ToVersion { get; }

    GameSaveData Migrate(
        GameSaveData saveData);
}
```

However, strongly typed old schemas may require a different migration design.

The exact serialization strategy should determine the final implementation.

---

# Migration Naming

Recommended naming:

```text
SaveMigrationV1ToV2
SaveMigrationV2ToV3
SaveMigrationV3ToV4
```

This makes the migration path obvious.

Avoid vague names such as:

```text
OldSaveFix
MigrationThing
SavePatch
```

---

# Migration Pipeline

A migration pipeline coordinates migration order.

Example conceptual flow:

```csharp
public sealed class SaveMigrationService
{
    private readonly IReadOnlyDictionary<int, ISaveMigration>
        _migrations;

    public GameSaveData MigrateToCurrent(
        GameSaveData saveData)
    {
        while (
            saveData.saveFormatVersion <
            SaveVersion.Current)
        {
            if (!_migrations.TryGetValue(
                saveData.saveFormatVersion,
                out ISaveMigration migration))
            {
                throw new InvalidOperationException(
                    $"No migration exists from Save Format " +
                    $"{saveData.saveFormatVersion}.");
            }

            saveData = migration.Migrate(saveData);

            if (
                saveData.saveFormatVersion !=
                migration.ToVersion)
            {
                throw new InvalidOperationException(
                    "Migration did not produce expected version.");
            }
        }

        return saveData;
    }
}
```

The final implementation may vary based on serialization requirements.

---

# Migration Registration

Migrations should be explicitly registered.

Example:

```text
1 → SaveMigrationV1ToV2
2 → SaveMigrationV2ToV3
3 → SaveMigrationV3ToV4
```

Validation should ensure:

- No duplicate source versions.
- No missing migration in the supported chain.
- Each migration advances exactly as intended.
- No migration points backward.
- Current version has no required outgoing migration.

---

# Migration Service Responsibility

The Save Migration Service may:

- Determine whether migration is required.
- Locate the next migration.
- Apply migrations in sequence.
- Verify version advancement.
- Return the current Save representation.
- Produce migration diagnostics.

It should not:

- Restore gameplay Systems.
- Update UI.
- Decide Save slot presentation.
- Modify unrelated runtime state.
- Delete the original Save before success.

---

# Save Load Pipeline

Recommended high-level flow:

```text
Player selects Save
    ↓
Read raw Save
    ↓
Create safety copy / preserve original
    ↓
Read Save Format Version
    ↓
Reject unsupported future version
    ↓
Deserialize enough data for migration
    ↓
Run required migrations
    ↓
Validate migrated Save
    ↓
Create migrated serialized Save
    ↓
Safely write migrated Save
    ↓
Restore gameplay Systems
    ↓
Enter game
```

The exact order of deserialization and migration depends on the chosen serializer.

---

# Migration Before Runtime Restoration

Do not restore half of an old Save into gameplay Systems and then attempt to migrate the remainder.

Preferred:

```text
Serialized Save
    ↓
Migration
    ↓
Current Save Data
    ↓
Validation
    ↓
Runtime Restoration
```

This keeps gameplay Systems independent of historical schemas.

---

# Compatibility Layers

Avoid compatibility checks scattered through gameplay code.

Bad:

```csharp
if (saveVersion < 4)
{
    // old behavior
}
else
{
    // new behavior
}
```

inside:

- Inventory System.
- Quest System.
- Relationship System.
- Farming System.
- Restoration System.

Preferred:

```text
Migration Layer
    ↓
Current Save Data
    ↓
Current Gameplay Code
```

---

# Migration Atomicity

Migration should be treated as an atomic operation from the player's perspective.

Either:

```text
Migration succeeds completely
```

or:

```text
Original Save remains available
```

Never leave a Save partially migrated.

---

# Preserve Original Save

Before overwriting an older Save with a migrated version:

- Preserve the original.
- Complete all migration steps.
- Validate the result.
- Serialize the result.
- Write to a temporary file.
- Verify the write when appropriate.
- Replace the active Save atomically.
- Retain or rotate backup according to Save policy.

Related Notes:

- Save Data

---

# Migration Failure

If migration fails:

- Do not overwrite the original Save.
- Log the migration step that failed.
- Record the source version.
- Record the intended target version.
- Present a safe player-facing error.
- Preserve backup data.
- Avoid entering gameplay with partially converted state.

---

# Unsupported Future Saves

A Save may have:

```text
saveFormatVersion > SaveVersion.Current
```

This can happen if:

- The player used a newer game build.
- The game was downgraded.
- A cloud Save came from a newer version.

The older game should not attempt to load it normally.

Example:

```csharp
if (
    saveData.saveFormatVersion >
    SaveVersion.Current)
{
    return SaveLoadResult.UnsupportedFutureVersion;
}
```

Player-facing behavior should explain that the Save was created by a newer game version.

---

# Missing Save Version

Very early development Saves may exist without a version field.

If support is required, define one explicit legacy interpretation.

Example:

```text
Missing Version = Legacy V0
```

Then migrate:

```text
V0 → V1
```

Do not indefinitely guess the version from arbitrary fields.

Once the versioned format exists, every new Save should contain it.

---

# New Fields

Adding a field may or may not require migration.

Example:

Old:

```csharp
public int bells;
```

New:

```csharp
public int bells;
public int totalBellsEarned;
```

If:

```text
totalBellsEarned = 0
```

is an acceptable value for all old Saves, migration may be unnecessary depending on the serializer.

If the correct value must be derived from old data, migration is required.

---

# Intentional Defaults

Defaults for migrated Saves should be explicit.

Example:

```text
New field:
hasSeenMapTutorial

Old Save default:
true
```

Why `true`?

Because an established player should not suddenly receive a tutorial introduced in a later update.

This is different from a new game default:

```text
New Game:
false
```

Migration defaults and New Game defaults do not always match.

---

# New Game Defaults vs Migration Defaults

This distinction is important.

Example:

A new feature is introduced in Game Version 1.2:

```text
Town Bulletin Board Tutorial
```

For a new game:

```text
tutorialCompleted = false
```

For an existing Year 3 Save:

```text
tutorialCompleted = true
```

Migration should intentionally decide the old-save behavior.

---

# Renamed Fields

If serialized field names matter to the serializer, renaming may break compatibility.

Old:

```csharp
public int money;
```

New:

```csharp
public int bells;
```

Migration should convert:

```text
money → bells
```

Do not assume a code rename automatically preserves serialized data.

Unity-specific attributes such as `FormerlySerializedAs` may help with Unity asset serialization, but Save files should use an explicit migration strategy appropriate to the chosen serializer.

---

# Changed Field Types

Changing a persistent field type requires careful migration.

Old:

```csharp
public int friendshipLevel;
```

New:

```csharp
public int friendshipPoints;
```

Migration might define:

```text
Old Level 0 → 0 Points
Old Level 1 → 250 Points
Old Level 2 → 500 Points
...
```

The conversion must preserve intended player progression.

---

# Splitting Fields

Old:

```csharp
public int relationshipProgress;
```

New:

```csharp
public int friendshipPoints;
public int connectionStage;
```

Migration must define how the old value maps to both new values.

Document assumptions in the migration.

---

# Merging Fields

Old:

```csharp
public int friendshipPoints;
public int bonusFriendshipPoints;
```

New:

```csharp
public int friendshipPoints;
```

Migration:

```text
new friendshipPoints =
    old friendshipPoints +
    old bonusFriendshipPoints
```

subject to gameplay rules and limits.

---

# Collection Changes

Changing persistent collections may require migration.

Old:

```text
List<string> completedQuestIds
```

New:

```text
Dictionary<string, QuestSaveData>
```

Migration may create:

```text
QuestSaveData
- questId
- state = Completed
- objectives = Completed
- rewardClaimed = true
```

for every previously completed Quest.

---

# Enum Changes

Persistent enums require special care.

If serialized numerically:

```text
0
1
2
3
```

reordering enum members may corrupt meaning.

Prefer stable string IDs or explicit serialized values for persistence where practical.

If an enum is persisted numerically:

- Never casually reorder values.
- Assign explicit numeric values.
- Migrate when meanings change.

Example:

```csharp
public enum RestorationState
{
    Weathered = 0,
    Rebuilding = 1,
    Recovering = 2,
    Renewed = 3,
    Growing = 4,
    Prospering = 5,
    Flourishing = 6
}
```

---

# Enum Removal

If an old enum state is removed:

```text
Damaged
```

migration must map it to a current state.

Example:

```text
Damaged → Weathered
```

The mapping should be documented.

---

# Enum Addition

Adding a new enum value does not necessarily require migration.

If old Saves remain semantically valid, no transformation may be needed.

However, ensure default numeric values do not accidentally map to a newly introduced meaning.

---

# Stable Data IDs

Persistent references should use stable Data IDs.

Examples:

```text
npc_lockwood_adrian
item_herb_peppermint
building_town-hall
```

This reduces migration requirements when:

- Assets move.
- Asset files are renamed.
- Display Names change.
- Localization changes.

Related Notes:

- Data IDs
- Scriptable Objects

---

# Data ID Renaming

Once shipped, a Data ID should be considered persistent.

If it must change:

Old:

```text
npc_adrian
```

New:

```text
npc_lockwood_adrian
```

migration must convert every persistent reference.

Potential locations include:

- Relationship Save Data.
- Quest references.
- Dialogue history.
- Mail state.
- Festival state.
- World state.
- Ledger state.

Do not only update the most obvious field.

---

# ID Migration Map

ID changes may use explicit maps.

Example:

```csharp
private static readonly Dictionary<string, string>
    NpcIdMap = new()
{
    {
        "npc_adrian",
        "npc_lockwood_adrian"
    }
};
```

A migration should apply the map only to the relevant old version.

Do not keep permanent alias logic throughout gameplay unless backward compatibility with external data specifically requires it.

---

# Removed Content

A later update may remove:

- Item.
- Quest.
- NPC.
- Recipe.
- Invention.
- Building state.
- Dialogue entry.

Migration must decide what happens to old references.

Possible strategies:

- Remove the obsolete entry.
- Convert it to replacement content.
- Refund equivalent resources.
- Mark old progression as completed.
- Preserve historical completion without requiring the removed definition.
- Convert to a safe fallback.

The correct strategy depends on player impact.

---

# Removed Inventory Item

If an Item is removed from the game, migration might:

```text
Old Item
    ↓
Replacement Item
```

or:

```text
Old Item
    ↓
Equivalent Bells / Materials
```

Avoid silently deleting valuable player possessions unless there is no meaningful alternative.

---

# Removed Quest

If a completed Quest is removed:

- Historical completion may still need to remain represented.
- Unlocks granted by the Quest should not disappear.
- Rewards should not be granted again.

If an active Quest is removed:

- Determine whether to cancel it.
- Replace it.
- Auto-complete it.
- Refund consumed resources.

Document the decision.

---

# Changed Quest Objectives

Quest objective structure is particularly migration-sensitive.

Old:

```text
Collect 20 Wood
```

New:

```text
Collect 10 Wood
Repair 1 Support Beam
```

Possible migration:

- Completed old Quest → completed new Quest.
- Active old Quest with 20/20 → mark first objective complete and determine second intentionally.
- Active old Quest with 8/20 → map 8 progress to the new collection objective.

Avoid resetting progression without considering player effort.

---

# Relationship Migration

Relationship systems may evolve.

Example:

Old:

```text
Heart Level
```

New:

```text
Friendship Points
Relationship Status
Connection Progression
```

Migration should preserve:

- Earned relationship progress.
- Completed events.
- Unlocks.
- Marriage state.
- Gift history where relevant.

Do not infer critical state only from current heart level if older Save Data contains more precise information.

---

# Restoration Migration

If Restoration progression changes:

Old:

```text
0–100 percentage
```

New:

```text
Weathered
Rebuilding
Recovering
Renewed
Growing
Prospering
Flourishing
```

Migration should define explicit thresholds.

Example:

```text
0–14   → Weathered
15–29  → Rebuilding
30–44  → Recovering
45–59  → Renewed
60–74  → Growing
75–89  → Prospering
90–100 → Flourishing
```

Actual thresholds should match the final design.

---

# Calendar Migration

Calendar changes are high impact because many Systems may depend on:

- Year.
- Season.
- Day.
- Time.
- Weekday.

If calendar representation changes, migration must preserve the player's actual temporal position as closely as possible.

Do not recalculate historical dates using changed rules unless intentionally required.

---

# World State Migration

World state may include:

- Resource nodes.
- Interactables.
- Doors.
- Chests.
- Building state.
- Placed objects.
- Farm tiles.
- Unlocks.

When world identifiers change, migration should map stable object IDs.

Avoid depending on scene hierarchy paths for persistent identity.

---

# Placed Object Migration

Player-placed objects require particular care.

Migration may need to preserve:

- Definition ID.
- Position.
- Rotation.
- Placement area.
- Custom state.
- Storage contents.

If placement rules change, old valid placements should not be deleted automatically unless absolutely necessary.

A recovery strategy may move invalid objects into:

- Player storage.
- A recovery container.
- A safe default location.

---

# Inventory Migration

Inventory migration should preserve:

- Item IDs.
- Quantities.
- Quality.
- Tool state.
- Slot order when relevant.
- Storage contents.

If stack limits change, migration may need to split stacks.

Example:

Old:

```text
Stack = 999
```

New maximum:

```text
Stack = 99
```

Migration:

```text
99
99
99
...
```

with overflow placed safely rather than deleted.

---

# Currency Migration

If currency representation changes, preserve player value.

Example:

Old:

```text
int bells
```

New:

```text
long bells
```

may be straightforward.

If the economy is rescaled:

```text
Old 100 Bells
New 10 Bells
```

migration should intentionally convert balances rather than relying on new price calculations alone.

---

# Discovery / Ledger Migration

If the Ledger begins tracking a category that was not previously persisted, exact historical reconstruction may be impossible.

Possible strategies:

- Derive from existing Save state.
- Mark only currently provable discoveries.
- Grandfather established progression.
- Begin the new category empty.

The chosen strategy should prioritize fairness and consistency.

---

# Achievement Migration

Platform Achievements and internal Save progression should not be assumed to be identical.

If a migration establishes that a player already met an Achievement condition:

- The runtime Achievement System may reconcile it after load.
- Platform APIs should be invoked through the appropriate platform abstraction.
- Migration itself should primarily transform Save Data.

---

# Tutorial Migration

New tutorials should define behavior for existing Saves.

Possible rules:

```text
New Game
    ↓
Tutorial not completed

Existing Save beyond relevant progression
    ↓
Tutorial considered completed
```

This avoids forcing basic tutorials onto established players.

---

# Feature Unlock Migration

When a new feature is introduced, migration should determine whether established Saves receive it immediately.

Example:

```text
Feature normally unlocks after Town Hall = Recovering
```

Migration can inspect the old Save's Town Hall progress.

If requirement already satisfied:

```text
featureUnlocked = true
```

---

# Derived Data

Avoid persisting values that can safely be derived from authoritative persistent state.

This reduces migration surface.

Example:

Instead of persisting both:

```text
Friendship Points
Relationship Status
```

if Status is always deterministically derived from Points, consider persisting only Points.

However, persist both when they represent independently meaningful historical state.

Related Notes:

- Save Data

---

# Migration and Derived Data

When a new derived field is introduced, migration may not be necessary if runtime code can derive it from current authoritative data.

Do not migrate values merely to duplicate information unnecessarily.

---

# Migration Context

Some migrations may require static content lookup.

Example:

- Mapping old Item IDs.
- Validating Quest definitions.
- Converting content-dependent state.

A migration context may provide narrowly scoped dependencies.

Example:

```csharp
public sealed class SaveMigrationContext
{
    public IItemRegistry Items { get; }
    public INpcRegistry Npcs { get; }
    public IQuestRegistry Quests { get; }
}
```

Avoid giving migrations unrestricted access to live gameplay Systems.

---

# Migrations Must Not Depend on Runtime State

Migration should not depend on:

- Current active scene.
- Current Player GameObject.
- Current UI.
- Current NPC instances.
- Current Weather System state.
- Current session randomness.

Migration should operate on:

- Old Save Data.
- Static definitions.
- Explicit migration configuration.

This keeps it deterministic.

---

# Deterministic Migration

Given the same old Save and game content version, migration should produce the same result.

Avoid unseeded random decisions.

Bad:

```text
Removed Item
    ↓
Random replacement Item
```

Preferred:

```text
Removed Item
    ↓
Explicit replacement mapping
```

---

# Idempotency

A migration should not be accidentally applied twice.

Version checks prevent this.

Example:

```text
V3 → V4
```

must only accept V3.

After success:

```text
saveFormatVersion = 4
```

If migration is retried from the untouched original V3 Save, it should produce the same V4 result.

---

# Migration Version Update

Each migration should update the Save Format Version only after its transformation succeeds.

Conceptually:

```csharp
public GameSaveData Migrate(
    GameSaveData save)
{
    // Transform all required data.

    save.saveFormatVersion = 4;

    return save;
}
```

Do not mark the Save as upgraded before completing the transformation.

---

# Migration Logging

Development builds should log:

- Source Save Format.
- Target Save Format.
- Each migration executed.
- Important conversions.
- Removed or replaced IDs.
- Validation warnings.
- Migration duration when useful.
- Failure details.

Avoid flooding release logs with unnecessary personal gameplay details.

---

# Migration Reports

For difficult migrations, an internal report may be useful.

Example:

```text
Save Migration
V7 → V8

- Converted 31 NPC relationship entries
- Replaced 2 deprecated Item IDs
- Migrated 14 Quest states
- Added 1 new tutorial flag
- 0 unresolved IDs
```

This is primarily a development and QA tool.

---

# Save Validation After Migration

Every migrated Save should be validated before runtime restoration.

Validation may check:

- Save Format equals current.
- Required root objects exist.
- Required IDs are valid.
- Quantities are within legal bounds.
- Calendar values are valid.
- Relationship values are valid.
- Quest states are coherent.
- Restoration states are valid.
- No impossible duplicate entries exist.
- Required world references can be resolved.

Related Notes:

- Save Data
- Services

---

# Validation Before Migration

Limited validation may also occur before migration.

The old Save must be valid enough to safely interpret.

However, validation rules should correspond to that old schema.

Do not reject an old Save merely because it does not satisfy requirements introduced in the current schema.

---

# Validation Severity

Validation findings may be categorized.

Example:

```text
Info
Warning
Recoverable Error
Fatal Error
```

Examples:

## Warning

Unknown optional Dialogue history ID.

## Recoverable Error

Removed Item can be converted to replacement.

## Fatal Error

Required root Save structure cannot be parsed.

---

# Corrupted Saves

Version migration is not a substitute for corruption recovery.

Corruption handling may use:

- Backup Save.
- Previous autosave.
- Atomic file replacement.
- Validation.
- Recovery logic.

Do not attempt arbitrary migration when the Save cannot be reliably interpreted.

---

# Backup Strategy

Recommended Save protection:

```text
Current Save
Backup Save
Temporary Write
```

During migration:

```text
Old Current Save
    ↓
Preserved Backup

Migrated Save
    ↓
Temporary File
    ↓
Validation
    ↓
Atomic Replacement
```

Exact platform implementation may differ.

---

# Cloud Saves

Cloud Save synchronization introduces version considerations.

Potential conflict:

```text
Local Save:
Format V8

Cloud Save:
Format V10
```

A V8 game build should not overwrite the newer V10 cloud Save.

Cloud conflict resolution should consider:

- Save Format.
- Last save time.
- Game Version.
- Platform rules.

Never assume the locally loadable Save is automatically the newest authoritative Save.

---

# Multiple Save Slots

Each Save slot has its own Save Format Version.

Migration should occur per Save.

Example:

```text
Slot 1 = V5
Slot 2 = V8
Slot 3 = V8
Current = V9
```

Loading Slot 1 runs:

```text
V5 → V6 → V7 → V8 → V9
```

Loading Slot 2 runs:

```text
V8 → V9
```

---

# Autosaves

Autosaves should use the same Save Format rules as manual Saves.

Do not maintain separate schema versions for:

- Manual Save.
- Autosave.
- Day End Save.

They should share the same core Save Data format unless there is a strong architectural reason otherwise.

---

# Save Preview Metadata

Save selection UI may need metadata before full migration.

Examples:

- Player Name.
- Year.
- Season.
- Day.
- Playtime.
- Last Save time.

Possible approaches:

- Keep preview metadata highly stable.
- Read it separately from the full Save.
- Support minimal metadata migration.
- Generate a cached Save-slot index.

Do not require full gameplay restoration merely to display the Save selection menu.

---

# Metadata Compatibility

Metadata should evolve conservatively.

Fields used to display Save slots should be:

- Simple.
- Stable.
- Easy to parse.
- Safe to default.

This reduces the chance that an old Save becomes invisible in the Save menu before migration.

---

# Save Versioning During Development

Development frequently changes Save structure.

Possible policy before release:

- Support only recent internal versions.
- Delete very old development Saves.
- Keep migration infrastructure operational.
- Write migrations for schemas used by QA or long-running test Saves.

After public release, compatibility requirements become much stricter.

---

# Pre-Release Save Compatibility

Before 1.0, decide explicitly whether player-facing early-access Saves are guaranteed to remain compatible.

If players can publicly play the game:

- Treat their Saves as important.
- Document any unavoidable compatibility break.
- Prefer migration whenever feasible.

Internal developer Saves do not require indefinite support.

---

# Release Compatibility

After public release, avoid intentionally breaking Saves.

Migration should prioritize:

- Player progress.
- Player possessions.
- Relationships.
- World restoration.
- Quest completion.
- Unlocks.
- Calendar progression.
- Player customization.

A migration that technically loads but silently destroys meaningful progress is not successful compatibility.

---

# Minimum Supported Save Version

The game may define a minimum supported Save Format.

Example:

```csharp
public static class SaveVersion
{
    public const int MinimumSupported = 3;
    public const int Current = 9;
}
```

Then:

```text
V1 → Unsupported
V2 → Unsupported
V3–V9 → Supported
```

For a released single-player game, retain support as far back as reasonably possible.

Dropping old Save support should be rare and intentional.

---

# Migration Consolidation

After many years, dozens of sequential migrations may become expensive or difficult to maintain.

Possible later strategy:

```text
Very Old Supported Schema
    ↓
Consolidated Migration
    ↓
Modern Baseline
    ↓
Recent Sequential Migrations
```

Do not prematurely consolidate.

Individual sequential migrations are easier to verify while the migration count is manageable.

---

# Never Rewrite Historical Migrations Casually

Once a migration has shipped, treat it as historical compatibility code.

Avoid changing:

```text
V4 → V5
```

after players may already have migrated through it.

Instead, if V5 requires correction:

```text
V5 → V6
```

should fix the resulting state.

Changing old migration behavior can make migration outcomes dependent on which game build performed the upgrade.

---

# Historical Migration Immutability

A shipped migration should ideally remain unchanged.

Exceptions may exist for a severe migration bug that prevents loading entirely, but fixes must be carefully tested against:

- Saves that have not migrated yet.
- Saves already migrated by the previous implementation.

---

# Migration Bugs

Suppose:

```text
V6 → V7
```

incorrectly gave some players:

```text
0 Friendship
```

A later build cannot assume every V7 Save is correct.

Possible V7 → V8 repair logic may inspect enough state to repair affected Saves where safely possible.

This is another reason migration tests and backups are critical.

---

# Migration Tests

Every migration should have automated tests.

Minimum:

```text
Given valid Vn Save
When migrated
Then result is Vn+1
```

Also test:

- Expected field conversion.
- Expected defaults.
- ID replacement.
- Removed content behavior.
- Edge values.
- Empty collections.
- Maximum values.
- Null/optional values where allowed.
- Invalid data handling.

---

# Full Chain Tests

Test migration through the entire supported chain.

Example:

```text
V1 Fixture
    ↓
V2
    ↓
V3
    ↓
V4
    ↓
Current
```

Verify the final Save is valid.

---

# Golden Save Fixtures

Maintain representative historical Save fixtures.

Examples:

```text
Save_V1_NewGame.json
Save_V1_MidGame.json
Save_V1_LateGame.json

Save_V4_Married.json
Save_V4_RestorationComplete.json

Save_V7_FullInventory.json
```

These can be used for regression tests.

Do not rely only on synthetic tiny test objects.

---

# Migration Test Scenarios

Representative Saves should cover:

- Brand-new game.
- Early game.
- Mid-game.
- Late game.
- Maximum Inventory.
- Multiple storage containers.
- High Friendship.
- Marriage.
- Completed Connection arcs.
- Active Quests.
- Completed Quests.
- Partially restored town.
- Fully restored town.
- Player-placed objects.
- Farm progression.
- Ledger progression.
- Festival progression.
- Edge calendar dates.
- Multiple years.

---

# Save Round-Trip Tests

Current Save Data should support:

```text
Runtime State
    ↓
Save Data
    ↓
Serialize
    ↓
Deserialize
    ↓
Validate
    ↓
Equivalent Save Data
```

Migration tests complement but do not replace current-format round-trip tests.

---

# Migration Performance

Save migration happens infrequently, so correctness is more important than micro-optimization.

However, avoid:

- Repeated full-database scans when unnecessary.
- Excessive allocations for enormous world Saves.
- Blocking operations beyond what loading UX can tolerate.

Migration may be performed during the loading screen.

---

# Migration Progress UI

If migrations become long enough to be noticeable, loading UI may display a generic status such as:

```text
Updating save data...
```

Avoid exposing technical schema numbers unless useful for diagnostics.

The player should not need to understand migration architecture.

---

# Cancellation During Migration

Do not allow unsafe interruption while a Save is being committed.

If cancellation is supported during preprocessing:

- Keep the original untouched.
- Discard temporary migrated data.
- Return safely to the menu.

Platform-specific save APIs may impose additional requirements.

---

# Crash During Migration

The architecture should survive a crash or power loss during migration.

Using:

- Original preservation.
- Temporary writes.
- Atomic replacement.
- Backup Saves.

prevents the only valid Save from being destroyed.

---

# Migration and Encryption

If Save files are later encrypted or compressed, distinguish:

```text
Storage Format Version
```

from:

```text
Gameplay Save Format Version
```

if necessary.

Example:

```text
Envelope Format = 2
Save Format = 14
```

The storage layer first decodes the envelope.

Then Save migration handles gameplay schema.

Do not overload one version number with unrelated responsibilities if formats become complex.

---

# Save Envelope

A future Save envelope may contain:

```text
Envelope Version
Compression Type
Encryption Information
Checksum
Save Format Version
Payload
```

This is optional and should only be introduced when needed.

The gameplay Save Format remains conceptually separate.

---

# Checksums

Checksums may detect incomplete or corrupted Save writes.

They are not a replacement for migration or validation.

Flow:

```text
Read
    ↓
Checksum verification
    ↓
Deserialize
    ↓
Migrate
    ↓
Validate
```

---

# Schema Documentation

Each Save Format increment should be documented.

Example:

```text
V4 → V5

Reason:
Quest objective state changed from integer progress
to per-objective Save entries.

Migration:
- Convert active Quest progress.
- Mark completed Quest objectives complete.
- Set rewardClaimed from prior completion state.
```

This documentation may live in:

- Migration code comments.
- Changelog.
- Save Versioning notes.
- Test fixture documentation.

---

# Migration Comment Example

```csharp
/// <summary>
/// V4 → V5
///
/// Replaces legacy heartLevel with friendshipPoints.
/// Existing heart levels are converted to the minimum
/// point threshold for that level.
/// </summary>
public sealed class SaveMigrationV4ToV5
{
}
```

Keep comments focused on why the transformation exists.

---

# Save Schema Changelog

A simple internal changelog may be maintained.

Example:

```text
V1
- Initial Save schema.

V2
- Added persistent NPC relationship entries.

V3
- Replaced Inventory slot Item names with stable Item IDs.

V4
- Added per-building Restoration state.

V5
- Reworked Quest objective persistence.
```

Do not increment the format merely to make the changelog match every game release.

---

# Suggested Folder Structure

```text
Save/
|
|-- Data/
|   |-- GameSaveData.cs
|   |-- PlayerSaveData.cs
|   |-- InventorySaveData.cs
|   |-- RelationshipSaveData.cs
|   |-- QuestSaveData.cs
|   |-- WorldSaveData.cs
|
|-- Versioning/
|   |-- SaveVersion.cs
|   |-- SaveMigrationService.cs
|   |-- ISaveMigration.cs
|   |
|   |-- Migrations/
|       |-- SaveMigrationV1ToV2.cs
|       |-- SaveMigrationV2ToV3.cs
|       |-- SaveMigrationV3ToV4.cs
|
|-- Validation/
|   |-- SaveValidationService.cs
|
|-- Serialization/
|   |-- ISaveSerializer.cs
|   |-- JsonSaveSerializer.cs
|
|-- Storage/
|   |-- ISaveStorageService.cs
|
|-- Tests/
    |-- Fixtures/
    |-- Migration/
```

---

# Namespace Structure

Possible namespaces:

```csharp
AsTheBellTolls.Save
AsTheBellTolls.Save.Data
AsTheBellTolls.Save.Versioning
AsTheBellTolls.Save.Versioning.Migrations
AsTheBellTolls.Save.Validation
AsTheBellTolls.Save.Serialization
AsTheBellTolls.Save.Storage
```

Use the final dependency structure established by the project.

---

# Migration Dependency Rules

Migration code may depend on:

- Old Save representations.
- Current Save representations.
- Static Data Registries where necessary.
- Migration-specific configuration.
- Core utility code.

Migration code should not depend on:

- UI.
- Scene Controllers.
- Active Player GameObject.
- Active NPC GameObjects.
- Current gameplay Systems.
- Presentation code.

---

# Legacy Save Types

Depending on serializer design, old schemas may require preserved legacy types.

Example:

```text
Legacy/
|
|-- V3/
|   |-- GameSaveDataV3.cs
|
|-- V4/
|   |-- GameSaveDataV4.cs
```

This is useful when structural changes are too large to deserialize directly into the current type.

---

# Legacy Type Scope

Legacy Save types should:

- Exist only for migration.
- Avoid gameplay methods.
- Avoid runtime System dependencies.
- Match the historical serialized shape.
- Be treated as compatibility infrastructure.

Do not continue using legacy types after migration.

---

# Raw Document Migration

Another possible approach is migrating serialized structures before strongly typed deserialization.

Example:

```text
JSON
    ↓
JSON document/tree
    ↓
V4 → V5 transformation
    ↓
Current GameSaveData
```

This may be useful for large structural changes.

The exact strategy depends on the serializer selected for the project.

---

# Typed Migration vs Raw Migration

## Typed Migration

Advantages:

- Strong compiler support.
- Clear data models.
- Easier refactoring within preserved historical types.

Disadvantages:

- May require maintaining legacy classes.

## Raw Migration

Advantages:

- Flexible for field renaming and structural changes.
- May avoid many historical class definitions.

Disadvantages:

- More string-based logic.
- Easier to introduce field-name errors.
- Harder to refactor safely.

Choose one primary strategy intentionally.

---

# Recommended Initial Strategy

For the initial architecture:

- Add `saveFormatVersion` immediately.
- Keep Save Data classes simple.
- Use stable Data IDs.
- Centralize `SaveVersion.Current`.
- Introduce a `SaveMigrationService`.
- Add one migration class per version transition.
- Preserve old Save fixtures once schemas begin changing.
- Keep gameplay Systems unaware of historical Save formats.
- Preserve the original file until migration and validation succeed.

This provides a strong foundation without overengineering early development.

---

# Example Version Class

```csharp
namespace AsTheBellTolls.Save.Versioning
{
    public static class SaveVersion
    {
        public const int MinimumSupported = 1;
        public const int Current = 1;
    }
}
```

As the schema changes:

```csharp
public const int Current = 2;
```

and add:

```text
SaveMigrationV1ToV2
```

---

# Example Migration Contract

```csharp
namespace AsTheBellTolls.Save.Versioning
{
    public interface ISaveMigration
    {
        int FromVersion { get; }
        int ToVersion { get; }

        GameSaveData Migrate(
            GameSaveData saveData);
    }
}
```

This contract is appropriate when the current data type can represent enough legacy data to migrate safely.

Otherwise use explicit legacy types or raw serialized migration.

---

# Example Simple Migration

Suppose V1 stores:

```text
money
```

and V2 stores:

```text
bells
```

Conceptually:

```csharp
public sealed class SaveMigrationV1ToV2 :
    ISaveMigration
{
    public int FromVersion => 1;
    public int ToVersion => 2;

    public GameSaveData Migrate(
        GameSaveData saveData)
    {
        saveData.economy.bells =
            saveData.economy.legacyMoney;

        saveData.saveFormatVersion =
            ToVersion;

        return saveData;
    }
}
```

The exact code depends on how legacy fields are represented.

---

# Example ID Migration

```csharp
private static string MigrateNpcId(
    string oldId)
{
    return oldId switch
    {
        "npc_adrian" =>
            "npc_lockwood_adrian",

        "npc_clara" =>
            "npc_weiss_clara",

        _ => oldId
    };
}
```

Apply this only in the migration where those IDs changed.

---

# Example Default Migration

New field:

```text
hasUnlockedLedger
```

Migration:

```csharp
saveData.player.hasUnlockedLedger =
    HasExistingProgressThatImpliesLedgerUnlock(
        saveData);
```

Do not blindly assign:

```csharp
false
```

if that would relock a feature for established players.

---

# Example Validation After Migration

```csharp
GameSaveData migrated =
    migrationService.MigrateToCurrent(saveData);

SaveValidationResult validation =
    saveValidationService.Validate(migrated);

if (!validation.IsValid)
{
    return SaveLoadResult.MigrationValidationFailed;
}
```

Only then proceed to gameplay restoration.

---

# Save Versioning Anti-Patterns

## Game Version as Schema Version

Bad:

```text
Save Version = 1.0.4
```

because every patch now appears to imply a schema migration.

Preferred:

```text
Game Version = 1.0.4
Save Format = 7
```

---

## No Explicit Version

Bad:

```text
Try deserializing and hope missing fields reveal age.
```

Preferred:

```text
saveFormatVersion
```

---

## Scattered Version Checks

Bad:

```text
Inventory System:
if old save...

Quest System:
if old save...

Relationship System:
if old save...
```

Preferred:

```text
Migration Pipeline
```

---

## Overwriting Before Validation

Bad:

```text
Load old Save
    ↓
Migrate
    ↓
Immediately overwrite original
    ↓
Discover invalid state
```

Preferred:

```text
Preserve original
    ↓
Migrate
    ↓
Validate
    ↓
Safe write
```

---

## Skipping Sequential Versions

Avoid maintaining many custom paths.

Preferred:

```text
V2 → V3 → V4 → V5
```

---

## Random Migration Decisions

Migration should be deterministic.

---

## Deleting Unknown Data Silently

Unknown persistent IDs should be:

- Mapped.
- Recovered.
- Warned.
- Intentionally discarded only when safe.

Do not silently erase valuable player progress.

---

## Changing Shipped Migrations

Do not casually rewrite old migration logic after release.

Create a newer migration to repair current state.

---

## Using Asset Names as Persistent IDs

Asset names can change.

Use stable Data IDs.

---

## Assuming New Game Defaults Are Correct for Old Saves

Migration defaults often need different behavior.

---

## Migrating Through Runtime Systems

Avoid:

```text
Load old Save
    ↓
Create gameplay world
    ↓
Use Systems to "fix" it
```

Prefer data migration before runtime restoration.

---

## Keeping Legacy Logic Forever

Once a Save reaches the current schema, gameplay code should not care which historical version it came from.

---

# Save Versioning Design Checklist

Before changing Save Data, determine:

1. Does this change affect persistent serialized structure?
2. Can old Saves deserialize safely?
3. Does the Save Format Version need to increment?
4. What is the old schema?
5. What is the new schema?
6. What exact transformation is required?
7. Are new fields needed?
8. What should their migration defaults be?
9. Are those defaults different from New Game defaults?
10. Were fields renamed?
11. Did field types change?
12. Were fields split or merged?
13. Did collection structure change?
14. Did an enum change?
15. Did any persistent Data ID change?
16. Was any referenced content removed?
17. Could player possessions be lost?
18. Could relationship progress be lost?
19. Could Quest progress be lost?
20. Could Restoration progress be lost?
21. Could unlocks be relocked?
22. Could rewards be granted twice?
23. Could tutorials replay incorrectly?
24. Could world objects disappear?
25. Could placed objects become invalid?
26. Does migration need static Registry access?
27. Is the migration deterministic?
28. Is it sequential?
29. Is the old Save preserved?
30. Is the migrated Save validated?
31. Is the migration tested?
32. Is there a representative historical fixture?
33. Has the full migration chain been tested?
34. Does a future-version Save fail safely?
35. Does the Save selection UI still read metadata?
36. Is the schema change documented?
37. Are shipped migrations being left intact?
38. Does gameplay remain unaware of historical formats?
39. Can a crash during migration preserve the original Save?
40. Is player progress preserved as faithfully as possible?

---

# Save Versioning Rules

- Every Save must contain an explicit Save Format Version.
- Keep Save Format Version separate from Game Version.
- Use sequential integer Save Format Versions.
- Maintain one authoritative `SaveVersion.Current`.
- Increment the format only when persistent compatibility requires intentional transformation.
- Migrate forward sequentially.
- Keep migrations small and focused.
- Run migration before gameplay restoration.
- Keep historical compatibility logic out of gameplay Systems.
- Preserve the original Save until migration and validation succeed.
- Never leave a Save partially migrated.
- Validate migrated Saves before loading them into gameplay.
- Reject Saves created by unsupported future formats.
- Use intentional migration defaults.
- Do not assume New Game defaults are correct for existing Saves.
- Treat shipped Data IDs as persistent.
- Explicitly migrate renamed Data IDs.
- Preserve player possessions and progression whenever possible.
- Handle removed content intentionally.
- Avoid silently deleting unknown or obsolete data.
- Preserve completed Quest rewards and unlocks.
- Avoid granting migrated rewards twice.
- Preserve relationship, marriage, Restoration, and world progression.
- Treat player-placed content carefully.
- Keep migration deterministic.
- Avoid unseeded randomness.
- Do not depend on active scenes or runtime GameObjects.
- Give migrations only the static dependencies they require.
- Keep old migration steps stable after release.
- Fix historical migration mistakes through newer migrations when possible.
- Maintain automated migration tests.
- Maintain representative historical Save fixtures.
- Test the full supported migration chain.
- Use atomic writes and backups during migration.
- Design cloud-save conflict handling with Save Format awareness.
- Keep Save metadata simple and backward-friendly.
- Document every Save Format change.
- Prefer fewer persisted derived values to reduce migration surface.
- Treat Save compatibility as part of the game's long-term architecture rather than a late release feature.

---

# Related Code Setup Notes

- Data IDs
- Dependencies
- Game Architecture
- Initialization Order
- Models
- Save Data
- Scriptable Objects
- Services

---

# Related System Notes

- Save System
- System Interaction Rules
- Individual gameplay System documentation
