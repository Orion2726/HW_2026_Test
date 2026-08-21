# Doofus Diary
 
A small Unity 3D platform-survival game built as a gameplay prototype for the Hitwicket Game Developer Challenge.
 
The player must navigate a sequence of disappearing platforms, unlock new movement abilities, and adapt to increasingly challenging platform layouts while trying to reach the target score.
 
<!-- Add 2–3 gameplay screenshots or a short GIF/video here before submitting -->
<!-- ![Gameplay Screenshot](path/to/screenshot.png) -->
 
## Play the Game
 
**[Play in browser](https://orion2726.github.io/HW_2026_Test/Build/)**
 
## Gameplay Video
 
**[Watch on YouTube](https://youtu.be/jcMOrFb_ZHk)**
 
## Features
 
### Third-Person 3D Character
- Character movement with camera-relative controls
- Character rotation based on movement direction
- Walking, jump, push, and other character animations
- Animator-driven state transitions

### Progressive Movement System
- Normal movement at the beginning
- Single jump unlocked after reaching a score threshold
- Double jump unlocked at a higher score threshold
- Unlock notifications with fade animations

### Dynamic Pulpit System
- Platforms have individual lifetimes and countdown timers
- Platforms fade in when spawned and fade out before destruction
- Next platform spawns before the current platform disappears
- Multiple platform behaviours:
  - Static
  - Side-to-side movement
  - Diagonal movement
  - Vertical + horizontal movement
- Different materials visually communicate platform types
- Moving platforms use visual trails

### Progressive Difficulty
- Platform lifetime changes as the player progresses
- Platform behaviour becomes more varied after progression thresholds
- Score value increases at higher progression stages
- Normal Mode has a victory condition
- Endless Mode allows continued progression

### Physics-Based Camera
- Spring-damper camera movement
- Smooth camera following with momentum
- Dynamic camera rotation when the platform path changes direction
- Responsive movement without abrupt snapping

### Scoring & Game Flow
- Score increases when platforms are successfully reached
- Progressive score rewards
- Game Over when the player falls
- Victory screen when the target score is reached in Normal Mode
- Restart and return-to-menu functionality

### UI & Feedback
- In-world platform countdown timers
- Billboard-style world-space UI
- Score display
- Game Over and Victory screens
- Animated ability-unlock notifications
- UI click and gameplay feedback sounds

### Audio System
- Centralized Audio Manager
- Background soundtrack
- UI click effects
- Score increase effects
- Game Over audio

### Data-Driven Configuration
- Gameplay configuration loaded from `doofus_diary.json`
- Player and pulpit parameters separated from gameplay code
- Gameplay values can be adjusted without hard-coding them throughout the project

## Architecture
 
The project is organized into focused, single-responsibility systems:
 
```text
Assets/Scripts/
├── Audio/
│   └── AudioManager.cs
├── Core/
│   ├── GameManager.cs
│   └── ScoreManager.cs
├── Data/
│   ├── DoofusDiaryLoader.cs
│   └── GameConfig.cs
├── Player/
│   ├── PlayerMovement.cs
│   └── ThirdPersonCamera.cs
├── Pulpits/
│   ├── Pulpit.cs
│   ├── PulpitSpawner.cs
│   └── PulpitTrigger.cs
└── UI/
    ├── BillboardUI.cs
    ├── StartScreenUI.cs
    ├── MenuDoofusAnimator.cs
    └── UnlockNotification.cs
```
 
Gameplay systems are separated into independent components, making the prototype easier to extend with additional platform behaviours, power-ups, scoring rules, visual effects, and movement abilities.
 
## Design Approach
 
The project focuses on **modular and extensible gameplay systems** rather than hard-coding individual levels.
 
Platforms are responsible for their own lifetime, visual behaviour, and movement, while the spawner handles platform generation and the scoring/game-management systems handle progression and game state.
 
This allows new platform types, materials, effects, and gameplay mechanics to be added without redesigning the entire game architecture.
 
## Controls
 
| Input         | Action             |
| ------------- | ------------------ |
| W / A / S / D | Move               |
| Space         | Jump / Double Jump |
| P             | Push Animation     |
 
## Game Modes
 
**Normal Mode** — Reach the target score to complete the game and trigger the Victory screen.
 
**Endless Mode** — Continue playing indefinitely as difficulty and platform variety increase.
 
## Built With
 
- Unity 6
- C#
- TextMeshPro
- Unity Animator
- Unity Rigidbody Physics
- Unity UI
- JSON-based configuration

## Goal
 
This project was built as a gameplay-focused prototype demonstrating:
 
- Character animation
- Physics-based movement
- Dynamic platform generation
- Progressive difficulty
- Camera systems
- UI feedback
- Audio integration
- Data-driven configuration
- Modular gameplay architecture
