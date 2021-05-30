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

    public GameObject prefabLvl1;
    public GameObject prefabLvl2;
    public GameObject prefabLvl3;

    public GameObject sueloSinMejora;
    public GameObject sueloConMejora;



// Storing different levels'
public GameObject[] levels;
    public int[] generacionObsidiumPorNivel;

    //particulas
    public GameObject particulasMejora;

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

    private void generarRecursos()
    {
        GameManager.Instance.Obsiidum = GameManager.Instance.Obsiidum + generacionObsidiumPorNivel[nivelActual] * Time.deltaTime;
    }

    public override void mejorar()
    {
        GameManager.Instance.Oro = GameManager.Instance.Oro - costeOroMejorar[nivelActual];

        comprobarCambiarPrefab();
        nivelActual++;
        // actualizar hud informacion
        setUpCanvasValues();
        settearVida();

        //emitir particulas
        ParticleSystem sistema = particulasMejora.GetComponent<ParticleSystem>();
        sistema.Play();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        setUpCanvasValues();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        comprobarDisponibilidadMejora();
        generarRecursos();
    }

    private void comprobarDisponibilidadMejora()
    {

        bool v = (nivelActual <= NivelMaximo - 1) && GameManager.Instance.NivelActualCastillo >= nivelMinimoCastilloParaMejorar[nivelActual]
            && (GameManager.Instance.Oro >= costeOroMejorar[nivelActual]);

        btnMejorar.interactable = v;
        btnMejorarInfo.enabled = v;

        
        if (v)
        {
            sueloConMejora.SetActive(true);
            sueloSinMejora.SetActive(false);
        }
        else
        {
            sueloConMejora.SetActive(false);
            sueloSinMejora.SetActive(true);
        }

    }

    private void setUpCanvasValues()
    {



        txtLvlActual.text = "Extractor Obsidium Nivel " + (nivelActual + 1).ToString();
        txtProduccionActual.text = generacionObsidiumPorNivel[nivelActual].ToString();
        txtSaludActual.text = vidaPorNivel[nivelActual].ToString();


        if (nivelActual < NivelMaximo) { 


            txtMejora.text = costeOroMejorar[nivelActual].ToString();

            
        }
        else
        {
            btnMejorar.gameObject.SetActive(false);
            btnMejorarInfo.gameObject.SetActive(false);
        }


    }
    private void comprobarCambiarPrefab()
    {

        if (nivelActual > 0 && // para que no se salga del array
             nivelMinimoCastilloParaMejorar[nivelActual - 1] < nivelMinimoCastilloParaMejorar[nivelActual])
        {
            // se cambia el prefab cuando el siguiente nivel minimo de castillo cambia
            // si el anterior es menor 
            if (nivelMinimoCastilloParaMejorar[nivelActual] == 1)
            {
                // prefab nivel 2
                prefabLvl1.SetActive(false);
                prefabLvl2.SetActive(true);


            }
            else
            {
                // prefab nivel 3
                prefabLvl2.SetActive(false);
                prefabLvl3.SetActive(true);


            }

        }


    }
    
}
