using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public string newGameScene;
    public GameObject panelLoad;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGame()
    {
        GameManager.isTutorialOn = false;
        panelLoad.SetActive(true);
        SceneManager.LoadScene(newGameScene);
    }

    public void PlayTutorial()
    {
        GameManager.isTutorialOn = true;
        panelLoad.SetActive(true);
        SceneManager.LoadScene(newGameScene);

    }

    public void Settings() {
    
    
    
    }

    public void Exit()
    {
        Application.Quit();
    }
}
