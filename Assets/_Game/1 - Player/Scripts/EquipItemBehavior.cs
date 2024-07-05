using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipItemBehavior : MonoBehaviour
{
    [Header("SOs")]
    [SerializeField] private SEquipmentEvents _equipmentEvents;
    [Space] 
    
    //TODO Remove later
    [Header("Debug")]
    [SerializeField] private SEquipmentData equipmentDataSo;

    //TODO change this name
    public void EquipItem()
    {
        _equipmentEvents.OnItemEquipped(equipmentDataSo);
    }

    public void UnequipItem()
    {
        _equipmentEvents.OnItemUnequipped(equipmentDataSo);
    }
}
