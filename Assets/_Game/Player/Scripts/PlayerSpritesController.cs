using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpritesController : MonoBehaviour
{
    [Header("SO's")] 
    [SerializeField] private SPlayerEvents playerEvents;
    
    [Header("Base Sprite")] 
    [SerializeField] private SpriteRenderer _baseSpriteSpriteRenderer;
    [SerializeField] private SSpriteData _baseSpriteData;

    private Helpers.FacingDirection _facingDirection = Helpers.FacingDirection.SOUTH;
    private Helpers.PlayerCurrentState _playerCurrentState = Helpers.PlayerCurrentState.WALKING;
    
    private bool _canAnimate = true;

    private SpriteState baseSpriteState;
    private SpriteState bodySpriteState;
    private SpriteState headSpriteState;
    private SpriteState hatSpriteState;
    
    
    private void OnEnable()
    {
        playerEvents.FacingDirectionChanged += OnPlayerDirectionChanged;
        playerEvents.PlayerCurrentStateChanged += OnPlayerCurrentStateChanged;
    }

    private void OnDisable()
    {
        playerEvents.FacingDirectionChanged -= OnPlayerDirectionChanged;
        playerEvents.PlayerCurrentStateChanged -= OnPlayerCurrentStateChanged;
    }

    void Start()
    {
        //base
        List<Sprite> sprites = _baseSpriteData.GetCurrentStateSprites(_facingDirection, _playerCurrentState);
        baseSpriteState = new SpriteState(sprites, _baseSpriteData.GetSpriteUpdateRate());
    }
    
    void Update()
    {
        if (_canAnimate)
        {
            UpdateCurrentSprite();
        }
    }

    private void UpdateCurrentSprite()
    {
       UpdateBaseSprite();
    }

    private void UpdateBaseSprite()
    {
        baseSpriteState.time += Time.deltaTime;
        
        if (baseSpriteState.time >= baseSpriteState.updateRate)
        {
            _baseSpriteSpriteRenderer.sprite = baseSpriteState.sprites[baseSpriteState.currentSpriteIndex];

            baseSpriteState.time = 0;

            baseSpriteState.currentSpriteIndex++; 
            
            if (baseSpriteState.currentSpriteIndex > baseSpriteState.sprites.Count - 1)
                baseSpriteState.currentSpriteIndex = 0;
        }
    }

    private void OnPlayerDirectionChanged(Helpers.FacingDirection direction)
    {
        _facingDirection = direction;
        
        ChangeSpritesList(_facingDirection, _playerCurrentState);
    }
    
    private void OnPlayerCurrentStateChanged(Helpers.PlayerCurrentState state)
    {
        _playerCurrentState = state;
        
        ChangeSpritesList(_facingDirection, _playerCurrentState);
    }
    
    
    private void ChangeSpritesList(Helpers.FacingDirection facingDirection, Helpers.PlayerCurrentState state)
    {
        baseSpriteState.sprites = _baseSpriteData.GetCurrentStateSprites(facingDirection, state);
        baseSpriteState.Reset();
        _baseSpriteSpriteRenderer.sprite = baseSpriteState.sprites[0];
    }

    class SpriteState
    {
        public List<Sprite> sprites;
        public float updateRate;
        public int currentSpriteIndex;
        public float time;

        public SpriteState(List<Sprite> spritesList, float updateRate, int index = 0, float time = 0)
        {
            this.sprites = spritesList;
            this.updateRate = updateRate;
            this.currentSpriteIndex = index;
            this.time = time;
        }

        public void Reset()
        {
            this.currentSpriteIndex = sprites.Count > 1 ? 1 : 0;
            this.time = 0;
        }
    }
}
