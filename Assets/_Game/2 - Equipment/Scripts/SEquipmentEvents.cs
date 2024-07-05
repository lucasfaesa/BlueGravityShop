using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EquipmentEvents", menuName = "ScriptableObjects/Equipment/EquipmentEvents")]
public class SEquipmentEvents : ScriptableObject
{
    //TODO changes this name?
    public event Action<SEquipmentData> CurrentEquipmentChanged;
    public event Action<SEquipmentData> CurrentEquipmentRemoved;
    
    public void OnCurrentEquipmentChanged(SEquipmentData equipmentData)
    {
        CurrentEquipmentChanged?.Invoke(equipmentData);
    }

    public void OnEquipmentRemove(SEquipmentData equipmentData)
    {
        CurrentEquipmentRemoved?.Invoke(equipmentData);
    }
}
