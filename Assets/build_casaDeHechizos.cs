using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class build_casaDeHechizos : MonoBehaviour
{
    public GameObject CasaDeHechizos_blueprint;

    public void spawn_CasaDeHechizos_blueprint()
    {
        Instantiate(CasaDeHechizos_blueprint);
    }
}
