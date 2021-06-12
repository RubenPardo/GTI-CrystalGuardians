using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePrintHechizos : MonoBehaviour
{
    RaycastHit hit;
    public LayerMask layerCasillas = ~0;
    public LayerMask layerEstructuras = ~0;
    public float gridSize;
    public GameObject prefab;
    public static float aumentoRadio = 1f;//mejora de aldea

    // Start is called before the first frame update
    void Start()
    {
        
        GameManager.Instance.SeEstaLanzandoHechizo(true);
        transform.localScale = new Vector3(transform.localScale.x * aumentoRadio, transform.localScale.y, transform.localScale.z * aumentoRadio);
        mover_blueprint();
 
    }

    void LateUpdate()
    {
        mover_blueprint();
        //Boton izquierdo se lanza el hechizo
        if (Input.GetMouseButtonDown(0))
        {
            descontarHechizo();
            // Lanzar el hechizo
            Instantiate(prefab, transform.position, transform.rotation);
            GameManager.Instance.SeEstaLanzandoHechizo(false);
            Destroy(gameObject);

        }
        // cuando se pulse el boton derecho se cancela la animacion de lanzar hechizos
        if (Input.GetMouseButton(1))
        {
            GameManager.Instance.SeEstaLanzandoHechizo(false);
            Destroy(gameObject);
        }
    }

    private void descontarHechizo()
    {
        Transform hechizo = prefab.transform.GetChild(0);
        HealScript heal;
        RayoScript rayo;
        BuffScript buff;
        
        if (hechizo.TryGetComponent<HealScript>(out heal))
        {
            GameManager.Instance.HealsDisponibles--;
            
        }
        else if (hechizo.TryGetComponent<RayoScript>(out rayo))
        {
            GameManager.Instance.RayosDisponibles--;
        }
        else if (hechizo.TryGetComponent<BuffScript>(out buff))
        {
             GameManager.Instance.BuffsDisponibles--;

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
            truePos.x =hit.point.x;
            truePos.y = 0;
            truePos.z = hit.point.z;

            transform.position = truePos;
            transform.rotation = new Quaternion(0f,0f,0f,0f);
        }
    }
}
