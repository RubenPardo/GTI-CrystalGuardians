using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showMenuEstructuraOnClick : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private Estructura estructuraAnterior;// para cuando se clicke en otro sitio esconder su menu

    // Update is called once per frame
    void Update()
    {
        //Check for mouse click 
        if (Input.GetMouseButtonDown(0))
        {
            
            RaycastHit raycastHit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit, Mathf.Infinity))
            {
                // el ray cast colisionara contra el cubo, cogemos el padre que es el que tiene el script estructura
                Estructura e = raycastHit.transform.parent.gameObject.GetComponent<Estructura>();
                 
                if (e != null)
                {
                    estructuraAnterior?.cerrarMenu();
                    estructuraAnterior = e;
                    e.abrirMenu();
                }
                else
                {
                    // se hizo click en otra cosa que no es una estructura
                    estructuraAnterior?.cerrarMenu();
                }


            }
            else
            { 
                // se hizo click en otra cosa que no es una estructura
                estructuraAnterior?.cerrarMenu();
            }
        }
    }
}
