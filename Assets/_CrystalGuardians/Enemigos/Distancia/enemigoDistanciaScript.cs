using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemigoDistanciaScript : EnemigoScript
{
    [Header("Unity SetUp")]
    //Bala a disparar
    public GameObject bulletPrefab;
    //Punto de disparo
    public Transform bulletPoint;
    //Caño a girar
    public Transform rotateObject;


    //cuenta atras para disparar
    private float fireCoutDwon = 0f;
    //Velocidad de giro del cañon
    public float turnSpeed = 10f;

    protected override void Start()
    {
        base.Start();
    }
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    override public void attack()
    {
        if (objetivoFijado != null)
        {
            Vector3 dir = objetivoFijado.transform.position - transform.position;
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

    public override List<GameObject> getPossibleTargets()
    {
        return GameManager.listaEstructurasEnJuego;
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, bulletPoint.position, bulletPoint.rotation);
        BalaMortero bala = bulletGO.GetComponent<BalaMortero>();

        if (bala != null)
        {
            bala.damage = danyoPorNivel[nivelActual];
            bala.setTarget(objetivoFijado.transform);
        }
    }
}
