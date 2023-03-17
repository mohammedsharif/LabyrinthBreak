using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance {get; private set;}

    private PlayerInputActions playerInputActions;

    private void Awake() 
    {
        Instance = this;

        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();
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

        Debug.Log(mouseVector);
        return mouseVector;
    }
}
