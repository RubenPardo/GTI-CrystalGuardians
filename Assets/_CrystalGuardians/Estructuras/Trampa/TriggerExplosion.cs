using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerExplosion : MonoBehaviour
{
    public Trampa trampa;
    public GameObject prefabActivadaNvl1;
    public GameObject prefabActivadaNvl2;

    private void OnTriggerEnter(Collider other)
    {
        

        if (other.tag.Equals("Enemigo"))
        {
            EnemigoScript enemigo = other.GetComponent<EnemigoScript>();
            int damage = trampa.danyoPorNivel[trampa.nivelActual];
            enemigo.setCurrentHealth(enemigo.vidaActual - damage);
            //prefabActivadaNvl1.SetActive(true);
        }

    }
}
