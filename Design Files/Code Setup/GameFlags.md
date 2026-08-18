---
Title: Code Setup / Game Flags
Game: As The Bell Tolls
Owner: Carey Clement Jr
Created: August, 2026
Updated: August, 2026
Version: 0.0.1
---

## Information

* Game Flags are persistent values used to remember important facts that do not naturally belong to another authoritative Gameplay System.
* Game Flags are primarily used for story progression, one-time occurrences, player decisions, narrative consequences, conditional Dialogue, and special Game Event requirements.
* Most Game Flags are Boolean values representing whether something has or has not occurred.
* Game Flags persist within the player's Save File.
* Game Flags may be read by Game Events, Dialogue, Quests, NPC Routines, Mail, Festivals, Tutorials, and other systems when determining eligibility or presentation.
* Game Flags should not replace authoritative state already owned by another System.
* Game Flags should not be used simply because a condition needs to be checked.
* A Game Flag should generally represent a fact that must be remembered independently after the action or event that created it has passed.

---

# Namespace

Game Flag definitions belong under:

`AsTheBellTolls.GameFlow.Flags`

Examples:

```text
AsTheBellTolls.GameFlow.Flags.GameFlagId
AsTheBellTolls.GameFlow.Flags.GameFlagState
AsTheBellTolls.GameFlow.Flags.GameFlagCollection
```

Game Flags may be queried throughout the game, but their persistent values should have one authoritative owner.

---

# Primary Purpose

Game Flags answer questions such as:

* Has this important narrative moment occurred?
* Has the player already seen this one-time sequence?
* Did the player make a particular story decision?
* Was a special condition established that another System does not own?
* Has a specific narrative fact been revealed to the player?
* Did the player choose one branch over another?
* Has a one-time world reaction already occurred?
* Should special Dialogue now be available?

Game Flags should generally **not** answer questions such as:

* What day is it?
* What Season is it?
* How many Friendship Hearts does Adrian have?
* Has a Quest been completed?
* What Restoration stage is the Manor?
* Is the player married?
* Does the player own a Fishing Rod?
* Has an Invention been crafted?
* Has a Game Event been completed?

Those answers already belong to other Gameplay Systems.

---

# Game Flag Format

Game Flag IDs use:

```text
flag_<category>_<description>
```

Examples:

```text
flag_story_intro-completed
flag_story_repossession-notice-read
flag_choice_family-discussion-accepted
flag_dialogue_myrtle-clock-secret-known
flag_world_blackmere-bell-first-rung
```

Rules:

* Use lowercase letters.
* Use a `flag_` prefix.
* Use an underscore between the category and description.
* Use hyphens between words within the description.
* Describe the persistent fact rather than the action that checks it.
* Avoid vague names such as `flag_story_01`.
* Avoid names tied to implementation details.
* A Flag ID should never be renamed after release without a Save migration.

---

# Story Flags

Story Flags represent significant narrative facts that need to remain known independently of individual Quest or Game Event state.

## Intro Completed

ID:

`flag_story_intro-completed`

Set When:

* The opening carriage sequence has completed.
* Avatar Creation has completed.
* Edward's introduction at Pendrelle Manor has completed.
* Normal Gameplay begins on Day 1 of Spring, Year 1.

Used For:

* Preventing the opening sequence from playing during normal Gameplay.
* Determining whether the Save File has entered normal Gameplay.
* Supporting special early-game logic if required.

---

## Myrtle's Will Read

ID:

`flag_story_myrtle-will-read`

Set When:

* The player reads Myrtle Pendrelle's Last Will and Testament during the introduction.

Used For:

* Narrative references to the player knowing they inherited Pendrelle Manor.
* Conditional Dialogue involving Myrtle's will or inheritance.

---

## Repossession Notice Read

ID:

`flag_story_repossession-notice-read`

Set When:

* The player reads the Conditional Repossession notice from the Office of the Vice Regent of Morvanya.

Used For:

* Dialogue regarding Blackmere's three-year recovery deadline.
* Story events involving the regional government.
* Rupert Munro-related Dialogue or events.

---

## Blackmere Recovery Crisis Known

ID:

`flag_story_blackmere-recovery-crisis-known`

Set When:

* Edward explains that Blackmere and Pendrelle Manor face repossession if they are deemed beyond recovery.

Used For:

* Story Dialogue.
* Early Restoration Dialogue.
* Regional government events.

---

## Grandfather Clock Secret Discovered

ID:

`flag_story_grandfather-clock-secret-discovered`

Set When:

* The player first discovers that Myrtle's grandfather clock possesses unusual properties connected to Time Manipulation.

Used For:

