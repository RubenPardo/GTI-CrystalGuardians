using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangosAtaque : MonoBehaviour
{
    // Start is called before the first frame update


    public void toggleChanged(bool state)
    {
        if (state)
        {
            MostrarTodosLosRangosDeAtaque();
            GameManager.Instance.rangoAtaqueSiempreVisible = true;
        }
        else
        {
            GameManager.Instance.rangoAtaqueSiempreVisible = false;
            OcultarTodosLosRangosDeAtaque();
           
        }
    }
    private void MostrarTodosLosRangosDeAtaque()
    {
        List<GameObject> aliados = GameManager.listaAliadosEnJuego;
        foreach( GameObject go in aliados){
            Ballestero ballestero;
            if (go.TryGetComponent<Ballestero>(out ballestero))
                ballestero.drawRangeAttack();
        }

        List<GameObject> estructuras = GameManager.listaEstructurasEnJuego;
        foreach (GameObject go in estructuras)
        {
            Torre torre;
            if (go.TryGetComponent<Torre>(out torre))
                torre.drawRangeAttack();
        }
    }

    private void OcultarTodosLosRangosDeAtaque()
    {
        List<GameObject> aliados = GameManager.listaAliadosEnJuego;
        foreach (GameObject go in aliados)
        {
            Ballestero ballestero;
            if (go.TryGetComponent<Ballestero>(out ballestero))
                ballestero.removeRangeAttack();
        }

        List<GameObject> estructuras = GameManager.listaEstructurasEnJuego;
        foreach (GameObject go in estructuras)
        {
            if (go != null)
            {
                if (go.TryGetComponent<Torre>(out Torre torre))
                    torre.removeRangeAttack();
            }
            
        }
    }
}
