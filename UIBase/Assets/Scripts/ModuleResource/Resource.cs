using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ResourceType
{
    MoneyType = 0,
    ItemType = 1,
    CharacterType = 2,
}

public enum ItemType
{
    Weapon = 0,
    Armor = 1,
    Ring = 2,
    Armulet = 3,
}

public enum MoneyType
{
    GEM = 0,
    GOLD = 1,
}

[System.Serializable]
public class Resource
{
    /// <summary>
    /// A type in ResourceType.
    /// </summary>
    public int type;

    /// <summary>
    /// Unique identify of a type.
    /// </summary>
    public int id;

    /// <summary>
    /// Number of resource.
    /// </summary>
    public long number;

    // Constructor
    public Resource()
    {

    }

    /// <summary>
    /// Resource constructor with params
    /// </summary>
    /// <param name="type"> A type in ResourceType. </param>
    /// <param name="id"> Unique identify of a type. </param>
    /// <param name="number"> Number of resource.. </param>
    /// <returns></returns>
    public Resource(int type, int id, long number)
    {
        this.type = type;

        this.id = id;

        this.number = number;
    }

    public static Resource CreateInstance(int type, int id, long number)
    {
        return new Resource(type, id, number);
    }

    public virtual bool Add(long value)
    {
        if (value < 0) return false;
        number += value;
        return true;
    }
    public virtual bool Sub(long value)
    {
        if (value < 0) return false;
        if (number - value < 0) return false;
        number -= value;
        return true;
    }
    public virtual void Set(long value)
    {
        if (value < 0) value = 0;
        number = value;
    }

}

[System.Serializable]
public class Reward : Resource, IRecieveReward
{
    /// <summary>
    /// Resource constructor with params
    /// </summary>
    /// <param name="type"> A type in ResourceType. </param>
    /// <param name="id"> Unique identify of a type. </param>
    /// <param name="number"> Number of resource.. </param>
    /// <returns></returns>
    public Reward(int type, int id, long number) : base(type, id, number)
    {
    }
    public static Reward CreateInstanceReward(int type,   int id, long number)
    {
        return new Reward(type, id, number);
    }

    public void RecieveReward()
    {
        if (type == (int)ResourceType.ItemType)
        {
            DataPlayer.PlayerInventory.AddNewItem(ItemResource.CreateInstance(type, id, number, 0 , 0));
        }
        else if(type == (int)ResourceType.MoneyType)
        {
            DataPlayer.PlayerMoney.AddOne((MoneyType) id, number);
        }
        else if( type == (int) ResourceType.CharacterType )
        {
            
        }
        else
        {
            
        }
    }
}
