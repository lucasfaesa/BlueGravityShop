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
    [SerializeField] private SInputReader _inputReader;
    
    [Header("Components")] 
    [SerializeField] private Transform window;
    [Space]
    [SerializeField] private InventoryItemDisplay headItemDisplay;
    [SerializeField] private InventoryItemDisplay hatItemDisplay;
    [SerializeField] private InventoryItemDisplay bodyItemDisplay;

    private bool _isCharacterWindowOpen = false;
    
    private void OnEnable()
    {
        _uiEvents.OpenCharacterWindow += OpenCharacterWindow;
        _uiEvents.CloseCharacterWindow += CloseCharacterWindow;
        _equipmentEvents.ItemEquipped += OnItemEquipped;
        _equipmentEvents.ItemUnequipped += OnItemUnequipped;
        _inputReader.Character += InventoryKeyPressed;
    }

    private void OnDisable()
    {
        _uiEvents.OpenCharacterWindow -= OpenCharacterWindow;
        _uiEvents.CloseCharacterWindow -= CloseCharacterWindow;
        _equipmentEvents.ItemEquipped -= OnItemEquipped;
        _equipmentEvents.ItemUnequipped -= OnItemUnequipped;
        _inputReader.Character += InventoryKeyPressed;
    }
    
    private void Start()
    {
        UpdateCharacterEquipments();
    }
    
    private void InventoryKeyPressed(bool _)
    {
        _isCharacterWindowOpen = !_isCharacterWindowOpen;
        
        if(_isCharacterWindowOpen)
            _uiEvents.OnOpenCharacterWindow();
        else
            _uiEvents.OnCloseCharacterWindow();
    }

    private void OpenCharacterWindow()
    {
        _isCharacterWindowOpen = true;
        
        UpdateCharacterEquipments();
        window.gameObject.SetActive(true);
    }
    
    private void CloseCharacterWindow()
    {
        _isCharacterWindowOpen = false;
        
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
