using System;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
public class BasePostProcessor : AssetPostprocessor
{
    public static string assetfile = "/";

    public static string classScriptObject = "/";

    public static string classData = "/";

    public static bool isRun = false;
    
    public BasePostProcessor()
    {
    }

    public static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets,
        string[] movedFromAssetPaths)
    {
        var asset = assetfile;
        
        string[] csv = asset.Split('/');
        
        var data = csv[csv.Length - 1] + ".csv";
        
        Setup(data, importedAssets);
    }

    static void Setup(string path, string[] importedAssets)
    {
        foreach (string str in importedAssets)
        {
            if (str.IndexOf(path) != -1)
            {
                TextAsset data = AssetDatabase.LoadAssetAtPath<TextAsset>(str);

                var fullPath = "Assets/Resources/" + assetfile + ".asset";
                var gm = AssetDatabase.LoadAssetAtPath(fullPath, Type.GetType(classScriptObject));
                if (gm == null)
                {
                    gm = ScriptableObject.CreateInstance(Type.GetType(classScriptObject));
                    AssetDatabase.CreateAsset(gm, fullPath);
                }
                Type type = Type.GetType(classData);

                var field = gm.GetType().GetField("dataGroups");
                
                field.SetValue(gm, CSVSerializer.Deserialize(data.text, type));
                
                EditorUtility.SetDirty(gm);
                AssetDatabase.SaveAssets();
                isRun = false;
#if DEBUG_LOG || UNITY_EDITOR
                Debug.Log("Reimported Asset: " + assetfile);
#endif    
            }
        }
    }
}
#endif