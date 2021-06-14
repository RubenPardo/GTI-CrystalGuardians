using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mina : Estructura
{
    public Text txtNivel;
    public Text txtMejora;

    public Text txtSaludActual;
    public Text txtProduccionActual;
    public Text txtLvlActual;
    public Button btnMejorar;
    public Button btnMejorarInfo;

    //prefabs mina
    public GameObject prefabLvl1;
    public GameObject prefabLvl2;
    public GameObject prefabLvl3;


    public bool isInstanciadoAlInicio = false;// para indicar si se pone al inicio del juego como base



    public int[] generacionOroPorNivel;

    public static float mejoraDeAldeaProduccionOro = 1;//100% = 1 


    private void generarRecursos()
    {
        //GameManager.Instance.Oro += 1 * Time.deltaTime; //mina lvl-1
        updateRecursos(true, false, generacionOroPorNivel[nivelActual] * Time.deltaTime * mejoraDeAldeaProduccionOro, transform);
        GameManager.Instance.OroTotalGenerado = GameManager.Instance.OroTotalGenerado + generacionOroPorNivel[nivelActual] * Time.deltaTime * mejoraDeAldeaProduccionOro;

    }

    public override void mejorar()
    {
        bool mejoraDisponible = (nivelActual <= NivelMaximo - 1)
    && GameManager.Instance.NivelActualCastillo >= nivelMinimoCastilloParaMejorar[nivelActual]
    && (GameManager.Instance.Oro >= costeOroMejorar[nivelActual]);

        if ((nivelActual <= NivelMaximo - 1))
        {
            if (GameManager.Instance.NivelActualCastillo < nivelMinimoCastilloParaMejorar[nivelActual])
            {
                GameManager.Instance.ShowMessage("Nivel de castillo insuficiente!");

            }else if (GameManager.Instance.Oro < costeOroMejorar[nivelActual])
            {
                GameManager.Instance.ShowMessage("Oro insuficiente!");
            }
        }
        else
        {
            mejoraDisponible = false;
        }
        if (mejoraDisponible)
        {
           
          
            updateRecursos(true,true, costeOroMejorar[nivelActual], transform);
            comprobarCambiarPrefab();
            nivelActual++;
            settearVida();

            // actualizar hud informacion
            setUpCanvasValues();

            //emitir particulas
            sistemaParticulasMejorar.Play();
        }
    }

   

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        if (!isInstanciadoAlInicio)
        {
            updateRecursos(true,true, GameManager.costeConstruirMina, transform);
        }
        setUpCanvasValues();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        comprobarDisponibilidadMejora();
        generarRecursos();
    }

    public override void cerrarMenu()
    {
        if (canvas != null)
        {
            canvas.SetActive(false);
        }
    }

    public override void abrirMenu()
    {
        if (canvas != null)
        {
            canvas.SetActive(true);
        }
    }

    private void comprobarDisponibilidadMejora()
    {

        bool mejoraDisponible = (nivelActual <= NivelMaximo - 1)
            && GameManager.Instance.NivelActualCastillo >= nivelMinimoCastilloParaMejorar[nivelActual]
            && (GameManager.Instance.Oro >= costeOroMejorar[nivelActual]);


        if (mejoraDisponible)
        {
            enableButtonEstructura(btnMejorar, btnMejorarInfo);
            if (!sistemaParticulasPosibleMejora.isEmitting)
            {
                sistemaParticulasPosibleMejora.Play();
            }
        }
        else if (!mejoraDisponible)
        {
            disableButtonEstructura(btnMejorar, btnMejorarInfo);
            sistemaParticulasPosibleMejora.Stop();
        }

    }

    private void setUpCanvasValues()
    {



        txtLvlActual.text = "Mina Nivel "+(nivelActual + 1).ToString();
        txtProduccionActual.text = generacionOroPorNivel[nivelActual].ToString();
        txtSaludActual.text = vidaPorNivel[nivelActual].ToString();

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
