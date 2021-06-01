using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class HealScript : Hechizo
{
    private float healCoutDwon = 0f;
    private ArrayList aliadosEnRadio;
    //ticksPorSegundo = 1 , hara un tick de sanacion cada 1s
    public float ticksPorSegundo = 1f;
    internal static float aumentoRadio = 1f;
    public AudioSource sonidoHechizoCura;

    // Start is called before the first frame update
    void Start()
    {
        sonidoHechizoCura.Play();
        transform.parent.localScale = new Vector3(transform.parent.localScale.x * aumentoRadio, transform.parent.localScale.y* aumentoRadio, transform.parent.localScale.z * aumentoRadio);
        var mainAreaParticulas = areaParticualas.main;
        mainAreaParticulas.startSize = mainAreaParticulas.startSize.constant*aumentoRadio;

        var shapeSistemaParticulas = areaParticualas.shape;
        shapeSistemaParticulas.radius= shapeSistemaParticulas.radius*aumentoRadio;

        aliadosEnRadio = new ArrayList();
        spwanHechizo = Time.time;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Unidad"))
        {
            aliadosEnRadio.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Unidad"))
        {
            aliadosEnRadio.Remove(other.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time-spwanHechizo > duracionHechizo)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
        else
        {

            if (healCoutDwon <= 0f)
            {
                Aliado ali;
                int healHechizo;
                foreach (GameObject go in aliadosEnRadio)
                {
                    if (go != null)
                    {
                        ali = go.transform.parent.GetComponent<Aliado>();
                        healHechizo = (int)(statsHechizoPorNivel[nivelActual] * ali.vidaPorNivel[ali.nivelActual]);
                        ali.setCurrentHealth(ali.vidaActual + healHechizo);
                    }
                    
                }
                healCoutDwon = 1f / ticksPorSegundo;
            }
            healCoutDwon -= Time.deltaTime;
        }
    }
}
