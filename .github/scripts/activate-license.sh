#!/bin/bash
# Unity License Activation Helper for GitHub Actions
# Run this locally to generate activation file

set -e

echo "🎮 Unity License Activation Helper"
echo "=================================="
echo ""

UNITY_VERSION="6000.0.3f1"

# Check if Docker is installed
if ! command -v docker &> /dev/null; then
    echo "❌ Docker is not installed!"
    echo "Please install Docker: https://docs.docker.com/get-docker/"
    exit 1
fi

echo "📥 Pulling Unity Docker image..."
docker pull unityci/editor:ubuntu-$UNITY_VERSION

echo ""
echo "🔧 Creating manual activation file..."
docker run --rm \
    -v "$(pwd)/activation:/root/.local/share/unity3d/Unity" \
    unityci/editor:ubuntu-$UNITY_VERSION \
    unity-editor \
    -quit -batchmode -nographics \
    -logFile /dev/stdout \
    -createManualActivationFile

echo ""
echo "✅ Activation file created!"
echo ""
echo "📋 Next steps:"
echo "1. Find the .alf file in ./activation/ directory"
echo "2. Go to https://license.unity3d.com/manual"
echo "3. Upload the .alf file and download .ulf file"
echo "4. Copy the entire content of .ulf file"
echo "5. Go to GitHub: Settings → Secrets → Actions"
echo "6. Create new secret 'UNITY_LICENSE' with .ulf content"
echo ""
echo "🔐 Also add these secrets:"
echo "   - UNITY_EMAIL: Your Unity account email"
echo "   - UNITY_PASSWORD: Your Unity account password"
echo ""
