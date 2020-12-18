using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyRewardView : MonoBehaviour
{

    [SerializeField] private Transform rewardAnchor = null;
    
    [SerializeField] private Text dayText = null;
    [SerializeField] private Text nameText = null;

    [SerializeField] private GameObject highLight = null;
    [SerializeField] private GameObject receivedCard = null;
    [SerializeField] private GameObject openedCard = null;

    private List<IconView> iconViews = new List<IconView>();
    private DailyRewardData dailyRewardData;
    private IconView prefab = null;
    private void Awake()
    {
        prefab = LoadResourceController.GetIconView();

    }

    public void SetupView(DailyRewardData dailyRewardData)
    {
        this.dailyRewardData = dailyRewardData;

        SetupIconViews();
        SetupUI();
    }

    private void SetupIconViews()
    {
        int i = 0;
        for( ; i < dailyRewardData.rewards.Length && i < iconViews.Count ; i++)
        {
            iconViews[i].SetData(dailyRewardData.rewards[i].GetResource());
        }
        
        for( ; i < dailyRewardData.rewards.Length ; i++)
        {
            var iconView = Instantiate(prefab, rewardAnchor);
            iconView.SetData(dailyRewardData.rewards[i].GetResource());
            iconViews.Add(iconView);
        }
    }

    private void SetupUI()
    {
        nameText.text = "Day: " + dailyRewardData.id;
        dayText.text = dailyRewardData.id.ToString();
        
        var isReceived = dailyRewardData.IsReceived();
        var isReceivable = dailyRewardData.IsReceivable();
        
        receivedCard.SetActive(isReceived);
        openedCard.SetActive(!isReceived);
        highLight.SetActive(!isReceived && isReceivable);
    }
}
