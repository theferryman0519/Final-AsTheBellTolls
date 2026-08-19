---
Title: Code Setup / Initialization Order
Game: As The Bell Tolls
Owner: Carey Clement Jr
Created: August, 2026
Updated: August, 2026
Version: 0.0.1
---

## Information

- Initialization Order defines the sequence in which core infrastructure, static data, Services, Systems, Save Data, scenes, Controllers, Views, and UI become available.
- Initialization should be explicit, deterministic, and repeatable.
- No component should assume that another dependency is ready unless the initialization sequence guarantees it.
- Runtime Systems should be initialized only after the static data and Services they depend on are available.
- Save restoration should occur only after the required Systems exist.
- Scene-specific Controllers and Views should initialize only after the scene is loaded and the required persistent Systems are ready.
- Event Channel subscriptions should occur at a defined lifecycle point to prevent missed events, duplicate listeners, or reactions during partial initialization.
- Normal gameplay should not begin until all required initialization stages have completed successfully.
- New Game and Load Game should share as much initialization infrastructure as possible while remaining explicit about where their state comes from.
- Initialization failures should stop progression into gameplay and report a clear failure state rather than leaving the application partially initialized.
- Shutdown and reset should reverse or clean up runtime registrations and subscriptions in a controlled way.

---

# Purpose

Initialization Order answers:

- What must exist before gameplay Systems can initialize?
- When are ScriptableObject definitions and registries loaded?
- When are Services constructed?
- When are Systems constructed?
- When is Save Data loaded?
- When does Save migration occur?
- When do Systems restore persistent state?
- When is the first gameplay scene loaded?
- When do Controllers receive dependencies?
- When do Event Channel listeners subscribe?
- When may UI read gameplay state?
- When is normal gameplay allowed to begin?
- How does New Game differ from Load Game?
- What happens when initialization fails?
- How are scene transitions initialized after the game has already started?

The goal is to prevent hidden startup assumptions and race conditions.

---

# Core Principle

Initialization proceeds from foundational dependencies toward higher-level runtime behavior.

General direction:

```text
Application Boot
    ↓
Core Infrastructure
    ↓
Static Data
    ↓
Registries
    ↓
Services
    ↓
Persistent Gameplay Systems
    ↓
New Game State OR Save Restoration
    ↓
Scene Loading
    ↓
Scene Runtime Registration
    ↓
Controllers
    ↓
Views / UI
    ↓
Final Synchronization
    ↓
Gameplay Ready
```

Higher-level components should never initialize before the dependencies they require.

---

# Initialization Goals

The initialization architecture should provide:

- Deterministic startup.
- Explicit dependency order.
- Clear ownership.
- Reliable Save loading.
- Reliable New Game creation.
- Safe scene transitions.
- Safe Event subscription.
- Easy debugging.
- Easy automated testing.
- Minimal hidden Unity lifecycle dependence.
- No duplicate initialization.
- No partially restored gameplay state.
- Clear failure handling.
- Clean restart or return-to-title behavior.

---

# Initialization Phases

Recommended top-level phases:

1. Application Bootstrap
2. Core Initialization
3. Static Data Initialization
4. Registry Initialization
5. Service Initialization
6. Persistent System Initialization
7. Session State Initialization
8. Save Migration or New Game Setup
9. Runtime State Restoration
10. Scene Loading
11. Scene Registration
12. Controller Initialization
13. View and UI Initialization
14. Event Subscription Finalization
15. Derived State Synchronization
16. Gameplay Activation

Each phase should complete before dependent phases begin.

---

# Phase 1: Application Bootstrap

Purpose:

Create the minimum infrastructure required to initialize the application.

Typical responsibilities:

- Start the application composition root.
- Establish application-level lifetime objects.
- Configure logging.
- Determine platform.
- Load application settings.
- Initialize platform-independent bootstrap code.
- Create or locate global persistent root objects.
- Establish the initial Game State as `Booting`.

Example conceptual state:

```text
GameState = Booting
```

Normal player input should not be enabled yet.

---

# Bootstrap Component

A dedicated bootstrap component may coordinate startup.

Possible names:

- `GameBootstrap`
- `ApplicationBootstrap`
- `GameCompositionRoot`

Example responsibility:

```csharp
public sealed class GameBootstrap : MonoBehaviour
{
    private async void Start()
    {
        await InitializeApplicationAsync();
    }
}
```

The bootstrap should coordinate initialization.

It should not become the owner of gameplay systems.

---

# Composition Root

The composition root is the location where major dependencies are created and connected.

It may:

- Create Services.
- Create Systems.
- Provide configuration.
- Provide registries.
- Wire interfaces to implementations.
- Register application-lifetime dependencies.

Example:

```text
GameBootstrap
    ↓
Composition Root
    ↓
Create Core Services
    ↓
Create Gameplay Systems
```

Dependency construction should be centralized enough that the application's runtime architecture is understandable.

---

# Phase 2: Core Initialization

Core infrastructure should initialize before gameplay-specific dependencies.

Possible Core components:

- Logging.
- Game State infrastructure.
- Application configuration.
- Platform abstraction.
- Random Service.
- Input infrastructure.
- Scene loading infrastructure.
- Save storage infrastructure.
- Event infrastructure.
- General utility Services.

Core should not depend on high-level gameplay Systems.

---

# Core Initialization Example

```text
Logger
    ↓
Platform Service
    ↓
Application Settings
    ↓
Game State System
    ↓
Scene Loading Service
    ↓
Input Infrastructure
```

The exact order depends on actual dependencies.

---

# Phase 3: Static Data Initialization

Static designer-authored content should become available before Systems that require it.

Examples:

- Item Definitions.
- NPC Definitions.
- Crop Definitions.
- Quest Definitions.
- Recipe Definitions.
- Invention Definitions.
- Building Definitions.
- Location Definitions.
- Festival Definitions.
- Dialogue Definitions.
- Audio Definitions.
- Configuration ScriptableObjects.

Static definitions should be treated as read-only during gameplay.

Related Notes:

- Scriptable Objects

---

# Static Data Loading

Depending on project implementation, static data may be provided through:

- Serialized references.
- ScriptableObject registry assets.
- Addressables.
- Explicit bootstrap configuration.
- Generated registries.

The initialization architecture should not require runtime gameplay Systems to search the Unity project for assets.

---

# Phase 4: Registry Initialization

Registries should initialize before Systems attempt to resolve stable Data IDs.

Examples:

- Item Registry.
- NPC Registry.
- Quest Registry.
- Crop Registry.
- Recipe Registry.
- Location Registry.
- Building Registry.
- Invention Registry.
- Festival Registry.

Registry initialization should:

- Build lookup collections.
- Validate IDs.
- Detect duplicates.
- Detect null entries.
- Detect invalid required references.

---

# Registry Validation

Initialization should fail in development when critical registry errors exist.

Examples:

- Duplicate Item IDs.
- Duplicate NPC IDs.
- Missing Quest ID.
- Missing required Item reference.
- Broken prerequisite reference.

Do not allow the application to continue silently when persistent content identity is ambiguous.

---

# Registry Initialization Order

Registries may depend on other registries.

Example:

```text
Location Registry
    ↓
NPC Registry
```

if NPC definitions reference locations.

However, direct asset references may not require lookup initialization.

Prefer minimizing registry-to-registry dependencies.

---

# Phase 5: Service Initialization

Services initialize after required configuration and static data are available.

Possible Services:

