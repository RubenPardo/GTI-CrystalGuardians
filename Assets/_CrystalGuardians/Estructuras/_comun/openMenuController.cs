using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openMenuController : MonoBehaviour
{
    // script que controla abrir menus que bloquean la pantalla
    public GameObject panelAAbrir;
    public void isOpen(bool open)
    {

        Camera.main.GetComponent<CameraeController>().isActive = !open;// bloqueamos los controles de la camara
        panelAAbrir.SetActive(open);
       
    }
}
