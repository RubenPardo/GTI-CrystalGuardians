using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class move_controller : MonoBehaviour
{

    private global_selection global_selection;
    RaycastHit hit;

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

                // NO-----
                // calcular un circulo formado por todo el grupo y asignarle un punto a cada uno
                /*float areaAgentes = 0.0f;
                int numAgentes = global_selection.selected_table.selectedTable.Count;
                foreach (KeyValuePair<int, GameObject> pair in global_selection.selected_table.selectedTable)
                {

                    NavMeshAgent agent = global_selection.selected_table.selectedTable[pair.Key].transform.parent.gameObject.GetComponent<NavMeshAgent>();
                    areaAgentes += agent.radius;
                    
                }

                float radioCirculo = Utility.getCircleRadiusByArea(areaAgentes) + 2;

                Debug.Log("area: " + areaAgentes);
                Debug.Log("radio circulo: " + radioCirculo);
                Vector3[] puntos = Utility.getPuntosEquidistribuidosDentroCirculo(radioCirculo, numAgentes);*/
                int k = 0;
                foreach (KeyValuePair<int, GameObject> pair in global_selection.selected_table.selectedTable)
                {
                    Aliado unidad = global_selection.selected_table.selectedTable[pair.Key].transform.parent.gameObject.GetComponent<Aliado>();
                    NavMeshAgent agent = unidad.GetComponent<NavMeshAgent>();
                    // cuando pulsamos restablecer por defecto los flags
                    unidad.setDefaultMoveFlags();

                    agent.SetDestination(hit.point);
                    
                }
                
            }
                
        }
    }
}
