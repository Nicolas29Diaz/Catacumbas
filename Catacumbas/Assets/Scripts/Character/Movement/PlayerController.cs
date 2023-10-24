using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;

    [Header("General")]
    public float gravity = -9.81f;

    [Header("References")]
    public Camera playerCamera;


    [Header("Movement")]
    public float walkSpeed = 5f;
    public float runSpeed = 10f;

    [Header("Jump")]
    public float jumpForce = 8f;
    public float jumpCooldown = 1.0f;
    public bool readyToJump = true;
    private float verticalVelocity = 0;

    [Header("Rotation")]
    public float rotationSensitivity = 2.0f;
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 rotationInput = Vector3.zero;
    private float cameraVerticalAngle;


    
    

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        HandleMovement();
        HandleRotation();
        //RotationFlashLight();
    }

    private void HandleMovement()
    {
        float speed = Input.GetButton("Sprint") ? runSpeed : walkSpeed;

        if (characterController.isGrounded)
        {
            HandleJump();
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        moveDirection = transform.TransformDirection(new Vector3(horizontalInput, verticalVelocity, verticalInput) * speed);

        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && readyToJump)
        {
            readyToJump = false;
            verticalVelocity = jumpForce;
            Invoke(nameof(ResetJump), jumpCooldown);
        }
        else
        {
            verticalVelocity = -0.5f;
        }
    }

    private void HandleRotation()
    {
        rotationInput.x = Input.GetAxis("Mouse X") * rotationSensitivity;
        rotationInput.y = -Input.GetAxis("Mouse Y") * rotationSensitivity;

        // Gira el personaje horizontalmente
        transform.Rotate(Vector3.up * rotationInput.x);

        // Gira la cámara verticalmente
        cameraVerticalAngle += rotationInput.y;
        cameraVerticalAngle = Mathf.Clamp(cameraVerticalAngle, -70, 70);
        playerCamera.transform.localRotation = Quaternion.Euler(cameraVerticalAngle,0f,0f);

    }

    //private void RotationFlashLight()
    //{
    //    // Gira la cámara verticalmente
    //    flashLight.transform.localRotation = Quaternion.Euler(cameraVerticalAngle, 0f, 0f);
    //}

    private void ResetJump()
    {
        readyToJump = true;
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Battery"))
    //    {
    //        flashLight.CollectBattery();
    //        Destroy(other.gameObject); // Destruye el objeto de la batería
    //    }
    //}
}
