using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{

    private Transform target;
    public float speed = 70f;
    public int damage;

    public void setTarget(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
        }
        else
        {

            Vector3 dir = target.position - transform.position;
            float distanceThisFrame = speed * Time.deltaTime;

            if (dir.magnitude <= distanceThisFrame)
            {
                HitTarget();

            }
            else
            {
                transform.Translate(dir.normalized * distanceThisFrame, Space.World);
            }
        }

        
    }

    void HitTarget()
    {
        enmigoScript enemigo = target.GetComponent<enmigoScript>();
        enemigo.setCurrentHealth(enemigo.vidaActual-damage);
        Destroy(gameObject);
    }
}
