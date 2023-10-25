using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabBattery : MonoBehaviour
{
    [Header("References")]
    public FlashLight flashLight;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Battery"))
        {
            if(other.GetComponent<Battery>() != null)
            {
                Battery battery = other.GetComponent<Battery>();
                flashLight.countBattery += battery.amountBattery;
                Destroy(other.gameObject); // Destruye el objeto de la batería
            }
           
        }
    }
}
