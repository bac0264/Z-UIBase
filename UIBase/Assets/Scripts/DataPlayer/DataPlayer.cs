using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public enum DataPlayerType
{
    
}
public static class DataPlayer
{
    // public static readonly Dictionary<DataPlayerType, object>
    //     ResgisteredModules = new Dictionary<DataPlayerType, object>();
    
    private static PlayerMoney _playerMoney;
    private static PlayerInventory _playerInventory;
    private static PlayerCharacter _playerCharacter;
    
    #region Set Get
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
    #endregion

    // private static void GetModule(DataPlayerType dataPlayerType, Type moduleType)
    // {
    //     if (ResgisteredModules.ContainsKey(dataPlayerType))
    //     {
    //         return 
    //     }
    //     //Tìm constructor đầu tiên
    //     var firstConstructor = moduleType.GetConstructors()[0];
    //     object module = null;
    //     //Nếu như không có tham số
    //     if (!firstConstructor.GetParameters().Any())
    //     {
    //         //Khởi tạo module
    //         module = firstConstructor.Invoke(null); // nếu có constructor con khởi tạo dữ liệu bên trong firstCons
    //     }
    //     else
    //     {
    //         Debug.Log("!! Warning, Not support Constructor with params");
    //     }
    //     //Lưu trữ interface và module tương ứng
    //     ResgisteredModules.Add(dataPlayerType, module);
    // }
}

[System.Serializable]
public class DataSave<T>
{
    public List<T> dataList;
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
