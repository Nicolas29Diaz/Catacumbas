using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class WalkSound : MonoBehaviour
{
    private CharacterController characterController;

    public LayerMask layer;

    public GameObject cameraObj;


    [Header("Sounds")]
    public AudioSource audioSource;
    public AudioClip groundSound;
    public AudioClip stoneSound;

    public float horizontalInput;
    public float verticalInput;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        Movement();
    }

    public void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if ((horizontalInput != 0 || verticalInput != 0) && characterController.isGrounded)
        {

            if (Input.GetButton("Sprint"))
            {
                Debug.Log("Corriendo");
                Run();
                StartBobbing();
            }
            else
            {
                Debug.Log("Caminando");
                Walk();
                StartBobbing();
            }
            
        }
        else
        {
            audioSource.Stop();
            StopBobbing();
        }

    }

    public void Walk()
    {
        //Debug.DrawRay(transform.position, Vector3.down*2);
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 2f, layer))
        {
            int layerNum = hit.collider.gameObject.layer;
            string layerName = LayerMask.LayerToName(layerNum);

            if (layerName.Equals("Ground"))
            {
                if (!audioSource.isPlaying)  // Verifica si no se está reproduciendo actualmente
                {
                    audioSource.PlayOneShot(groundSound);  // Reproduce el sonido si no se está reproduciendo
                }

            }
            else if (layerName.Equals("Stone"))
            {
                if (!audioSource.isPlaying)  // Verifica si no se está reproduciendo actualmente
                {
                    audioSource.PlayOneShot(stoneSound);  // Reproduce el sonido si no se está reproduciendo
                }
            }
        }
    }

    public void Run()
    {
        //Debug.DrawRay(transform.position, Vector3.down*2);
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 2f, layer))
        {
            int layerNum = hit.collider.gameObject.layer;
            string layerName = LayerMask.LayerToName(layerNum);

            if (layerName.Equals("Ground"))
            {
                if (!audioSource.isPlaying)  // Verifica si no se está reproduciendo actualmente
                {
                    audioSource.PlayOneShot(groundSound);  // Reproduce el sonido si no se está reproduciendo
                }

            }
            else if (layerName.Equals("Stone"))
            {
                if (!audioSource.isPlaying)  // Verifica si no se está reproduciendo actualmente
                {
                    audioSource.PlayOneShot(stoneSound);  // Reproduce el sonido si no se está reproduciendo
                }
            }
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
