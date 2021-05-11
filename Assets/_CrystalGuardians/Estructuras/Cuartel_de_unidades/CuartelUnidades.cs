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
    public GameObject prefabLvl1;
    public GameObject prefabLvl2;
    public GameObject prefabLvl3;


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
        settearVida();


    }
    private void Update()
    {
        comprobarDisponibilidadCosteUnidades();
        comprobarDisponibilidadMejora();
        comprobarVida0();
    }

    private void OnDestroy()
    {
        GameManager.Instance.TopeUnidades -= capacidadUnidades[nivelActual]; // restar tope de unidades
        GameManager.Instance.CuartelesConstruidos--;
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


        btnMejorar.enabled = (nivelActual <= NivelMaximo - 1) &&
            GameManager.Instance.NivelActualCastillo  >= nivelMinimoCastilloParaMejorar[nivelActual]
            && (GameManager.Instance.Oro >= costeOroMejorar[nivelActual]);
        
        btnMejorarInfo.enabled = (nivelActual <= NivelMaximo - 1) &&
            GameManager.Instance.NivelActualCastillo >= nivelMinimoCastilloParaMejorar[nivelActual]
            && (GameManager.Instance.Oro >= costeOroMejorar[nivelActual]);
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

        Debug.Log("-----");
        Debug.Log(prefabLvl1.activeSelf);
        Debug.Log(prefabLvl2.activeSelf);
        Debug.Log(prefabLvl3.activeSelf);

        aliado.nivelActual = nivelActual;
        aliado.settearVida();
        GameManager.Instance.Obsiidum -= aliado.costePorNivel[nivelActual];

        GameObject g = Instantiate(unidadAliada);
        g.transform.position = transform.position + spawnPoint;
       
        GameManager.Instance.Unidades++;

        GameManager.Instance.listaAliadosEnJuego.Add(g);

    }



    // METODOS DE HUD ------------------------------------------
    private void setUpCanvasValues()
    {
        // panel actual
        txtNivel.text = (nivelActual + 1).ToString();
        txtLvlActual.text = (nivelActual + 1).ToString();

        txtCapacidadActual.text = capacidadUnidades[nivelActual].ToString();

        txtSaludActual.text = vidaPorNivel[nivelActual].ToString();

        txtCosteBallestero.text = guerrero.costePorNivel[nivelActual].ToString();
        txtCosteGuerrero.text = guerrero.costePorNivel[nivelActual].ToString();



        // panel siguiente
        if(nivelActual < NivelMaximo)
        {
            txtMejora.text = costeOroMejorar[nivelActual].ToString();
            txtLvlSiguiente.text = (nivelActual + 2).ToString();
            txtCapacidadMejorada.text = capacidadUnidades[nivelActual + 1].ToString();
            txtSaludMejorada.text = vidaPorNivel[nivelActual + 1].ToString();
        }
        else
        {
            txtMejora.text = "Nivel Maximo Alcanzado";
            txtLvlSiguiente.text = "----";
            txtCapacidadMejorada.text = "----";
            txtSaludMejorada.text = "----";
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
    /*private void comprobarCambiarPrefab()
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

    }*/
}
