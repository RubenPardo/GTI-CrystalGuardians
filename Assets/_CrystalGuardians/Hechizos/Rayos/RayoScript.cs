using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayoScript : Hechizo
{
    // Start is called before the first frame update
    void Start()
    {
        spwanHechizo = Time.time;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Enemigo"))
        {
            EnemigoScript enemigo = other.GetComponent<EnemigoScript>();
            //El daño del rayo sera en % de vida
            nivelActual = GameManager.nivelCasaHechizos;
            int damageHechizo = (int) ( statsHechizoPorNivel[nivelActual] * enemigo.vidaPorNivel[enemigo.nivelActual]);
            enemigo.setCurrentHealth(enemigo.vidaActual - damageHechizo);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - spwanHechizo > duracionHechizo)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
