using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particulasdestruccion : MonoBehaviour
{
    public float duracionAnimacion = 2f;//son segundos
    private float timeSpawn;//son segundos

    void Start()
    {
        //gameObject.GetComponentInChildren<ParticleSystem>().Play();
        timeSpawn = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - timeSpawn > duracionAnimacion)
        {
            Destroy(gameObject);
        }

    }
}
