using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnemyHero : Enemy
{
    private Vector3 _originalPos;

    private void Start()
    {
        _originalPos = transform.position;
    }
    
    public override void Attack(Combantant target)
    {
        var targetPos = target.CombantantTargetPos;
        var targetHealthController = target.GetComponent<IHealthController>();
        transform.DOMove(targetPos.position, 1f).OnComplete(() =>
        {
            //TODO: Play a small animation
            targetHealthController.TakeDamage((int)AttackPower);
            StartCoroutine(TempReturnBackToPos());
        });
    }

    private IEnumerator TempReturnBackToPos()
    {
        yield return new WaitForSeconds(1);
        ReturnBackToPos();

    }
    private void ReturnBackToPos()
    {
        transform.DOMove(_originalPos, 0.5f).OnComplete(() =>
        {
            BattleManager.Instance.IsHeroTurn = true;
            EventManager.InvokeOnTurnChangeTextSet(BattleManager.Instance.IsHeroTurn);
        });
    }
    
    
}
