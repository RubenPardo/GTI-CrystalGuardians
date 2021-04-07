using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasaDeHechizos : Estructura
{
    public override void mejorar()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        // al empezar restar el oro
        GameManager.Instance.Oro = GameManager.Instance.Oro - GameManager.costeConstruirCasaHechizos;
        Debug.Log(GameManager.Instance.Oro);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
