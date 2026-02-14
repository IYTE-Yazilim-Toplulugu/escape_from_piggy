# Escape from Piggy

A Unity 2D horror/chase game where players must escape from Piggy while solving puzzles and avoiding capture.

## Project Info

- **Unity Version:** 2022.3+ (LTS)
- **Render Pipeline:** Universal Render Pipeline (URP) 2D
- **Target Platform:** PC/Mac (expandable to mobile)

## Project Structure

This project follows Unity's official organization best practices. All game content is organized in `Assets/_EscapeFromPiggy/`.

For detailed folder structure and naming conventions, see: [PROJECT_STRUCTURE.md](Assets/_EscapeFromPiggy/PROJECT_STRUCTURE.md)

### Quick Overview

```
Assets/
├── _EscapeFromPiggy/    # Main game project
│   ├── Art/             # Sprites, materials, animations
│   ├── Audio/           # Music and sound effects
│   ├── Prefabs/         # Reusable game objects
│   ├── Scenes/          # Game levels
│   ├── Scripts/         # C# code (organized by domain)
│   ├── ScriptableObjects/  # Data assets
│   └── Settings/        # Unity configuration
├── ThirdParty/          # External assets
└── _Dev/                # Developer sandbox
```

## Getting Started

1. Clone the repository
2. Open project in Unity Hub (Unity 2022.3 LTS or newer)
3. Open `Assets/_EscapeFromPiggy/Scenes/SampleScene.unity`
4. Read the [PROJECT_STRUCTURE.md](docs/PROJECT_STRUCTURE.md) for organization guidelines

## Development Guidelines

### Naming Conventions
- **Files/Folders:** PascalCase, no spaces
- **Scripts:** PascalCase classes, camelCase variables
- **Scenes:** Descriptive names (`MainMenu.unity`, `Level_01.unity`)
- **Prefabs:** Noun phrases (`Player`, `EnemyPiggy`)

### Code Style
- Use `EscapeFromPiggy` namespace for all game scripts
- One class per file
- Follow C# conventions and Unity best practices

Example:
```csharp
namespace EscapeFromPiggy
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;

        private void Update()
        {
            // Player logic
        }
    }
}
```

### Version Control
- Always commit `.meta` files with assets
- Move files within Unity Editor (not file system) to preserve references
- Use Git LFS for large files (already configured)

## Git LFS

This project uses Git LFS for large binary files (textures, audio, models, etc.). LFS is already configured - just commit assets normally.

Tracked file types:
- Images: `.png`, `.jpg`, `.psd`, etc.
- 3D Models: `.fbx`, `.obj`, `.blend`
- Audio: `.mp3`, `.wav`, `.ogg`
- Video: `.mp4`, `.mov`
- And more (see `.gitattributes`)

## Contributing

1. Follow the project structure defined in PROJECT_STRUCTURE.md
2. Use consistent naming conventions
3. Test your changes before committing
4. Write clear commit messages

## Team

Software Society - IYTE Yazılım Topluluğu
