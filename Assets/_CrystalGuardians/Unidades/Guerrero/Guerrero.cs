using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class Guerrero : Aliado
{

    // ----- 
    // movimiento
    GameObject[] enemigos;
    Dictionary<GameObject, float> enemigosDistancias; // enemigo, distancia
    NavMeshAgent agent;

    GameObject enemigoFijado;
    

    void Start()
    {

        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!isMoving)
        {
            enemigos = GameObject.FindGameObjectsWithTag("Enemigo"); // obtener todos los enemigos de la escena
            enemigosDistancias = new Dictionary<GameObject, float>();


            if (!isEnemigoFijado)// intentar fijar un enemigo
            {
                foreach (GameObject enemigo in enemigos)
                {
                    // distancia enemigos
                    Vector3 pOrigen = transform.position;
                    Vector3 pEnemigo = enemigo.transform.position;

                    enemigosDistancias.Add(enemigo, Vector3.Distance(pOrigen, pEnemigo));

                }
                // ordenamos por distancia de menos a mas
                List<KeyValuePair<GameObject, float>> enemigosDistanciaOrdered = enemigosDistancias.ToList();
                enemigosDistanciaOrdered.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));
                float distanciaMasCorta = enemigosDistanciaOrdered[0].Value;

                if (distanciaMasCorta <= rangoVision)
                {
                    enemigoFijado = enemigosDistanciaOrdered[0].Key;
                    agent.SetDestination(enemigoFijado.transform.position);
                    isEnemigoFijado = true;
                    isMoving = true;


                }


            }


        }
        else
        {
            if (isEnemigoFijado)
            {
                if (agent.remainingDistance <= rangoAtaque)
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
            Debug.Log("GUERRERO atacando");
            // si muere isEnemigoFijado = false
        }




    }
}
