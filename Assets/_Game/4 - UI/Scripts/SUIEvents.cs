using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UIEvents", menuName = "ScriptableObjects/UI/UIEvents")]
public class SUIEvents : ScriptableObject
{
    public event Action OpenInventoryWindow;
    public event Action CloseInventoryWindow;

    public event Action<SCharacterInventory> OpenTradingWindow;
    public event Action CloseTradingWindow;

    public event Action OpenCharacterWindow;
    public event Action CloseCharacterWindow;

    public event Action ShowInteractionBanner;
    public event Action HideInteractionBanner;
    
    public void OnOpenInventoryWindow()
    {
        OpenInventoryWindow?.Invoke();
    }

    public void OnCloseInventoryWindow()
    {
        CloseInventoryWindow?.Invoke();
    }

    public void OnOpenTradingWindow(SCharacterInventory npcInventory)
    {
        OpenTradingWindow?.Invoke(npcInventory);
    }

    public void OnCloseTradingWindow()
    {
        CloseTradingWindow?.Invoke();
    }

    public void OnOpenCharacterWindow()
    {
        OpenCharacterWindow?.Invoke();
    }

    public void OnCloseCharacterWindow()
    {
        CloseCharacterWindow?.Invoke();
    }

    public void OnShowInteractionBanner()
    {
        ShowInteractionBanner?.Invoke();
    }

    public void OnHideInteractionBanner()
    {
        HideInteractionBanner?.Invoke();
    }
}
