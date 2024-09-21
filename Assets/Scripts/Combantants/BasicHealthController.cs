using System;
using System.Collections;
using System.Collections.Generic;
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
        set
        {
            health = value;
            //TODO:Set HealthBar percentages  here
        }
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
            Debug.Log($"{_combantant.Name} has died! :( sad times!!! ");
            //TODO: Play a fake death animation
        }
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health < 0) Health = 0;
        
        Debug.Log($"{_combantant.Name} took {damage} damage.");
    }

    public bool IsAlive()
    {
        return Health > 0;
    }

    private void HealthBarState()
    {
        
    }
    
}
