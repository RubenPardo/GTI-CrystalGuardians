using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btnHechizo : MonoBehaviour
{

    [Header("Campos del prefab")]
    [SerializeField]
    private Image imgHechizo;
    [SerializeField]
    private Button btn;
    [SerializeField]
    private Text textPrecio;

    [Header("Campos personalizables")]
    [SerializeField]
    private string strPrecio;
    [SerializeField]
    private Sprite paramImgHechizo;
    [SerializeField]
    private Color colorPrimary;

    
    private bool available = false;

    public bool Available
    {
        get => available;
        set
        {
            available = value;
            //btn.interactable = value;
            setColor(available ? colorPrimary : Color.red);
        }
    }

    private void setColor(Color newC)
    {

        /*Color c = imgEstructura.color;
        imgEstructura.color = newC;*/

        Color cBtn = btn.image.color;
        cBtn = newC;
        btn.image.color = cBtn;
    }

    // Start is called before the first frame update
    void Start()
    {
        textPrecio.text = strPrecio;
        imgHechizo.sprite = paramImgHechizo;
        setColor(colorPrimary);
    }
}
