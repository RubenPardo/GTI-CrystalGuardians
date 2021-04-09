using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    
    public Text hudProba;


    // singleton

    static GameManager instance;
    public static GameManager Instance { get => instance; set => instance = value; }

    public bool seEstaConstruyendo = false; // cuando se pulsa un boton poner a true y deshabilitar todo hasta que se deje de construir
    public bool SeEstaConstruyendo { get => seEstaConstruyendo; set => seEstaConstruyendo = value; }

    // casa de hechizos -----------
    public static int nivelMinimoCastilloCasaHechizos = 1;
    public static int costeConstruirCasaHechizos = 1440;

    // cuartel de unidades -----------
    public static int nivelMinimoCastilloCuartel = 0;
    public static int costeConstruirCuartel = 400;
    public static int topeCuartelUnidades = 4;
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
    public static int costeConstruirMuro = 500;

    // mina -----------
    public static int nivelMinimoCastilloMina = 0;
    public static int costeConstruirMina = 3150;

    // extractor -----------
    public static int nivelMinimoCastilloExtractor = 0;
    public static int costeConstruirExtractor = 2160;

    //recursos -------------
    private float oro = 0;
    private float obsidium = 0;
    public bool oroConstruido = false;
    public bool obsidiumConstruido = false;

    public GameObject prefabOro;
    public GameObject prefabObsidium;
    public float Oro { get => oro; set => oro = value; }
    public float Obsiidum { get => obsidium; set => obsidium = value; }

    public bool OroConstruido { get => oroConstruido; set => oroConstruido = value; }
    public bool ObsidiumConstruido { get => obsidiumConstruido; set => obsidiumConstruido = value; }


    public GameObject castillo; // se construira al inicio

    public int i = 0;
    public int y = 0;

    // Start is called before the first frame update
    void Start()
    {
        oro = 8000;
        if(instance == null)
        {
            instance = this;

            Instantiate(castillo, transform.position, transform.rotation);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    // Update is called once per frame

    
    void Update()
    {
        /*/generacion de recursos manual
        Vector3 posicionOro = new Vector3(0, 0, 0);
        Vector3 posicionObsidium = new Vector3(3, 0, 0);
        if (OroConstruido)
        {
            if (oro < 999999999.0f)//mina lvl-1
            {
                oro = oro + 100 * Time.deltaTime;

            }
        }
        if (ObsidiumConstruido)
        {
            Obsiidum = Obsiidum + 10000;
        }
        hudProba.text = "" + oro.ToString("f0");
        */

        /*if (Input.GetKeyDown(KeyCode.Space) && i == 0)
        {
            Debug.Log("generador de oro construido");
            Instantiate(prefabOro, posicionOro, transform.rotation);
            OroConstruido = true;
            i++;
        }

        if (Input.GetKeyDown(KeyCode.Return) && y == 0)
        {
            Debug.Log("El obsidium aumenta");
            Instantiate(prefabObsidium, posicionObsidium, transform.rotation);
            ObsidiumConstruido = true;
            y++;
        }*/
    }
}
