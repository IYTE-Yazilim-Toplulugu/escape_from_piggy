# 🚀 Quick Start: GitHub Actions CI/CD

## ⚡ 5-Minute Setup

### 1. Get Unity License (One-time)

```bash
# Run from project root
./.github/scripts/activate-license.sh
```

This will create an activation file. Follow the instructions in the script output.

### 2. Add GitHub Secrets

Go to: **Repository → Settings → Secrets and variables → Actions**

Add these 3 secrets:

| Secret Name | Value | Where to get it |
|------------|-------|----------------|
| `UNITY_EMAIL` | your@email.com | Your Unity account email |
| `UNITY_PASSWORD` | yourpassword | Your Unity password |
| `UNITY_LICENSE` | (entire .ulf file content) | From activation step |

### 3. Enable GitHub Actions

Go to: **Repository → Settings → Actions → General**

- ✅ Enable "Allow all actions and reusable workflows"
- ✅ Set permissions to "Read and write"

### 4. Choose Your Workflow

**Option A: Start Simple** (Recommended)
- Rename `unity-ci-basic.yml.disabled` → `unity-ci-basic.yml`
- Only runs on PRs
- Tests + Windows build only

**Option B: Full Power**
- Rename `unity-ci.yml.disabled` → `unity-ci.yml`  
- Multi-platform builds
- Auto-deployment
- All the bells and whistles

### 5. Test It!

Create a PR and watch the magic happen! ✨

```bash
git checkout -b test-ci
git commit --allow-empty -m "test: trigger CI"
git push origin test-ci
# Create PR on GitHub
```

---

## 📊 What Gets Built

### Basic Workflow
- ✅ Tests run
- 🏗️ Windows 64-bit build

### Full Workflow
- ✅ Tests + Coverage
- 🏗️ Windows, Linux, macOS, WebGL builds
- 🔍 Code quality checks
- 📝 Commit validation
- 🚀 Auto-deploy to Itch.io (on `main`)

---

## 🆘 Troubleshooting

### "License error"
- Make sure you copied the **entire** .ulf file content
- No extra spaces or newlines
- Check UNITY_EMAIL and UNITY_PASSWORD match your Unity account

### "Not enough storage"
- Workflows create artifacts that use GitHub storage
- Free tier: 500 MB storage, 2000 minutes/month
- Reduce artifact retention in workflow (change `retention-days`)

### "Build timeout"
- First build takes ~15-20 minutes (downloads Unity)
- Subsequent builds: ~5-10 minutes (cached)
- Increase `timeout-minutes` if needed

---

## 🎯 Next Steps

1. ✅ Get basic CI working
2. Add actual Unity tests (Issue #TODO)
3. Configure Itch.io deployment
4. Setup Discord notifications
5. Add more platforms (Android, iOS)

---

## 📖 Full Documentation

See [.github/workflows/README.md](.github/workflows/README.md) for complete guide.

---

**Need help?** Ask in Discord or open an issue!
