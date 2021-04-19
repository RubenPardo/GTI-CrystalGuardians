using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enmigoScript : MonoBehaviour
{
    public int vida = 100;
    public float speed;
    private bool dir;

    public HealthBarScript healthBar;
    // Start is called before the first frame update
    void Start()
    {
        dir = true;
    }

    // Update is called once per frame
    void Update()
    {
        float x;
        if (dir)
        {
            x = transform.position.x + speed * Time.deltaTime;
        }
        else {
            x = transform.position.x - speed * Time.deltaTime;
        }
        transform.position = new Vector3(x, transform.position.y, transform.position.z);


        if (transform.position.x >= 8f)
        {
            dir = false;
        }
        else if (transform.position.x <= -8f) {

            dir = true;
        }

        if(vida <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void setCurrentHealth(int health)
    {
       
        healthBar.SetHeatlh(health);
        vida = health;
    }
}
