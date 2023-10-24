using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class HeadBobbing : MonoBehaviour
{
    private CharacterController characterController;
    public GameObject cameraObj;

    public float horizontalInput;
    public float verticalInput;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if ((horizontalInput != 0 || verticalInput != 0) && characterController.isGrounded)
        {

            if (Input.GetButton("Sprint"))
            {
                StartBobbing();
            }
            else
            {

                StartBobbing();
            }

        }
        else
        {
            StopBobbing();
        }
    }

    public void StartBobbing()
    {
        cameraObj.GetComponent<Animator>().Play("Bobbing");
    }
    public void StopBobbing()
    {
        cameraObj.GetComponent<Animator>().Play("Stop");
    }
}
