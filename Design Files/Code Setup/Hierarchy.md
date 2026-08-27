---
Title: Code Setup / Hierarchy
Game: As The Bell Tolls
Owner: Carey Clement Jr
Created: August, 2026
Updated: August, 2026
Version: 0.0.1
---

## Code Structure

* Domain Name
    * Controller
    * Coordinator
    * Enum
    * Events
    * Models
    * Save Data
    * Scriptable Objects
    * Services

---

## Information

- Controller runs all the services, pulls scriptable objects into models, updates models, saves models into save data.
- Coordinator speaks to other domains' coordinators to call back into the controller.
- Enum holds the custom static variable options.
- Events are triggers that run controller and coordinator methods after actions take place.
- Models hold the updated realtime data for objects.
- Save Data stores the data needed to be pushed to a save file on the device.
- Scriptable Objects are objects with their static information.
- Services are the various methods that run functions for the domain.
