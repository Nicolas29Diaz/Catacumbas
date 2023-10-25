using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabBullet : MonoBehaviour
{
    [Header("References")]
    public Gun gun;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            if (other.GetComponent<Bullet>() != null)
            {
                Bullet bullet = other.GetComponent<Bullet>();
                gun.bulletSaved += bullet.amountBullet;
                Destroy(other.gameObject); // Destruye el objeto de la batería
            }
           
        }
    }
}
