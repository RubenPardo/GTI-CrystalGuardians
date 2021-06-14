using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    string frase = "Bienvenido a Crystal Guardians . Tu objetivo en esta aventura será defender nuestra aldea de los enemigos del bosque, para ello deberás recolectar recursos y construir defensas.";
    public Text texto;
    public bool estaEscrito = true;

    public int indicePasosTuto = 0;

    public  GameObject panelRecursos;

    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(maquinaEscribir());
        
    }
    public void escribirTexto()
    {
        StartCoroutine(maquinaEscribir());
    }
    // Update is called once per frame
    void Update()
    {
        if(estaEscrito == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                indicePasosTuto++;
                estaEscrito = false;

            }
        }
            
        
        
        if(indicePasosTuto == 1)
        {
            
            if(estaEscrito == false)
            {
                frase = "Estos son tus contadores de recursos.Primero tenemos el oro , que te ayudará a construir y mejorar tus estructuras y defensas.";
                estaEscrito = true;
                mostrarPaneles(1);
                escribirTexto();
            }
            
        }
        if (indicePasosTuto == 2)
        {
            
            if (estaEscrito == false)
            {
                frase = "Luego nuestro bien más preciado , el obsidium , utilízalo para entrenar a nuestros guerreros e invocar hechizos.Como puedes ver debajo del obsidium se muestra la cantidad máxima de guerreros que pueden ser entrenados , mejora tus cuarteles para aumentar esa cifra.";
                estaEscrito = true;
                escribirTexto();
            }

        }
        if (indicePasosTuto == 3)
        {

        }
        if (indicePasosTuto == 4)
        {

        }

    }

    public  void mostrarPaneles(int i )
    {
        if(i == 1)
        {
            panelRecursos.SetActive(true);
        }
    }
    IEnumerator maquinaEscribir()
    {
        if (texto != null)
        {
            texto.text = "";
            foreach (char caracter in frase)
            {


                texto.text = texto.text + caracter;
                yield return new WaitForSeconds(0.02f);

            }
            
        }
        estaEscrito = true;
    }
}
