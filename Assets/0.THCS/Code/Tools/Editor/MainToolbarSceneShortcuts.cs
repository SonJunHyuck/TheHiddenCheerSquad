using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.Toolbars;
using UnityEngine;

namespace THCS.Editor
{
    public static class MainToolbarSceneShortcuts
    {
        private const string SceneFolderPath = "Assets/0.THCS/Scenes";
        private const string DefaultScenePath = "Assets/0.THCS/Scenes/Title.unity";

        [MainToolbarElement("THCS/Scene/Open Title", defaultDockPosition = MainToolbarDockPosition.Middle)]
        public static MainToolbarElement OpenTitleSceneButton()
        {
            var icon = EditorGUIUtility.IconContent("SceneAsset Icon").image as Texture2D;
            var content = new MainToolbarContent("Title", icon, "Open the Title scene");
            return new MainToolbarButton(content, () => OpenSceneSingle(DefaultScenePath));
        }

        [MainToolbarElement("THCS/Scene/Open From Folder", defaultDockPosition = MainToolbarDockPosition.Middle)]
        public static MainToolbarElement OpenSceneDropdown()
        {
            var icon = EditorGUIUtility.IconContent("SceneAsset Icon").image as Texture2D;
            var content = new MainToolbarContent("Scenes", icon, $"Open a scene from {SceneFolderPath}");
            return new MainToolbarDropdown(content, ShowSceneMenu);
        }

        private static void ShowSceneMenu(Rect rect)
        {
            var menu = new GenericMenu();
            var scenePaths = GetScenePathsFromFolder(SceneFolderPath);

            if (scenePaths.Length == 0)
            {
                menu.AddDisabledItem(new GUIContent("No scenes found"));
                menu.DropDown(rect);
                return;
            }

            foreach (var scenePath in scenePaths)
            {
                var capturedScenePath = scenePath;
                var menuLabel = GetSceneMenuLabel(scenePath);
                menu.AddItem(new GUIContent(menuLabel), false, () => OpenSceneSingle(capturedScenePath));
            }

            menu.DropDown(rect);
        }

        private static string[] GetScenePathsFromFolder(string folderPath)
        {
            return AssetDatabase.FindAssets("t:Scene", new[] { folderPath })
                .Select(AssetDatabase.GUIDToAssetPath)
                .Where(path => path.StartsWith(folderPath, StringComparison.Ordinal))
                .OrderBy(path => path)
                .ToArray();
        }

        private static string GetSceneMenuLabel(string scenePath)
        {
            var relativePath = scenePath.Substring(SceneFolderPath.Length).TrimStart('/', '\\');
            return Path.ChangeExtension(relativePath, null)?.Replace('\\', '/')
                ?? Path.GetFileNameWithoutExtension(scenePath);
        }

        private static void OpenSceneSingle(string scenePath)
        {
            if (!AssetDatabase.LoadAssetAtPath<SceneAsset>(scenePath))
            {
                Debug.LogWarning($"Scene not found: {scenePath}");
                return;
            }

            if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                return;

            EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);
        }
    }
}
