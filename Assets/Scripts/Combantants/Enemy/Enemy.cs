using UnityEngine;

public abstract class Enemy : Combantant
{
    protected Vector3 _originalPos;
    
    private void Start()
    {
        Initialize();
    }
    private void Initialize()
    {
        AttackPower = CombantantConfig.BaseAttackPower;
        _originalPos = transform.position;
    }
}