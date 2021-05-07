using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Muro : Estructura
{

    public Text txtNivel;
    public Text txtMejora;
    
    public Text txtSaludActual;
    public Text txtSaludMejorada;
    
    public Text txtLvlActual;
    public Text txtLvlSiguiente;
    public Button btnMejorar;
    public Button btnMejorarInfo;

    // Storing different levels'
    public GameObject[] levels;


    //prefabs muro
    public GameObject prefabNvl1;
    public GameObject prefabNvl2;
    public GameObject prefabNvl3;

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
        GameManager.Instance.Oro = GameManager.Instance.Oro - GameManager.costeConstruirMuro;
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
    private void Update()
    {
        
        comprobarDisponibilidadMejora();
        comprobarVida0();
    }

    private void comprobarDisponibilidadMejora()
    {

        btnMejorar.enabled = (nivelActual <= NivelMaximo - 1) && GameManager.Instance.NivelActualCastillo >= nivelMinimoCastilloParaMejorar[nivelActual]
            && (GameManager.Instance.Oro >= costeOroMejorar[nivelActual]);

        btnMejorarInfo.enabled = (nivelActual <= NivelMaximo - 1) && GameManager.Instance.NivelActualCastillo >= nivelMinimoCastilloParaMejorar[nivelActual]
            && (GameManager.Instance.Oro >= costeOroMejorar[nivelActual]);
    }
    public override void mejorar()
    {


        GameManager.Instance.Oro = GameManager.Instance.Oro - costeOroMejorar[nivelActual];
        nivelActual = nivelActual + 1;


        // actualizar hud informacion
        setUpCanvasValues();
        settearVida();
        comprobarNivelCasa();
    }

    private void setUpCanvasValues()
    {


        
        txtLvlActual.text = (nivelActual + 1).ToString();
        txtSaludActual.text = vidaPorNivel[nivelActual].ToString();

        if (nivelActual < NivelMaximo)
        {
            txtLvlSiguiente.text = (nivelActual + 2).ToString();

            txtMejora.text = costeOroMejorar[nivelActual].ToString();

            txtSaludMejorada.text = vidaPorNivel[nivelActual + 1].ToString();
        }
        else
        {
            txtLvlSiguiente.text = "--------";

            txtMejora.text = "Nivel Maximo";

            txtSaludMejorada.text = "----------";
        }

    }

    public void comprobarNivelCasa()
    {
        switch (nivelActual)
        {


            case 1:


                prefabNvl1.SetActive(false);
                prefabNvl2.SetActive(true);

                //Debug.Log("estoy a nivel 2");
                break;
            case 2:

                //Debug.Log("estoy a nivel 3");

                prefabNvl2.SetActive(false);
                prefabNvl3.SetActive(true);
                break;
        }
    }
}
