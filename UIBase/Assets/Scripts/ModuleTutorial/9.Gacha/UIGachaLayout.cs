using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGachaLayout : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private Image iconGacha;
    [SerializeField] private Button gacha1Btn;
    [SerializeField] private Button gacha10Btn;

    [SerializeField] private GachaData gachaData;
    private void Awake()
    {
        gacha1Btn.onClick.AddListener(OnClickGacha1);
        gacha10Btn.onClick.AddListener(OnClickGacha10);
    }

    public void SetupView(GachaData gachaData)
    {
        this.gachaData = gachaData;
        background.sprite = LoadResourceController.GetGachaBackground(gachaData.id);
        iconGacha.sprite = LoadResourceController.GetGachaIcon(gachaData.id);
    }

    public void OnClickGacha1()
    {
        if (gachaData != null)
        {
            var rewards = gachaData.GetGacha();
            for (int i = 0; i < rewards.Length; i++)
            {
                rewards[i].RecieveReward();
            }
            WindowManager.Instance.ShowWindowWithData<Reward[]>(WindowType.UI_SHOW_REWARD, rewards);
        }
    }
    
    public void OnClickGacha10()
    {
        if (gachaData != null)
        {
            var rewards = gachaData.GetGacha10();
            for (int i = 0; i < rewards.Length; i++)
            {
                rewards[i].RecieveReward();
            }
            WindowManager.Instance.ShowWindowWithData<Reward[]>(WindowType.UI_SHOW_REWARD, rewards);
        }
    }
}
