using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trampa : Estructura
{

    public Text txtMejora;

    public Text txtDañoActual;
    public Text txtLvlActual;
    public Button btnMejorar;
    public Button btnMejorarInfo;

    public GameObject trampaInactivaNvl1;
    public GameObject trampaActivaNvl1;
    public GameObject trampaInactivaNvl2;
    public GameObject trampaActivaNvl2;

    public GameObject sueloConMejora;
        


public GameObject colliderExplosion;

    //particulas
    public GameObject particulasMejora;

    public int[] danyoPorNivel;
    public override void mejorar()
    {
        GameManager.Instance.Oro = GameManager.Instance.Oro - costeOroMejorar[nivelActual];

        
        nivelActual++;



        GameManager.Instance.Oro = GameManager.Instance.Oro - costeOroMejorar[nivelActual];

        //cambio de prefab 
        trampaInactivaNvl1.SetActive(false);
        trampaInactivaNvl2.SetActive(true);

        colliderTrampa colliderTrampa = colliderExplosion.GetComponent<colliderTrampa>();
        colliderTrampa.rangoExplosion = trampaActivaNvl2;

        // actualizar hud informacion
        setUpCanvasValues();

        //emitir particulas
        ParticleSystem sistema = particulasMejora.GetComponent<ParticleSystem>();
        sistema.Play();

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
        canvas = gameObject.transform.Find("Canvas").gameObject;
        if (canvas != null)
        {

            canvas.SetActive(false);
        }
        setUpCanvasValues();
    }

    // Update is called once per frame
    protected override void Update()
    {
        textNivelSubMenu.text = "Nivel " + (nivelActual + 1);
        comprobarDisponibilidadMejora();
    }

    private void comprobarDisponibilidadMejora()
    {
        bool v = (nivelActual <= NivelMaximo - 1) && GameManager.Instance.NivelActualCastillo >= nivelMinimoCastilloParaMejorar[nivelActual]
            && (GameManager.Instance.Oro >= costeOroMejorar[nivelActual]);

        btnMejorar.interactable = v;
        btnMejorarInfo.interactable = v;


        if (v)
        {
            sueloConMejora.SetActive(true);
            
        }
        else
        {
            sueloConMejora.SetActive(false);
            
        }

    }

    private void setUpCanvasValues()
    {



        txtLvlActual.text = "Trampa Nivel "+(nivelActual + 1).ToString();
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
