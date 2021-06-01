using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacionController : MonoBehaviour
{
    public int damage;
    [Header("Unity SetUp")]
    //Bala a disparar
    public GameObject areaAtaqueprefab;
    public GameObject centroAtaqueArea;
   
    public void crearAreaAtaque()
    {
        GameObject go = Instantiate(areaAtaqueprefab);
        go.transform.position = centroAtaqueArea.transform.position;
        go.GetComponent<TriggerScriptEnemigoFuerte>().damage = damage;
        
    }
}
