using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HUDShake : MonoBehaviour
{
    public float power = 5.0f;
    public float duration = 0.05f;
    public float slowDownAmount = 0.5f;
    public GameObject imgGolpe;
    public bool shouldShake = false;

    Vector3 startPosition;
    float initialDuration;

    private void Start()
    {
        
        startPosition = transform.localPosition;
        initialDuration = duration;
    }

    private void Update()
    {
        if (shouldShake)
        {
            if(duration > 0)
            {
                imgGolpe.SetActive(true);
                transform.localPosition = startPosition + Random.insideUnitSphere * power;
                duration -= Time.deltaTime * slowDownAmount;
            }
            else
            {
                
                imgGolpe.SetActive(false);
                shouldShake = false;
                duration = initialDuration;
                transform.localPosition = startPosition;
            }
        }
    }
     
}
