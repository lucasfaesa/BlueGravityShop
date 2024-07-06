using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReader", menuName = "ScriptableObjects/Input/InputReader")]
public class SInputReader : ScriptableObject
{
    [Header("Input Actions")]
    [SerializeField] private InputActionReference moveReference;
    [SerializeField] private InputActionReference interactReference;
    [SerializeField] private InputActionReference sprintReference;
    [SerializeField] private InputActionReference inventoryReference;
    [SerializeField] private InputActionReference characterReference;
    [SerializeField] private InputActionReference quitGameReference;
    [SerializeField] private InputActionReference enterReference;
    
    public event Action<bool> Interact;
    public event Action<bool> Sprint;
    public event Action<bool> Inventory;
    public event Action<bool> Character;
    public event Action<bool> QuitGame;
    public event Action<bool> Enter; 

    public Vector2 GetInputDirection()
    {
        return moveReference.action.ReadValue<Vector2>();
    }
    
    public void EnableInputActions()
    {
        moveReference.action.Enable();
        interactReference.action.Enable();
        sprintReference.action.Enable();
        inventoryReference.action.Enable();
        characterReference.action.Enable();
        quitGameReference.action.Enable();
        enterReference.action.Enable();
        AddListeners();
    }
    
    public void DisableInputActions()
    {
        moveReference.action.Disable();
        interactReference.action.Disable();
        sprintReference.action.Disable();
        inventoryReference.action.Disable();
        characterReference.action.Disable();
        quitGameReference.action.Disable();
        enterReference.action.Disable();
        RemoveListeners();
    }

    private void AddListeners()
    {
        interactReference.action.performed += OnInteractPerformed;
        sprintReference.action.performed += OnSprintPerformed;
        sprintReference.action.canceled += OnSprintCanceled;
        inventoryReference.action.performed += OnInventoryPerformed;
        characterReference.action.performed += OnCharacterPerformed;
        quitGameReference.action.performed += OnQuitGamePerformed;
        enterReference.action.performed += OnEnterPerformed;
    }

    private void RemoveListeners()
    {
        interactReference.action.performed -= OnInteractPerformed;
        sprintReference.action.performed -= OnSprintPerformed;
        sprintReference.action.canceled -= OnSprintCanceled;
        inventoryReference.action.performed -= OnInventoryPerformed;
        characterReference.action.performed -= OnCharacterPerformed;
        quitGameReference.action.performed -= OnQuitGamePerformed;
        enterReference.action.performed -= OnEnterPerformed;
    }

    private void OnInteractPerformed(InputAction.CallbackContext callbackContext)
    {
        Interact?.Invoke(callbackContext.ReadValueAsButton());
    }
    
    private void OnSprintPerformed(InputAction.CallbackContext callbackContext)
    {
        Sprint?.Invoke(callbackContext.ReadValueAsButton());
    }
    
    private void OnSprintCanceled(InputAction.CallbackContext callbackContext)
    {
        Sprint?.Invoke(callbackContext.ReadValueAsButton());
    }
    
    private void OnInventoryPerformed(InputAction.CallbackContext callbackContext)
    {
        Inventory?.Invoke(callbackContext.ReadValueAsButton());
    }
    
    private void OnCharacterPerformed(InputAction.CallbackContext callbackContext)
    {
        Character?.Invoke(callbackContext.ReadValueAsButton());
    }
    
    private void OnQuitGamePerformed(InputAction.CallbackContext callbackContext)
    {
        QuitGame?.Invoke(callbackContext.ReadValueAsButton());
    }

    private void OnEnterPerformed(InputAction.CallbackContext callbackContext)
    {
        Enter?.Invoke(callbackContext.ReadValueAsButton());
    }
}
