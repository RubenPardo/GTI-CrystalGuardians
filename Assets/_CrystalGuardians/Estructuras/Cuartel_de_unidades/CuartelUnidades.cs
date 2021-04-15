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

      
        // canvas del menu de botones
        canvas = gameObject.transform.Find("Canvas").gameObject;
        if (canvas != null)
        {
            canvas.SetActive(false);
        }

    }

    private void Update()
    {
        
    }


    public override void mejorar()
    {
        throw new System.NotImplementedException();
    }

    public override void abrirMenu()
    {

        canvas.SetActive(true);
    }

    public override void cerrarMenu()
    {
        canvas.SetActive(false);

    }
}
