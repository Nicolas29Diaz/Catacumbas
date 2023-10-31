using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointsLogic : MonoBehaviour
{
    MeshRenderer MR;
    private int HearingDistance = 10;
    public bool isArrived;
    // Start is called before the first frame update
    void Start()
    {
        MR = GetComponent<MeshRenderer>();
        MR.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, HearingDistance);
        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        isArrived = true;
        if (other.tag == "Enemy")
        {
            //Debug.Log("ShotHeared");
            
            //LastKnownPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        isArrived = false;
        if (other.tag == "Enemy")
        {
            //Debug.Log("ShotHeared");
            
            //LastKnownPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        }
    }
}
