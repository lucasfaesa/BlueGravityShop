using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TradingItemDisplay : MonoBehaviour
{
    [SerializeField] private Image equipmentThumbnail;
    [SerializeField] private TextMeshProUGUI equipmentName;
    [SerializeField] private TextMeshProUGUI equipmentPrice;
    
    public void SetData(Sprite thumb, string name, string price)
    {
        equipmentThumbnail.sprite = thumb;
        equipmentName.text = name;
        equipmentPrice.text = $"{price} z";
    }
}
