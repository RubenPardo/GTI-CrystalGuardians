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
   

    // Storing different levels'
    public GameObject[] levels;
    // Counting current level
    int current_level = 0;

    public int[] generacionObsidiumPorNivel;

    public override void abrirMenu()
    {
        canvas.SetActive(true);
    }

    public override void cerrarMenu()
    {
         canvas.SetActive(false);
    }

    private void generarRecursos()
    {
        GameManager.Instance.Obsiidum = GameManager.Instance.Obsiidum + generacionObsidiumPorNivel[nivelActual] * Time.deltaTime;
    }

    public override void mejorar()
    {
        GameManager.Instance.Oro = GameManager.Instance.Oro - costeOroMejorar[nivelActual];
        nivelActual = nivelActual + 1;



        

        // actualizar hud informacion
        setUpCanvasValues();
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
    }

    // Update is called once per frame
    void Update()
    {
        
        comprobarDisponibilidadMejora();
        generarRecursos();
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
}
