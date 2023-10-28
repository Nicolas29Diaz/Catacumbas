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
    
    public float delayRealoadBullet = 1f;
    public float delayShootBullet = 1f;

    public bool shootReady;
    public bool reloadReady;
    public bool cancelReload;

    private bool isGamePaused = false;

    private void OnEnable()
    {
        PauseLogic.OnGamePaused += HandleGamePause;
    }

    private void OnDisable()
    {
        PauseLogic.OnGamePaused -= HandleGamePause;
    }

    private void HandleGamePause(bool pauseStatus)
    {
        isGamePaused = pauseStatus;
    }

        // Start is called before the first frame update
        void Start()
    {
        shootReady = true;
        reloadReady = true;
        cancelReload = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (!isGamePaused)
        {

            if (Input.GetMouseButtonDown(0) && shootReady && actualAmo > 0)
            {
                Shoot();
            }

            if (Input.GetKeyDown(KeyCode.E) && reloadReady)
            {
                StartCoroutine(Reload());

            }
            else if (Input.GetKeyDown(KeyCode.E) && !reloadReady && cancelReload)
            {
                CancelReload();
            }
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


        for (int i = 0; i < bulletSavedInit; i++)
        {
            Debug.Log(i);
            Debug.Log(bulletSaved);
           
            if (actualAmo <= 5)
            {
                yield return new WaitForSeconds(delayRealoadBullet); // Espera 
                actualAmo++;
                if (bulletSaved > 0)
                {
                    bulletSaved--;
                }
                
            }
        }

        shootReady = true;
        reloadReady = true;
    }
    private IEnumerator WaitForShoot()
    {
        cancelReload = false;
        shootReady = false;
        reloadReady = false;
        yield return new WaitForSeconds(delayShootBullet); // Espera 
        shootReady = true;
        reloadReady = true;
        cancelReload = true;
    }

    private void CancelReload()
    {
        StopAllCoroutines();
        shootReady = true;
        reloadReady = true;
    }



}
   


