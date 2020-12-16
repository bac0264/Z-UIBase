using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class ItemStatCollection : ScriptableObject
{
    public ItemStatData[] dataGroups;

    public ItemStatData GetItemStatDataWithItemId(int id)
    {
        for (int i = 0; i < dataGroups.Length; i++)
        {
            if (id == dataGroups[i].id)
            {
                return dataGroups[i];
            }
        }

        return null;
    }
}

[System.Serializable]
public class ItemStatData
{
    public int id;
    public ItemStat[] itemStats;
}

#if UNITY_EDITOR
public class ItemStatProcessor : BasePostProcessor
{
    public ItemStatProcessor()
    {
        if (!isRun)
        {
            assetfile = PathUtils.itemStats;
            classScriptObject = "ItemStatCollection";
            classData = "ItemStatData";
            isRun = true;
        }
    }
}
#endif