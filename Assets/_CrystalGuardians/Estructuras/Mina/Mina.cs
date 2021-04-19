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
       
        /*GameManager.Instance.Oro = GameManager.Instance.Oro - GameManager.costeConstruirMina;
        GameManager.Instance.oroConstruido = true;*/
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

        generarRecursos();

    }

    public override void cerrarMenu()
    {
        canvas.SetActive(false);
    }

    public override void abrirMenu()
    {
        canvas.SetActive(true);
    }
}
