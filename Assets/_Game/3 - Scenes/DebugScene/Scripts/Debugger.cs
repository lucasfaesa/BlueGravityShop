using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    [Header("UI Events")] 
    [SerializeField] private SUIEvents uiEvents;
    
    [Header("Trading related")]
    [SerializeField] private STradingEvents tradingEventsSO;
    [SerializeField] private SCharacterInventory npcInventory;
    
    private bool _isTradingWindowClosed;
    private bool _isInventoryClosed;
    private bool _isCharacterWindowClosed;
    
    public void ToggleShop()
    {
        _isTradingWindowClosed = !_isTradingWindowClosed;
        
        if(_isTradingWindowClosed)
            uiEvents.OnOpenTradingWindow(npcInventory);
        else
            uiEvents.OnCloseTradingWindow();
    }

    public void ToggleInventory()
    {
        _isInventoryClosed = !_isInventoryClosed;
        
        if(_isInventoryClosed)
            uiEvents.OnOpenInventoryWindow();
        else
            uiEvents.OnCloseInventoryWindow();
    }

    public void ToggleCharacterWindow()
    {
        _isCharacterWindowClosed = !_isCharacterWindowClosed;
        
        if(_isCharacterWindowClosed)
            uiEvents.OnOpenCharacterWindow();
        else
            uiEvents.OnCloseCharacterWindow();
    }
}
