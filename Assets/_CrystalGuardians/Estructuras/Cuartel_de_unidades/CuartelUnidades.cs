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
    public Text txtCapacidadActual;
    public Text txtLvlActual;
    public Button btnMejorar;
    public Button btnMejorarInfo;

    public GameObject menuCrearUnidades;
    private bool isInPosition1 = true;// para limpiar los flags de las posiciones del gamemanager al destruirse hay dos menus de crear unidades, si true es el que esta arriba,

    public btnUnidad btnGenerarGuerrero;
    public btnUnidad btnGenerarBallestero;

  
    public GameObject prefabLvl1;
    public GameObject prefabLvl2;
    public GameObject prefabLvl3;


    protected override void Start()
    {
        base.Start();
        GameManager.Instance.Oro = GameManager.Instance.Oro - GameManager.costeConstruirCuartel;
        GameManager.Instance.CuartelesConstruidos++;


        setUpMenuUnidades();
        setUpCanvasValues();
        sumarTopeUnidades(false);

        


    }

    protected override void Update()
    {
        base.Update();
        comprobarDisponibilidadCosteUnidades();
        comprobarDisponibilidadMejora();
    }

    private void OnDestroy()
    {
        if (isInPosition1)
        {
            GameManager.Instance.isCuartelPos1Empty = true;
        }
        else
        {
            GameManager.Instance.isCuartelPos2Empty = true;
        }
        GameManager.Instance.TopeUnidades -= capacidadUnidades[nivelActual]; // restar tope de unidades
        GameManager.Instance.CuartelesConstruidos--;
    }

   


    // METODOS ------------------------------------------
    public override void mejorar()
    {

        
        GameManager.Instance.Oro = GameManager.Instance.Oro - costeOroMejorar[nivelActual];

        comprobarCambiarPrefab();
        nivelActual++;


        // actualizar hud informacion
        //comprobarCambiarPrefab();
        setUpCanvasValues();
        sumarTopeUnidades(true);
        settearVida();
        
       
    }

    private void comprobarCambiarPrefab()
    {
        if (nivelActual > 0 && // para que no se salga del array
             nivelMinimoCastilloParaMejorar[nivelActual - 1] < nivelMinimoCastilloParaMejorar[nivelActual])
        {
            // se cambia el prefab cuando el siguiente nivel minimo de castillo cambia
            // si el anterior es menor 
            if (nivelMinimoCastilloParaMejorar[nivelActual] == 1)
            {
                // prefab nivel 2
                prefabLvl1.SetActive(false);
                prefabLvl2.SetActive(true);


            }
            else
            {
                // prefab nivel 3
                prefabLvl2.SetActive(false);
                prefabLvl3.SetActive(true);
               
            }

        }
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

    


    public void spawnUnidades(GameObject unidadAliada)
    {
        Vector3 spawnPoint = Utility.getPuntoPerimetroRectangulo(distanciaSpawn);
        
        Aliado aliado = unidadAliada.GetComponent<Aliado>();

        aliado.prefabLvl1.SetActive(prefabLvl1.activeSelf);
        aliado.prefabLvl2.SetActive(prefabLvl2.activeSelf);
        aliado.prefabLvl3.SetActive(prefabLvl3.activeSelf);

    

        aliado.nivelActual = nivelActual;
        aliado.settearVida();
        GameManager.Instance.Obsiidum -= aliado.costePorNivel[nivelActual];

        GameObject g = Instantiate(unidadAliada);
        g.transform.position = transform.position + spawnPoint;
       
        GameManager.Instance.Unidades++;

        GameManager.listaAliadosEnJuego.Add(g);

    }



    // METODOS DE HUD ------------------------------------------

    private void comprobarDisponibilidadCosteUnidades()
    {
        btnGenerarGuerrero.Available =
            (GameManager.Instance.Obsiidum >= guerrero.costePorNivel[nivelActual])
            && GameManager.Instance.TopeUnidades > GameManager.Instance.Unidades;
        btnGenerarBallestero.Available =
            (GameManager.Instance.Obsiidum >= ballestero.costePorNivel[nivelActual])
            && GameManager.Instance.TopeUnidades > GameManager.Instance.Unidades;

    }
    private void comprobarDisponibilidadMejora()
    {

        bool v = (nivelActual <= NivelMaximo - 1) &&
            GameManager.Instance.NivelActualCastillo >= nivelMinimoCastilloParaMejorar[nivelActual]
            && (GameManager.Instance.Oro >= costeOroMejorar[nivelActual]);

        btnMejorar.interactable = v;
        btnMejorarInfo.interactable = v;
    }
    private void setUpMenuUnidades()
    {


        btnGenerarBallestero.setUnidad(ballestero);
        btnGenerarGuerrero.setUnidad(guerrero);

        // posicionar el menu en una de las dos pos, comprobar si la primera esta vacia, sinno en la segunga
        if (GameManager.Instance.isCuartelPos1Empty)
        {
            GameManager.Instance.isCuartelPos1Empty = false;
        }
        else
        {
            // mover el menu de unidades a la posicion 2, 80 pixeles hacia arriba
            GameManager.Instance.isCuartelPos2Empty = false;
            isInPosition1 = false;
            Vector3 pos = menuCrearUnidades.transform.position;
            menuCrearUnidades.transform.position = new Vector3(pos.x, pos.y + Screen.height*0.18f, pos.z);
        }
    }


    private void setUpCanvasValues()
    {


        // panel actual
        txtLvlActual.text = "Cuartel de Unidades Nivel " + (nivelActual + 1).ToString();
        txtNivel.text = (nivelActual + 1).ToString();
        txtCapacidadActual.text = capacidadUnidades[nivelActual].ToString();

        txtSaludActual.text = vidaPorNivel[nivelActual].ToString();

        



        // panel siguiente
        if(nivelActual < NivelMaximo)
        {
            txtMejora.text = costeOroMejorar[nivelActual].ToString();
        }
        else
        {
            btnMejorar.gameObject.SetActive(false);
            btnMejorarInfo.gameObject.SetActive(false);
        }
       

       

    }

   
    public override void abrirMenu()
    {
        if (canvas != null)
        {
            canvas.SetActive(true);
        }
    }
    public override void cerrarMenu()
    {

        if (canvas != null)
        {
            canvas.SetActive(false);
        }


        
    }
    
    
}
