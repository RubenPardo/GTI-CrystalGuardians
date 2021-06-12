using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Muro : Estructura
{

    public Text txtMejora;
    
    public Text txtSaludActual;
    
    public Text txtLvlActual;
    public Button btnMejorar;
    public Button btnMejorarInfo;



    //prefabs muro
    public GameObject prefabNvl1;
    public GameObject prefabNvl2;
    public GameObject prefabNvl3;

        



    public override void abrirMenu()
    {
        canvas.SetActive(true);

    }
    public override void cerrarMenu()
    {
        canvas.SetActive(false);
    }
   

    // Start is called before the first frame update
    protected override void Start()
    {
        GameManager.Instance.Oro = GameManager.Instance.Oro - GameManager.costeConstruirMuro;
        base.Start();
        setUpCanvasValues();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        comprobarDisponibilidadMejora();
    }

    private void comprobarDisponibilidadMejora()
    {
        bool mejoraDisponible = (nivelActual <= NivelMaximo - 1)
            && GameManager.Instance.NivelActualCastillo >= nivelMinimoCastilloParaMejorar[nivelActual]
            && (GameManager.Instance.Oro >= costeOroMejorar[nivelActual]);

        if (mejoraDisponible)
        {
            if (!sistemaParticulasPosibleMejora.isEmitting)
            {
            sistemaParticulasPosibleMejora.Play();
            }
            enableButtonEstructura(btnMejorar, btnMejorarInfo);
        }
        else if (!mejoraDisponible)
        {
            sistemaParticulasPosibleMejora.Stop();
            disableButtonEstructura(btnMejorar, btnMejorarInfo);
        }

    }
    public override void mejorar()
    {
        bool mejoraDisponible = true;

        if ((nivelActual <= NivelMaximo - 1))
        {
            if (GameManager.Instance.NivelActualCastillo < nivelMinimoCastilloParaMejorar[nivelActual])
            {
                mejoraDisponible = false;
                GameManager.Instance.ShowMessage("Nivel de castillo insuficiente!");
            }else if (GameManager.Instance.Oro < costeOroMejorar[nivelActual])
            {
                mejoraDisponible = false;
                GameManager.Instance.ShowMessage("Oro insuficiente!");
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
            comprobarCambioPrefab();
            settearVida();

            // actualizar hud informacion
            setUpCanvasValues();

            //emitir particulas
            sistemaParticulasMejorar.Play();
        }
    }

    private void setUpCanvasValues()
    {


        
        txtLvlActual.text = "Muro Nivel "+(nivelActual + 1).ToString();
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

    public void comprobarCambioPrefab()
    {
        switch (nivelActual)
        {


            case 1:
                prefabNvl1.SetActive(false);
                prefabNvl2.SetActive(true);

                break;
            case 2:

                prefabNvl2.SetActive(false);
                prefabNvl3.SetActive(true);
                break;
        }
    }
}
