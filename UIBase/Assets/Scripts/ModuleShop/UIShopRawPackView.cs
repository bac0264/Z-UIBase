using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShopRawPackView : MonoBehaviour
{
    [SerializeField] private Text priceTxt;
    [SerializeField] private Text packNameTxt;
    
    [SerializeField] private Image icon;
    
    [SerializeField] private Button purchaseBtn;
    [SerializeField] private Button freeBtn;

    [SerializeField] private List<Reward> rewardDatas;

    [SerializeField] private Resource price;
    public int id;
    
    private void Awake()
    {
        InitButtons();
        InitView();
    }

    private void InitView()
    {
        price = Resource.CreateInstance((int)ResourceType.MoneyType, (int)MoneyType.GEM, 20 * (id + 1));
        var rewardNumber = 100 * (id + 1);
        icon.sprite = LoadResourceController.GetRawPackIcon(id);
        packNameTxt.text = "Raw pack: " + (id + 1);
        priceTxt.text = price.number + "$";

        rewardDatas = new List<Reward>();
        rewardDatas.Add(Reward.CreateInstanceReward(UnityEngine.Random.Range(0,2), UnityEngine.Random.Range(0,3), rewardNumber));
    }
    
    
    private void InitButtons()
    {
        purchaseBtn.onClick.AddListener(OnClickPurchase);
        freeBtn.onClick.AddListener(OnClickFreeBtn);
    }
    
    private void OnClickFreeBtn()
    {
        void onSuccess()
        {
            
        }
        IronSourceManager.instance.ShowRewardedVideo(onSuccess);
    }

    private void OnClickPurchase()
    {
        var canPurchase = DataPlayer.PlayerMoney.IsEnoughMoney(price);
        if (canPurchase)
        {
            for (int i = 0; i < rewardDatas.Count; i++)
            {
                rewardDatas[i].RecieveReward();
            }
        }
    }
}
