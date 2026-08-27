---
Title: Systems / Dialogue System
Game: As The Bell Tolls
Owner: Carey Clement Jr
Created: July, 2026
Updated: August, 2026
Version: 0.0.1
---

## Information

**<u>Activating Mechanics:</u>** When the player is adjacent and facing a non-player character (NPC), they can interact with that character. Once the player interacts with the NPC, it will open the Dialogue Panel and the NPC will start to speak.

**<u>Mechanic:</u>** Once spoken to, the NPC will have a piece of dialogue that they will speak toward the player. This can include an initial greeting (if meeting for the first time), a reaction to a quest, a tip to help the player, or even just a generic comment about the weather, time of day, or friendship level. Once stated, this piece of dialogue will be added to a list of three "recently used" dialogue IDs for the specific character. When speaking next to the character, they will be unable to say one of those dialogue pieces again, reducing the amount of potential repetition. After this first piece of dialogue, the player will have certain options, depending on the context with that NPC. Any further action could be taken, or the player could leave the conversation.

---

## Speech Audio Information

- Each character uses one of four base clip sets
    - Female A (softer / lighter)
    - Female B (fuller / deeper)
    - Male A (softer / lighter)
    - Male B (fuller / deeper)
- Pitch is randomized from base pitch plus/minus pitch variation
- Volume is randomized from base volume plus/minus volume variation (clamped between 0 and 1)
- Interval is pausing between vocalizations
- Audio is able to be adjusted in the settings menu

---

## Example Flows

### Meeting Nora Reed for the First Time

- Player interacts with Nora Reed.
- Dialogue Panel appears with an initial meeting dialogue piece from Nora.
- Dialogue Panel shows options of Give Gift or Leave Conversation.
- Player selects Leave Conversation.

### Giving Walter Pierce a Gift

- Player interacts with Walter Pierce.
- Dialogue Panel appears with a generic dialogue piece from Walter.
- Dialogue Panel shows options of Give Gift or Leave Conversation.
- Player selects Give Gift.
- Give Gift Panel opens to show available items that can be gifted.
- Player selects an item and confirms to give to Walter.
- Dialogue Panel appears with a gift response dialogue piece from Walter.
- Dialogue Panel shows option of Leave Conversation.
- Player selects Leave Conversation.

### Completing a Quest for Edward Ashcroft

- Player interacts with Edward Ashcroft.
- Dialogue Panel appears with a generic dialogue piece from Edward.
- Dialogue Panel shows options of Complete Quest, Give Gift, or Leave Conversation.
- Player selects Complete Quest.
- Dialogue Panel appears with a quest response dialogue piece from Edward.
- Dialogue Panel shows options of Give Gift or Leave Conversation.
- Player selects Leave Conversation.

### Using the Stove with Theo Bennett

- Player enters Winding Banks Inn.
- Player interacts with Theo Bennett.
- Dialogue Panel appears with a generic dialogue piece from Theo.
- Dialogue Panel shows options of Cook with Theo, Give Gift, or Leave Conversation.
- Dialogue Panel appears with a cooking response dialogue piece from Theo.
- Player completes cooking action.
