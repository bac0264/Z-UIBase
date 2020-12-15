using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class ShopRawPackCollection : ScriptableObject
{
    public RawPackInfo[] shopGroups;
}

#if UNITY_EDITOR
public class ShopRawProcessor : AssetPostprocessor
{
    static string csv = PathUtils.shopRawPack + ".csv";
    static string path = PathUtils.shopPath;
    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        string fullPath = path + csv;

        Setup(csv, fullPath, importedAssets);
    }

    static void Setup(string path, string fullPath, string[] importedAssets)
    {
        foreach (string str in importedAssets)
        {
            if (str.IndexOf(path) != -1)
            {
                var assetPath = str.Replace(path, fullPath);
                
                TextAsset data = AssetDatabase.LoadAssetAtPath<TextAsset>(str);
                
                string assetfile = assetPath.Replace(".csv", ".asset");
                
                ShopRawPackCollection gm = AssetDatabase.LoadAssetAtPath<ShopRawPackCollection>(assetfile);
                if (gm == null)
                {
                    gm = ScriptableObject.CreateInstance<ShopRawPackCollection>();
                    AssetDatabase.CreateAsset(gm, assetfile);
                }
                
                gm.shopGroups = CSVSerializer.Deserialize<RawPackInfo>(data.text);
                
                EditorUtility.SetDirty(gm);
                AssetDatabase.SaveAssets();
#if DEBUG_LOG || UNITY_EDITOR
                Debug.Log("Reimported Asset: " + assetPath);
#endif
            }
        }
    }
}
#endif
