using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeHero : Hero
{
    void Start()
    {
        
    }

    void Update()
    {
    }

    public override void Attack(Combantant target)
    {
        Debug.Log($"Ranger  Attacked: {target}");

    }
}
