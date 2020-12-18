using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DailyRewardCollection : ScriptableObject
{
    public DailyRewardData[] dataGroups;
}


[System.Serializable]
public class DailyRewardData
{
    public int id;
    public Reward[] rewards;

    private PlayerDailyReward playerDailyReward = null;
    
    public bool IsReceived()
    {
        if (playerDailyReward == null) playerDailyReward = DataPlayer.PlayerDailyReward;
        return playerDailyReward.IsReceived(id);
    }
    
    public bool IsReceivable()
    {
        if (playerDailyReward == null) playerDailyReward = DataPlayer.PlayerDailyReward;
        return (id - 1) <= playerDailyReward.GetCurrentDay();
    }
}

