# 📋 GitHub Issues Summary - Escape from Piggy

**Created:** 2026-01-11
**Total Issues:** 10
**Project Board:** https://github.com/orgs/IYTE-Yazilim-Toplulugu/projects/25

---

## 🎯 Issues Created

### Critical Priority (Start Here!)

These are **blocking** issues - everything else depends on them. Complete these first!

#### [\#1: Implement Core Player Movement System (Celeste-style)](https://github.com/IYTE-Yazilim-Toplulugu/escape_from_piggy/issues/1)
- **Priority:** 🔴 Critical
- **Size:** L (1-2 days)
- **Type:** Programming
- **Status:** Open

**What:** Celeste-inspired movement with jump, 8-directional dash, wall-slide, wall-jump, coyote time, and jump buffering.

**Why Critical:** This is the foundation - nothing works without it!

**Key Deliverables:**
- Variable-height jump
- 8-directional dash with charges
- Wall mechanics (slide + jump)
- Coyote time & jump buffering for forgiveness
- Tight, responsive controls (90%+ playtester approval)

---

#### [\#2: Setup Player Character GameObject and Prefab](https://github.com/IYTE-Yazilim-Toplulugu/escape_from_piggy/issues/2)
- **Priority:** 🔴 Critical
- **Size:** M (4-6 hours)
- **Type:** Programming
- **Depends on:** #1

**What:** Create Player GameObject with Rigidbody2D, colliders, animation controller, and proper physics setup.

**Key Deliverables:**
- Player GameObject with all components
- Physics Material 2D (zero friction)
- Animation Controller with states
- Sorting layers and collision layers
- Player prefab

---

#### [\#3: Configure Unity Input System](https://github.com/IYTE-Yazilim-Toplulugu/escape_from_piggy/issues/3)
- **Priority:** 🔴 Critical
- **Size:** M (4-6 hours)
- **Type:** Programming
- **Depends on:** #1

**What:** Setup Unity's new Input System for keyboard + gamepad support with rebindable controls.

**Key Deliverables:**
- Input Actions asset (Move, Jump, Dash, Interact)
- InputManager singleton
- Keyboard bindings (WASD + Arrows)
- Gamepad bindings
- Integration with PlayerController

---

### High Priority

#### [\#4: Implement 2D Camera Controller with Follow System](https://github.com/IYTE-Yazilim-Toplulugu/escape_from_piggy/issues/4)
- **Priority:** 🟠 High
- **Size:** M (4-6 hours)
- **Type:** Programming
- **Depends on:** #1, #2

**What:** Cinemachine camera system with smooth follow, look-ahead, bounds, and camera shake.

**Key Deliverables:**
- Cinemachine virtual camera
- Smooth follow with damping
- Look-ahead in movement direction
- Camera bounds (confiner)
- Camera shake on dash/impacts

---

#### [\#5: Create Player Sprite Animations](https://github.com/IYTE-Yazilim-Toplulugu/escape_from_piggy/issues/5)
- **Priority:** 🟠 High
- **Size:** L (8-12 hours)
- **Type:** Art
- **Depends on:** #2
- **Good First Issue:** ✅ Great for artists!

**What:** Pixel art animations for Rector Yusuf Baran (Idle, Run, Jump, Fall, Dash, Wall Slide).

**Key Deliverables:**
- Character design (32x32 pixel art recommended)
- 6 animation states
- Sprite sheets imported to Unity
- Connected to Animator Controller
- Team-approved character design

---

#### [\#6: Implement DNA Collection and Economy System](https://github.com/IYTE-Yazilim-Toplulugu/escape_from_piggy/issues/6)
- **Priority:** 🟠 High
- **Size:** L (8-10 hours)
- **Type:** Programming
- **Depends on:** #3

**What:** Core DNA collection mechanic - collect from environment/enemies, spend at Bio-Stations.

**Key Deliverables:**
- DNAManager singleton
- DNA collectible prefab with magnet effect
- DNA counter HUD
- Bio-Station interactable
- Save/load DNA amounts
- Visual/audio feedback on collection

