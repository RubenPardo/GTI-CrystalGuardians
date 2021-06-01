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

          
        if (suficienteNivel(GameManager.nivelMinimoCastilloCasaHechizos, prefabBtnCasaHechizos)
            && oroSufcienteParaConstruir(GameManager.costeConstruirCasaHechizos, prefabBtnCasaHechizos) 
            && topeEstructuasNoAlcanzado(GameManager.Instance.CasasDeHechizosConstruidas, GameManager.topeCasaHechizos,prefabBtnCasaHechizos))
        {
            habilitar(prefabBtnCasaHechizos, true);
        }
        else
        {
            habilitar(prefabBtnCasaHechizos, false);
        }

        // CAMBIO DE ICONO CUARTEL
        if (suficienteNivel(GameManager.nivelMinimoCastilloCuartel, prefabBtnCuartel)
          && oroSufcienteParaConstruir(GameManager.costeConstruirCuartel, prefabBtnCuartel)
          && topeEstructuasNoAlcanzado(GameManager.Instance.CuartelesConstruidos, GameManager.topeCuartelUnidades, prefabBtnCuartel))
        {
            habilitar(prefabBtnCuartel, true);
        }
        else
        {
            habilitar(prefabBtnCuartel, false);
        }


        // CAMBIO DE ICONO MINA

        if (suficienteNivel(GameManager.nivelMinimoCastilloMina, prefabBtnMina)
             && oroSufcienteParaConstruir(GameManager.costeConstruirMina, prefabBtnMina))
        {
            habilitar(prefabBtnMina, true);
        }
        else
        {
            habilitar(prefabBtnMina, false);
        }


        // CAMBIO DE ICONO EXTRACTOR
        if (suficienteNivel(GameManager.nivelMinimoCastilloExtractor, prefabBtnExtractor)
            && oroSufcienteParaConstruir(GameManager.costeConstruirExtractor, prefabBtnExtractor))
        {
            habilitar(prefabBtnExtractor, true);
        }
        else
        {
            habilitar(prefabBtnExtractor, false);
        }



        // CAMBIO DE ICONO MURO
        if (suficienteNivel(GameManager.nivelMinimoCastilloMuros, prefabBtnMuro)
            && oroSufcienteParaConstruir(GameManager.costeConstruirMuro, prefabBtnMuro))
        {
            habilitar(prefabBtnMuro, true);
        }
        else
        {
            habilitar(prefabBtnMuro, false);
        }

        // CAMBIO DE ICONO TORRE
        if (suficienteNivel(GameManager.nivelMinimoCastilloTorre, prefabBtnTorre)
            && oroSufcienteParaConstruir(GameManager.costeConstruirTorre, prefabBtnTorre))
        {
            habilitar(prefabBtnTorre, true);
        }
        else
        {
            habilitar(prefabBtnTorre, false);
        }


        // CAMBIO DE ICONO TRAMPA
        if (suficienteNivel(GameManager.nivelMinimoCastilloTrampa, prefabBtnTrampa)
            && oroSufcienteParaConstruir(GameManager.costeConstruirTrampa, prefabBtnTrampa))
        {
            habilitar(prefabBtnTrampa, true);
        }
        else
        {
            habilitar(prefabBtnTrampa, false);
        }
    }

    private bool suficienteNivel(int nivelCastilloRequerido, GameObject prefabBtn)
    {
        bool suficienteNivelConstruir = true;
        if (nivelCastilloRequerido > GameManager.Instance.NivelActualCastillo )
        {
            suficienteNivelConstruir = false;
            prefabBtn.GetComponent<BtnConstruccion>().EnoughLevel = false;
            prefabBtn.GetComponent<BtnConstruccion>().textNoSePuedeConstruir = "Nivel de castillo insuficiente!";
        }
        else
        {
            prefabBtn.GetComponent<BtnConstruccion>().EnoughLevel = true;
        }
        return suficienteNivelConstruir;
    }

    private void habilitar(GameObject prefabBtn, bool v)
    {
        prefabBtn.GetComponent<BtnConstruccion>().Available = v;
    }

    public void spawn_CasaDeHechizos_blueprint(BtnConstruccion btn)
    {
        if (btn.Available)
        {
            Instantiate(CasaDeHechizos_blueprint);
        }
        else
        {
            showMessage(btn.textNoSePuedeConstruir);
        }
    }
    public void spawn_CuartelUnidades_blueprint(BtnConstruccion btn)
    {

        if (btn.Available)
        {
            Instantiate(CuartelUnidades_blueprint);
        }
        else
        {
            showMessage(btn.textNoSePuedeConstruir);
        }
    }
    public void spawn_Mina(BtnConstruccion btn)
    {
        if (btn.Available)
        {
            GameManager.Instance.oroConstruido = true;
            Instantiate(Mina_blueprint);
        }
        else
        {
            showMessage(btn.textNoSePuedeConstruir);

        }
    }
    public void spawn_Extractor(BtnConstruccion btn)
    {
        if (btn.Available)
        {
            Instantiate(Extractor_blueprint);
        }
        else
        {
            showMessage(btn.textNoSePuedeConstruir);

        }
    }
    public void spawn_Muro(BtnConstruccion btn)
    {
        if (btn.Available)
        {
            Instantiate(Muro_blueprint);
        }
        else
        {
            showMessage(btn.textNoSePuedeConstruir);
        }
    }
    public void spawn_Torre(BtnConstruccion btn)
    {
        if (btn.Available)
        {
            Instantiate(Torre_blueprint);
        }
        else
        {
            showMessage(btn.textNoSePuedeConstruir);

        }
    }
    public void spawn_Trampa(BtnConstruccion btn)
    {
        if (btn.Available)
        {
            Instantiate(Trampa_blueprint);
        }
        else
        {
            showMessage(btn.textNoSePuedeConstruir);

        }
    }



    private bool oroSufcienteParaConstruir(int costeOro, GameObject btn) {
        bool oroDisponible = true;

        if (GameManager.Instance.Oro < costeOro)
        {
            btn.GetComponent<BtnConstruccion>().textNoSePuedeConstruir = "Oro insuficiente!";
            oroDisponible = false;
        }
        return oroDisponible;
    }

    private bool topeEstructuasNoAlcanzado(int estrcuturasActuales,int topeEstructuras, GameObject btn)
    {
        bool sePuedeConstruir = true;

        if (estrcuturasActuales >= topeEstructuras)
        {
            btn.GetComponent<BtnConstruccion>().textNoSePuedeConstruir = "No puedes construir mas estrscturas de ese tipo!";
            sePuedeConstruir = false;
        }
        return sePuedeConstruir;
    }
    private void showMessage(String text)
    {
        GameManager.Instance.ShowMessage(text);
    }
}
