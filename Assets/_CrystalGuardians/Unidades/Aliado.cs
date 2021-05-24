using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Aliado : MonoBehaviour
{
    public GameObject prefabLvl1;
    public GameObject prefabLvl2;
    public GameObject prefabLvl3;
    public Animator animator;
    public int nivelActual;
    public int[] costePorNivel;
    public int[] danyoPorNivel;
    public int[] vidaPorNivel;
    public float rangoVision; // casillas para ver a los enemigos
    public float rangoAtaque; // casillas para atacar a los enemigos
    public float velocidadAtaque;// ataque por segundo
    public int vidaActual;
    public HealthBarScript healthBar;
    public float attackSpeed = 1f;
    private float attackCoutDwon = 0f;

    public float buffDamage = 1f;
    // movimiento
    public bool isEnemigoFijado = false;
    public bool isMoving = false;
    public bool isAtacking = false;

    List<GameObject> enemigos;
    Dictionary<GameObject, float> enemigosDistancias; // enemigo, distancia
    public NavMeshAgent agent;

    GameObject enemigoFijado;

    public GameObject rangeGameObject;
    public Material materialRangeAttack;

    protected float mejoraDanyo;

    protected virtual void Start()
    {
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        settearVida();
    }

    protected virtual void Update()
    {
        mover(mejoraDanyo);
        comprobarVida0();
    }

    public void setDefaultMoveFlags()
    {
        isEnemigoFijado = false;
        isMoving = true;
        isAtacking = false;
        animator.SetBool("SeMueve", true);
        animator.SetBool("Ataca", false);

    }

    protected void mover(float mejoraDanyo)
    {
        if (!isMoving)
        {
            
            enemigos = GameManager.Instance.listaEnemigosRonda; // obtener todos los enemigos de la escena
            enemigosDistancias = new Dictionary<GameObject, float>();


            if (!isEnemigoFijado && enemigos.Count > 0)// intentar fijar un enemigo si hay
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
                    animator.SetBool("SeMueve", true);


                }


            }

        }
        else
        {
            if (enemigoFijado== null)
            {
                isAtacking = false;
                isEnemigoFijado = false;
                animator.SetBool("Ataca", false);
            }
            else if (isEnemigoFijado)
            {
                if (agent.remainingDistance <= rangoAtaque)
                {
                    // el enemigo esta dentro del rango de ataque

                    // parar el agent y true el flag de atacar

                    isMoving = false;
                    animator.SetBool("SeMueve", false);
                    agent.SetDestination(this.transform.position);

                    isAtacking = true;
                    animator.SetBool("Ataca", true);

                }
                else
                {
                    agent.SetDestination(enemigoFijado.transform.position); //por si el objetivo se mueve
                }
            }

            if (agent.remainingDistance == 0)// para cuando se pulsa a otra direccion hay que comprobar cuando para
            {

                isMoving = false;
                animator.SetBool("SeMueve", false);
            }
        }

        if (isAtacking)
        {
            

            if (enemigoFijado == null)
            {
                // si el enemigo ha muerto
                isAtacking = false;
                isEnemigoFijado= false;
                animator.SetBool("Ataca", false);
            }
            // comprobar que el enemigo ha muerto antes de estos calculos
            else if (Vector3.Distance(transform.position, enemigoFijado.transform.position) > (rangoAtaque+0.2)) // el 0.2 es por el tama�o de las unidades
            {
                // si el enemigo sale del rango de ataque desfijarlo
                isAtacking = false;
                isEnemigoFijado = false;
                animator.SetBool("Ataca", false);
            }
            else
            {
                //Atacara pasado 1s
                if (attackCoutDwon <= 0f)
                {
                    EnemigoScript enemigo = enemigoFijado.GetComponent<EnemigoScript>();
                    enemigo.setCurrentHealth(enemigo.vidaActual - Mathf.RoundToInt(danyoPorNivel[nivelActual] * mejoraDanyo));
                    enemigo.setCurrentHealth(enemigo.vidaActual - (int)(danyoPorNivel[nivelActual]*buffDamage));
                    attackCoutDwon = 1f / attackSpeed;
                }

                attackCoutDwon -= Time.deltaTime;
            }

        }
    }

    //Actualiza la vida actua�l
    public void setCurrentHealth(int heal)
    {
        if (heal>=vidaPorNivel[nivelActual])
        {
            heal = vidaPorNivel[nivelActual];
        }
        healthBar.SetHeatlh(heal);
        vidaActual = heal;
    }

    //Setea la vida actual y maxima cuando mejoras de nivel alguna estructura
    public void settearVida()
    {

        healthBar.SetMaxHealth(vidaPorNivel[nivelActual]);
        healthBar.SetHeatlh(vidaPorNivel[nivelActual]);
        vidaActual = vidaPorNivel[nivelActual];
        //Debug.Log("SETEANDO -> "+ healthBar.slider.maxValue + " Current: "+ healthBar.slider.value);
    }

    public void comprobarVida0()
    {
        if (vidaActual < healthBar.slider.maxValue)
        {
            healthBar.setVisbility(true);
        }
        if (vidaActual <= 0)
        {
            GameManager.Instance.Unidades--;
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        GameManager.listaAliadosEnJuego.Remove(gameObject);
    }

    public void drawRangeAttack()
    {
        
        if (rangeGameObject == null)
        {
            rangeGameObject = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            rangeGameObject.GetComponent<CapsuleCollider>().enabled = false;
            rangeGameObject.transform.parent = gameObject.transform;
            rangeGameObject.transform.localScale = new Vector3(rangoAtaque * 2, -0.05f, rangoAtaque * 2);
            rangeGameObject.transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
            rangeGameObject.GetComponent<MeshRenderer>().material = materialRangeAttack;
        }
        else
        {
            rangeGameObject.transform.localScale = new Vector3(rangoAtaque * 2, 0.1f, rangoAtaque* 2);
            rangeGameObject.SetActive(true);
        }

    }

    public void removeRangeAttack()
    {
        rangeGameObject.SetActive(false);
    }

}
