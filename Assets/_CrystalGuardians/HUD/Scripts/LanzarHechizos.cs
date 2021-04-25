using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanzarHechizos : MonoBehaviour
{
    [Header("Heal")]
    public GameObject healBluePrint;
    public Button btnHeal;
    public Texture texturaReadyHeal;
    public Texture texturaGrisHeal;

    [Header("Rayo")]
    public GameObject rayoBluePrint;
    public Button btnRayo;
    public Texture texturaReadyRayo;
    public Texture texturaGrisRayo;

    [Header("Heal")]
    public GameObject buffBluePrint;
    public Button btnBuff;
    public Texture texturaReadyBuff;
    public Texture texturaGrisBuff;

    [Header("HUD")]
    public Canvas canvasHUD;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void comprobarDisponibilidadBotones()
    {
        // comprobar nivel y luego los recursos para habilitar o deshabilitar los botones

        /*// CAMBIO DE ICONO HEAL
        if (GameManager.Instance.Oro < GameManager.costeLanzarHeal)
        {
            // recursos insuficiente

            RawImage icono = btnHeal.GetComponent<RawImage>();
            icono.texture = texturaGrisHeal;
            btnHeal.interactable = false;
        }
        else
        {
            // disponible

            RawImage icono = btnHeal.GetComponent<RawImage>();
            icono.texture = texturaReadyHeal;
            btnHeal.interactable = true;


        }

        // CAMBIO DE ICONO Rayo
        if (GameManager.Instance.Oro < GameManager.costeLanzarRayo)
        {
            // recursos insuficiente

            RawImage icono = btnRayo.GetComponent<RawImage>();
            icono.texture = texturaGrisRayo;
            btnRayo.interactable = false;
        }
        else
        {
            // disponible

            RawImage icono = btnRayo.GetComponent<RawImage>();
            icono.texture = texturaReadyRayo;
            btnRayo.interactable = true;


        }

        // CAMBIO DE ICONO Buff
        if (GameManager.Instance.Oro < GameManager.costeLanzarBuff)
        {
            // recursos insuficiente

            RawImage icono = btnHeal.GetComponent<RawImage>();
            icono.texture = texturaGrisBuff;
            btnBuff.interactable = false;
        }
        else
        {
            // disponible

            RawImage icono = btnHeal.GetComponent<RawImage>();
            icono.texture = texturaReadyHeal;
            btnBuff.interactable = true;


        }*/


    }

    public void lanzarHeal()
    {

        Instantiate(healBluePrint);

    }

    public void lanzarRayo()
    {

        Instantiate(rayoBluePrint);

    }

    public void lanzarBuff()
    {

        Instantiate(buffBluePrint);

    }
    // Update is called once per frame
    void Update()
    {
        comprobarDisponibilidadBotones();
    }

    private void habilitarCanvas(bool habilitado)
    {

        canvasHUD.enabled = (habilitado);
    }

}
