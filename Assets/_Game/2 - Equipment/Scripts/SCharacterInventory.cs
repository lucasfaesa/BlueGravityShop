using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterInventory", menuName = "ScriptableObjects/Characters/CharacterInventory")]
public class SCharacterInventory : ScriptableObject
{
    
    [SerializeField] private List<SEquipmentData> equipmentsList;

    public List<SEquipmentData> GetEquipmentsList()
    {
        return equipmentsList;
    }

    public void AddEquipment(SEquipmentData equipmentDataSO)
    {
        equipmentsList.Add(equipmentDataSO);
    }

    public void RemoveEquipment(SEquipmentData equipmentDataSO)
    {
        equipmentsList.Remove(equipmentDataSO);
    }
    
}
