using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIModuleDailyRewardView : MonoBehaviour
{
    [SerializeField] private Transform viewAnchor;
    
    private List<DailyRewardView> dailyRewardViews = new List<DailyRewardView>();
    private DailyRewardCollection dailyRewardCollection = null;
    private DailyRewardView prefab = null;
    
    public void Awake()
    {
        prefab = LoadResourceController.GetDailyRewardView();
        dailyRewardCollection = LoadResourceController.GetDailyReward();
        
        UpdateView();
    }

    public void UpdateView()
    {
        int i = 0;
        for (; i < dailyRewardCollection.dataGroups.Length && i < dailyRewardViews.Count; i++)
        {
            var view = dailyRewardViews[i];
            view.SetupView(dailyRewardCollection.dataGroups[i]);
        }
        
        for (; i < dailyRewardCollection.dataGroups.Length; i++)
        {
            var view = Instantiate(prefab, viewAnchor);
            view.SetupView(dailyRewardCollection.dataGroups[i]);
        }
    }
}
