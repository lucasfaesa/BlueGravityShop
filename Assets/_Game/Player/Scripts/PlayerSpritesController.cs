using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpritesController : MonoBehaviour
{
    [Header("SO's")] 
    [SerializeField] private SPlayerEvents playerEvents;
    
    [Header("Base Related")] 
    [SerializeField] private SpriteRenderer _baseSpriteSpriteRenderer;
    [SerializeField] private SSpriteData _baseSpriteDataSO;
    [Header("Body Related")] 
    [SerializeField] private SpriteRenderer _bodySpriteSpriteRenderer;
    [SerializeField] private SSpriteData _equippedBodySpriteDataSO; //TODO remove from serialized field
    [Header("Head Related")] 
    [SerializeField] private SpriteRenderer _headSpriteSpriteRenderer;
    [SerializeField] private SSpriteData _equippedHeadSpriteDataSO;
    [Header("Hat Related")] 
    [SerializeField] private SpriteRenderer _hatSpriteSpriteRenderer;
    [SerializeField] private SSpriteData _equippedHatSpriteDataSO;

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
        List<Sprite> baseSprites = _baseSpriteDataSO.GetCurrentStateSprites(_facingDirection, _playerCurrentState);
        _baseSpriteState = new SpriteState(baseSprites, _baseSpriteDataSO.GetSpriteUpdateRate(_playerCurrentState));
        
        //TODO REMOVE LATER
        List<Sprite> bodySprites = _equippedBodySpriteDataSO.GetCurrentStateSprites(_facingDirection, _playerCurrentState);
        _bodySpriteState = new SpriteState(bodySprites, _baseSpriteDataSO.GetSpriteUpdateRate(_playerCurrentState));
        
        List<Sprite> headSprites = _equippedHeadSpriteDataSO.GetCurrentStateSprites(_facingDirection, _playerCurrentState);
        _headSpriteState = new SpriteState(headSprites, _baseSpriteDataSO.GetSpriteUpdateRate(_playerCurrentState));
        
        List<Sprite> hatSprites = _equippedHatSpriteDataSO.GetCurrentStateSprites(_facingDirection, _playerCurrentState);
        _hatSpriteState = new SpriteState(hatSprites, _baseSpriteDataSO.GetSpriteUpdateRate(_playerCurrentState));
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
       UpdateBodySprite();
    }

    private void UpdateBaseSprite()
    {
        _baseSpriteState.Time += Time.deltaTime;
        
        if (_baseSpriteState.Time >= _baseSpriteState.UpdateRate)
        {
            _baseSpriteSpriteRenderer.sprite = _baseSpriteState.Sprites[_baseSpriteState.CurrentSpriteIndex];

            _baseSpriteState.Time = 0;

            _baseSpriteState.CurrentSpriteIndex++; 
            
            if (_baseSpriteState.CurrentSpriteIndex > _baseSpriteState.Sprites.Count - 1)
                _baseSpriteState.CurrentSpriteIndex = 0;
        }
    }

    private void UpdateBodySprite()
    {
        _bodySpriteState.Time += Time.deltaTime;
        
        if (_bodySpriteState.Time >= _bodySpriteState.UpdateRate)
        {
            _bodySpriteSpriteRenderer.sprite = _bodySpriteState.Sprites[_bodySpriteState.CurrentSpriteIndex];

            _bodySpriteState.Time = 0;

            _bodySpriteState.CurrentSpriteIndex++; 
            
            if (_bodySpriteState.CurrentSpriteIndex > _bodySpriteState.Sprites.Count - 1)
                _bodySpriteState.CurrentSpriteIndex = 0;
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
        _baseSpriteState.Sprites = _baseSpriteDataSO.GetCurrentStateSprites(facingDirection, state);
        _baseSpriteState.Reset();
        _baseSpriteSpriteRenderer.sprite = _baseSpriteState.Sprites[0];
        _baseSpriteState.UpdateRate = _baseSpriteDataSO.GetSpriteUpdateRate(state);
        
        _bodySpriteState.Sprites = _equippedBodySpriteDataSO.GetCurrentStateSprites(facingDirection, state);
        _bodySpriteState.Reset();
        _bodySpriteSpriteRenderer.sprite = _bodySpriteState.Sprites[0];
        _bodySpriteState.UpdateRate = _equippedBodySpriteDataSO.GetSpriteUpdateRate(state);
    }

    class SpriteState
    {
        public bool IsActive { get; set; }
        public List<Sprite> Sprites { get; set; }
        public float UpdateRate { get; set; }
        public int CurrentSpriteIndex { get; set; }
        public float Time { get; set; }

        public SpriteState(List<Sprite> spritesList, float updateRate, bool isActive = true, int index = 0, float time = 0)
        {
            this.Sprites = spritesList;
            this.UpdateRate = updateRate;
            this.CurrentSpriteIndex = index;
            this.Time = time;
            this.IsActive = true;
        }

        public void Reset()
        {
            this.CurrentSpriteIndex = Sprites.Count > 1 ? 1 : 0;
            this.Time = 0;
        }
    }
}
