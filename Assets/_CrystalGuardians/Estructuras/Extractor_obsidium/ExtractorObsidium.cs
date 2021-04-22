using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtractorObsidium : Estructura
{

    public int[] generacionObsidiumPorNivel;

    public override void abrirMenu()
    {
        canvas.SetActive(true);
    }

    public override void cerrarMenu()
    {
         canvas.SetActive(false);
    }

    private void generarRecursos()
    {
        GameManager.Instance.Obsiidum = GameManager.Instance.Obsiidum + generacionObsidiumPorNivel[nivelActual] * Time.deltaTime;
    }

    public override void mejorar()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        //GameManager.Instance.Oro = GameManager.Instance.Oro - GameManager.costeConstruirExtractor;
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
}
