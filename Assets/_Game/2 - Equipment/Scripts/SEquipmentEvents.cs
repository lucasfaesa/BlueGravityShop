using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EquipmentEvents", menuName = "ScriptableObjects/Equipment/EquipmentEvents")]
public class SEquipmentEvents : ScriptableObject
{
    //TODO changes this name?
    public event Action<SEquipmentData> ItemEquipped;
    public event Action<SEquipmentData> ItemUnequipped;
    
    public void OnItemEquipped(SEquipmentData equipmentData)
    {
        ItemEquipped?.Invoke(equipmentData);
    }

    public void OnItemUnequipped(SEquipmentData equipmentData)
    {
        ItemUnequipped?.Invoke(equipmentData);
    }
}
