using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mina : Estructura
{


    public int[] generacionOroPorNivel;

    private void generarRecursos()
    {
        //GameManager.Instance.Oro += 1 * Time.deltaTime; //mina lvl-1
        GameManager.Instance.Oro = GameManager.Instance.Oro + generacionOroPorNivel[nivelActual] * Time.deltaTime;
    }

    public override void mejorar()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Este se llama nada mas empezar el juego, NO CUANDO SE CONSTRUYE LA ESTRUCTURA
        //GameManager.Instance.Oro = GameManager.Instance.Oro - GameManager.costeConstruirMina;
        //GameManager.Instance.oroConstruido = true;
    }

    // Update is called once per frame
    void Update()
    {

        generarRecursos();

    }
}
