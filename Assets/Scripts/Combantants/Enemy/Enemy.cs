using UnityEngine;

public abstract class Enemy : Combantant
{
    public IHealthController BasicHealthController { get; private set; }
    private void Awake()
    {
        BasicHealthController = GetComponent<IHealthController>();
    }
    
    private void Start()
    {
        UpdateStats();
    }

    private void UpdateStats()
    {
        AttackPower = CombantantConfig.BaseAttackPower;
    }
}