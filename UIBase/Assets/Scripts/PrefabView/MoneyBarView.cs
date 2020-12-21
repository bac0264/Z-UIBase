using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyBarView : IconView
{
    public MoneyType moneyType;

    public override void SetData(Resource resource)
    {
        icon.sprite = LoadResourceController.GetMoneyIcon(resource.id);
        icon.SetNativeSize();
        value.text = resource.number.ToString();
    }

    public void OnEnable()
    {
        if (moneyType != MoneyType.NONE)
        {
            var data = DataPlayer.PlayerMoney.GetMoney(moneyType);
            SetData(data);
        }
    }
}