- Random Service.
- Sell Price Service.
- Gift Evaluation Service.
- Condition Evaluation Service.
- Dialogue Selection Service.
- Save Serializer.
- Save Storage Service.
- Save Validation Service.
- Save Migration Service.
- Localization Service.
- Input Prompt Service.
- Platform Services.

Stateless Services may require little or no active initialization.

Stateful Services should expose explicit initialization where necessary.

Related Notes:

- Services

---

# Service Dependency Order

Services should initialize in dependency order.

Example:

```text
Platform Service
    ↓
Save Storage Service

Configuration
    ↓
Sell Price Service

Registries
    ↓
Dialogue Selection Service
```

Avoid circular Service initialization.

---

# Phase 6: Persistent Gameplay System Initialization

Persistent Systems should be created after foundational dependencies are ready.

Examples:

- Game State System.
- Time System.
- Calendar System.
- Weather System.
- Inventory System.
- Economy System.
- Stamina System.
- Tool System.
- Quest System.
- NPC Friendship System.
- NPC Connection System.
- Marriage System.
- Restoration System.
- Farming System.
- World State System.
- Mail System.
- Invention System.
- Ledger System.
- Festival System.

Systems should receive required Services, registries, configuration, and Event Channels through explicit dependency wiring.

---

# System Construction vs State Restoration

System construction and state restoration are separate phases.

Construction:

```text
Create Inventory System
```

does not yet mean:

```text
Load player's Inventory
```

The System must first exist in a valid empty or default state.

Then either:

```text
New Game Initialization
```

or:

```text
Save Restoration
```

provides the session state.

---

# System Initialization Contract

Systems may use an explicit initialization contract.

Example:

```csharp
public interface IInitializable
{
    void Initialize();
}
```

However, do not force every class to implement one interface if many Systems require different initialization inputs.

Explicit methods are acceptable.

Examples:

```csharp
timeSystem.Initialize(configuration);

inventorySystem.Initialize();

weatherSystem.Initialize(weatherConfiguration);
```

---

# Avoid Awake-Based Hidden Ordering

Do not rely on arbitrary Unity `Awake()` ordering across unrelated GameObjects for core architecture.

Problem:

```text
InventorySystem.Awake()
QuestSystem.Awake()
UI.Awake()
```

Unity object ordering may not communicate the true architectural dependency.

Prefer:

```text
Bootstrap
    ↓
Explicit Initialization
```

Unity lifecycle callbacks may create components, but important dependency readiness should be controlled explicitly.

---

# Script Execution Order

Avoid using Unity Script Execution Order as the primary dependency management architecture.

It may be useful for rare Unity-specific infrastructure cases.

It should not become the mechanism that explains the entire game startup sequence.

Explicit initialization is clearer and more testable.

---

# Phase 7: Session State Initialization

Once persistent Systems exist, the application determines how the gameplay session should begin.

Primary paths:

```text
New Game
```

or:

```text
Load Game
```

The two paths should converge before scene presentation begins.

---

# New Game Path

High-level New Game flow:

```text
Persistent Systems Created
    ↓
Create Default Runtime State
    ↓
Apply Character Creation Choices
    ↓
Apply New Game Progression Defaults
    ↓
Initialize Starting Calendar
    ↓
Initialize Starting Time
    ↓
Initialize Starting Weather
    ↓
Initialize Starting Inventory
    ↓
Initialize Starting World State
    ↓
Initialize Starting Quest / Story State
    ↓
Determine Intro Scene
    ↓
Continue to Scene Loading
```

---

# New Game Defaults

New Game defaults should come from:

- Static configuration.
- New Game configuration.
- Definitions.
- Explicit starting rules.

Avoid hardcoding starting values across many unrelated Systems.

Possible New Game Configuration:

```csharp
[CreateAssetMenu(
    fileName = "NewGameConfig",
    menuName = "As The Bell Tolls/Configuration/New Game")]
public sealed class NewGameConfiguration : ScriptableObject
{
}
```

Possible values:

- Starting Bells.
- Starting Stamina.
- Starting date.
- Starting time.
- Starting location.
- Starting Items.
- Initial unlocked Tools.
- Initial Quests.
- Intro flags.

---

# Character Creation Timing

Character creation may occur before full gameplay state creation if required.

Possible flow:

```text
Application initialized
    ↓
Start New Game
    ↓
Intro sequence
    ↓
Avatar Creation
    ↓
Character Creation Result
    ↓
Apply Player identity and appearance
    ↓
Continue New Game
```

The exact sequence should match the final intro implementation.

Character Creation UI should return a structured result rather than directly modifying Save Data.

---

# New Game Save Data

New Game should initialize runtime Systems first.

A Save Data snapshot may be created later when the first Save occurs.

Alternative:

```text
Create Initial GameSaveData
    ↓
Restore Systems through normal Load pathway
```

may be used if it significantly simplifies consistency.

Whichever approach is chosen should produce identical runtime state.

---

# Load Game Path

High-level Load Game flow:

```text
Persistent Systems Created
    ↓
Read Save File
    ↓
Validate Storage Envelope
    ↓
Read Save Format Version
    ↓
Migrate if required
    ↓
Validate Current Save Data
    ↓
Restore Persistent Systems
    ↓
Rebuild Derived Runtime State
    ↓
Determine Saved Location
    ↓
Continue to Scene Loading
```

Related Notes:

- Save Data
- Save Versioning

---

# Phase 8: Save Migration

Migration occurs before gameplay Systems restore old persistent data.

Flow:

```text
Raw Save
    ↓
Deserialize
    ↓
Read Save Format
    ↓
Migration Pipeline
    ↓
Current Save Format
    ↓
Validation
```

Gameplay Systems should not need to know which historical Save version was loaded.

---

# Save Migration Failure

If migration fails:

```text
Initialization stops
```

The application should:

- Preserve the original Save.
- Report failure.
- Return to a safe menu state.
- Avoid partially restoring Systems.
- Avoid entering gameplay.

---

# Phase 9: Runtime State Restoration

After current Save Data is available, persistent Systems restore their state.

Recommended rules:

- Restore in dependency order.
- Suppress normal gameplay Events during partial restoration.
- Rebuild runtime-only caches.
- Validate resolved IDs.
- Keep Systems internally coherent after each restore step.
- Do not update UI yet.

---

# Restoration Order

A possible high-level restoration order:

1. Game State baseline.
2. Calendar.
3. Time.
4. Player identity.
5. Economy.
6. Stamina.
7. Inventory.
8. Tools.
9. Restoration.
10. Relationships.
11. Quests.
12. Inventions.
13. Farming.
14. World state.
15. Mail.
16. Ledger.
17. Festivals.
18. Weather.
19. NPC-related runtime state.
20. Derived unlocks and cross-system reconciliation.

The exact order should be adjusted based on actual dependencies.

---

# Restoration Dependency Example

Suppose Weather restoration requires the Calendar.

Then:

```text
Calendar restore
    ↓
Weather restore
```

Suppose NPC routine restoration depends on:

- Calendar.
- Time.
- Weather.
- Quest state.
- Relationship state.

Then NPC routine runtime state should be resolved after those Systems are restored.

---

# Restore Without Gameplay Events

During load:

```text
Inventory restored
```

should not automatically cause:

```text
Quest updated
HUD notification
Audio cue
Tutorial trigger
```

Normal gameplay Events should remain suppressed until restoration completes.

---

# Restore Mode

The application may expose a load-state context.

