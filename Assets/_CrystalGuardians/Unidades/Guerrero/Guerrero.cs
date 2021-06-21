using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class Guerrero : Aliado
{
   
    public static float mejoraDanyoGuerrero = 1f;//mejora de aldea
                                        



    // Update is called once per frame
    protected override void Update()
    {

        base.mejoraDanyo = Guerrero.mejoraDanyoGuerrero;
        base.Update();
       
    }
    

    }




