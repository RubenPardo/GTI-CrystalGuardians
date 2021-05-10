using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colliderTrampa : MonoBehaviour
{
    public GameObject rangoExplosion;
    
    
    private bool destruir = false;
    private float detectedTime;
    public float delayExplosion = 5f;//son segundos


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (destruir && Time.time - detectedTime > delayExplosion)
        {
            Destroy(gameObject.transform.parent.gameObject); 
        }

    } 
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Enemigo"))
        {
            rangoExplosion.SetActive(true);
            
            destruir = true;
            detectedTime = Time.time;
        }
    }
}
