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
    public Text txtMejora;
    public Text txtCosteGuerrero;
    public Text txtCosteBallestero;
    public Text txtSaludActual;
    public Text txtSaludMejorada;
    public Text txtCapacidadActual;
    public Text txtCapacidadMejorada;
    public Text txtLvlActual;
    public Text txtLvlSiguiente;
    public Button btnMejorar;
    public Button btnMejorarInfo;
    public Button btnGenerarGuerrero;
    public Button btnGenerarBallestero;
    public Image imgMejora;

    // Storing different levels'
    public GameObject[] levels;
    // Counting current level
    int current_level = 0;


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
        comprobarDisponibilidadMejora();
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
    private void comprobarDisponibilidadMejora()
    {
       
        btnMejorar.enabled = GameManager.Instance.NivelActualCastillo  >= nivelMinimoCastilloParaMejorar[nivelActual]
            && (GameManager.Instance.Oro >= costeOroMejorar[nivelActual]);
        //imgMejora.enabled = GameManager.Instance.NivelActualCastillo >= nivelMinimoCastilloParaMejorar[nivelActual]
            //&& (GameManager.Instance.Oro >= costeOroMejorar[nivelActual]);
        btnMejorarInfo.enabled = GameManager.Instance.NivelActualCastillo >= nivelMinimoCastilloParaMejorar[nivelActual]
            && (GameManager.Instance.Oro >= costeOroMejorar[nivelActual]);
    }

    // METODOS ------------------------------------------
    public override void mejorar()
    {


        current_level = current_level++;
        nivelActual = nivelActual + 1;
        
        

      GameManager.Instance.Oro = GameManager.Instance.Oro - costeOroMejorar[nivelActual];

       // actualizar hud informacion
        setUpCanvasValues();
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
        txtLvlActual.text = (nivelActual + 1).ToString();
        txtLvlSiguiente.text = (nivelActual + 2).ToString();
        txtCapacidadActual.text = capacidadUnidades[nivelActual].ToString();
        txtCapacidadMejorada.text = capacidadUnidades[nivelActual + 1].ToString();
        txtMejora.text = costeOroMejorar[nivelActual].ToString();
        txtSaludActual.text = vidaPorNivel[nivelActual].ToString();
        txtSaludMejorada.text = vidaPorNivel[nivelActual + 1].ToString();
        txtCosteBallestero.text = guerrero.costePorNivel[nivelActual].ToString();
        txtCosteGuerrero.text = guerrero.costePorNivel[nivelActual].ToString();


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
