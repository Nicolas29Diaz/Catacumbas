using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladders : MonoBehaviour
{
    public Transform ParteSuperior;
    public CharacterController JugadorController;

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("OnTriggerStayed");
        if (Input.GetKeyDown(KeyCode.Space) && JugadorController != null)
        {
            JugadorController.enabled = false; 
            JugadorController.transform.position = ParteSuperior.position;
            JugadorController.enabled = true; 
            Debug.Log("SPACE PRESSED");
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && JugadorController != null)
        {
            JugadorController.enabled = false;
            JugadorController.transform.position = ParteSuperior.position;
            JugadorController.enabled = true;
            Debug.Log("SPACE PRESSED");
        }
    }
}