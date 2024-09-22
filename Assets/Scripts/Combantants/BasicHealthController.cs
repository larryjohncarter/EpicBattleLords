using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BasicHealthController : MonoBehaviour, IHealthController
{
    [SerializeField] private Transform _healthBarTransform;
    [SerializeField] private Image _healthBarImage;
    private Combantant _combantant;
    private Camera _mainCamera;
    private float health;
    public float Health
    {
        get => health;
        set => health = value;
    }

    public float MaxHealth
    {
        get => _combantant.CombantantConfig.MaxHealth;
        set{}
    }

    private void Awake()
    {
        _combantant = GetComponent<Combantant>();
        _mainCamera = Camera.main;
    }

    private void Start()
    {
        Health = MaxHealth;
    }

    private void LateUpdate()
    {
        _healthBarTransform.LookAt(_mainCamera.transform);
    }

    public void Die()
    {
        if (!IsAlive())
        {
            transform.DOScale(Vector3.zero, 0.15f);
        }
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health < 0)
        {
            Health = 0;
        }
        HealthBar();

    }

    public bool IsAlive()
    {
        return Health > 0;
    }

    private void HealthBar()
    {
        var percentage = Health / MaxHealth;
        _healthBarImage.DOFillAmount(percentage, 0.5f).OnComplete(() =>
        {
            if(percentage  <= 0)
                Die();
        });
       
    }
}
