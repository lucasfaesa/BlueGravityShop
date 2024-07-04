using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpritesController : MonoBehaviour
{
    [Header("Base Sprite")] 
    [SerializeField] private SpriteRenderer _baseSpriteSpriteRenderer;
    [SerializeField] private SSpriteData _baseSpriteData;

    private Helpers.FacingDirection _facingDirection = Helpers.FacingDirection.SOUTH;
    private Helpers.PlayerCurrentState _playerCurrentState = Helpers.PlayerCurrentState.WALKING;
    
    private float _spriteUpdateRate = 0.3f;

    private float _time = 0f;
    private bool _canAnimate = true;

    private int _currentSprite = 0;

    private List<Sprite> baseSprites = new();
    
    // Start is called before the first frame update
    void Start()
    {
        baseSprites = _baseSpriteData.GetCurrentStateSprites(_facingDirection, _playerCurrentState);
    }

    // Update is called once per frame
    void Update()
    {
        if (_canAnimate)
        {
            UpdateCurrentSprite();
        }
    }

    private void UpdateCurrentSprite()
    {
        _time += Time.deltaTime;

        if (_time >= _baseSpriteData.GetSpriteUpdateRate())
        {

            _baseSpriteSpriteRenderer.sprite = baseSprites[_currentSprite];
            
            _time = 0;

            _currentSprite++;
            
            if (_currentSprite > baseSprites.Count - 1)
                _currentSprite = 0;
        }
        
    }
    
    
    
    private void ChangeSpritesList(Helpers.FacingDirection facingDirection, Helpers.PlayerCurrentState state)
    {
        baseSprites = _baseSpriteData.GetCurrentStateSprites(_facingDirection, _playerCurrentState);
    }
    
    


    #region Debug Methods

    

    #endregion
}
