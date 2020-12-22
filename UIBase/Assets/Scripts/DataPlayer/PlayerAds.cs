using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using UnityEngine;

[System.Serializable]
public class PlayerAds
{
    private PlayerAdsSaveLoad adsSaveLoad;

    public PlayerAds()
    {
        Load();
    }

    public void Load()
    {
        adsSaveLoad = JsonConvert.DeserializeObject<PlayerAdsSaveLoad>(PlayerPrefs.GetString(KeyUtils.ADS_DATA));
        if (adsSaveLoad == null)
        {
            adsSaveLoad = new PlayerAdsSaveLoad();
            adsSaveLoad.adsCount = 0;
        }
    }

    public void Save()
    {
        PlayerPrefs.SetString(KeyUtils.ADS_DATA, JsonConvert.SerializeObject(adsSaveLoad));
    }

    public void AddAds(int value)
    {
        adsSaveLoad.adsCount += value;
    }
}

[System.Serializable]
public class PlayerAdsSaveLoad
{
    public int adsCount ;
}
