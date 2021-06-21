using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class RondasEnemigos : MonoBehaviour
{
    public Text contadorRondas;
    public Text txtOleada;
    public float tiempoEntreRonda;
    private float contadorRonda;
    public int numeroRnda = 1;
    private bool isRondaActive = false;
    
   
    public int cantidadEnemigosPorRonda=2;
    public int maxEnemigosPorRonda=50;

    private GameObject[] listaSpawn;

    public GameObject panelMejoras;
    public GameObject panelRondas;
    public GameObject btnFinalizar;
    public GameObject btnEmpezar;


    // Start is called before the first frame upd0ate
    public GameObject enemigoMelee;
    public GameObject enemigoDistancia;
    public GameObject enemigoFuerte;
    public GameObject mina;
    public GameObject castillo;


    // control ciclo dia noche
    [Header("Luz Ambiente")]
    [SerializeField]
    private Light luzAmbiente;
    [SerializeField]
    private int maxOscuridad = 10;
    [SerializeField]
    private int maxLuz = 52;
    [SerializeField]
    private int luzAmbienteParaEncenderLuces = 20;
    private int tiempoRonda;// usado para realizar la regla de tres entre la luz y el tiempo

    

    //musica para las rondas

    
    public AudioClip musicaRondaNormal;
    public AudioClip musicaRondaBoss;
    public AudioClip musicaEntreRondas;

    public AudioSource sonidoCuerno;
    public AudioSource sonidoRugido;
    public AudioSource sonidoVictoria;
    public AudioSource sonidoMejora;



    private int rondaEnemigosDistancia = 3;// a partir de la ronda 3 aparecen a distancia
    private int rondaEnemigosFuertes = 10;// cada 10 rondas enemigos fuertes
    private int rondaMejorarEnemigos = 5;// cada x rondas suben de nivel los enemigos
    private int rondaMejorarEnemigosFuertes = 10;// cada x rondas suben de nivel los enemigos fuertes
    private int rondaMejoras = 3;// cada x rondas aparecen mejoras
    private int maxEnemigosFuertes = 4;

    void Start()
    {
        if (GameManager.isTutorialOn == false)
        {
            contadorRonda = tiempoEntreRonda;
            tiempoRonda = (int)tiempoEntreRonda;
            listaSpawn = GameObject.FindGameObjectsWithTag("Respawn");
        }
    }
   

    private void comprobarLanzarMejorasAldeas()
    {
        // cada 3 rondas se lanzaran las mejoras de la aldea
        if (numeroRnda % rondaMejoras == 0)
        {
            //lanzamos la musica de mejoras
            sonidoMejora.Play();

            //lanzar mejoras de aldea
            panelMejoras.SetActive(true);

            
        }
    }
    

    public void comenzarRonda()
    {
        

        if (!isRondaActive)
        {


            contadorRonda = 0;
            spawn();
            isRondaActive = true;
            GameManager.Instance.RondaEnemigosActiva = true;
            contadorRondas.text = "";
            txtOleada.text = "OLEADA " + numeroRnda.ToString("f0");
            txtOleada.color = Color.red;

            if (numeroRnda % rondaEnemigosFuertes == 0)
            {
                AudioSource source = GameManager.Instance.musicaAmbiente.GetComponent<AudioSource>();
               
                updateLuzAmbiente();
                sonidoRugido.Play();
                source.clip = musicaRondaBoss;
                source.Play();

            }
            else
            {
                AudioSource source = GameManager.Instance.musicaAmbiente.GetComponent<AudioSource>();
                sonidoCuerno.Play();
                source.clip = musicaRondaNormal;
                source.Play();
            }
        }
       
    }

    public void forzarFinalizarRonda()
    {
        finRonda();

        GameObject[] listaEnemigosEnPartida = GameObject.FindGameObjectsWithTag("Enemigo");
        for (int i = 0; i < listaEnemigosEnPartida.Length; i++)
        {
            Destroy(listaEnemigosEnPartida[i]);
        }

        comprobarLanzarMejorasAldeas();

    }

    // Update is called once per frame
    void Update()
    {

        if (GameManager.isTutorialOn == false)
        {
            btnEmpezar.SetActive(!isRondaActive);

            if (updateCronometro())
            {
                comenzarRonda();
            }
            if (isRondaActive)
            {

                contadorRondas.text = "";

                if (comprobarFinRonda())
                {
                    finRonda();


                }
            }
            else if (numeroRnda % rondaEnemigosFuertes == 0) { updateLuzAmbiente(); }
        }
        
        

    }

    private void finRonda()
    {
        GameManager.Instance.listaEnemigosRonda.Clear();
        numeroRnda++;
        GameManager.Instance.RondaEnemigosActiva = false;

        isRondaActive = false;
        contadorRonda = tiempoEntreRonda;
        tiempoRonda = (int)contadorRonda;
        txtOleada.text = "PARA LA OLEADA " + numeroRnda.ToString("f0");
        txtOleada.color = Color.white;
        comprobarLanzarMejorasAldeas();

        // hacer de dia
        GameManager.Instance.lucesActivas = false;
        Vector3 rotation = new Vector3(maxLuz,
                        transform.rotation.y,
                        transform.rotation.z);
        luzAmbiente.transform.eulerAngles = rotation;

        AudioSource source = GameManager.Instance.musicaAmbiente.GetComponent<AudioSource>();
        if (numeroRnda % rondaMejoras != 0)
        {
            sonidoVictoria.Play();
        }
        
        source.clip = musicaEntreRondas;
        source.Play();

    }

    private bool updateCronometro()
    {
        
        contadorRonda -= Time.deltaTime;

        float seconds = Mathf.FloorToInt(contadorRonda % 60); 
        float minutes = Mathf.FloorToInt(contadorRonda / 60);

       
        contadorRondas.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        return contadorRonda <= 0.0f;
    }
    private void updateLuzAmbiente()
    {
     

        float cocienteTiempo = contadorRonda / tiempoRonda;

        // quitar el tanto porciento entre la resta y luego sumar el min, 
        // asi si se redujo el 100% debe dar el maxOscuridad 
        float luzActual = ((maxLuz - maxOscuridad) * cocienteTiempo) + maxOscuridad;
       
        if(luzActual <= luzAmbienteParaEncenderLuces)
        {
            GameManager.Instance.lucesActivas = true;
        }

        Vector3 rotation = new Vector3(luzActual,
                        transform.rotation.y,
                        transform.rotation.z);
        luzAmbiente.transform.eulerAngles = rotation;

    }

    private bool comprobarFinRonda()
    {
        GameObject[] listaEnemigosEnPartida = GameObject.FindGameObjectsWithTag("Enemigo");
        if (listaEnemigosEnPartida.Length == 0)
        {
            return true;
        }
        return false;
    }
  
    private void spawn()
    {

       GameManager.Instance.RondaMaximaAlcanzada = numeroRnda;

        int nivelEnemigos = numeroRnda / rondaMejorarEnemigos;
        enemigoMelee.GetComponent<EnemigoScript>().nivelActual = nivelEnemigos;
        enemigoDistancia.GetComponent<enemigoDistanciaScript>().nivelActual = nivelEnemigos;

        int enemigosRonda = (numeroRnda * cantidadEnemigosPorRonda) > maxEnemigosPorRonda ? maxEnemigosPorRonda : numeroRnda * cantidadEnemigosPorRonda;
      for (int i=0; i < enemigosRonda; i++)
        {
            GameObject casilla = listaSpawn[Random.Range(0, listaSpawn.Length)];
            GameObject g;
            if (numeroRnda>rondaEnemigosDistancia)
            {
                int meleeDistancia = Random.Range(0, 2);
                if (meleeDistancia == 1)
                {
                    //generar melee
                    g = Instantiate(enemigoMelee);
                    g.transform.position = casilla.transform.position;

                }
                else
                {
                    //generar distancia
                    g = Instantiate(enemigoDistancia);
                    g.transform.position = casilla.transform.position;
                }
            }
            else
            {
                g = Instantiate(enemigoMelee);
                g.transform.position = casilla.transform.position;
            }



            GameManager.Instance.listaEnemigosRonda.Add(g);

        }

        // generar enemigos fuertes
        if(numeroRnda % rondaEnemigosFuertes == 0)
        {

            int nivelEnemigosFuertes = numeroRnda / rondaMejorarEnemigosFuertes;
            enemigoFuerte.GetComponent<EnemigoScript>().nivelActual = nivelEnemigosFuertes;
            int cantidadEnemigosFuertes = numeroRnda / 5;
            //Debug.Log(cantidadEnemigosFuertes);
            if (cantidadEnemigosFuertes > maxEnemigosFuertes) cantidadEnemigosFuertes = maxEnemigosFuertes;

            for (int i = 0; i < cantidadEnemigosFuertes; i++) 
            {
                GameObject casilla = listaSpawn[Random.Range(0, listaSpawn.Length)];
                
                GameObject g = Instantiate(enemigoFuerte);
                
                g.transform.position = casilla.transform.position;

                


            }

        }
       

    }



}