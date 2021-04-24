using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selection_component : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        gameObject.transform.parent.Find("boxSelection").gameObject.SetActive(true);
        //this.gameObject.Find("boxSelection").gameObject.SetActive(true);
        //GetComponent<Renderer>().material.color = Color.green;
    }

    private void OnDestroy()
    {
        gameObject.transform.parent.Find("boxSelection").gameObject.SetActive(false);
        //transform.Find("boxSelection").gameObject.SetActive(false);
        //GetComponent<Renderer>().material.color = Color.white;
    }
}
