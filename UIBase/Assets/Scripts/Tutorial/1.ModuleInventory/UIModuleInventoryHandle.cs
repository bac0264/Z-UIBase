using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIModuleInventoryHandle : MonoBehaviour
{
    [SerializeField] private UIModuleEquipmentView equipmentView;
    [SerializeField] private UIModuleInventoryView inventoryView;
    
    void Awake()
    {
        equipmentView.OnRightClickEvent = Unequip;
        inventoryView.OnRightClickEvent = Equip;
    }

    private void OnValidate()
    {
        if (equipmentView == null) equipmentView = transform.GetComponentInChildren<UIModuleEquipmentView>();

        if (inventoryView == null) inventoryView = transform.GetComponentInChildren<UIModuleInventoryView>();
    }

    private void Unequip(ItemResource item)
    {
        if (equipmentView.RemoveToUnequip(item, ()=>inventoryView.ReloadData()))
        {
            DataPlayer.PlayerInventory.Save();
        }
    }

    private void Equip(ItemResource item)
    {
        var inventoryId = item.inventoryId;
        if (equipmentView.AddToEquip(item, () => inventoryView.ReloadDataWithInventoryId(inventoryId)))
        {
            DataPlayer.PlayerInventory.Save();
        }
    }

    public void RefreshUI()
    {
        var index = DataPlayer.PlayerCharacter.GetCurrentCharacter().characterId + 1;
        if (index > 5) index = 0;
        DataPlayer.PlayerCharacter.SetCurrentCharacter(index);
        equipmentView.RefreshUI();
        inventoryView.ReloadData();
    }
}
