Ruins Above the Storm

<div align="center">
  <h1>Ruins Above the Storm</h1>
  <p><i>A precision-focused 3D isometric platformer set above the lethal desert clouds.</i></p>
  
  <img src="https://i.pinimg.com/originals/e7/d7/d4/e7d7d4b15db6b9929191e4c27fdba49b.gif" width="800px" alt="Ruins Above the Storm Gameplay Demo">

  <br>
  <p>
    <b>Precision Jumping</b> • <b>Isometric Navigation</b> • <b>Environmental Storytelling</b>
  </p>
</div>

---
Ruins Above the Storm is a precision-focused 3rd-person isometric 3D platformer built in Unity. Set atop the levitating remnants of an ancient desert civilization, players must navigate precarious floating ruins suspended above a lethal, bottomless sandstorm.

Drawing inspiration from the mechanical clarity of Super Mario 3D World and the atmospheric signposting of Journey, the game emphasizes spatial awareness, momentum-based physics, and environmental puzzle-solving.
🎮 Gameplay Overview

In a high-stakes vertical environment, players must ascend toward a summit shrine. The core loop centers on "Readability" and "Mechanical Flow"—challenging the player's ability to judge distances and timing within a fixed three-dimensional coordinate system.
Core Mechanics

    Isometric Movement: A camera-relative movement system where input is mapped to the angled perspective (e.g., "Up" moves the character toward the top-right).

    Precision Jumping: Variable jump heights controlled by input duration, allowing for micro-adjustments in mid-air.

    Momentum Physics: Utilizes Unity’s physics engine for gravity and inertia, making landing impact and ground friction critical to success.

    Environmental Hazards: Features collapsing floors and wind-affected platforms that test timing and pathfinding.

🛠️ Technical Implementation
Dialogue & Interaction System

The project includes a custom JSON-driven dialogue system that utilizes a Queue ADT (Abstract Data Type) to manage narrative flow.

    Data-Driven: Dialogue is stored in external JSON files and loaded via Resources.

    Typewriter Effect: A Coroutine-based UI system that reveals text character-by-character for improved "juice" and player engagement.

    Trigger-Based: Narrative events are fired through 3D Trigger Colliders, decoupling story beats from hard-coded sequences.

Character Controller

    Physics-based controller designed to handle slopes and terminal velocity.

    Fixed isometric camera perspective to provide a wide field of vision and visual signposting.

📂 Project Structure

    Scripts/: Contains the C# logic for character movement, dialogue queues, and trigger interactions.

    Resources/: Holds the dialogue.json data used for in-game NPC interactions and level guides.

    Prefabs/: Modular platforming pieces and "Ancient Shrine" checkpoints.

    Scenes/: Three distinct levels demonstrating a progression in platforming difficulty.

📖 Design Philosophy

    Grid-Based Clarity: Borrowed from Super Mario 3D World, the environment is designed to help players subconsciously measure jump distances.

    Visual Signposting: Inspired by Journey, the ultimate goal (the Summit Shrine) is often visible on the horizon to provide constant orientation.

    Failure States: The "Sandstorm Void" establishes a clear failure state. Falling triggers a respawn at the last activated "Ancient Shrine," maintaining a balance between challenge and progression.
