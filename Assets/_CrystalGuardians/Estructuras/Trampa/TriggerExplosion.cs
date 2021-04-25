using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerExplosion : MonoBehaviour
{
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        

        if (other.tag.Equals("Enemigo"))
        {
            enmigoScript enemigo = other.GetComponent<enmigoScript>();
            enemigo.setCurrentHealth(enemigo.vidaActual - 20);
        }

    }
}
