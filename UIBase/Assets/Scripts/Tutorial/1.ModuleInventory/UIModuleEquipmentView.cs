using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIModuleEquipmentView : MonoBehaviour
{
    [SerializeField] private UIModuleEquipmentItemView[] equipments = null;

    public Action<ItemResource> OnRightClickEvent;

    private CharacterResource currentCharacter;

    private void OnValidate()
    {
        if (equipments == null || equipments.Length == 0)
        {
            equipments = transform.GetComponentsInChildren<UIModuleEquipmentItemView>();
        }
    }

    public virtual void Start()
    {
        SetupEvent();
        RefreshUI();
    }

    public void SetupEvent()
    {
        for (int i = 0; i < equipments.Length; i++)
        {
            equipments[i].SetData(null, OnRightClickEvent);
        }
    }

    public void RefreshUI()
    {
        currentCharacter = DataPlayer.PlayerCharacter.GetCurrentCharacter();
        var itemList = DataPlayer.PlayerInventory.GetEquipmentItemWithIdCharacter(currentCharacter.characterId);
        
        for (int i = 0; i < equipments.Length; i++)
        {
            equipments[i].SetupItem(null);
            // Set data
            if (itemList != null)
            {
                for (int j = 0; j < itemList.Count; j++)
                {
                    if ((int) equipments[i].id == PlayerMoney.GetRealItemId(itemList[j].id))
                    {
                        equipments[i].SetupItem(itemList[j]);
                        break;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Equip item in inventory.
    /// </summary>
    /// <param name="item"> Item is picked in Inventory.</param>
    /// <returns></returns>
    public bool AddToEquip(ItemResource item, Action reloadData = null)
    {
        var equipSlot = GetSlot(item);
        if (equipSlot == null) return false;

        if (equipSlot.itemResource != null)
        {
            DataPlayer.PlayerInventory.RemoveEquipmentItem(currentCharacter.characterId, equipSlot.itemResource);
            DataPlayer.PlayerInventory.AddItem(equipSlot.itemResource);
        }

        equipSlot.SetupItem(item);

        DataPlayer.PlayerInventory.AddEquipmentItem(currentCharacter.characterId, equipSlot.itemResource);
        DataPlayer.PlayerInventory.RemoveItem(equipSlot.itemResource);

        // reload data in Inventory
        reloadData?.Invoke();

        return true;
    }

    /// <summary>
    /// Unequip item in Equipment Panel.
    /// </summary>
    /// <param name="item"> Item is picked in Equipment Panel.</param>
    /// <returns></returns>
    public bool RemoveToUnequip(ItemResource item, Action reloadData = null)
    {
        var equipSlot = GetSlot(item);
        if (equipSlot == null || equipSlot.itemResource == null) return false;

        equipSlot.SetupItem(null);

        DataPlayer.PlayerInventory.AddItem(item);
        DataPlayer.PlayerInventory.RemoveEquipmentItem(currentCharacter.characterId, item);
    
        // reload data in Inventory
        reloadData?.Invoke();
        return true;
    }

    /// <summary>
    /// Get equipment slot with item type.
    /// </summary>
    /// <param name="item"> ItemResource pick in inventory or equipment.</param>
    /// <returns></returns>
    private UIModuleInventoryItemView GetSlot(ItemResource item)
    {
        foreach (UIModuleEquipmentItemView itemSlot in equipments)
        {
            if (PlayerMoney.GetRealItemId(item.id) == (int) itemSlot.id)
            {
                return itemSlot;
            }
        }

        return null;
    }
}