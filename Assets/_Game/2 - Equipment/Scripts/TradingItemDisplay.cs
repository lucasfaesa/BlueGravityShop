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

    private SEquipmentData equipment;
    
    public void SetData(SEquipmentData equipmentData, bool isPlayerWindow)
    {
        equipment = equipmentData;
        _equipmentThumbnail.sprite = equipment.GetEquipmentThumbnail();
        _equipmentName.text = equipment.GetEquipmentName();

        value = isPlayerWindow ? equipment.GetEquipmentDepreciatedValue() : equipment.GetEquipmentBuyValue();
        _equipmentPrice.text = $"{value} z";
        
        _buttonText.text = isPlayerWindow ? "SELL" : "BUY";
        
        this.isPlayerWindow = isPlayerWindow;
    }

    public void FinishTransaction()
    {
        if(isPlayerWindow)
            _tradingEventsSo.OnEquipmentSold(equipment);
        else
            _tradingEventsSo.OnEquipmentBought(equipment); //TODO check if the player has the money
    }
}