* Story Dialogue.
* Edward Dialogue.
* Lucian/Vivian Dialogue.
* Game Events involving Myrtle or the clock.

Does Not:

* Unlock Time Manipulation itself.

The Time Manipulation System and Restoration progression determine whether the mechanic is usable.

---

## Myrtle's Connection to Time Manipulation Known

ID:

`flag_story_myrtle-time-secret-known`

Set When:

* The player learns that Myrtle knowingly understood or used the grandfather clock's abilities.

Used For:

* Later Main Story events.
* Lucian/Vivian Connection events.
* Edward Dialogue.
* Myrtle-related discoveries.

---

## Lucian/Vivian's Knowledge of the Clock Revealed

ID:

`flag_story_darrow-clock-knowledge-revealed`

Set When:

* Lucian/Vivian reveals that Myrtle entrusted them with knowledge concerning the grandfather clock.

Used For:

* Later Lucian/Vivian Dialogue.
* Main Story Dialogue.
* Endgame narrative references.

---

## Clock Stewardship Accepted

ID:

`flag_story_clock-stewardship-accepted`

Set When:

* The story establishes that the player understands the grandfather clock is something to use responsibly rather than a means of rewriting every mistake.

Used For:

* Endgame Dialogue.
* Lucian/Vivian's completed character arc.
* Myrtle-related story conclusions.

---

## Rupert First Visit Occurred

ID:

`flag_story_rupert-first-visit-occurred`

Set When:

* Rupert Munro makes his first story-related visit to Blackmere.

Used For:

* Later Rupert Dialogue.
* Town Dialogue reacting to his visit.
* Preventing introductory Rupert reactions from appearing afterward.

Note:

The Game Event System still owns whether the actual Rupert visit Game Event is completed.

This Flag represents the broader narrative fact that **Blackmere has now been observed by the regional government**.

---

## Government Recognizes Blackmere Recovery

ID:

`flag_story_blackmere-recovery-recognized`

Set When:

* The appropriate late-game story sequence establishes that Blackmere's recovery is formally recognized.

Used For:

* Endgame Dialogue.
* Town reactions.
* Regional government narrative.
* Post-story content.

---

# Choice Flags

Choice Flags remember decisions where the specific choice matters later.

They should not be created for every Dialogue response.

Only choices with future consequences require Flags.

---

## Player Chose to Begin Restoration

ID:

`flag_choice_restore-blackmere-accepted`

Set When:

* The player makes the narrative commitment to remain in Blackmere and participate in its recovery.

Used For:

* Main Story presentation if this becomes an explicit player decision.

If the player is always required to remain in Blackmere, this Flag is unnecessary.

---

## Family Discussion Accepted

ID:

`flag_choice_family-discussion-accepted`

Set When:

* Following the spouse's 15 Heart Event, the player chooses that they want to begin pursuing a family.

Used For:

* Family System narrative progression.
* Spouse Dialogue.
* Nursery-related events.

Does Not:

* Represent whether the player currently has children.

The Family System owns family state.

---

## Family Discussion Deferred

ID:

`flag_choice_family-discussion-deferred`

Set When:

* The player chooses not to begin pursuing a family during the first available discussion.

Used For:

* Preventing the same conversation from immediately repeating.
* Allowing an appropriate later follow-up.

This Flag may be cleared if the Family System deliberately allows the discussion to occur again.

---

# Dialogue Knowledge Flags

Dialogue Flags represent information the **player character has learned**.

These are useful when knowledge itself matters independently from progression.

---

## Myrtle Clock History Known

ID:

`flag_dialogue_myrtle-clock-history-known`

Set When:

* The player has been explicitly told enough information to understand the history between Myrtle and the grandfather clock.

Used For:

* Preventing NPCs from explaining information the player already knows.
* Unlocking more advanced discussions about the clock.

---

## Flood History Explained

ID:

`flag_dialogue_blackmere-flood-history-known`

Set When:

* The player has received the full narrative explanation of the flood and its effect on Blackmere.

Used For:

* Dialogue progression.
* Preventing repeated exposition.

---

## Repossession Process Explained

ID:

`flag_dialogue_repossession-process-known`

Set When:

* The player has received the detailed explanation of how the Conditional Repossession process works.

Used For:

* Beatrice Dialogue.
* Rupert Dialogue.
* Main Story Dialogue.

---

## Grand Showcase Explained

ID:

`flag_dialogue_grand-showcase-explained`

Set When:

* The player receives the complete explanation of the Grand Showcase.

Used For:

* Festival Dialogue.
* Invention Dialogue.
* Preventing repeated introductory explanations.

Does Not:

* Represent Grand Showcase participation or completion.

The Festival System owns those states.

