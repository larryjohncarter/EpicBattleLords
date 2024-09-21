using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Combantant : MonoBehaviour
{
    [SerializeField] protected Transform _combantantTargetPos;
    [SerializeField] private Combantant_SO _combantantConfig;
    
    public Combantant_SO CombantantConfig
    {
        get => _combantantConfig;
        set => _combantantConfig = value;
    }

    public Transform CombantantTargetPos => _combantantTargetPos;
    public string Name { get; protected set; }
    public float AttackPower { get; protected set; }


    public abstract void Attack(Combantant target);


    protected void TestFloatText(Transform target)
    {
        var camera = Locator.Instance.MainCamera;
        var spawnPos = camera.WorldToScreenPoint(target.position);
        spawnPos.y += transform.position.y + 3;
        var floatingTextObj = ObjectPool.Instance.SpawnFromPool(2, spawnPos, Quaternion.identity);
        var floatingText = floatingTextObj.GetComponent<FloatingTextController>();
        floatingText.SetFloatText((int)AttackPower);
    }
    
    
}
