using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecursosObsidium : MonoBehaviour
{

    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        //text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (GameManager.Instance.Obsiidum >= 1000 && GameManager.Instance.Obsiidum < 1000000)
        {

            float cantidadRedondeada = GameManager.Instance.Obsiidum / 1000;
            //Debug.Log(cantidadRedondeada);
            text.text = cantidadRedondeada.ToString("f2") + "k";
        }
        else if (GameManager.Instance.Obsiidum >= 1000000)
        {
            float cantidadRedondeada = GameManager.Instance.Obsiidum / 1000000;
            text.text = cantidadRedondeada.ToString("f2") + "M";
        }
        else
        {
            text.text = GameManager.Instance.Obsiidum.ToString("f0");
        }

    }
}