---

# World Flags

World Flags represent unique world events that need to remain remembered but are not full progression states.

---

## Blackmere Bell First Rung

ID:

`flag_world_blackmere-bell-first-rung`

Set When:

* The player rings the Blackmere Bell for the first time.

Used For:

* First-time Dialogue.
* First-time presentation.
* Irene-related reactions.

Does Not:

* Track whether the daily Bell benefit has already been used.

A daily runtime state should handle that separately.

---

## Pendrelle Manor First Entered

ID:

`flag_world_pendrelle-manor-first-entered`

Set When:

* The player enters Pendrelle Manor for the first time during normal Gameplay.

Used For:

* One-time Manor introduction.
* Edward's early-game presentation.

---

## Ashfall Mines First Entered

ID:

`flag_world_ashfall-mines-first-entered`

Set When:

* The player enters Ashfall Mines for the first time.

Used For:

* First-time area presentation.
* Claudia Dialogue.
* Mine tutorial presentation.

Does Not:

* Determine whether Ashfall Mines are unlocked.

---

## Hall of Wonder First Entered

ID:

`flag_world_hall-of-wonder-first-entered`

Set When:

* The player enters the Hall of Wonder for the first time.

Used For:

* Dante introduction presentation.
* Museum introduction.

---

## Thread & Thimble First Entered

ID:

`flag_world_thread-and-thimble-first-entered`

Set When:

* The player enters Thread & Thimble for the first time.

Used For:

* Leo's return-related introduction.
* Store introduction presentation.

---

## Butler Tunnels First Used

ID:

`flag_world_butler-tunnels-first-used`

Set When:

* The player uses the Butler Tunnels for the first time.

Used For:

* First-use presentation.
* Edward Dialogue.

Does Not:

* Determine whether the Butler Tunnels are unlocked.

Restoration owns the unlock requirement.

---

# NPC Flags

NPC Flags should be used sparingly.

Most NPC information belongs to NPC Save Data, Friendship, Connection, Dialogue, Marriage, or Game Event state.

Do not create general Flags for:

```text
flag_npc_adrian-met
flag_npc_adrian-3-hearts
flag_npc_adrian-quest-completed
flag_npc_adrian-6-keys
```

Those facts already belong elsewhere.

NPC Flags are appropriate when an NPC-specific narrative fact must persist outside those systems.

Format:

```text
flag_npc_<character>_<fact>
```

Examples:

```text
flag_npc_edward_clock-secret-discussed
flag_npc_darrow_player-trusted-with-clock
flag_npc_rupert_blackmere-inspection-completed
```

---

## Edward Clock Secret Discussed

ID:

`flag_npc_edward_clock-secret-discussed`

Set When:

* Edward and the player have their first substantial conversation concerning Myrtle's grandfather clock.

Used For:

* Edward Dialogue progression.
* Clock-related Main Story events.

---

## Lucian/Vivian Trusts Player With Clock

ID:

`flag_npc_darrow_player-trusted-with-clock`

Set When:

* Lucian/Vivian's story reaches the point where they accept the player as a responsible steward of Myrtle's secret.

Used For:

* Post-Connection Dialogue.
* Endgame story reactions.
* Clock-related events.

This may occur alongside completion of Lucian/Vivian's Connection progression, but represents a specific narrative fact rather than the Connection level itself.

---

## Rupert Inspection Completed

ID:

`flag_npc_rupert_blackmere-inspection-completed`

Set When:

* Rupert has completed his formal inspection or evaluation of Blackmere.

Used For:

* Rupert's later Dialogue.
* Town reactions.
* Government story progression.

---

# Family Flags

Family Flags should only supplement the Family System.

The Family System remains authoritative for:

* Spouse
* Number of children
* Child data
* Pregnancy / adoption equivalent progression
* Nursery requirements
* Family progression

Possible narrative Flags include:

---

## First Family Conversation Seen

ID:

`flag_family_first-discussion-seen`

Set When:

* The first post-15-Heart family conversation has occurred.

Used For:

* Preventing the introductory conversation from repeating.
* Selecting later spouse Dialogue.

---

## First Child Introduction Seen

ID:

`flag_family_first-child-introduction-seen`

Set When:

* The one-time introduction sequence for the player's first child has completed.

Used For:

* Preventing introductory presentation from repeating.
* Post-event Dialogue.

---

## Second Child Introduction Seen

ID:

`flag_family_second-child-introduction-seen`

---

## Third Child Introduction Seen

ID:

`flag_family_third-child-introduction-seen`

---

## Fourth Child Introduction Seen

ID:

`flag_family_fourth-child-introduction-seen`

These Flags only control one-time narrative presentation.

