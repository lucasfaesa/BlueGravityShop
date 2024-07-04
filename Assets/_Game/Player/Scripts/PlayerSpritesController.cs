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
    [SerializeField] private SSpriteData _baseSpriteDataSO;

    private Helpers.FacingDirection _facingDirection = Helpers.FacingDirection.SOUTH;
    private Helpers.PlayerCurrentState _playerCurrentState = Helpers.PlayerCurrentState.WALKING;
    
    private bool _canAnimate = true;

    private SpriteState _baseSpriteState;
    private SpriteState _bodySpriteState;
    private SpriteState _headSpriteState;
    private SpriteState _hatSpriteState;
    
    
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
        List<Sprite> sprites = _baseSpriteDataSO.GetCurrentStateSprites(_facingDirection, _playerCurrentState);
        _baseSpriteState = new SpriteState(sprites, _baseSpriteDataSO.GetSpriteUpdateRate(_playerCurrentState));
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
        _baseSpriteState.time += Time.deltaTime;
        
        if (_baseSpriteState.time >= _baseSpriteState.updateRate)
        {
            _baseSpriteSpriteRenderer.sprite = _baseSpriteState.sprites[_baseSpriteState.currentSpriteIndex];

            _baseSpriteState.time = 0;

            _baseSpriteState.currentSpriteIndex++; 
            
            if (_baseSpriteState.currentSpriteIndex > _baseSpriteState.sprites.Count - 1)
                _baseSpriteState.currentSpriteIndex = 0;
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
        _baseSpriteState.sprites = _baseSpriteDataSO.GetCurrentStateSprites(facingDirection, state);
        _baseSpriteState.Reset();
        _baseSpriteSpriteRenderer.sprite = _baseSpriteState.sprites[0];
        _baseSpriteState.updateRate = _baseSpriteDataSO.GetSpriteUpdateRate(state);
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
