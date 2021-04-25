using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanzarHechizos : MonoBehaviour
{
    RaycastHit hit;
    public LayerMask layerCasillas = ~0;
    public LayerMask layerEstructuras = ~0;
    public float gridSize;  
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.SeEstaConstruyendo = true;
        mover_blueprint();
 
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
