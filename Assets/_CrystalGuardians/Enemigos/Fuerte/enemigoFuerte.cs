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
        base.Update();
        AnimacionController.damage = danyoPorNivel[nivelActual];
    }

    override public void attack()
    {
        /*Metodo que invoca el ataque fuerte del enemigo
        //El ataque se produce al terminar la animacion
        */
    }

    public override List<GameObject> getPossibleTargets()
    {
        return GameManager.listaEstructurasEnJuego;
    }
}