Example:

```csharp
public enum InitializationMode
{
    NewGame,
    LoadGame
}
```

or:

```csharp
public bool IsRestoringSave { get; }
```

Systems may use dedicated restoration methods rather than checking global flags everywhere.

Preferred:

```csharp
inventorySystem.RestoreFromSave(data);
```

rather than:

```csharp
inventorySystem.AddItem(...)
```

for every saved entry.

---

# Phase 10: Derived State Reconstruction

Not every runtime value belongs in Save Data.

After persistent state restoration, recalculate derived state.

Examples:

- Relationship Status from Friendship Points.
- Heart Level.
- Daylight State.
- Inventory lookup dictionaries.
- Quest availability.
- Unlock eligibility.
- Current NPC routine selection.
- Weather presentation profile.
- Ledger completion percentages.
- Restoration feature availability.
- Shop availability.
- Current seasonal content.

---

# Reconciliation Phase

Some Systems may need cross-system reconciliation after all primary state is restored.

Example:

```text
Quest state
Relationship state
Restoration state
Calendar state
    ↓
Unlock Reconciliation
```

This step should be explicit.

Avoid hidden initialization Events that cause unpredictable chains.

---

# Reconciliation Examples

Possible reconciliation operations:

- Ensure unlocked recipes match completed progression.
- Ensure Restoration feature gates are correct.
- Ensure spouse residence state is correct.
- Ensure currently available Quests match prerequisites.
- Ensure NPC routines reflect current world state.
- Ensure active Festival state matches Calendar.
- Ensure Mail eligibility matches progression.
- Ensure Ledger completion caches are rebuilt.

---

# Phase 11: Scene Loading

Once persistent runtime state is ready, load the required gameplay scene.

For New Game:

```text
Starting Scene
```

For Load Game:

```text
Saved Current Location
```

or the appropriate logical location scene.

Scene loading should occur through the Scene System or Scene Loading Service.

---

# Scene Loading State

Before loading:

```text
GameState = Loading
```

Normal gameplay input should remain disabled.

Possible operations:

- Fade out.
- Start loading presentation.
- Pause gameplay interaction.
- Begin async scene load.

---

# Scene Identifier

Save Data should use a logical location or scene identifier rather than depending on Unity build index.

Prefer:

```text
location_blackmere_town-square
```

or:

```text
scene_blackmere
```

over:

```text
Scene Build Index = 4
```

Build indices may change.

---

# Phase 12: Scene Registration

After a scene loads, scene-specific objects register themselves or are discovered by a scene composition component.

Possible registrations:

- Player spawn points.
- NPC spawn points.
- Doors.
- Interactables.
- Resource nodes.
- Storage objects.
- Farming regions.
- Cameras.
- Scene Audio.
- Scene-specific Controllers.
- World Views.

Persistent Systems already exist.

The scene now provides presentation objects and scene-local references.

---

# Scene Context

A `SceneContext` or similar component may expose scene-specific dependencies.

Example:

```csharp
public sealed class SceneContext : MonoBehaviour
{
    [SerializeField]
    private Transform _playerRoot;

    [SerializeField]
    private Camera _mainCamera;
}
```

The Scene Context should coordinate scene references.

It should not become a second gameplay state owner.

---

# Scene Registration IDs

Persistent world objects should register with stable IDs.

Example:

```text
resource_blackmere_forest_001
storage_pendrelle_kitchen_001
door_town-hall_main
```

Registration allows runtime Systems to synchronize persistent state with scene presentation.

---

# Duplicate Scene IDs

Scene initialization should validate duplicate persistent world IDs.

Example:

Two objects register:

```text
storage_pendrelle_001
```

This is invalid.

Fail loudly during development.

---

# Phase 13: Player Scene Initialization

After scene registration:

1. Resolve saved or starting location.
2. Resolve spawn point.
3. Create or activate Player presentation.
4. Apply appearance.
5. Restore logical facing direction.
6. Connect Player Controllers.
7. Configure camera target.
8. Keep gameplay Input disabled until final activation.

---

# Exact Position vs Logical Spawn

For normal scene transitions, logical spawn IDs are preferred.

For Save restoration, the final design may store:

- Exact position.
- Logical spawn ID.
- Both.

If saved exact coordinates are invalid after a content update:

```text
Fallback Spawn ID
```

should provide recovery.

---

# Phase 14: NPC Scene Initialization

NPC presentation should be created or activated according to authoritative runtime state.

Flow:

```text
Calendar
Time
Weather
Relationships
Quests
Festivals
NPC Routine System
    ↓
Determine NPC runtime location
    ↓
Current scene contains NPC?
    ↓
Yes → Spawn / activate NPC View
No → Do not spawn here
```

Scene GameObjects should represent the NPC.

NPC Systems own the relevant gameplay state.

---

# NPC Positioning

NPCs should generally initialize from their current routine rather than saved exact coordinates.

Exceptions may exist for:

- Scripted events.
- Persistent travel.
- Special event positions.
- Marriage-specific states.

The routine architecture should determine the correct initialization rule.

---

# Phase 15: Controller Initialization

Controllers initialize after:

- Required Systems exist.
- Runtime state is restored.
- Required scene references exist.

Controllers may receive:

- Systems.
- Services.
- Event Channels.
- Scene Views.
- Input interfaces.
- Camera references.
- Audio presentation dependencies.

---

# Controller Initialization Example

```csharp
playerInteractionController.Initialize(
    interactionSystem,
    inputSystem,
    sceneInteractionRegistry);
```

Do not allow the Controller to search globally for unrelated dependencies when explicit wiring is possible.

---

# Controller Readiness

A Controller should not process player input until its initialization is complete.

Possible property:

```csharp
public bool IsInitialized { get; private set; }
```

However, centralized Game State gating is preferable to every Controller implementing separate ad hoc checks.

---

# Phase 16: View Initialization

Views synchronize Unity presentation with current runtime state.

Examples:

- Inventory slot visuals.
- Crop visuals.
- Building Restoration visuals.
- NPC appearance.
- Tool appearance.
- Resource node active state.
- Door state.
- World decoration.
- Lighting.
- Weather effects.

Views should query or receive current state.

They should not establish authoritative gameplay state.

---

# Initial View Synchronization

Initial synchronization is different from responding to change Events.

At startup:

```text
System already contains current state
    ↓
View requests snapshot
    ↓
View renders current state
```

Do not assume the View will receive every historical Event that produced that state.

---

# Phase 17: UI Initialization

UI initializes after required Systems and presentation dependencies are available.

Examples:

- HUD.
- Date/Time panel.
- Stamina display.
- Interaction prompt.
- Relationship UI.
- Quest UI.
- Map.
- Calendar.
- Ledger.
- Settings.

UI should perform an initial snapshot refresh.

---

# HUD Initial Synchronization

Example:

```text
Time System
Calendar System
Economy System
Stamina System
    ↓
HUD Controller
    ↓
HUD View
```

The HUD should immediately display current values.

It should not wait for the next:

```text
TimeChanged
```

or:

```text
StaminaChanged
```

Event.

---

# Phase 18: Event Channel Subscriptions

Event subscriptions should be established at a clear lifecycle point.

Listeners should generally subscribe when:

- Their dependencies exist.
- Their own initialization is complete.
- They are ready to process Events.

Listeners should unsubscribe when:

- Disabled permanently.
- Destroyed.
- Scene unloaded.
- Session reset.
- Application shuts down.

