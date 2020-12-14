using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIModuleItemToolTipView : MonoBehaviour
{
    private const int LEVEL_MAX = 19;
    [SerializeField] private UIModuleInventoryItemView itemView = null;
    
    [SerializeField] private Text localizeDescribeItem = null;
    [SerializeField] private Text levelTxt = null;
    [SerializeField] private Text nextLevelTxt = null;
    [SerializeField] private Text statsTxt = null;
    [SerializeField] private Text nextStatsTxt = null;
    [SerializeField] private Text goldSellTxt = null;
    
    [SerializeField] private Button upgradeBtn = null;
    [SerializeField] private Button sellItemBtn = null;
    [SerializeField] private Button nextItemBtn = null;
    
    [SerializeField] private GameObject nextStatsContainer;
    
    [SerializeField] private ItemResource itemPick;
    
    private int index = 0;

    private int goldSell = 0;
    private int goldUpgrade = 0;
    private void Awake()
    {
        goldSell = 100;
        goldUpgrade = 0;
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
        
        var isMaxLevel = itemPick.level == LEVEL_MAX;
        var stats = "";
        var nextStats = "";
        
        SetupStats(ref stats,ref nextStats, isMaxLevel);

        SetupView(stats, nextStats, isMaxLevel);
    }

    private void SetupStats(ref string stats, ref string nextStats, bool isMaxLevel)
    {
        Debug.Log(isMaxLevel);
        // Add Item Stat
        if (itemPick.itemStats == null || itemPick.itemStats.Length == 0)
        {
            List<ItemStat> itemStatList = new List<ItemStat>();
            for (int i = 0; i < 4; i++)
            {
                ItemStat itemStat = new ItemStat(itemPick.level + 1, i);
                stats += itemStat.GetLocalize() + "\n";
                itemStatList.Add(itemStat);

                if (!isMaxLevel)
                {
                    var nextLevel = itemStat.GetCopy();
                    nextLevel.baseStat.BaseValue = itemPick.level + 2;
                    nextStats += nextLevel.GetLocalize() + "\n";
                }
            }

            itemPick.itemStats = itemStatList.ToArray();
        }
        else
        {
            for (int i = 0; i < itemPick.itemStats.Length; i++)
            {

                itemPick.itemStats[i].baseStat.BaseValue = itemPick.level + 1 ;
                stats += itemPick.itemStats[i].GetLocalize() + "\n";

                if (!isMaxLevel)
                {
                    var nextLevel = itemPick.itemStats[i].GetCopy();
                    nextLevel.baseStat.BaseValue = itemPick.level + 2;
                    nextStats += nextLevel.GetLocalize() + "\n";
                }
            }
        }
    }

    private void SetupView(string stats,string nextStats, bool isMaxLevel)
    {
        itemView.SetupItem(itemPick);
        levelTxt.text = "level: " + (itemView.itemResource.level + 1);
        nextLevelTxt.text = "next level: " + (itemView.itemResource.level + 2);
        statsTxt.text = stats;
        nextStatsTxt.text = nextStats;
        goldSellTxt.text = (this.goldSell * (itemPick.level + 1)).ToString();
        localizeDescribeItem.text = "localize item: "+itemPick.id;
        
        nextStatsContainer.SetActive(!isMaxLevel);
    }
    
    private void OnClickUpgrade()
    {
        // 
        var goldUpgrade = this.goldUpgrade;
        
        if (DataPlayer.PlayerMoney.IsEnoughMoney(Resource.CreateInstance((int)ResourceType.MoneyType, (int)MoneyType.GOLD,
            goldUpgrade)) && itemPick != null && itemPick.level < LEVEL_MAX )
        {
            itemPick.level += 1;

            DataPlayer.PlayerMoney.SubOne(MoneyType.GOLD, goldUpgrade);
            DataPlayer.PlayerInventory.Save();
            
            UpdateView(itemPick);
        }
    }
    
    private void OnClickSellItem()
    {
        var goldUpgrade = this.goldUpgrade * (itemPick.level + 1);

        if (itemPick != null )
        {
            DataPlayer.PlayerInventory.RemoveItem(itemPick);
            DataPlayer.PlayerInventory.Save();
            DataPlayer.PlayerMoney.AddOne(MoneyType.GOLD, goldUpgrade);
            
            NextItem();
        }
    }
    
    private void NextItem()
    {
        UpdateView(DataPlayer.PlayerInventory.GetItem(index));
        index++;
        Debug.Log("index: " +index);
    }
}
