using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextHandler : MonoBehaviour
{
    // se destruye al segundo y sigue a la camara

    private Transform cam;

    void Start()
    {
        cam = Camera.main.transform;
        Destroy(gameObject, 1f);
    }

    private void Update()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}
