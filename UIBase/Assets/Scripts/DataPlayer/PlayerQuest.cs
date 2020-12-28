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


    public QuestProgress GetProgressWithId(int id)
    {
        return playerQuestData.GetQuestProgress(id);
    }
    
    
    private void Load()
    {
        playerQuestData = JsonConvert.DeserializeObject<PlayerQuestData>(PlayerPrefs.GetString(KeyUtils.QUEST_DATA));

        if (playerQuestData == null)
        {
            playerQuestData = new PlayerQuestData();
        }
    }
    
    public void Save()
    {
        PlayerPrefs.SetString(KeyUtils.QUEST_DATA, JsonConvert.SerializeObject(playerQuestData));
    }
}

[System.Serializable]
public class PlayerQuestData
{
    private Dictionary<int, QuestProgress> dailyQuest = new Dictionary<int, QuestProgress>();

    public QuestProgress GetQuestProgress(int id)
    {
        if (dailyQuest.ContainsKey(id))
        {
            return dailyQuest[id];
        }
        var questProgess = new QuestProgress()
        {
            progress =  0,
            state = 0
        };
        dailyQuest.Add(id, questProgess);
        return questProgess;
    }

    public void AddQuest(int type, int value)
    {
        
    }
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
    Doing = 0,
    Done = 1,
    Claimed = 2,
}