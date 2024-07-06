using System;
using System.Collections;
using UnityEngine;

public class NpcBehavior : MonoBehaviour
{
    [Header("SOs")]
    [SerializeField] private SInputReader _inputReader;
    [SerializeField] private SUIEvents uiEvents;
    [SerializeField] private SCharacterInventory npcInventory;

    private bool _inTradingMode;
    private bool _insideTradingArea;
    
    private void OnEnable()
    {
        _inputReader.Interact += Interact;
    }

    private void OnDisable()
    {
        _inputReader.Interact -= Interact;
    }

    public void OnPlayerEnteredTrigger()
    {
        uiEvents.OnShowInteractionBanner();
        _insideTradingArea = true;
    }

    public void OnPlayerLeftTrigger()
    {
        uiEvents.OnHideInteractionBanner();
        _insideTradingArea = false;
    }

    private void Interact(bool _)
    {
        if (!_insideTradingArea) return;
        
        _inTradingMode = !_inTradingMode;
        if(_inTradingMode)
            uiEvents.OnOpenTradingWindow(npcInventory);
        else
            uiEvents.OnCloseTradingWindow();
    }
    
}
