using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class Guerrero : Aliado
{
    public GameObject prefabLvl1;
    public GameObject prefabLvl2;
    public GameObject prefabLvl3;
    public static float mejoraDanyo = 1f;//mejora de aldea
                                        



    // Update is called once per frame
    protected override void Update()
    {

        base.mejoraDanyo = Guerrero.mejoraDanyo;
        base.Update();
        comprobarCambiarPrefab();
    }
    private void comprobarCambiarPrefab()
    {
        /*if (nivelActual > 0) {
            if (nivelActual == 1)
            {
                // prefab nivel 2
                prefabLvl1.SetActive(false);
                prefabLvl2.SetActive(true);


            }
            else
            {
                // prefab nivel 3
                prefabLvl2.SetActive(false);
                prefabLvl3.SetActive(true);
               
            }
        }*/
    }

    }




