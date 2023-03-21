using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public event EventHandler<EventArgsOnHealthChanged> OnHealthChanged;
    public class EventArgsOnHealthChanged : EventArgs
    {
        public int maxHealth;
    }

    [SerializeField]private GameObject cameraObject;
    [SerializeField]private Transform leftHand;
    [SerializeField]private Transform rightHand;
    [SerializeField]private LayerMask equippableLayerMask;

    private Weapon weapon;

    private int health;
    private int maxHealth;

    private float movementSpeed = 5f;
    private float mouseSensitivityX = 100f, mouseSensitivityY = 50f;
    private float yRotate = 0f, xRotate = 0, minAngle = -50, maxAngle = 50;

    private void Awake() 
    {
        Instance = this;
        health = 100;
        maxHealth = 100;    
    }

    private void Start() 
    {
        GameInput.Instance.OnShootPerformed += GameInput_OnShootPerformed;
        GameInput.Instance.OnInteractPerformed += GameInput_OnInteractPerformed;   
    }

    private void GameInput_OnInteractPerformed(object sender, EventArgs e)
    {
        float interactDistance = 5f;

        if(this.weapon == null)
        {
            if(Physics.Raycast(cameraObject.transform.position, cameraObject.transform.forward, out RaycastHit raycastHit, interactDistance, equippableLayerMask))
            {
                if(raycastHit.transform.TryGetComponent<Weapon>(out Weapon weapon))
                {
                    weapon.Equip(rightHand);
                    this.weapon = weapon;
                }
            }
        }
        else
        {
            this.weapon.Drop();
            weapon = null;
        }
    }

    private void GameInput_OnShootPerformed(object sender, EventArgs e)
    {
        if(weapon != null)
        {
            Gun gun = weapon as Gun;
            gun.Shoot();
        }
    }

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

        //rotate player in the direction of camera
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, xRotate, transform.eulerAngles.z);
    }

    private void HandleCameraMovement()
    {
        Vector2 mouseVector = GameInput.Instance.GetMouseMovementVectorNormalized();
        Vector3 rotateVector;

        xRotate += mouseVector.x * mouseSensitivityX * Time.deltaTime;
        yRotate += mouseVector.y * mouseSensitivityY * Time.deltaTime * (-1);

        //limit the camera rotation in the z axis
        yRotate = Mathf.Clamp(yRotate, minAngle, maxAngle);

        rotateVector = new Vector3(yRotate, xRotate, 0);
        cameraObject.transform.eulerAngles = rotateVector;
    }

    public void SetHealth(int health)
    {  
        if(this.health - health > 0)
        {
            this.health = health;

            OnHealthChanged?.Invoke(this, new EventArgsOnHealthChanged{
                maxHealth = maxHealth
            });
        }
    }

    public int GetHealth()
    {
        return health;
    }

    internal void SetHealth(object value)
    {
        throw new NotImplementedException();
    }
}
