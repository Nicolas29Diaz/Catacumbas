using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointsLogic : MonoBehaviour
{
    MeshRenderer MR;
    // Start is called before the first frame update
    void Start()
    {
        MR = GetComponent<MeshRenderer>();
        MR.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
