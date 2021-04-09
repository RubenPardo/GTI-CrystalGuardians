using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecursosOro : MonoBehaviour
{

    public Text textHud;

    // Start is called before the first frame update
    void Start()
    {
        //textHud = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

            
            if (GameManager.Instance.Oro >= 1000 && GameManager.Instance.Oro < 1000000)
            {

                float cantidadRedondeada = GameManager.Instance.Oro / 1000;
                //Debug.Log(cantidadRedondeada);
                textHud.text = cantidadRedondeada.ToString("f2") + "k";
            }
            else if (GameManager.Instance.Oro >= 1000000)
            {
                float cantidadRedondeada = GameManager.Instance.Oro / 1000000;
                textHud.text = cantidadRedondeada.ToString("f2") + "M";
            }
            else
            {
                textHud.text = GameManager.Instance.Oro.ToString("f0");
            }
        
        
    }
}
