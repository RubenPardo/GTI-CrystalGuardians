using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Torre : Estructura
{


    public Text txtNivel;
    public Text txtMejora;
    
    public Text txtSaludActual;
    public Text txtSaludMejorada;
    public Text txtDañoActual;
    public Text txtDañoMejorada;
    public Text txtLvlActual;
    public Text txtLvlSiguiente;
    public Button btnMejorar;
    public Button btnMejorarInfo;
    

    // Storing different levels'
    public GameObject[] levels;

    [Header("Atributos")]
    public int[] danyoPorNivel;

    public override void abrirMenu()
    {
        canvas.SetActive(true);
    }

    public override void cerrarMenu()
    {
         canvas.SetActive(false);
    }

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

        GameManager.Instance.Oro = GameManager.Instance.Oro - costeOroMejorar[nivelActual];
        
        nivelActual = nivelActual + 1;


        settearVida();


        // actualizar hud informacion
        setUpCanvasValues();
        settearVida();
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.Oro = GameManager.Instance.Oro - GameManager.costeConstruirTorre;
        // canvas del menu de botones
        canvas = gameObject.transform.Find("Canvas").gameObject;
        if (canvas != null)
        {
            canvas.SetActive(false);
        }
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        setUpCanvasValues();
        settearVida();
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
        comprobarDisponibilidadMejora();
        comprobarVida0();
    }

    private void comprobarDisponibilidadMejora()
    {

        btnMejorar.enabled = (nivelActual <= NivelMaximo - 1) && GameManager.Instance.NivelActualCastillo >= nivelMinimoCastilloParaMejorar[nivelActual]
            && (GameManager.Instance.Oro >= costeOroMejorar[nivelActual]);

        btnMejorarInfo.enabled = (nivelActual <= NivelMaximo - 1) && GameManager.Instance.NivelActualCastillo >= nivelMinimoCastilloParaMejorar[nivelActual]
            && (GameManager.Instance.Oro >= costeOroMejorar[nivelActual]);
    }

    void Shoot ()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, bulletPoint.position, bulletPoint.rotation);
        Bala bala = bulletGO.GetComponent<Bala>();

        if (bala != null)
        {
            bala.damage = danyoPorNivel[nivelActual];
            bala.setTarget(target);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    private void setUpCanvasValues()
    {


        
        txtLvlActual.text = (nivelActual + 1).ToString();
        txtDañoActual.text = danyoPorNivel[nivelActual].ToString();
        txtSaludActual.text = vidaPorNivel[nivelActual].ToString();



        if (nivelActual < NivelMaximo)
        {
            txtLvlSiguiente.text = (nivelActual + 2).ToString();

            txtDañoMejorada.text = danyoPorNivel[nivelActual + 1].ToString();
            txtMejora.text = costeOroMejorar[nivelActual].ToString();

            txtSaludMejorada.text = vidaPorNivel[nivelActual + 1].ToString();
        }
        else{
            txtLvlSiguiente.text = "---------------";

            txtDañoMejorada.text = "---------------";
            txtMejora.text = "Nivel Maximo Alcanzado";

            txtSaludMejorada.text = "-------------";
        }


    }
}
