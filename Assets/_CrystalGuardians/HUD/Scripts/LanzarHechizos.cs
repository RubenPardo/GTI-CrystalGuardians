using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanzarHechizos : MonoBehaviour
{
    [Header("Heal")]
    public GameObject healBluePrint;
    public GameObject btnHeal;

    [Header("Rayo")]
    public GameObject rayoBluePrint;
    public GameObject btnRayo;

    [Header("Heal")]
    public GameObject buffBluePrint;
    public GameObject btnBuff;

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

        btnRayo.GetComponent<btnHechizo>().Available = GameManager.Instance.RayosDisponibles > 0;
        btnHeal.GetComponent<btnHechizo>().Available = GameManager.Instance.HealsDisponibles > 0;
        btnBuff.GetComponent<btnHechizo>().Available = GameManager.Instance.BuffsDisponibles > 0;
       

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
