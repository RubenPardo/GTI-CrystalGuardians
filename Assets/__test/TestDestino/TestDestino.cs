using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDestino : MonoBehaviour
{

    public GameObject punto;

    public int cantidad = 6;

    public float radio = 2f;

    private DestinoCirculo destinoCirculo;

    // Start is called before the first frame update
    void Start()
    {
        destinoCirculo = new DestinoCirculo(transform.position, radio, cantidad);
        for (int i = 0; i < cantidad; i++)
        {
            Instantiate(punto, destinoCirculo.siguientePunto(), Quaternion.identity);
        }
    }
}
