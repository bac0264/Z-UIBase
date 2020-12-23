﻿using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class PlayerDailyReward
{
    private DailyRewardSaveLoad dailyReward;

    [NonSerialized] private int DAY_MAX;

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
        dailyReward =
            JsonConvert.DeserializeObject<DailyRewardSaveLoad>(PlayerPrefs.GetString(KeyUtils.DAILY_REWARD_DATA));
        if (dailyReward == null)
        {
            dailyReward = new DailyRewardSaveLoad();
        }

        DAY_MAX = LoadResourceController.GetDailyReward().dataGroups.Length;
    }

    public int GetCurrentDay()
    {
        return dailyReward.currentDay;
    }

    public void SetNextDay()
    {
        if (dailyReward.currentDay >= DAY_MAX)
        {
            Reset();
            return;
        }

        dailyReward.currentDay++;
    }

    public void AddDayReceived(int day)
    {
        dailyReward.AddDay(day);
    }

    public void Reset()
    {
        if (dailyReward.dayReceivedDic.Count >= DAY_MAX)
            dailyReward.Reset();
    }

    public bool IsReceived(int day)
    {
        return dailyReward.IsReceived(day);
    }

    public bool IsNextDay(int day)
    {
        return dailyReward.currentDay == day;
    }

    public long GetLastTimeOnline()
    {
        return dailyReward.GetLastTimeOnline();
    }

    public void SetLastTimeOnline(long lastTimeOnline)
    {
        // Set new current day
        if (dailyReward.lastTimeOnline > 1000)
        {
            var dayBonus = TimeUtils.GetDayCount(dailyReward.lastTimeOnline, lastTimeOnline);
            
            if (dayBonus >= 0 && dailyReward.currentDay < DAY_MAX)
            {
                dailyReward.currentDay += (int) dayBonus;
                if (dailyReward.currentDay > DAY_MAX) dailyReward.currentDay = DAY_MAX;
            }
            else if (dailyReward.currentDay >= DAY_MAX)
            {
                Reset();
            }
        }

        // Set current time
        dailyReward.SetLastTimeOnline(lastTimeOnline);
        Save();
    }

    public void Add1Day()
    {
        dailyReward.lastTimeOnline -= TimeUtils.GetTimeADay();
        Save();
    }
}

[System.Serializable]
public class DailyRewardSaveLoad
{
    [JsonProperty("0")] public int currentDay;
    [JsonProperty("1")] public long lastTimeOnline;
    [JsonProperty("2")] public Dictionary<int, int> dayReceivedDic = new Dictionary<int, int>();

    public DailyRewardSaveLoad()
    {
        currentDay = 1;
    }

    public void AddDay(int day)
    {
        if (dayReceivedDic.ContainsKey(day)) return;
        dayReceivedDic.Add(day, 1);
    }

    public void Reset()
    {
        currentDay = 1;
        dayReceivedDic.Clear();
    }

    public bool IsReceived(int day)
    {
        return dayReceivedDic.ContainsKey(day);
    }

    public void SetLastTimeOnline(long lastTimeOnline)
    {
        this.lastTimeOnline = lastTimeOnline;
    }

    public long GetLastTimeOnline()
    {
        return lastTimeOnline;
    }
}