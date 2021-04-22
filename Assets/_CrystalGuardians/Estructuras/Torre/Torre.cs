using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torre : Estructura
{

    [Header("Atributos")]
    public int[] danyoPorNivel;

    public float attackSpeed = 1f;
    private float fireCoutDwon = 0f;

    private Transform target;
    public float range = 15f;

    [Header("Unity SetUp")]

    public string enemyTag = "Enemigo";
    public Transform rotateObject;
    public float turnSpeed = 10f;

    public GameObject bulletPrefab;
    public Transform bulletPoint;
    


    public override void mejorar()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach(GameObject enemy in enemies){
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if(nearestEnemy!=null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            Vector3 dir = target.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(rotateObject.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles; 
                //lookRotation.eulerAngles;
            rotateObject.rotation = Quaternion.Euler(0f, rotation.y, 0f);

            if (fireCoutDwon <= 0f)
            {
                Shoot();
                fireCoutDwon = 1f / attackSpeed;
            }

            fireCoutDwon -= Time.deltaTime;
        }
        
       
    }

    void Shoot ()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, bulletPoint.position, bulletPoint.rotation);
        Bala bala = bulletGO.GetComponent<Bala>();

        if (bala != null)
        {
            bala.Seek(target);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
