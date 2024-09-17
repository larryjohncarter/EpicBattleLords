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
    private void Awake()
    {
        _combantant = GetComponent<Combantant>();
        _mainCamera = Camera.main;
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
        _combantant.Health -= damage;
        if (_combantant.Health < 0) _combantant.Health = 0;
        
        Debug.Log($"{_combantant.Name} took {damage} damage.");
    }

    public bool IsAlive()
    {
        return _combantant.Health > 0;
    }

    private void HealthBarState()
    {
        
    }
    
}
