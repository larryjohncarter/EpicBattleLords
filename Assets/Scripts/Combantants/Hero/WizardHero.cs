using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardHero : Hero
{
    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }


    public override void Attack(Combantant target)
    {
        Debug.Log($"Wizard  Attacked: {target}");
    }
}
