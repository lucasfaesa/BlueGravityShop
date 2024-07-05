using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerEvents", menuName = "ScriptableObjects/Player/PlayerEvents")]
public class SPlayerEvents : ScriptableObject
{
    public event Action<Helpers.FacingDirection> FacingDirectionChanged;
    public event Action<Helpers.PlayerCurrentState> PlayerCurrentStateChanged;
    
    //TODO separate player events from equipment events
    public event Action<SSpriteData> CurrentEquipmentChanged;
    public event Action<Helpers.EquipmentType> CurrentEquipmentRemoved;

    public void OnFacingDirectionChanged(Helpers.FacingDirection direction)
    {
        FacingDirectionChanged?.Invoke(direction);
    }
    
    public void OnPlayerCurrentStateChanged(Helpers.PlayerCurrentState state)
    {
        PlayerCurrentStateChanged?.Invoke(state);
    }

    public void OnCurrentEquipmentChanged(SSpriteData spriteData)
    {
        CurrentEquipmentChanged?.Invoke(spriteData);
    }

    public void OnEquipmentRemove(Helpers.EquipmentType type)
    {
        CurrentEquipmentRemoved?.Invoke(type);
    }
    
    
    
    #region Debug Methods

    public void FaceNorth()
    {
        OnFacingDirectionChanged(Helpers.FacingDirection.NORTH);
    }
    public void FaceSouth()
    {
        OnFacingDirectionChanged(Helpers.FacingDirection.SOUTH);
    }
    public void FaceEast()
    {
        OnFacingDirectionChanged(Helpers.FacingDirection.EAST);
    }
    public void FaceWest()
    {
        OnFacingDirectionChanged(Helpers.FacingDirection.WEST);
    }
    
    public void ToIdle()
    {
        OnPlayerCurrentStateChanged(Helpers.PlayerCurrentState.IDLE);
    }
    public void ToWalk()
    {
        OnPlayerCurrentStateChanged(Helpers.PlayerCurrentState.WALKING);
    }
    public void ToRun()
    {
        OnPlayerCurrentStateChanged(Helpers.PlayerCurrentState.RUNNING);
    }


    #endregion
}
