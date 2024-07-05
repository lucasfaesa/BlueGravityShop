using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradingBehavior : MonoBehaviour
{
    [Header("SOs")]
    [SerializeField] private SCharacterInventory _playerInventory;
    
    [Header("Player")] 
    [SerializeField] private Transform _playerInventoryPanelContent;
    [Header("NPC")]
    [SerializeField] private Transform _npcInventoryPanelContent;

    [Header("Misc")] 
    [SerializeField] private TradingItemDisplay _tradingItemDisplayPrefab;
    [SerializeField] private List<TradingItemDisplay> _instantiatedTradingItemDisplays;

    private void OnEnable()
    {
        if (_playerInventory.GetEquipmentsList().Count > _instantiatedTradingItemDisplays.Count)
        {
            InstantiateTradingItemDisplays(_playerInventory.GetEquipmentsList().Count - _instantiatedTradingItemDisplays.Count, _playerInventoryPanelContent);
        }

        var inventoryList = _playerInventory.GetEquipmentsList();
        
        for (int i = 0; i < inventoryList.Count; i++)
        {
            var equipment = inventoryList[i];
            _instantiatedTradingItemDisplays[i].SetData(equipment.GetEquipmentThumbnail(), 
                                                        equipment.GetEquipmentName(),
                                                        equipment.GetEquipmentDepreciatedValue().ToString() );
            
            _instantiatedTradingItemDisplays[i].gameObject.SetActive(true);
        }
    }

    private void OnDisable()
    {
        foreach (var tradingItemDisplay in _instantiatedTradingItemDisplays)
        {
            tradingItemDisplay.gameObject.SetActive(false);
        }
    }

    private void InstantiateTradingItemDisplays(int qty, Transform window)
    {
        for (int i = 0; i < qty; i++)
        {
            var instantiatedPrefab = Instantiate(_tradingItemDisplayPrefab, window);
            instantiatedPrefab.gameObject.SetActive(false);
        
            _instantiatedTradingItemDisplays.Add(instantiatedPrefab);    
        }
    }
}
