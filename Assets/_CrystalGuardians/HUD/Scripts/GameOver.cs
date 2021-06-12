using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{

    public Text textOro;
    public Text textObsidium;
    public Text textUnidadesGeneradas;
    public Text textEnemigosEliminados;
    public Text textHechizosLanzados;
    public Text textEstructurasConstruidas;
    public Text textTiempoDeJuego;
    public Text textRondaMasAlta;

    public void UpdateStats()
    {
        textUnidadesGeneradas.text = GameManager.Instance.UnidadesAliadasTotalesGeneradas.ToString();
        textEnemigosEliminados.text = GameManager.Instance.EnemigosTotalesEliminados.ToString();
        textHechizosLanzados.text = GameManager.Instance.HechizosTotalesLanzados.ToString();
        textEstructurasConstruidas.text = GameManager.Instance.EstructurasTotalesConstruidas.ToString();
        textRondaMasAlta.text = GameManager.Instance.RondaMaximaAlcanzada.ToString();

        float time = Time.timeSinceLevelLoad;
        float seconds = Mathf.FloorToInt(time % 60);
        float minutes = Mathf.FloorToInt(time / 60);
        textTiempoDeJuego.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (GameManager.Instance.OroTotalGenerado >= 1000 && GameManager.Instance.OroTotalGenerado < 1000000)
        {
            float cantidadRedondeada = GameManager.Instance.OroTotalGenerado / 1000;
            textOro.text = cantidadRedondeada.ToString("f2") + "k";
        }
        else if (GameManager.Instance.OroTotalGenerado >= 1000000)
        {
            float cantidadRedondeada = GameManager.Instance.OroTotalGenerado / 1000000;
            textOro.text = cantidadRedondeada.ToString("f2") + "M";
        }
        else
        {
            textOro.text = GameManager.Instance.OroTotalGenerado.ToString("f0");
        }

        if (GameManager.Instance.ObsidiumTotalGenerado >= 1000 && GameManager.Instance.ObsidiumTotalGenerado < 1000000)
        {

            float cantidadRedondeada = GameManager.Instance.ObsidiumTotalGenerado / 1000;
            textObsidium.text = cantidadRedondeada.ToString("f2") + "k";
        }
        else if (GameManager.Instance.ObsidiumTotalGenerado >= 1000000)
        {
            float cantidadRedondeada = GameManager.Instance.ObsidiumTotalGenerado / 1000000;
            textObsidium.text = cantidadRedondeada.ToString("f2") + "M";
        }
        else
        {
            textObsidium.text = GameManager.Instance.ObsidiumTotalGenerado.ToString("f0");
        }
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