---

# Subscription Timing

Persistent runtime Systems may subscribe before Save restoration only when they intentionally ignore or suppress restoration notifications.

Scene Views and Controllers should subscribe after they are initialized.

Recommended principle:

```text
Initialize State
    ↓
Initialize Listener
    ↓
Synchronize Current State
    ↓
Subscribe for Future Changes
```

or:

```text
Subscribe
    ↓
Synchronize Current State
```

when Events are already safely suppressed.

Choose one convention and use it consistently.

---

# Avoid Missed Event Dependency

Components must not depend on having witnessed past Events.

Example:

Bad:

```text
HUD starts with 0 Bells.
HUD waits for EconomyChanged Event.
```

If no new transaction occurs, the HUD remains wrong.

Preferred:

```text
HUD reads Economy snapshot during initialization.
Then listens for EconomyChanged.
```

---

# Duplicate Subscription Prevention

Avoid subscribing multiple times due to repeated `OnEnable()` calls without matching unsubscribe logic.

Bad:

```csharp
private void OnEnable()
{
    eventChannel.Raised += HandleEvent;
}
```

with no matching `OnDisable()`.

Preferred:

```csharp
private void OnEnable()
{
    eventChannel.Raised += HandleEvent;
}

private void OnDisable()
{
    eventChannel.Raised -= HandleEvent;
}
```

or explicit initialization/disposal based on component lifetime.

---

# Persistent Listener Lifetimes

Application- or session-lifetime Systems should not accidentally subscribe again every scene load.

Scene-lifetime listeners should not remain subscribed after their scene is unloaded.

Event listener lifetime should match object lifetime.

---

# Phase 19: Final Synchronization

Before gameplay begins, perform one final synchronization pass.

Possible tasks:

- Verify active location.
- Verify Player placement.
- Refresh HUD.
- Refresh Weather presentation.
- Refresh lighting.
- Refresh NPC visibility.
- Refresh interactables.
- Refresh Restoration presentation.
- Refresh Farm presentation.
- Refresh quest markers.
- Refresh available interactions.
- Verify camera state.
- Verify Audio state.

---

# Final Validation

Before enabling gameplay, assert critical readiness.

Example checklist:

```text
Core initialized?
Registries valid?
Services ready?
Persistent Systems ready?
Session state created/restored?
Scene loaded?
Player initialized?
Scene registrations valid?
Controllers initialized?
UI synchronized?
Critical subscriptions active?
```

If any required item is false:

```text
Do not enter Gameplay state.
```

---

# Phase 20: Gameplay Activation

Once initialization is complete:

```text
GameState = Gameplay
```

Then enable:

- Player movement.
- Player interaction.
- Tool input.
- Normal simulation.
- NPC movement.
- Gameplay timers.
- Normal Event processing.
- HUD interaction where appropriate.

Initialization is complete.

---

# Startup State Machine

A startup state machine may make the process explicit.

Example:

```csharp
public enum StartupState
{
    None,
    Booting,
    LoadingStaticData,
    InitializingServices,
    InitializingSystems,
    LoadingSave,
    MigratingSave,
    RestoringState,
    LoadingScene,
    InitializingScene,
    Synchronizing,
    Ready,
    Failed
}
```

This state is for infrastructure diagnostics.

It does not need to become player-facing.

---

# Game State vs Startup State

`GameState` and `StartupState` may be separate concepts.

Game State might include:

```text
Title
Gameplay
Paused
Dialogue
Cinematic
DayEnd
Loading
```

Startup State tracks the boot pipeline.

Avoid overloading one enum with every possible infrastructure phase if that makes gameplay state difficult to use.

---

# Asynchronous Initialization

Some initialization operations may be asynchronous.

Examples:

- Platform initialization.
- Save file reading.
- Cloud save synchronization.
- Addressable loading.
- Scene loading.

Use explicit async sequencing.

Example:

```csharp
await platformService.InitializeAsync();
await saveSystem.LoadAsync(slotId);
await sceneSystem.LoadAsync(sceneId);
```

Do not use uncontrolled `async void` outside Unity event entry points where exceptions cannot be managed properly.

---

# Cancellation

Initialization may support cancellation from:

- Save selection.
- Cloud conflict screen.
- Loading screen.

Cancellation should only occur at safe boundaries.

Never cancel in a way that leaves:

- Half-restored Systems.
- Half-written Save.
- Partially registered scene state.

---

# Initialization Timeout

Do not add arbitrary timeout behavior to normal local initialization unless a dependency can realistically hang.

Platform or network Services may use timeout policies.

Local deterministic initialization should either:

- Succeed.
- Fail with an error.

---

# Loading Screen

The loading presentation may remain active throughout:

```text
Save Read
Migration
System Restoration
Scene Loading
Scene Initialization
Final Synchronization
```

Gameplay should not be visible in a half-initialized state.

---

# Initialization Progress

Initialization may expose coarse progress stages.

Example:

```text
Preparing game...
Loading save...
Loading Blackmere...
Preparing residents...
Ready
```

The player does not need to see technical class names.

---

# New Game Initialization Sequence

Recommended complete New Game sequence:

```text
1. Application Boot
2. Core Infrastructure
3. Static Definitions
4. Registries
5. Services
6. Persistent Systems
7. Enter New Game Setup
8. Apply starting configuration
9. Apply Player creation choices
10. Initialize Calendar
11. Initialize Time
12. Initialize Economy
13. Initialize Stamina
14. Initialize Inventory
15. Initialize Tools
16. Initialize Restoration
17. Initialize Relationships
18. Initialize Quests
19. Initialize World state
20. Initialize Weather
21. Initialize other progression Systems
22. Reconcile derived state
23. Load starting scene
24. Register scene objects
25. Initialize Player
26. Initialize NPC presentation
27. Initialize Controllers
28. Initialize Views
29. Initialize UI
30. Establish Event subscriptions
31. Synchronize presentation
32. Enter Gameplay / Intro state
```

The intro may temporarily use `Cinematic` rather than normal `Gameplay`.

---

# Load Game Initialization Sequence

Recommended complete Load Game sequence:

```text
1. Application Boot
2. Core Infrastructure
3. Static Definitions
4. Registries
5. Services
6. Persistent Systems
7. Read selected Save
8. Detect Save Format
9. Migrate if necessary
10. Validate Save
11. Begin restoration mode
12. Restore Calendar
13. Restore Time
14. Restore Player state
15. Restore Economy
16. Restore Stamina
17. Restore Inventory
18. Restore Tools
19. Restore Relationships
20. Restore Restoration
21. Restore Quests
22. Restore Farming
23. Restore World state
24. Restore Mail
25. Restore Inventions
26. Restore Ledger
27. Restore Festival state
28. Restore Weather
29. Rebuild derived state
30. Reconcile cross-system state
31. End restoration mode
32. Load saved location
33. Register scene objects
34. Initialize Player presentation
35. Initialize NPC presentation
36. Initialize Controllers
37. Initialize Views
38. Initialize UI
39. Establish Event subscriptions
40. Synchronize presentation
41. Raise Load Completed
42. Enter Gameplay state
```

---

# Continue Game

A `Continue` action should resolve the most recent valid Save slot.

It should then use the exact same Load Game pipeline.

Do not maintain a separate simplified loading architecture for Continue.

---

# Return to Title

Returning to Title requires controlled session teardown.

Recommended sequence:

