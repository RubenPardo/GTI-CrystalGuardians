using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muro : Estructura
{
    public override void abrirMenu()
    {
        canvas.SetActive(true);

    }
    public override void cerrarMenu()
    {
        canvas.SetActive(false);
    }
    public override void mejorar()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.Oro = GameManager.Instance.Oro - GameManager.costeConstruirMuro;
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
