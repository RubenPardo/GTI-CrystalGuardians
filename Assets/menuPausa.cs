using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuPausa : MonoBehaviour
{
    public static bool juegoPausado = false;
    public GameObject menuPausaUI;
    public GameObject HUD;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (juegoPausado)
            {
                Continuar();
            }
            else
            {
                Pausa();
            }
        }
    }

    public void Continuar()
    {
        menuPausaUI.SetActive(false);
        Time.timeScale = 1f;
        juegoPausado = false;
        HUD.SetActive(true);
    }
    public void Pausa()
    {
        menuPausaUI.SetActive(true);
        Time.timeScale = 0f;
        juegoPausado = true;
        HUD.SetActive(false);
    }
    
    public void SalirDelJuego()
    {
        Debug.Log("Saliendo del juego");
        Application.Quit();
    }
    public void ReiniciarPartida()
    {

        Debug.Log("Partida Reiniciada");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //Time.timeScale = 1f;

    }
}
