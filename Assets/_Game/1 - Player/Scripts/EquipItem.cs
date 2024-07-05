using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipItem : MonoBehaviour
{
    [Header("SOs")]
    [SerializeField] private SPlayerEvents _playerEvents;
    [Space] 
    [SerializeField] private SEquipmentData equipmentDataSo;

    //TODO change this name
    public void EquipItemMethod()
    {
        _playerEvents.OnCurrentEquipmentChanged(equipmentDataSo);
    }

    public void UnequipItem()
    {
        _playerEvents.OnEquipmentRemove(equipmentDataSo.GetEquipmentType());
    }
}
