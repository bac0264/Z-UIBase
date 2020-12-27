using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class PlayerQuest
{
    private PlayerQuestData playerQuestData;

    public PlayerQuest()
    {
        Load();
    }

    
    private void Load()
    {
        playerQuestData = JsonConvert.DeserializeObject<PlayerQuestData>(PlayerPrefs.GetString(KeyUtils.QUEST_DATA));
    }
    
    public void Save()
    {
        PlayerPrefs.SetString(KeyUtils.QUEST_DATA, JsonConvert.SerializeObject(playerQuestData));
    }
}

[System.Serializable]
public class PlayerQuestData
{
    private Dictionary<int, QuestProgress> questDict = new Dictionary<int, QuestProgress>();
}
[System.Serializable]
public class QuestProgress
{
    [JsonProperty("0")] public long progress;
    [JsonProperty("1")] public int state;
    
    public QuestState GetState()
    {
        return (QuestState) state;
    }

    public void Claim()
    {
        state = (int) QuestState.Claimed;
    }
}

public enum QuestState
{
    Doing,
    Done,
    Claimed
}