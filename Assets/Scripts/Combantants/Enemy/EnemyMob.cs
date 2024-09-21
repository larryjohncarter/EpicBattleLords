using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnemyMob : Enemy
{
    
    public override void Attack(Combantant target)
    {
        var targetPos = target.CombantantTargetPos;
        var targetHealthController = target.GetComponent<IHealthController>();
        transform.DOMove(targetPos.position, 1f).OnComplete(() =>
        {
            Debug.Log($"Enemy Attack Power: {(int)AttackPower}");
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
