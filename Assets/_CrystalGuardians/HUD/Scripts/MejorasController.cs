using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MejorasController : MonoBehaviour
{
    // Start is called before the first frame update

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

    //Listas que almacenan las cartas a utilizar
    List<Carta> listaCartas;
    List<Carta> listaTresCartas;
    private void Start()
    {
        //recibimos las cartas procedentes de GameManager
        listaCartas= GameManager.Instance.listaCartas;
        //elegimos tres cartas al azar de la lista de Game Manager
        listaTresCartas = ChooseThreeCartas(listaCartas);
        //personalizamos las  3 cartas que apareceran en el canvas de seleccion de cartas
        personalizarPanelSeleccion(listaTresCartas);
    }
    
    
    
    //metodo para elegir una carta al azar
    public  string ChooseUnaCarta(List<Carta> listaCartas)
    {
        //Debug.Log("hola "+ listaStrings.Count);
        
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
        Debug.Log(cartasElegidas.Count);
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
            }
            if (i == 1)
            {
                Carta cartaElegida = lista3Cartas[i];
                elegirClase(cartaElegida, contenedorCarta2, iconoCarta2);
                titulo2.text = cartaElegida.Titulo;
                descripcionCarta2.text = cartaElegida.Descripcion;
            }
            if (i == 2)
            {
                Carta cartaElegida = lista3Cartas[i];
                elegirClase(cartaElegida, contenedorCarta3, iconoCarta3);
                titulo3.text = cartaElegida.Titulo;
                descripcionCarta3.text = cartaElegida.Descripcion;
            }
        }
    }
    
}
