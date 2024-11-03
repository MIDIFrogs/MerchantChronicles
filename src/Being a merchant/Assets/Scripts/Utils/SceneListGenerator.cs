#if UNITY_EDITOR
using SibGameJam;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SibGameJam.Utils
{
    public class SceneListGenerator : MonoBehaviour
    {
        [MenuItem("Tools/Generate Playable Scene List")]
        public static void GenerateSceneList()
        {
            SceneList sceneList = ScriptableObject.CreateInstance<SceneList>();

            string[] scenePaths = AssetDatabase.FindAssets("t:Scene");
            foreach (string scenePath in scenePaths)
            {
                string path = AssetDatabase.GUIDToAssetPath(scenePath);
                string sceneName = System.IO.Path.GetFileNameWithoutExtension(path);
                if (!sceneName.StartsWith("Game Segment"))
                    continue;
                sceneList.SceneNames.Add(sceneName);
            }

            AssetDatabase.CreateAsset(sceneList, "Assets/Resources/PlayableSceneList.asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
#endif