using System;
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
    public BundlePackInfo[] dataGroups;

    public List<string> GetShopBundleProductIds()
    {
        var list = new List<string>();
        for (int i = 0; i < dataGroups.Length; i++)
        {
            list.Add(dataGroups[i].packnameIap);
        }

        return list;
    }
}

#if UNITY_EDITOR
public class ShopBundleProcessor : BasePostProcessor
{
    public ShopBundleProcessor()
    {
        if (!isRun)
        {
            assetfile = PathUtils.shopBundle;
            classScriptObject = "ShopBundleCollection";
            classData = "BundlePackInfo";
            isRun = true;
        }
    }
}
#endif