---

#### [\#7: Implement Basic Enemy AI (Piggy Chase Behavior)](https://github.com/IYTE-Yazilim-Toplulugu/escape_from_piggy/issues/7)
- **Priority:** 🟠 High
- **Size:** L (8-10 hours)
- **Type:** Programming

**What:** Enemy AI with patrol, player detection, chase, and return to patrol behaviors.

**Key Deliverables:**
- EnemyAI script with state machine (Idle, Patrol, Chase)
- Player detection with line-of-sight
- Patrol between waypoints
- Chase timeout/distance
- Enemy prefab
- Gizmos visualization

---

### Medium Priority

#### [\#8: Create Level Design Framework and Test Level](https://github.com/IYTE-Yazilim-Toplulugu/escape_from_piggy/issues/8)
- **Priority:** 🟡 Medium
- **Size:** M (6-8 hours)
- **Type:** Level Design
- **Depends on:** #1, #2, #6
- **Good First Issue:** ✅ Great for learning tilemaps!

**What:** Tilemap system and test level showcasing all movement mechanics.

**Key Deliverables:**
- Tilemap setup (Ground, Platforms, Hazards, Background)
- Basic tiles (can be placeholder)
- Test level with jumps, gaps, walls, secrets
- Prefab library (platforms, hazards, collectibles)
- Colliders configured properly

---

#### [\#9: Setup Audio Manager and Basic Sound Effects](https://github.com/IYTE-Yazilim-Toplulugu/escape_from_piggy/issues/9)
- **Priority:** 🟡 Medium
- **Size:** M (4-6 hours)
- **Type:** Audio

**What:** Audio system with Music, SFX, and volume controls.

**Key Deliverables:**
- AudioManager singleton
- Audio Mixer (Master/Music/SFX groups)
- Placeholder SFX (jump, dash, land, collect)
- Background music system
- Volume controls
- Integration with player actions

---

#### [\#10: Create Basic UI System (HUD, Pause Menu)](https://github.com/IYTE-Yazilim-Toplulugu/escape_from_piggy/issues/10)
- **Priority:** 🟡 Medium
- **Size:** M (6-8 hours)
- **Type:** Programming
- **Depends on:** #3, #6, #9

**What:** HUD with DNA counter, pause menu, settings, and main menu.

**Key Deliverables:**
- Canvas setup with proper scaling
- HUD layout (DNA counter, health placeholder)
- Pause menu (Resume, Settings, Quit)
- Settings panel with volume sliders
- Main menu scene
- Scene transitions

---

## 📊 Quick Stats

- **Total Issues:** 10
- **Critical Priority:** 3 issues
- **High Priority:** 4 issues
- **Medium Priority:** 3 issues

**By Type:**
- Programming: 7 issues
- Art: 1 issue
- Level Design: 1 issue
- Audio: 1 issue

**By Size:**
- Large (8+ hours): 5 issues
- Medium (4-8 hours): 5 issues

