using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CuartelUnidades : Estructura
{
    public int[] capacidadUnidades;
    public int[] costesGuerreroPorNivel;
    public int[] costesBallesteroPorNivel;

    public Text txtNivel;
    public Text txtCosteGuerrero;
    public Text txtCosteBallestero;
    public Text txtTopeUnidades;
    

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
        setUpCanvasValues();
        sumarTopeUnidades(false);


    }

    private void sumarTopeUnidades(bool isMejora)
    {
        // si estamos mejorando se aumenta solo la resta entre el nivel nuevo menos el anterior
        if (isMejora)
        {
            GameManager.Instance.TopeUnidades += capacidadUnidades[nivelActual] - capacidadUnidades[nivelActual - 1];
        }
        else
        {
            GameManager.Instance.TopeUnidades += capacidadUnidades[nivelActual];
        }

    }

    private void setUpCanvasValues()
    {
       

        txtNivel.text = (nivelActual+1).ToString();
        txtCosteBallestero.text = costesBallesteroPorNivel[nivelActual].ToString();
        txtCosteGuerrero.text = costesGuerreroPorNivel[nivelActual].ToString();
     

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
