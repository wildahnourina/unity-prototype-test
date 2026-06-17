# Side-Scroller Horror Game

A work-in-progress 2D side-scroller horror game built with Unity 6.

This project focuses on gameplay programming and modular system architecture. Temporary placeholder assets are currently used while core mechanics are being developed.

## Features

### Player System
* Generic Finite State Machine (FSM) architecture.
* Shared parent entity states extended by PlayerState and GhostState.
* Built with Unity Input System.

### Interaction System
Interface-based interaction system using `IInteractable`.
Supports:
* NPC interactions
* Item pickups
* Locked objects requiring specific items
* Custom interaction behaviors

Examples:

-Picking up the flashlight automatically enables the battery UI.

-Certain objects require items from the inventory before they can be used.

### Dialogue System
ScriptableObject-driven dialogue system featuring:
* Character names
* Portrait support
* Typewriter effect
* Skip functionality
* NPC conversations
* Triggered player monologues

### Inventory & Item System
Inventory implemented using List.

Supports:
* Stackable items
* Consumable items
* Left-click item usage
* Right-click item dropping
* Custom item effects if needed

Item data is stored using ScriptableObjects and supports custom item effects.

### Flashlight System
Custom flashlight controller featuring:
* Battery drain over time
* Configurable low-power threshold
* Flickering behavior at low battery
* Battery restoration through consumable items
* Light-based ghost interaction triggers

The flashlight can be turned off to conserve battery power.

### Enemy AI
Ghost enemies are built on the same FSM architecture.

Current states:
* Idle
* Active
* Chase

Designed to be expanded with additional behaviors and animations.

### Trigger Event System
Custom event-driven trigger architecture consisting of:
* Trigger Context
* Trigger Signal
* Trigger Emitter
* Trigger Reaction

This enables dynamic cause-and-effect interactions between gameplay objects.

### Room Transition System
Room-based transitions managed by a Room Manager.
Door interactions support:
* Target room references
* Connection IDs
* Spawn points

Transitions are handled through fade effects.

### Objective System
Temporary objective hints are used to guide player progression and exploration.

### Audio System
Audio management built around:
* ScriptableObject audio database
* Audio Mixer integration
* Synchronized options settings

## Architecture
* Interface-based interaction system
* ScriptableObject-driven data
* Generic Finite State Machine architecture
* Event-driven trigger system
* Modular and extensible gameplay systems

## Status
This project is currently under active development.
Visual assets are temporary placeholders, with development focused primarily on gameplay systems and architecture.
