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
    Vector3 movePoint;
    public GameObject prefab;
    public float gridSize;

    private bool sePuedeConstruir = true; // si no cumple requisitos de recursos
    private bool hayColision = false; // detecta una colision se pondr� a false
    private Material mat; // para cambiar el color al detectar colisiones
    private Color colorNormal;
    private Color colorColision = new Color(255, 0, 0, 0.5f);
    void Start()
    {
        GameManager.Instance.SeEstaConstruyendo = true;
        mover_blueprint();
        mat = transform.GetChild(0).GetComponent<Renderer>().material; // coger el material del cubo hijo
        colorNormal = mat.color;
       

    }

    void LateUpdate()
    {

        comprobarOro();
        mover_blueprint();


        // si no hay colision y se puede construir
        if (!hayColision && sePuedeConstruir)
        {
            mat.color = colorNormal; // color normal
            mat.SetColor("_EmissionColor", colorNormal);
            // si se pulsa el izquierdo
            if (Input.GetMouseButtonDown(0))
            {
                // construir la estructura 

                GameObject estructuraConstruida = Instantiate(prefab, transform.position, transform.rotation);
                if (!prefab.GetComponent<Trampa>())
                {
                    GameManager.listaEstructurasEnJuego.Add(estructuraConstruida.gameObject);
                }

                // mina
                if (prefab.GetComponent<Mina>())
                {
                    GameManager.Instance.Oro = GameManager.Instance.Oro - GameManager.costeConstruirMina;

                }
                if (prefab.GetComponent<ExtractorObsidium>())
                {
                    GameManager.Instance.Oro = GameManager.Instance.Oro - GameManager.costeConstruirExtractor;

                }

            }

        }
        else
        {
            // si no a alguna de esas dos poner en color rojo
            mat.color = colorColision;
            mat.SetColor("_EmissionColor", colorColision);

            // si se pulsa el izquierdo
            if (Input.GetMouseButtonDown(0))
            {
                if (hayColision)
                {
                    GameManager.Instance.ShowMessage("No puedes construir ah�!");
                }else if (!sePuedeConstruir)
                {
                    // casa de hechizos
                    if (prefab.GetComponent<CasaDeHechizos>())
                    {

                        
                        if (GameManager.Instance.CasasDeHechizosConstruidas >= GameManager.topeCasaHechizos)
                        {
                            GameManager.Instance.ShowMessage("No puedes construir mas estructuras de ese tipo!");

                        }else if (GameManager.Instance.Oro < GameManager.costeConstruirCasaHechizos)
                        {
                            GameManager.Instance.ShowMessage("Oro insufciente");
                        }

                    }
                    else if (prefab.GetComponent<CuartelUnidades>())
                    {
                        if (GameManager.Instance.CuartelesConstruidos >= GameManager.topeCuartelUnidades)
                        {
                            GameManager.Instance.ShowMessage("No puedes construir mas estructuras de ese tipo!");
                        }
                        else if (GameManager.Instance.Oro < GameManager.costeConstruirCuartel)
                        {
                            GameManager.Instance.ShowMessage("Oro insufciente");
                        }
                    }
                    else
                    {
                        GameManager.Instance.ShowMessage("Oro insifuciente!");
                    }

                   
                }
            }

        }
        // cuando se pulse el boton derecho se deja de construir
        if (Input.GetMouseButton(1)) {         
        
            GameManager.Instance.SeEstaConstruyendo = false;
            Destroy(gameObject);
        }

        
    }

    // como el blue print estar� hasta que se pulse click derecho, hay que comprobar los recursos si se puede consturir
    // y ponerlo en rojo cuando no se pueda
    private void comprobarOro()
    {
        // el prefab tiene un script de una de las estructuras si lo intentamos coger y
        // no es null es que es ese tipe de estructura

        // muro
        if (prefab.GetComponent<Muro>()) {
            sePuedeConstruir = ((GameManager.Instance.Oro >= GameManager.costeConstruirMuro));
          
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
        // las capas de casillas con movimiento truncado al tama�o de la casillas
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