The Family System owns the actual children.

---

# Festival Flags

The Festival System should own:

* Festival availability
* Festival date
* Festival attendance
* Festival completion
* Festival competition results
* Festival participation state

Game Flags are only needed for special persistent narrative facts surrounding Festivals.

---

## Grand Showcase First Entered

ID:

`flag_festival_grand-showcase-first-entered`

Set When:

* The player participates in the Grand Showcase for the first time.

Used For:

* One-time introductory presentation.
* NPC Dialogue.

---

## Toll of Hearths First Hosted

ID:

`flag_festival_toll-of-hearths-first-hosted`

Set When:

* The player hosts Toll of Hearths at Pendrelle Manor for the first time.

Used For:

* First-host Dialogue.
* Edward reactions.
* Manor narrative.

---

## River Remembrance First Attended

ID:

`flag_festival_river-remembrance-first-attended`

Set When:

* The player attends River Remembrance Day for the first time.

Used For:

* First-time memorial Dialogue.
* Later Dialogue that assumes the player understands the ceremony.

---

## Hollow Moon First Attended

ID:

`flag_festival_hollow-moon-first-attended`

Set When:

* The player attends Hollow Moon Night for the first time.

---

## Snow Bells Eve First Attended

ID:

`flag_festival_snow-bells-eve-first-attended`

Set When:

* The player attends Snow Bells Eve for the first time.

---

# Tutorial Flags

Tutorial Quests are owned by the Quest System.

Game Flags should therefore not duplicate Tutorial Quest completion.

Use Tutorial Flags only for tutorial presentation that exists independently of a Quest.

---

## Movement Tutorial Seen

ID:

`flag_tutorial_movement-seen`

---

## Interaction Tutorial Seen

ID:

`flag_tutorial_interaction-seen`

---

## Tool Selection Tutorial Seen

ID:

`flag_tutorial_tool-selection-seen`

---

## Player Menu Tutorial Seen

ID:

`flag_tutorial_player-menu-seen`

---

## Restoration Tutorial Seen

ID:

`flag_tutorial_restoration-seen`

---

## Relationship Tutorial Seen

ID:

`flag_tutorial_relationships-seen`

---

## Time Manipulation Tutorial Seen

ID:

`flag_tutorial_time-manipulation-seen`

---

# Initial Game Flag Catalog

## Story

* `flag_story_intro-completed`
* `flag_story_myrtle-will-read`
* `flag_story_repossession-notice-read`
* `flag_story_blackmere-recovery-crisis-known`
* `flag_story_grandfather-clock-secret-discovered`
* `flag_story_myrtle-time-secret-known`
* `flag_story_darrow-clock-knowledge-revealed`
* `flag_story_clock-stewardship-accepted`
* `flag_story_rupert-first-visit-occurred`
* `flag_story_blackmere-recovery-recognized`

## Choices

* `flag_choice_restore-blackmere-accepted`
* `flag_choice_family-discussion-accepted`
* `flag_choice_family-discussion-deferred`

## Dialogue Knowledge

* `flag_dialogue_myrtle-clock-history-known`
* `flag_dialogue_blackmere-flood-history-known`
* `flag_dialogue_repossession-process-known`
* `flag_dialogue_grand-showcase-explained`

## World

* `flag_world_blackmere-bell-first-rung`
* `flag_world_pendrelle-manor-first-entered`
* `flag_world_ashfall-mines-first-entered`
* `flag_world_hall-of-wonder-first-entered`
* `flag_world_thread-and-thimble-first-entered`
* `flag_world_butler-tunnels-first-used`

## NPC

* `flag_npc_edward_clock-secret-discussed`
* `flag_npc_darrow_player-trusted-with-clock`
* `flag_npc_rupert_blackmere-inspection-completed`

## Family

* `flag_family_first-discussion-seen`
* `flag_family_first-child-introduction-seen`
* `flag_family_second-child-introduction-seen`
* `flag_family_third-child-introduction-seen`
* `flag_family_fourth-child-introduction-seen`

## Festivals

* `flag_festival_grand-showcase-first-entered`
* `flag_festival_toll-of-hearths-first-hosted`
* `flag_festival_river-remembrance-first-attended`
* `flag_festival_hollow-moon-first-attended`
* `flag_festival_snow-bells-eve-first-attended`

## Tutorials

* `flag_tutorial_movement-seen`
* `flag_tutorial_interaction-seen`
* `flag_tutorial_tool-selection-seen`
* `flag_tutorial_player-menu-seen`
* `flag_tutorial_restoration-seen`
* `flag_tutorial_relationships-seen`
* `flag_tutorial_time-manipulation-seen`

---
