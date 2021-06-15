using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    string frase ;
    public Text texto;

    public bool pasoCumplido;
    
    public int indicePasosTuto = 0;

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
    public GameObject[] blueprints;
    bool hayMina = false;
    bool hayExtractor = false;
    bool hayTorre = false;
    bool hayMuro = false;

    //enemigos 
    public GameObject prefabEM;
    public GameObject prefabED;
    public bool spawnMele = false;
    public bool spawnDist = false;
    


    // Start is called before the first frame update
    void Start()
    {
        frase = "Bienvenido a Crystal Guardians. Tu objetivo en esta aventura ser� defender nuestra aldea de los enemigos del bosque, para ello deber�s recolectar recursos y construir defensas.";
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
        if (pasoCumplido)
        {
           
            if (Input.GetKeyUp(KeyCode.Space))
            {
                //cuando ya esta escrito paso al siguiente paso
                indicePasosTuto++;


            }
        }
        

        switch(indicePasosTuto){
            case 1:
                frase = "Estos son tus contadores de recursos. Primero tenemos el oro, que te ayudar� a construir y mejorar tus estructuras y defensas.";
                //estaEscrito = true;
                mostrarPaneles(1);
                escribirTexto();
                break;
            case 2:
                frase = "Luego nuestro bien m�s preciado, el obsidium, util�zalo para entrenar a nuestros guerreros e invocar hechizos.Como puedes ver debajo del obsidium se muestra la cantidad m�xima de guerreros que pueden ser entrenados , mejora tus cuarteles para aumentar esa cifra.";
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
                activacionBotones(1);
                foreach (GameObject g in GameManager.listaEstructurasEnJuego)
                {
                    if (g.GetComponent<Mina>() != null)
                    {
                        hayMina = true;
                        
                        habilitar(btnMina, false);
                        /*
                        GameManager.Instance.SeEstaConstruyendo = false;
                        blueprints = GameObject.FindGameObjectsWithTag("Blueprint");
                        foreach(GameObject obj in blueprints)
                        {
                            Destroy(obj);
                        }
                        */

}
                    if (g.GetComponent<ExtractorObsidium>() != null)
                    {
                        
                        hayExtractor = true;
                        
                        habilitar(btnExtractor, false);
                        /*
                        GameManager.Instance.SeEstaConstruyendo = false;
                        blueprints = GameObject.FindGameObjectsWithTag("Blueprint");
                        foreach (GameObject obj in blueprints)
                        {
                            Destroy(obj);
                        }
                        */
                    }
                    if (hayMina && hayExtractor)
                    {
                        pasoCumplido = true;
                        indicePasosTuto++;
                    }
                }
                break;
            case 5:
                
                indicadorPasosTutorial.SetActive(false);
                panelTutorial.SetActive(true);

                frase = "Ahora vamos a crear muros y una torre";
                escribirTexto();
                break;
            case 6:
                //paso muros y torre 
                pasoCumplido = false;
                mostrarPaneles(3);
                activacionBotones(2);
                foreach (GameObject g in GameManager.listaEstructurasEnJuego)
                {
                    if (g.GetComponent<Torre>() != null)
                    {
                        hayTorre = true;
                        habilitar(btnTorre, false);
                    }
                    if (g.GetComponent<Muro>() != null)
                    {
                        hayMuro = true;
                        
                    }
                    if (hayTorre && hayMuro)
                    {
                        pasoCumplido = true;
                        indicePasosTuto++;
                    }
                }
                break;
            case 7:
                indicadorPasosTutorial.SetActive(false);
                panelTutorial.SetActive(true);

                frase = "Vamos a crear un cuartel y a crear unidades . Para ello construye un cuartel y haz click sobre los iconos de creaci�n de tropas de la derecha de la pantalla.Recuerda que seleccionando las tropas con un barrido con <ClickIzquierdo> puedes dirigir a tus unidades haciendo click en cualquier parte de la aldea con <ClickDerecho>.";
                escribirTexto();
                break;
            case 8:
                //paso de crear unidades
                pasoCumplido = false;
                mostrarPaneles(4);
                activacionBotones(3);
                foreach(GameObject g in GameManager.listaEstructurasEnJuego)
                {
                    if(g.GetComponent<CuartelUnidades>() != null)
                    {
                        habilitar(btnCuartel, false);
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

                frase = "Para hacer tu aldea m�s poderosa deber�s mejorar tus estructuras.Pulsa sobre el castillo central y mejora la estructura con el bot�n naranja.";
                escribirTexto();
                break;
            case 10:
                //paso de mejorar castillo
                pasoCumplido = false;
                mostrarPaneles(5);
                if(GameManager.Instance.NivelActualCastillo == 1)
                {
                    pasoCumplido = true;
                    indicePasosTuto++;
                }

                break;
            case 11:
                indicadorPasosTutorial.SetActive(false);
                panelTutorial.SetActive(true);

                frase = "Tal y como has hecho en el castillo, puedes mejorar todas las estructuras que construyas,pero recuerda,�debes administrar tus recursos!";
                escribirTexto();
                break;
            case 12:
                frase = "Nuestro castillo ya est� a nivel 2.Construyamos una casa de hechizos.Al igual que en los cuarteles los hechizos tambi�n se crean desde un men� lateral.Esta vez est� en la izquerda de tu pantalla.Los hechizos nos ser�n de gran ayuda para combatir a nuestros enemigos.";
                escribirTexto();
                break;
            case 13:
                //paso de construir casa de hechizos y lanzar hechizo
                pasoCumplido = false;
                mostrarPaneles(6);
                activacionBotones(4);
                if (GameManager.Instance.hechizosLanzados > 0)
                {
                    pasoCumplido = true;
                    indicePasosTuto++;
                }

                break;
            case 14:
                indicadorPasosTutorial.SetActive(false);
                panelTutorial.SetActive(true);
                frase = "Desde la capital nos comunican que cada cierto tiempo nos enviar�n ayuda, estas ser�n las mejoras de aldea.Cuando elijas una de ellas podr�s consultarlas haciendo click en el bot�n que se encuentra junto al bot�n de pausa.";
                escribirTexto();
                btnMejorasAldea.SetActive(true);
                break;
            case 15:
                
                frase = "�Vaya!, parece que se acercan enemigos , utiliza tus tropas y tus defensas para defender la aldea";
                escribirTexto();
                break;
            case 16:
                //paso de defender la aldea
                pasoCumplido = false;
                mostrarPaneles(7);
                GameObject go;
                GameObject go2;
                if (!spawnMele)
                {
                    go2 = Instantiate(prefabEM);
                    
                    go2.transform.position = new Vector3(-20, 0);
                    spawnMele = true;
                }
                if(!spawnDist){
                    
                    go = Instantiate(prefabED);
                    go.transform.position = new Vector3(20, 0);
                    spawnDist = true;
                }
                if(spawnDist == true && spawnMele == true && GameManager.Instance.listaEnemigosRonda.Count ==  0)
                {

                }
                break;
        }

        

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
                textoPasoIndicadorTutorial.text = "Construye una mina y un extractor";
                break;
            case 3:
                desactivarPanelTutorial();
                
                indicadorPasosTutorial.SetActive(true);
                textoPasoIndicadorTutorial.text = "Construye muros y una torre";
                break;
            case 4:
                desactivarPanelTutorial();
                
                indicadorPasosTutorial.SetActive(true);
                textoPasoIndicadorTutorial.text = "Construye un cuartel y crea unidades";

                break;
            case 5:
                desactivarPanelTutorial();

                indicadorPasosTutorial.SetActive(true);
                textoPasoIndicadorTutorial.text = "Mejora el castillo central pulsando sobre �l";
                break;
            case 6:
                desactivarPanelTutorial();

                indicadorPasosTutorial.SetActive(true);
                textoPasoIndicadorTutorial.text = "Construye la casa de hechizos, crea y utiliza un hechizo";
                break;
            case 7:
                desactivarPanelTutorial();

                indicadorPasosTutorial.SetActive(true);
                textoPasoIndicadorTutorial.text = "�Defiende la aldea!";
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
        }
    }
    public void desactivarPanelTutorial()
    {
        panelTutorial.SetActive(false);
    }
    private void habilitar(GameObject prefabBtn, bool v)
    {
        prefabBtn.GetComponent<BtnConstruccion>().EnoughLevel = v;
        
    }


}
