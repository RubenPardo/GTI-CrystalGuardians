using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExtractorObsidium : Estructura
{
    public Text txtNivel;
    public Text txtMejora;
   
    public Text txtSaludActual;
    public Text txtSaludMejorada;
    public Text txtProduccionActual;
    public Text txtProduccionMejorada;
    public Text txtLvlActual;
    public Text txtLvlSiguiente;
    public Button btnMejorar;
    public Button btnMejorarInfo;

    public GameObject prefabLvl1;
    public GameObject prefabLvl2;
    public GameObject prefabLvl3;

    // Storing different levels'
    public GameObject[] levels;
    public int[] generacionObsidiumPorNivel;

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

    private void generarRecursos()
    {
        GameManager.Instance.Obsiidum = GameManager.Instance.Obsiidum + generacionObsidiumPorNivel[nivelActual] * Time.deltaTime;
    }

    public override void mejorar()
    {
        GameManager.Instance.Oro = GameManager.Instance.Oro - costeOroMejorar[nivelActual];
        nivelActual++;
        // actualizar hud informacion
        comprobarCambiarPrefab();
        setUpCanvasValues();
        settearVida();
    }

    // Start is called before the first frame update
    void Start()
    {
        //GameManager.Instance.Oro = GameManager.Instance.Oro - GameManager.costeConstruirExtractor;
        // canvas del menu de botones
        canvas = gameObject.transform.Find("Canvas").gameObject;
        if (canvas != null)
        {
            canvas.SetActive(false);
        }
        setUpCanvasValues();
        settearVida();
    }

    // Update is called once per frame
    void Update()
    {
        
        comprobarDisponibilidadMejora();
        generarRecursos();
        comprobarVida0();
    }
    private void comprobarDisponibilidadMejora()
    {

        btnMejorar.enabled = (nivelActual <= NivelMaximo - 1) && GameManager.Instance.NivelActualCastillo >= nivelMinimoCastilloParaMejorar[nivelActual]
            && (GameManager.Instance.Oro >= costeOroMejorar[nivelActual]);
        btnMejorarInfo.enabled = (nivelActual <= NivelMaximo - 1) && GameManager.Instance.NivelActualCastillo >= nivelMinimoCastilloParaMejorar[nivelActual]
            && (GameManager.Instance.Oro >= costeOroMejorar[nivelActual]);
    }

    private void setUpCanvasValues()
    {



        txtLvlActual.text = (nivelActual + 1).ToString();
        txtProduccionActual.text = generacionObsidiumPorNivel[nivelActual].ToString();
        txtSaludActual.text = vidaPorNivel[nivelActual].ToString();


        if (nivelActual < NivelMaximo) { 
        txtLvlSiguiente.text = (nivelActual + 2).ToString();

        txtProduccionMejorada.text = generacionObsidiumPorNivel[nivelActual + 1].ToString();
        txtMejora.text = costeOroMejorar[nivelActual].ToString();

        txtSaludMejorada.text = vidaPorNivel[nivelActual + 1].ToString();
        }
        else
        {
            txtLvlSiguiente.text = "----------";

            txtProduccionMejorada.text = "---------";
            txtMejora.text = "Nivel Maximo";

            txtSaludMejorada.text = "-------------";
        }


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
}
