using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class Guerrero : Aliado
{

    
    

    void Start()
    {

        agent = GetComponent<NavMeshAgent>();
        settearVida();
    }

    // Update is called once per frame
    void Update()
    {

        mover();
        comprobarVida0();

    }
}
