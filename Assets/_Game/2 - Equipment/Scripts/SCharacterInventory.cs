using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterInventory", menuName = "ScriptableObjects/Characters/CharacterInventory")]
public class SCharacterInventory : ScriptableObject
{
    [Header("Gold")]
    [SerializeField] private int gold = 30;
    
    [Header("Equipment")]
    [SerializeField] private List<SEquipmentData> equipmentsList;

    public event Action<int> GoldUpdated;

    public void OnGoldUpdated(int newValue)
    {
        GoldUpdated?.Invoke(newValue);
    }
    
    public int GetGold()
    {
        return gold;
    }

    public int AddGold(int value)
    {
        gold += value;
        
        OnGoldUpdated(gold);
        
        return gold;
    }
    
    public int SpendGold(int value)
    {
        gold -= value;
        
        OnGoldUpdated(gold);
        
        return gold;
    }

    public bool HasGold(int value)
    {
        return gold >= value;
    }
    
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

    private void Reset()
    {
        gold = 100;
    }
}
