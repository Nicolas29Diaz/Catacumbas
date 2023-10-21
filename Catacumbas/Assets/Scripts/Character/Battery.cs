using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    [Header("References")]
    public FlashLight flashLight;
    

    [Header("Battery")]
    public int amountBattery = 1;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Battery"))
        {
            flashLight.countBattery += amountBattery;
            Destroy(other.gameObject); // Destruye el objeto de la batería
        }
    }
}
