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

    //prefabs castillo 
    public GameObject prefabNvl1;
    public GameObject prefabNvl2;
    public GameObject prefabNvl3;


    // controlador del shake camera para cuando le peguen
    public HUDShake cameraShake;
    


    public int[] costeObsidiumConstruirMejorar;

    bool possibleSueloMejora = false;
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
        //if()//comprobacion oro o obsidium - nivel castillo 
        GameManager.Instance.Obsiidum = GameManager.Instance.Obsiidum - costeObsidiumMejorar[nivelActual];
        GameManager.Instance.Oro = GameManager.Instance.Oro - costeOroMejorar[nivelActual];

        nivelActual++;

        GameManager.Instance.NivelActualCastillo++;



        // actualizar hud informacion
        setUpCanvasValues();



        settearVida();
        comprobarNivelCastillo();

        //emitir particulas
       
        sistemaParticulasMejorar.Play();

    }

    // Start is called before the first frame update
    protected override void Start()
    {
        GameManager.listaEstructurasEnJuego.Add(this.gameObject);
        base.Start();
        

    }
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        setUpCanvasValues();
        comprobarDisponibilidadMejora();

    }

    private void comprobarDisponibilidadMejora()
    {
       
        bool inte = (nivelActual <= NivelMaximo - 1) && (GameManager.Instance.Oro >= costeOroMejorar[GameManager.Instance.NivelActualCastillo])
       && GameManager.Instance.Obsiidum >= costeObsidiumMejorar[GameManager.Instance.NivelActualCastillo];
        
        btnMejorar.interactable = inte;
        btnMejorarInfo.interactable = inte;

        if (inte && !sistemaParticulasPosibleMejora.isEmitting)
        {
            sistemaParticulasPosibleMejora.Play();
        }
        else if (!inte)
        {
            sistemaParticulasPosibleMejora.Stop();
        }


    }

    private void setUpCanvasValues()
    {

        

        txtLvlActual.text = "Castillo Nivel "+(nivelActual + 1).ToString();
        txtSaludActual.text = vidaPorNivel[nivelActual].ToString();
        
        if (nivelActual < NivelMaximo)
        {
            txtMejoraOro.text = costeOroMejorar[nivelActual].ToString();
            txtMejoraObsidium.text = costeObsidiumConstruirMejorar[nivelActual + 1].ToString();
        }
        else
        {
            btnMejorar.gameObject.SetActive(false);
            btnMejorarInfo.gameObject.SetActive(false);
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


    public void onShakeCamera()
    {
        cameraShake.shouldShake = true;
    }
}
