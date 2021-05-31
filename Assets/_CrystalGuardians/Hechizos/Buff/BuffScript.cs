using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffScript : Hechizo
{
    //ticksPorSegundo = 1 , hara un tick de sanacion cada 1s

    // Start is called before the first frame update
    public float[] statsDamagePorNivel;
    public float[] statsSpeedPorNivel;
    public float[] statsAttackSpeedPorNivel;
    internal static float aumentoRadio = 1f;
    public AudioSource sonidoHechizoBuff;

    void Start()
    {
        sonidoHechizoBuff.Play();

        transform.parent.localScale = new Vector3(transform.parent.localScale.x * aumentoRadio, transform.parent.localScale.y * aumentoRadio, transform.parent.localScale.z * aumentoRadio);
        var mainAreaParticulas = areaParticualas.main;
        mainAreaParticulas.startSize = mainAreaParticulas.startSize.constant * aumentoRadio;

        var shapeSistemaParticulas = areaParticualas.shape;
        shapeSistemaParticulas.radius = shapeSistemaParticulas.radius * aumentoRadio;
        spwanHechizo = Time.time;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Unidad"))
        {
            Aliado ali;
            ali = other.transform.parent.GetComponent<Aliado>();
            ali.buffDamage = 1f;
            ali.attackSpeed = ali.attackSpeed * statsAttackSpeedPorNivel[nivelActual];
            ali.agent.speed = ali.agent.speed * statsSpeedPorNivel[nivelActual];
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Unidad"))
        {
         
            Aliado ali;
            ali = other.transform.parent.GetComponent<Aliado>();
            ali.buffDamage = 1f;
            ali.attackSpeed = ali.attackSpeed / statsAttackSpeedPorNivel[nivelActual];
            ali.agent.speed = ali.agent.speed / statsSpeedPorNivel[nivelActual];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - spwanHechizo > duracionHechizo)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
