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
    private float contadorTiempoRonda = 10.0f;
    public int numeroRnda = 1;
    private bool isRondaActive = false;


   
    public int cantidadEnemigosPorRonda=3;

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

    void Start()
    {
        tiempoRonda = (int)contadorTiempoRonda;
        listaSpawn = GameObject.FindGameObjectsWithTag("Respawn");

    }
   

    private void comprobarLanzarMejorasAldeas()
    {
        // cada 3 rondas se lanzaran las mejoras de la aldea
        if (numeroRnda % 6 == 0)
        {
            //lanzar mejoras de aldea
            panelMejoras.SetActive(true);
        }
    }
    

    public void comenzarRonda()
    {
        if (!isRondaActive)
        {
            spawn();
            isRondaActive = true;
            contadorRondas.text = "";
            txtOleada.text = "OLEADA " + numeroRnda.ToString("f0");
            txtOleada.color = Color.red;
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
        else if(numeroRnda % 5 == 0){ updateLuzAmbiente(); }
        

    }

    private void finRonda()
    {
        GameManager.Instance.listaEnemigosRonda.Clear();
        numeroRnda++;
        isRondaActive = false;
        contadorTiempoRonda = 10.0f;
        tiempoRonda = (int)contadorTiempoRonda;
        txtOleada.text = "PARA LA OLEADA " + numeroRnda.ToString("f0");
        txtOleada.color = Color.white;
        comprobarLanzarMejorasAldeas();

        // hacer de dia
        GameManager.Instance.lucesActivas = false;
        Vector3 rotation = new Vector3(maxLuz,
                        transform.rotation.y,
                        transform.rotation.z);
        luzAmbiente.transform.eulerAngles = rotation;
            
    }

    private bool updateCronometro()
    {
        
        contadorTiempoRonda -= Time.deltaTime;

        float seconds = Mathf.FloorToInt(contadorTiempoRonda % 60); 
        float minutes = Mathf.FloorToInt(contadorTiempoRonda / 60);

       
        contadorRondas.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        return contadorTiempoRonda <= 0.0f;
    }
    private void updateLuzAmbiente()
    {
        // contadorTiempoRonda ( tmAct ) -> tiempoRonda (tmMax)
        // luzAmbiente.rotation.x ( X ) -> maxOscuridad (luzMax)
        // tmAct/tmMax = X/LuzMax -> X = (tmAct * luzMax) / tmMax


        float cocienteTiempo = contadorTiempoRonda / tiempoRonda;

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
        
 
        for (int i=0; i < numeroRnda * cantidadEnemigosPorRonda; i++)

        {
            GameObject casilla = listaSpawn[Random.Range(0, listaSpawn.Length)];
            int meleeDistancia = Random.Range(0, 2);
            GameObject g;
            if(meleeDistancia == 1)
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


            GameManager.Instance.listaEnemigosRonda.Add(g);

        }

        // generar enemigos fuertes
        if(numeroRnda % 5 == 0)
        {
            int cantidadEnemigosFuertes = numeroRnda / 5;
            Debug.Log(cantidadEnemigosFuertes);
            if (cantidadEnemigosFuertes > 4) cantidadEnemigosFuertes = 4;

            for (int i = 0; i < cantidadEnemigosFuertes; i++) 
            {
                GameObject casilla = listaSpawn[Random.Range(0, listaSpawn.Length)];
                
                GameObject g = Instantiate(enemigoFuerte);
                
                g.transform.position = casilla.transform.position;

                


            }

        }
       

    }
}