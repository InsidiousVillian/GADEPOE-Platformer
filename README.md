# Ruins Above the Storm

<div align="center">
  <h1>Ruins Above the Storm</h1>
  <p><i>A precision-focused 3D isometric platformer set above the lethal desert clouds.</i></p>
  
  <img src="https://i.pinimg.com/originals/e7/d7/d4/e7d7d4b15db6b9929191e4c27fdba49b.gif" width="400px" alt="Ruins Above the Storm Gameplay Demo">

  <br>
  <p>
    <b>Precision Jumping</b> • <b>Isometric Navigation</b> • <b>Environmental Storytelling</b>
  </p>
</div>

---

### 🏜️ Project Overview
**Ruins Above the Storm** is a 3D isometric platformer built in Unity. Navigate precarious floating ruins suspended above a lethal sandstorm, using spatial awareness and precision timing to reach a summit shrine.

* **Inspirations:** *Super Mario 3D World* (Clarity) and *Journey* (Signposting).
* **Core Goal:** Master a vertical environment where every movement is calculated against physics-based constraints.

---

### 🎮 Gameplay & Mechanics
* **Isometric Movement:** Camera-relative system where "Up" moves the character toward the top-right.
* **Precision Jumping:** Variable jump heights based on input duration.
* **Momentum Physics:** Custom gravity and inertia handling for weighted "landing feel."
* **Environmental Hazards:** Collapsing floors and wind-affected platforms.

---

### 🛠️ Technical Implementation

**Dialogue System (Queue ADT)**
* **Data-Driven:** JSON-loaded dialogue via `Resources`.
* **Typewriter Effect:** Character-by-character reveal using C# Coroutines.
* **Trigger-Based:** 3D Colliders decouple narrative logic from hard-coding.

**Character Controller**
* Physics-based system handling slopes and terminal velocity.
* Fixed-angle camera for optimized field of vision.

---

### 📂 Structure & Design
* **Scripts/**: Movement, Dialogue Queue, and Trigger logic.
* **Resources/**: External `dialogue.json` data.
* **Design Philosophy:** Uses "Grid-Based Clarity" for distance measurement and "Ancient Shrine" checkpoints for balanced failure states.
