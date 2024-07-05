using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EquipmentEvents", menuName = "ScriptableObjects/Equipment/EquipmentEvents")]
public class SEquipmentEvents : ScriptableObject
{
    public event Action<SEquipmentData> CurrentEquipmentChanged;
    public event Action<Helpers.EquipmentType> CurrentEquipmentRemoved;
    
    public void OnCurrentEquipmentChanged(SEquipmentData equipmentData)
    {
        CurrentEquipmentChanged?.Invoke(equipmentData);
    }

    public void OnEquipmentRemove(Helpers.EquipmentType type)
    {
        CurrentEquipmentRemoved?.Invoke(type);
    }
}
