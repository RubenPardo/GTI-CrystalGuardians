using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecursosOro : MonoBehaviour
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
        if (GameManager.Instance.Oro >= 1000 && GameManager.Instance.Oro < 1000000)
        {
            float cantidadRedondeada = GameManager.Instance.Oro / 1000;
            //Debug.Log(cantidadRedondeada);
            text.text = cantidadRedondeada.ToString() + "k";
        }
        else if (GameManager.Instance.Oro >= 1000000)
        {
            float cantidadRedondeada = GameManager.Instance.Oro / 1000000;
            text.text = cantidadRedondeada.ToString() + "M";
        }
        else
        {
            text.text = GameManager.Instance.Oro.ToString();
        }
    }
}
