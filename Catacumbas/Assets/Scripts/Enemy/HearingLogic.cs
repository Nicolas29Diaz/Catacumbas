using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyManager))]
public class HearingLogic : MonoBehaviour
{
    [Header("Parametros")]
    [Range(10, 100)]
    [SerializeField] private int HearingDistance = 10;
    

    private EnemyManager EM;
    private void Start()
    {
        EM = GetComponent<EnemyManager>();
    }

    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawWireSphere(transform.position, HearingDistance);
    //}
}
