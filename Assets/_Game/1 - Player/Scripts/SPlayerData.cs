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

    [Header("States")] 
    [SerializeField] private Helpers.FacingDirection _facingDirection = Helpers.FacingDirection.SOUTH;
    [SerializeField] private Helpers.PlayerCurrentState _currentState = Helpers.PlayerCurrentState.IDLE;
    
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

    public Helpers.FacingDirection GetFacingDirection()
    {
        return _facingDirection;
    }

    public Helpers.PlayerCurrentState GetCurrentState()
    {
        return _currentState;
    }
    
    public void SetFacingDirection(Helpers.FacingDirection facingDirection)
    {
        _facingDirection = facingDirection;
    }

    public void SetCurrentState(Helpers.PlayerCurrentState playerCurrentState)
    {
        _currentState = playerCurrentState;
    }

    public SEquipmentData GetEquippedItemByType(Helpers.EquipmentType type)
    {
        switch (type)
        {
            case Helpers.EquipmentType.HEAD:
                return headEquipment;
                
            case Helpers.EquipmentType.HAT:
                return hatEquipment;
                
            case Helpers.EquipmentType.BODY:
                return bodyEquipment;
        }

        return null;
    }
    
    public void EquipItem(SEquipmentData data)
    {
        switch (data.GetEquipmentType())
        {
            case Helpers.EquipmentType.HEAD:
                headEquipment = data;
                break;
            case Helpers.EquipmentType.HAT:
                hatEquipment = data;
                break;
            case Helpers.EquipmentType.BODY:
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
