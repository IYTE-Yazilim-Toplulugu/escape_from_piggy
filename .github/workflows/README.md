# 🚀 GitHub Actions CI/CD for Unity

This directory contains GitHub Actions workflows for automated testing, building, and deployment of the Escape from Piggy Unity project.

## 📋 Available Workflows

### 1. **unity-ci.yml** - Full CI/CD Pipeline
Comprehensive workflow with all features:
- ✅ Unity tests (EditMode + PlayMode)
- 🏗️ Multi-platform builds (Windows, Linux, macOS, WebGL)
- 🔍 Code quality checks
- 📝 Commit message validation
- 🔒 Security scanning
- 🚀 Auto-deployment to Itch.io
- 📦 GitHub Release creation
- 💬 Discord/Slack notifications

**Triggers:** Push to `dev`/`main`, Pull Requests

### 2. **unity-ci-basic.yml** - Simplified CI
Minimal workflow for getting started:
- ✅ Unity tests
- 🏗️ Windows build only

**Triggers:** Pull Requests only

---

## 🔧 Setup Instructions

### Step 1: Activate Unity License

You need to activate a Unity license for GitHub Actions. Choose one:

#### Option A: Personal License (Free)
1. Go to repository **Settings → Secrets and variables → Actions**
2. Add these secrets:
   - `UNITY_EMAIL`: Your Unity account email
   - `UNITY_PASSWORD`: Your Unity account password
   - `UNITY_LICENSE`: (Generated in Step 3)

3. Get activation file:
```bash
# Run locally with Docker
docker run -it unityci/editor:ubuntu-6000.0.3f1 \
  unity-editor -quit -batchmode -nographics \
  -logFile /dev/stdout \
  -createManualActivationFile
```

4. Activate at https://license.unity3d.com/manual
5. Copy the `.ulf` file content to `UNITY_LICENSE` secret

#### Option B: Unity Professional License
1. Get your Unity Pro serial key
2. Add secrets:
   - `UNITY_SERIAL`: Your Pro license serial
   - `UNITY_EMAIL`: Your Unity email
   - `UNITY_PASSWORD`: Your Unity password

### Step 2: Enable GitHub Actions

1. Go to repository **Settings → Actions → General**
2. Enable "Allow all actions and reusable workflows"
3. Set "Workflow permissions" to "Read and write permissions"

### Step 3: Configure Git LFS

Make sure `.gitattributes` includes Unity LFS tracking:

```gitattributes
# Unity LFS
*.cubemap filter=lfs diff=lfs merge=lfs -text
*.unitypackage filter=lfs diff=lfs merge=lfs -text
# ... (existing LFS rules)
```

---

## 🎯 Workflow Features Explained

### Testing
- **EditMode Tests**: Tests that run in Unity Editor mode
- **PlayMode Tests**: Tests that simulate gameplay
- **Code Coverage**: Generates coverage reports

**Add tests in:** `Assets/_EscapeFromPiggy/Tests/`

### Building
Builds for multiple platforms:
- **StandaloneWindows64**: Windows executable
- **StandaloneLinux64**: Linux executable
- **StandaloneOSX**: macOS app
- **WebGL**: Browser-based build

### Code Quality
- **C# Style Check**: Validates code formatting (requires `.editorconfig`)
- **TODO Scanner**: Reports remaining TODOs/FIXMEs
- **Commit Validation**: Ensures conventional commit format

### Deployment

#### Itch.io (Auto-deploy on `main`)
1. Create game at https://itch.io/dashboard/new-game
2. Install Butler: https://itch.io/docs/butler/
3. Get API key: https://itch.io/user/settings/api-keys
4. Add secrets:
   - `BUTLER_CREDENTIALS`: Your Butler API key
   - `ITCH_USER`: Your itch.io username

#### GitHub Releases (On version tags)
Automatically creates releases when you push tags:
```bash
git tag v1.0.0
git push origin v1.0.0
```

### Notifications

#### Discord
1. Create webhook in Discord server settings
2. Add secret: `DISCORD_WEBHOOK`

#### Slack (Alternative)
Use `slackapi/slack-github-action@v1` instead

---

## 📊 Workflow Customization

### Change Unity Version
Edit in workflow file:
```yaml
env:
  UNITY_VERSION: 6000.0.3f1  # Change to your version
```

### Disable Specific Jobs
Comment out or remove jobs you don't need:
```yaml
# jobs:
#   security-scan:  # Disable security scanning
#     ...
```

### Add More Build Platforms
```yaml
matrix:
  targetPlatform:
    - StandaloneWindows64
    - Android  # Add mobile
    - iOS      # Add iOS
```

### Modify Test Timeout
```yaml
timeout-minutes: 30  # Increase if tests take longer
```

---

## 🐛 Troubleshooting

### Build Fails with License Error
- Check `UNITY_LICENSE` secret is correctly set
- Verify Unity version matches project version
- Try regenerating activation file

### Git LFS Timeout
```yaml
- uses: actions/checkout@v4
  with:
    lfs: true
    lfs-timeout: 600  # Increase timeout
```

### Out of Storage
GitHub provides 2 GB storage for artifacts. Clean old builds:
```yaml
retention-days: 3  # Reduce from 7 days
```

### Slow Builds
Enable caching properly:
```yaml
- uses: actions/cache@v3
  with:
    path: Library
    key: Library-${{ runner.os }}-${{ hashFiles('**/*.asset') }}
```

---

## 📖 Resources

- **Game CI Documentation**: https://game.ci/docs
- **Unity Test Framework**: https://docs.unity3d.com/Packages/com.unity.test-framework@latest
- **GitHub Actions**: https://docs.github.com/actions
- **Conventional Commits**: https://www.conventionalcommits.org/

---

## 🎮 Example Usage

### Running Tests Locally
```bash
# Install Unity Test Framework package first
# Then run from command line:
/path/to/Unity -runTests -batchmode -projectPath . \
  -testResults results.xml -testPlatform PlayMode
```

### Manual Build
```bash
/path/to/Unity -quit -batchmode -projectPath . \
  -executeMethod BuildScript.Build \
  -buildTarget StandaloneWindows64
```

---

## ✅ Recommended Workflow Activation Order

1. **Start with `unity-ci-basic.yml`** (simpler, easier to debug)
2. Get tests passing
3. Verify builds work
4. **Switch to `unity-ci.yml`** when ready for full CI/CD
5. Enable deployment features (Itch.io, releases)
6. Add notifications

---

## 🔒 Security Best Practices

- ✅ Never commit secrets to repository
- ✅ Use GitHub Secrets for all credentials
- ✅ Enable branch protection rules
- ✅ Require PR reviews before merging
- ✅ Enable security scanning (GitGuardian)
- ✅ Regularly update action versions

---

## 💡 Tips

- **Cache Library folder**: Reduces build time by 50-80%
- **Matrix builds**: Test multiple Unity versions simultaneously
- **Parallel jobs**: Run tests and builds concurrently
- **Artifacts retention**: Balance between storage and debugging needs
- **Notifications**: Only notify on failures to reduce noise

---

## 📞 Support

If you encounter issues:
1. Check workflow logs in GitHub Actions tab
2. Review Game CI docs: https://game.ci/docs
3. Ask in project Discord/Slack
4. Open issue in repository

---

**Last Updated:** 2026-01-11  
**Maintained by:** Escape from Piggy Dev Team
