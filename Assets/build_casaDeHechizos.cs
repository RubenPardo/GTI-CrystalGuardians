using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class build_casaDeHechizos : MonoBehaviour
{
    public GameObject CasaDeHechizos_blueprint;

    private void Start()
    {
        Debug.Log("start spawn");
    }

    public void spawn_CasaDeHechizos_blueprint()
    {
        Debug.Log("on click spawn");
        Instantiate(CasaDeHechizos_blueprint);
    }
}
