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
            
            case Helpers.PlayerCurrentState.WALKING:
                return GetSpriteBlockByDirection(direction).walkingSprites;
            
            case Helpers.PlayerCurrentState.RUNNING:
                return GetSpriteBlockByDirection(direction).runningSprites;
            

            default:
                Debug.LogError("Error, trying to get inexistent state");
                return GetSpriteBlockByDirection(direction).idleSprites;
            
                
        }
    }

    public SpritesBlock GetSpriteBlockByDirection(Helpers.FacingDirection direction)
    {
        //TODO remove later
        var ok = spritesBlocks.Find(x => x.facingDirection == direction);
        return ok;
    }

    public float GetSpriteUpdateRate(Helpers.PlayerCurrentState state)
    {
        
        switch (state)
        {
            case Helpers.PlayerCurrentState.IDLE:
                return _idleUpdateRate;
                
            case Helpers.PlayerCurrentState.WALKING:
                return _walkUpdateRate;
                
            case Helpers.PlayerCurrentState.RUNNING:
                return _runUpdateRate;
                

            default:
                Debug.LogError("Error, trying to get inexistent state");
                return _idleUpdateRate;
                
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
