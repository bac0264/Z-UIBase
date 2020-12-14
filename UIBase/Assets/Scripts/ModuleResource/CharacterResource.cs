using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterResource : Resource
{
    public int characterId;

    public long exp;
    public CharacterResource(int type, int id, long number, int characterId, int exp) : base(type, id, number)
    {
        this.characterId = characterId;
        this.exp = exp;
    }

    public void AddExp(long value)
    {
        exp += value;
    }
    public static CharacterResource CreateInstance(int type, int id, long number, int characterId = -1, int exp = 0)
    {
        return new CharacterResource(type,id,number,characterId, exp);
    }
}
