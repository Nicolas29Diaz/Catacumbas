using System;
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
    public float crouchSpeed = 3f;
    public float runCrouchSpeed = 4f;

    [Header("Stamina")]
    public float actualStamina;
    public float velRegainStamina = 1f;
    public float maxStamina = 50f;

    [Header("Jump")]
    public float jumpForce = 8f;
    public float jumpCooldown = 1.0f;
    private float verticalVelocity = 0;

    [Header("Rotation")]
    public float rotationSensitivity = 2.0f;
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 rotationInput = Vector3.zero;
    private float cameraVerticalAngle;

    [Header("Teclas")]
    public KeyCode crouchKey = KeyCode.LeftControl;
    public KeyCode jumpKey = KeyCode.LeftControl;
    public KeyCode runKey = KeyCode.LeftControl;

    private bool readyToJump = true;
    private bool upCollision;
    private bool crouching = false;
    private bool running = false;
    private bool canRun = true;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        actualStamina = maxStamina;
    }

    private void Update()
    {
        HandleMovement();
        HandleRotation();
        UpCollision();
        HandleCrouching();
        HandleStamina();
    }
 
    private float SpeedMovement()
    {
        float speed = walkSpeed;

        if (Input.GetButton("Sprint") && canRun)
        {
            if (crouching)
            {
                speed = runCrouchSpeed;
               
            }
            else
            {
                speed = runSpeed;
            }

            running = true;
        }
        else
        {
            if (crouching)
            {
                speed = crouchSpeed;
            }
            else
            {
                speed = walkSpeed;
            }

            running = false;
        }

        return speed;
    }
    private void HandleMovement()
    {
        float speed = SpeedMovement();


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
    private void ResetJump()
    {
        readyToJump = true;
    }

    private void HandleCrouching()
    {
        if (Input.GetKeyDown(crouchKey))
        {
            Crouching();
            crouching = true;
        }
        else if (Input.GetKeyUp(crouchKey) && upCollision)
        {
            Crouching();
            crouching = true;
        }
        else if (Input.GetKeyUp(crouchKey) && !upCollision)
        {
            NoCrouching();
            crouching = false;
        }
    }
    private void Crouching()
    {
        characterController.height = 1f;
        characterController.center = new Vector3(0, -0.5f, 0);
        playerCamera.transform.localPosition = new Vector3(0, 0.3f, 0);
    }
    private void NoCrouching()
    {
        characterController.height = 2f;
        characterController.center = new Vector3(0, 0, 0);
        playerCamera.transform.localPosition = new Vector3(0, 0.7f, 0);
    }

    private void HandleStamina()
    {
        if (running)
        {
            ConsumeStamina();
        }
        else
        {
            RegainStamina();
        }
    }
    private void ConsumeStamina()
    {
        if (actualStamina > 0)
        {
            actualStamina -= velRegainStamina * Time.deltaTime;
        }
        else
        {
            canRun = false;
        }
        
    }
    private void RegainStamina()
    {
        if (actualStamina <= maxStamina)
        {
            actualStamina += velRegainStamina * Time.deltaTime;
        }
        else
        {
            canRun = true;
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
        playerCamera.transform.localRotation = Quaternion.Euler(cameraVerticalAngle, 0f, 0f);

    }

    private void UpCollision()
    {
        RaycastHit hit;

        Vector3 upDirection = playerCamera.transform.up; // Obtiene la dirección "arriba" de la cámara

        if (Physics.Raycast(playerCamera.transform.position, upDirection, out hit, 0.5f))
        {
            upCollision = true;
            readyToJump = false;

        }
        else
        {
            upCollision = false;
            readyToJump = true;
        }
    }
}
