using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CasaDeHechizos : Estructura
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
    public override void abrirMenu()
    {
        canvas.SetActive(true);
    }
    public override void cerrarMenu()
    {
        canvas.SetActive(false);
    }

    public override void mejorar()
    {
        GameManager.Instance.Oro = GameManager.Instance.Oro - costeOroMejorar[nivelActual];

        nivelActual++;
        GameManager.nivelCasaHechizos++;
        // actualizar hud informacion
        setUpCanvasValues();
        settearVida();
    }

    // Start is called before the first frame update
    void Start()
    {
        // al empezar restar el oro
        GameManager.Instance.Oro = GameManager.Instance.Oro - GameManager.costeConstruirCasaHechizos;
        // canvas del menu de botones
        canvas = gameObject.transform.Find("Canvas").gameObject;
        if (canvas != null)
        {
            canvas.SetActive(false);
        }
        GameManager.nivelCasaHechizos = 0;
        setUpCanvasValues();
        settearVida();
    }

    private void Update()
    {
        
        comprobarDisponibilidadMejora();
        comprobarVida0();
    }
    private void comprobarDisponibilidadMejora()
    {


        btnMejorar.enabled = 
            (nivelActual <= NivelMaximo-1) 
            && GameManager.Instance.NivelActualCastillo >= nivelMinimoCastilloParaMejorar[nivelActual]
            && (GameManager.Instance.Oro >= costeOroMejorar[nivelActual]);
        
        btnMejorarInfo.enabled = 
            (nivelActual <= NivelMaximo-1) 
            && GameManager.Instance.NivelActualCastillo >= nivelMinimoCastilloParaMejorar[nivelActual]
            && (GameManager.Instance.Oro >= costeOroMejorar[nivelActual]);
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
            txtMejora.text = "----------";

            txtSaludMejorada.text ="--------";
        }


    }
}
