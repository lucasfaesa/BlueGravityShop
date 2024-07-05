using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldDisplay : MonoBehaviour
{
    
    [SerializeField] private SCharacterInventory _playerInventory;
    [Space] 
    [SerializeField] private TextMeshProUGUI _goldQtyText;

    private void OnEnable()
    {
        _playerInventory.GoldUpdated += UpdateText;
    }

    private void OnDisable()
    {
        _playerInventory.GoldUpdated -= UpdateText;
    }

    void Start()
    {
        _goldQtyText.text = _playerInventory.GetGold().ToString();
    }

    private void UpdateText(int value)
    {
        _goldQtyText.text = value.ToString();
    }
}
