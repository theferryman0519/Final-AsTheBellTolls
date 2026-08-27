---
Title: Systems / Restoration System
Game: As The Bell Tolls
Owner: Carey Clement Jr
Created: July, 2026
Updated: August, 2026
Version: 0.0.1
---

## Information

- Each building in Blackmere will have a restoration progression, each starting at Weathered stage.
- Each building in Blackmere will take 3 days to restore from one stage to the next, closing access to it for those 3 days (starting the next day).
- Each room at Pendrelle Manor will have a restoration progression, each starting at Weathered stage.
- Each room at Pendrelle Manor will restore from one stage to the next overnight, not affecting access.

---

## Progression States

- Weathered
- Rebuilding
- Recovering
- Renewed
- Growing
- Prospering
- Flourishing

---

## Town Restoration Progression

| Stage Progression         | Items Needed
|---------------------------|---|
| Weathered to Rebuilding   | 90 Wood, 50 Stone, 10 Clay
| Rebuilding to Recovering  | 25 Wood Beams, 25 Stone Bricks
| Recovering to Renewed     | 30 Wood Beams, 25 Stone Bricks, 10 Glass Panels, 5 Hinges
| Renewed to Growing        | 15 Gear Assemblies, 10 Precision Gears, 10 Support Frames
| Growing to Prospering     | 15 Precision Gears, 15 Reinforced Glass, 10 Energy Conduits
| Prospering to Flourishing | 25 Precision Gears, 20 Energy Conduits, 10 Arc Tubes, 5 Cobalt Cores

---

## Manor Room Restoration Progression

| Stage Progression         | Items Needed
|---------------------------|---|
| Weathered to Rebuilding   | 45 Wood, 25 Stone, 5 Clay
| Rebuilding to Recovering  | 10 Wood Beams, 10 Stone Bricks
| Recovering to Renewed     | 15 Wood Beams, 10 Stone Bricks, 5 Glass Panels
| Renewed to Growing        | 5 Gear Assemblies, 5 Precision Gears, 5 Support Frames
| Growing to Prospering     | 5 Precision Gears, 5 Reinforced Glass, 5 Energy Conduits
| Prospering to Flourishing | 10 Precision Gears, 10 Energy Conduits, 5 Arc Tubes

---

## Time Manipulation Integration

* Advance applies only after restoration resources are committed.
* Advance reduces remaining restoration days.
* Advance instantly resolves manor restoration rooms (but uses 2 Chimes to do so).
* Chimes never replace restoration materials.
