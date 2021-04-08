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

    public Canvas canvasHUD;

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
        GameManager.Instance.Oro += 50000 * Time.deltaTime;
        habilitarCanvas(!GameManager.Instance.seEstaConstruyendo);
        comprobarDisponibilidadBotones();
        

    }

    private void habilitar(Button btn, bool hablitado)
    {
        btn.interactable = hablitado;
    }
    private void habilitarCanvas(bool habilitado)
    {

        canvasHUD.enabled = (habilitado);
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

            habilitar(btnCasaHechizos,false);
           
        }
        else if (GameManager.Instance.Oro < GameManager.costeConstruirCasaHechizos)
        {
            // recursos insuficiente
            habilitar(btnCasaHechizos, false);
        }
        else
        {
            // disponible
            habilitar(btnCasaHechizos, true);
        }

        // cuartel de unidades
        habilitar(btnCuartel, (castillo.nivelActual >= GameManager.nivelMinimoCastilloCuartel && GameManager.Instance.Oro >= GameManager.costeConstruirCuartel && GameManager.Instance.CuartelesConstruidos < GameManager.topeCuartelUnidades));
        // mina
        habilitar ( btnMina,castillo.nivelActual >= GameManager.nivelMinimoCastilloMina && GameManager.Instance.Oro >= GameManager.costeConstruirMina);
        // obsidium
        habilitar( btnExtractor,castillo.nivelActual >= GameManager.nivelMinimoCastilloExtractor && GameManager.Instance.Oro >= GameManager.costeConstruirExtractor);
        // muro
        habilitar(btnMuro,castillo.nivelActual >= GameManager.nivelMinimoCastilloMuros && GameManager.Instance.Oro >= GameManager.costeConstruirMuro);
        // torre
        habilitar(btnTorre,castillo.nivelActual >= GameManager.nivelMinimoCastilloTorre && GameManager.Instance.Oro >= GameManager.costeConstruirTorre);
        // trampa
        habilitar(btnTrampa,castillo.nivelActual >= GameManager.nivelMinimoCastilloTrampa && GameManager.Instance.Oro >= GameManager.costeConstruirTrampa);


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
