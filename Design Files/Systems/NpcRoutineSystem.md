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

An NPC Routine entry may determine:

- Start Time
- End Time
- Location
- Destination
- Activity
- Idle Area
- Animation
- Interaction Availability

---

## Routine Priority

When multiple routines are available, higher-priority routines override lower-priority routines.

| Priority | Routine Type |
|---|---|
| 1 | Game Event |
| 2 | Festival |
| 3 | Quest |
| 4 | Special Routine |
| 5 | Weather Routine |
| 6 | Seasonal Routine |
| 7 | Standard Routine |

---

## NPC Navigation

- When an NPC's current Routine entry changes, the NPC Navigation System will move the NPC toward the new destination.
- Once the NPC reaches the destination, they will begin the assigned activity or idle behavior.
- NPCs may walk around or stand still within specified idle areas.
- NPC movement stops while the NPC is speaking with the player.
- NPC movement stops while the NPC is operating a shop from a stationary position.