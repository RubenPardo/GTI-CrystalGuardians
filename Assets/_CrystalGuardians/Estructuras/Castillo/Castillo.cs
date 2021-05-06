using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    //prefabs castillo 
    public GameObject prefabNvl1;
    public GameObject prefabNvl2;
    public GameObject prefabNvl3;

    



    public int[] costeObsidiumConstruirMejorar;

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
    public override void mejorar()
    {

        GameManager.Instance.Obsiidum = GameManager.Instance.Obsiidum - costeObsidiumMejorar[nivelActual];
        GameManager.Instance.Oro = GameManager.Instance.Oro - costeOroMejorar[nivelActual];

        nivelActual++;

        GameManager.Instance.NivelActualCastillo++;



        // actualizar hud informacion
        setUpCanvasValues();



        settearVida();
        comprobarNivelCastillo();

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
       && GameManager.Instance.Obsiidum >= costeObsidiumMejorar[GameManager.Instance.NivelActualCastillo];


        btnMejorarInfo.enabled = (nivelActual <= NivelMaximo - 1) && (GameManager.Instance.Oro >= costeOroMejorar[nivelActual])
        && GameManager.Instance.Obsiidum >= costeObsidiumMejorar[nivelActual];



    }

    private void setUpCanvasValues()
    {

        

        txtLvlActual.text = (nivelActual + 1).ToString();
        txtSaludActual.text = vidaPorNivel[nivelActual].ToString();
        
        if (nivelActual < NivelMaximo)
        {
            txtSaludMejorada.text = vidaPorNivel[nivelActual + 1].ToString();
            txtLvlSiguiente.text = (nivelActual + 2).ToString();
            txtMejoraOro.text = costeOroMejorar[nivelActual].ToString();
            txtMejoraObsidium.text = costeObsidiumConstruirMejorar[nivelActual + 1].ToString();
        }
        else
        {
            txtSaludMejorada.text = "---";
            txtLvlSiguiente.text = "---";
            txtMejoraOro.text = "---";
            txtMejoraObsidium.text = "---";
        }

        


    }

    private void OnDestroy()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void comprobarNivelCastillo()
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
