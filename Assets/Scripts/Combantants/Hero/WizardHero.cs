using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using ZakhanSpellsPack;

public class WizardHero : Hero
{
    [SerializeField] private Transform _spellSpawnPos;
    public override void Attack(Combantant target)
    {
        var fireBallObj = ObjectPool.Instance.SpawnFromPool(0, _spellSpawnPos.position, Quaternion.identity);
        var fireBallTransform = fireBallObj.transform;
        var targetHealthController = target.GetComponent<IHealthController>();
        var fireBallProjectile = fireBallObj.GetComponent<Projectile>();
        var targetPos = target.transform.position;
        fireBallObj.SetActive(true);
        targetPos.y = 1;
        fireBallTransform.DOMove(targetPos, 1.5f).OnComplete(()=>
        {
            targetHealthController.TakeDamage((int)AttackPower);
            fireBallProjectile.Collision();
            fireBallObj.SetActive(false);
            base.Attack(target);
        });
    }
}
