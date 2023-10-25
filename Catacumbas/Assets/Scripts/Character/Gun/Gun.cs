using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    

    [Header("References")]
    public Camera fpsCamera;
    public ParticleSystem muzzleFlash;

    [Header("Bullet")]
    public float damage = 10f;
    public float range = 100f;

    [Header("Ammo")]
    public int amoCapacity = 6;
    public int actualAmo = 6;
    public int bulletSaved = 2;
    
    public float shootDelay = 2f;
    public float delayRealoadBullet = 1f;
    public float delayShootBullet = 1f;

    public bool shootReady;
    public bool reloadReady;




    // Start is called before the first frame update
    void Start()
    {
        shootReady = true;
        reloadReady = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetButtonDown("Fire1") && shootReady && actualAmo > 0)
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.E) && reloadReady)
        {
            StartCoroutine(Reload());
        }
    }


    public void Shoot()
    {
        actualAmo--;

            muzzleFlash.Play();

            RaycastHit hit;

            if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
            {
                TargetShooting targetShooting = hit.transform.GetComponent<TargetShooting>();
                if (targetShooting != null)
                {
                    targetShooting.TakeDamage(damage);
                    Debug.Log("Damage: " + damage);
                }
                else
                {
                    Debug.Log(hit.transform.name);
                }
            }

        StartCoroutine(WaitForShoot());
    }

    private IEnumerator Reload()
    {
        shootReady = false;
        reloadReady = false;

        float bulletSavedInit = bulletSaved;


        for (int i = 0; i <= bulletSavedInit; i++)
        {
            Debug.Log(i);
            Debug.Log(bulletSaved);
           
            if (actualAmo <= 5)
            {
                yield return new WaitForSeconds(delayRealoadBullet); // Espera 
                actualAmo++;
                bulletSaved--;
            }
        }

        shootReady = true;
        reloadReady = true;
    }
    private IEnumerator WaitForShoot()
    {
        shootReady = false;
        reloadReady = false;
        yield return new WaitForSeconds(delayShootBullet); // Espera 
        shootReady = true;
        reloadReady = true;
    }

}
    //private void WaitReload()
    //{
    //    bulletReloaded = false;
    //    timePerBulletRelaod -= timerPerBulletRelaod * Time.deltaTime;
    //    if(timePerBulletRelaod <= 0)
    //    {
    //        bulletReloaded = true;
    //    }

    //}


    //public void Reload()
    //{
    //    for (int i = 1; i <= bulletSaved; i++)
    //    {
    //        shootReady = false;
    //        if (actualAmo < 6)
    //        {
    //            WaitReload();
    //        }
    //    }

    //    Debug.Log("Cargador Lleno");
    //    shootReady = true;
    //}

