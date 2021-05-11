using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openMenuControllerInfo : MonoBehaviour
{
    // script que controla abrir menus que bloquean la pantalla
    public GameObject panelAAbrir;
    private CameraeController cameraController;
    private void Start()
    {
        cameraController = Camera.main.transform.parent.transform.GetComponent<CameraeController>();
        //CameraController.transform
    }
    public void isOpen(bool open)
    {


        cameraController.isActive = !open;// bloqueamos los controles de la camara
        panelAAbrir.SetActive(open);

    }
}
