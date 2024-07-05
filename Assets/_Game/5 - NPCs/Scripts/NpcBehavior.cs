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
    
    private void OnEnable()
    {
        _inputReader.Interact += Interact;
        uiEvents.CloseTradingWindow += OnLeftTrading;
    }

    private void OnDisable()
    {
        _inputReader.Interact -= Interact;
        uiEvents.CloseTradingWindow -= OnLeftTrading;
    }

    public void OnPlayerEnteredTrigger()
    {
        uiEvents.OnShowInteractionBanner();
    }

    public void OnPlayerLeftTrigger()
    {
        uiEvents.OnHideInteractionBanner();
    }

    private void Interact(bool _)
    {
        if (_inTradingMode) return;

        _inTradingMode = true;
        uiEvents.OnOpenTradingWindow(npcInventory);
    }

    private void OnLeftTrading()
    {
        _inTradingMode = false;
    }
    
}
