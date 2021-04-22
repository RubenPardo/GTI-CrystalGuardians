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
              
                foreach (KeyValuePair<int, GameObject> pair in global_selection.selected_table.selectedTable)
                {
                    
                    NavMeshAgent agent = global_selection.selected_table.selectedTable[pair.Key].transform.parent.gameObject.GetComponent<NavMeshAgent>();
                    agent.SetDestination(hit.point);
                }
                
            }
                
        }
    }
}
