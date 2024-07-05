using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/Player/PlayerData")]
public class SPlayerData : ScriptableObject
{
    [Header("Status Related")] 
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 10f;
    
    [Header("Equipment Related")]
    [SerializeField] private SEquipmentData headEquipment;
    [SerializeField] private SEquipmentData hatEquipment;
    [SerializeField] private SEquipmentData bodyEquipment;

    public float GetSpeed(bool sprinting)
    {
        return sprinting ? sprintSpeed : walkSpeed;
    }
    
    
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
    
    public void EquipItem(SEquipmentData data)
    {
        switch (data.GetEquipmentType())
        {
            case Helpers.EquipmentType.HEAD:
                if(headEquipment)
                    headEquipment.SetCurrentlyEquipped(false);
                
                headEquipment = data;
                break;
            case Helpers.EquipmentType.HAT:
                if(hatEquipment)
                    hatEquipment.SetCurrentlyEquipped(false);
                
                hatEquipment = data;
                break;
            case Helpers.EquipmentType.BODY:
                if(bodyEquipment)
                    bodyEquipment.SetCurrentlyEquipped(false);
                
                bodyEquipment = data;
                break;
        }
        
        data.SetCurrentlyEquipped(true);
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
        
        data.SetCurrentlyEquipped(false);
    }
}
