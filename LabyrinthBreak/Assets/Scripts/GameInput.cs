using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance {get; private set;}

    public event EventHandler OnShootPerformed;
    public event EventHandler OnInteractPerformed;  

    private PlayerInputActions playerInputActions;

    private void Awake() 
    {
        Instance = this;

        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();

        playerInputActions.Player.Shoot.performed += PlayerShoot_Performed;
        playerInputActions.Player.Interact.performed += PlayerInteract_Performed;
    }

    private void PlayerInteract_Performed(InputAction.CallbackContext obj)
    {
        OnInteractPerformed?.Invoke(this, EventArgs.Empty);
    }

    private void PlayerShoot_Performed(InputAction.CallbackContext obj)
    {
        OnShootPerformed?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector;
        
        inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;

        return inputVector;
    }

    public Vector2 GetMouseMovementVectorNormalized()
    {
        Vector2 mouseVector;

        mouseVector = playerInputActions.Player.Look.ReadValue<Vector2>();

        mouseVector = mouseVector.normalized;

        return mouseVector;
    }
}
