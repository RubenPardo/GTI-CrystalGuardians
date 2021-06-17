using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteManager : MonoBehaviour
{
    private bool isMuted;
    public GameObject textoMuteGame;

    // Start is called before the first frame update
    void Start()
    {
        isMuted = false;
    }

    public void MutePress()
    {
        isMuted = !isMuted;
        AudioListener.pause = isMuted;
        if (isMuted)
        {
            textoMuteGame.GetComponent<Text>().text = "Activar sonido";
        }
        else
        {
            textoMuteGame.GetComponent<Text>().text = "Desactivar sonido";
        }
    }
    // Update is called once per frame
    
}
