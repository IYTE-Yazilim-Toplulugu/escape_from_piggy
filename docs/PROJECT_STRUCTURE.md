# Escape from Piggy - Project Structure

This document outlines the folder structure and organization conventions for the Escape from Piggy Unity project.

## Philosophy

This project follows Unity's official best practices for project organization:
- **Organized by asset type** for easy navigation
- **Consistent naming conventions** throughout
- **Separation of concerns** between game content, third-party assets, and development
- **Documentation-first** approach to maintain clarity

## Folder Structure Overview

```
Assets/
├── _EscapeFromPiggy/          # Main game project (underscore = top of list)
│   ├── Art/                   # Visual assets
│   ├── Audio/                 # Sound and music
│   ├── Prefabs/               # Reusable game objects
│   ├── Scenes/                # Game scenes
│   ├── Scripts/               # C# code
│   ├── ScriptableObjects/     # Data assets
│   └── Settings/              # Unity configuration
├── ThirdParty/                # External assets and plugins
└── _Dev/                      # Developer sandbox/testing
```

## Detailed Breakdown

### `_EscapeFromPiggy/`
Main project namespace. All game-specific content lives here.

#### `Art/`
Visual assets organized by type:
- **Sprites/** - 2D textures, character sprites, UI images
- **Materials/** - Unity materials and shaders
- **Animations/** - Animation clips and controllers
- **UI/** - UI-specific artwork (buttons, panels, icons)

#### `Audio/`
Sound assets organized by category:
- **Music/** - Background music tracks
- **SFX/** - Sound effects (footsteps, doors, pickups, etc.)

#### `Prefabs/`
Reusable GameObject prefabs organized by function:
- **Characters/** - Player, Piggy, NPCs
- **Environment/** - Level objects (doors, walls, furniture)
- **UI/** - UI prefabs (menus, HUD elements)
- **Collectibles/** - Keys, items, power-ups

#### `Scenes/`
Unity scene files. Name scenes clearly:
- `MainMenu.unity`
- `Level_01.unity`
- `Level_02.unity`
- `TestLevel.unity` (for development)

#### `Scripts/`
All C# code organized by domain:

- **Player/** - Player controller, movement, abilities
  - `PlayerController.cs`
  - `PlayerMovement.cs`
  - `PlayerInventory.cs`

- **Enemy/** - AI and enemy behavior
  - `EnemyAI.cs`
  - `EnemyChase.cs`
  - `EnemyPatrol.cs`

- **Level/** - Level mechanics
  - `DoorController.cs`
  - `KeyPickup.cs`
  - `TrapTrigger.cs`

- **UI/** - User interface controllers
  - `MainMenuController.cs`
  - `HUDController.cs`
  - `PauseMenu.cs`

- **Managers/** - Game systems and singletons
  - `GameManager.cs`
  - `AudioManager.cs`
  - `InputManager.cs`
  - `SceneLoader.cs`

- **Data/** - Data structures and ScriptableObject definitions
  - `CharacterData.cs`
  - `LevelData.cs`
  - Enums and constants

- **Utilities/** - Helper classes and extensions
  - `Extensions.cs`
  - `DebugHelper.cs`
  - `MathUtils.cs`

#### `ScriptableObjects/`
Data-driven game assets:
- **Characters/** - Character stats and configuration
- **Items/** - Item definitions (keys, power-ups)

#### `Settings/`
Unity project settings (URP, Input System, Volume Profiles, etc.)

### `ThirdParty/`
External assets from Asset Store or other sources. Keep separate to avoid conflicts during updates.

Examples:
- `ThirdParty/TextMeshPro/`
- `ThirdParty/DOTween/`
- `ThirdParty/PostProcessing/`

### `_Dev/`
Developer sandbox for testing and experimentation. Not included in builds.

Organize by developer name:
- `_Dev/YourName/` - Personal test scenes and assets

## Naming Conventions

### Files and Folders
- **Use PascalCase** for all files and folders: `PlayerController.cs`, `MainMenu.unity`
- **NO SPACES** - Use underscores or PascalCase: `Level_01.unity` or `MainMenuScene.unity`
- **Be descriptive** - Avoid abbreviations: `PlayerController` not `PlyrCtrl`

### Scripts
- **Classes** - PascalCase: `PlayerController`, `EnemyAI`
- **Variables** - camelCase: `playerSpeed`, `maxHealth`
- **Constants** - UPPER_SNAKE_CASE: `MAX_PLAYERS`, `DEFAULT_SPEED`
- **Private fields** - Prefix with underscore: `_rigidbody`, `_isGrounded`
- **Interfaces** - Prefix with I: `IMoveable`, `IDamageable`

### Unity Assets
- **Scenes** - Descriptive names: `MainMenu`, `Level_01`, `GameplayScene`
- **Prefabs** - Noun phrases: `Player`, `EnemyPiggy`, `WoodenDoor`
- **Materials** - Descriptive + Mat suffix: `PlayerMat`, `WallBrickMat`
- **Animations** - Action + subject: `PlayerIdle`, `DoorOpen`, `PiggyChase`

## Best Practices

### Version Control (.meta files)
- **ALWAYS commit .meta files** - They preserve Unity's asset references
- **Move files in Unity Editor** - Right-click → Show in Explorer → move there
- If you move files outside Unity, close Unity first, then reopen

### Asset Organization
- **Group related assets** - Keep a feature's scripts, prefabs, and art together when logical
- **Avoid deep nesting** - Max 3-4 folder levels for easy navigation
- **Delete unused assets** - Keep the project clean

### Prefab Workflow
- **Prefab variants** for similar objects (e.g., `Door` base, `WoodenDoor` variant)
- **Nested prefabs** for composition (e.g., `Room` contains `Door` prefabs)
- **Override sparingly** - Too many overrides make prefabs hard to maintain

### ScriptableObjects for Data
Use ScriptableObjects instead of hardcoded values:
```csharp
// Bad
public class Enemy : MonoBehaviour {
    public float speed = 5f;
    public int health = 100;
}

// Good
public class EnemyData : ScriptableObject {
    public float speed;
    public int health;
}

public class Enemy : MonoBehaviour {
    public EnemyData data;
}
```

### Code Organization
- **One class per file** - File name matches class name
- **Namespaces** - Use `EscapeFromPiggy` namespace for all game code
- **Regions** - Group related methods (use sparingly)
```csharp
namespace EscapeFromPiggy
{
    public class PlayerController : MonoBehaviour
    {
        #region Unity Callbacks
        private void Start() { }
        private void Update() { }
        #endregion

        #region Public Methods
        public void TakeDamage(int amount) { }
        #endregion

        #region Private Methods
        private void HandleMovement() { }
        #endregion
    }
}
```

## Scene Organization

Organize GameObjects in hierarchy:
```
SampleScene
├── --- MANAGEMENT ---
│   ├── GameManager
│   ├── AudioManager
│   └── EventSystem
├── --- ENVIRONMENT ---
│   ├── Ground
│   ├── Walls
│   └── Props
├── --- GAMEPLAY ---
│   ├── Player
│   ├── Enemies
│   └── Collectibles
├── --- UI ---
│   └── Canvas
└── --- LIGHTING ---
    ├── Global Volume
    └── Lights
```

Use empty GameObjects with `---` prefix as separators.

## Adding New Assets

When adding new assets:
1. Identify the correct folder based on asset type
2. Follow naming conventions
3. Update this documentation if adding new categories
4. Commit .meta files with the asset

## References

- [Unity - How to organize your project](https://unity.com/how-to/organizing-your-project)
- [Unity - Best practices](https://unity.com/how-to/naming-and-code-style-tips-c-scripting-unity)

---

**Last Updated:** 2026-01-10
**Maintained by:** Escape from Piggy Team
