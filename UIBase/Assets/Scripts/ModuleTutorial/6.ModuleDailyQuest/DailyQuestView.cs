using System;
using System.Collections;
using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;
using UnityEngine;
using UnityEngine.UI;
using Zitga.Localization;
using Zitga.Localization.Tutorials;

public class DailyQuestView : EnhancedScrollerCellView
{
    [SerializeField] private Text id;
    [SerializeField] private Text type;

    [SerializeField] private Button claimBtn;

    [SerializeField] private Image progress;
    [SerializeField] private Text progressTxt;

    [SerializeField] private Transform rewardAnchor;

    private DailyQuestData questData;
    private QuestProgress questProgress;

    private List<IconView> iconViews = new List<IconView>();
    private IconView prefab = null;

    private void Awake()
    {
        prefab = LoadResourceController.GetIconView();
        claimBtn.onClick.AddListener(OnCickClaim);
    }

    private void OnCickClaim()
    {
        Reward.RecieveManyRewards(questData.rewards);
    }

    public void SetData(DailyQuestData questData)
    {
        this.questData = questData;
        this.questProgress = questData.GetProgress();

        progressTxt.text = questProgress.progress + "/" + questData.required;
        id.text = "id: " + questData.id;
        type.text = ((QuestType) questData.type).ToString();

        var fill = (float) questProgress.progress / questData.required;
        progress.fillAmount = fill;

        InitOrUpdateView();
    }

    private void InitOrUpdateView()
    {
        int i = 0;
        for (; i < questData.rewards.Length; i++)
        {
            if (i < iconViews.Count)
            {
                iconViews[i].gameObject.SetActive(true);
                iconViews[i].SetData(questData.rewards[i].GetResource());
            }
            else
            {
                var view = Instantiate(prefab, rewardAnchor);
                view.SetData(questData.rewards[i].GetResource());
                iconViews.Add(view);
            }
        }

        for (; i < iconViews.Count; i++)
        {
            iconViews[i].gameObject.SetActive(false);
        }
    }

    private void UpdateProgress()
    {
    }
}