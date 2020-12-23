using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;


// public enum DataPlayerType
// {
//     PlayerMoney,
//     PlayerInventory,
//     PlayerCharacter,
//     PlayerShop,
//     PlayerDailyReward,
//     PlayerGacha,
//     PlayerAds,
// }

public static class DataPlayer
{
    #region Set Get
    private static PlayerMoney _playerMoney;
    private static PlayerInventory _playerInventory;
    private static PlayerCharacter _playerCharacter;
    private static PlayerShop _playerShop;
    private static PlayerDailyReward _playerDailyReward;
    private static PlayerGacha _playerGacha;
    private static PlayerAds _playerAds;

    
    public static PlayerMoney PlayerMoney
    {
        get
        {
            if (_playerMoney == null) _playerMoney = new PlayerMoney();
            return _playerMoney;
        }
    }
    
    public static PlayerInventory PlayerInventory
    {
        get
        {
            if (_playerInventory == null) _playerInventory = new PlayerInventory();
            return _playerInventory;
        }
    }
    
    public static PlayerCharacter PlayerCharacter
    {
        get
        {
            if (_playerCharacter == null) _playerCharacter = new PlayerCharacter();
            return _playerCharacter;
        }
    }
    
    public static PlayerShop PlayerShop
    {
        get
        {
            if (_playerShop == null) _playerShop = new PlayerShop();
            return _playerShop;
        }
    }
    
    public static PlayerDailyReward PlayerDailyReward
    {
        get
        {
            if (_playerDailyReward == null) _playerDailyReward = new PlayerDailyReward();
            return _playerDailyReward;
        }
    }
    
    public static PlayerGacha PlayerGacha
    {
        get
        {
            if (_playerGacha == null) _playerGacha = new PlayerGacha();
            return _playerGacha;
        }
    }
    
    #endregion

    #region Option1
    private static readonly Dictionary<Type, object>
        ResgisteredModules = new Dictionary<Type, object>();
    
    public static T GetModule<T>()
    {
        return (T) _GetModule(typeof(T));
    }

    private static object _GetModule(Type moduleType)
    {
        if (ResgisteredModules.ContainsKey(moduleType))
        {
            return ResgisteredModules[moduleType];
        }

        var firstConstructor = moduleType.GetConstructors()[0];
        
        object module = null;
        
        if (!firstConstructor.GetParameters().Any())
        {
            module = firstConstructor.Invoke(null);
        }
        else
        {
            Debug.Log("!! Warning, Not support Constructor with params");
        }
        
        ResgisteredModules.Add(moduleType, module);
        return module;
    }
    #endregion
    
    #region Option2
    //
    // private static readonly Dictionary<DataPlayerType, object>
    //     ResgisteredModules2 = new Dictionary<DataPlayerType, object>();
    // public static T GetModule2<T>(DataPlayerType dataPlayerType)
    // {
    //     if (typeof(T).ToString().Equals(dataPlayerType.ToString()))
    //         return (T) _GetModule2(dataPlayerType);
    //     return default;
    // }
    //
    // private static object _GetModule2(DataPlayerType dataPlayerType)
    // {
    //     if (ResgisteredModules2.ContainsKey(dataPlayerType))
    //     {
    //         return ResgisteredModules2[dataPlayerType];
    //     }
    //
    //     Type moduleType = Type.GetType(dataPlayerType.ToString());
    //     
    //     var firstConstructor = moduleType.GetConstructors()[0];
    //     
    //     object module = null;
    //     
    //     if (!firstConstructor.GetParameters().Any())
    //     {
    //         module = firstConstructor.Invoke(null);
    //     }
    //     else
    //     {
    //         Debug.Log("!! Warning, Not support Constructor with params");
    //     }
    //     
    //     ResgisteredModules2.Add(dataPlayerType, module);
    //     return module;
    // }
     #endregion
}

[System.Serializable]
public class DataSave<T>
{
    [JsonProperty("0")] public List<T> dataList;

    public DataSave()
    {
        dataList = new List<T>();
    }

    public virtual void AddData(T t)
    {
        dataList.Add(t);
    }

    public virtual void RemoveData(T t)
    {
        dataList.Remove(t);
    }
}
