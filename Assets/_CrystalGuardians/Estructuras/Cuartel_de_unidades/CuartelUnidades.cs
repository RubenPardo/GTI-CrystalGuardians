using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuartelUnidades : Estructura
{
    public int[] capacidadUnidades;
    private void Start()
    {
        GameManager.Instance.Oro = GameManager.Instance.Oro - GameManager.costeConstruirCuartel;
        GameManager.Instance.CuartelesConstruidos++;
    }

    private void Update()
    {
        
    }

    public override void mejorar()
    {
        throw new System.NotImplementedException();
    }

}
