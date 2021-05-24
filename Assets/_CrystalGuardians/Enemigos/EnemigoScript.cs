using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EnemigoScript : MonoBehaviour
{
    [Header("Stasts Enemgio")]
    public int nivelActual;
    public int[] danyoPorNivel;
    public int[] vidaPorNivel;
    public int vidaMaxima;
    public int vidaActual;

    public float speed;
    public float rangoVision;
    public float rangoAtaque;
    public float attackSpeed = 1f;
    protected float attackCoutDwon = 0f;
    private bool dir;
    [Header("HUD")]
    public HealthBarScript healthBar;

    GameObject[] estructurasUnidades;
    Dictionary<GameObject, float> dictDistancias;
    protected GameObject objetivoFijado;

    private bool isMoving;
    private bool isObjetivoFijado;
    private bool isAtacking;

    //animaciones
    private Animator animator;


    NavMeshAgent agent;

    // Start is called before the first frame update
    protected void Start()
    {
        dir = true;
        agent = GetComponent<NavMeshAgent>();
        settearVida();

        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        moverEnemigo();
        comprobarVida0();
    }

    private void moverEnemigo()
    {
       

        if(!isMoving)
        {

            List<GameObject> estructurasUnidades = getPossibleTargets();

            dictDistancias = new Dictionary<GameObject, float>();
            
            if (!isObjetivoFijado && estructurasUnidades.Count > 0)// intentar fijar un enemigo
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
                    animator.SetBool("atacando", false);



                }


            }





        }
        else
        {
            if (objetivoFijado == null)
            {
                isAtacking = false;
               
                isObjetivoFijado = false;
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
                        animator.SetBool("atacando", true);


                    }
                    else
                    {
                        agent.SetDestination(objetivoFijado.transform.position); //por si el objetivo se mueve
                    }
                }
            }
            

            if (agent.remainingDistance == 0)// para cuando se pulsa a otra direccion hay que comprobar cuando para
            {

                isMoving = false;
                //animator.SetBool("atacando", false);

            }
        }

        if (isAtacking)
        {
            
            if (objetivoFijado == null)
            {
                // si el enemigo ha muerto
                isAtacking = false;
                //animator.SetBool("atacando", false);
                isObjetivoFijado = false;
            }
            else if (Vector3.Distance(transform.position, objetivoFijado.transform.position) > rangoAtaque)
            {
                // si el enemigo sale del rango de ataque desfijarlo
                isAtacking = false;
                //animator.SetBool("atacando", false);
                isObjetivoFijado = false;
            }
            else
            {
                attack();
            }
        }
    }

    public virtual List<GameObject> getPossibleTargets()
    {
        return Utility.unirDosArrays(
                GameManager.listaAliadosEnJuego,
                GameManager.listaEstructurasEnJuego);
    }
    public virtual void attack()
    {
        if (attackCoutDwon <= 0f)
        {
            Estructura estructura;
            /*
            Guerrero guerrero;
            Ballestero ballestero;
            */
            Aliado aliado;
            if (objetivoFijado.TryGetComponent<Estructura>(out estructura))
            {
                estructura.setCurrentHealth(estructura.vidaActual - danyoPorNivel[nivelActual]);
            }
            else
            {
                aliado = objetivoFijado.GetComponentInParent<Aliado>();
                aliado.setCurrentHealth(aliado.vidaActual - danyoPorNivel[nivelActual]);
            }

            attackCoutDwon = 1f / attackSpeed;
        }

        attackCoutDwon -= Time.deltaTime;
    }

    //Actualiza la vida actual
    public void setCurrentHealth(int health)
    {

        healthBar.SetHeatlh(health);
        vidaActual = health;
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
           
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.listaEnemigosRonda.Remove(gameObject);
    }
}
