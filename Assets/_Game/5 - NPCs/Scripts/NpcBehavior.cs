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
    }

    private void OnDisable()
    {
        _inputReader.Interact -= Interact;
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
        _inTradingMode = !_inTradingMode;
        if(_inTradingMode)
            uiEvents.OnOpenTradingWindow(npcInventory);
        else
            uiEvents.OnCloseTradingWindow();
    }
    
}
