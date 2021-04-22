using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aliado : MonoBehaviour
{
    public int nivelActual;
    public int[] costePorNivel;
    public int[] danyoPorNivel;
    public int[] vidaPorNivel;
    public float rangoVision; // casillas para ver a los enemigos
    public float rangoAtaque; // casillas para atacar a los enemigos
    public float velocidadAtaque;// ataque por segundo

    public bool isEnemigoFijado = false;
    public bool isMoving = false;
    public bool isAtacking = false;

    public void setDefaultMoveFlags()
    {
        isEnemigoFijado = false;
        isMoving = true;
        isAtacking = false;
    }


}
