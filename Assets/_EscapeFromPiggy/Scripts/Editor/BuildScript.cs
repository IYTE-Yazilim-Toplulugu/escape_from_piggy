using System;
using System.Linq;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace EscapeFromPiggy.Editor
{
    /// <summary>
    /// Build script for CI/CD automation
    /// Used by GitHub Actions to build the game
    /// </summary>
    public static class BuildScript
    {
        private static readonly string[] Scenes = FindEnabledScenes();

        /// <summary>
        /// Main build method called by CI
        /// </summary>
        public static void Build()
        {
            // Get build target from command line args
            string[] args = Environment.GetCommandLineArgs();
            BuildTarget target = GetBuildTarget(args);
            string buildPath = GetBuildPath(args, target);

            Debug.Log($"Starting build for {target} at {buildPath}");

            // Configure build options
            BuildPlayerOptions buildOptions = new BuildPlayerOptions
            {
                scenes = Scenes,
                locationPathName = buildPath,
                target = target,
                options = BuildOptions.None
            };

            // Perform build
            BuildReport report = BuildPipeline.BuildPlayer(buildOptions);
            BuildSummary summary = report.summary;

            // Log results
            if (summary.result == BuildResult.Succeeded)
            {
                Debug.Log($"✅ Build succeeded: {summary.totalSize} bytes in {summary.totalTime}");
                EditorApplication.Exit(0);
            }
            else
            {
                Debug.LogError($"❌ Build failed: {summary.result}");
                
                // Log errors
                foreach (var step in report.steps)
                {
                    if (step.messages.Any(m => m.type == LogType.Error || m.type == LogType.Exception))
                    {
                        Debug.LogError($"Step: {step.name}");
                        foreach (var message in step.messages)
                        {
                            if (message.type == LogType.Error || message.type == LogType.Exception)
                            {
                                Debug.LogError(message.content);
                            }
                        }
                    }
                }
                
                EditorApplication.Exit(1);
            }
        }

        /// <summary>
        /// Build for Windows 64-bit
        /// </summary>
        public static void BuildWindows()
        {
            Build(BuildTarget.StandaloneWindows64, "builds/Windows/EscapeFromPiggy.exe");
        }

        /// <summary>
        /// Build for Linux 64-bit
        /// </summary>
        public static void BuildLinux()
        {
            Build(BuildTarget.StandaloneLinux64, "builds/Linux/EscapeFromPiggy.x86_64");
        }

        /// <summary>
        /// Build for macOS
        /// </summary>
        public static void BuildMacOS()
        {
            Build(BuildTarget.StandaloneOSX, "builds/macOS/EscapeFromPiggy.app");
        }

        /// <summary>
        /// Build for WebGL
        /// </summary>
        public static void BuildWebGL()
        {
            Build(BuildTarget.WebGL, "builds/WebGL");
        }

        private static void Build(BuildTarget target, string path)
        {
            Debug.Log($"Building {target} to {path}");

            BuildPlayerOptions buildOptions = new BuildPlayerOptions
            {
                scenes = Scenes,
                locationPathName = path,
                target = target,
                options = BuildOptions.None
            };

            BuildReport report = BuildPipeline.BuildPlayer(buildOptions);
            HandleBuildReport(report);
        }

        private static void HandleBuildReport(BuildReport report)
        {
            BuildSummary summary = report.summary;

            if (summary.result == BuildResult.Succeeded)
            {
                Debug.Log($"✅ Build succeeded!");
                Debug.Log($"   Size: {FormatBytes(summary.totalSize)}");
                Debug.Log($"   Time: {summary.totalTime}");
                Debug.Log($"   Path: {summary.outputPath}");
            }
            else
            {
                Debug.LogError($"❌ Build failed: {summary.result}");
                throw new Exception("Build failed");
            }
        }

        private static BuildTarget GetBuildTarget(string[] args)
        {
            int index = Array.IndexOf(args, "-buildTarget");
            if (index >= 0 && index < args.Length - 1)
            {
                string targetString = args[index + 1];
                if (Enum.TryParse(targetString, out BuildTarget target))
                {
                    return target;
                }
            }

            // Default to current platform
            return EditorUserBuildSettings.activeBuildTarget;
        }

        private static string GetBuildPath(string[] args, BuildTarget target)
        {
            int index = Array.IndexOf(args, "-buildPath");
            if (index >= 0 && index < args.Length - 1)
            {
                return args[index + 1];
            }

            // Default paths
            return target switch
            {
                BuildTarget.StandaloneWindows64 => "builds/Windows/EscapeFromPiggy.exe",
                BuildTarget.StandaloneLinux64 => "builds/Linux/EscapeFromPiggy.x86_64",
                BuildTarget.StandaloneOSX => "builds/macOS/EscapeFromPiggy.app",
                BuildTarget.WebGL => "builds/WebGL",
                _ => "builds/Build"
            };
        }

        private static string[] FindEnabledScenes()
        {
            return EditorBuildSettings.scenes
                .Where(s => s.enabled)
                .Select(s => s.path)
                .ToArray();
        }

        private static string FormatBytes(ulong bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB" };
            double len = bytes;
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len /= 1024;
            }
            return $"{len:0.##} {sizes[order]}";
        }
    }
}
