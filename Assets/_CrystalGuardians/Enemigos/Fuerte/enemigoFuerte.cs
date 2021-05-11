using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemigoFuerte : EnemigoScript
{
    [Header("Unity SetUp")]
    //Bala a disparar
    public GameObject areaAtaqueprefab;

   


    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    override public void attack()
    {
        /*Metodo que invoca el ataque fuerte del enemigo
        createAttack();
        */
    }

    public override List<GameObject> getPossibleTargets()
    {
        return GameManager.listaEstructurasEnJuego;
    }

    void createAttack()
    {
       
        areaAtaqueprefab.SetActive(true);
        areaAtaqueprefab.GetComponent<TriggerScriptEnemigoFuerte>().damage = danyoPorNivel[nivelActual];
    }
}