```text
Gameplay
    ↓
Enter Loading / SessionEnding
    ↓
Disable gameplay Input
    ↓
Close menus / dialogue
    ↓
Unsubscribe scene listeners
    ↓
Unload gameplay scene
    ↓
Dispose session-lifetime Controllers
    ↓
Reset persistent gameplay Systems
    ↓
Clear active Save session
    ↓
Keep application-level Services
    ↓
Load / reveal Title
    ↓
GameState = Title
```

---

# Starting Another Save

Starting or loading another Save in the same application process must not inherit state from the previous session.

Persistent session Systems should either:

- Be recreated.
- Or implement a complete explicit reset before new restoration.

Recreating session-owned Systems is often safer when architecture permits it.

---

# Reset Requirements

Session reset must clear:

- Inventory.
- Economy.
- Time.
- Calendar.
- Relationships.
- Quests.
- Farming.
- World state.
- Restoration.
- Mail.
- Inventions.
- Ledger.
- Festivals.
- Game Flags.
- NPC runtime state.
- Temporary caches.
- Session-specific Event subscriptions.

Application settings should remain.

---

# Scene Transition Initialization

Normal gameplay scene transitions use a shorter initialization sequence because persistent Systems already exist.

Example:

```text
Transition Requested
    ↓
Validate destination
    ↓
GameState = Loading
    ↓
Disable gameplay Input
    ↓
Fade out
    ↓
Unsubscribe / dispose old scene listeners
    ↓
Unload old scene
    ↓
Load destination scene
    ↓
Register scene objects
    ↓
Resolve Player spawn
    ↓
Initialize scene Controllers
    ↓
Initialize Views
    ↓
Spawn / synchronize NPCs
    ↓
Synchronize Weather / Lighting / Audio
    ↓
Subscribe scene listeners
    ↓
Fade in
    ↓
GameState = Gameplay
```

---

# Scene Transition Persistence

A normal scene transition should not recreate persistent gameplay Systems.

Examples that persist:

- Time.
- Calendar.
- Inventory.
- Economy.
- Relationships.
- Quests.
- Restoration.
- Farming state.
- Weather.
- Mail.
- Game Flags.

Only scene presentation changes.

---

# Scene Unload Order

Before unloading a scene:

1. Stop scene-specific input handling.
2. End transient interactions.
3. Unsubscribe scene listeners.
4. Unregister persistent world Views.
5. Release scene-specific Controllers.
6. Release scene-specific Audio.
7. Clear scene references.
8. Unload scene.

Persistent Systems remain.

---

# Scene Load Order

After loading a scene:

1. Locate or initialize Scene Context.
2. Register stable world objects.
3. Validate duplicate IDs.
4. Synchronize persistent world state.
5. Resolve Player spawn.
6. Initialize Player presentation.
7. Resolve NPC presence.
8. Initialize NPC presentation.
9. Initialize scene Controllers.
10. Initialize Views.
11. Initialize scene UI if any.
12. Synchronize Weather.
13. Synchronize lighting.
14. Synchronize Audio.
15. Establish scene subscriptions.
16. Enable gameplay.

---

# Additive Scenes

If using additive scenes:

- Define which scene owns which presentation objects.
- Register and unregister by scene lifetime.
- Avoid duplicate persistent Controllers.
- Avoid duplicate cameras or Audio listeners.
- Keep gameplay Systems independent of additive scene composition.

The same dependency principles still apply.

---

# Interior Initialization

Entering a building interior may use:

```text
Persistent Gameplay Systems
    ↓
Load Interior Scene
    ↓
Register doors / interactables
    ↓
Resolve NPCs present
    ↓
Initialize scene Controllers
    ↓
Synchronize Views
```

The building's Restoration state remains owned by the Restoration System.

The interior scene only displays the result.

---

# Festival Initialization

Festival scenes or modes may require special preparation.

Possible sequence:

```text
Festival eligibility confirmed
    ↓
GameState = Loading
    ↓
Preserve normal world state
    ↓
Load Festival scene / configuration
    ↓
Initialize Festival System state
    ↓
Spawn participants
    ↓
Initialize Festival Controllers
    ↓
Initialize Festival UI
    ↓
Synchronize score / activities
    ↓
GameState = Festival
```

When leaving:

```text
Finalize Festival results
    ↓
Persist authoritative outcome
    ↓
Unload Festival presentation
    ↓
Return to normal location
    ↓
Reinitialize scene presentation
```

---

# Day End Initialization Boundary

Day End is not a full application initialization.

However, it includes a controlled transition between daily runtime states.

Example:

```text
Gameplay
    ↓
DayEnd
    ↓
Process daily systems
    ↓
Advance Calendar
    ↓
Advance Weather
    ↓
Reset daily state
    ↓
Recalculate NPC routines
    ↓
Autosave
    ↓
Initialize new day's scene presentation
    ↓
Gameplay
```

If the player wakes in a different scene or position, scene reinitialization may occur.

---

# Daily Reset Order

A possible daily reset sequence:

1. Complete Day End transactions.
2. Advance Calendar.
3. Reset Time to morning.
4. Advance Weather.
5. Reset Stamina as required.
6. Advance Crops.
7. Advance resource respawns.
8. Reset daily NPC interaction limits.
9. Resolve Mail.
10. Resolve Quest daily state.
11. Resolve Festival state.
12. Resolve Shop stock.
13. Resolve NPC routines.
14. Resolve daily Dialogue availability.
15. Reconcile daily unlocks.
16. Save.
17. Initialize morning presentation.

The final order should follow actual System dependencies.

---

# Event Channel Initialization

Event Channel assets may exist before Systems initialize.

Listeners should not assume that simply having the channel asset means gameplay is ready.

Possible phases:

```text
Event Assets Available
    ↓
Systems Constructed
    ↓
Persistent Listeners Subscribe
    ↓
State Created / Restored
    ↓
Scene Listeners Subscribe
    ↓
Gameplay Events Enabled
```

---

# Event Suppression During Initialization

A central mechanism may suppress normal Event propagation during:

- New Game bulk initialization.
- Save restoration.
- Session reset.

Alternative:

Use dedicated internal methods that do not raise Events.

Prefer the approach that keeps behavior explicit.

---

# Avoid Global Event Suppression When Possible

Global suppression can hide bugs if overused.

Preferred:

```csharp
RestoreFromSave(...)
```

with intentional silent mutation.

Then perform:

```text
Final synchronization
```

A limited initialization event gate may still be useful for infrastructure.

---

# Initialization Events

A small number of high-level initialization notifications may be useful.

Examples:

```text
ApplicationInitialized
SessionInitialized
SceneInitialized
LoadCompleted
NewGameInitialized
```

Do not create an Event for every initialization step unless there is a real independent listener requirement.

---

# LoadCompleted Event

`LoadCompleted` should occur only after:

- Save migration.
- Save validation.
- System restoration.
- Derived state reconstruction.
- Scene initialization.
- Critical presentation synchronization.

It should mean:

```text
The loaded session is safe for normal runtime behavior.
```

---

# SceneInitialized Event

`SceneInitialized` should occur only after:

- Scene registration.
- Player positioning.
- NPC spawning.
- Controller wiring.
- World synchronization.

Listeners should not use it to perform missing core initialization that should already have occurred.

---

# Initialization Dependencies

Dependencies should be explicit.

Example:

```text
Quest System requires:
- Quest Registry
- Inventory query
- Relationship query
- Event Channels
```

Therefore those dependencies must exist before Quest System initialization.

