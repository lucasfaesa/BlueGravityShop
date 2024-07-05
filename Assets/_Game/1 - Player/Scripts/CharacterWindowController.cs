using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWindowController : MonoBehaviour
{
    [Header("SOs")] 
    [SerializeField] private SUIEvents _uiEvents;
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
        _uiEvents.OpenCharacterWindow += OpenCharacterWindow;
        _uiEvents.CloseCharacterWindow += CloseCharacterWindow;
        _equipmentEvents.ItemEquipped += OnItemEquipped;
        _equipmentEvents.ItemUnequipped += OnItemUnequipped;
    }

    private void OnDisable()
    {
        _uiEvents.OpenCharacterWindow -= OpenCharacterWindow;
        _uiEvents.CloseCharacterWindow -= CloseCharacterWindow;
        _equipmentEvents.ItemEquipped -= OnItemEquipped;
        _equipmentEvents.ItemUnequipped -= OnItemUnequipped;
    }
    
    
    private void Start()
    {
        UpdateCharacterEquipments();
    }

    private void OpenCharacterWindow()
    {
        UpdateCharacterEquipments();
        window.gameObject.SetActive(true);
    }
    
    private void CloseCharacterWindow()
    {
        window.gameObject.SetActive(false);
    }
    
    private void UpdateCharacterEquipments()
    {
        UpdateItemDisplay(headItemDisplay, _playerData.GetHeadEquippedItem());
        UpdateItemDisplay(hatItemDisplay, _playerData.GetHatEquippedItem());
        UpdateItemDisplay(bodyItemDisplay, _playerData.GetBodyEquippedItem());
    }
    
    private void OnItemEquipped(SEquipmentData sEquipmentData)
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
    
    private void OnItemUnequipped(SEquipmentData sEquipmentData)
    {
        switch (sEquipmentData.GetEquipmentType())
        {
            case Helpers.EquipmentType.HEAD:
                UpdateItemDisplay(headItemDisplay, null);
                break;
            case Helpers.EquipmentType.HAT:
                UpdateItemDisplay(hatItemDisplay, null);
                break;
            case Helpers.EquipmentType.BODY:
                UpdateItemDisplay(bodyItemDisplay, null);
                break;
        }
    }

    private void UpdateItemDisplay(InventoryItemDisplay itemDisplay, SEquipmentData data)
    {
        if (!data)
        {
            itemDisplay.gameObject.SetActive(false);
            return;
        };
        
        itemDisplay.SetData(data);
        itemDisplay.gameObject.SetActive(true);
    }
}
