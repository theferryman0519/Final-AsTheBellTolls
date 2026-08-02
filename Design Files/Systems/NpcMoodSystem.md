---
Title: Systems / NPC Mood System
Game: As The Bell Tolls
Owner: Carey Clement Jr
Created: August, 2026
Updated: August, 2026
Version: 0.0.1
---

## Information

- NPC Moods can affect the Friendship and Connection Points earned with interactions.
- NPC Moods can change because of player actions or weather.
- Every NPC starts at Indifferent each morning.

---

## Mood Point Breakdown

| Mood Type   | Positive / Negative | Points Change
|-------------|--------------------|---|
| Angry       | Negative           | -3
| Sad         | Negative           | -2
| Tired       | Negative           | -1
| Indifferent | Neutral            | +0
| Content     | Positive           | +1
| Happy       | Positive           | +2
| In Love     | Positive           | +3

---

## Mood Shift Causes

| Event / Action        | Mood Shift
|-----------------------|---|
| Liked Weather         | Up 2
| Neutral Weather       | Stays
| Disliked Weather      | Down 2
| Favorite Gift         | Up 3
| Loved Gift            | Up 2
| Liked Gift            | Up 1
| Tolerated Gift        | Stays
| Disliked Gift         | Down 1
| Hated Gift            | Down 2
| Completed Daily Quest | Up 1
| Completed Quest       | Up 2
| Aurora Watch          | Up 3
| Shared Meal           | Up 2