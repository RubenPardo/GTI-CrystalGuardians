using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampa : Estructura
{
    public GameObject rangoExplosion;
    private bool destruir=false;
    private float detectedTime;
    
    public float delayExplosion = 5f;//son segundos
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
        if(destruir && Time.time-detectedTime > delayExplosion)
        {
            Destroy(gameObject);
        }
       
    }
    private void OnTriggerEnter(Collider other)
    {
        //Vector3 posicionActual = transform.position;

        if (other.tag.Equals("Enemigo"))
        {
            rangoExplosion.SetActive(true);

            /*enemigo = other.GetComponent<enmigoScript>();
            enemigo.setCurrentHealth(enemigo.vida-20);*/
            destruir = true;
            detectedTime = Time.time;
        }
        
    }
}
