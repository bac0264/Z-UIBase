using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CampaignMapView : MonoBehaviour
{
    [SerializeField] private Image icon;
    
    [SerializeField] private Text mapIdText;
    [SerializeField] private Text statusText;
    
    [SerializeField] private CampaignMapConfig map;

    public void SetupView(CampaignMapConfig map)
    {
        this.map = map;

        var currentMapId = CampaignMapConfig.GetMapId(DataPlayer.GetModule<PlayerCampaign>().dataCampaign.currentStage);
        
        if (map.mapId < currentMapId)
            statusText.text = "completed";
        else if (currentMapId == map.mapId)
            statusText.text = "open";
        else
            statusText.text = "lock";

        icon.sprite = LoadResourceController.GetCampaignMapIcon(map.mapId);
        
        mapIdText.text = "Map: " + map.mapId;
    }
}
