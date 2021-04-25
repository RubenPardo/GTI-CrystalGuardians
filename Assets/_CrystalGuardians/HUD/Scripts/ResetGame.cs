using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ResetGame : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject botonResetGame;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if no hay estructuras
        botonResetGame.SetActive(true);

    }
}
