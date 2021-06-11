using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvisoScript : MonoBehaviour
{
    public float delayAviso;
    void Start()
    {
        Destroy(gameObject, delayAviso);
    }
}
