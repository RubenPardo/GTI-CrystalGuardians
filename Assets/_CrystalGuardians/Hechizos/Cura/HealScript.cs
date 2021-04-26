using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealScript : Hechizo
{
    private float healCoutDwon = 0f;
    private ArrayList aliadosEnRadio;
    //ticksPorSegundo = 1 , hara un tick de sanacion cada 1s
    public float ticksPorSegundo = 1f;
    // Start is called before the first frame update
    void Start()
    {
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
