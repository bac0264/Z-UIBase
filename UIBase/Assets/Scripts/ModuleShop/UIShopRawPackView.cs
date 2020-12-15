using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShopRawPackView : MonoBehaviour
{
    [SerializeField] private Text priceTxt;
    [SerializeField] private Text packNameTxt;
    [SerializeField] private Text freeTxt;

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
    }

    private void RefreshUI()
    {
        var count = DataPlayer.PlayerMoney.GetMoney(MoneyType.DAILY_RAW_PACK_COUNT).number;
    }
    private void InitButtons()
    {
        purchaseBtn.onClick.AddListener(OnClickPurchase);
        freeBtn.onClick.AddListener(OnClickFreeBtn);
        freeBtn.gameObject.SetActive(false);
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
            AddRewards();
        }

        IronSourceManager.instance.ShowRewardedVideo(onSuccess);
    }

    private void OnClickPurchase()
    {
        void onSuccess()
        {
            Debug.Log(" IAP success");
            AddRewards();
        }
        
        IAPManager.Instance.Buy("id", onSuccess);
    }

    private void AddRewards()
    {
        for (int i = 0; i < rewardDatas.Length; i++)
        {
            rewardDatas[i].RecieveReward();
        }
    }
}