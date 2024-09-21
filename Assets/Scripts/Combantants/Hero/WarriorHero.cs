using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class WarriorHero : Hero
{
    private Animator _animator;
    private Vector3 _originalPos;
    private static readonly int Attack1 = Animator.StringToHash("Attack");

    void Start()
    {
        _animator = GetComponent<Animator>();
        _originalPos = transform.position;
        base.Start();
    }
    
    public override void Attack(Combantant target)
    {
        var targetPos = target.CombantantTargetPos.position;
        var targetHealthController = target.GetComponent<IHealthController>();
        transform.DOMove(targetPos, 1).OnComplete(() =>
        {
            targetHealthController.TakeDamage((int)AttackPower);
            _animator.SetTrigger(Attack1);
            TestFloatText(target.transform);
        });
    }

    public void ReturnBackToPos()
    {
        transform.DOMove(_originalPos, 0.5f).OnComplete(() =>
        {
            BattleManager.Instance.IsHeroTurn = false;
            EventManager.InvokeOnTurnChangeTextSet(BattleManager.Instance.IsHeroTurn);
        });
    }
}
