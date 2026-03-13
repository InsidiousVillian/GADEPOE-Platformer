# Ruins Above the Storm

<div align="center">
  <h1>Ruins Above the Storm</h1>
  <p><i>A precision-focused 3D isometric platformer set above the lethal desert clouds.</i></p>
  
  <img src="https://i.pinimg.com/originals/d9/0d/f5/d90df5a539713414ca2c2bafd396e5ee.gif" width="400px" alt="Ruins Above the Storm Gameplay Demo">

  <br>
  <p>
    <b>Precision Jumping</b> • <b>Isometric Navigation</b> • <b>Environmental Storytelling</b>
  </p>
</div>

---

### 🏜️ Project Overview
**Ruins Above the Storm** is a 3D isometric platformer built in Unity. Players navigate precarious floating ruins suspended above a lethal sandstorm, using spatial awareness and precision timing to reach a summit shrine.

* **Inspirations:** *Super Mario 3D World* (Grid Clarity) and *Journey* (Visual Signposting).
* **Core Goal:** Ascend a high-stakes vertical environment where traversal is the primary puzzle.

---

### 🎮 Gameplay & Mechanics
* **Isometric Movement:** Camera-relative system where input is mapped to the angled perspective.
* **Precision Jumping:** Variable jump heights controlled by input duration for mid-air micro-adjustments.
* **Momentum Physics:** Custom gravity and inertia handling to ensure every landing feels weighted and intentional.
* **Environmental Hazards:** Includes collapsing floors and wind-affected platforms.

---

### 🛠️ Technical Implementation

**Dialogue System (Queue ADT)**
* **Data-Driven:** Narrative content loaded from external JSON via `Resources`.
* **Typewriter Effect:** Character-by-character reveal using C# Coroutines for improved "juice."
* **Trigger-Based:** 3D Colliders decouple narrative logic from hard-coded sequences.

**Character Controller**
* Physics-based system optimized for slopes, friction, and terminal velocity.
* Fixed-angle camera designed to maximize the player's field of vision for hazard detection.

---

### 📂 Structure & Design
* **Scripts/**: Movement, Dialogue Queue, and Physics-based Trigger logic.
* **Resources/**: External `dialogue.json` data for easy content iteration.
* **Design Philosophy:** Focuses on "Grid-Based Clarity" to help players measure distances and "Ancient Shrine" checkpoints to balance difficulty.
