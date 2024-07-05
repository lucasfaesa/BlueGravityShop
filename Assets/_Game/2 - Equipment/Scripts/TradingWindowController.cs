using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradingWindowController : MonoBehaviour
{
    [Header("SOs")] 
    [SerializeField] private STradingEvents _tradingEventsSo;
    [SerializeField] private SCharacterInventory _playerInventory;
    
    [Header("Player")] 
    [SerializeField] private Transform _playerInventoryPanelContent;
    [Header("NPC")]
    [SerializeField] private Transform _npcInventoryPanelContent;

    [Header("Misc")] 
    [SerializeField] private Transform shopWindow;
    [SerializeField] private TradingItemDisplay _tradingItemDisplayPrefab;
    
    private List<TradingItemDisplay> _instantiatedPlayerTradingItemDisplays = new();
    private List<TradingItemDisplay> _instantiatedNpcTradingItemDisplays = new();

    private void OnEnable()
    {
        _tradingEventsSo.TradeStarted += OpenShop;
        _tradingEventsSo.TradeEnded += CloseShop;
    }

    private void OnDisable()
    {
        _tradingEventsSo.TradeStarted -= OpenShop;
        _tradingEventsSo.TradeEnded -= CloseShop;
    }
    
    private void OpenShop(SCharacterInventory npcEquipmentData)
    {
        
        /*if (_playerInventory.GetEquipmentsList().Count > _instantiatedPlayerTradingItemDisplays.Count)
        {
            InstantiateTradingItemDisplays(_playerInventory.GetEquipmentsList().Count - _instantiatedPlayerTradingItemDisplays.Count, 
                                                _playerInventoryPanelContent, true);
        }
        
        if (npcEquipmentData.GetEquipmentsList().Count > _instantiatedNpcTradingItemDisplays.Count)
        {
            InstantiateTradingItemDisplays(npcEquipmentData.GetEquipmentsList().Count - _instantiatedNpcTradingItemDisplays.Count,
                                                _npcInventoryPanelContent, false);
        }*/
        
        SetDisplayData(_playerInventory, _instantiatedPlayerTradingItemDisplays, true);
        
        SetDisplayData(npcEquipmentData, _instantiatedNpcTradingItemDisplays, false);
        
        
        shopWindow.gameObject.SetActive(true);
    }

    private void SetDisplayData(SCharacterInventory characterInventorySO, List<TradingItemDisplay> displayPool, bool isPlayerWindow)
    {
        var equipmentsList = characterInventorySO.GetEquipmentsList();
        
        if (characterInventorySO.GetEquipmentsList().Count > displayPool.Count)
        {
            InstantiateTradingItemDisplays(equipmentsList.Count - displayPool.Count,
                                                isPlayerWindow ? _playerInventoryPanelContent : _npcInventoryPanelContent,
                                                    isPlayerWindow);
        }
        
        for (int i = 0; i < equipmentsList.Count; i++)
        {
            var equipment = equipmentsList[i];

            displayPool[i].SetData(equipment.GetEquipmentThumbnail(), equipment.GetEquipmentName(),
                                            isPlayerWindow ? equipment.GetEquipmentDepreciatedValue() : equipment.GetEquipmentBuyValue(), 
                                                isPlayerWindow );
            
            displayPool[i].gameObject.SetActive(true); 
        }
    }

    private void CloseShop()
    {
        shopWindow.gameObject.SetActive(false);

        _instantiatedPlayerTradingItemDisplays.ForEach(x=>x.gameObject.SetActive(false));
        _instantiatedNpcTradingItemDisplays.ForEach(x=>x.gameObject.SetActive(false));
    }

    private void InstantiateTradingItemDisplays(int qty, Transform window, bool playerWindow)
    {
        for (int i = 0; i < qty; i++)
        {
            var instantiatedPrefab = Instantiate(_tradingItemDisplayPrefab, window);
            instantiatedPrefab.gameObject.SetActive(false);
            
            if(playerWindow)
                _instantiatedPlayerTradingItemDisplays.Add(instantiatedPrefab);    
            else
                _instantiatedNpcTradingItemDisplays.Add(instantiatedPrefab);
        }
    }
}
