using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Wait : MonoBehaviour
{

    public float wait_time = 14;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Wait_intro());
    }
    IEnumerator Wait_intro()
    {
        yield return new WaitForSeconds(wait_time);

        SceneManager.LoadScene(1);


    }
   
}
