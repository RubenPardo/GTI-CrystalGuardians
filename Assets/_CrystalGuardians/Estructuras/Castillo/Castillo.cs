using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Castillo : Estructura
{
    public Text txtSaludActual;
    public Text txtSaludMejorada;

    public Text txtLvlActual;
    public Text txtLvlSiguiente;
    public Button btnMejorar;
    public Button btnMejorarInfo;
    public Text txtMejoraOro;
    public Text txtMejoraObsidium;

    public int[] costeObsidiumMejorar;
    // Storing different levels'
    public GameObject[] levels;



    public int[] costeObsidiumConstruirMejorar;

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

        GameManager.Instance.Obsiidum = GameManager.Instance.Obsiidum - costeObsidiumConstruirMejorar[nivelActual];
        GameManager.Instance.Oro = GameManager.Instance.Oro - costeOroMejorar[nivelActual];

        nivelActual++;

        GameManager.Instance.NivelActualCastillo++;



        // actualizar hud informacion
        setUpCanvasValues();



        settearVida();

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

        btnMejorar.enabled = (nivelActual <= NivelMaximo - 1) && (GameManager.Instance.Oro >= costeOroMejorar[GameManager.Instance.NivelActualCastillo])
       && GameManager.Instance.Obsiidum >= costeObsidiumConstruirMejorar[GameManager.Instance.NivelActualCastillo];


        btnMejorarInfo.enabled = (nivelActual <= NivelMaximo - 1) && (GameManager.Instance.Oro >= costeOroMejorar[nivelActual])
        && GameManager.Instance.Obsiidum >= costeObsidiumConstruirMejorar[nivelActual];



    }

    private void setUpCanvasValues()
    {



        txtLvlActual.text = (GameManager.Instance.NivelActualCastillo + 1).ToString();
        txtSaludActual.text = vidaPorNivel[GameManager.Instance.NivelActualCastillo].ToString();

        if (nivelActual < NivelMaximo)
        {
            txtSaludMejorada.text = vidaPorNivel[GameManager.Instance.NivelActualCastillo + 1].ToString();
            txtLvlSiguiente.text = (GameManager.Instance.NivelActualCastillo + 2).ToString();
            txtMejoraOro.text = costeOroMejorar[GameManager.Instance.NivelActualCastillo].ToString();
            txtMejoraObsidium.text = costeObsidiumConstruirMejorar[GameManager.Instance.NivelActualCastillo + 1].ToString();
        }
        else
        {
            txtSaludMejorada.text = "---";
            txtLvlSiguiente.text = "---";
            txtMejoraOro.text = "---";
            txtMejoraObsidium.text = "---";
        }




    }
}
