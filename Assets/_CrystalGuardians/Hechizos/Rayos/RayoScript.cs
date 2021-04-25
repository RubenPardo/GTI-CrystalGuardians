using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayoScript : MonoBehaviour
{

    public float duracionRayo = 1f;//son segundos

    // Start is called before the first frame update
    void Start()
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

    // Update is called once per frame
    void Update()
    {
        if (Time.time > duracionRayo)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
