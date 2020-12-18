using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[System.Serializable]
public class CharacterResource : Resource
{
    public int characterId;

    public long exp;


    [NonSerialized] public Dictionary<int, ItemStat> characterStats;

    [NonSerialized] private CharacterStatCollection characterStatCollection = null;
    [NonSerialized] private CharacterLevelCollection characterLevelCollection = null;

    public CharacterResource(int type, int id, long number, int characterId, int exp) : base(type, id, number)
    {
        this.characterId = characterId;
        this.exp = exp;

        if (characterStatCollection == null)
            characterStatCollection = LoadResourceController.GetCharacterStat();

        if (characterLevelCollection == null)
            characterLevelCollection = LoadResourceController.GetCharacterLevelCollection();

        ReloadCharacterStat();
    }

    public void AddExp(long value)
    {
        exp += value;
    }

    public void ReloadCharacterStat()
    {
        this.characterStats = characterStatCollection.GetItemStatDataWithItemId(id).GetItemStats(characterLevelCollection.GetCurrentLevel(exp));

        var itemList = DataPlayer.PlayerInventory.GetEquipmentItemWithIdCharacter(characterId);

        for (int j = 0; j < itemList.Count; j++)
        {
            for (int k = 0; k < itemList[j].itemStats.Length; k++)
            {
                var itemStat = itemList[j].itemStats[k];
                var statModifier = new StatModifier(itemStat.baseStat.Value, StatModType.Flat);

                characterStats[itemStat.statType].baseStat.AddModifier(statModifier);
            }
        }
    }

    public static CharacterResource CreateInstance(int type, int id, long number, int characterId = -1, int exp = 0)
    {
        return new CharacterResource(type, id, number, characterId, exp);
    }
}