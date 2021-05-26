using System;
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
    public Text txtDanyoActual;
    public Text txtDanyoMejorada;
    public Text txtLvlActual;
    public Text txtLvlSiguiente;
    public Button btnMejorar;
    public Button btnMejorarInfo;
   



    [Header("Atributos")]
    public int[] danyoPorNivel;
    public GameObject prefabLvl1;
    public GameObject prefabLvl2;
    public GameObject prefabLvl3;

    public GameObject cannon;
    public Material materialCannonNivel3;

    //particulas
    public GameObject particulasMejora;

    public override void abrirMenu()
    {
        if (canvas != null)
        {
            canvas.SetActive(true);
        }
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
    public GameObject posicionOrigenTorre;
    public Material materialRangeAttack;

    private GameObject rangeGameObject;


    public override void abrirMenu()
    {
        if (canvas != null)
        {
            canvas.SetActive(true);
            drawRangeAttack();
        }
    }

    public override void cerrarMenu()
    {
        if (canvas != null)
        {
            canvas.SetActive(false);
            removeRangeAttack();
        }
    }

    public override void mejorar()
    {

        GameManager.Instance.Oro = GameManager.Instance.Oro - costeOroMejorar[nivelActual];



        comprobarCambiarPrefab();

        nivelActual++;


        settearVida();


        // actualizar hud informacion
        setUpCanvasValues();
        settearVida();

        //emitir particulas
        ParticleSystem sistema = particulasMejora.GetComponent<ParticleSystem>();
        sistema.Play();

    }

    private void comprobarCambiarPrefab()
    {
        if (nivelActual > 0 && // para que no se salga del array
            nivelMinimoCastilloParaMejorar[nivelActual - 1] < nivelMinimoCastilloParaMejorar[nivelActual])
        {
            // se cambia el prefab cuando el siguiente nivel minimo de castillo cambia
            // si el anterior es menor 
            if(nivelMinimoCastilloParaMejorar[nivelActual] == 1)
            {
                // prefab nivel 2
                prefabLvl1.SetActive(false);
                prefabLvl2.SetActive(true);

                
            }
            else
            {
                // prefab nivel 3
                prefabLvl2.SetActive(false);
                prefabLvl3.SetActive(true);
                Renderer cannonRender = cannon.GetComponent<Renderer>();
                // no se puede modificar directamente, hay que referenciarlo como una copia
                var a = cannonRender.materials;
                a[0] = materialCannonNivel3; // cambiar el metal a por oro del ca�on
                cannonRender.materials = a;

            }

        }
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        GameManager.Instance.Oro = GameManager.Instance.Oro - GameManager.costeConstruirTorre;
        // canvas del menu de botones
        base.Start();
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        setUpCanvasValues();
        settearVida();

        if (GameManager.Instance.rangoAtaqueSiempreVisible)
            drawRangeAttack();
       
    }

    public void drawRangeAttack()
    {
        if(rangeGameObject == null)
        {
            rangeGameObject = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            rangeGameObject.GetComponent<CapsuleCollider>().enabled = false;
            rangeGameObject.transform.parent = gameObject.transform;
            rangeGameObject.transform.localScale = new Vector3(range * 2, -0.05f, range * 2);
            rangeGameObject.transform.position = new Vector3(posicionOrigenTorre.transform.position.x, 0f, posicionOrigenTorre.transform.position.z);
            rangeGameObject.GetComponent<MeshRenderer>().material = materialRangeAttack;
            /*var outline = rangeGameObject.AddComponent<Outline>();

            outline.OutlineMode = Outline.Mode.OutlineAll;
            outline.OutlineColor = Color.white;
            outline.OutlineWidth = 10f;*/
        }
        else
        {
            rangeGameObject.transform.localScale = new Vector3(range * 2, 0.1f, range * 2);
            rangeGameObject.SetActive(true);
        }
       
    }

    public void removeRangeAttack()
    {
        if (GameManager.Instance.rangoAtaqueSiempreVisible == false)
            rangeGameObject.SetActive(false);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies){
            float distanceToEnemy = Vector3.Distance(posicionOrigenTorre.transform.position, enemy.transform.position);

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
   protected override void Update()
    {
        base.Update();
        Animator animator = cannon.GetComponent<Animator>();
        ParticleSystem p = bulletPoint.GetComponentInChildren<ParticleSystem>();
        
        if (target != null)
        {
            Vector3 dir = target.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(rotateObject.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles; 
                //lookRotation.eulerAngles;
            rotateObject.rotation = Quaternion.Euler(0f, rotation.y, 0f);
           
            if (fireCoutDwon <= 0f)
            {
                
                
                animator.speed = attackSpeed;
                animator.SetBool("StartShot", true);
                p.Play();
                //ParticleSystem.EmissionModule emission = p.emission;
                //emission.enabled = true;
                Shoot();
                fireCoutDwon = 1f / attackSpeed;
            }

            fireCoutDwon -= Time.deltaTime;
        }
        else
        {
            animator.SetBool("StartShot", false);
        }
        comprobarDisponibilidadMejora();
        
    }

    private void comprobarDisponibilidadMejora()
    {

        bool v = (nivelActual <= NivelMaximo - 1) && GameManager.Instance.NivelActualCastillo >= nivelMinimoCastilloParaMejorar[nivelActual]
            && (GameManager.Instance.Oro >= costeOroMejorar[nivelActual]);

        btnMejorar.interactable = v;
        btnMejorarInfo.interactable = v;
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(posicionOrigenTorre.transform.position, range);
    }

    private void setUpCanvasValues()
    {


        
        txtLvlActual.text = "Torre Nivel "+(nivelActual + 1).ToString();
        txtDanyoActual.text = danyoPorNivel[nivelActual].ToString();
        txtSaludActual.text = vidaPorNivel[nivelActual].ToString();



        if (nivelActual < NivelMaximo)
        {

            txtMejora.text = costeOroMejorar[nivelActual].ToString();

        }
        else{
            btnMejorar.gameObject.SetActive(false);
            btnMejorarInfo.gameObject.SetActive(false);
        }


    }
}
