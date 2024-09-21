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
        Debug.Log($"Setting Enemy AttackPower  to:{CombantantConfig.BaseAttackPower}");
        AttackPower = CombantantConfig.BaseAttackPower;
        _originalPos = transform.position;

    }
}