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
   
    public Button btnCasaHechizos;
    public Button btnCuartel;
    public Button btnMina;
    public Button btnExtractor;
    public Button btnMuro;
    public Button btnTorre;
    public Button btnTrampa;


    private Castillo castillo;
    

    private void Start()
    {
        // coger los botones y deshabilitar los que requieran un nivel superior al del castillo 
        // y deshabilitar los que no puedes construir por recursos
    
        castillo = GameManager.Instance.castillo.GetComponent<Castillo>();
        comprobarDisponibilidadBotones();

    }

 
    private void Update()
    {
        
        comprobarDisponibilidadBotones();
    }

    private void comprobarDisponibilidadBotones()
    {
        // comprobar nivel y luego los recursos para habilitar o deshabilitar los botones

        // casa hechizos
        if(castillo.nivelActual < GameManager.nivelMinimoCastilloCasaHechizos)
        {
            // nivel insuficiente
            // cuando se haga con imagen poner esto tmb con la imagen que le toca
            // button.GetComponent<Image>().sprite = Image1;

            btnCasaHechizos.interactable = false;
           
        }
        else if (GameManager.Instance.Oro < GameManager.costeConstruirCasaHechizos)
        {
            // recursos insuficiente
            btnCasaHechizos.interactable = false;
        }
        else
        {
            // disponible
            btnCasaHechizos.interactable = true;
        }
        
        // cuartel de unidades
        btnCuartel.interactable = (castillo.nivelActual >= GameManager.nivelMinimoCastilloCuartel && GameManager.Instance.Oro >= GameManager.costeConstruirCuartel && GameManager.Instance.CuartelesConstruidos < GameManager.topeCuartelUnidades);
        // mina
        btnMina.interactable = (castillo.nivelActual >= GameManager.nivelMinimoCastilloMina && GameManager.Instance.Oro >= GameManager.costeConstruirMina);
        // obsidium
        btnExtractor.interactable = (castillo.nivelActual >= GameManager.nivelMinimoCastilloExtractor && GameManager.Instance.Oro >= GameManager.costeConstruirExtractor);
        // muro
        btnMuro.interactable = (castillo.nivelActual >= GameManager.nivelMinimoCastilloMuros && GameManager.Instance.Oro >= GameManager.costeConstruirMuro);
        // torre
        btnTorre.interactable = (castillo.nivelActual >= GameManager.nivelMinimoCastilloTorre && GameManager.Instance.Oro >= GameManager.costeConstruirTorre);
        // trampa
        btnTrampa.interactable = (castillo.nivelActual >= GameManager.nivelMinimoCastilloTrampa && GameManager.Instance.Oro >= GameManager.costeConstruirTrampa);


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
