using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class blueprint_script : MonoBehaviour
{

    RaycastHit hit;
    public LayerMask layerCasillas = ~0;
    public LayerMask layerEstructuras = ~0;
   
    public GameObject prefab;
    public float gridSize;

    private bool sePuedeConstruir = true; // si no cumple requisitos de recursos
    private bool hayColision = false; // detecta una colision se pondrá a false
    private Material mat; // para cambiar el color al detectar colisiones
    private Color colorNormal;
    private Color colorColision = new Color(255, 0, 0, 0.5f);

    // control de construccion muro
    Vector3 mousePositionMurosIni;
    Vector3 mousePositionMurosFin;
    private bool seEstaConstruyendoMuro;
    private int numMuros = 1;

    void Start()
    {
        GameManager.Instance.SeEstaConstruyendo = true;
        mover_blueprint();
        if (!prefab.GetComponent<Muro>())
        {

            mat = transform.GetChild(0).GetComponent<Renderer>().material; // coger el material del cubo hijo
            
        }
        colorNormal = Color.green;

    }

    void LateUpdate()
    {
        

        if (!seEstaConstruyendoMuro)
        {
            comprobarOro();
            mover_blueprint();


            // si no hay colision y se puede construir
            if (!hayColision && sePuedeConstruir)
            {
                cambiarColor(true);
                // si se pulsa el izquierdo
                if (Input.GetMouseButtonDown(0) && prefab.GetComponent<Muro>())
                {
                    // se esta construyendo muro
                    mousePositionMurosIni = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    seEstaConstruyendoMuro = true;

                }
                else if (Input.GetMouseButtonDown(0))
                {
                    // construir la estructura 
                    
                    GameObject estructuraConstruida = Instantiate(prefab, transform.position, transform.rotation);
                    if (!prefab.GetComponent<Trampa>())
                    {
                        GameManager.listaEstructurasEnJuego.Add(estructuraConstruida.gameObject);
                        GameManager.Instance.EstructurasTotalesConstruidas++;
                    }
                }

            }
            else
            {
                // si no a alguna de esas dos poner en color rojo
                cambiarColor(false);

                // si se pulsa el izquierdo
                if (Input.GetMouseButtonDown(0))
                {
                    if (hayColision)
                    {
                        GameManager.Instance.ShowMessage("¡No puedes construir ahí!");
                    }
                    else if (!sePuedeConstruir)
                    {
                        // casa de hechizos
                        if (prefab.GetComponent<CasaDeHechizos>())
                        {


                            if (GameManager.Instance.CasasDeHechizosConstruidas >= GameManager.topeCasaHechizos)
                            {
                                GameManager.Instance.ShowMessage("¡No puedes construir más estructuras de ese tipo!");

                            }
                            else if (GameManager.Instance.Oro < GameManager.costeConstruirCasaHechizos)
                            {
                                GameManager.Instance.ShowMessage("¡Oro insuficiente!");
                            }

                        }
                        else if (prefab.GetComponent<CuartelUnidades>())
                        {
                            if (GameManager.Instance.CuartelesConstruidos >= GameManager.topeCuartelUnidades)
                            {
                                GameManager.Instance.ShowMessage("¡No puedes construir más estructuras de ese tipo!");
                            }
                            else if (GameManager.Instance.Oro < GameManager.costeConstruirCuartel)
                            {
                                GameManager.Instance.ShowMessage("¡Oro insuficiente!");
                            }
                        }
                        else
                        {
                            GameManager.Instance.ShowMessage("¡Oro insuficiente!");
                        }


                    }
                }

            }
        }
        else
        {
            
            
           

            Transform cubeColliderMuro = transform.GetChild(0);
            cubeColliderMuro.localScale = new Vector3(1F, 1F, 1F);
            // calculos sobre la distancia del cursor
            Vector3 mousePositionActual = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float distX = Mathf.Abs(mousePositionMurosIni.x) - Mathf.Abs(mousePositionActual.x);
            float disty = Mathf.Abs(mousePositionMurosIni.y) - Mathf.Abs(mousePositionActual.y);
            bool isX = Mathf.Abs(distX) > Mathf.Abs(disty); // poner los murso donde este mas largo el cursor
            int isNegativo;
            if (isX)
            {
                isNegativo = mousePositionMurosIni.x -mousePositionActual.x > 0 ? -1 : 1;
                numMuros = (int)(Mathf.Abs(distX));
            }
            else
            {
                isNegativo = mousePositionMurosIni.y - mousePositionActual.y > 0 ? -1 : 1;
                numMuros = (int)(Mathf.Abs(disty));
            }

            comprobarOro();
            cambiarColor((!hayColision && sePuedeConstruir));

            // suelta el click
            if (Input.GetMouseButtonUp(0))
            {
                seEstaConstruyendoMuro = false;
                if(!hayColision && sePuedeConstruir)
                {
                    construirMuros(isX, isNegativo);
                }

                if (GameManager.Instance.Oro < GameManager.costeConstruirMuro * numMuros)
                {
                    GameManager.Instance.ShowMessage("¡Oro insuficiente!");
                }
                if (hayColision)
                {
                    GameManager.Instance.ShowMessage("¡No puedes construir ahí!");
                }
            }
            // mantiene el click
            else
            {
                
                if(isX)
                {
                    
                    cubeColliderMuro.localScale = new Vector3(1F * numMuros *isNegativo, 1F, 1F);
                }
                else
                {
                    cubeColliderMuro.localScale = new Vector3(1F, 1F, 1F * numMuros* isNegativo);
                }
            }
        }
        // cuando se pulse el boton derecho se deja de construir
        if (Input.GetMouseButton(1)) {

            numMuros = 0;
            GameManager.Instance.SeEstaConstruyendo = false;
            Destroy(gameObject);
        }

        
    }

    /// <summary>
    /// Construye los n muros
    /// </summary>
    /// <param name="dir">true = X, False Z</param>
    /// <param name="isNegativo">1 o -1 para la direccion</param>
    private void construirMuros(bool dir, int isNegativo)
    {
        Vector3 pos = transform.position;
        GameObject estructuraConstruida = Instantiate(prefab, pos, transform.rotation);
        GameManager.listaEstructurasEnJuego.Add(estructuraConstruida.gameObject);
        GameManager.Instance.EstructurasTotalesConstruidas++;

        for (int i = 0; i < numMuros; i++)
        {
            
                if (dir)
                {
                    // modificar la x
                    pos += new Vector3(1f * isNegativo, 0f, 0f);
                }
                else
                {
                    // modificar la z
                    pos += new Vector3(0f, 0f, 1f * isNegativo);
                }

            

            estructuraConstruida = Instantiate(prefab, pos, transform.rotation);
            GameManager.listaEstructurasEnJuego.Add(estructuraConstruida.gameObject);
            GameManager.Instance.EstructurasTotalesConstruidas++;
        }
        numMuros = 0;
    }

    private void cambiarColor(bool v)
    {
        if (v)
        {
            if (mat != null)
            {
                mat.color = colorNormal; // color normal
                mat.SetColor("_EmissionColor", colorNormal);
            }
            else
            {
                Material m = transform.GetChild(0).GetChild(0).GetComponent<Renderer>().material;
                m.color = colorNormal;
                m.SetColor("_EmissionColor", colorNormal);
            }
        }
        else
        {
            if (mat != null)
            {
                mat.color = colorColision; // color normal
                mat.SetColor("_EmissionColor", colorColision);
            }
            else
            {
                Material m = transform.GetChild(0).GetChild(0).GetComponent<Renderer>().material;
                m.color = colorColision;
                m.SetColor("_EmissionColor", colorColision);
            }
        }
    }

    // como el blue print estará hasta que se pulse click derecho, hay que comprobar los recursos si se puede consturir
    // y ponerlo en rojo cuando no se pueda
    private void comprobarOro()
    {
        // el prefab tiene un script de una de las estructuras si lo intentamos coger y
        // no es null es que es ese tipe de estructura

        // muro
        if (prefab.GetComponent<Muro>()) {
            sePuedeConstruir = ((GameManager.Instance.Oro >= GameManager.costeConstruirMuro* Mathf.Abs(numMuros)));
          
        }

        // mina
        if (prefab.GetComponent<Mina>())
        {
            sePuedeConstruir = ((GameManager.Instance.Oro >= GameManager.costeConstruirMina));
           
        }

        // ExtractorObsidium
        if (prefab.GetComponent<ExtractorObsidium>())
        {
            sePuedeConstruir = ((GameManager.Instance.Oro >= GameManager.costeConstruirExtractor));
            
        }

        // trampa
        if (prefab.GetComponent<Trampa>()) {
            sePuedeConstruir = ((GameManager.Instance.Oro >= GameManager.costeConstruirTrampa));
            
         }

        // cuartel
        if (prefab.GetComponent<CuartelUnidades>()) {
            sePuedeConstruir = ((GameManager.Instance.Oro >= GameManager.costeConstruirCuartel) && GameManager.Instance.CuartelesConstruidos < GameManager.topeCuartelUnidades);
           
        }

         // casa de hechizos
        if (prefab.GetComponent<CasaDeHechizos>()) {
            sePuedeConstruir = GameManager.Instance.Oro >= GameManager.costeConstruirCasaHechizos && GameManager.Instance.CasasDeHechizosConstruidas < GameManager.topeCasaHechizos;           
        }

        // torre
        if (prefab.GetComponent<Torre>())
        {
            sePuedeConstruir = ((GameManager.Instance.Oro >= GameManager.costeConstruirTorre));
          
        }

    }

    private void mover_blueprint()
    {
        // mover el blue print por donde apunta el raton por encima de solo 
        // las capas de casillas con movimiento truncado al tamaño de la casillas
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerCasillas))
        {
            Vector3 truePos;
            truePos.x = Mathf.Floor(hit.point.x / gridSize) * gridSize;
            truePos.y = 0;
            truePos.z = Mathf.Floor(hit.point.z / gridSize) * gridSize;

            transform.position = truePos;
        }
    }



    private void OnTriggerStay(Collider other)
    {
        
        hayColision = true;

    }


    private void OnTriggerExit(Collider other)
    {
       hayColision = false;
    }

}
