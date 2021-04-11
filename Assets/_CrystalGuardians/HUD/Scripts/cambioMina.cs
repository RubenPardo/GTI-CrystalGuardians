using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cambioMina : MonoBehaviour
{
    // Start is called before the first frame update

    public Texture texturaGris;
    public Texture texturaRoja;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RawImage icono = GetComponent<RawImage>();
        if (GameManager.Instance.NivelMinimoCastilloMina > GameManager.Instance.NivelActualCastillo)
        {
            
            icono.texture = texturaGris;

        }else if(GameManager.Instance.Oro < GameManager.Instance.CosteConstruirMina)
        {
            icono.texture = texturaRoja;
        }
        
    }
}
