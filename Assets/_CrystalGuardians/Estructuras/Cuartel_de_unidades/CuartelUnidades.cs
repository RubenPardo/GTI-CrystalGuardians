using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CuartelUnidades : Estructura
{
    public int[] capacidadUnidades;
    public Aliado guerrero;
    public Aliado ballestero;
    public float distanciaSpawn = 0.5f;

    public Text txtNivel;
    public Text txtCosteGuerrero;
    public Text txtCosteBallestero;
    public Button btnGenerarGuerrero;
    public Button btnGenerarBallestero;
    
    

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
    private void Update()
    {
        comprobarDisponibilidadCosteUnidades();
    }

    private void comprobarDisponibilidadCosteUnidades()
    {
        btnGenerarGuerrero.enabled = 
            (GameManager.Instance.Obsiidum >= guerrero.costePorNivel[nivelActual]) 
            && GameManager.Instance.TopeUnidades > GameManager.Instance.Unidades;
        btnGenerarBallestero.enabled = 
            (GameManager.Instance.Obsiidum >= ballestero.costePorNivel[nivelActual])
            && GameManager.Instance.TopeUnidades > GameManager.Instance.Unidades;
      
    }

    // METODOS ------------------------------------------
    public override void mejorar()
    {
        throw new System.NotImplementedException();
    }


    // METODOS UNIDADES ------------------------------------------
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
    public void spawnUnidades(Aliado aliado)
    {
        Vector3 spawnPoint = Utility.getPuntoPerimetroRectangulo(distanciaSpawn);
        GameObject g = Instantiate(aliado.gameObject);
        aliado.nivelActual = nivelActual;
        GameManager.Instance.Obsiidum -= aliado.costePorNivel[nivelActual];
        
        g.transform.position = transform.position + spawnPoint;

        GameManager.Instance.Unidades++;
        
    }



    // METODOS DE HUD ------------------------------------------
    private void setUpCanvasValues()
    {


        txtNivel.text = (nivelActual + 1).ToString();
        txtCosteBallestero.text = guerrero.costePorNivel[nivelActual].ToString();
        txtCosteGuerrero.text = guerrero.costePorNivel[nivelActual].ToString();// TODO cambiar


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
