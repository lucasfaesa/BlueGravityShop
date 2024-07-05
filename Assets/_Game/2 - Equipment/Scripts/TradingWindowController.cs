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

    private SCharacterInventory npcCharacterInventory;
    
    private void OnEnable()
    {
        _tradingEventsSo.TradeStarted += OpenShop;
        _tradingEventsSo.TradeEnded += CloseShop;
        _tradingEventsSo.TryingToBuyEquipment += OnTryToBuyEquipment;
        _tradingEventsSo.EquipmentSold += OnEquipmentSold;
    }

    private void OnDisable()
    {
        _tradingEventsSo.TradeStarted -= OpenShop;
        _tradingEventsSo.TradeEnded -= CloseShop;
        _tradingEventsSo.TryingToBuyEquipment -= OnTryToBuyEquipment;
        _tradingEventsSo.EquipmentSold -= OnEquipmentSold;
    }
    
    private void OpenShop(SCharacterInventory npcInventory)
    {
        npcCharacterInventory = npcInventory;
        
        SetDisplayData(_playerInventory, _instantiatedPlayerTradingItemDisplays, true);
        
        SetDisplayData(npcInventory, _instantiatedNpcTradingItemDisplays, false);
        
        shopWindow.gameObject.SetActive(true);
    }

    private void SetDisplayData(SCharacterInventory characterInventorySO, List<TradingItemDisplay> displayPool, bool isPlayerWindow)
    {
        var equipmentsList = characterInventorySO.GetEquipmentsList();
        
        //instantiating pool
        if (characterInventorySO.GetEquipmentsList().Count > displayPool.Count)
        {
            InstantiateTradingItemDisplays(equipmentsList.Count - displayPool.Count,
                                                isPlayerWindow ? _playerInventoryPanelContent : _npcInventoryPanelContent,
                                                    isPlayerWindow);
        }
        
        for (int i = 0; i < equipmentsList.Count; i++)
        {
            var equipment = equipmentsList[i];

            displayPool[i].SetData(equipment, isPlayerWindow );
            
            displayPool[i].gameObject.SetActive(true); 
        }
    }

    private void RefreshShop(SCharacterInventory npcInventory)
    {
        HideAllEquipmentsDisplay();
        
        SetDisplayData(_playerInventory, _instantiatedPlayerTradingItemDisplays, true);
        
        SetDisplayData(npcInventory, _instantiatedNpcTradingItemDisplays, false);
    }

    private void CloseShop()
    {
        shopWindow.gameObject.SetActive(false);
        
        npcCharacterInventory = null;

        HideAllEquipmentsDisplay();
    }

    private void HideAllEquipmentsDisplay()
    {
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

    private void OnTryToBuyEquipment(SEquipmentData equipmentData)
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
    }
}
