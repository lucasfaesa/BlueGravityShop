using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helpers
{
    public enum EquipmentType
    {
        BASE,
        HEAD,
        BODY,
        HAT
    };

    public enum FacingDirection
    {
        NORTH,
        SOUTH,
        EAST,
        WEST
    }

    public enum PlayerCurrentState
    {
        IDLE,
        WALKING,
        RUNNING
    }
}
