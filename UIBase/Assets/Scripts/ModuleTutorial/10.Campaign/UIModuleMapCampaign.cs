using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIModuleMapCampaign : MonoBehaviour
{
    [SerializeField] private Transform mapViewAnchor;
    [SerializeField] private Snap snap;
    [SerializeField] private UIModuleStageCampaign stageCampaign;
    
    private CampaignModeConfig mode;
    
    private List<CampaignMapView> mapViews = new List<CampaignMapView>();
    private CampaignMapView prefab = null;

    public void InitOrUpdateView(CampaignModeConfig mode)
    {
        this.mode = mode;
        if (prefab == null) prefab = LoadResourceController.GetCampaignMapView();
        
        int i = 0;
        for (; i < mode.mapList.Count; i++)
        {
            if (i < mapViews.Count)
            {
                mapViews[i].SetupView(mode.mapList[i]);
            }
            else
            {
                var view = Instantiate(prefab, mapViewAnchor);
                view.SetupView(mode.mapList[i]);
                mapViews.Add(view);
                snap.AddRectTransform(view.GetComponent<RectTransform>());
            }
        }
        snap.SetupSnap();
    }

    public void OnClickGo()
    {
        stageCampaign.gameObject.SetActive(true);
        stageCampaign.UpdateView(mode.mapList[snap.GetIndex()]);
        gameObject.SetActive(false);
    }
}