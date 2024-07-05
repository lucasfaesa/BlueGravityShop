using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TradingItemDisplay : MonoBehaviour
{
    [SerializeField] private STradingEvents _tradingEventsSo;
    [Space]
    [SerializeField] private Image _equipmentThumbnail;
    [SerializeField] private TextMeshProUGUI _equipmentName;
    [SerializeField] private TextMeshProUGUI _equipmentPrice;
    [SerializeField] private TextMeshProUGUI _buttonText;

    private int _value;
    private bool _isPlayerWindow;

    private SEquipmentData _equipment;
    
    public void SetData(SEquipmentData equipmentData, bool playerWindow)
    {
        _equipment = equipmentData;
        _equipmentThumbnail.sprite = _equipment.GetEquipmentThumbnail();
        _equipmentName.text = _equipment.GetEquipmentName();
        _isPlayerWindow = playerWindow;

        _value = playerWindow ? _equipment.GetEquipmentDepreciatedValue() : _equipment.GetEquipmentBuyValue();
        _equipmentPrice.text = $"{_value} z";
        
        _buttonText.text = playerWindow ? "SELL" : "BUY";
        
    }

    public void FinishTransaction()
    {
        if(_isPlayerWindow)
            _tradingEventsSo.OnEquipmentSold(_equipment);
        else
            _tradingEventsSo.OnTryingToBuyEquipment(_equipment);
    }
}
