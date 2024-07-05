using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryEvents", menuName = "ScriptableObjects/Inventory/InventoryEvents")]
public class SInventoryEvents : ScriptableObject
{
    public event Action InventoryOpen;
    public event Action InventoryClosed;

    public void OnInventoryOpen()
    {
        InventoryOpen?.Invoke();
    }

    public void OnInventoryClosed()
    {
        InventoryClosed?.Invoke();
    }
}
