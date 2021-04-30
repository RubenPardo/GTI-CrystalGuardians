using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerExplosion : MonoBehaviour
{
    public Trampa trampa;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        

        if (other.tag.Equals("Enemigo"))
        {
            EnemigoScript enemigo = other.GetComponent<EnemigoScript>();
            int damage = trampa.danyoPorNivel[trampa.nivelActual];
            enemigo.setCurrentHealth(enemigo.vidaActual - damage);
        }

    }
}
