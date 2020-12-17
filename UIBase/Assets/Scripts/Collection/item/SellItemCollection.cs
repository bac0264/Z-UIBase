using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SellItemCollection : ScriptableObject
{
    public SellItemData[] dataGroups;

    public SellItemData GetSellData(int priority)
    {
        for (int i = 0; i < dataGroups.Length; i++)
        {
            if (dataGroups[i].priority == priority)
            {
                return dataGroups[i];
            }
        }

        return null;
    }
}

[System.Serializable]
public class SellItemData
{
    public int priority;
    public Reward sellRequire;
    public float option1;

    public Resource GetPrice()
    {
        var resource = sellRequire.GetResource();
        resource.number = sellRequire.resNumber + priority * (long) option1;
        return resource;
    }
}