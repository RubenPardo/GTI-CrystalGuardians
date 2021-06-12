using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class move_controller : MonoBehaviour
{

    private global_selection global_selection;
    RaycastHit hit;
    public GameObject flechasMovimiento;

    void Start()
    {
        global_selection = GetComponent<global_selection>();
    }

    // Update is called once per frame
    void Update()
    {
        // mover todos las unidades dentro del array de global_selection a la posicion cuando se pulsa el boton derecho y 
        // hace click en la UI
        if (Input.GetMouseButtonDown(1) && !Utility.rayCastUI())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 50000.0f))
            {

                
                int k = 0;
                foreach (KeyValuePair<int, GameObject> pair in global_selection.selected_table.selectedTable)
                {
                   
                    Aliado unidad = global_selection.selected_table.selectedTable[pair.Key].transform.parent.gameObject.GetComponent<Aliado>();
                    NavMeshAgent agent = unidad.GetComponent<NavMeshAgent>();
                    // cuando pulsamos restablecer por defecto los flags
                    unidad.setDefaultMoveFlags();
                    //Debug.Log("PUNTO " +k +": "  + (hit.point + PuntosPosicionamiento.puntos[k] * (agent.radius+0.7f)));
                    Debug.Log(PuntosPosicionamiento.puntos[k]);
                    agent.SetDestination(hit.point+PuntosPosicionamiento.puntos[k]*(agent.radius + 1.0f));
                   
                    k++;

                    
                }
                if (k > 0)
                {
                    global_selection.selected_table.deselectAll();
                    GameObject go = Instantiate(flechasMovimiento);
                    Vector3 pos = new Vector3(hit.point.x, 2, hit.point.z);
                    go.transform.position = pos;
                    Destroy(go, 0.5f);


                }

            }

                
        }
    }
}
