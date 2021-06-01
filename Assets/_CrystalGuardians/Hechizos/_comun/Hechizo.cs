using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hechizo : MonoBehaviour
{
    public float duracionHechizo = 1f;
    public float[] statsHechizoPorNivel;
    public int nivelActual = 0;
    protected int nivelMaximo = 0;
    public ParticleSystem sistemaParticualas;
    public ParticleSystem areaParticualas;
    //El segundo en el que spawnea el hechizo
    protected float spwanHechizo;

}
