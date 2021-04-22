using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Script que se asocia a un game object para que un canvas este donde esta y mire a camara
 */
public class MenuFollowCamera : MonoBehaviour
{

  
    [SerializeField] public Transform lookAt;
    [SerializeField] public Vector3 offset;

    private Camera cam;


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
       
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = cam.WorldToScreenPoint(lookAt.position + offset);

        if(transform.position != pos)
        {
            transform.position = pos;
        }

    }
}
