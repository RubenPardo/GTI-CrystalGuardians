using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemigoFuerte : EnemigoScript
{
    [Header("Unity SetUp")]


    //Ataque a realizar
    public GameObject prefabAreaAttack;

    
    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
    override public void attack()
    {
        producirAtaque();
        

    }
    void producirAtaque()
    {

        Instantiate(prefabAreaAttack);
    }
    

}
