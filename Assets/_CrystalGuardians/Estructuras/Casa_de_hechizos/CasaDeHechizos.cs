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

    public Text txtCosteHeal;
    public Text txtCosteRayo;
    public Text txtCosteBuff;
    
    public Text txtLvlActual;
    public Text txtLvlSiguiente;
    public Button btnMejorar;
    public Button btnMejorarInfo;
    public Button btnHeal;
    public Button btnRayo;
    public Button btnBuff;


    public GameObject prefabNvl1;
    public GameObject prefabNvl2;
    public GameObject prefabNvl3;

    public override void abrirMenu()
    {
        if(canvas != null)
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


    public override void mejorar()
    {
        GameManager.Instance.Oro = GameManager.Instance.Oro - costeOroMejorar[nivelActual];

        nivelActual++;
        GameManager.nivelCasaHechizos++;
        // actualizar hud informacion
        setUpCanvasValues();
        settearVida();
        comprobarNivelCasa();
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
        GameManager.Instance.CasasDeHechizosConstruidas++;
        setUpCanvasValues();
        settearVida();


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

    private void Update()
    {
        comprobarDisponibilidadMejora();
        comprobarVida0();
        comprobarBotones();
        
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
        txtCosteHeal.text = GameManager.costeLanzarHeal[nivelActual].ToString();
        txtCosteRayo.text = GameManager.costeLanzarRayo[nivelActual].ToString();
        txtCosteBuff.text = GameManager.costeLanzarBuff[nivelActual].ToString();



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

     private void comprobarBotones()
    {
        btnHeal.enabled = GameManager.Instance.Obsiidum>=GameManager.costeLanzarHeal[nivelActual];
        btnBuff.enabled = GameManager.Instance.Obsiidum>=GameManager.costeLanzarBuff[nivelActual];
        btnRayo.enabled = GameManager.Instance.Obsiidum>=GameManager.costeLanzarRayo[nivelActual];
    }

    public void generarRayo()
    {
        GameManager.Instance.Obsiidum -= GameManager.costeLanzarRayo[nivelActual];
        GameManager.Instance.RayosDisponibles++;
    }
    public void generarHeal()
    {
        GameManager.Instance.Obsiidum -= GameManager.costeLanzarBuff[nivelActual];
        GameManager.Instance.HealsDisponibles++;
    }
    public void generarBuff()
    {
        GameManager.Instance.Obsiidum -= GameManager.costeLanzarBuff[nivelActual];
        GameManager.Instance.BuffsDisponibles++;
    }

    private void OnDestroy()
    {
        GameManager.Instance.CasasDeHechizosConstruidas--;
    }
}
