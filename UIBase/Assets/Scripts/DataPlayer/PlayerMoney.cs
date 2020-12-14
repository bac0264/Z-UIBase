using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

public class PlayerMoney
{
    private DataSave<Resource> resourceList = new DataSave<Resource>();
    private Dictionary<MoneyType, Resource> resourceDic = new Dictionary<MoneyType, Resource>();

    public PlayerMoney()
    {
        Load();
    }

    public bool AddOne(MoneyType id, long value)
    {
        if (resourceDic.ContainsKey(id))
        {
            bool check = resourceDic[id].Add(value);
            if (check) Save();
            return check;
        }
        else
        {
            Resource resource = Resource.CreateInstance((int) ResourceType.MoneyType, (int) id, value);
            resourceDic.Add(id, resource);
            resourceList.AddData(resource);
            Save();
            return true;
        }
    }

    public bool SubOne(MoneyType id, long value)
    {
        if (resourceDic.ContainsKey(id))
        {
            bool check = resourceDic[id].Sub(value);
            if (check) Save();
            return check;
        }
        else
        {
            Resource resource = Resource.CreateInstance((int) ResourceType.MoneyType, (int) id, 0);
            resourceDic.Add(id, resource);
            resourceList.AddData(resource);
            Save();
            return true;
        }
    }

    public void AddManyMoney(List<Resource> dataList)
    {
        for (int i = 0; i < dataList.Count; i++)
        {
            MoneyType id = (MoneyType) dataList[i].id;
            long value = dataList[i].number;
            if (resourceDic.ContainsKey(id))
            {
                resourceDic[id].Add(dataList[i].number);
            }
            else
            {
                Resource resource = new Resource((int) ResourceType.MoneyType, (int) id, value);
                resourceDic.Add(id, resource);
                resourceList.AddData(resource);
            }
        }

        Save();
    }

    public void SubManyMoney(List<Resource> dataList)
    {
        for (int i = 0; i < dataList.Count; i++)
        {
            MoneyType id = (MoneyType) dataList[i].id;
            if (resourceDic.ContainsKey(id))
            {
                resourceDic[id].Sub(dataList[i].number);
            }
            else
            {
                Resource resource = new Resource((int) ResourceType.MoneyType, (int) id, 0);
                resourceDic.Add(id, resource);
                resourceList.AddData(resource);
            }
        }

        Save();
    }

    public Resource GetMoney(MoneyType id)
    {
        if (resourceDic.ContainsKey(id))
        {
            return resourceDic[id];
        }

        Resource resource = new Resource((int) ResourceType.MoneyType, (int) id, 0);
        resourceDic.Add(id, resource);
        resourceList.AddData(resource);
        Save();

        return resource;
    }

    public bool IsEnoughMoney(Resource requireResource)
    {
        var myMoney = GetMoney((MoneyType) requireResource.id);
        if (myMoney.number - requireResource.number >= 0)
        {
            return true;
        }

        return false;
    }

    public List<Resource> GetMoneyList()
    {
        return resourceList.dataList;
    }

    public void Load()
    {
        resourceList = JsonConvert.DeserializeObject<DataSave<Resource>>(PlayerPrefs.GetString(KeyUtils.RESOURCE_DATA));
        if (resourceList == null)
        {
            resourceList = new DataSave<Resource>();
        }

        for (int i = 0; i < resourceList.dataList.Count; i++)
        {
            resourceDic.Add((MoneyType) resourceList.dataList[i].id, resourceList.dataList[i]);
        }
    }

    public void Save()
    {
        PlayerPrefs.SetString(KeyUtils.RESOURCE_DATA, JsonConvert.SerializeObject(resourceList));
    }

    public static int GetRealItemId(int id)
    {
        return id / 1000;
    }
}