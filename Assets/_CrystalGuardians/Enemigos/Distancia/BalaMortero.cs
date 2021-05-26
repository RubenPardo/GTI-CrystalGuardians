using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaMortero : MonoBehaviour
{

    private Transform target;
    public int damage;
    private Transform origin;
    public float secondsOnAir = 20f;
    public GameObject rangoExplosion;
    public GameObject bala;

    void Start()
    {
        origin = transform;
        Vector3 dir = target.position - transform.position;
        Rigidbody ro = transform.GetComponent<Rigidbody>();
        ro.velocity = CalculateVelocity();
        //transform. = CalculateVelocity();
        //transform.Translate(CalculateVelocity(), Space.World);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
        }
    }
    void HitTarget()
    {
        //EnemigoScript enemigo = target.GetComponent<EnemigoScript>();
        //enemigo.setCurrentHealth(enemigo.vidaActual - damage);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
      
        if (other.transform.CompareTag("Estructura") || ( other.transform.parent !=null && other.transform.parent.CompareTag("Estructura") )
            || other.transform.CompareTag("Suelo"))
        {
            bala.SetActive(false);
            rangoExplosion.SetActive(true);
            Rigidbody rb = transform.GetComponent<Rigidbody>();
            rb.isKinematic = true;
            rangoExplosion.GetComponentInChildren<ParticleSystem>().Play();
        }
    }
    public void setTarget(Transform _target)
    {
        target = _target;
    }


    //Calcular veolcidad de la parabola
    Vector3 CalculateVelocity()
    {
        Vector3 distance = target.position - origin.position;
        Vector3 distanceXZ = distance;
        distanceXZ.y = 0f;


        float Sy = distance.y;
        float Sxz = distanceXZ.magnitude;

        float Vxz = Sxz / secondsOnAir;
        float Vy = Sy / secondsOnAir +0.5f *Mathf.Abs( Physics.gravity.y) * secondsOnAir;

        Vector3 result = distanceXZ.normalized;

        result *= Vxz;
        result.y = Vy;

        return result;
    }
}
