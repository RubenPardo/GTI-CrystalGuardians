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
    public GameObject HUDHechizos;
    public Text txtRayosDisponibles;
    public Text txtHealsDisponibles;
    public Text txtBuffsDisponibles;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void comprobarDisponibilidadBotones()
    {

        // CAMBIO DE ICONO HEAL
        if (GameManager.Instance.HealsDisponibles<=0)
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
        if (GameManager.Instance.RayosDisponibles <= 0)
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
        if (GameManager.Instance.BuffsDisponibles <= 0)
        {
            // recursos insuficiente

            RawImage icono = btnBuff.GetComponent<RawImage>();
            icono.texture = texturaGrisBuff;
            btnBuff.interactable = false;
        }
        else
        {
            // disponible

            RawImage icono = btnBuff.GetComponent<RawImage>();
            icono.texture = texturaReadyBuff;
            btnBuff.interactable = true;


        }


    }

    public void lanzarHeal()
    {
        GameManager.Instance.HealsDisponibles--;
        Instantiate(healBluePrint);

    }

    public void lanzarRayo()
    {
        GameManager.Instance.RayosDisponibles--;
        Instantiate(rayoBluePrint);

    }

    public void lanzarBuff()
    {
        GameManager.Instance.BuffsDisponibles--;
        Instantiate(buffBluePrint);

    }
    // Update is called once per frame
    void Update()
    {
        comprobarDisponibilidadBotones();
        if (GameManager.Instance.CasasDeHechizosConstruidas==1)
        {
            habilitarCanvasHechizos(true);
        }
        else
        {
            habilitarCanvasHechizos(false);
        }
        actualizarHechizosDisponibles();
    }

    private void actualizarHechizosDisponibles()
    {
        txtBuffsDisponibles.text = GameManager.Instance.BuffsDisponibles.ToString();
        txtRayosDisponibles.text = GameManager.Instance.RayosDisponibles.ToString();
        txtHealsDisponibles.text = GameManager.Instance.HealsDisponibles.ToString();
    }

    private void habilitarCanvasHechizos(bool habilitado)
    {

        HUDHechizos.SetActive(habilitado);
    }

}
