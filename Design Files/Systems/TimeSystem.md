---
Title: Systems / Time System
Game: As The Bell Tolls
Owner: Carey Clement Jr
Created: July, 2026
Updated: August, 2026
Version: 0.0.1
---

## Information

- Time pauses when the player is not in Gameplay input map (this includes open menus, partaking in minigames, interacting with objects and dialogue)
- Clock on HUD updates to the current time every 5 minutes.
- Clock settings can be toggled to show 12-Hour Format or 24-Hour Format.

---

## Player Bedtime

- The earliest the player can wake up is 6:00am.
- The latest the player can stay out is 12:00am when the player will faint.
- The player wakes up based on when they fall asleep:
    - If the player falls asleep before 11:00pm, they will wake up at 6:00am the next day.
    - If the player falls asleep between 11:00pm and 11:59pm, they will wake up at 7:00am the next day.
    - If the player faints at midnight, they will wake up at 8:00am the next day.

---

## Game Time vs. Real Time

| Game Time | Real Seconds | Real Time
|---|---|---|
| 1 Minute | 0.8 Seconds | 0.8 Seconds
| 10 Minutes | 8 Seconds | 8 Seconds
| 1 Hour | 48 Seconds | 48 Seconds
| 6 hours | 288 Seconds | 4 Minutes, 48 Seconds
| 12 Hours | 576 Seconds | 9 Minutes, 36 Seconds
| 1 Day | 864 Seconds | 14 Minutes, 24 Seconds
| 1 Week | 6,048 Seconds | 1 Hour, 40 Minutes, 48 Seconds
| 1 Season | 26,784 Seconds | 7 Hours, 26 Minutes, 24 Seconds
| 1 Year | 107,136 Seconds | 29 Hours, 45 Minutes, 36 Seconds
