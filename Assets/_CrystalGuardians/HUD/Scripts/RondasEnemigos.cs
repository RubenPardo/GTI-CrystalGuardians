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
    private float contadorRonda = 0.0f;
    public int numeroRnda = 1;
    private int comprobarBotonRondasPulsado = 0;


    //public int rangoSpawn = 0;
    //public float xPos = 0;

    //public float zPos = 0;
    public int cantidadEnemigosPorRonda=5;
    private int cantidadEnemigosActual;


    // Start is called before the first frame upd0ate
    public GameObject enemigoMelee;
    public GameObject enemigoDistancia;
    public GameObject enemigoFuerte;
    public GameObject mina;
    public Button btnRondas;
    public Button btnReiniciar;

    void Start()
    {
        Button btn = btnRondas.GetComponent<Button>();
        Button btnR = btnReiniciar.GetComponent<Button>();
        cantidadEnemigosActual = numeroRnda * cantidadEnemigosPorRonda;
        btn.onClick.AddListener(spawn);
        btn.onClick.AddListener(botonRondasPulsado);
        btnR.onClick.AddListener(botonReiniciarPulsado);
        //contadorRondas.text = numeroRnda.ToString("f0");
        numeroRonda.text = numeroRnda.ToString("f0");
        //contadorRondas.text = contadorRonda.ToString("f2");

    }




    // Update is called once per frame
    void Update()
    {

        if (comprobarBotonRondasPulsado == 1)
        {
            updateCronometro();
            if (comprobarFinRonda())
            {
                numeroRnda++;
                cantidadEnemigosActual = numeroRnda * cantidadEnemigosPorRonda;
                numeroRonda.text = numeroRnda.ToString("f0");
                contadorRonda = 0;
                spawn();

            }
        }
        if (comprobarBotonRondasPulsado == 0)
        {
            contadorRonda = 0;
            contadorRondas.text = contadorRonda.ToString("f0");
            numeroRnda = 0;
            cantidadEnemigosActual = numeroRnda * cantidadEnemigosPorRonda;
            numeroRonda.text = numeroRnda.ToString("f0");
            if (comprobarFinRonda() == true)
            {
                GameObject[] listaEnemigosEnPartida = GameObject.FindGameObjectsWithTag("Enemigos");
                for (int i = 0; i < listaEnemigosEnPartida.Length; i++)
                {
                    Destroy(listaEnemigosEnPartida[i]);
                }
                
            }
            GameObject[] estructuras = GameObject.FindGameObjectsWithTag("Estructuras");
            for (int i = 0; i < estructuras.Length; i++)
            {
                Destroy(estructuras[i]);
            }
            
            if(estructuras.Length == 0)
            {
                Instantiate(mina, new Vector3(10, 0, 0), Quaternion.identity);

            }
            

        }




        //timerRonda();

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //nunca llegara al 10
        //Debug.Log(Random.Range(0, 10));
        //Random de un vector de 3, entre 1 y -1 
        //Debug.Log(Random.insideUnitSphere);


        /* Vector3 v = new Vector3(Random.Range(xOrigen-rangoSpawn, rangoSpawn+yOrigen), 0,Random.Range(xOrigen-rangoSpawn, rangoSpawn+yOrigen));
         Quaternion q = new Quaternion(0,0,0,0);
         Instantiate(enemigo, v, q);*/
        // spawnIncircle();
        //}

    }

    private void updateCronometro()
    {
        contadorRonda = contadorRonda + Time.deltaTime;
        if (contadorRonda >= 60 && contadorRonda < 36000)
        {
            contadorRondas.text = (contadorRonda / 60).ToString("f1") + "m";
        }
        else if (contadorRonda >= 36000)
        {
            contadorRondas.text = (contadorRonda / 36000).ToString("f1") + "h";
        }
        else
        {
            contadorRondas.text = contadorRonda.ToString("f1") + "s";

        }
    }

    private void botonRondasPulsado()
    {
        comprobarBotonRondasPulsado = 1;
    }
    private void botonReiniciarPulsado()
    {
        comprobarBotonRondasPulsado = 0;
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
    /*public void timerRonda()
    {
        contadorRonda = contadorRonda + Time.deltaTime;
        contadorRondas.text = contadorRonda.ToString("f2");
    }*/
    public void spawn()
    {
        
        //Generar enemigos
        GameObject[] listaSpawn = GameObject.FindGameObjectsWithTag("Respawn");
        

        
        
        
        for (int i=0; i < cantidadEnemigosActual;i++) { }
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

            /*if (numeroRnda > 0 && numeroRnda % 5 == 0)
            {

                Vector3 posicionCasilla = listaSpawn[numeroRandomListaSpawn].transform.position;
                if (a == 0 && a % 2 == 0 && a % 5 != 0)
                {
                    GameObject g = Instantiate(enemigoMelee, transform);
                    g.transform.position = posicionCasilla;
                    g.transform.parent = null;
                }
                else if(a > 0 && a % 5== 0 && a <21)
                {
                    GameObject g = Instantiate(enemigoFuerte, transform);
                    g.transform.position = posicionCasilla;
                    g.transform.parent = null;
                }
                else if(a > 0 && a % 2 != 0 && a % 5 != 0)
                {
                    GameObject g = Instantiate(enemigoDistancia, transform);
                    g.transform.position = posicionCasilla;
                    g.transform.parent = null;
                }
            }
            else
            {

                Vector3 posicionCasilla = listaSpawn[numeroRandomListaSpawn].transform.position;
               
                if(a % 2 == 0)
                {
                    GameObject g = Instantiate(enemigoMelee, transform);
                    g.transform.position = posicionCasilla;
                    g.transform.parent = null;
                }
                else
                {
                    GameObject g = Instantiate(enemigoDistancia, transform);
                    g.transform.position = posicionCasilla;
                    g.transform.parent = null;
                }
                
            }
            a++;*/
             
        }

        // generar enemigos fuertes
        if(numeroRnda % 5 == 0)
        {
            int cantidadEnemigosFuertes = numeroRnda / 5;
            if (cantidadEnemigosFuertes > 4) cantidadEnemigosFuertes = 4;

            for (int i = 0; i < cantidadEnemigosFuertes; i++) { }
            {
                GameObject casilla = listaSpawn[Random.Range(0, listaSpawn.Length)];
                
                GameObject g = Instantiate(enemigoFuerte);
                g.transform.position = casilla.transform.position;

                


            }

        }
        /*if (cantidadEnemigosActual < cantidadEnemigosPorRonda)
        {


            

            //Spwan de enemigos en x posiscion
            //xPos = Random.Range(22.50f, 23.50f);
            //zPos = Random.Range(-22.50f, 23.50f);
            /*GameObject g = Instantiate(enemigo, transform);
            g.transform.position = Vector3.forward * rangoSpawn;
            transform.Rotate(Vector3.up, Random.Range(0, 360));
            g.transform.parent = null;

            //Debug.Log(Random.Range(0, 10));
            //Vector3 v = new Vector3(Random.Range(xOrigen - rangoSpawn, rangoSpawn + yOrigen), 0, Random.Range(xOrigen - rangoSpawn, rangoSpawn + yOrigen));
            Vector3 v = new Vector3(xPos, 0, zPos);
            Quaternion q = Quaternion.identity;
            Instantiate(enemigo, v, q);
            
            cantidadEnemigosActual++;
        }*/

    }
}