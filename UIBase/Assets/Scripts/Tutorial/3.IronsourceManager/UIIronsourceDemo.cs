using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIIronsourceDemo : MonoBehaviour
{
    [SerializeField] private Text rewardTxt = null;
    
    [SerializeField] private Button rewardBtn = null;
    [SerializeField] private Button interBtn = null;

    private void Awake()
    {
        InitButtons();
    }

    private void InitButtons()
    {
        rewardBtn.onClick.AddListener(OnClickRewardVideo);
        interBtn.onClick.AddListener(OnClickInterVideo);
    }
    
    private void OnClickRewardVideo()
    {    
        rewardTxt.text = "reward btn";
        void onSuccess()
        {
            rewardTxt.text = "reward onsuccess";
            Debug.Log("rewardTxt: " + rewardTxt.text);
        }
        
        IronSourceManager.instance.ShowRewardedVideo(onSuccess);
    }
    
    private void OnClickInterVideo()
    {
        IronSourceManager.instance.ShowInterstitial();
    }
}
