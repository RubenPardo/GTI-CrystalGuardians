using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemigoFuerte : EnemigoScript
{
    [Header("Unity SetUp")]
    //Bala a disparar
    public AnimacionController AnimacionController;

   
    // Update is called once per frame
    protected override void Update()
    {
        AnimacionController.damage = danyoPorNivel[nivelActual];
    }

  

    public override List<GameObject> getPossibleTargets()
    {
        return GameManager.listaEstructurasEnJuego;
    }

}