Document important dependencies in individual System notes.

---

# Required vs Optional Dependencies

Initialization should distinguish:

## Required

Failure prevents component initialization.

Example:

```text
Inventory System requires Item Registry
```

## Optional

Feature may operate with reduced capability.

Example:

```text
Telemetry Service unavailable
```

should not necessarily prevent gameplay.

Criticality should be explicit.

---

# Initialization Failure Categories

Possible categories:

- Missing configuration.
- Missing registry.
- Duplicate Data IDs.
- Save read failure.
- Save migration failure.
- Save validation failure.
- Scene load failure.
- Missing critical spawn point.
- Missing required scene context.
- Platform Service failure.
- Corrupt Save.
- Unsupported Save version.

---

# Failure State

Initialization failure should place the application in a safe state.

Example:

```text
StartupState = Failed
GameState = Error / Title
```

Then:

- Disable gameplay input.
- Preserve logs.
- Preserve Save files.
- Display appropriate error UI.
- Allow returning to Title when possible.

---

# Development Failure Behavior

In development builds, critical initialization errors should be loud.

Examples:

- Throw clear exceptions.
- Log detailed dependency information.
- Fail validation.
- Prevent Gameplay state.

Silent fallback may hide architectural problems.

---

# Release Failure Behavior

Release builds should:

- Fail safely.
- Preserve player data.
- Avoid exposing raw stack traces.
- Provide understandable player-facing messaging.
- Log technical details for diagnostics where appropriate.

---

# Missing Optional Content

Optional content may be skipped if absent and the game remains valid.

Example:

- Optional cosmetic definition.
- Optional ambient Audio.

Critical content should fail initialization.

Define which content categories are mandatory.

---

# Initialization and Save Validation

Save validation should complete before gameplay scene initialization.

Avoid loading the player's world and only then discovering:

```text
Inventory Save Data is invalid.
```

Fail earlier when possible.

---

# Initialization and Data Validation

Static Data validation should ideally happen before Save loading.

Reason:

Save migration and restoration may depend on valid registries.

Example:

```text
Duplicate NPC ID
```

must be resolved before relationship Save Data can be safely restored.

---

# Initialization and Localization

Localization should initialize before UI that requires localized strings.

Possible order:

```text
Localization Service
    ↓
UI Initialization
```

Static Data may store localization keys before localization itself is ready.

---

# Initialization and Audio

Audio infrastructure may initialize early.

Scene-specific music and ambience should start after:

- Scene is loaded.
- Current location is known.
- Weather is known.
- Time is known.
- Festival state is known.

Avoid playing default scene Audio before persistent state synchronization.

---

# Initialization and Weather Presentation

Weather System state should already be authoritative before scene effects initialize.

Flow:

```text
Weather System
    ↓
Current Weather Snapshot
    ↓
Scene Weather Controller
    ↓
Particles / Lighting / Audio
```

Do not allow scene Weather components to randomly choose Weather.

---

# Initialization and Lighting

Lighting presentation may depend on:

- Time.
- Daylight State.
- Season.
- Weather.

Initialize after those Systems have restored.

---

# Initialization and Camera

Camera infrastructure may exist early.

Scene camera behavior should initialize after:

- Scene loaded.
- Player View exists.
- Player spawn resolved.

Then:

```text
Camera target = Player
```

---

# Initialization and Input

Input infrastructure may initialize at application boot.

Gameplay Input should remain gated until:

```text
GameState = Gameplay
```

UI Input may be active earlier for:

- Title screen.
- Save selection.
- Character Creation.
- Loading cancellation where allowed.

---

# Input Context Initialization

Different input contexts may include:

```text
Title
Menu
Gameplay
Dialogue
Festival
Cinematic
```

The active Input map should follow current Game State.

Do not enable Gameplay Input merely because the Input System itself is initialized.

---

# Initialization and UI Navigation

Initial UI selection should be set after:

- UI Views exist.
- Input context is correct.
- Required data is available.

This is particularly important for controller navigation.

---

# Initialization and Save UI

The Save selection UI may initialize before a gameplay session exists.

Application-level Save metadata Services should therefore not depend on active gameplay Systems.

---

# Initialization and Game Flags

Game Flags should restore before Systems that use them to determine:

- Quest availability.
- Dialogue availability.
- World changes.
- Tutorial state.

If Game Flags depend on other progression data, reconciliation may occur after both are restored.

---

# Initialization and Quests

Quest restoration should occur before UI or world markers initialize.

Quest availability recalculation should occur only after prerequisites such as:

- Relationships.
- Restoration.
- Game Flags.
- Calendar.

are available.

---

# Initialization and Relationships

Relationship state should restore before:

- NPC relationship UI.
- Relationship-dependent Dialogue selection.
- Heart Event availability.
- Marriage-dependent routines.

---

# Initialization and NPC Routines

NPC Routine System should resolve current schedules after:

- Calendar.
- Time.
- Weather.
- Festival state.
- Quest state.
- Relationship state.
- World state.

are available.

This makes routine selection one of the later gameplay-state initialization steps.

---

# Initialization and Dialogue

Dialogue Systems may initialize early as infrastructure.

Actual available Dialogue should be resolved only after contextual Systems are ready.

Examples:

- Time.
- Weather.
- Friendship.
- Quests.
- Marriage.
- Festivals.
- Restoration.
- Game Flags.

---

# Initialization and Farming

Farming state should restore before Farm Views initialize.

Then:

```text
Farming System
    ↓
Farm Tile Snapshots
    ↓
Crop / Soil Views
```

Crop growth should not accidentally advance during load.

---

# Initialization and Restoration

Restoration state should restore before:

- Building visuals.
- Feature availability.
- Shop availability.
- World accessibility.
- Related NPC routines.

The world presentation then reflects the restored state.

---

# Initialization and Shops

Shop runtime availability may depend on:

- Calendar.
- Time.
- Restoration.
- Festival state.
- NPC presence.

Shop stock may be daily state initialized after daily date resolution.

---

# Initialization and Mail

Mail eligibility may depend on:

- Calendar.
- Quest progress.
- Friendship.
- Restoration.
- Game Flags.

Restore existing Mail first.

Then evaluate new eligible Mail at the appropriate daily or session boundary.

Do not repeatedly issue the same Mail during load.

---

# Initialization and Ledger

Ledger persistent progress should restore first.

Derived completion percentages should calculate afterward.

UI initializes from the resulting snapshot.

---

# Initialization and Inventions

Invention state should restore before:

- Workbench UI.
- Invention menu.
- Unlock-dependent world features.

Reconcile unlocks after related Quest and Restoration state is available.

---

# Initialization and Time Advancement

Time should not advance while initialization is incomplete.

The Time System may exist, but ticking remains disabled until Gameplay begins.

Likewise, NPC simulation should not progress during loading.

---

# Simulation Gate

A central simulation gate may use Game State.

Example:

```csharp
if (!gameStateSystem.IsGameplayActive)
{
    return;
}
```

for systems that tick continuously.

Avoid every System inventing separate startup booleans.

---

# Initialization and Pausing

A loaded game should not briefly run simulation before the loading screen ends.

Sequence:

```text
Restore
    ↓
Initialize Scene
    ↓
Synchronize
    ↓
Enable Simulation
```

---

# Initialization and Autosave

Do not autosave during partial initialization.

Autosave becomes available after:

```text
SessionInitialized
```

or:

```text
LoadCompleted
```

