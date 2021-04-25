using UnityEngine;

public abstract class Estructura : MonoBehaviour
{
    public int NivelMaximo;
    public int nivelActual;
    // todos estos atributos dependen del nivel al cual esta
    public int[] nivelMinimoCastilloParaMejorar;
    public int[] vidaPorNivel;

    public int currentVida;
    public HealthBarScript healthBar;
    public int[] costeOroMejorar; // costes para mejorar (el primer valor es el nivel 2)

    public abstract void mejorar();
    public abstract void abrirMenu();
    public abstract void cerrarMenu();

    protected GameObject canvas;

    //Actualiza la vida actuañl
    public void setCurrentHealth(int health)
    {

        healthBar.SetHeatlh(health);
        currentVida = health;
    }

    //Setea la vida actual y maxima cuando mejoras de nivel alguna estructura
    public void settearVida()
    {
       
        healthBar.SetMaxHealth(vidaPorNivel[nivelActual]);
        healthBar.SetHeatlh(vidaPorNivel[nivelActual]);
        currentVida = vidaPorNivel[nivelActual];
        //Debug.Log("SETEANDO -> "+ healthBar.slider.maxValue + " Current: "+ healthBar.slider.value);
    }

    public void comprobarVida0()
    {
        if (currentVida <= 0)
        {
            Destroy(gameObject);
        }
    }


}
