using System;

[System.Serializable]
public class Reward : IRecieveReward
{

    public int resType;

    public int resId;

    public long resNumber;

    public Reward()
    {
    }
    
    public Reward(int resType, int resId, long resNumer)
    {
        this.resType = resType;
        this.resId = resId;
        this.resNumber = resNumer;
    }

    public static Reward CreateInstanceReward(int type, int id, long number)
    {
        return new Reward(type, id, number);
    }

    public Resource GetResource()
    {
        return Resource.CreateInstance(resType, resId, resNumber);
    }

    public void RecieveReward()
    {
        if (resType == (int) ResourceType.ItemType)
        {
            DataPlayer.PlayerInventory.AddNewItem(ItemResource.CreateInstance(resType, resId, resNumber, 0, 0));
        }
        else if (resType == (int) ResourceType.MoneyType)
        {
            DataPlayer.PlayerMoney.AddOne((MoneyType) resId, resNumber);
        }
        else if (resType == (int) ResourceType.CharacterType)
        {
            DataPlayer.PlayerCharacter.AddCharacter(CharacterResource.CreateInstance(resType, resId, resNumber));
        }
        else
        {

        }
    }
}