If migration writes an upgraded Save, that is a dedicated migration write rather than normal autosave behavior.

---

# Initialization and Achievements

Achievement reconciliation should occur after authoritative progression is restored.

It should not interfere with migration.

Possible flow:

```text
Load Completed
    ↓
Achievement System evaluates persistent progression
    ↓
Platform Service reconciles unlocked Achievements
```

---

# Initialization and Tutorials

Tutorial state should restore before tutorial triggers become active.

Otherwise loading into an established Save could replay completed tutorials.

---

# Initialization and Notifications

Gameplay notifications should generally be disabled during restoration.

Do not display:

```text
Quest Completed!
Friendship Increased!
Recipe Unlocked!
```

simply because those values were reconstructed from a Save.

Only new runtime changes should create normal notifications.

---

# Initialization and Cinematics

When loading into a one-time cinematic trigger:

- Persistent event state must be restored first.
- Availability must be evaluated after load.
- Cinematic should only begin if it is genuinely pending.
- Completed Events must not replay due solely to scene initialization.

---

# First Scene After New Game

The New Game intro may use a special initialization branch.

Example:

```text
Core session ready
    ↓
Intro carriage scene
    ↓
Avatar Creation
    ↓
Pendrelle Manor arrival scene
    ↓
Gameplay state
```

Persistent Systems can already exist during the intro.

Scene presentation changes as the intro progresses.

---

# First Playable Moment

Define one explicit boundary:

```text
First Playable Moment
```

At this point:

- Session state is valid.
- Player identity exists.
- Scene is valid.
- Player View exists.
- Required NPCs exist.
- HUD is synchronized.
- Input is enabled.
- Simulation is enabled.
- Save state is coherent.

This boundary should be easy to identify during debugging.

---

# Application Lifetime

Application-lifetime dependencies exist until the executable closes.

Examples:

- Logging.
- Platform Services.
- Application Settings.
- Save Storage.
- Localization.
- Global static Data Registries.

They initialize once.

---

# Session Lifetime

Session-lifetime dependencies exist while one Save/New Game session is active.

Examples:

- Inventory System.
- Economy System.
- Calendar System.
- Weather System.
- Quest System.
- Relationship Systems.
- Farming System.
- Restoration System.

They initialize when a gameplay session starts.

They reset or dispose when leaving the session.

---

# Scene Lifetime

Scene-lifetime dependencies exist only while a specific scene is loaded.

Examples:

- Scene Context.
- Scene Controllers.
- World Views.
- NPC GameObjects.
- Resource Views.
- Scene-specific Audio.
- Camera presentation.

---

# UI Lifetime

UI lifetime may vary.

Examples:

Application lifetime:

- Title UI infrastructure.

Session lifetime:

- HUD root.

Temporary:

- Inventory Menu.
- Dialogue Panel.
- Day End Screen.

UI lifetime should not control gameplay System lifetime.

---

# Initialization Ownership

Each object should have one clear initialization owner.

Examples:

- Application Bootstrap initializes Core.
- Composition Root constructs Services and Systems.
- Save System coordinates Save restoration.
- Scene System loads scenes.
- Scene Context coordinates scene references.
- Controllers initialize Views.
- Systems own their internal state initialization.

Avoid multiple unrelated objects all trying to initialize the same dependency.

---

# Double Initialization

Components should protect against accidental repeated initialization when repeated setup would be harmful.

Example:

```csharp
public void Initialize(...)
{
    if (_isInitialized)
    {
        throw new InvalidOperationException(
            "Controller already initialized.");
    }

    _isInitialized = true;
}
```

Whether to throw or safely return depends on component expectations.

Critical architecture should detect duplicate initialization during development.

---

# Reinitializable Components

Some components intentionally initialize repeatedly.

Examples:

- Scene Controllers after each scene load.
- Temporary UI screens.
- Festival presentation.

Document this lifecycle explicitly.

---

# Initialization Interfaces

Possible interfaces:

```csharp
public interface IInitializable
{
    void Initialize();
}

public interface IAsyncInitializable
{
    Task InitializeAsync();
}

public interface IShutdownable
{
    void Shutdown();
}
```

Use these only if they simplify orchestration.

Do not force unrelated initialization patterns into one generic contract.

---

# Initialization Results

Complex initialization steps may return Results.

Example:

```csharp
public readonly struct InitializationResult
{
    public bool Success { get; }
    public InitializationFailureReason FailureReason { get; }
}
```

Useful for:

- Platform initialization.
- Save loading.
- Registry validation.
- Scene loading.

---

# Exceptions vs Results

Use exceptions for programming/configuration errors during development.

Examples:

- Duplicate Data ID.
- Missing required registry.
- Impossible configuration.

Use Results for expected operational failures.

Examples:

- Save file missing.
- Save corrupted.
- Cloud service unavailable.
- Scene load failed.

Release handling may convert exceptions into safe failure Results at architectural boundaries.

---

# Dependency Readiness

Avoid methods such as:

```csharp
FindObjectOfType<T>()
```

as a substitute for initialization readiness.

Explicitly provide required dependencies.

A component should know when it is initialized.

---

# Lazy Initialization

Lazy initialization may be useful for optional features.

Example:

```text
Photo Mode Service
```

only initializes when opened.

Core gameplay Systems should generally initialize explicitly at session startup.

Avoid lazy initialization when it creates hidden ordering.

---

# Initialization Performance

Initialization should prioritize correctness and clarity.

However:

- Load large content efficiently.
- Avoid repeated registry building.
- Avoid repeated static validation in release when unnecessary.
- Avoid loading unused scene assets eagerly.
- Consider Addressables later if content scale requires it.

Do not prematurely optimize by making initialization unpredictable.

---

# Initialization Profiling

Useful profiling categories:

- Static Data loading.
- Registry building.
- Save deserialization.
- Save migration.
- System restoration.
- Scene loading.
- NPC spawning.
- World synchronization.
- UI initialization.

Measure before introducing complexity.

---

# Initialization Logging

Development logs should include major stages.

Example:

```text
[Init] Core initialized.
[Init] 412 Item definitions registered.
[Init] 31 NPC definitions registered.
[Init] Services initialized.
[Init] Gameplay Systems initialized.
[Load] Save Format V3 detected.
[Load] Migrated V3 → V4.
[Load] Persistent state restored.
[Scene] Blackmere loaded.
[Init] Scene synchronized.
[Init] Gameplay ready.
```

Avoid logging every tiny field.

---

# Initialization Diagnostics

For debugging, maintain a report of:

- Initialized dependency.
- Initialization duration.
- Failure.
- Missing dependency.
- Registry counts.
- Save Format.
- Scene ID.
- Session type.

This can dramatically simplify startup bugs.

---

# Initialization Tests

Initialization should support automated tests.

Important tests:

- New Game initializes successfully.
- Current-version Save loads.
- Old-version Save migrates and loads.
- Missing required registry fails.
- Duplicate IDs fail validation.
- Missing optional content does not crash.
- Scene initializes after state restoration.
- UI receives initial snapshots.
- No normal gameplay notifications occur during load.
- Event subscriptions are not duplicated.
- Return to Title clears session state.
- Loading a second Save does not retain first Save state.
- Scene transition preserves persistent Systems.
- NPC routines resolve correctly after load.
- Day End reinitializes daily state correctly.

---

# Initialization Integration Test

Example:

