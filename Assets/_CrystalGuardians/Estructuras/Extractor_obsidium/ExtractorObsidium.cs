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




// Storing different levels'
public GameObject[] levels;
    public int[] generacionObsidiumPorNivel;

  

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
        GameManager.Instance.ObsidiumTotalGenerado = GameManager.Instance.ObsidiumTotalGenerado +  (int)(generacionObsidiumPorNivel[nivelActual] * Time.deltaTime);

    }

    public override void mejorar()
    {

        bool mejoraDisponible = true;

        if ((nivelActual <= NivelMaximo - 1))
        {
            if (GameManager.Instance.NivelActualCastillo < nivelMinimoCastilloParaMejorar[nivelActual])
            {
                GameManager.Instance.ShowMessage("Nivel de castillo insuficiente!");
                mejoraDisponible = false;

            }
            else if((GameManager.Instance.Oro < costeOroMejorar[nivelActual]))
            {
                mejoraDisponible = false;
                GameManager.Instance.ShowMessage("Oro insuficiente");
            }
        }
        else
        {
            mejoraDisponible = false;
        }

        if (mejoraDisponible)
        {
            GameManager.Instance.Oro = GameManager.Instance.Oro - costeOroMejorar[nivelActual];
            comprobarCambiarPrefab();
            nivelActual++;

            // actualizar hud informacion
            setUpCanvasValues();
            settearVida();

            //emitir particulas
            sistemaParticulasMejorar.Play();
        }
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

        bool mejoraDisponible = (nivelActual <= NivelMaximo - 1) && GameManager.Instance.NivelActualCastillo >= nivelMinimoCastilloParaMejorar[nivelActual]
            && (GameManager.Instance.Oro >= costeOroMejorar[nivelActual]);


        if (mejoraDisponible && !sistemaParticulasPosibleMejora.isEmitting)
        {
            enableButtonEstructura(btnMejorar, btnMejorarInfo);
            sistemaParticulasPosibleMejora.Play();
        }
        else if (!mejoraDisponible)
        {
            disableButtonEstructura(btnMejorar, btnMejorarInfo);
            sistemaParticulasPosibleMejora.Stop();
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
