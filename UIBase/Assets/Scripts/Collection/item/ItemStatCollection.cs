﻿using System.Collections;
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
    public int[] statTypes;

    private StatConfigCollection statConfigCollection = null;
    public ItemStat[] GetItemStats(int level)
    {
        if (statConfigCollection == null) statConfigCollection = LoadResourceController.GetStatConfigCollection();
        
        List<ItemStat> itemStatList = new List<ItemStat>();
        for (int i = 0; i < statTypes.Length; i++)
        {
            var baseValue = statConfigCollection.GetStatConfigData(statTypes[i]).GetBaseValue(level);
            ItemStat itemStat = new ItemStat(baseValue, statTypes[i]);
            itemStatList.Add(itemStat);
        }

        return itemStatList.ToArray();
    }
}