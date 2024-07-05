using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquippedItemsController : MonoBehaviour
{
    [Header("SOs")] 
    [SerializeField] private STradingEvents tradingEvents;
    [SerializeField] private SEquipmentEvents equipmentEvents;
    [SerializeField] private SPlayerData _playerData;
    
    private void OnEnable()
    {
        tradingEvents.EquipmentSold += OnEquipmentSold;
        equipmentEvents.ItemEquipped += OnItemEquipped;
        equipmentEvents.ItemUnequipped += OnItemUnequipped;
    }

    private void OnDisable()
    {
        tradingEvents.EquipmentSold -= OnEquipmentSold;
        equipmentEvents.ItemEquipped -= OnItemEquipped;
        equipmentEvents.ItemUnequipped -= OnItemUnequipped;
    }
    
    private void OnItemEquipped(SEquipmentData sEquipmentData)
    {
        _playerData.EquipItem(sEquipmentData);
    }
    
    private void OnItemUnequipped(SEquipmentData sEquipmentData)
    {
        _playerData.UnequipItem(sEquipmentData);
    }

    private void OnEquipmentSold(SEquipmentData sEquipmentData)
    {
        if (sEquipmentData.GetCurrentlyEquipped())
            equipmentEvents.OnItemUnequipped(sEquipmentData);
    }
}
