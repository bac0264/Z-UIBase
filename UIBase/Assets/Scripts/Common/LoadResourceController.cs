﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;
using UnityEngine;

public class LoadResourceController
{
    public static readonly Dictionary<string, Object> resourceCache = new Dictionary<string, Object>();
    public static readonly Dictionary<string, Object[]> resourcesCache = new Dictionary<string, Object[]>();

    public static T LoadFromResource<T>(string path, string fileName = " ") where T : Object
    {
        string fullPath;
        if (!fileName.Equals(" "))
            fullPath = Path.Combine(path, fileName);
        else fullPath = path;
        if (!resourceCache.ContainsKey(fullPath))
        {
            resourceCache.Add(fullPath, Resources.Load<T>(fullPath));
        }
        return resourceCache[fullPath] as T;
    }
    public static T[] LoadFromResources<T>(string path, string fileName = " ") where T : Object
    {
        string fullPath;
        if (!fileName.Equals(" "))
            fullPath = Path.Combine(path, fileName);
        else fullPath = path;
        if (!resourcesCache.ContainsKey(fullPath))
        {
            T[] datas = Resources.LoadAll<T>(fullPath) as T[];
            resourcesCache.Add(fullPath, datas);
            return datas;
        }

        return resourcesCache[fullPath] as T[];
    }
    public static T[] LoadFromResourcesWithNoCache<T>(string path, string fileName = " ") where T : Object
    {
        string fullPath;
        if (!fileName.Equals(" "))
            fullPath = Path.Combine(path, fileName);
        else fullPath = path;
        return Resources.LoadAll<T>(fullPath) as T[];
    }
    
    #region Get Sprite
    public static Sprite GetItemIcon(int id)
    {
        return LoadFromResource<Sprite>(string.Format(PathUtils.iconItems, id));
    }
    
    public static Sprite GetCharacterItem(int id)
    {
        return LoadFromResource<Sprite>(string.Format(PathUtils.iconCharacters, id));
    }
    
    public static Sprite GetFrameWithPriority(int priority)
    {
        return LoadFromResource<Sprite>(string.Format(PathUtils.iconFrames, priority));
    }
    
    public static Sprite GetRawPackIcon(int id)
    {
        return LoadFromResource<Sprite>(string.Format(PathUtils.iconRawPacks, id));
    }
    
    public static Sprite GetBundleItemIcon(int id)
    {
        return LoadFromResource<Sprite>(string.Format(PathUtils.iconRawPacks, id));
    }
    
    public static Sprite GetMoneyIcon(int id)
    {
        return LoadFromResource<Sprite>(string.Format(PathUtils.iconCoins, id));
    }
    
    public static Sprite GetCurrencyIcon(int id)
    {
        return LoadFromResource<Sprite>(string.Format(PathUtils.iconCurrencies, id));
    }

    public static Sprite GetIconResource(int type, int id)
    {
        if (type == (int) ResourceType.ItemType)
        {
            return GetItemIcon(id);
        }
        else if(type == (int) ResourceType.MoneyType)
        {
            return GetCurrencyIcon(id);
        }
        else if(type == (int) ResourceType.CharacterType)
        {
            return GetCharacterItem(id);
        }

        return null;
    }
    
    public static Sprite GetStatIcon(int id)
    {
        return LoadFromResource<Sprite>(string.Format(PathUtils.iconStats, id));
    }

    #endregion
    
    #region Get Prefabs
    public static UIShopRawPackView GetRawPackView()
    {
        return LoadFromResource<UIShopRawPackView>(PathUtils.rawPackView);
    }
    
    public static UIShopBundleItemView GetBundleItemView()
    {
        return LoadFromResource<UIShopBundleItemView>(PathUtils.bundleItemView);
    }
    
    public static IconView GetIconView()
    {
        return LoadFromResource<IconView>(PathUtils.iconView);
    }
    
    public static MoneyBarView GetMoneyBarView()
    {
        return LoadFromResource<MoneyBarView>(PathUtils.moneyBarView);
    }
    
    public static StatView GetStatView()
    {
        return LoadFromResource<StatView>(PathUtils.statView);
    }
    
    #endregion
    
    #region Get Collection

    public static ShopRawPackCollection GetShopRawPackCollection()
    {
        return LoadFromResource<ShopRawPackCollection>(PathUtils.shopRawPack);
    }
    
    public static ShopBundleCollection GetShopBundleCollection()
    {
        return LoadFromResource<ShopBundleCollection>(PathUtils.shopBundle);
    }
    
    public static ItemStatCollection GetItemStat()
    {
        return LoadFromResource<ItemStatCollection>(PathUtils.itemStats);
    }
    
    public static CharacterStatCollection GetCharacterStat()
    {
        return LoadFromResource<CharacterStatCollection>(PathUtils.characterStats);
    }
    
    public static DefineCollection GetDefineCollection()
    {
        return LoadFromResource<DefineCollection>(PathUtils.defineCollection);
    }
    
    public static StatConfigCollection GetStatConfigCollection()
    {
        return LoadFromResource<StatConfigCollection>(PathUtils.statConfig);
    }

    public static UpgradeItemCollection GetUpgradeItemCollection()
    {
        return LoadFromResource<UpgradeItemCollection>(PathUtils.upgradeItem);
    }
    
    public static SellItemCollection GetSellItemCollection()
    {
        return LoadFromResource<SellItemCollection>(PathUtils.sellItem);
    }
    
    public static CharacterLevelCollection GetCharacterLevelCollection()
    {
        return LoadFromResource<CharacterLevelCollection>(PathUtils.characterLevel);
    }
    
    public static CharacterStatConfigCollection GetCharacterConfigCollection()
    {
        return LoadFromResource<CharacterStatConfigCollection>(PathUtils.characterConfig);
    }
    #endregion
}
