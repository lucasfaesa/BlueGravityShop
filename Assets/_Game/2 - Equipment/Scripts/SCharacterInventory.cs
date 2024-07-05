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
    
}
