using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    
    public GameObject hudGameOver;
    //objeto que controla la musica de la escena
    public GameObject musicaAmbiente;

    public GameObject textoAviso;
    public float timeDelayAviso;

    //tutorial
    public static bool isTutorialOn = true;
    public bool IsTutorialOn { get => isTutorialOn; set => isTutorialOn = value; }

    public GameObject textoAvisoSalirConstruccion;
    public float duracionAviso = 3f;
    public GameObject hudPrincipal;
    public GameObject textoAvisoFlotante;

    //HUD 
    public GameObject panelBarraRondas;
    public GameObject panelBarraConstruccion;
    public GameObject hudBtnMejorasAldea;
    public GameObject barraRecursos;
    public GameObject btnRangos;
    public GameObject panelTutorial;

    // singleton

    static GameManager instance;
    public static GameManager Instance { get => instance; set => instance = value; }

    public bool seEstaConstruyendo = false; // cuando se pulsa un boton poner a true y deshabilitar todo hasta que se deje de construir
    public bool SeEstaConstruyendo { get => seEstaConstruyendo; 
        set {
            seEstaConstruyendo = value;
            textoAvisoSalirConstruccion.SetActive(value);
            textoAvisoSalirConstruccion.GetComponent<Text>().text = "Click derecho para salir del modo construccion";
        } 
    }

    public void SeEstaLanzandoHechizo(bool state)
    {
        seEstaConstruyendo = state;
        textoAvisoSalirConstruccion.SetActive(state);
        textoAvisoSalirConstruccion.GetComponent<Text>().text = "Click derecho para salir del modo lanzamiento";
    }

    public void SeEstaDesplazandoUnidades(bool state)
    {
        //seEstaConstruyendo = state;
        textoAvisoSalirConstruccion.SetActive(state);
        textoAvisoSalirConstruccion.GetComponent<Text>().text = "Click derecho para desplazar a las unidaes";
    }


    // casa de hechizos -----------
    public static int nivelMinimoCastilloCasaHechizos = 1;
    public static int costeConstruirCasaHechizos = 1440;

    // cuartel de unidades -----------
    public static int nivelMinimoCastilloCuartel = 0;
    public static int costeConstruirCuartel = 400;
    public static int topeCuartelUnidades = 2;
    private int cuartelesConstruidos = 0;
    public int CuartelesConstruidos { get => cuartelesConstruidos; set => cuartelesConstruidos = value; }

    // trampas -----------
    public static int nivelMinimoCastilloTrampa = 0;
    public static int costeConstruirTrampa = 500;

    // torre -----------
    public static int nivelMinimoCastilloTorre = 0;
    public static int costeConstruirTorre = 1350;

    // muros -----------
    public static int nivelMinimoCastilloMuros = 0;
    public static int costeConstruirMuro = 100;

    // mina -----------
    public static int nivelMinimoCastilloMina = 0;
    public static int costeConstruirMina = 3150;
    public int NivelMinimoCastilloMina { get => nivelMinimoCastilloMina; set => nivelMinimoCastilloMina = value; }
    public int CosteConstruirMina { get => costeConstruirMina; set => costeConstruirMina = value; }

    // extractor -----------
    public static int nivelMinimoCastilloExtractor = 0;
    public static int costeConstruirExtractor = 2160;

    // hechizos --------
    public static int[] costeLanzarHeal = { 250, 1500, 5000} ;
    public static int[] costeLanzarRayo = { 250, 1500, 5000 };
    public static int[] costeLanzarBuff = { 250, 1500, 5000 };
    public static int nivelCasaHechizos;

    private static int healsDisponibles = 0;
    private static int rayosDisponibles = 0;
    private static int buffsDisponibles = 0;
    public int hechizosLanzados = 0;

    public static int topeCasaHechizos=1;

    private static int casasDeHechizosConstruidas = 0;

    public int CasasDeHechizosConstruidas { get => casasDeHechizosConstruidas; set => casasDeHechizosConstruidas = value; }
    public int BuffsDisponibles { get => buffsDisponibles; set => buffsDisponibles = value; }
    public int RayosDisponibles { get => rayosDisponibles; set => rayosDisponibles = value; }
    public int HealsDisponibles { get => healsDisponibles; set => healsDisponibles = value; }
    //recursos -------------
    //private float oro = 990000000;//3150;
    //private float obsidium = 990000000;

    private float oro = 5000;
    private float obsidium = 0;

    public bool oroConstruido = false;
    public bool obsidiumConstruido = false;

    public GameObject prefabOro;
    public GameObject prefabObsidium;
    public float Oro { get => oro; set => oro = value; }
    public float Obsiidum { get => obsidium; set => obsidium = value; }

    public bool OroConstruido { get => oroConstruido; set => oroConstruido = value; }
    public bool ObsidiumConstruido { get => obsidiumConstruido; set => obsidiumConstruido = value; }



    // a medida que se van construyendo y borrando los enemigos/estructuras/aliados se iran a�adiendo o borrando de estas listas
    // asi evitamos tener que hacer el FinWithTag por cada update en los scripts de fijacion de las unidades
    public List<GameObject> listaEnemigosRonda = new List<GameObject>();
    public static List<GameObject> listaEstructurasEnJuego = new List<GameObject>();
    public static List<GameObject> listaAliadosEnJuego = new List<GameObject>();


    private bool rondaEnemigosActiva = false;
    public bool RondaEnemigosActiva { get => rondaEnemigosActiva; set => rondaEnemigosActiva = value; }

    public GameObject castillo; // se construira al inicio

    //atributos del castillo
    private int nivelActualCastillo = 0;
    public int NivelActualCastillo { get => nivelActualCastillo; set => nivelActualCastillo = value; }
    public int TopeUnidades { get; internal set; }
    public bool isCuartelPos1Empty = true;
    public bool isCuartelPos2Empty = true;
    public int Unidades { get; internal set; }


    //Mejoras de aldea
    public List<Carta> listaCartas;

    // controlar las luces para activarlas solo de noche
    internal bool lucesActivas = false;
    
    public bool rangoAtaqueSiempreVisible = false;
    public bool RangoAtaqueSiempreVisible { get => rangoAtaqueSiempreVisible; set => rangoAtaqueSiempreVisible = value; }

    //Estadisticas generales para pantalla de derrota
    private float oroTotalGenerado = 0;
    public float OroTotalGenerado { get => oroTotalGenerado; set => oroTotalGenerado = value; }

    private float obsidiumTotalGenerado = 0;
    public float ObsidiumTotalGenerado { get => obsidiumTotalGenerado; set => obsidiumTotalGenerado = value; }

    private int unidadesAliadasTotalesGeneradas = 0;
    public int UnidadesAliadasTotalesGeneradas { get => unidadesAliadasTotalesGeneradas; set => unidadesAliadasTotalesGeneradas = value; }

    private int enemigosTotalesEliminados = 0;
    public int EnemigosTotalesEliminados { get => enemigosTotalesEliminados; set => enemigosTotalesEliminados = value; }

    private int hechizosTotalesLanzados = 0;
    public int HechizosTotalesLanzados { get => hechizosTotalesLanzados; set => hechizosTotalesLanzados = value; }

    private int estructurasTotalesConstruidas = 0;
    public int EstructurasTotalesConstruidas { get => estructurasTotalesConstruidas; set => estructurasTotalesConstruidas = value; }

    private int rondaMaximaAlcanzada = 0;
    public int RondaMaximaAlcanzada { get => rondaMaximaAlcanzada; set => rondaMaximaAlcanzada = value; }

    // Start is called before the first frame update


    void Start()
    {
        
        if(instance == null)
        {

            instance = this;
            if (!isTutorialOn)
            {
                panelBarraRondas.SetActive(true);
                panelBarraConstruccion.SetActive(true);
                hudBtnMejorasAldea.SetActive(true);
                barraRecursos.SetActive(true);
                btnRangos.SetActive(true);
                panelTutorial.SetActive(false);
            }
            //Instantiate(castillo, transform.position, transform.rotation);
           

            //A�adimos las cartas a la lista de cartas disponibles
            listaCartas = new List<Carta>();
            listaCartas.Add(new Carta("Estructura", "El coste de las estructuras se reduce un 10%", "estructuras", reducirCosteEstructuras));
            listaCartas.Add(new Carta("Recursos", "Las minas producen un 20% mas rapido", "recursos", aumentarProduccionMinas20));
            listaCartas.Add(new Carta("Unidades", "Mejora el ataque de tus unidades", "unidades", aumentoDeDanyoAliados));
            listaCartas.Add(new Carta("Hechizos", "El radio de los hechizos ha aumentado", "hechizos", aumentarRadioHechizos));

            

        }
        else
        {
            DestroyImmediate(gameObject);
        }

        
        
    }

    // Update is called once per frame

    
    void Update()
    {
      
    }
    static int aumentoDeDanyoAliados()
    {
        Debug.Log("Tus unidades ahora hacen mas da�o");
        Guerrero.mejoraDanyo *= 5f;
        Ballestero.mejoraDanyo *= 5f;

        return 0;
    }
    static int reducirCosteEstructuras()
    {
        Debug.Log("Las estructuras han bajado de coste un 10%");

        //Debug.Log("Coste antes --> " + costeConstruirMina);

        costeConstruirMina = costeConstruirMina -  10 * costeConstruirMina / 100;
        costeConstruirExtractor = costeConstruirExtractor - 10 * costeConstruirExtractor / 100;
        costeConstruirCuartel = costeConstruirCuartel - 10 * costeConstruirCuartel / 100;
        costeConstruirCasaHechizos = costeConstruirCasaHechizos - 10 * costeConstruirCasaHechizos / 100;
        costeConstruirMuro = costeConstruirMuro - 10 * costeConstruirMuro / 100;
        costeConstruirTorre = costeConstruirTorre - 10 * costeConstruirTorre / 100;
        costeConstruirTrampa = costeConstruirTrampa - 10 * costeConstruirTrampa / 100;

        //Debug.Log("Coste despues --> " + costeConstruirMina);

        return 0;
    }
    static int aumentarRadioHechizos()
    {
        Debug.Log("El radio de los hechizos ha aumentado");
        BluePrintHechizos.aumentoRadio *= 3f;
        RayoScript.aumentoRadio *= 3f;
        BuffScript.aumentoRadio *= 3f;
        HealScript.aumentoRadio *= 3f;
        return 0;
    }
    static int aumentarProduccionMinas20()
    {
        //aumenta un 20% la produccion de las minas
        Debug.Log("Has mejorado la produccion de las minas un 20%");
        //cambiar la variable 
        Mina.mejoraDeAldeaProduccionOro = Mina.mejoraDeAldeaProduccionOro *  1.2f;
        return 0;
    }
    public void ShowMessage(string text)
    {

        //GameObject go = Instantiate(textoAvisoFlotante, hudPrincipal.transform);
        //go.GetComponent<Text>().text = text;
        GameObject go = Instantiate(textoAvisoFlotante);
        go.GetComponentInChildren<Text>().text = text;
        Destroy(go, duracionAviso);
    }

    public void GameOver()
    {
        if (hudGameOver != null)
        {
            hudGameOver.SetActive(true);
            hudGameOver.GetComponent<GameOver>().UpdateStats();

        }
       
    }
}
