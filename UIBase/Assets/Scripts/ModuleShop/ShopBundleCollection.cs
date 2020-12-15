using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[System.Serializable]
public class BundlePackInfo : RawPackInfo
{
}
[System.Serializable]
public class ShopBundleCollection : ScriptableObject
{
    public BundlePackInfo[] shopGroups;
}

#if UNITY_EDITOR
public class ShopBundleProcessor : AssetPostprocessor
{
    static string csv = PathUtils.shopBundle + ".csv";
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
                
                ShopBundleCollection gm = AssetDatabase.LoadAssetAtPath<ShopBundleCollection>(assetfile);
                if (gm == null)
                {
                    gm = ScriptableObject.CreateInstance<ShopBundleCollection>();
                    AssetDatabase.CreateAsset(gm, assetfile);
                }
                
                gm.shopGroups = CSVSerializer.Deserialize<BundlePackInfo>(data.text);
                
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
