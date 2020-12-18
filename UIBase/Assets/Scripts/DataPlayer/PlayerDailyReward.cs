using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class PlayerDailyReward
{
    private DailyRewardSaveLoad dailyReward;

    public PlayerDailyReward()
    {
        Load();
    }

    private void Save()
    {
        PlayerPrefs.SetString(KeyUtils.DAILY_REWARD_DATA, JsonConvert.SerializeObject(dailyReward));
    }

    private void Load()
    {
        dailyReward = JsonConvert.DeserializeObject<DailyRewardSaveLoad>(PlayerPrefs.GetString(KeyUtils.DAILY_REWARD_DATA));
        if (dailyReward == null)
        {
            dailyReward = new DailyRewardSaveLoad();
        }
    }

    public int GetCurrentDay()
    {
        return dailyReward.currentDay;
    }

    public void SetCurrentDay(int currentDay)
    {
        dailyReward.currentDay = currentDay;
    }

    public void AddDayReceived(int day)
    {
        dailyReward.AddDay(day);
    }
    
    public void Reset()
    {
        dailyReward.Reset();
    }
    
    public bool IsReceived(int day)
    {
        return dailyReward.IsReceived(day);
    }
}

[System.Serializable]
public class DailyRewardSaveLoad
{
    public int currentDay;
    public List<int> dayReceivedList = new List<int>();

    public DailyRewardSaveLoad()
    {
        currentDay = 0;
    }
    public void AddDay(int day)
    {
        dayReceivedList.Add(day);
    }
    
    public void Reset()
    {
        dayReceivedList.Clear();
    }
    
    public bool IsReceived(int day)
    {
        for (int i = 0; i < dayReceivedList.Count; i++)
        {
            if (day == dayReceivedList[i]) return true;
        }

        return false;
    }
}
