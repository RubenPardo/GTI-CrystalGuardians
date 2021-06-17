using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class MejorasController : MonoBehaviour
{
    // Start is called before the first frame update

    //panel general
    public GameObject panelGeneral;

    //Parametros del UI
    public RawImage contenedorCarta1;
    public Text titulo1;
    public Text descripcionCarta1;
    public RawImage iconoCarta1;

    public RawImage contenedorCarta2;
    public Text titulo2;
    public Text descripcionCarta2;
    public RawImage iconoCarta2;

    public RawImage contenedorCarta3;
    public Text titulo3;
    public Text descripcionCarta3;
    public RawImage iconoCarta3;

    //plantillas de las cartas
    public Texture plantillaClaseUnidades;
    public Texture plantillaClaseEstructuras;
    public Texture plantillaClaseHechizos;
    public Texture plantillaClaseRecursos;
    public Texture iconoClaseUnidad;
    public Texture iconoClaseEstructura;
    public Texture iconoClaseHechizos;
    public Texture iconoClaseRecursos;

    //botones que representan a cada carta
    public Button botonCarta1;
    public Button botonCarta2;
    public Button botonCarta3;

    //Listas que almacenan las cartas a utilizar
    List<Carta> listaCartas;
    List<Carta> listaTresCartas;

    //Texto a modificar del contenedor de mis mejoras
    public Text textoDeMisMejoras;
    public bool hasElegidoMejora ;

    //Musica de entre rondas para cuando se seleccione una carta
    public AudioClip musicaEntreRondas;


    private void Start()
    {
        //recibimos las cartas procedentes de GameManager
        listaCartas= GameManager.Instance.listaCartas;
        //elegimos tres cartas al azar de la lista de Game Manager
        listaTresCartas = ChooseThreeCartas(listaCartas);
        //personalizamos las  3 cartas que apareceran en el canvas de seleccion de cartas
        personalizarPanelSeleccion(listaTresCartas);
        //indicamos este booleano en falso para controlar el texto del contenedor mis mejoras
        hasElegidoMejora = false;
    }
    
    
    
    //metodo para elegir una carta al azar
    public   string ChooseUnaCarta(List<Carta> listaCartas)
    {
       
        
        return listaCartas[Random.Range(0, listaCartas.Count)].Titulo;
    }

    //metodo para elegir tres cartas al azar
    public List<Carta> ChooseThreeCartas(List<Carta> listaGeneral)
    {
        List<Carta> cartasElegidas = new List<Carta>();
        for(int i = 0; i < 3; i++)
        {
            cartasElegidas.Add(listaGeneral[Random.Range(0, listaGeneral.Count)]);
        }
        
        return cartasElegidas;
    }

    //metodo que elige el contenedor y el icono a mostrar dependiendo de la clase de la carta
    public void elegirClase(Carta carta , RawImage contenedor , RawImage icono)
    {
        if(carta.Clase == "unidades")
        {

            icono.texture = iconoClaseUnidad;
            contenedor.texture = plantillaClaseUnidades;

        }
        else if(carta.Clase == "estructuras")
        {
            icono.texture = iconoClaseEstructura;
            contenedor.texture = plantillaClaseEstructuras;
        }
        else if (carta.Clase == "hechizos")
        {
            icono.texture = iconoClaseHechizos;
            contenedor.texture = plantillaClaseHechizos;
        }
        else
        {
            icono.texture = iconoClaseRecursos;
            contenedor.texture = plantillaClaseRecursos;

        }
    }

    //metodo que personaliza las tres cartas que se muestran en el selector de mejoras
    public void personalizarPanelSeleccion(List<Carta> lista3Cartas)
    {
        for(int i = 0; i<3; i++)
        {
            if(i == 0)
            {
                Carta cartaElegida = lista3Cartas[i];
                elegirClase(cartaElegida, contenedorCarta1 , iconoCarta1);
                titulo1.text = cartaElegida.Titulo;
                descripcionCarta1.text = cartaElegida.Descripcion;

                //agregamos los eventos que sucederan al seleccionar la carta
                botonCarta1.onClick.AddListener(delegate { cartaElegida.startFunction(); });
                botonCarta1.onClick.AddListener(delegate { registrarCarta(cartaElegida.Titulo,cartaElegida.Descripcion); });
                
                 
            }
            if (i == 1)
            {
                Carta cartaElegida = lista3Cartas[i];
                elegirClase(cartaElegida, contenedorCarta2, iconoCarta2);
                titulo2.text = cartaElegida.Titulo;
                descripcionCarta2.text = cartaElegida.Descripcion;

                //agregamos los eventos que sucederan al seleccionar la carta
                botonCarta2.onClick.AddListener(delegate { cartaElegida.startFunction(); });
                botonCarta2.onClick.AddListener(delegate { registrarCarta(cartaElegida.Titulo, cartaElegida.Descripcion); });
                
            }
            if (i == 2)
            {
                Carta cartaElegida = lista3Cartas[i];
                elegirClase(cartaElegida, contenedorCarta3, iconoCarta3);
                titulo3.text = cartaElegida.Titulo;
                descripcionCarta3.text = cartaElegida.Descripcion;

                //agregamos los eventos que sucederan al seleccionar la carta
                
                botonCarta3.onClick.AddListener(delegate { cartaElegida.startFunction(); });
                botonCarta3.onClick.AddListener(delegate { registrarCarta(cartaElegida.Titulo, cartaElegida.Descripcion); });
            }
        }
    }

    //con este metodo al elegir una carta se generarán otras nuevas tres cartas
    public void nuevas3cartas()
    {
        listaTresCartas = ChooseThreeCartas(listaCartas);
        personalizarPanelSeleccion(listaTresCartas);

        
    }

    //con este metodo registramos la carta en mis mejoras

    public void registrarCarta(string titulo , string descripcion)
    {
        
     
        if (hasElegidoMejora)
        {
            textoDeMisMejoras.text += "-"  + titulo + ": " + descripcion + "\n";
            
        }
        else
        {
           textoDeMisMejoras.text = "-" + titulo + ": " + descripcion + "\n";
            
            hasElegidoMejora = true;
        }

        AudioSource source = GameManager.Instance.musicaAmbiente.GetComponent<AudioSource>();
        source.clip = musicaEntreRondas;
        source.Play();
        

    }

    public void Cerrar() 
    {

        //borramos los onclick anteriores
        botonCarta1.onClick.RemoveAllListeners();
        botonCarta2.onClick.RemoveAllListeners();
        botonCarta3.onClick.RemoveAllListeners();

        //elegimos las tres proximas nuevas cartas
        listaTresCartas = ChooseThreeCartas(listaCartas);
        personalizarPanelSeleccion(listaTresCartas);

        //cerramos el panel
        panelGeneral.SetActive(false);
    }
        



        
        
    }
    