**Good First Issues:** 2 (#5 Art, #8 Level Design)

---

## 🚀 Recommended Development Order

Follow this order for optimal workflow:

### Phase 1: Core Foundation (Week 1)
1. **Issue #1** - Movement System (CRITICAL)
2. **Issue #2** - Player Setup (CRITICAL)
3. **Issue #3** - Input System (CRITICAL)
4. **Issue #4** - Camera Controller

**Goal:** Playable character that feels good to control

---

### Phase 2: Gameplay Systems (Week 2)
5. **Issue #6** - DNA System
6. **Issue #7** - Enemy AI
7. **Issue #8** - Test Level
8. **Issue #5** - Player Animations (can be parallel with others)

**Goal:** Core gameplay loop functional (collect DNA, avoid enemies, reach goal)

---

### Phase 3: Polish & UI (Week 3)
9. **Issue #9** - Audio System
10. **Issue #10** - UI System

**Goal:** Game feels polished and complete

---

## 🎨 Good First Issues

New to the project? Start here!

### For Artists:
- **[\#5: Player Sprite Animations](https://github.com/IYTE-Yazilim-Toplulugu/escape_from_piggy/issues/5)**
  - Create pixel art character and animations
  - Great introduction to Unity sprite import workflow
  - Requires: Art software (Aseprite, Piskel, etc.)

### For Programmers (Unity beginners):
- **[\#8: Level Design Framework](https://github.com/IYTE-Yazilim-Toplulugu/escape_from_piggy/issues/8)**
  - Learn Unity Tilemaps
  - Practice level design principles
  - See all systems come together

---

## 💡 Working with Issues

### Before Starting an Issue

1. **Read the issue completely**
   - Understand task description
   - Review step-by-step roadmap
   - Check resources
   - Note dependencies

2. **Assign yourself**
   ```bash
   gh issue edit <issue-number> --add-assignee @me --repo IYTE-Yazilim-Toplulugu/escape_from_piggy
   ```

3. **Create a branch**
   ```bash
   git checkout dev
   git pull origin dev
   git checkout -b feature/issue-<number>-short-description
   # Example: git checkout -b feature/issue-1-player-movement
   ```

### While Working

1. **Follow the roadmap** - It's step-by-step for a reason!
2. **Check off todo items** - Edit issue on GitHub to track progress
3. **Ask questions** - Comment on the issue if stuck
4. **Commit frequently** - Small commits with clear messages
   ```bash
   git commit -m "feat(movement): implement jump with coyote time"
   ```

### When Complete

1. **Test against acceptance criteria** - Every checkbox must pass!
2. **Create Pull Request**
   ```bash
   git push origin feature/issue-<number>-short-description
   gh pr create --title "Closes #<issue-number>: <title>" --body "Fixes #<issue-number>"
   ```
3. **Link PR to issue** - Use "Closes #X" in PR description
4. **Request review** - Tag team members
5. **Address feedback** - Make requested changes

---

## 🔗 Useful Links

- **Project Board:** https://github.com/orgs/IYTE-Yazilim-Toplulugu/projects/25
- **Repository:** https://github.com/IYTE-Yazilim-Toplulugu/escape_from_piggy
- **Team Guide:** [TAKIM_GIRIS.md](TAKIM_GIRIS.md)
- **Project Structure:** [docs/PROJECT_STRUCTURE.md](docs/PROJECT_STRUCTURE.md)
- **Game Brief:** [_bmad-output/game-brief.md](_bmad-output/game-brief.md)

---

## 📝 Labels Explained

### Priority Labels
- 🔴 **priority: critical** - Blocking work, must do first
- 🟠 **priority: high** - Important, do soon
- 🟡 **priority: medium** - Should do, but not urgent
- 🟢 **priority: low** - Nice to have

### Size Labels
- **size: XS** - 1-2 hours
- **size: S** - 2-4 hours
- **size: M** - 4-8 hours
- **size: L** - 8-16 hours (1-2 days)
- **size: XL** - 2+ days

### Type Labels
- **type: programming** - C# code
- **type: art** - Sprites, animations, visual assets
- **type: level-design** - Level creation and tilemaps
- **type: audio** - Music and sound effects

### Other Labels
- **good first issue** - Newcomer-friendly
- **enhancement** - New feature
- **bug** - Something broken
- **documentation** - Docs update

---

## 🎯 Tips for Success

1. **Start with Critical issues** - Don't skip ahead!
2. **Read the entire issue** before starting
3. **Use the provided code examples** - They're tested and follow best practices
4. **Ask questions early** - Don't struggle alone
5. **Test thoroughly** - Check every acceptance criteria item
6. **Code review is learning** - Welcome feedback
7. **Update documentation** - If you change something, update docs
8. **Have fun!** - We're making a game, enjoy the process!

---

**Questions?** Ask in Discord/Slack or comment on the issue!

**Good luck and happy coding! 🚀🐷**
