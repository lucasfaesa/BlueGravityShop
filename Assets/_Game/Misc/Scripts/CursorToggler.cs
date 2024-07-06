using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorToggler : MonoBehaviour
{
    [SerializeField] private SUIEvents uiEvents;

    private bool tradeWindowOpen;
    private bool characterWindowOpen;
    private bool inventoryWindowOpen;
    
    private void OnEnable()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        uiEvents.OpenTradingWindow += OnTradeWindowOpen;
        uiEvents.OpenInventoryWindow += OnInventoryWindowOpen;
        uiEvents.OpenCharacterWindow += OnCharacterWindowOpen;

        uiEvents.CloseTradingWindow += OnTradeWindowClosed;
        uiEvents.CloseInventoryWindow += OnInventoryWindowClosed;
        uiEvents.CloseCharacterWindow += OnCharacterWindowClosed;
    }

    private void OnDisable()
    {
        uiEvents.OpenTradingWindow -= OnTradeWindowOpen;
        uiEvents.OpenInventoryWindow -= OnInventoryWindowOpen;
        uiEvents.OpenCharacterWindow -= OnCharacterWindowOpen;

        uiEvents.CloseTradingWindow -= OnTradeWindowClosed;
        uiEvents.CloseInventoryWindow -= OnInventoryWindowClosed;
        uiEvents.CloseCharacterWindow -= OnCharacterWindowClosed;
    }

    private void OnTradeWindowOpen(SCharacterInventory _)
    {
        tradeWindowOpen = true;
        FreeCursor();
    }

    private void OnTradeWindowClosed()
    {
        tradeWindowOpen = false;
        LockCursor();
    }
    
    private void OnInventoryWindowOpen()
    {
        inventoryWindowOpen = true;
        FreeCursor();
    }

    private void OnInventoryWindowClosed()
    {
        inventoryWindowOpen = false;
        LockCursor();
    }
    
    private void OnCharacterWindowOpen()
    {
        characterWindowOpen = true;
        FreeCursor();
    }

    private void OnCharacterWindowClosed()
    {
        characterWindowOpen = false;
        LockCursor();
    }

    private void FreeCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void LockCursor()
    {
        if (!tradeWindowOpen && !characterWindowOpen && !inventoryWindowOpen)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    
}
