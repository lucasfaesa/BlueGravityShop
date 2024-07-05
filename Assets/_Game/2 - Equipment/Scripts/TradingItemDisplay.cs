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

    private int value;
    private bool isPlayerWindow;
    
    public void SetData(Sprite thumb, string name, int price, bool isPlayerWindow)
    {
        _equipmentThumbnail.sprite = thumb;
        _equipmentName.text = name;
        _equipmentPrice.text = $"{price} z";
        _buttonText.text = isPlayerWindow ? "SELL" : "BUY";
        value = price;
        this.isPlayerWindow = isPlayerWindow;
    }

    public void FinishTransaction()
    {
        if(isPlayerWindow)
            _tradingEventsSo.OnEquipmentSold(value);
        else
            _tradingEventsSo.OnEquipmentBought(value);
    }
}
