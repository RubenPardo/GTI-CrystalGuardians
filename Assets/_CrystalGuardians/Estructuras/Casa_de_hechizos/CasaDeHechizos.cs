using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CasaDeHechizos : Estructura
{
    public Text txtMejora;
    
    public Text txtSaludActual;

    public Text txtCosteHeal;
    public Text txtCosteRayo;
    public Text txtCosteBuff;
    
    public Text txtLvlActual;
    public Button btnMejorar;
    public Button btnMejorarInfo;
    public Button btnHeal;
    public Button btnRayo;
    public Button btnBuff;


    public GameObject prefabNvl1;
    public GameObject prefabNvl2;
    public GameObject prefabNvl3;



  


    public GameObject textoAvisoRonda;


    public override void abrirMenu()
    {
        if(canvas != null)
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

        bool mejoraDisponible = true;
        if ((nivelActual <= NivelMaximo - 1))
        {

            if (GameManager.Instance.NivelActualCastillo < nivelMinimoCastilloParaMejorar[nivelActual])
            {
                GameManager.Instance.ShowMessage("Nivel de castillo insuficiente!");
                mejoraDisponible = false;
            }
            else if ((GameManager.Instance.Oro < costeOroMejorar[nivelActual]))
            {
                GameManager.Instance.ShowMessage("Oro insuficiente!");
                mejoraDisponible = false;
            }
        }
        else
        {
            mejoraDisponible = false;
        }
        if (mejoraDisponible)
        {
            GameManager.Instance.Oro = GameManager.Instance.Oro - costeOroMejorar[nivelActual];

            nivelActual++;
            GameManager.nivelCasaHechizos++;
            // actualizar hud informacion
            setUpCanvasValues();
            settearVida();
            comprobarNivelCasa();

            //emitir particulas
            sistemaParticulasMejorar.Play();
        }

      
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        // canvas del menu de botones
        base.Start();
        // al empezar restar el oro
        GameManager.Instance.Oro = GameManager.Instance.Oro - GameManager.costeConstruirCasaHechizos;
        
        GameManager.nivelCasaHechizos = 0;
        GameManager.Instance.CasasDeHechizosConstruidas++;
        setUpCanvasValues();


    }
    public void comprobarNivelCasa()
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

    protected override void Update()
    {
        base.Update();
        setUpCanvasValues();
        comprobarDisponibilidadMejora();
        
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
        else if (!mejoraDisponible){
            sistemaParticulasPosibleMejora.Stop();

            disableButtonEstructura(btnMejorar, btnMejorarInfo);
        }
            
           

    }
    private void setUpCanvasValues()
    {

        
        txtLvlActual.text = "Casa de Hechizos Nivel " + (nivelActual + 1).ToString();
        txtSaludActual.text = vidaPorNivel[nivelActual].ToString();
        txtCosteHeal.text = GameManager.costeLanzarHeal[nivelActual].ToString();
        txtCosteRayo.text = GameManager.costeLanzarRayo[nivelActual].ToString();
        txtCosteBuff.text = GameManager.costeLanzarBuff[nivelActual].ToString();



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

    public void generarRayo()
    {
        if (obsidiumSuficienteLanzarHechizo(GameManager.costeLanzarRayo[nivelActual]) )
        {
            GameManager.Instance.Obsiidum -= GameManager.costeLanzarRayo[nivelActual];
            GameManager.Instance.RayosDisponibles++;
        }
       
    }
    public void generarHeal()
    {
        if (obsidiumSuficienteLanzarHechizo(GameManager.costeLanzarHeal[nivelActual]))
        {
            GameManager.Instance.Obsiidum -= GameManager.costeLanzarHeal[nivelActual];
            GameManager.Instance.HealsDisponibles++;
        }
           
    }
    public void generarBuff()
    {
        if (obsidiumSuficienteLanzarHechizo(GameManager.costeLanzarBuff[nivelActual])){
            GameManager.Instance.Obsiidum -= GameManager.costeLanzarBuff[nivelActual];
            GameManager.Instance.BuffsDisponibles++;
        }
    }

    private bool obsidiumSuficienteLanzarHechizo(int costeHechizo)
    {
        bool suficiente= true;
        if(GameManager.Instance.Obsiidum< costeHechizo)
        {
            suficiente = false;
            GameManager.Instance.ShowMessage("Obsidium insifuciente para crear el hechizo!");
        }


        return suficiente;
    }

    private void OnDestroy()
    {
        GameManager.Instance.CasasDeHechizosConstruidas--;
    }

}
