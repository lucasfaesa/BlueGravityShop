using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryWindowController : MonoBehaviour
{

    [Header("SOs")] 
    [SerializeField] private SInventoryEvents _inventoryEvents;
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
        _inventoryEvents.InventoryOpen += OpenInventory;
        _inventoryEvents.InventoryClosed += CloseInventory;
    }

    private void OnDisable()
    {
        _inventoryEvents.InventoryOpen -= OpenInventory;
        _inventoryEvents.InventoryClosed -= CloseInventory;
    }
    
    private void OpenInventory()
    {
        SetDisplayData(_playerInventory, _instantiatedPlayerInventoryItemDisplays);
        
        _inventoryWindow.gameObject.SetActive(true);
    }

    private void SetDisplayData(SCharacterInventory characterInventorySO, List<InventoryItemDisplay> displayPool)
    {
        var equipmentsList = characterInventorySO.GetEquipmentsList();
        
        //instantiating pool
        if (characterInventorySO.GetEquipmentsList().Count > displayPool.Count)
        {
            InstantiateEquipmentItemDisplays(equipmentsList.Count - displayPool.Count, _inventoryWindow);
        }
        
        for (int i = 0; i < equipmentsList.Count; i++)
        {
            var equipment = equipmentsList[i];

            displayPool[i].SetData(equipment);
            
            displayPool[i].gameObject.SetActive(true); 
        }
    }

    private void RefreshInventory(SCharacterInventory playerInventory)
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

    private void InstantiateEquipmentItemDisplays(int qty, Transform window)
    {
        
        for (int i = 0; i < qty; i++)
        {
            var instantiatedPrefab = Instantiate(_inventoryItemDisplayPrefab, _inventoryPanelContent);
            instantiatedPrefab.gameObject.SetActive(false);
            
            _instantiatedPlayerInventoryItemDisplays.Add(instantiatedPrefab);    
        }
    }

    //TODO Implement later
    /*private void OnTryToBuyEquipment(SEquipmentData equipmentData)
    {
        if (!_playerInventory.HasGold(equipmentData.GetEquipmentBuyValue()))
            return;
        
        npcCharacterInventory.RemoveEquipment(equipmentData);
        _playerInventory.AddEquipment(equipmentData);

        _playerInventory.SpendGold(equipmentData.GetEquipmentBuyValue());
        
        RefreshShop(npcCharacterInventory);
    }
    
    private void OnEquipmentSold(SEquipmentData equipmentData)
    {
        npcCharacterInventory.AddEquipment(equipmentData);
        _playerInventory.RemoveEquipment(equipmentData);

        _playerInventory.AddGold(equipmentData.GetEquipmentDepreciatedValue());
        
        RefreshShop(npcCharacterInventory);
    }*/
}