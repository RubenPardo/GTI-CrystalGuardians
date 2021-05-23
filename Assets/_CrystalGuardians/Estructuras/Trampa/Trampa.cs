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

    public GameObject trampaInactivaNvl1;
    public GameObject trampaActivaNvl1;
    public GameObject trampaInactivaNvl2;
    public GameObject trampaActivaNvl2;

    public GameObject colliderExplosion;

    // Storing different levels'
    public GameObject[] levels;
    public int[] danyoPorNivel;
    public override void mejorar()
    {
        GameManager.Instance.Oro = GameManager.Instance.Oro - costeOroMejorar[nivelActual];

        
        nivelActual = nivelActual + 1;



        GameManager.Instance.Oro = GameManager.Instance.Oro - costeOroMejorar[nivelActual];

        //cambio de prefab 
        trampaInactivaNvl1.SetActive(false);
        trampaInactivaNvl2.SetActive(true);

        colliderTrampa colliderTrampa = colliderExplosion.GetComponent<colliderTrampa>();
        colliderTrampa.rangoExplosion = trampaActivaNvl2;

        // actualizar hud informacion
        setUpCanvasValues();
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

    // Start is called before the first frame update
    protected override void Start()
    {
        GameManager.Instance.Oro = GameManager.Instance.Oro - GameManager.costeConstruirTrampa;
        base.Start();
        setUpCanvasValues();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        comprobarDisponibilidadMejora();
    }

    private void comprobarDisponibilidadMejora()
    {
        bool v = (nivelActual <= NivelMaximo - 1) && GameManager.Instance.NivelActualCastillo >= nivelMinimoCastilloParaMejorar[nivelActual]
            && (GameManager.Instance.Oro >= costeOroMejorar[nivelActual]);

        btnMejorar.interactable = v;
        btnMejorarInfo.interactable = v;
    }

    private void setUpCanvasValues()
    {



        txtLvlActual.text = (nivelActual + 1).ToString();
        txtDañoActual.text = danyoPorNivel[nivelActual].ToString();


        if (nivelActual < NivelMaximo)
        {

            txtMejora.text = costeOroMejorar[nivelActual].ToString();
        }
        else
        {

            btnMejorar.gameObject.SetActive(false);
            btnMejorarInfo.gameObject.SetActive(false);
        }



    }
}
