using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[CreateAssetMenu(fileName = "SpriteData", menuName = "ScriptableObjects/Sprite/SpriteData")]
public class SEquipmentData : ScriptableObject
{
    [Header("Item Settings")]
    [SerializeField] private Helpers.EquipmentType _equipmentType = Helpers.EquipmentType.BASE;
    [SerializeField] private Sprite equipmentThumbnail;
    [SerializeField] private string equipmentName;
    [SerializeField] private int equipmentBuyValue;
    [Range(10, 100)]
    [SerializeField] private int valueDepreciationPercent;
    
    [Header("Animation Settings")]
    [SerializeField] private float _idleUpdateRate = 1f;
    [SerializeField] private float _walkUpdateRate = 0.15f;
    [SerializeField] private float _runUpdateRate = 0.2f;
    [Space]
    [SerializeField] private List<SpritesBlock> spritesBlocks = new();

    public Helpers.EquipmentType GetEquipmentType()
    {
        return _equipmentType;
    }

    public Sprite GetEquipmentThumbnail()
    {
        return equipmentThumbnail;
    }
    public string GetEquipmentName()
    {
        return equipmentName;
    }
    
    public int GetEquipmentBuyValue()
    {
        return equipmentBuyValue;
    }
    
    public int GetEquipmentDepreciatedValue()
    {
        float depreciationRateDecimal = valueDepreciationPercent / 100.0f;
        int finalValue = (int)(equipmentBuyValue * (1 - depreciationRateDecimal));
        
        return finalValue;
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
