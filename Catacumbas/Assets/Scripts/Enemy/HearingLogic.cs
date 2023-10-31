using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyManager))]
public class HearingLogic : MonoBehaviour
{
    [Header("Parametros")]
    [Range(10, 100)]
    [SerializeField] private int HearingDistance = 10;
    Gun PlayerGun;
    public bool ShotHeard = false;
    public Vector3 LastKnownPosition;
    

    private EnemyManager EM;
    private void Start()
    {
        PlayerGun = GameObject.FindGameObjectWithTag("Gun").GetComponent<Gun>();
        EM = GetComponent<EnemyManager>();
    }

    private void Update()
    {
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, HearingDistance);
        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            if (targetsInViewRadius[i].tag == "Player" && PlayerGun.Shoted)
            {
                //Debug.Log("ShotHeared");
                ShotHeard = true;
                LastKnownPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
            }
        }
    }


    private void OnDrawGizmos()
    {
        // Dibuja una esfera en la posición del GameObject con el radio especificado
        Gizmos.color = Color.red; // Puedes cambiar el color como desees
        Gizmos.DrawWireSphere(transform.position, HearingDistance);
    }
}
