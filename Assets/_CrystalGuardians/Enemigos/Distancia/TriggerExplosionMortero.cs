using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerExplosionMortero : MonoBehaviour
{
    public BalaMortero bala;
    private bool destruir;
    private float detectedTime;
    public float delayExplosion = 5f;//son segundos

    private void Update()
    {
        if (destruir && Time.time - detectedTime > delayExplosion)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Estructura estructura;
        if (other.tag.Equals("Estructura"))
        {
            detectedTime = Time.time;
            destruir = true;
            estructura = other.GetComponent<Estructura>();
            int damage = bala.damage;
            estructura.setCurrentHealth(estructura.vidaActual - damage);

            
                 
        }else if(other.transform.parent != null && other.transform.parent.CompareTag("Estructura"))
        {
            detectedTime = Time.time;
            destruir = true;
            estructura = other.transform.parent.GetComponent<Estructura>();
            int damage = bala.damage;
            estructura.setCurrentHealth(estructura.vidaActual - damage);
            
        }

    }
}
