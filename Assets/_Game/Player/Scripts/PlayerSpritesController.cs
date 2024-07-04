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

    private SpriteAnimation _baseSpriteAnimation;
    private SpriteAnimation _bodySpriteAnimation;
    private SpriteAnimation _headSpriteAnimation;
    private SpriteAnimation _hatSpriteAnimation;
    
    
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
        _baseSpriteAnimation = new SpriteAnimation(baseSprites, _baseSpriteDataSO.GetSpriteUpdateRate(_playerCurrentState));
        
        //TODO REMOVE LATER
        List<Sprite> bodySprites = _equippedBodySpriteDataSO.GetCurrentStateSprites(_facingDirection, _playerCurrentState);
        _bodySpriteAnimation = new SpriteAnimation(bodySprites, _baseSpriteDataSO.GetSpriteUpdateRate(_playerCurrentState));
        
        List<Sprite> headSprites = _equippedHeadSpriteDataSO.GetCurrentStateSprites(_facingDirection, _playerCurrentState);
        _headSpriteAnimation = new SpriteAnimation(headSprites, _baseSpriteDataSO.GetSpriteUpdateRate(_playerCurrentState));
        
        List<Sprite> hatSprites = _equippedHatSpriteDataSO.GetCurrentStateSprites(_facingDirection, _playerCurrentState);
        _hatSpriteAnimation = new SpriteAnimation(hatSprites, _baseSpriteDataSO.GetSpriteUpdateRate(_playerCurrentState));
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
       UpdateSprite(_baseSpriteAnimation, _baseSpriteSpriteRenderer);
       UpdateSprite(_bodySpriteAnimation, _bodySpriteSpriteRenderer);
       UpdateSprite(_headSpriteAnimation, _headSpriteSpriteRenderer);
       UpdateSprite(_hatSpriteAnimation, _hatSpriteSpriteRenderer);
    }

    private void UpdateSprite(SpriteAnimation spriteAnimation, SpriteRenderer spriteRenderer)
    {
        if (!spriteAnimation.IsActive) return;
        
        spriteAnimation.Time += Time.deltaTime;
        
        if (spriteAnimation.Time >= spriteAnimation.UpdateRate)
        {
            spriteRenderer.sprite = spriteAnimation.Sprites[spriteAnimation.CurrentSpriteIndex];

            spriteAnimation.Time = 0;

            spriteAnimation.CurrentSpriteIndex++; 
            
            spriteAnimation.CurrentSpriteIndex %= spriteAnimation.Sprites.Count;
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
        if(_baseSpriteAnimation.IsActive)
            _baseSpriteAnimation.SetData(_baseSpriteSpriteRenderer, _baseSpriteDataSO, facingDirection, state);
        
        if(_bodySpriteAnimation.IsActive)
            _bodySpriteAnimation.SetData(_bodySpriteSpriteRenderer, _equippedBodySpriteDataSO, facingDirection, state);
        
        if(_headSpriteAnimation.IsActive)
            _headSpriteAnimation.SetData(_headSpriteSpriteRenderer, _equippedHeadSpriteDataSO, facingDirection, state);
        
        if(_hatSpriteAnimation.IsActive)
            _hatSpriteAnimation.SetData(_hatSpriteSpriteRenderer, _equippedHatSpriteDataSO, facingDirection, state);
    }

    class SpriteAnimation
    {
        public bool IsActive { get; set; } = false;
        public List<Sprite> Sprites { get; set; }
        public float UpdateRate { get; set; }
        public int CurrentSpriteIndex { get; set; }
        public float Time { get; set; }

        public SpriteAnimation(List<Sprite> spritesList, float updateRate, bool isActive = true, int index = 0, float time = 0)
        {
            this.Sprites = spritesList;
            this.UpdateRate = updateRate;
            this.CurrentSpriteIndex = index;
            this.Time = time;
            this.IsActive = true;
        }

        public void SetData(SpriteRenderer spriteRenderer, SSpriteData spriteDataSO, Helpers.FacingDirection facingDirection,
                                Helpers.PlayerCurrentState playerState)
        {
            if (!IsActive) return;
        
            Sprites = spriteDataSO.GetCurrentStateSprites(facingDirection, playerState);
            Reset();
            spriteRenderer.sprite = Sprites[0];
            UpdateRate = spriteDataSO.GetSpriteUpdateRate(playerState);
        }
        
        public void Reset()
        {
            this.CurrentSpriteIndex = Sprites.Count > 1 ? 1 : 0;
            this.Time = 0;
        }
    }
}
