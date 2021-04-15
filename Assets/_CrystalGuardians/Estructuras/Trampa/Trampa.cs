using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampa : Estructura
{
    public override void mejorar()
    {
        throw new System.NotImplementedException();
    }

    public override void abrirMenu()
    {
        canvas.SetActive(true);
    }
    public override void cerrarMenu()
    {
        canvas.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.Oro = GameManager.Instance.Oro - GameManager.costeConstruirTrampa;
        // canvas del menu de botones
        canvas = gameObject.transform.Find("Canvas").gameObject;
        if (canvas != null)
        {
            canvas.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
