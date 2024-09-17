using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Combantant
{
    public string Name { get; protected set; }
    public float Health { get; protected internal set; }
    public float AttackPower { get; protected set; }


    protected abstract void Attack(Combantant target);
    
    
}
