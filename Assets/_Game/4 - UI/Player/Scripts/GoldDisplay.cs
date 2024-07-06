using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GoldDisplay : MonoBehaviour
{
    
    [SerializeField] private SCharacterInventory _playerInventory;
    [Space] 
    [SerializeField] private TextMeshProUGUI _goldQtyText;
    [SerializeField] private HorizontalLayoutGroup _horizontalLayoutGroup;

    private void OnEnable()
    {
        UpdateText(_playerInventory.GetGold());
        
        _playerInventory.GoldUpdated += UpdateText;
    }

    private void OnDisable()
    {
        _playerInventory.GoldUpdated -= UpdateText;
    }
    
    private void UpdateText(int value)
    {
        _goldQtyText.text = value + " z";
        StartCoroutine(UpdateRect());
    }
    
    IEnumerator UpdateRect()
    {
        _horizontalLayoutGroup.enabled = false;
        yield return new WaitForSeconds(0.01f);
        _horizontalLayoutGroup.enabled = true;
    }
}
