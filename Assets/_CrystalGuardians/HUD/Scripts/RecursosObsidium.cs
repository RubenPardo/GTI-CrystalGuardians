using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecursosObsidium : MonoBehaviour
{

    Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.Obsiidum >= 1000 && GameManager.Instance.Obsiidum < 1000000)
        {
            float cantidadRedondeada = GameManager.Instance.Obsiidum / 1000;
            text.text =  cantidadRedondeada +"k";
        }
        else if (GameManager.Instance.Obsiidum >= 1000000)
        {
            float cantidadRedondeada = GameManager.Instance.Obsiidum / 1000000;
            text.text = cantidadRedondeada + "M";
        }
        else
        {
            text.text = GameManager.Instance.Obsiidum.ToString();
        }
        
    }
}
