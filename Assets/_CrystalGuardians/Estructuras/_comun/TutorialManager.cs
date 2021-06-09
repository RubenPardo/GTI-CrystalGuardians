using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] popUps;
    private int indexPopUps;

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < popUps.Length; i++)
        {
            if(i == indexPopUps)
            {
                popUps[i].SetActive(true);
            }
            else
            {
                popUps[i].SetActive(false);
            }
        }
        if(indexPopUps == 0)
        {

        }
    }
}
