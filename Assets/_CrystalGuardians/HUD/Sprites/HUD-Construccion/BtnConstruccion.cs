using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnConstruccion : MonoBehaviour
{
    [Header("Campos del prefab")]
    [SerializeField]
    private Image imgEstructura;
    [SerializeField]
    private Button btn;
    [SerializeField]
    private Text textPrecio;

    [Header("Campos personalizables")]
    [SerializeField]
    private string strPrecio;
    [SerializeField]
    private Sprite paramImgEstructura;

    private bool selected = false;
    private bool available = false;
    private bool enoughLevel = false;
    private Color colorPrimary = new Color(208, 156, 45);

    public string textNoSePuedeConstruir;

   
    public bool Selected
    {
        get => selected;
        private set
        {
            if (selected == value) return;
            selected = value;

           /* Color c = imgEstructura.color;
            c.a = selected ? 1f : .5f;
            imgEstructura.color = c;*/

            Color cBtn = btn.image.color;
            cBtn.a = selected ? 1f : .5f;
            btn.image.color = cBtn;
        }
    }


    public bool Available
    {
        get => available;
        set
        {
            available = value;
            //btn.interactable = value;
            if (!enoughLevel)
            {
                setColor(available ? colorPrimary : Color.gray);
            }
            else if(available==false)
            {
                setColor(available ? colorPrimary : Color.red);
            }
            
        }
    }
    
    public bool EnoughLevel
    {
        get => enoughLevel;
        set
        {
            enoughLevel = value;
            btn.interactable = value;
            //btn.enabled = value;
            setColor(enoughLevel ? colorPrimary : Color.gray);
        }
    }


    // cambiar el color del boton y de la imagen
    private void setColor(Color newC)
    {

        Color c = imgEstructura.color;
        imgEstructura.color = newC;

        Color cBtn = btn.image.color;
        cBtn = newC;
        btn.image.color = cBtn;
    }

    private void Start()
    {
        textPrecio.text = strPrecio;
        imgEstructura.sprite = paramImgEstructura;
    }


}
