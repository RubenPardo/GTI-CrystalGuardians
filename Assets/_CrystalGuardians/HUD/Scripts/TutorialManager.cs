using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    string frase ;
    public Text texto;
    /*
    public bool estaEscrito = true;
    public bool pasoCumplido ;
    */

    public int indicePasosTuto = 0;

    //control de paneles
    public GameObject panelTutorial;
    public GameObject panelRecursos;
    public GameObject panelConstruccion;
    public GameObject indicadorPasosTutorial;

    //control botones
    public Button btnMina;
    public Button btnExtractor;
    public Button btnTorre;
    public Button btnTrampa;
    public Button btnCasaHechizos;
    public Button btnCuartel;
    public Button btnMuro;

    //control textos
    public Text textoPasoIndicadorTutorial;

    //control estructuras
    bool hayMina = false;
    bool hayExtractor = false;

    // Start is called before the first frame update
    void Start()
    {
        
        
        frase = "Bienvenido a Crystal Guardians. Tu objetivo en esta aventura será defender nuestra aldea de los enemigos del bosque, para ello deberás recolectar recursos y construir defensas.";
        escribirTexto();

    }
    public void escribirTexto()
    {
        texto.text = frase;
    }
   
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(indicePasosTuto);
        if (Input.GetKeyUp(KeyCode.Space))
        {
            //cuando ya esta escrito paso al siguiente paso
            indicePasosTuto++;


        }

        switch(indicePasosTuto){
            case 1:
                frase = "Estos son tus contadores de recursos. Primero tenemos el oro, que te ayudará a construir y mejorar tus estructuras y defensas.";
                //estaEscrito = true;
                mostrarPaneles(1);
                escribirTexto();
                break;
            case 2:
                frase = "Luego nuestro bien más preciado, el obsidium, utilízalo para entrenar a nuestros guerreros e invocar hechizos.Como puedes ver debajo del obsidium se muestra la cantidad máxima de guerreros que pueden ser entrenados , mejora tus cuarteles para aumentar esa cifra.";
                //estaEscrito = true;
                escribirTexto();
                break;
            case 3:
                frase = "Para generar recursos vamos a construir una mina y un extractor.";
                escribirTexto();
                break;

            case 4:
                //paso mina y extractor

                mostrarPaneles(2);
                activacionBotones(1);
                foreach (GameObject g in GameManager.listaEstructurasEnJuego)
                {
                    if (g.GetComponent<Mina>() != null)
                    {
                        hayMina = true;
                    }
                    if (g.GetComponent<ExtractorObsidium>() != null)
                    {
                        hayExtractor = true;
                    }
                    if (hayMina && hayExtractor)
                    {
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
                mostrarPaneles(3);
                break;
            case 7:
                indicadorPasosTutorial.SetActive(false);
                panelTutorial.SetActive(true);

                frase = "Vamos a crear un cuartel y a crear unidades . Para ello construye un cuartel y haz click sobre los iconos de creación de tropas de la derecha de la pantalla.Recuerda que seleccionando las tropas con un barrido con <ClickIzquierdo> puedes dirigir a tus unidades haciendo click en cualquier parte de la aldea con <ClickDerecho>.";
                escribirTexto();
                break;
            case 8:
                //paso de crear unidades
                mostrarPaneles(4);
                break;
            case 9:
                indicadorPasosTutorial.SetActive(false);
                panelTutorial.SetActive(true);

                frase = "Para hacer tu aldea más poderosa deberás mejorar tus estructuras.Pulsa sobre el castillo central y mejora la estructura con el botón naranja.";
                escribirTexto();
                break;
            case 10:
                //paso de mejorar castillo
                mostrarPaneles(5);

                break;
            case 11:
                indicadorPasosTutorial.SetActive(false);
                panelTutorial.SetActive(true);

                frase = "Tal y como has hecho en el castillo, puedes mejorar todas las estructuras que construyas,pero recuerda,¡debes administrar tus recursos!";
                escribirTexto();
                break;
            case 12:
                frase = "Nuestro castillo ya está a nivel 2.Construyamos una casa de hechizos.Al igual que en los cuarteles los hechizos también se crean desde un menú lateral.Esta vez está en la izquerda de tu pantalla.Los hechizos nos serán de gran ayuda para combatir a nuestros enemigos.";
                escribirTexto();
                break;
            case 13:
                //paso de construir casa de hechizos
                mostrarPaneles(6);
                break;
            case 14:
                indicadorPasosTutorial.SetActive(false);
                panelTutorial.SetActive(true);
                frase = "¡Vaya!, parece que se acercan enemigos , utiliza tus tropas y tus defensas para defender la aldea";
                escribirTexto();
                break;
            case 15:
                //paso de defender la aldea
                mostrarPaneles(7);
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
                textoPasoIndicadorTutorial.text = "Mejora el castillo central pulsando sobre él";
                break;
            case 6:
                desactivarPanelTutorial();

                indicadorPasosTutorial.SetActive(true);
                textoPasoIndicadorTutorial.text = "Construye la casa de hechizos, crea y utiliza un hechizo";
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
        btnCasaHechizos.enabled = false;
        btnMuro.enabled = false;
        btnTrampa.enabled = false;
        btnTorre.enabled = false;
        btnCuartel.enabled = false;
        btnMina.enabled = false;
        btnExtractor.enabled = false;
        switch (i)
        {
            case 1:
                btnMina.enabled = true;
                btnExtractor.enabled = true;
                break;
        }
    }
    public void desactivarPanelTutorial()
    {
        panelTutorial.SetActive(false);
    }
    
    
}
