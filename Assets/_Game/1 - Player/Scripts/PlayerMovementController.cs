using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [Header("SOs")]
    [SerializeField] private SPlayerEvents _playerEvents;
    [SerializeField] private SInputReader _inputReader;
    [SerializeField] private SPlayerData _playerData;
    
    [Header("Components")]
    [SerializeField] private Rigidbody2D _rigidbody2D;

    private Vector2 movementDirectionInput;

    private float playerSpeed;

    private Helpers.FacingDirection _currentFacingDirection;
    private Helpers.PlayerCurrentState _playerCurrentState;
        
    private void OnEnable()
    {
        _inputReader.EnableInputActions();
        _inputReader.Sprint += OnSprint;
    }

    private void OnDisable()
    {
        _inputReader.DisableInputActions();
        _inputReader.Sprint -= OnSprint;
    }

    private void Start()
    {
        _currentFacingDirection = _playerData.GetFacingDirection();
        _playerCurrentState = _playerData.GetCurrentState();
        
        playerSpeed = _playerData.GetSpeed(false);
    }

    private void Update()
    {
        movementDirectionInput = _inputReader.GetInputDirection().normalized;
        
        UpdateFacingDirection();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector2 velocity = movementDirectionInput * (playerSpeed * Time.fixedDeltaTime);
        
        _rigidbody2D.velocity = velocity;
    }

    private void UpdateFacingDirection()
    {
        Helpers.FacingDirection newDir = _currentFacingDirection;

        if (movementDirectionInput.x > 0)
            newDir = Helpers.FacingDirection.EAST;
        if(movementDirectionInput.x < 0)
            newDir = Helpers.FacingDirection.WEST;
        
        //y facing dir always in priority
        if (movementDirectionInput.y > 0)
            newDir = Helpers.FacingDirection.NORTH;
        
        if(movementDirectionInput.y < 0)
            newDir = Helpers.FacingDirection.SOUTH;

        if (newDir != _currentFacingDirection)
        {
            _currentFacingDirection = newDir;
            _playerEvents.OnFacingDirectionChanged(_currentFacingDirection);
        }
    }

    private void UpdateState()
    {
        
    }

    private void OnSprint(bool pressed)
    {
        playerSpeed = _playerData.GetSpeed(pressed);
    }
}
