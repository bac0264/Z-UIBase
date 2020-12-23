using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class PlayerCampaign
{
    public PlayerCampaignSaveLoad dataCampaign;

    public PlayerCampaign()
    {
        Load();
    }

    public void Save()
    {
        PlayerPrefs.SetString(KeyUtils.CAMPAIGN_DATA, JsonConvert.SerializeObject(dataCampaign));
    }

    public void Load()
    {
        dataCampaign = JsonConvert.DeserializeObject<PlayerCampaignSaveLoad>(PlayerPrefs.GetString(KeyUtils.CAMPAIGN_DATA));
        if (dataCampaign == null)
        {
            dataCampaign = new PlayerCampaignSaveLoad {currentStage = 101001};
        }
    }

}

[System.Serializable]
public class PlayerCampaignSaveLoad{
    [JsonProperty("0")] public int currentStage;
}
