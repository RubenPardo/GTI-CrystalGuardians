using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayoScript : Hechizo
{
    public int mejoraDanyoRayo = 1;
    public static float aumentoRadio = 1f;//mejora de aldea
                                          // Start is called before the first frame update
    public AudioSource sonidoHechizoRayo;

    void Start()
       
    {
        sonidoHechizoRayo.Play();

        transform.parent.localScale = new Vector3(transform.parent.localScale.x * aumentoRadio, transform.parent.localScale.y * aumentoRadio, transform.parent.localScale.z * aumentoRadio);
        var mainAreaParticulas = areaParticualas.main;
        mainAreaParticulas.startSize = mainAreaParticulas.startSize.constant * aumentoRadio;

        var shapeSistemaParticulas = areaParticualas.shape;
        shapeSistemaParticulas.radius = shapeSistemaParticulas.radius * aumentoRadio;
        spwanHechizo = Time.time;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Enemigo"))
        {
            EnemigoScript enemigo = other.GetComponent<EnemigoScript>();
            //El da�o del rayo sera en % de vida
            nivelActual = GameManager.nivelCasaHechizos;
            int damageHechizo = (int) ( statsHechizoPorNivel[nivelActual] * enemigo.vidaPorNivel[enemigo.nivelActual]);
            enemigo.setCurrentHealth(enemigo.vidaActual - damageHechizo* mejoraDanyoRayo);
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
