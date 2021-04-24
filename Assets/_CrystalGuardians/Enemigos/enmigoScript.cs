using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class enmigoScript : MonoBehaviour
{
    public int vida = 100;
    public float speed;
    public float rangoVision;
    public float rangoAtaque;
    private bool dir;

    public HealthBarScript healthBar;

    GameObject[] estructurasUnidades;
    Dictionary<GameObject, float> dictDistancias;
    GameObject objetivoFijado;

    private bool isMoving;
    private bool isObjetivoFijado;
    private bool isAtacking;

    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        dir = true;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        moverEnemigo();
        //Cambiar a mover a estructuras o enemigos
        /*float x;
        if (dir)
        {
            x = transform.position.x + speed * Time.deltaTime;
        }
        else {
            x = transform.position.x - speed * Time.deltaTime;
        }
        transform.position = new Vector3(x, transform.position.y, transform.position.z);


        if (transform.position.x >= 8f)
        {
            dir = false;
        }
        else if (transform.position.x <= -8f) {

            dir = true;
        }*/

        //-----
        //No cambiar
        if(vida <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void moverEnemigo()
    {
       

        if(!isMoving)
        {
            
            GameObject[] estructurasUnidades = Utility.unirDosArrays(
                GameObject.FindGameObjectsWithTag("Estructura"), 
                GameObject.FindGameObjectsWithTag("Unidad"));

            dictDistancias = new Dictionary<GameObject, float>();
            
            if (!isObjetivoFijado && estructurasUnidades.Length > 0)// intentar fijar un enemigo
            {
                foreach (GameObject objetivo in estructurasUnidades)
                {
                    // distancia enemigos
                    Vector3 pOrigen = transform.position;
                    Vector3 pEnemigo = objetivo.transform.position;

                    dictDistancias.Add(objetivo, Vector3.Distance(pOrigen, pEnemigo));

                }
                // ordenamos por distancia de menos a mas
                List<KeyValuePair<GameObject, float>> enemigosDistanciaOrdered = dictDistancias.ToList();
                enemigosDistanciaOrdered.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));
                float distanciaMasCorta = enemigosDistanciaOrdered[0].Value;

                if (distanciaMasCorta <= rangoVision)
                {
                    objetivoFijado = enemigosDistanciaOrdered[0].Key;
                    agent.SetDestination(objetivoFijado.transform.position);
                    isObjetivoFijado = true;
                    isMoving = true;


                }


            }





        }
        else
        {
            if (isObjetivoFijado)
            {
                if (Vector3.Distance(transform.position, objetivoFijado.transform.position) <= rangoAtaque)
                {
                    // el enemigo esta dentro del rango de ataque

                    // parar el agent y true el flag de atacar

                    isMoving = false;
                    agent.SetDestination(this.transform.position);

                    isAtacking = true;


                }
            }

            if (agent.remainingDistance == 0)// para cuando se pulsa a otra direccion hay que comprobar cuando para
            {

                isMoving = false;

            }
        }

        if (isAtacking)
        {

            // comprobar que el objetivo ha muerto antes de estos calculos
            if (Vector3.Distance(transform.position, objetivoFijado.transform.position) > rangoAtaque)
            {
                // si el enemigo sale del rango de ataque desfijarlo
                Debug.Log("Objetivo fuera de rango");
                isAtacking = false;
                isObjetivoFijado = false;
            }
            else
            {
                Debug.Log("Enemigo atacando");
                // si muere isEnemigoFijado
                // isAtacking = false;
                // isEnemigoFijado = false;
            }
        }
    }

    public void setCurrentHealth(int health)
    {
       
        healthBar.SetHeatlh(health);
        vida = health;
    }
}
