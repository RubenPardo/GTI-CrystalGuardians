using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayoScript : MonoBehaviour
{

    public float duracionRayo = 1f;//son segundos
    public int danyoHechizoRayo = 20;
    public int mejoraDanyoRayo = 1;
    public static float aumentoRadio = 1f;//mejora de aldea
    // Start is called before the first frame update
    void Start()
    {

        transform.localScale = new Vector3(transform.localScale.x * aumentoRadio, transform.localScale.y, transform.localScale.z * aumentoRadio);
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Enemigo"))
        {
            EnemigoScript enemigo = other.GetComponent<EnemigoScript>();
            enemigo.setCurrentHealth(enemigo.vidaActual - (danyoHechizoRayo * mejoraDanyoRayo));
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
