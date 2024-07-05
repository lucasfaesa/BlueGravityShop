using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemDisplay : MonoBehaviour
{
    [SerializeField] private SEquipmentEvents _equipmentEvents;
    [Space]
    [SerializeField] private Image _equipmentThumbnail;
    [SerializeField] private TextMeshProUGUI _equipmentName;

    private SEquipmentData _equipment;
    
    public void SetData(SEquipmentData equipmentData)
    {
        _equipment = equipmentData;
        _equipmentThumbnail.sprite = _equipment.GetEquipmentThumbnail();
        _equipmentName.text = _equipment.GetEquipmentName();
    }

    public void EquipItem()
    {
        _equipmentEvents.OnCurrentEquipmentChanged(_equipment);
    }
}
