using UnityEngine;
using UnityEngine.UI;

public abstract class Estructura : MonoBehaviour
{
    public int NivelMaximo;
    public int nivelActual;
    // todos estos atributos dependen del nivel al cual esta
    public int[] nivelMinimoCastilloParaMejorar;
    public int[] vidaPorNivel;

    public int vidaActual;
    public HealthBarScript healthBar;
    public Text textNivelSubMenu;
    public int[] costeOroMejorar; // costes para mejorar (el primer valor es el nivel 2)

    public abstract void mejorar();
    public abstract void abrirMenu();
    public abstract void cerrarMenu();

    protected GameObject canvas;

    //Actualiza la vida actuañl


    protected virtual void Start()
    {
        
     
        // canvas del menu de botones
        canvas = gameObject.transform.Find("Canvas").gameObject;
        if (canvas != null)
        {

            canvas.SetActive(false);
        }
        settearVida();
    }

    protected virtual void Update()
    {
        textNivelSubMenu.text = "Nivel " + (nivelActual + 1);
        comprobarVida0();
    }
    public void setCurrentHealth(int health)
    {
        if(health < vidaActual && TryGetComponent<Castillo>(out Castillo castillo))
        {

            Debug.Log("CASTILLO: " + castillo);
            castillo.onShakeCamera();
            
        }
        healthBar.SetHeatlh(health);
        vidaActual = health;
    }

    //Setea la vida actual y maxima cuando mejoras de nivel alguna estructura
    public void settearVida()
    {

       
        healthBar?.SetMaxHealth(vidaPorNivel[nivelActual]);
        healthBar?.SetHeatlh(vidaPorNivel[nivelActual]);
        vidaActual = vidaPorNivel[nivelActual];
        //Debug.Log("SETEANDO -> "+ healthBar.slider.maxValue + " Current: "+ healthBar.slider.value);
    }

    public void comprobarVida0()
    {
        if (vidaActual < healthBar.slider.maxValue)
        {
            healthBar.setVisbility(true);
        }
        if (vidaActual <= 0)
        {
         
            GameManager.listaEstructurasEnJuego.Remove(gameObject);
            Destroy(gameObject);
        }
    }

    



}
