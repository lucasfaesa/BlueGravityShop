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
    
    //debug
    public void EquipItem()
    {
        _equipmentEvents.OnEquipItemRequest(equipmentDataSo);
    }

    //debug
    public void UnequipItem()
    {
        _equipmentEvents.OnUnequipItemRequest(equipmentDataSo);
    }
}
