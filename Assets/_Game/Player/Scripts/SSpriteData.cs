using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[CreateAssetMenu(fileName = "SpriteData", menuName = "ScriptableObjects/Sprite/SpriteData")]
public class SSpriteData : ScriptableObject
{
    [SerializeField] private Helpers.EquipmentType _equipmentType = Helpers.EquipmentType.BASE;
    [Space]
    [SerializeField] private float _idleUpdateRate = 1f;
    [SerializeField] private float _walkUpdateRate = 0.15f;
    [SerializeField] private float _runUpdateRate = 0.2f;
    [Space]
    [SerializeField] private List<SpritesBlock> spritesBlocks = new();

    public Helpers.EquipmentType GetEquipmentType()
    {
        return _equipmentType;
    }

    public List<SpritesBlock> GetSpritesBlockList()
    {
        return spritesBlocks;
    }

    public List<Sprite> GetCurrentStateSprites(Helpers.FacingDirection direction, Helpers.PlayerCurrentState state)
    {
        switch (state)
        {
            case Helpers.PlayerCurrentState.IDLE:
                return GetSpriteBlockByDirection(direction).idleSprites;
            break;
            case Helpers.PlayerCurrentState.WALKING:
                return GetSpriteBlockByDirection(direction).walkingSprites;
            break;
            case Helpers.PlayerCurrentState.RUNNING:
                return GetSpriteBlockByDirection(direction).runningSprites;
            break;

            default:
                Debug.LogError("Error, trying to get inexistent state");
                return GetSpriteBlockByDirection(direction).idleSprites;
            break;
                
        }
    }

    public SpritesBlock GetSpriteBlockByDirection(Helpers.FacingDirection direction)
    {
        return spritesBlocks.Find(x => x.facingDirection == direction);
    }

    public float GetSpriteUpdateRate(Helpers.PlayerCurrentState state)
    {
        
        switch (state)
        {
            case Helpers.PlayerCurrentState.IDLE:
                return _idleUpdateRate;
                break;
            case Helpers.PlayerCurrentState.WALKING:
                return _walkUpdateRate;
                break;
            case Helpers.PlayerCurrentState.RUNNING:
                return _runUpdateRate;
                break;

            default:
                Debug.LogError("Error, trying to get inexistent state");
                return _idleUpdateRate;
                break;
        }
    }


    [Serializable]
    public struct SpritesBlock
    {
        public Helpers.FacingDirection facingDirection;
        public List<Sprite> idleSprites;
        public List<Sprite> walkingSprites;
        public List<Sprite> runningSprites;
    }
    
}
