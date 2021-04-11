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

    public Texture texturaReadyMina;
    public Texture texturaGrisMina;
    public Texture texturaRojaMina;
    public Texture texturaReadyExtractor;
    public Texture texturaGrisExtractor;
    public Texture texturaRojaExtractor;
    public Texture texturaReadyCuartel;
    public Texture texturaGrisCuartel;
    public Texture texturaRojaCuartel;
    public Texture texturaReadyTorre;
    public Texture texturaGrisTorre;
    public Texture texturaRojaTorre;
    public Texture texturaReadyTrampa;
    public Texture texturaGrisTrampa;
    public Texture texturaRojaTrampa;
    public Texture texturaReadyMuro;
    public Texture texturaGrisMuro;
    public Texture texturaRojaMuro;
    public Texture texturaReadyHechizos;
    public Texture texturaGrisHechizos;
    public Texture texturaRojaHechizos;


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

        // CAMBIO DE ICONO CASA DE HECHIZOS
        if(castillo.nivelActual < GameManager.nivelMinimoCastilloCasaHechizos)
        {
            // nivel insuficiente
            // cuando se haga con imagen poner esto tmb con la imagen que le toca
            // button.GetComponent<Image>().sprite = Image1;

            RawImage icono = btnCasaHechizos.GetComponent<RawImage>();
            icono.texture = texturaGrisHechizos;

            habilitar(btnCasaHechizos,false);
           
        }
        else if (GameManager.Instance.Oro < GameManager.costeConstruirCasaHechizos)
        {
            // recursos insuficiente

            RawImage icono = btnCasaHechizos.GetComponent<RawImage>();
            icono.texture = texturaRojaHechizos;
            habilitar(btnCasaHechizos, false);
        }
        else
        {
            // disponible

            RawImage icono = btnCasaHechizos.GetComponent<RawImage>();
            icono.texture = texturaReadyHechizos;
            habilitar(btnCasaHechizos, true);

            
        }

        // CAMBIO DE ICONO CUARTEL
        //habilitar(btnCuartel, (castillo.nivelActual >= GameManager.nivelMinimoCastilloCuartel && GameManager.Instance.Oro >= GameManager.costeConstruirCuartel && GameManager.Instance.CuartelesConstruidos < GameManager.topeCuartelUnidades));
        if (castillo.nivelActual < GameManager.nivelMinimoCastilloCuartel)
        {
            // nivel insuficiente
            // cuando se haga con imagen poner esto tmb con la imagen que le toca

            RawImage icono = btnCuartel.GetComponent<RawImage>();
            icono.texture = texturaGrisCuartel;


            habilitar(btnCuartel, false);
        }
        else if (GameManager.Instance.Oro < GameManager.costeConstruirCuartel)
        {
            // recursos insuficiente

            RawImage icono = btnCuartel.GetComponent<RawImage>();
            icono.texture = texturaRojaCuartel;

            habilitar(btnCuartel, false);
        }
        else
        {
            // disponible

            RawImage icono = btnCuartel.GetComponent<RawImage>();
            icono.texture = texturaReadyCuartel;

            habilitar(btnCuartel, true);
        }

        // CAMBIO DE ICONO MINA
        //habilitar( btnMina,castillo.nivelActual >= GameManager.nivelMinimoCastilloMina && GameManager.Instance.Oro >= GameManager.costeConstruirMina);
        if (castillo.nivelActual < GameManager.nivelMinimoCastilloMina)
        {
            // nivel insuficiente
            // cuando se haga con imagen poner esto tmb con la imagen que le toca

            RawImage icono = btnMina.GetComponent<RawImage>();
            icono.texture =  texturaGrisMina;


            habilitar(btnMina, false);
        }
        else if (GameManager.Instance.Oro < GameManager.costeConstruirMina)
        {
            // recursos insuficiente

            RawImage icono = btnMina.GetComponent<RawImage>();
            icono.texture = texturaRojaMina;

            habilitar(btnMina, false);
        }
        else
        {
            // disponible

            RawImage icono = btnMina.GetComponent<RawImage>();
            icono.texture = texturaReadyMina;

            habilitar(btnMina, true);
        }

        // CAMBIO DE ICONO EXTRACTOR
        //habilitar( btnExtractor,castillo.nivelActual >= GameManager.nivelMinimoCastilloExtractor && GameManager.Instance.Oro >= GameManager.costeConstruirExtractor);
        if (castillo.nivelActual < GameManager.nivelMinimoCastilloExtractor)
        {
            // nivel insuficiente
            // cuando se haga con imagen poner esto tmb con la imagen que le toca
            // button.GetComponent<Image>().sprite = Image1;

            RawImage icono = btnExtractor.GetComponent<RawImage>();
            icono.texture = texturaGrisExtractor;

            habilitar(btnExtractor, false);

        }
        else if (GameManager.Instance.Oro < GameManager.costeConstruirExtractor)
        {
            // recursos insuficiente

            RawImage icono = btnExtractor.GetComponent<RawImage>();
            icono.texture = texturaRojaExtractor;
            habilitar(btnExtractor, false);
        }
        else
        {
            // disponible

            RawImage icono = btnExtractor.GetComponent<RawImage>();
            icono.texture = texturaReadyExtractor;
            habilitar(btnExtractor, true);
        }

        // CAMBIO DE ICONO MURO
        //habilitar(btnMuro,castillo.nivelActual >= GameManager.nivelMinimoCastilloMuros && GameManager.Instance.Oro >= GameManager.costeConstruirMuro);
        if (castillo.nivelActual < GameManager.nivelMinimoCastilloMuros)
        {
            // nivel insuficiente
            // cuando se haga con imagen poner esto tmb con la imagen que le toca
            // button.GetComponent<Image>().sprite = Image1;

            RawImage icono = btnMuro.GetComponent<RawImage>();
            icono.texture = texturaGrisMuro;

            habilitar(btnMuro, false);

        }
        else if (GameManager.Instance.Oro < GameManager.costeConstruirMuro)
        {
            // recursos insuficiente

            RawImage icono = btnMuro.GetComponent<RawImage>();
            icono.texture = texturaRojaMuro;
            habilitar(btnMuro, false);
        }
        else
        {
            // disponible

            RawImage icono = btnMuro.GetComponent<RawImage>();
            icono.texture = texturaReadyMuro;
            habilitar(btnMuro, true);
        }
        // CAMBIO DE ICONO TORRE
        //habilitar(btnTorre,castillo.nivelActual >= GameManager.nivelMinimoCastilloTorre && GameManager.Instance.Oro >= GameManager.costeConstruirTorre);
        if (castillo.nivelActual < GameManager.nivelMinimoCastilloTorre)
        {
            // nivel insuficiente
            // cuando se haga con imagen poner esto tmb con la imagen que le toca
            // button.GetComponent<Image>().sprite = Image1;

            RawImage icono = btnTorre.GetComponent<RawImage>();
            icono.texture = texturaGrisTorre;

            habilitar(btnTorre, false);

        }
        else if (GameManager.Instance.Oro < GameManager.costeConstruirTorre)
        {
            // recursos insuficiente

            RawImage icono = btnTorre.GetComponent<RawImage>();
            icono.texture = texturaRojaTorre;
            habilitar(btnTorre, false);
        }
        else
        {
            // disponible
            RawImage icono = btnTorre.GetComponent<RawImage>();
            icono.texture = texturaReadyTorre;
            habilitar(btnTorre, true);
        }
        // CAMBIO DE ICONO TRAMPA
        //habilitar(btnTrampa,castillo.nivelActual >= GameManager.nivelMinimoCastilloTrampa && GameManager.Instance.Oro >= GameManager.costeConstruirTrampa);
        if (castillo.nivelActual < GameManager.nivelMinimoCastilloTrampa)
        {
            // nivel insuficiente
            // cuando se haga con imagen poner esto tmb con la imagen que le toca
            // button.GetComponent<Image>().sprite = Image1;

            RawImage icono = btnTrampa.GetComponent<RawImage>();
            icono.texture = texturaGrisTrampa;

            habilitar(btnTrampa, false);

        }
        else if (GameManager.Instance.Oro < GameManager.costeConstruirTrampa)
        {
            // recursos insuficiente

            RawImage icono = btnTrampa.GetComponent<RawImage>();
            icono.texture = texturaRojaTrampa;
            habilitar(btnTrampa, false);
        }
        else
        {
            // disponible
            RawImage icono = btnTrampa.GetComponent<RawImage>();
            icono.texture = texturaReadyTrampa;
            habilitar(btnTrampa, true);
        }

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
