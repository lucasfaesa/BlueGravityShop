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
        
    private Helpers.FacingDirection _newDir;
    private Helpers.PlayerCurrentState _newState;
    private bool _isSprinting;
    
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

        _newDir = _currentFacingDirection;
        
        playerSpeed = _playerData.GetSpeed(false);
    }

    private void Update()
    {
        movementDirectionInput = _inputReader.GetInputDirection().normalized;
        
        UpdateFacingDirection();
        UpdateState();
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
        if (movementDirectionInput.x > 0)
            _newDir = Helpers.FacingDirection.EAST;
        if(movementDirectionInput.x < 0)
            _newDir = Helpers.FacingDirection.WEST;
        
        //y facing dir always in priority
        if (movementDirectionInput.y > 0)
            _newDir = Helpers.FacingDirection.NORTH;
        
        if(movementDirectionInput.y < 0)
            _newDir = Helpers.FacingDirection.SOUTH;

        if (_newDir != _currentFacingDirection)
        {
            _currentFacingDirection = _newDir;
            _playerEvents.OnFacingDirectionChanged(_currentFacingDirection);
        }
    }

    private void UpdateState()
    {
        if (movementDirectionInput.magnitude == 0)
            _newState = Helpers.PlayerCurrentState.IDLE;

        else if (_isSprinting)
            _newState = Helpers.PlayerCurrentState.RUNNING;
        else
            _newState = Helpers.PlayerCurrentState.WALKING;

        if (_newState != _playerCurrentState)
        {
            _playerCurrentState = _newState;
            _playerEvents.OnPlayerCurrentStateChanged(_playerCurrentState);
        }
    }

    private void OnSprint(bool pressed)
    {
        _isSprinting = pressed;
        playerSpeed = _playerData.GetSpeed(pressed);
    }
}
