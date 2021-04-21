using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
            // mostrar y esconder menus de las estructuras si no se hace click en UI
            if (!rayCastUI())
            {
                RaycastHit raycastHit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out raycastHit, Mathf.Infinity))
                {
                    // el ray cast colisionara contra el cubo, cogemos el padre que es el que tiene el script estructura
                    Transform parent = raycastHit.transform.parent;
                    if (parent != null)
                    {
                        Estructura e = parent.gameObject.GetComponent<Estructura>();

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
                }
                else
                {
                    // se hizo click en otra cosa que no es una estructura
                    estructuraAnterior?.cerrarMenu();
                }
            }
            else
            {
               // golpeo en UI
            }

           
        }
    }

    
    private bool rayCastUI()
    {

        PointerEventData pointerData = new PointerEventData(EventSystem.current);

        pointerData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);
        if (results.Count > 0)
        {
            //WorldUI is my layer name
            if (results[0].gameObject.layer == LayerMask.NameToLayer("UI"))
            {
                return true;
            }
        }
        return false;
    }
}
