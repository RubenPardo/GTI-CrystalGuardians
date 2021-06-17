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

   
        


    public GameObject colliderExplosion;

   

    public int[] danyoPorNivel;
    public override void mejorar()
    {


        bool mejoraDisponible = true;

        if (nivelActual <= NivelMaximo - 1)
        {
            if (GameManager.Instance.NivelActualCastillo < nivelMinimoCastilloParaMejorar[nivelActual])
            {
                mejoraDisponible = false;
                GameManager.Instance.ShowMessage("¡Nivel de castillo insuficiente!");
            }else if (GameManager.Instance.Oro < costeOroMejorar[nivelActual+1])
            {
                mejoraDisponible = false;
                GameManager.Instance.ShowMessage("¡Oro insuficiente!");
            }
        }
        else
        {
            mejoraDisponible = false;
        }

        if (mejoraDisponible)
        {
            
            nivelActual++;
            updateRecursos(true, true, costeOroMejorar[nivelActual], transform);

            //cambio de prefab 
            trampaInactivaNvl1.SetActive(false);
            trampaInactivaNvl2.SetActive(true);
            colliderTrampa colliderTrampa = colliderExplosion.GetComponent<colliderTrampa>();
            colliderTrampa.rangoExplosion = trampaActivaNvl2;

            // actualizar hud informacion
            setUpCanvasValues();

            //emitir particulas
            sistemaParticulasMejorar.Play();
        }
        

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
        sistemaParticulasMejorar = particulasMejora.GetComponent<ParticleSystem>();
        sistemaParticulasPosibleMejora = particulasPosibleMejora.GetComponent<ParticleSystem>();

        updateRecursos(true, true, GameManager.costeConstruirTrampa, transform);


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
        bool mejoraDisponible = (nivelActual <= NivelMaximo - 1) && GameManager.Instance.NivelActualCastillo >= nivelMinimoCastilloParaMejorar[nivelActual]
            && (GameManager.Instance.Oro >= costeOroMejorar[nivelActual+1]);

        if (mejoraDisponible)
        {
            enableButtonEstructura(btnMejorar, btnMejorarInfo);
            if(!sistemaParticulasPosibleMejora.isEmitting)
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



        txtLvlActual.text = "Trampa Nivel "+(nivelActual + 1).ToString();
        txtDañoActual.text = danyoPorNivel[nivelActual].ToString();


        if (nivelActual < NivelMaximo)
        {

            txtMejora.text = costeOroMejorar[nivelActual+1].ToString();
        }
        else
        {

            btnMejorar.gameObject.SetActive(false);
            btnMejorarInfo.gameObject.SetActive(false);
        }



    }
}
