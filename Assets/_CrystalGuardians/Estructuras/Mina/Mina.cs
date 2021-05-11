using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mina : Estructura
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

    //prefabs mina
    public GameObject prefabLvl1;
    public GameObject prefabLvl2;
    public GameObject prefabLvl3;


    // Storing different levels'
    public GameObject[] levels;
    // Counting current level
    int current_level = 0;

    public int[] generacionOroPorNivel;

    public static float mejoraDeAldeaProduccionOro = 1;//100% = 1 

    private void generarRecursos()
    {
        //GameManager.Instance.Oro += 1 * Time.deltaTime; //mina lvl-1
        GameManager.Instance.Oro = GameManager.Instance.Oro + generacionOroPorNivel[nivelActual] * Time.deltaTime * mejoraDeAldeaProduccionOro;

        // Debug.Log("Estoy generando --> " + (generacionOroPorNivel[nivelActual] * mejoraDeAldeaProduccionOro));
    }

    public override void mejorar()
    {
        GameManager.Instance.Oro = GameManager.Instance.Oro - costeOroMejorar[nivelActual];


        comprobarCambiarPrefab();
        nivelActual++;


        settearVida();

        // actualizar hud informacion
        setUpCanvasValues();

    }

    // Start is called before the first frame update
    void Start()
    {

        /*GameManager.Instance.Oro = GameManager.Instance.Oro - GameManager.costeConstruirMina;
        GameManager.Instance.oroConstruido = true;*/
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

    public override void cerrarMenu()
    {
        if (canvas != null)
        {
            canvas.SetActive(false);
        }
    }

    public override void abrirMenu()
    {
        if (canvas != null)
        {
            canvas.SetActive(true);
        }
    }

    private void comprobarDisponibilidadMejora()
    {

        btnMejorar.enabled = (nivelActual <= NivelMaximo - 1)
            && GameManager.Instance.NivelActualCastillo >= nivelMinimoCastilloParaMejorar[nivelActual]
            && (GameManager.Instance.Oro >= costeOroMejorar[nivelActual]);
        btnMejorarInfo.enabled = (nivelActual <= NivelMaximo - 1)
            && GameManager.Instance.NivelActualCastillo >= nivelMinimoCastilloParaMejorar[nivelActual]
            && (GameManager.Instance.Oro >= costeOroMejorar[nivelActual]);
    }

    private void setUpCanvasValues()
    {



        txtLvlActual.text = (nivelActual + 1).ToString();
        txtProduccionActual.text = generacionOroPorNivel[nivelActual].ToString();
        txtSaludActual.text = vidaPorNivel[nivelActual].ToString();

        if (nivelActual < NivelMaximo)
        {
            txtLvlSiguiente.text = (nivelActual + 2).ToString();

            txtProduccionMejorada.text = generacionOroPorNivel[nivelActual + 1].ToString();
            txtMejora.text = costeOroMejorar[nivelActual].ToString();
            ;
            txtSaludMejorada.text = vidaPorNivel[nivelActual + 1].ToString();
        }
        else
        {
            txtLvlSiguiente.text = "----------";

            txtProduccionMejorada.text = "-----------";
            txtMejora.text = "----------";
            txtSaludMejorada.text = "-------";
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
