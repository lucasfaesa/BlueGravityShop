using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TradingEvents", menuName = "ScriptableObjects/Trading/TradingEvents")]
public class STradingEvents : ScriptableObject
{
    
    public event Action<SCharacterInventory> TradeStarted;
    public event Action TradeEnded;
    public event Action<SEquipmentData> EquipmentSold;
    public event Action<SEquipmentData> TryingToBuyEquipment;
    public event Action<SEquipmentData> EquipmentBought;

    public void OnTradeStarted(SCharacterInventory npcInventory)
    {
        TradeStarted?.Invoke(npcInventory);
    }

    public void OnTradeEnded()
    {
        TradeEnded?.Invoke();
    }

    public void OnEquipmentSold(SEquipmentData equipmentData)
    {
        EquipmentSold?.Invoke(equipmentData);
    }

    public void OnTryingToBuyEquipment(SEquipmentData equipmentData)
    {
        TryingToBuyEquipment?.Invoke(equipmentData);
    }
    
    public void OnEquipmentBought(SEquipmentData equipmentData)
    {
        EquipmentBought?.Invoke(equipmentData);
    }
}
