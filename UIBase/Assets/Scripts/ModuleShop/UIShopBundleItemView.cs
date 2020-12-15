using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UIShopBundleItemView : MonoBehaviour
{
    [SerializeField] private Reward[] rewardDatas = null;

    [SerializeField] private Text priceTxt = null;
    
    [SerializeField] private Image icon = null;
    
    [SerializeField] private Button purchaseBtn = null;

    [SerializeField] private Transform rewardAnchor = null;

    private BundlePackInfo bundlePackInfo;

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

    public void InitView(BundlePackInfo info, int id)
    {
        this.id = id;
        bundlePackInfo = info;

        rewardDatas = bundlePackInfo.rewards;
        icon.sprite = LoadResourceController.GetBundleItemIcon(id);
        priceTxt.text = bundlePackInfo.cost.ToString();

        for (int i = 0; i < rewardDatas.Length; i++)
        {
            var iconView = Instantiate(LoadResourceController.GetIconView(), rewardAnchor);
            iconView.SetData(rewardDatas[i].GetResource());
        }
    }

    private void OnClickPurchase()
    {
        
    }
}
