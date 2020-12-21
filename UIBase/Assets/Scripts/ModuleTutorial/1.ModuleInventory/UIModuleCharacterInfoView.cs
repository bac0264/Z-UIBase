using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIModuleCharacterInfoView : MonoBehaviour
{
    [SerializeField] private List<StatView> statViews = new List<StatView>();
    [SerializeField] private StatView prefab = null;
    
    [SerializeField] private Image iconCharacter = null;
    [SerializeField] private Text level = null;
    
    [SerializeField] private Transform statAnchor;

    private CharacterResource characterResource = null;
    private void Awake()
    {
        InitData();
        UpdateCharacterView();
    }

    private void InitData()
    {
        prefab = LoadResourceController.GetStatView();
    }
    
    public void UpdateCharacterView()
    {
        characterResource = DataPlayer.PlayerCharacter.GetCurrentCharacter();
        iconCharacter.sprite = LoadResourceController.GetCharacterItem(characterResource.characterId);
        level.text = "Lv. "+ (LoadResourceController.GetCharacterLevelCollection().GetCurrentLevel(characterResource.exp) + 1);
        characterResource.ReloadCharacterStat();

        int i = 0;
        for (;i < characterResource.characterStats.Count && i < statViews.Count; i++)
        {
            statViews[i].SetData(characterResource.characterStats[i]);
        }

        for (; i < characterResource.characterStats.Count; i++)
        {
            var view = Instantiate(prefab, statAnchor);
            view.SetData(characterResource.characterStats[i]);
            statViews.Add(view);
        }

    }
}
