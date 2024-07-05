using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TradingEvents", menuName = "ScriptableObjects/Trading/TradingEvents")]
public class STradingEvents : ScriptableObject
{
    
    public event Action<SCharacterInventory> TradeStarted;
    public event Action TradeEnded;
    public event Action<int> EquipmentSold;
    public event Action<int> EquipmentBought;

    public void OnTradeStarted(SCharacterInventory npcInventory)
    {
        TradeStarted?.Invoke(npcInventory);
    }

    public void OnTradeEnded()
    {
        TradeEnded?.Invoke();
    }

    public void OnEquipmentSold(int value)
    {
        EquipmentSold?.Invoke(value);
    }
    
    public void OnEquipmentBought(int value)
    {
        EquipmentBought?.Invoke(value);
    }
}
