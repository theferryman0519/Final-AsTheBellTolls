---
Title: Systems / NPC Routine System
Game: As The Bell Tolls
Owner: Carey Clement Jr
Created: August, 2026
Updated: August, 2026
Version: 0.0.1
---

## Information

- Each NPC follows a routine that determines where they should be and what they should be doing throughout the day.
- NPC routines are separate from NPC navigation. The NPC Routine System determines the NPC's destination and activity while the NPC Navigation System determines how the NPC physically travels there.
- NPC routines may change depending on the season, day of the week, weather, festivals, quests, Game Events, and other gameplay conditions.
- NPCs may have different routines for workdays and non-workdays.
- NPCs may have special routines for specific days or circumstances.
- NPCs will follow their normal routine unless a higher-priority routine temporarily overrides it.
- NPCs will not follow their normal routine while participating in a cinematic Game Event.

---

## Routine Information

An NPC Routine entry for each day of the week (Sunday through Monday) starts at 6:00am and ends at 12:00am. Each segment of time for the Routine determines the NPC sleeping, traveling, or performing an activity at a specified location. Each routine segment is broken down into 10-minute intervals for full transparency, and the ability to track each NPC by game time "tick".

The block of routines for each NPC are generated in the format similar to the below example:

- Sunday
    * 6:00am
        1) Location: 1 Resident Lane
        2) Activity: Sleeping
    * 6:10am
        1) Location: 1 Resident Lane
        2) Activity: Sleeping
    * 6:20am
        1) Location: 1 Resident Lane
        2) Activity: Sleeping
    * 6:30am
        1) Location: 1 Resident Lane
        2) Activity: Waking up
    * 6:40am
        1) Location: 1 Resident Lane
        2) Activity: Having breakfast
    * 6:50am
        1) Location: 1 Resident Lane
        2) Activity: Having breakfast
    * 7:00am
        1) Location: 1 Resident Lane
        2) Activity: Leaving for Blackmere Town Hall
    * 7:10am
        1) Location: Blackmere
        2) Activity: Traveling
    * 7:20am
        1) Location: Blackmere
        2) Activity: Traveling
    * 7:30am
        1) Location: Blackmere
        2) Activity: Traveling
    * 7:40am
        1) Location: Blackmere Town Hall
        2) Activity: Arrives at Blackmere Town Hall
    
...

---

## Routine Priority

The above represents the base routine for each NPC. However, there are several priorities that take place that might alter sections of the base routine. These priorities are ranked and override lower-priority routines.

| Priority | Routine Type
|----------|--------------|
| 1        | Game Event
| 2        | Main Festival
| 3        | Mini Festival
| 4        | Ongoing Event
| 5        | Weather Override
| 6        | Flourishing Override
| 7        | Prospering Override
| 8        | Growing Override
| 9        | Renewed Override
| 10       | Recovering Override
| 11       | Rebuilding Override
| 12       | Interaction Override
| 13       | Base Routine

---

## NPC Navigation

- When an NPC's current Routine entry changes, the NPC Navigation System will move the NPC toward the new destination.
- Once the NPC reaches the destination, they will begin the assigned activity or idle behavior.
- NPCs may walk around or stand still within specified idle areas.
- NPC movement stops while the NPC is speaking with the player.
- NPC movement stops while the NPC is operating a shop from a stationary position.
