using System;

[System.Serializable]
public class Reward : IRecieveReward
{

    public int res_type;

    public int res_id;

    public long res_number;

    public Reward()
    {
    }
    
    public Reward(int res_type, int res_id, long res_number)
    {
        this.res_type = res_type;
        this.res_id = res_id;
        this.res_number = res_number;
    }

    public static Reward CreateInstanceReward(int type, int id, long number)
    {
        return new Reward(type, id, number);
    }

    public Resource GetResource()
    {
        return Resource.CreateInstance(res_type, res_id, res_number);
    }

    public void RecieveReward()
    {
        if (res_type == (int) ResourceType.ItemType)
        {
            DataPlayer.PlayerInventory.AddNewItem(ItemResource.CreateInstance(res_type, res_id, res_number, 0, 0));
        }
        else if (res_type == (int) ResourceType.MoneyType)
        {
            DataPlayer.PlayerMoney.AddOne((MoneyType) res_id, res_number);
        }
        else if (res_type == (int) ResourceType.CharacterType)
        {

        }
        else
        {

        }
    }
}