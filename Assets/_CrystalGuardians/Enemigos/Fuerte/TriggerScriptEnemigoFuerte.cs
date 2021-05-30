using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScriptEnemigoFuerte : MonoBehaviour
{
    public int damage = 0;
    public bool destruir = false;
    private float detectedTime;
    public float delayAtaque = 5f;//son segundos

    void Update()
    {
        if (destruir && Time.time - detectedTime > delayAtaque)
        {
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        
        if ((other.GetComponentInParent<Estructura>()!=null && other.GetComponentInParent<Estructura>().tag.Equals("Estructura")  )
            || other.tag.Equals("Estructura") 
            || other.tag.Equals("Aliado"))
        {
            
            destruir = true;
            detectedTime = Time.time;

            Estructura estructura;
           
            Aliado aliado;
            if (other.GetComponentInParent<Estructura>().TryGetComponent<Estructura>(out estructura))
            {
                estructura.setCurrentHealth(estructura.vidaActual - damage);
            }
            else if(other.GetComponentInParent<Aliado>().TryGetComponent<Aliado>(out aliado))
            {
                aliado.setCurrentHealth(aliado.vidaActual - damage);
            }
        }
    }




}
