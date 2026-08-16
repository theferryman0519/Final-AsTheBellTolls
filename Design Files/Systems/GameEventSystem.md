---
Title: Systems / Game Event System
Game: As The Bell Tolls
Owner: Carey Clement Jr
Created: August, 2026
Updated: August, 2026
Version: 0.0.1
---

## Information

- Game Events are scripted events that occur when their required conditions have been met.
- Game Events include Main Story events, NPC Friendship events, NPC Connection events, tutorials, restoration-related events, and other scripted sequences.
- Festivals are managed through the Festival System but may trigger Game Events such as introductions, competitions, or cinematics.
- Game Events may contain dialogue, NPC movement, player movement, animations, camera changes, audio changes, item rewards, relationship changes, quest changes, and other gameplay actions.
- Only one cinematic Game Event can be actively playing at a time.
- Game Events that have already been completed cannot normally be triggered again unless the event is specifically marked as repeatable.

---

## Event Requirements

A Game Event may require one or more of the following conditions:

- Main Story progression
- Friendship Heart level
- Connection Key level
- Quest state
- Previous Game Event completion
- Manor Restoration progress
- Town Restoration progress
- Season
- Day
- Day of the Week
- Time of Day
- Daylight State
- Weather
- Location
- NPC availability
- Player relationship status
- Marriage status
- Item possession
- Game Flag

---

## Event States

| State | Information |
|---|---|
| Inactive | Requirements for the Game Event have not been met. |
| Eligible | Requirements have been met and the Game Event can be triggered. |
| Playing | The Game Event is currently occurring. |
| Completed | The Game Event has been completed. |

---

## Event Completion

Completing a Game Event may:

- Set or update a Game Flag.
- Start, update, or complete a Quest.
- Award an Item.
- Award or remove Bellnotes.
- Award Friendship Points.
- Award Connection Points.
- Unlock dialogue.
- Unlock an Invention.
- Unlock a Blueprint.
- Unlock a location or feature.
- Change an NPC Routine.
- Allow another Game Event to become eligible.