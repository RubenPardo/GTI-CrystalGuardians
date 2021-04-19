using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Carta 
{
    private string titulo;
    private string descripcion;
    private string clase;
    private Func<int> funcion;

    public string Titulo
    {
        get { return titulo; }
        set{ titulo = value; }
    }
    public string Descripcion
    {
        get { return descripcion; }
        set { descripcion = value; }
    }
    public string Clase
    {
        get { return clase; }
        set { clase = value; }
    }
    public Func<int> Funcion
    {
        get { return funcion; }
        set { funcion = value; }
    }

    public Carta(string t , string d , string c , Func<int> f)
    {

        Titulo= t;
        Descripcion = d;
        Clase = c;
        Funcion = f;

    }

    public void startFunction()
    {
        funcion();
    }

}
