﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UIShopBundleItemView : MonoBehaviour
{
    [SerializeField] private Reward[] rewardDatas = null;

    [SerializeField] private Text priceTxt = null;
    [SerializeField] private Text boughtCountTxt = null;
    
    [SerializeField] private Image icon = null;
    
    [SerializeField] private Button purchaseBtn = null;

    [SerializeField] private Transform rewardAnchor = null;

    private BundlePackInfo info;

    private int id;
    private void Awake()
    {
        InitLocalize();
        InitButtons();
    }

    private void InitLocalize()
    {
        
    }

    private void InitButtons()
    {
        purchaseBtn.onClick.AddListener(OnClickPurchase);
    }

    private void RefreshUI()
    {
        purchaseBtn.gameObject.SetActive(true);
        
        boughtCountTxt.text = DataPlayer.PlayerShop.GetBoughtCount(ShopEnum.BUNDLE, id).ToString();
    }
    
    public void InitView(BundlePackInfo info, int id)
    {
        this.id = id;
        this.info = info;

        rewardDatas = info.rewards;
        icon.sprite = LoadResourceController.GetBundleItemIcon(id);
        priceTxt.text = info.cost.ToString();

        for (int i = 0; i < rewardDatas.Length; i++)
        {
            var iconView = Instantiate(LoadResourceController.GetIconView(), rewardAnchor);
            iconView.SetData(rewardDatas[i].GetResource());
        }

        RefreshUI();
    }

    private void OnClickPurchase()
    {
        void onSuccess()
        {
            Debug.Log(" IAP success");
            DataPlayer.PlayerShop.AddBought(ShopEnum.BUNDLE, id);
            AddRewards();
            
        }

        Debug.Log(IAPManager.Instance);
        Debug.Log(info);
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