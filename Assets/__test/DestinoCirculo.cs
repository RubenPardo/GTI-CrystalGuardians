using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinoCirculo
{

    private Vector3 centro;

    private float radio;

    private List<Vector3> puntos = new List<Vector3>();

    private int contador = 0;

    public DestinoCirculo(Vector3 centro, float radio, int puntos)
    {
        this.centro = centro;
        this.radio = radio;
        DefinirPuntos(puntos);
    }

    public Vector3 siguientePunto()
    {
        Vector3 p = puntos[contador % puntos.Count];
        contador++;
        return (p * radio) + centro;
    }

    private void DefinirPuntos(int cantidad)
    {
        puntos = new List<Vector3>();

        for (int i = 0; i < cantidad; i++)
        {
            Vector3 p = RotarVector(Vector3.forward, (360/cantidad)*i);
            this.puntos.Add(p);
        }
    }

    private Vector3 RotarVector(Vector3 vector, float angulo)
    {
        return Quaternion.Euler(0, angulo, 0) * vector;
    }
}
