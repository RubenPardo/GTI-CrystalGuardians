using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParpadearLuz : MonoBehaviour
{
    public bool estaParpadeando = false;
    public float timeDelay;

    // Update is called once per frame
    void Update()
    {
        if(estaParpadeando == false)
        {
            StartCoroutine(LuzParpadeante());
        }
    }
    IEnumerator LuzParpadeante()
    {
        estaParpadeando = true;
        this.gameObject.GetComponent<Light>().enabled = false;
        timeDelay = 0.35f;
        yield return new WaitForSeconds(timeDelay);
        this.gameObject.GetComponent<Light>().enabled = true;
        yield return new WaitForSeconds(timeDelay);
        estaParpadeando = false;
    }
}
