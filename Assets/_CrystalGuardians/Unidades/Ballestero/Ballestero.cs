using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using System.Linq;
using UnityEngine;

public class Ballestero : Aliado
{

    public static float mejoraDanyo = 1f;//mejora de aldea
                                         // Start is called before the first frame update
    protected override void Start()
    {
       base.Start();
        if (GameManager.Instance.rangoAtaqueSiempreVisible)
            drawRangeAttack();
    }


    // Update is called once per frame
    protected override void Update()
    {

        base.mejoraDanyo = Ballestero.mejoraDanyo;
        base.Update();

    }


}
