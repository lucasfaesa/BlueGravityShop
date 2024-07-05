using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWindowController : MonoBehaviour
{
    [Header("SOs")] 
    [SerializeField] private SEquipmentEvents _equipmentEvents;
    [SerializeField] private SPlayerData _playerData;
    
    [Header("Components")] 
    [SerializeField] private Transform window;
    [Space]
    [SerializeField] private InventoryItemDisplay headItemDisplay;
    [SerializeField] private InventoryItemDisplay hatItemDisplay;
    [SerializeField] private InventoryItemDisplay bodyItemDisplay;

    private void OnEnable()
    {
        _equipmentEvents.CurrentEquipmentChanged += OnEquipmentChanged ;
    }

    private void OnDisable()
    {
        _equipmentEvents.CurrentEquipmentChanged -= OnEquipmentChanged ;
    }

    
    private void Start()
    {
        UpdateCharacterEquipments();
    }
    
    private void UpdateCharacterEquipments()
    {
        UpdateItemDisplay(headItemDisplay, _playerData.GetHeadEquippedItem());
        UpdateItemDisplay(hatItemDisplay, _playerData.GetHatEquippedItem());
        UpdateItemDisplay(bodyItemDisplay, _playerData.GetBodyEquippedItem());
    }
    
    private void OnEquipmentChanged(SEquipmentData sEquipmentData)
    {
        switch (sEquipmentData.GetEquipmentType())
        {
            case Helpers.EquipmentType.HEAD:
                UpdateItemDisplay(headItemDisplay, sEquipmentData);
                break;
            case Helpers.EquipmentType.HAT:
                UpdateItemDisplay(hatItemDisplay, sEquipmentData);
                break;
            case Helpers.EquipmentType.BODY:
                UpdateItemDisplay(bodyItemDisplay, sEquipmentData);
                break;
        }
    }

    private void UpdateItemDisplay(InventoryItemDisplay itemDisplay, SEquipmentData data)
    {
        if (!data) return;
        
        itemDisplay.SetData(data);
        itemDisplay.gameObject.SetActive(true);
    }
}
