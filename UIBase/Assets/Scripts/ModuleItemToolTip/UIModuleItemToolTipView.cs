using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIModuleItemToolTipView : MonoBehaviour
{
    [SerializeField] private UIModuleInventoryItemView itemView = null;

    [SerializeField] private Text localizeDescribeItem = null;
    [SerializeField] private Text levelTxt = null;
    [SerializeField] private Text nextLevelTxt = null;
    [SerializeField] private Text statsTxt = null;
    [SerializeField] private Text nextStatsTxt = null;

    [SerializeField] private Button upgradeBtn = null;
    [SerializeField] private Button sellItemBtn = null;
    [SerializeField] private Button nextItemBtn = null;

    [SerializeField] private MoneyBarView sellMoneyBar = null;
    [SerializeField] private MoneyBarView upgradeMoneyBar = null;
    
    [SerializeField] private GameObject nextStatsContainer;

    [SerializeField] private ItemStatCollection itemStatCollection;
    [SerializeField] private UpgradeItemCollection upgradeCollection;
    [SerializeField] private SellItemCollection sellCollection;
    
    [SerializeField] private ItemResource itemPick;

    private int index = 0;
    private void Awake()
    {
        itemStatCollection = LoadResourceController.GetItemStat();
        upgradeCollection = LoadResourceController.GetUpgradeItemCollection();
        sellCollection = LoadResourceController.GetSellItemCollection();
        
        InitButtons();
        InitLocalize();

        NextItem();
    }

    private void InitButtons()
    {
        upgradeBtn.onClick.AddListener(OnClickUpgrade);
        sellItemBtn.onClick.AddListener(OnClickSellItem);
        nextItemBtn.onClick.AddListener(NextItem);
    }

    private void InitLocalize()
    {
    }

    public void UpdateView(ItemResource itemPick)
    {
        if (itemPick == null) return;

        this.itemPick = itemPick;

        var isMaxLevel = itemPick.level == upgradeCollection.dataGroups.maxLevel;
        var stats = "";
        var nextStats = "";

        SetupStats(ref stats, ref nextStats, isMaxLevel);

        SetupView(stats, nextStats, isMaxLevel);
    }

    private void SetupStats(ref string stats, ref string nextStats, bool isMaxLevel)
    {
        itemPick.itemStats = itemStatCollection.GetItemStatDataWithItemId(itemPick.id).GetItemStats(itemPick.level);
        
        for (int i = 0; i < itemPick.itemStats.Length; i++)
        {
            stats += itemPick.itemStats[i].GetLocalize() + "\n";

            if (!isMaxLevel)
            {
                var nextLevel = itemPick.itemStats[i].GetStatNextLevel(itemPick.level);
                nextStats += nextLevel.GetLocalize() + "\n";
            }
        }
    }

    private void SetupView(string stats, string nextStats, bool isMaxLevel)
    {
        itemView.SetupItem(itemPick);
        
        statsTxt.text = stats;
        nextStatsTxt.text = nextStats;
        levelTxt.text = "level: " + (itemView.itemResource.level + 1);
        localizeDescribeItem.text = "localize item: " + itemPick.id;
        nextLevelTxt.text = "next level: " + (itemView.itemResource.level + 2);

        upgradeMoneyBar.SetData(upgradeCollection.dataGroups.GetPrice(itemPick.level));
        sellMoneyBar.SetData(sellCollection.GetSellData(itemPick.GetPriority()).GetPrice());

        nextStatsContainer.SetActive(!isMaxLevel);
    }

    private void OnClickUpgrade()
    {
        // 
        var goldUpgrade = upgradeCollection.dataGroups.GetPrice(itemPick.level).number;

        if (DataPlayer.PlayerMoney.IsEnoughMoney(Resource.CreateInstance((int) ResourceType.MoneyType,
            (int) MoneyType.GOLD,
            goldUpgrade)) && itemPick != null && itemPick.level < upgradeCollection.dataGroups.maxLevel)
        {
            itemPick.level += 1;

            DataPlayer.PlayerMoney.SubOne(MoneyType.GOLD, goldUpgrade);
            DataPlayer.PlayerInventory.Save();

            UpdateView(itemPick);
        }
    }

    private void OnClickSellItem()
    {
        var goldSell  = sellCollection.GetSellData(itemPick.GetPriority()).GetPrice().number;

        if (itemPick != null)
        {
            DataPlayer.PlayerInventory.RemoveItem(itemPick);
            DataPlayer.PlayerInventory.Save();
            DataPlayer.PlayerMoney.AddOne(MoneyType.GOLD, goldSell);

            NextItem();
        }
    }

    private void NextItem()
    {
        UpdateView(DataPlayer.PlayerInventory.GetItem(index));
        index++;
    }
}