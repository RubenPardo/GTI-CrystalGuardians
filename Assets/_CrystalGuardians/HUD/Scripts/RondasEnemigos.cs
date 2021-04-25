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


    //public int rangoSpawn = 0;
    //public float xPos = 0;

    //public float zPos = 0;
    public int cantidadEnemigosPorRonda=3;


    // Start is called before the first frame upd0ate
    public GameObject enemigoMelee;
    public GameObject enemigoDistancia;
    public GameObject enemigoFuerte;
    public GameObject mina;
    public Button btnRondas;
    public Button btnReiniciar;

    void Start()
    {
        //Button btn = btnRondas.GetComponent<Button>();
       // Button btnR = btnReiniciar.GetComponent<Button>();
      
        numeroRonda.text = numeroRnda.ToString("f0");
        //contadorRondas.text = contadorRonda.ToString("f2");

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
                if(numeroRnda % 3 == 0)
                {
                    //lanzar mejoras de aldea
                }
                
                

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
        else if (contadorTiempoRonda >= 36000)
        {
            contadorRondas.text = (contadorTiempoRonda / 36000).ToString("f1") + "h";
        }
        else
        {
            contadorRondas.text = contadorTiempoRonda.ToString("f1") + "s";

        }

        return contadorTiempoRonda == 0;
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