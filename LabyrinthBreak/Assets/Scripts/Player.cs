using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameInput gameInput;
    private float movementSpeed = 5f;

    private void Update() 
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        transform.Translate(moveDir * movementSpeed * Time.deltaTime);
    }
    
}
