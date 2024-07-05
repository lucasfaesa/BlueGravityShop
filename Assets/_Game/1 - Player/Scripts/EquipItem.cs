using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipItem : MonoBehaviour
{
    [Header("SOs")]
    [SerializeField] private SPlayerEvents _playerEvents;
    [Space] 
    [SerializeField] private SSpriteData _spriteDataSO;

    //TODO change this name
    public void EquipItemMethod()
    {
        _playerEvents.OnCurrentEquipmentChanged(_spriteDataSO);
    }

    public void UnequipItem()
    {
        _playerEvents.OnEquipmentRemove(_spriteDataSO.GetEquipmentType());
    }
}
