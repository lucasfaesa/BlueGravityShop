using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpritesController : MonoBehaviour
{
    [Header("SO's")] 
    [SerializeField] private SPlayerEvents playerEvents;
    [SerializeField] private SEquipmentEvents equipmentEvents;
    
    [Header("Base Related")] 
    [SerializeField] private SpriteRenderer _baseSpriteSpriteRenderer;
    [SerializeField] private SEquipmentData equippedBaseEquipmentDataSo;
    
    [Header("Body Related")] 
    [SerializeField] private SpriteRenderer _bodySpriteSpriteRenderer;
    [Header("Head Related")] 
    [SerializeField] private SpriteRenderer _headSpriteSpriteRenderer;
    [Header("Hat Related")] 
    [SerializeField] private SpriteRenderer _hatSpriteSpriteRenderer;
    

    private Helpers.FacingDirection _facingDirection = Helpers.FacingDirection.SOUTH;
    private Helpers.PlayerCurrentState _playerCurrentState = Helpers.PlayerCurrentState.WALKING;
    
    private bool _canAnimate = true;

    private SEquipmentData _equippedBodyEquipmentDataSo;
    private SEquipmentData _equippedHeadEquipmentDataSo;
    private SEquipmentData _equippedHatEquipmentDataSo;
    
    private SpriteAnimation _baseSpriteAnimation = new SpriteAnimation();
    private SpriteAnimation _bodySpriteAnimation = new SpriteAnimation();
    private SpriteAnimation _headSpriteAnimation = new SpriteAnimation();
    private SpriteAnimation _hatSpriteAnimation = new SpriteAnimation();
    
    
    private void OnEnable()
    {
        playerEvents.FacingDirectionChanged += OnPlayerDirectionChanged;
        playerEvents.PlayerCurrentStateChanged += OnPlayerCurrentStateChanged;
        equipmentEvents.CurrentEquipmentChanged += OnEquipmentChanged;
        equipmentEvents.CurrentEquipmentRemoved += OnEquipmentRemoved;
    }

    private void OnDisable()
    {
        playerEvents.FacingDirectionChanged -= OnPlayerDirectionChanged;
        playerEvents.PlayerCurrentStateChanged -= OnPlayerCurrentStateChanged;
        equipmentEvents.CurrentEquipmentChanged -= OnEquipmentChanged;
        equipmentEvents.CurrentEquipmentRemoved -= OnEquipmentRemoved;
    }

    void Start()
    {
        //base starts equipped
        _baseSpriteAnimation = new SpriteAnimation(equippedBaseEquipmentDataSo, _facingDirection, _playerCurrentState);
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
            _baseSpriteAnimation.SetData(_baseSpriteSpriteRenderer, equippedBaseEquipmentDataSo, facingDirection, state);
        
        if(_bodySpriteAnimation.IsActive)
            _bodySpriteAnimation.SetData(_bodySpriteSpriteRenderer, _equippedBodyEquipmentDataSo, facingDirection, state);
        
        if(_headSpriteAnimation.IsActive)
            _headSpriteAnimation.SetData(_headSpriteSpriteRenderer, _equippedHeadEquipmentDataSo, facingDirection, state);
        
        if(_hatSpriteAnimation.IsActive)
            _hatSpriteAnimation.SetData(_hatSpriteSpriteRenderer, _equippedHatEquipmentDataSo, facingDirection, state);
    }

    private void OnEquipmentChanged(SEquipmentData equipmentData)
    {
        switch (equipmentData.GetEquipmentType())
        {
            case Helpers.EquipmentType.BASE:
                equippedBaseEquipmentDataSo = equipmentData;
                UpdateSprites(_baseSpriteSpriteRenderer, equipmentData, _baseSpriteAnimation);
                break;
            case Helpers.EquipmentType.BODY:
                _equippedBodyEquipmentDataSo = equipmentData;
                UpdateSprites(_bodySpriteSpriteRenderer, equipmentData, _bodySpriteAnimation);
                break;
            case Helpers.EquipmentType.HEAD:
                _equippedHeadEquipmentDataSo = equipmentData;
                UpdateSprites(_headSpriteSpriteRenderer, equipmentData, _headSpriteAnimation);
                break;
            case Helpers.EquipmentType.HAT:
                _equippedHatEquipmentDataSo = equipmentData;
                UpdateSprites(_hatSpriteSpriteRenderer, equipmentData, _hatSpriteAnimation);
                break;
        }
        
        ResetAllSpritesToBeginning();
    }

    private void UpdateSprites(SpriteRenderer rend, SEquipmentData data, SpriteAnimation spriteAnimation)
    {
        if (!_baseSpriteAnimation.IsInitialized)
            spriteAnimation = new SpriteAnimation(data, _facingDirection, _playerCurrentState);
        else
            spriteAnimation.SetData(rend, data, _facingDirection, _playerCurrentState);
    }

    private void ResetAllSpritesToBeginning()
    {
        if(_baseSpriteAnimation.IsActive)
            _baseSpriteAnimation.Reset(_baseSpriteSpriteRenderer);
        
        if(_bodySpriteAnimation.IsActive)
            _bodySpriteAnimation.Reset(_bodySpriteSpriteRenderer);
        
        if(_headSpriteAnimation.IsActive)
            _headSpriteAnimation.Reset(_headSpriteSpriteRenderer);
        
        if(_hatSpriteAnimation.IsActive)
            _hatSpriteAnimation.Reset(_hatSpriteSpriteRenderer);
    }

    private void OnEquipmentRemoved(Helpers.EquipmentType type)
    {
        switch (type)
        {
            case Helpers.EquipmentType.BASE:
                _baseSpriteAnimation.Deactivate(_baseSpriteSpriteRenderer);
                break;
            case Helpers.EquipmentType.BODY:
                _bodySpriteAnimation.Deactivate(_bodySpriteSpriteRenderer);
                break;
            case Helpers.EquipmentType.HEAD:
                _headSpriteAnimation.Deactivate(_headSpriteSpriteRenderer);
                break;
            case Helpers.EquipmentType.HAT:
                _hatSpriteAnimation.Deactivate(_hatSpriteSpriteRenderer);
                break;
            default:
                Debug.LogError("Nonexistent equipment type");
                break;
        }
    }
    
    class SpriteAnimation
    {
        public bool IsInitialized { get; set; } = false;
        public bool IsActive { get; set; } = false;
        public List<Sprite> Sprites { get; set; }
        public float UpdateRate { get; set; }
        public int CurrentSpriteIndex { get; set; }
        public float Time { get; set; }

        public SpriteAnimation(SEquipmentData data, Helpers.FacingDirection dir, Helpers.PlayerCurrentState state, 
                                    bool isActive = true, int index = 0, float time = 0)
        {
            this.Sprites = data.GetCurrentStateSprites(dir, state);
            this.UpdateRate = data.GetSpriteUpdateRate(state);
            this.CurrentSpriteIndex = index;
            this.Time = time;
            this.IsInitialized = true;
            this.IsActive = true;
        }

        public SpriteAnimation() { }

        public void SetData(SpriteRenderer spriteRenderer, SEquipmentData equipmentDataSo, Helpers.FacingDirection facingDirection,
                                Helpers.PlayerCurrentState playerState)
        {
            Sprites = equipmentDataSo.GetCurrentStateSprites(facingDirection, playerState);
            UpdateRate = equipmentDataSo.GetSpriteUpdateRate(playerState);
            Reset(spriteRenderer);
            this.IsActive = true;
        }

        public void Deactivate(SpriteRenderer renderer)
        {
            IsActive = false;
            renderer.sprite = null;
        }
        
        public void Reset(SpriteRenderer spriteRenderer)
        {
            spriteRenderer.sprite = Sprites[0];
            this.CurrentSpriteIndex = Sprites.Count > 1 ? 1 : 0;
            this.Time = 0;
        }
    }
}
