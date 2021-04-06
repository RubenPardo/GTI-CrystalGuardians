using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blueprint_script : MonoBehaviour
{

    RaycastHit hit;
    public LayerMask layer = ~0;
    Vector3 movePoint;
    public GameObject prefab;
    public Transform target;
    public float gridSize;
    // Start is called before the first frame update
    void Start()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
        {
            Vector3 truePos;
            truePos.x = Mathf.Floor(hit.point.x / gridSize) * gridSize;
            truePos.y = 0;
            truePos.z = Mathf.Floor(hit.point.z / gridSize) * gridSize;
            Debug.Log(truePos);
            transform.position = truePos;
        }
       
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
        {
            
            Vector3 truePos;
           // truncar x,z para que solo valga valores enteros y cuadre con las casillas
            truePos.x = Mathf.Floor(hit.point.x / gridSize) * gridSize;
            truePos.y = 0;
            truePos.z = Mathf.Floor(hit.point.z / gridSize) * gridSize;
            Debug.Log(truePos.ToString());
            transform.position = truePos;
        }
        if (Input.GetMouseButton(0))
        {
            // construir la estructura y borrar el blue print
            Instantiate(prefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }

    }
}
