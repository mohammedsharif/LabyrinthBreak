using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]private GameObject cameraObject;

    private float movementSpeed = 5f;
    private float mouseSensitivityX = 100f, mouseSensitivityY = 100f;
    private float yRotate = 0f, xRotate = 0, minAngle = -30, maxAngle = 30;

    private void Update() 
    {
        HandleMovement();
        HandleCameraMovement();
    }

    private void HandleMovement()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalized();
        Vector3 forwardVector = new Vector3(0,0, cameraObject.transform.position.z);

        transform.Translate(Vector3.forward * inputVector.y * movementSpeed * Time.deltaTime);
        transform.Translate(Vector3.right * inputVector.x * movementSpeed * Time.deltaTime);
    }

    private void HandleCameraMovement()
    {
        Vector2 mouseVector = GameInput.Instance.GetMouseMovementVectorNormalized();
        Vector3 rotateVector;

        xRotate += mouseVector.x * mouseSensitivityX * Time.deltaTime;
        yRotate += mouseVector.y * mouseSensitivityY * Time.deltaTime;

        //limit the camera rotation in the z axis
        yRotate = Mathf.Clamp(yRotate, minAngle, maxAngle);

        rotateVector = new Vector3(yRotate, xRotate, 0);
        cameraObject.transform.eulerAngles = rotateVector;

        //rotate player in the direction of camera
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, xRotate, transform.eulerAngles.z);
    }
    
}
