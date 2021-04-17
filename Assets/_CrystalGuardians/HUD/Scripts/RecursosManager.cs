using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecursosManager : MonoBehaviour
{

    public Text textOro;
    public Text textObsidium;
    public Text textTopeUnidades;
    public Text textActualUnidades;

    // Start is called before the first frame update
    void Start()
    {
        //textHud = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {


        updateOro();
        updateObsidium();
        updateUnidades();
        
        
    }

    private void updateUnidades()
    {
        textTopeUnidades.text = GameManager.Instance.TopeUnidades.ToString() ;
    }

    private void updateObsidium()
    {
        if (GameManager.Instance.Obsiidum >= 1000 && GameManager.Instance.Obsiidum < 1000000)
        {

            float cantidadRedondeada = GameManager.Instance.Obsiidum / 1000;
            //Debug.Log(cantidadRedondeada);
            textObsidium.text = cantidadRedondeada.ToString("f2") + "k";
        }
        else if (GameManager.Instance.Obsiidum >= 1000000)
        {
            float cantidadRedondeada = GameManager.Instance.Obsiidum / 1000000;
            textObsidium.text = cantidadRedondeada.ToString("f2") + "M";
        }
        else
        {
            textObsidium.text = GameManager.Instance.Obsiidum.ToString("f0");
        }
    }

    private void updateOro()
    {
        if (GameManager.Instance.Oro >= 1000 && GameManager.Instance.Oro < 1000000)
        {

            float cantidadRedondeada = GameManager.Instance.Oro / 1000;
            //Debug.Log(cantidadRedondeada);
            textOro.text = cantidadRedondeada.ToString("f2") + "k";
        }
        else if (GameManager.Instance.Oro >= 1000000)
        {
            float cantidadRedondeada = GameManager.Instance.Oro / 1000000;
            textOro.text = cantidadRedondeada.ToString("f2") + "M";
        }
        else
        {
            textOro.text = GameManager.Instance.Oro.ToString("f0");
        }
    }
}
