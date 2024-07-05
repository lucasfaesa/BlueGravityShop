using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryWindowController : MonoBehaviour
{

    [Header("SOs")] 
    [SerializeField] private STradingEvents _tradingEvents;
    [SerializeField] private SUIEvents _uiEvents;
    [SerializeField] private SEquipmentEvents _equipmentEvents;
    [SerializeField] private SCharacterInventory _playerInventory;
    
    [Header("Player")] 
    [SerializeField] private Transform _inventoryPanelContent;

    [Header("Misc")] 
    [SerializeField] private Transform _inventoryWindow;
    [SerializeField] private InventoryItemDisplay _inventoryItemDisplayPrefab;
    
    private List<InventoryItemDisplay> _instantiatedPlayerInventoryItemDisplays = new();

    private void OnEnable()
    {
        _equipmentEvents.ItemEquipped += RefreshInventory;
        _equipmentEvents.ItemUnequipped += RefreshInventory;
        _tradingEvents.EquipmentBought += RefreshInventory;
        _tradingEvents.EquipmentSold += RefreshInventory;
        _uiEvents.OpenInventoryWindow += OpenInventory;
        _uiEvents.CloseInventoryWindow += CloseInventory;
    }

    private void OnDisable()
    {
        _equipmentEvents.ItemEquipped -= RefreshInventory;
        _equipmentEvents.ItemUnequipped -= RefreshInventory;
        _tradingEvents.EquipmentBought -= RefreshInventory;
        _tradingEvents.EquipmentSold -= RefreshInventory;
        _uiEvents.OpenInventoryWindow -= OpenInventory;
        _uiEvents.CloseInventoryWindow -= CloseInventory;
    }
    
    private void OpenInventory()
    {
        SetDisplayData(_playerInventory, _instantiatedPlayerInventoryItemDisplays);
        
        _inventoryWindow.gameObject.SetActive(true);
    }

    private void SetDisplayData(SCharacterInventory characterInventorySO, List<InventoryItemDisplay> displayPool)
    {
        var equipmentsList = characterInventorySO.GetEquipmentsList().Where(x=>!x.GetCurrentlyEquipped()).ToList();
        
        //instantiating pool
        if (characterInventorySO.GetEquipmentsList().Count > displayPool.Count)
        {
            InstantiateEquipmentItemDisplays(equipmentsList.Count - displayPool.Count);
        }
        
        for (int i = 0; i < equipmentsList.Count; i++)
        {
            var equipment = equipmentsList[i];

            displayPool[i].SetData(equipment);
            
            displayPool[i].gameObject.SetActive(true); 
        }
    }

    private void RefreshInventory(SEquipmentData _)
    {
        HideAllEquipmentsDisplay();
        
        SetDisplayData(_playerInventory, _instantiatedPlayerInventoryItemDisplays);
    }

    private void CloseInventory()
    {
        _inventoryWindow.gameObject.SetActive(false);
        
        HideAllEquipmentsDisplay();
    }

    private void HideAllEquipmentsDisplay()
    {
        _instantiatedPlayerInventoryItemDisplays.ForEach(x=>x.gameObject.SetActive(false));
    }

    private void InstantiateEquipmentItemDisplays(int qty)
    {
        for (int i = 0; i < qty; i++)
        {
            var instantiatedPrefab = Instantiate(_inventoryItemDisplayPrefab, _inventoryPanelContent);
            instantiatedPrefab.gameObject.SetActive(false);
            
            _instantiatedPlayerInventoryItemDisplays.Add(instantiatedPrefab);    
        }
    }
}
