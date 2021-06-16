using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class TutorialManager : MonoBehaviour
{
    string frase ;
    public Text texto;

    private bool pasoCumplido;

    private int indicePasosTuto = 0;

    //control de paneles
    public GameObject panelTutorial;
    public GameObject panelRecursos;
    public GameObject panelConstruccion;
    public GameObject indicadorPasosTutorial;
    public GameObject btnMejorasAldea;

    //control botones
    public GameObject btnMina;
    public GameObject btnExtractor;
    public GameObject btnTorre;
    public GameObject btnTrampa;
    public GameObject btnCasaHechizos;
    public GameObject btnCuartel;
    public GameObject btnMuro;

    //control textos
    public Text textoPasoIndicadorTutorial;

    //control estructuras
    private GameObject[] blueprints;
    bool hayMina = false;
    bool hayExtractor = false;
    bool hayTorre = false;
    bool hayMuro = false;
    bool hayCasaHechizos = false;
    bool hayCuartel = false;

    //enemigos 
    public GameObject prefabEM;
    public GameObject prefabED;
    private bool spawnMele = false;
    private bool spawnDist = false;

    public CameraeController cameraController;



    // Start is called before the first frame update
    void Start()
    {
       
        panelConstruccion.SetActive(false);
        frase = "Bienvenido a Crystal Guardians. Tu objetivo en esta aventura será defender nuestra aldea de los enemigos del bosque, para ello deberás recolectar recursos y construir defensas.";
        escribirTexto();
        pasoCumplido = true;
        

    }
    public void escribirTexto()
    {
        texto.text = frase;
    }
   
    // Update is called once per frame
    void Update()
    {
        cameraController.isActive = !panelTutorial.activeSelf;// bloqueamos los controles de la camara si esta el texto activo
        

        if (pasoCumplido)
        {
           
            if (Input.GetKeyUp(KeyCode.Space) || (Input.GetMouseButtonDown(0) && panelTutorial.activeSelf))
            {
                //cuando ya esta escrito paso al siguiente paso
                indicePasosTuto++;


            }
        }
        

        switch(indicePasosTuto){
            case 1:
                frase = "Estos son tus contadores de recursos. Primero tenemos el oro, que te ayudará a construir y mejorar tus estructuras y defensas.";
                //estaEscrito = true;
                mostrarPaneles(1);
                escribirTexto();
                break;
            case 2:
                frase = "Luego nuestro bien más preciado, el obsidium, utilízalo para entrenar a nuestros guerreros e invocar hechizos. Como puedes ver debajo del obsidium se muestra la cantidad máxima de guerreros que pueden ser entrenados, mejora tus cuarteles para aumentar esa cifra.";
                //estaEscrito = true;
                escribirTexto();
                break;
            case 3:
                frase = "Para generar recursos vamos a construir una mina y un extractor.";
                escribirTexto();
                break;

            case 4:
                
                //paso mina y extractor
                pasoCumplido = false;
                mostrarPaneles(2);
                if (!hayMina && !hayExtractor)
                {
                    activacionBotones(1);
                }
                
                foreach (GameObject g in GameManager.listaEstructurasEnJuego)
                {
                    if (g.GetComponent<Mina>() != null && !hayMina)
                    {
                        hayMina = true;
                        
                        habilitar(btnMina, false);
                        salirModoConstruccion();



                    }
                    else if (g.GetComponent<ExtractorObsidium>() != null && !hayExtractor)
                    {
                        
                        hayExtractor = true;
                        habilitar(btnExtractor, false); 
                        salirModoConstruccion();

                    }
                    
                }
                if (hayMina && hayExtractor)
                {
                    //Debug.Log("He entrado al if");
                    pasoCumplido = true;
                    indicePasosTuto++;
                }
                break;
            case 5:
                
                indicadorPasosTutorial.SetActive(false);
                panelTutorial.SetActive(true);

                frase = "Ahora vamos a crear muros y una torre.";
                escribirTexto();
                break;
            case 6:
                //paso muros y torre 
                pasoCumplido = false;
                mostrarPaneles(3);
                if (!hayTorre)
                {
                    activacionBotones(2);
                }
                foreach (GameObject g in GameManager.listaEstructurasEnJuego)
                {
                    if (g.GetComponent<Torre>() != null && !hayTorre)
                    {
                        hayTorre = true;
                        habilitar(btnTorre, false);
                        salirModoConstruccion();
                    }
                    else if (g.GetComponent<Muro>() != null && !hayMuro)
                    {
                        hayMuro = true;
                        

                    }
                   
                }
                if (hayTorre && hayMuro && !GameManager.Instance.SeEstaConstruyendo)
                {
                    pasoCumplido = true;
                    indicePasosTuto++;
                }
                break;
            case 7:
                indicadorPasosTutorial.SetActive(false);
                panelTutorial.SetActive(true);

                frase = "Vamos a crear un cuartel y unidades. Para ello construye un cuartel y haz click sobre los iconos de creación de tropas de la derecha de la pantalla. Recuerda que seleccionando las tropas con un barrido con <ClickIzquierdo> puedes dirigir a tus unidades haciendo click en cualquier parte de la aldea con <ClickDerecho>.";
                escribirTexto();
                break;
            case 8:
                //paso de crear unidades
                pasoCumplido = false;
                if (!hayCuartel)
                {
                    mostrarPaneles(4);
                }
                activacionBotones(3);
                foreach(GameObject g in GameManager.listaEstructurasEnJuego)
                {
                    if(g.GetComponent<CuartelUnidades>() != null && !hayCuartel)
                    {
                        habilitar(btnCuartel, false);
                        hayCuartel = true;
                        salirModoConstruccion();
                    }
                }
               
                if(GameManager.listaAliadosEnJuego.Count >= 2)
                {
                    pasoCumplido = true;
                    indicePasosTuto++;
                }
                break;
            case 9:
                indicadorPasosTutorial.SetActive(false);
                panelTutorial.SetActive(true);

                frase = "Para hacer tu aldea más poderosa deberás mejorar tus estructuras. Pulsa sobre el castillo central y mejora la estructura con el botón amarillo.";
                escribirTexto();
                break;
            case 10:
                //paso de mejorar castillo
                pasoCumplido = false;
                mostrarPaneles(5);
                activacionBotones(6);
                if(GameManager.Instance.NivelActualCastillo == 1)
                {
                    pasoCumplido = true;
                    indicePasosTuto++;
                }

                break;
            case 11:
                indicadorPasosTutorial.SetActive(false);
                panelTutorial.SetActive(true);

                frase = "Tal y como has hecho en el castillo, puedes mejorar todas las estructuras que construyas, pero recuerda, ¡debes administrar tus recursos!";
                escribirTexto();
                break;
            case 12:
                frase = "Nuestro castillo ya está a nivel 2. Construyamos una casa de hechizos. Al igual que en los cuarteles los hechizos también se crean desde un menú lateral. Esta vez está en la izquerda de tu pantalla. Los hechizos nos serán de gran ayuda para combatir a nuestros enemigos.";
                escribirTexto();
                break;
            case 13:
                //paso de construir casa de hechizos y lanzar hechizo
                pasoCumplido = false;
                mostrarPaneles(6);

                if (!hayCasaHechizos)
                {
                    activacionBotones(4);
                }

                foreach (GameObject g in GameManager.listaEstructurasEnJuego)
                {
                    if (g.GetComponent<CasaDeHechizos>() != null && !hayCasaHechizos)
                    {
                        hayCasaHechizos = true;
                        habilitar(btnCasaHechizos, false);
                        salirModoConstruccion();
                    }
                }

                if (GameManager.Instance.hechizosLanzados > 0)
                {
                    pasoCumplido = true;
                    indicePasosTuto++;
                }

                break;
            case 14:
                indicadorPasosTutorial.SetActive(false);
                panelTutorial.SetActive(true);
                frase = "Desde la capital nos comunican que cada cierto tiempo nos enviarán ayuda, estas serán las mejoras de aldea. Cuando elijas una de ellas podrás consultarlas haciendo click en el botón que se encuentra junto al botón de pausa.";
                escribirTexto();
                btnMejorasAldea.SetActive(true);
                break;
            case 15:
                
                frase = "¡Vaya!, parece que se acercan enemigos, utiliza tus tropas y tus defensas para defender la aldea.";
                escribirTexto();
                break;
            case 16:
                //paso de defender la aldea
                pasoCumplido = false;
                mostrarPaneles(7);
                activacionBotones(5);
                GameObject go;
                GameObject go2;
                if (!spawnMele)
                {
                    go2 = Instantiate(prefabEM);
                    GameManager.Instance.listaEnemigosRonda.Add(go2);
                    go2.transform.position = new Vector3(-20, 0);
                    spawnMele = true;
                }
                if(!spawnDist){
                    
                    go = Instantiate(prefabED);
                    GameManager.Instance.listaEnemigosRonda.Add(go);
                    go.transform.position = new Vector3(20, 0);
                    spawnDist = true;
                }
                if(spawnDist == true && spawnMele == true && GameManager.Instance.listaEnemigosRonda.Count ==  0)
                {
                    indicePasosTuto++;
                    pasoCumplido = true;
                }
                break;
            case 17:
                indicadorPasosTutorial.SetActive(false);
                panelTutorial.SetActive(true);
                frase = "¡Enhorabuena, has completado el tutorial de Crystal Guardians! Pulsa <Espacio> para empezar la partida.";
                escribirTexto();

                break;
            case 18:

                GameManager.isTutorialOn= false;
                GameManager.listaEstructurasEnJuego.Clear();
                GameManager.listaAliadosEnJuego.Clear();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);

                break;
        }

        

    }



    private void salirModoConstruccion()
    {
        GameManager.Instance.seEstaConstruyendo = false;
        Destroy(GameObject.FindGameObjectsWithTag("Blueprint")[0]);
    }
        
        
        

    

    public  void mostrarPaneles(int i )
    {
        
        switch (i)
        {
            case 1:
                panelRecursos.SetActive(true);
                break;
            case 2:
                desactivarPanelTutorial();
                panelConstruccion.SetActive(true);
                indicadorPasosTutorial.SetActive(true);
                textoPasoIndicadorTutorial.text = "Construye almenos una mina y un extractor.";
                break;
            case 3:
                desactivarPanelTutorial();
                
                indicadorPasosTutorial.SetActive(true);
                textoPasoIndicadorTutorial.text = "Construye muros y almenos una torre.";
                break;
            case 4:
                desactivarPanelTutorial();
                
                indicadorPasosTutorial.SetActive(true);
                textoPasoIndicadorTutorial.text = "Construye un cuartel y crea unidades.";

                break;
            case 5:
                desactivarPanelTutorial();

                indicadorPasosTutorial.SetActive(true);
                textoPasoIndicadorTutorial.text = "Mejora el castillo central pulsando sobre él.";
                break;
            case 6:
                desactivarPanelTutorial();

                indicadorPasosTutorial.SetActive(true);
                textoPasoIndicadorTutorial.text = "Construye la casa de hechizos, crea y utiliza un hechizo.";
                break;
            case 7:
                desactivarPanelTutorial();

                indicadorPasosTutorial.SetActive(true);
                textoPasoIndicadorTutorial.text = "¡Defiende la aldea!";
                break;

        }
    }
    public void activacionBotones(int i )
    {
        habilitar(btnCasaHechizos, false);
        habilitar(btnMuro, false);
        habilitar(btnTrampa, false);
        habilitar(btnTorre, false);
        habilitar(btnCuartel, false);
        habilitar(btnMina, false);
        habilitar(btnExtractor, false);
        
        switch (i)
        {
            case 1:
                habilitar(btnMina, true);
                habilitar(btnExtractor, true);
                break;
            case 2:
                habilitar(btnTorre, true);
                habilitar(btnMuro, true);
                break;
            case 3:
                habilitar(btnCuartel, true);
                break;
            case 4:
                habilitar(btnCasaHechizos, true);
                break;
            case 5:
                habilitar(btnCasaHechizos, true);
                habilitar(btnMuro, true);
                habilitar(btnTrampa, true);
                habilitar(btnTorre, true);
                habilitar(btnCuartel, true);
                habilitar(btnMina, true);
                habilitar(btnExtractor, true);
                break;
            case 6:
                habilitar(btnCasaHechizos, false);
                habilitar(btnMuro, false);
                habilitar(btnTrampa, false);
                habilitar(btnTorre, false);
                habilitar(btnCuartel, false);
                habilitar(btnMina, false);
                habilitar(btnExtractor, false);
                break;
        }
    }
    public void desactivarPanelTutorial()
    {
        panelTutorial.SetActive(false);
    }
    private void habilitar(GameObject prefabBtn, bool v)
    {
        prefabBtn.GetComponent<BtnConstruccion>().EnoughLevel = v;
        prefabBtn.GetComponent<BtnConstruccion>().Available = v;
        
        
    }


}
