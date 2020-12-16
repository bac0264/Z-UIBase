﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShopRawPackView : MonoBehaviour
{
    [SerializeField] private Text priceTxt;
    [SerializeField] private Text packNameTxt;
    [SerializeField] private Text freeTxt;
    [SerializeField] private Text freeCountTxt;
    [SerializeField] private Text boughtCountTxt; 
    
    [SerializeField] private Image icon;

    [SerializeField] private Button purchaseBtn;
    [SerializeField] private Button freeBtn;

    [SerializeField] private Reward[] rewardDatas;

    [SerializeField] private RawPackInfo info;

    [SerializeField] private int id;

    private void Awake()
    {
        InitButtons();
        InitLocalize();
    }

    public void InitView(RawPackInfo info, int id)
    {
        this.id = id;
        this.info = info;
        
        icon.sprite = LoadResourceController.GetRawPackIcon(id);
        priceTxt.text = info.cost.ToString();

        RefreshUI();
    }

    private void RefreshUI()
    {
        var isFree = info.free && DataPlayer.PlayerShop.IsAvailableForBuying(ShopEnum.RAW_PACK_FREE, id, info.stockFree);
        
        freeBtn.gameObject.SetActive(isFree);
        purchaseBtn.gameObject.SetActive(!isFree);

        freeCountTxt.text = DataPlayer.PlayerShop.GetBoughtCount(ShopEnum.RAW_PACK_FREE, id).ToString() +"/" + info.stockFree;
        boughtCountTxt.text = DataPlayer.PlayerShop.GetBoughtCount(ShopEnum.RAW_PACK, id).ToString();
    }
    
    private void InitButtons()
    {
        purchaseBtn.onClick.AddListener(OnClickPurchase);
        freeBtn.onClick.AddListener(OnClickFreeBtn);
    }

    private void InitLocalize()
    {
        freeTxt.text = "Free";
    }

    private void OnClickFreeBtn()
    {
        void onSuccess()
        {
            Debug.Log(" ironsource success");
            DataPlayer.PlayerShop.AddBought(ShopEnum.RAW_PACK_FREE, id);
            AddRewards();
        }

        IronSourceManager.instance.ShowRewardedVideo(onSuccess);
    }

    private void OnClickPurchase()
    {
        void onSuccess()
        {
            Debug.Log(" IAP success");
            DataPlayer.PlayerShop.AddBought(ShopEnum.RAW_PACK, id);
            AddRewards();
            
        }

        IAPManager.Instance.Buy(info.packnameIap, onSuccess);
    }

    private void AddRewards()
    {
        for (int i = 0; i < rewardDatas.Length; i++)
        {
            rewardDatas[i].RecieveReward();
        }

        RefreshUI();
    }
}