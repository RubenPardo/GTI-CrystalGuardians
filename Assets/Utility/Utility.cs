using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility 
{
    // Devuelve un punto aleatorio dentro de un perimetro de un rectangulo de lado 2*distance
    public static Vector3 getPuntoPerimetroRectangulo(float distance)
    {
        // para generar un punto random dentro del perimetro de un rectangulo
        // debemos escoger un lado x o z para fijarlo a 3 o -3 (el signo es aleatorio)
        // y la otra coordenada debe ser un valor entre -3 o 3 aleatorio
        Vector3 r;
        r.y = 0;
        int ladoFijo = Random.Range(0, 2);
        if (ladoFijo == 0)
        {
            // x fijo
            int signo = Random.Range(0, 2) == 0 ? 1 : -1;
            r.x = distance * signo;
            r.z = Random.Range(-distance, distance);
        }
        else
        {
            // z fijo
            int signo = Random.Range(0, 2) == 0 ? 1 : -1;
            r.z = distance * signo;
            r.x = Random.Range(-distance, distance);
        }

        //GameObject g = Instantiate(psj);

        //g.transform.position = transform.position + r;

        return r;
    }
}
