using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstruirEstructuras : MonoBehaviour
{
    public GameObject CasaDeHechizos_blueprint;

    private void Start()
    {
        // coger los botones y deshabilitar los que requieran un nivel superior al del castillo 
        // y deshabilitar los que no puedes construir por recursos

        

        Debug.Log("start spawn" + GameManager.nivelMinimoCastilloCasaHechizos);



    }

    public void spawn_CasaDeHechizos_blueprint()
    {

        GameManager g = GameManager.Instance;
        Castillo castillo = g.castillo.GetComponent<Castillo>();
        // comprobar nivel
        if(castillo.nivelActual >= GameManager.nivelMinimoCastilloCasaHechizos)
        {
            Debug.Log("on click spawn");
            Instantiate(CasaDeHechizos_blueprint);
        }

        // comprobar recursos

    }
}
