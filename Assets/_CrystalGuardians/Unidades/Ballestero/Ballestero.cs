using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using System.Linq;
using UnityEngine;

public class Ballestero : Aliado
{


    // Start is called before the first frame update

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
