using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EquipmentEvents", menuName = "ScriptableObjects/Equipment/EquipmentEvents")]
public class SEquipmentEvents : ScriptableObject
{
    public event Action<SEquipmentData> EquipItemRequest;
    public event Action<SEquipmentData> UnequipItemRequest;
    
    public event Action<SEquipmentData> ItemEquipped;
    public event Action<SEquipmentData> UnequippedItem;
    
    public void OnEquipItemRequest(SEquipmentData equipmentData)
    {
        EquipItemRequest?.Invoke(equipmentData);
    }

    public void OnUnequipItemRequest(SEquipmentData equipmentData)
    {
        UnequipItemRequest?.Invoke(equipmentData);
    }
    
    public void OnEquipItem(SEquipmentData equipmentData)
    {
        ItemEquipped?.Invoke(equipmentData);
    }

    public void OnUnequipItem(SEquipmentData equipmentData)
    {
        UnequippedItem?.Invoke(equipmentData);
    }
}
