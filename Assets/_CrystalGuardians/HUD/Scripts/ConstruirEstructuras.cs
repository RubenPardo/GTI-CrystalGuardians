using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConstruirEstructuras : MonoBehaviour
{
    public GameObject CasaDeHechizos_blueprint;
    public GameObject CuartelUnidades_blueprint;
    public GameObject Mina_blueprint;
    public GameObject Extractor_blueprint;
    public GameObject Muro_blueprint;
    public GameObject Torre_blueprint;
    public GameObject Trampa_blueprint;

    public GameObject prefabBtnMina;
    public GameObject prefabBtnExtractor;
    public GameObject prefabBtnCuartel;
    public GameObject prefabBtnTorre;
    public GameObject prefabBtnTrampa;
    public GameObject prefabBtnMuro;
    public GameObject prefabBtnCasaHechizos;
   

    public Canvas canvasHUD;

   
    

    private void Start()
    {
       
        comprobarDisponibilidadBotones();

    }

 
    private void Update()
    {
     
        habilitarCanvas(!GameManager.Instance.seEstaConstruyendo);
        comprobarDisponibilidadBotones();
        
    }

    private void habilitarCanvas(bool habilitado)
    {

        canvasHUD.enabled = (habilitado);
    }

    private void comprobarDisponibilidadBotones()
    {
        // comprobar nivel y luego los recursos para habilitar o deshabilitar los botones

        // CAMBIO DE ICONO CASA DE HECHIZOS
        if (GameManager.Instance.NivelActualCastillo < GameManager.nivelMinimoCastilloCasaHechizos)
        {

            suficienteNivel(prefabBtnCasaHechizos, false);

        }
        else
        {
            habilitar(prefabBtnCasaHechizos, GameManager.Instance.Oro >= GameManager.costeConstruirCasaHechizos
                && GameManager.Instance.CasasDeHechizosConstruidas < GameManager.topeCasaHechizos);
        }

        

        // CAMBIO DE ICONO CUARTEL
        if (GameManager.Instance.NivelActualCastillo < GameManager.nivelMinimoCastilloCuartel)
        {

            suficienteNivel(prefabBtnCuartel, false);

        }
        else
        {
            habilitar(prefabBtnCuartel, GameManager.Instance.Oro >= GameManager.costeConstruirCuartel
               && GameManager.Instance.CuartelesConstruidos < GameManager.topeCuartelUnidades);
        }

       

        // CAMBIO DE ICONO MINA
        if (GameManager.Instance.NivelActualCastillo < GameManager.nivelMinimoCastilloMina)
        {

            suficienteNivel(prefabBtnMina, false);

        }
        else
        {
            habilitar(prefabBtnMina, GameManager.Instance.Oro >= GameManager.costeConstruirMina);
        }
       
        
        

        // CAMBIO DE ICONO EXTRACTOR
        if (GameManager.Instance.NivelActualCastillo < GameManager.nivelMinimoCastilloExtractor)
        {

            suficienteNivel(prefabBtnExtractor, false);

        }
        else
        {
            habilitar(prefabBtnExtractor, GameManager.Instance.Oro >= GameManager.costeConstruirExtractor);
        }

      

        // CAMBIO DE ICONO MURO
        if (GameManager.Instance.NivelActualCastillo < GameManager.nivelMinimoCastilloMuros)
        {

            suficienteNivel(prefabBtnMuro, false);

        }
        else
        {
            habilitar(prefabBtnMuro, GameManager.Instance.Oro >= GameManager.costeConstruirMuro);
        }
        

        // CAMBIO DE ICONO TORRE
        if (GameManager.Instance.NivelActualCastillo < GameManager.nivelMinimoCastilloTorre)
        {

            suficienteNivel(prefabBtnTorre, false);

        }
        else
        {
            habilitar(prefabBtnTorre, GameManager.Instance.Oro >= GameManager.costeConstruirTorre);
        }
        

        // CAMBIO DE ICONO TRAMPA
        if (GameManager.Instance.NivelActualCastillo < GameManager.nivelMinimoCastilloTrampa)
        {

            suficienteNivel(prefabBtnTrampa, false);

        }
        else
        {
            habilitar(prefabBtnTrampa, GameManager.Instance.Oro >= GameManager.costeConstruirTrampa);
        }

       

    }

    private void suficienteNivel(GameObject prefabBtn, bool v)
    {

        prefabBtn.GetComponent<BtnConstruccion>().EnoughLevel = (v);
    }

    private void habilitar(GameObject prefabBtn, bool v)
    {
        prefabBtn.GetComponent<BtnConstruccion>().Available = (v);
    }

    public void spawn_CasaDeHechizos_blueprint()
    {
        
        Instantiate(CasaDeHechizos_blueprint);

    }
    public void spawn_CuartelUnidades_blueprint()
    {
         
       
        Instantiate(CuartelUnidades_blueprint);
       
    }
    public void spawn_Mina()
    {

        GameManager.Instance.oroConstruido = true;
        Instantiate(Mina_blueprint);
        
    }
    public void spawn_Extractor()
    {  
        Instantiate(Extractor_blueprint);
        
    }
    public void spawn_Muro()
    {
        
    
        Instantiate(Muro_blueprint);
    }
    public void spawn_Torre()
    {
   
        Instantiate(Torre_blueprint);
 
    }
    public void spawn_Trampa()
    {
      
        
        Instantiate(Trampa_blueprint);
        
    }
}
