using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blueprint_script : MonoBehaviour
{

    RaycastHit hit;
    public LayerMask layerCasillas = ~0;
    public LayerMask layerEstructuras = ~0;
    Vector3 movePoint;
    public GameObject prefab;
    public float gridSize;

    private bool sePuedeConstruir = true; // si detecta una colision se pondrá a false
    private Material mat; // para cambiar el color al detectar colisiones
    private Color colorNormal;
    private Color colorColision = new Color(255, 0, 0, 0.5f);

    // Start is called before the first frame update
    void Start()
    {
        mover_blueprint();
        mat = transform.GetChild(0).GetComponent<Renderer>().material; // coger el material del cubo hijo
        colorNormal = mat.color;
       

    }
    // Update is called once per frame
    void LateUpdate()
    {
        mover_blueprint();

       
        
        if (Input.GetMouseButton(0) && sePuedeConstruir)
        {
            // construir la estructura y borrar el blue print
            Instantiate(prefab, transform.position, transform.rotation);
            Destroy(gameObject);
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
        // se debe hacer en trigger stay ya que si hay dos edificios juntos 
        // y sales de uno no detectará el on enter solo el on exit
        sePuedeConstruir = false;
        mat.color = colorColision;

    }

    private void OnTriggerExit(Collider other)
    {
        sePuedeConstruir = true;
        mat.color = colorNormal;
    }

}
