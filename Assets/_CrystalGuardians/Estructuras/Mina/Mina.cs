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
        current_level = current_level++;
        nivelActual = nivelActual + 1;



        GameManager.Instance.Oro = GameManager.Instance.Oro - costeOroMejorar[nivelActual];

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
    }

    // Update is called once per frame
    void Update()
    {

        comprobarDisponibilidadMejora();
        generarRecursos();
    }

    public override void cerrarMenu()
    {
        canvas.SetActive(false);
    }

    public override void abrirMenu()
    {
        canvas.SetActive(true);
    }

    private void comprobarDisponibilidadMejora()
    {

        btnMejorar.enabled = GameManager.Instance.NivelActualCastillo >= nivelMinimoCastilloParaMejorar[nivelActual]
            && (GameManager.Instance.Oro >= costeOroMejorar[nivelActual]);
        btnMejorarInfo.enabled = GameManager.Instance.NivelActualCastillo >= nivelMinimoCastilloParaMejorar[nivelActual]
            && (GameManager.Instance.Oro >= costeOroMejorar[nivelActual]);
    }

    private void setUpCanvasValues()
    {


        
        txtLvlActual.text = (nivelActual + 1).ToString();
        txtLvlSiguiente.text = (nivelActual + 2).ToString();
        txtProduccionActual.text = generacionOroPorNivel[nivelActual].ToString();
        txtProduccionMejorada.text = generacionOroPorNivel[nivelActual + 1].ToString();
        txtMejora.text = costeOroMejorar[nivelActual].ToString();
        txtSaludActual.text = vidaPorNivel[nivelActual].ToString();
        txtSaludMejorada.text = vidaPorNivel[nivelActual + 1].ToString();



    }
}
