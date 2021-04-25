using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealScript : MonoBehaviour
{
    public float duracionHeal = 2.5f;//son segundos

    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Unidad"))
        {
            //Curar aliado!

            //enmigoScript enemigo = other.GetComponent<enmigoScript>();
            //enemigo.setCurrentHealth(enemigo.vida - 20);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > duracionHeal)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
