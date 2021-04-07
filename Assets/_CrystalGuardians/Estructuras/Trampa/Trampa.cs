using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampa : Estructura
{
    public override void mejorar()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.Oro = GameManager.Instance.Oro - GameManager.costeConstruirTrampa;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
