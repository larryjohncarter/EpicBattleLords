using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Combantant : MonoBehaviour
{
    [SerializeField] protected Transform _combantantTargetPos;
    [SerializeField] private Combantant_SO _combantantConfig;

   // public Transform
    public Combantant_SO CombantantConfig
    {
        get => _combantantConfig;
        set => _combantantConfig = value;
    }
    public string Name { get; protected set; }
    public float AttackPower { get; protected set; }

    public abstract void Attack(Combantant target);
    
    
}
