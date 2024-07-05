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
        playerSpeed = _playerData.GetSpeed(false);
    }

    private void Update()
    {
        movementDirectionInput = _inputReader.GetInputDirection().normalized;
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector2 velocity = movementDirectionInput * (playerSpeed * Time.fixedDeltaTime);
        Debug.Log($"velocity: {velocity}");
        _rigidbody2D.velocity = velocity;
    }

    private void OnSprint(bool pressed)
    {
        playerSpeed = _playerData.GetSpeed(pressed);
    }
}
