using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyManager))]
public class StatesManager : EnemyManager
{
    // Start is called before the first frame update
    private EnemyManager EM;
    [HideInInspector]
    

    void Start()
    {
        EM = GetComponent<EnemyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
