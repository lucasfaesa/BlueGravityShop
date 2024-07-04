using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerEvents", menuName = "ScriptableObjects/Player/PlayerEvents")]
public class SPlayerEvents : ScriptableObject
{
    public event Action<Helpers.FacingDirection> FacingDirectionChanged;
    public event Action<Helpers.PlayerCurrentState> PlayerCurrentStateChanged;

    public void OnFacingDirectionChanged(Helpers.FacingDirection direction)
    {
        FacingDirectionChanged?.Invoke(direction);
    }
    
    public void OnPlayerCurrentStateChanged(Helpers.PlayerCurrentState state)
    {
        PlayerCurrentStateChanged?.Invoke(state);
    }
}
