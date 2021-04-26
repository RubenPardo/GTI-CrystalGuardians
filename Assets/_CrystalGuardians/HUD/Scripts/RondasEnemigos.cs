using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class RondasEnemigos : MonoBehaviour
{
    public Text contadorRondas;
    public Text numeroRonda;
    private float contadorTiempoRonda = 300.0f;
    public int numeroRnda = 1;
    private bool isRondaActive = false;


   
    public int cantidadEnemigosPorRonda=3;

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




    void Start()
    {

        Button btnF = btnFinalizar.GetComponent<Button>();
        Button btnE = btnEmpezar.GetComponent<Button>();
      

    }
   

    private void comprobarLanzarMejorasAldeas()
    {
        // cada 3 rondas se lanzaran las mejoras de la aldea
        if (numeroRnda % 3 == 0)
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
        }
       
    }

    public void forzarFinalizarRonda()
    {
        isRondaActive = false;
        contadorTiempoRonda = 300.0f;
        numeroRnda++;
        numeroRonda.text = numeroRnda.ToString("f0");
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

            contadorRondas.text = "--:--";

            if (comprobarFinRonda())
            {
                numeroRnda++;
                isRondaActive = false;
                contadorTiempoRonda = 300.0f;
                numeroRonda.text = numeroRnda.ToString("f0");
                comprobarLanzarMejorasAldeas();


            }
        }
        

    }

    private bool updateCronometro()
    {
        
        contadorTiempoRonda -= Time.deltaTime;
        if (contadorTiempoRonda >= 60 && contadorTiempoRonda < 36000)
        {
            contadorRondas.text = (contadorTiempoRonda / 60).ToString("f1") + "m";
        }
        else
        {
            contadorRondas.text = contadorTiempoRonda.ToString("f1") + "s";

        }

        return contadorTiempoRonda <= 0.0f;
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
        
        //Generar enemigos
        GameObject[] listaSpawn = GameObject.FindGameObjectsWithTag("Respawn");
        

       
        
        for (int i=0; i < numeroRnda * cantidadEnemigosPorRonda; i++)

        {
            GameObject casilla = listaSpawn[Random.Range(0, listaSpawn.Length)];
            int meleeDistancia = Random.Range(0, 2);
            if(meleeDistancia == 1)
            {
                //generar melee
                GameObject g = Instantiate(enemigoMelee);
                g.transform.position = casilla.transform.position;
                
            }
            else
            {
                //generar distancia
                GameObject g = Instantiate(enemigoDistancia);
                g.transform.position = casilla.transform.position;
            }

           
             
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