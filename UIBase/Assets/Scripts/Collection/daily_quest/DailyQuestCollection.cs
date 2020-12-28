using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DailyQuestCollection : ScriptableObject
{
    public DailyQuestData[] dataGroups;
}

[System.Serializable]
public class DailyQuestData : BaseQuestData
{
    
}