```text
Boot Test Application
    ↓
Initialize New Game
    ↓
Assert GameState = Gameplay
    ↓
Assert Calendar initialized
    ↓
Assert Inventory initialized
    ↓
Assert HUD synchronized
    ↓
Assert Player spawned
```

---

# Load Integration Test

Example:

```text
Create Fixture Save
    ↓
Boot
    ↓
Load Save
    ↓
Assert persistent state restored
    ↓
Assert scene loaded
    ↓
Assert Player position restored
    ↓
Assert NPC routines resolved
    ↓
Assert no duplicate Events
    ↓
Assert Gameplay active
```

---

# Session Reset Test

Example:

```text
Load Save A
    ↓
Record state
    ↓
Return to Title
    ↓
Load Save B
    ↓
Assert no Save A state remains
```

This test is important for long application sessions.

---

# Initialization Anti-Patterns

## Unity Awake Ordering as Architecture

Avoid relying on unrelated `Awake()` calls occurring in a specific order.

---

## Script Execution Order Everywhere

Avoid using Unity Script Execution Order to manage broad gameplay dependencies.

---

## Global Find Calls

Avoid:

```csharp
FindObjectOfType<InventorySystem>();
```

throughout startup.

Use explicit dependency wiring.

---

## UI Before State

Avoid initializing UI before authoritative Systems have state.

---

## Gameplay Events During Load

Avoid firing normal progression notifications while restoring a Save.

---

## Scene-Owned Persistent Systems

Avoid recreating authoritative Inventory, Quest, Relationship, or Calendar state every scene.

---

## Duplicate Persistent Systems

Avoid one persistent System in the bootstrap scene and another copy inside gameplay scenes.

---

## Controller-Owned Startup State

Avoid making individual Controllers decide whether the entire game is ready.

Use centralized initialization and Game State.

---

## Save Restoration Through Normal Gameplay APIs

Avoid reconstructing Save state by replaying:

```text
AddItem
GainFriendship
CompleteQuest
```

unless those APIs have explicit silent restoration semantics.

Use dedicated restore methods.

---

## Hidden Lazy Dependencies

Avoid a System that looks initialized but only fails later when it first tries to find an uninitialized Service.

---

## Event-Only Initialization

Avoid assuming components will become correct because they eventually receive Events.

Initial state must be synchronized explicitly.

---

## Partial Gameplay Activation

Avoid enabling movement before:

- UI.
- NPCs.
- Weather.
- interactions.

are ready.

---

## Reset by Scene Reload Alone

Reloading a scene does not reset session-lifetime Systems.

Explicitly reset or recreate them.

---

## Silent Critical Failures

Do not continue into gameplay after missing critical data.

---

## Repeated Initialization Without Cleanup

Scene-lifetime Controllers should clean subscriptions and registrations before being initialized again.

---

# Initialization Design Checklist

Before adding a new component, determine:

1. What is its lifetime?
2. Who creates it?
3. Who initializes it?
4. What dependencies must already exist?
5. Does it require static data?
6. Does it require a Registry?
7. Does it require a Service?
8. Does it require another System?
9. Does it require Save state?
10. Does it require a loaded scene?
11. Does it require a Player GameObject?
12. Does it require UI?
13. When does it subscribe to Events?
14. When does it unsubscribe?
15. Does it need initial snapshot synchronization?
16. Can it process Events during Save restoration?
17. Should it suppress normal notifications during initialization?
18. Does it initialize for New Game?
19. Does it initialize for Load Game?
20. Does it initialize again on scene transition?
21. Does it persist across scene transitions?
22. How is it reset when returning to Title?
23. Can it be initialized twice accidentally?
24. Does it need async initialization?
25. Can initialization fail?
26. How is failure represented?
27. Is failure critical or optional?
28. Can the application safely recover?
29. Does it need validation?
30. Does it depend on Unity lifecycle ordering?
31. Can that dependency be made explicit?
32. Does it begin simulation before Gameplay state?
33. Does it create runtime state that belongs to another owner?
34. Can it be tested independently?
35. Is initialization order documented clearly enough to debug later?

---

# Initialization Rules

- Initialize foundational dependencies before higher-level dependencies.
- Keep initialization explicit and deterministic.
- Use a central bootstrap or composition root for major dependency wiring.
- Do not rely on arbitrary Unity `Awake()` ordering.
- Do not use Script Execution Order as the primary architecture.
- Load and validate static definitions before Systems that require them.
- Initialize registries before resolving stable Data IDs.
- Initialize Services before Systems that depend on them.
- Construct Systems before creating or restoring session state.
- Separate System construction from state restoration.
- Migrate Saves before restoring gameplay Systems.
- Validate migrated Save Data before gameplay restoration.
- Restore authoritative state before scene presentation.
- Suppress normal gameplay reactions during partial restoration.
- Rebuild derived runtime state after persistent state is restored.
- Perform cross-System reconciliation explicitly.
- Load scenes only after required session state exists.
- Register scene objects before scene Controllers depend on them.
- Initialize Controllers only after their Systems and scene references exist.
- Synchronize Views and UI from current snapshots.
- Do not rely on past Events to establish initial presentation.
- Subscribe listeners only when they are ready to process future Events.
- Match Event subscription lifetime to object lifetime.
- Prevent duplicate subscriptions.
- Keep gameplay Input disabled until final initialization succeeds.
- Keep Time and simulation paused during loading.
- Enter Gameplay state only after final synchronization.
- Use a shorter scene initialization pipeline for normal scene transitions.
- Keep persistent Systems alive across scene transitions.
- Explicitly tear down scene-lifetime registrations before unloading.
- Explicitly reset or recreate session-lifetime state before loading another Save.
- Keep application-lifetime Services separate from session-lifetime gameplay Systems.
- Fail safely when critical initialization fails.
- Preserve Save data when loading or migration fails.
- Log major initialization stages in development.
- Test New Game, Load Game, scene transition, and session reset paths.
- Treat initialization order as an architectural contract rather than incidental Unity behavior.

---

# Recommended High-Level Order

```text
APPLICATION
|
|-- 1. Bootstrap
|-- 2. Logging / Platform / Settings
|-- 3. Static Configurations
|-- 4. Content Definitions
|-- 5. Registries
|-- 6. Core Services
|-- 7. Domain Services
|-- 8. Persistent Gameplay Systems
|
SESSION
|
|-- 9. New Game OR Read Save
|-- 10. Save Migration
|-- 11. Save Validation
|-- 12. Persistent State Restoration
|-- 13. Derived State Reconstruction
|-- 14. Cross-System Reconciliation
|
SCENE
|
|-- 15. Load Scene
|-- 16. Register Scene Objects
|-- 17. Initialize Player Presentation
|-- 18. Initialize NPC Presentation
|-- 19. Initialize Scene Controllers
|-- 20. Initialize Views
|-- 21. Initialize UI
|-- 22. Establish Scene Event Subscriptions
|-- 23. Final Presentation Synchronization
|
READY
|
|-- 24. Enable Simulation
|-- 25. Enable Gameplay Input
|-- 26. Enter Gameplay State
```

---

# Related Code Setup Notes

- Controllers
- Data IDs
- Dependencies
- Enums
- Event Channels
- Game Architecture
- Game Flags
- Models
- Save Data
- Save Versioning
- Scriptable Objects
- Services

---

# Related System Notes

- Game State System
- Save System
- Scene System
- Time System
- Calendar System
- Weather System
- NPC Routine System
- Day End System
- System Interaction Rules
- Individual System documentation
