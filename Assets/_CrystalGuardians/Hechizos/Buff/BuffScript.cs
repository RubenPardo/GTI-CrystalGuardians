using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffScript : MonoBehaviour
{
    public float duracionBuff = 2.5f;//son segundos

    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Unidad"))
        {

            //Aumentar stats aliado! 

            //enmigoScript enemigo = other.GetComponent<enmigoScript>();
            //enemigo.setCurrentHealth(enemigo.vida - 20);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > duracionBuff)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
