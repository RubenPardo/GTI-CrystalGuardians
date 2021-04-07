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



    private void Start()
    {
        // coger los botones y deshabilitar los que requieran un nivel superior al del castillo 
        // y deshabilitar los que no puedes construir por recursos

        GameManager.Instance.Oro = 1350;
        

        Debug.Log("start spawn" + GameManager.nivelMinimoCastilloCasaHechizos);



    }

    public void spawn_CasaDeHechizos_blueprint()
    {
        

        GameManager g = GameManager.Instance;
        Castillo castillo = g.castillo.GetComponent<Castillo>();
        
        // comprobar nivel y recursos
        if(castillo.nivelActual >= GameManager.nivelMinimoCastilloCasaHechizos && GameManager.Instance.Oro >= GameManager.costeConstruirCasaHechizos)
        {
            
            GameManager.Instance.Oro = GameManager.Instance.Oro - GameManager.costeConstruirCasaHechizos;
            Instantiate(CasaDeHechizos_blueprint);
        }
        else
        {
            //btnCasaHechizos.interactable = false;
        }

        
        

    }

    public void spawn_CuartelUnidades_blueprint()
    {
        
        GameManager g = GameManager.Instance;
        Castillo castillo = g.castillo.GetComponent<Castillo>();

        // comprobar nivel y recursos
        if (castillo.nivelActual >= GameManager.nivelMinimoCastilloCuartel && GameManager.Instance.Oro >= GameManager.costeConstruirCuartel)
        {
            
            GameManager.Instance.Oro = GameManager.Instance.Oro - GameManager.costeConstruirCuartel;
            Instantiate(CuartelUnidades_blueprint);
        }
        else
        {
            //btnCuartel.interactable = false;
        }

       
    }
    public void spawn_Mina()
    {
        
        GameManager g = GameManager.Instance;
        Castillo castillo = g.castillo.GetComponent<Castillo>();
      

        // comprobar nivel y recursos
        if (castillo.nivelActual >= GameManager.nivelMinimoCastilloMina && GameManager.Instance.Oro >= GameManager.costeConstruirMina)
        {
           
            GameManager.Instance.Oro = GameManager.Instance.Oro - GameManager.costeConstruirMina;
            Instantiate(Mina_blueprint);
        }
        else
        {
           // btnMina.interactable = false;
        }
       

    }
    public void spawn_Extractor()
    {
        
        GameManager g = GameManager.Instance;
        Castillo castillo = g.castillo.GetComponent<Castillo>();

        // comprobar nivel y recursos
        if (castillo.nivelActual >= GameManager.nivelMinimoCastilloExtractor && GameManager.Instance.Oro >= GameManager.costeConstruirExtractor)
        {
            
            GameManager.Instance.Oro = GameManager.Instance.Oro - GameManager.costeConstruirExtractor;
            Instantiate(Extractor_blueprint);
        }
        else
        {
            //btnExtractor.interactable = false;
        }
       


    }
    public void spawn_Muro()
    {
        
        GameManager g = GameManager.Instance;
        Castillo castillo = g.castillo.GetComponent<Castillo>();

        // comprobar nivel y recursos
        if (castillo.nivelActual >= GameManager.nivelMinimoCastilloMuros && GameManager.Instance.Oro >= GameManager.costeConstruirMuro)
        {
            
            GameManager.Instance.Oro = GameManager.Instance.Oro - GameManager.costeConstruirCasaHechizos;
            Instantiate(Muro_blueprint);
        }
        else
        {
           // btnMuro.interactable = false;
        }
       
        


    }
    public void spawn_Torre()
    {
        
        GameManager g = GameManager.Instance;
        Castillo castillo = g.castillo.GetComponent<Castillo>();

        // comprobar nivel y recursos
        if (castillo.nivelActual >= GameManager.nivelMinimoCastilloTorre && GameManager.Instance.Oro >= GameManager.costeConstruirTorre)
        {
            
            GameManager.Instance.Oro = GameManager.Instance.Oro - GameManager.costeConstruirTorre;
            Instantiate(Torre_blueprint);
        }
        else
        {
            //btnTorre.interactable = false;
        }
        

    }
    public void spawn_Trampa()
    {
        
        GameManager g = GameManager.Instance;
        Castillo castillo = g.castillo.GetComponent<Castillo>();

        // comprobar nivel y recursos
        if (castillo.nivelActual >= GameManager.nivelMinimoCastilloTrampa && GameManager.Instance.Oro >= GameManager.costeConstruirTrampa)
        {
            
            GameManager.Instance.Oro = GameManager.Instance.Oro - GameManager.costeConstruirTrampa;
            Instantiate(Trampa_blueprint);
        }
        else
        {
            //btnTrampa.interactable = false;
        }
        

    }
}
