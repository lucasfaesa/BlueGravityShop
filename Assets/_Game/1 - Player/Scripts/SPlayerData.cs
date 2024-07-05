using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/Player/PlayerData")]
public class SPlayerData : ScriptableObject
{
    [SerializeField] private SEquipmentData headEquipment;
    [SerializeField] private SEquipmentData hatEquipment;
    [SerializeField] private SEquipmentData bodyEquipment;

    public SEquipmentData GetHeadEquippedItem()
    {
        return headEquipment;
    }
    public SEquipmentData GetHatEquippedItem()
    {
        return hatEquipment;
    }
    public SEquipmentData GetBodyEquippedItem()
    {
        return bodyEquipment;
    }
    
    
    public void SetHeadEquippedItem(SEquipmentData data)
    {
        headEquipment = data;
    }
    public void SetHatEquippedItem(SEquipmentData data)
    {
        hatEquipment = data;
    }
    public void SetBodyEquippedItem(SEquipmentData data)
    {
        bodyEquipment = data;
    }
    
    public void UnequipItem(SEquipmentData data)
    {
        switch (data.GetEquipmentType())
        {
            case Helpers.EquipmentType.HEAD:
                headEquipment = null;
                break;
            case Helpers.EquipmentType.HAT:
                hatEquipment = null;
                break;
            case Helpers.EquipmentType.BODY:
                bodyEquipment = null;
                break;
        }
        
    }
}
