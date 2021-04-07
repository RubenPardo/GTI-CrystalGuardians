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

    private void mover_blueprint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerEstructuras))
        {
            Debug.Log("Encima de una estructura");
        }

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
        sePuedeConstruir = false;
        mat.color = colorColision;

    }
    private void OnTriggerEnter(Collider other)
    {
       
    }

    private void OnTriggerExit(Collider other)
    {
        sePuedeConstruir = true;
        mat.color = colorNormal;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        mover_blueprint();

       
        if (sePuedeConstruir)
        {
            if (Input.GetMouseButton(0))
            {
                // construir la estructura y borrar el blue print
                Instantiate(prefab, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }

        

    }
}
