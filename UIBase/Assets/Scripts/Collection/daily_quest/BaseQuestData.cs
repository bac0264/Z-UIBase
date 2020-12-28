using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseQuestData
{
    [SerializeField] public int id;
    [SerializeField] public int type;
    [SerializeField] public int idTarget;
    [SerializeField] public long required;
    [SerializeField] public Reward[] rewards;
    [SerializeField] public string iconName;
    [SerializeField] private string description;
    

    public string Description
    {
        get { return GetDescription(); }
    }

    public bool CanGoto()
    {
        return false; //QuestAchievementHelper.CanGoToQuest((QuestType) type);
    }

    public string GetDescription()
    {
        return ""; // QuestAchievementHelper.GetDescription(type, description, required, idTarget);
    }
}