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
    [SerializeField] private float _updateSpeed = 0.3f;
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

    public SpritesBlock GetSpriteBlockByDirection(Helpers.FacingDirection direction)
    {
        return spritesBlocks.Find(x => x.facingDirection == direction);
    }

    public float GetSpriteUpdateSpeed()
    {
        return _updateSpeed;
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
