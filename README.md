This was a case study I've done for a company. I've really enjoyed creating this. I built a lightweight, turn-based clicker combat demo. Although it uses placeholder art, the core mechanics and code structure highlight solid software-engineering practices and performance optimizations.
Gameplay Mechanics
Team Selection

At the start of each match, the player drafts three “Combatant” characters from a pool of available classes (e.g. Mage, Archer, Warrior).

Turn-Based Rounds

Combat proceeds in discrete turns. On the player’s turn, you click one of your three combatants to execute its Attack() method against the AI’s active fighter.

After the player’s action resolves (damage, floating text, particle effects), control shifts to the AI.

AI Behavior

The AI selects one of the surviving player combatants at random and attacks. This random targeting keeps each encounter unpredictable and forces players to adapt.

Victory Conditions

Rounds continue until either all three player combatants or all enemy units are defeated.

Key Features
Click-to-Attack Interface: Intuitive point-and-click controls let players target any of their three combatants each turn.

Object Pooling: Frequently spawned objects—arrows, particle effects, floating-damage text—are managed by a generic pool to avoid expensive instantiation and GC spikes.

Expandable Combatant Hierarchy:

An abstract Combatant base class holds references to a ScriptableObject (stats, cooldowns) and a target Transform.

Shared methods (Attack(), TakeDamage()) handle core logic and trigger UI feedback (floating text, sounds).

Subclasses override movement and effect-spawning hooks for specialized behaviors (e.g. ranged vs. melee).

Data-Driven Design: All combatant attributes and balance parameters live in ScriptableObjects for easy tuning without code changes.

Event-Driven Feedback: Damage and death events fire UnityEvents, decoupling core combat logic from UI and VFX systems.

Architecture & Design Patterns
Singleton Pool Manager

Automatically initializes pools for any prefab tagged “poolable.”

Exposes a SpawnFromPool() API that handles activation, reset, and despawning.

Abstract Base Classes

Combatant encapsulates stats, target tracking, and shared combat methods.

Derived classes (e.g. Mage, Archer, Warrior) implement their own attack ranges, animations, and effect triggers.

ScriptableObject Tables

Designers can clone and tweak SO assets—health, damage, cooldowns—to create new enemy or player types without touching code.

Loose Coupling via Events

Combat events dispatch UnityEvents for UI updates (floating text), audio, screen shake, etc., allowing new feedback features to plug in cleanly.
https://github.com/user-attachments/assets/37e20d56-ef1e-4d1e-9c96-2e5ba140c51a

