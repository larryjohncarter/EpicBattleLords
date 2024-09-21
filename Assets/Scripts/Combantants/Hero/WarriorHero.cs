using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class WarriorHero : Hero
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
        Debug.Log($"Warrior  Attacked: {target}");

    }
}
