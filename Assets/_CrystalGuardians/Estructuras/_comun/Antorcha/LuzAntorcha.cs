using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuzAntorcha : MonoBehaviour
{
    [SerializeField]
    private Light luz;
    [SerializeField]
    private GameObject fuego;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fuego != null)
        {
            fuego.SetActive(GameManager.Instance.lucesActivas);
        }
        luz.enabled = GameManager.Instance.lucesActivas;
    }
}
