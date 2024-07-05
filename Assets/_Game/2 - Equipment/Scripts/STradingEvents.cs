using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TradingEvents", menuName = "ScriptableObjects/Trading/TradingEvents")]
public class STradingEvents : ScriptableObject
{
    public event Action<SEquipmentData> SellEquipmentRequest;
    public event Action<SEquipmentData> EquipmentSold;
    public event Action<SEquipmentData> BuyEquipmentRequest;
    public event Action<SEquipmentData> EquipmentBought;

    public void OnSellEquipmentRequest(SEquipmentData equipmentData)
    {
        SellEquipmentRequest?.Invoke(equipmentData);
    }

    public void OnEquipmentSold(SEquipmentData equipmentData)
    {
        EquipmentSold?.Invoke(equipmentData);
    }

    public void OnBuyEquipmentRequest(SEquipmentData equipmentData)
    {
        BuyEquipmentRequest?.Invoke(equipmentData);
    }
    
    public void OnEquipmentBought(SEquipmentData equipmentData)
    {
        EquipmentBought?.Invoke(equipmentData);
    }
}
