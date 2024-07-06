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
        equipmentEvents.EquipItemRequest += OnEquipItemRequest;
        equipmentEvents.UnequipItemRequest += OnUnequipItemRequest;
    }

    private void OnDisable()
    {
        tradingEvents.EquipmentSold -= OnEquipmentSold;
        equipmentEvents.EquipItemRequest -= OnEquipItemRequest;
        equipmentEvents.UnequipItemRequest -= OnUnequipItemRequest;
    }

    private void Awake()
    {
        if(_playerData.GetHeadEquippedItem())
            _playerData.GetHeadEquippedItem().SetCurrentlyEquipped(true);
        if(_playerData.GetHatEquippedItem())
            _playerData.GetHatEquippedItem().SetCurrentlyEquipped(true);
        if(_playerData.GetBodyEquippedItem())
            _playerData.GetBodyEquippedItem().SetCurrentlyEquipped(true);
    }

    private void OnEquipItemRequest(SEquipmentData sEquipmentData)
    {
        SEquipmentData itemToBeUnequipped = _playerData.GetEquippedItemByType(sEquipmentData.GetEquipmentType());

        if (itemToBeUnequipped)
        {
            _playerData.UnequipItem(itemToBeUnequipped);
            equipmentEvents.OnUnequipItem(sEquipmentData);
        }
        
        _playerData.EquipItem(sEquipmentData);
        equipmentEvents.OnEquipItem(sEquipmentData);
    }
    
    private void OnUnequipItemRequest(SEquipmentData sEquipmentData)
    {
        _playerData.UnequipItem(sEquipmentData);
        equipmentEvents.OnUnequipItem(sEquipmentData);
    }

    private void OnEquipmentSold(SEquipmentData sEquipmentData)
    {
        if (sEquipmentData.GetCurrentlyEquipped())
            equipmentEvents.OnUnequipItemRequest(sEquipmentData);
    }
}
