using System.IO;
using UnityEngine;
using UnityEditor;
using VContainer.Unity;
using System.Linq;

/// <summary>
/// Editor Menu Item để quản lý VContainer Setting
/// </summary>

namespace RSFramework
{
    public class VContainerEditor
    {
        private const string SETTING_NAME = "VContainerSettings";
        private const string GAMEPLAY_FOLDER = "Assets/GamePlay";
        private const string ASSETS_FOLDER = "Assets";

        [MenuItem("RSFramework/VContainer Setting")]
        public static void OpenVContainerSetting()
        {
            var setting = FindOrCreateVContainerSetting();

            if (setting != null)
            {
                // Highlight object trong Project
                EditorGUIUtility.PingObject(setting);

                // Select và show trong Inspector
                Selection.activeObject = setting;
                EditorUtility.FocusProjectWindow();
            }
            else
            {
                EditorUtility.DisplayDialog("Error",
                    "Không thể tìm hoặc tạo VContainerSetting!",
                    "OK");
            }
        }

        /// <summary>
        /// Tìm hoặc tạo VContainer Setting ScriptableObject
        /// </summary>
        public static VContainerSettings FindOrCreateVContainerSetting()
        {
            // Tìm VContainerSetting trong project
            string[] guids = AssetDatabase.FindAssets($"{SETTING_NAME} t:VContainerSettings");

            if (guids.Length > 0)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guids[0]);
                var setting = AssetDatabase.LoadAssetAtPath<VContainerSettings>(assetPath);

                if (setting != null)
                {
                    Debug.Log($"✓ Tìm thấy VContainerSetting tại: {assetPath}");
                    return setting;
                }
            }

            // Nếu không tìm thấy, tạo mới
            return CreateNewVContainerSetting();
        }

        /// <summary>
        /// Tạo VContainerSetting mới
        /// </summary>
        private static VContainerSettings CreateNewVContainerSetting()
        {
            // Thử tạo trong folder Assets/GamePlay trước
            string targetFolder = GAMEPLAY_FOLDER;

            // Nếu folder không tồn tại, tạo mới
            if (AssetDatabase.IsValidFolder(targetFolder))
            {
                targetFolder = GAMEPLAY_FOLDER;
            }
            else
            {
                targetFolder = ASSETS_FOLDER;
            }

            // Tạo instance mới
            var newSetting = ScriptableObject.CreateInstance<VContainerSettings>();

            // Path cho asset mới
            string assetPath = Path.Combine(targetFolder, $"{SETTING_NAME}.asset");
            assetPath = AssetDatabase.GenerateUniqueAssetPath(assetPath);

            // Lưu asset
            AssetDatabase.CreateAsset(newSetting, assetPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            //Save preload Asset để Root Setting được load.
            var preloadedAssets = UnityEditor.PlayerSettings.GetPreloadedAssets().ToList();
            preloadedAssets.RemoveAll(x => x is VContainerSettings);
            preloadedAssets.Add(newSetting);
            UnityEditor.PlayerSettings.SetPreloadedAssets(preloadedAssets.ToArray());

            Debug.Log($"✓ Tạo VContainerSetting mới tại: {assetPath}");
            return newSetting;
        }
    }
}