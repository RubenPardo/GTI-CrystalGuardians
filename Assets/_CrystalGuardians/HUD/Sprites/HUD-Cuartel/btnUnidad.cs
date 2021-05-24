using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btnUnidad : MonoBehaviour
{

    [Header("Campos del prefab")]
    [SerializeField]
    private Image imgUnidad;
    [SerializeField]
    private Button btn;
    [SerializeField]
    private Text textPrecio;

    [Header("Campos personalizables")]
    [SerializeField]
    private Sprite paramImgUnidad;
    [SerializeField]
    private CuartelUnidades cuartel;


    private Color colorPrimary = new Color(208, 156, 45);
    private bool available = false;
    private Aliado unidad;

    public void setUnidad(Aliado aliado)
    {
        this.unidad = aliado;
    }

    public bool Available
    {
        get => available;
        set
        {
            available = value;
            btn.interactable = value;
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
        imgUnidad.sprite = paramImgUnidad;
    }

    // Update is called once per frame
    void Update()
    {
        if (unidad != null){
            textPrecio.text = unidad.costePorNivel[cuartel.nivelActual].ToString();
        }

    }
}
