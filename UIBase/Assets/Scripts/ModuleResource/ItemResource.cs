using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatType
{
    Atk = 0,
    Hp = 1,
    Def = 2,
    Speed = 3,
}

[System.Serializable]

public class ItemResource : Resource
{
    [NonSerialized] public ItemStat[] itemStats;

    public int inventoryId;

    public int level;

    /// <summary>
    /// Item constructor with params
    /// </summary>
    /// <param name="type"> A type in ResourceType. </param>
    /// <param name="id"> Unique identify of a type. </param>
    /// <param name="number"> Number of resource.. </param>
    /// <param name="itemStats"> Data of item including BaseStat and StatType. </param>
    /// <returns></returns>
    public ItemResource(int type, int id, long number, int inventoryId, int level, ItemStat[] itemStats = null) : base(type, id, number)
    {
        this.level = level;

        this.itemStats = itemStats;

        this.inventoryId = inventoryId;
    }

    public ItemResource GetCopy()
    {
        return new ItemResource(type, id, number, inventoryId, level, itemStats);
    }

    public int GetPriority()
    {
        return id % 1000;
    }
    
    public static ItemResource CreateInstance(int type, int id, long number,int inventoryId, int level, ItemStat[] itemStats = null)
    {
        return new ItemResource(type, id, number, inventoryId, level, itemStats);
    }
}

[System.Serializable]
public class ItemStat
{
    public BaseStat baseStat;

    /// <summary>
    /// (StatType) Stat type of Item to get key localize.
    /// </summary>
    public int statType;

    public ItemStat(float _baseStat, StatType _statType)
    {
        this.baseStat = BaseStat.CreateInstance(_baseStat);
        this.statType = (int)_statType;
    }
    
    public ItemStat(float _baseStat, int _statType)
    {
        this.baseStat = BaseStat.CreateInstance(_baseStat);
        this.statType = (int)_statType;
    }

    public ItemStat GetCopy()
    {
        return new ItemStat(baseStat.Value, statType);    
    }
    
    public string GetLocalize()
    {
        var localize = (StatType) statType + ": +" + baseStat.Value;
        return localize;
    }
}
