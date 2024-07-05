using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    [SerializeField] private STradingEvents tradingEventsSO;

    [SerializeField] private SCharacterInventory npcInventory;

    private bool _isShopClosed;

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
}
