using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{

    public Text textOro;

    public void UpdateStats()
    {

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
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMainMenu()
    {

    }
}
