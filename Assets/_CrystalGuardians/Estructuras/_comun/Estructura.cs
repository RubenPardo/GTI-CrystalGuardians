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
    //particulas
    public GameObject particulasDestruccion;
    protected ParticleSystem sistemaParticulasDestruccion;
    public GameObject particulasPosibleMejora;
    protected ParticleSystem sistemaParticulasPosibleMejora;
    public GameObject particulasMejora;
    protected ParticleSystem sistemaParticulasMejorar;


    public HealthBarScript healthBar;
    public Text textNivelSubMenu;
    public int[] costeOroMejorar; // costes para mejorar (el primer valor es el nivel 2)

    public abstract void mejorar();
    public abstract void abrirMenu();
    public abstract void cerrarMenu();
    public Color colorPrimary = new Color(208, 156, 45);


    protected GameObject canvas;
    public GameObject floatingText;

    //Actualiza la vida actuañl


    protected virtual void Start()
    {
        
     
        // canvas del menu de botones
        canvas = gameObject.transform.Find("Canvas").gameObject;
        if (canvas != null)
        {

            canvas.SetActive(false);
        }


        sistemaParticulasMejorar = particulasMejora.GetComponent<ParticleSystem>();
        sistemaParticulasPosibleMejora = particulasPosibleMejora.GetComponent<ParticleSystem>();
        sistemaParticulasDestruccion = particulasDestruccion.GetComponent<ParticleSystem>();

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
            GameObject go =  Instantiate(particulasDestruccion);
            go.transform.position = transform.position;
            go.GetComponentInChildren<ParticleSystem>().Play();

            GameManager.listaEstructurasEnJuego.Remove(gameObject);
            Destroy(gameObject);
        }
    }

    protected void disableButtonEstructura(Button btnMejorar, Button btnMejorarInfo)
    {
        btnMejorar.GetComponent<RawImage>().color = Color.gray;
        btnMejorarInfo.GetComponentInChildren<RawImage>().color = Color.gray;


    }

    protected void enableButtonEstructura(Button btnMejorar, Button btnMejorarInfo)
    {

        btnMejorar.GetComponent<RawImage>().color = colorPrimary;

        btnMejorarInfo.GetComponentInChildren<RawImage>().color = colorPrimary;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="isOro">Oro o obsidium</param>
    /// <param name="isGasto">Restar o sumar</param>
    /// <param name="coste">La cantidad</param>
    /// <param name="position">Si no es gasto no hace falta, para instanciar el texto de gasto</param>
    public void updateRecursos(bool isOro, bool isGasto, float coste, Transform position)
    {
        if (isGasto)
        {
            // se gasta recursos
            if (isOro)
            {
                GameManager.Instance.Oro -= coste;
            }
            else
            {
                GameManager.Instance.Obsiidum -= coste;
            }


            instanciarTextoFlotante(isOro, coste, position);
        }
        else
        {
            // se genera recursos
            if (isOro)
            {
                GameManager.Instance.Oro += coste;
            }
            else
            {
                GameManager.Instance.Obsiidum += coste;
            }
        }

    }

    private void instanciarTextoFlotante(bool isOro, float coste, Transform position)
    {
        GameObject text = Instantiate(floatingText, position);

        TextMesh tM = text.transform.GetChild(0).GetComponent<TextMesh>();
        tM.text = "- " + coste;
        Color32 yellow = new Color32(239, 192, 17, 255);
        Color32 prurple = new Color32(104, 30, 113, 255);
        tM.color = isOro ? yellow : prurple;
    }

}
