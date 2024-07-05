using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquippedItemsController : MonoBehaviour
{
    [Header("SOs")] 
    [SerializeField] private SEquipmentEvents equipmentEvents;
    [SerializeField] private SPlayerData _playerData;
    
    private void OnEnable()
    {
        equipmentEvents.CurrentEquipmentChanged += OnEquipmentChanged;
        equipmentEvents.CurrentEquipmentRemoved += OnEquipmentRemoved;
    }

    private void OnDisable()
    {
        equipmentEvents.CurrentEquipmentChanged -= OnEquipmentChanged;
        equipmentEvents.CurrentEquipmentRemoved -= OnEquipmentRemoved;
    }
    
    private void OnEquipmentChanged(SEquipmentData sEquipmentData)
    {
        switch (sEquipmentData.GetEquipmentType())
        {
            case Helpers.EquipmentType.HEAD:
                _playerData.SetHeadEquippedItem(sEquipmentData);
                break;
            case Helpers.EquipmentType.HAT:
                _playerData.SetHatEquippedItem(sEquipmentData);
                break;
            case Helpers.EquipmentType.BODY:
                _playerData.SetBodyEquippedItem(sEquipmentData);
                break;
        }
        
        sEquipmentData.SetCurrentlyEquipped(true);
    }
    
    private void OnEquipmentRemoved(SEquipmentData sEquipmentData)
    {
        sEquipmentData.SetCurrentlyEquipped(false);
        _playerData.UnequipItem(sEquipmentData);
    }
}
