using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RangeHero : Hero
{
    [SerializeField] private Transform _arrowSpawnPos;

    private const int ArrowPoolId = 1;
    
    public override void Attack(Combantant target)
    {
        var targetHealthController = target.GetComponent<IHealthController>();
        SpawnAndShootArrow(target.transform, () =>
        {
            targetHealthController.TakeDamage((int)AttackPower);
        });
    }

    private void SpawnAndShootArrow(Transform target, Action OnMoveComplete)
    {
        var arrowObj = ObjectPool.Instance.SpawnFromPool(ArrowPoolId, _arrowSpawnPos.position, Quaternion.identity);
        var arrowTransform = arrowObj.transform;
        var targetPos = target.position;
        targetPos.y = 2;
        arrowTransform.DOMove(targetPos, 1f).OnComplete(() =>
        {
           OnMoveComplete?.Invoke(); 
           arrowObj.SetActive(false);
           BattleManager.Instance.IsHeroTurn = false;
           TestFloatText(target.transform);

        });
    }
}
