using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trampa : Estructura
{

    public Text txtNivel;
    public Text txtMejora;

    public Text txtSaludActual;
    public Text txtSaludMejorada;
    public Text txtDañoActual;
    public Text txtDañoMejorada;
    public Text txtLvlActual;
    public Text txtLvlSiguiente;
    public Button btnMejorar;
    public Button btnMejorarInfo;


    // Storing different levels'
    public GameObject[] levels;
    // Counting current level
    int current_level = 0;
    public int[] danyoPorNivel;
    public override void mejorar()
    {


        current_level = current_level++;
        nivelActual = nivelActual + 1;



        GameManager.Instance.Oro = GameManager.Instance.Oro - costeOroMejorar[nivelActual];

        // actualizar hud informacion
        setUpCanvasValues();
    }

    public override void abrirMenu()
    {
        canvas.SetActive(true);
    }

    public override void cerrarMenu()
    {
        canvas.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
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
        txtDañoActual.text = danyoPorNivel[nivelActual].ToString();
        txtDañoMejorada.text = danyoPorNivel[nivelActual + 1].ToString();
        txtMejora.text = costeOroMejorar[nivelActual].ToString();
        



    }
}
