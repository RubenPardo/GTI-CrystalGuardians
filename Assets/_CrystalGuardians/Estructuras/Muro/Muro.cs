using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Muro : Estructura
{

    public int[] capacidadUnidades;
    public Aliado guerrero;
    public Aliado ballestero;
    public float distanciaSpawn = 0.5f;

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
    // Counting current level
    int current_level = 0;

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
    }

    // Update is called once per frame
    private void Update()
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
    public override void mejorar()
    {


        current_level = current_level++;
        nivelActual = nivelActual + 1;



        GameManager.Instance.Oro = GameManager.Instance.Oro - costeOroMejorar[nivelActual];

        // actualizar hud informacion
        setUpCanvasValues();
    }

    private void setUpCanvasValues()
    {


        
        txtLvlActual.text = (nivelActual + 1).ToString();
        txtLvlSiguiente.text = (nivelActual + 2).ToString();
        
        txtMejora.text = costeOroMejorar[nivelActual].ToString();
        txtSaludActual.text = vidaPorNivel[nivelActual].ToString();
        txtSaludMejorada.text = vidaPorNivel[nivelActual + 1].ToString();
        

    }
}
