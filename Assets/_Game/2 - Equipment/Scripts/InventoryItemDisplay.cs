using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//TODO Change to equippableItemDisplay?
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

    public void ToggleEquipmentStatus()
    {
        if(_equipment.GetCurrentlyEquipped())
            _equipmentEvents.OnItemUnequipped(_equipment);
        else
            _equipmentEvents.OnItemEquipped(_equipment);
    }
}
