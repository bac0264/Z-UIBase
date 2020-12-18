using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class CharacterLevelCollection : ScriptableObject
{
    public CharacterLevelData[] dataGroups;

    public int GetCurrentLevel(long exp)
    {
        var level = 0;
        for (int i = 0; i < dataGroups.Length; i++)
        {
            if (exp < dataGroups[i].exp)
            {
                return level;
            }

            level = i;
        }

        return level;
    }
}

[System.Serializable]
public class CharacterLevelData
{
    public int level;
    public long exp;
}
