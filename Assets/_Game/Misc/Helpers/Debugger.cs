using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    [Header("Trading")]
    [SerializeField] private STradingEvents tradingEventsSO;
    [SerializeField] private SCharacterInventory npcInventory;

    [Header("Inventory")]
    [SerializeField] private SInventoryEvents inventoryEventsSO;
    
    private bool _isShopClosed;
    private bool _isInventoryClosed;
    
    public void ToggleShop()
    {
        _isShopClosed = !_isShopClosed;
        
        if(_isShopClosed)
            OpenShopWithNpc();
        else
            CloseShopWithNpc();
    }
    
    public void OpenShopWithNpc()
    {
        tradingEventsSO.OnTradeStarted(npcInventory);
    }

    public void CloseShopWithNpc()
    {
        tradingEventsSO.OnTradeEnded();
    }

    public void ToggleInventory()
    {
        _isInventoryClosed = !_isInventoryClosed;
        
        if(_isInventoryClosed)
            inventoryEventsSO.OnInventoryOpen();
        else
            inventoryEventsSO.OnInventoryClosed();
    }
}
