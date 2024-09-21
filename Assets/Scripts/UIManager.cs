using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button _battleButton;
    [SerializeField] private Transform _selectedMoreThanRequiredHeroesPanel;
    private void OnEnable()
    {
        EventManager.OnHeroSelected += BattleButtonState;
        EventManager.OnHeroSelectionMaxAmount += ShowHeroRequiredPanel;
    }

    private void OnDisable()
    {
        EventManager.OnHeroSelected -= BattleButtonState;
        EventManager.OnHeroSelectionMaxAmount -= ShowHeroRequiredPanel;

    }

    private void Awake()
    {
        SelectedMoreThanRequiredHeroesPanel(false,true);
    }

    private void BattleButtonState(bool state)
    {
        _battleButton.interactable = state;
    }


    private void ShowHeroRequiredPanel() => StartCoroutine(SelectedMoreThanRequiredHeroCoroutine());
    
    private IEnumerator SelectedMoreThanRequiredHeroCoroutine()
    {
        SelectedMoreThanRequiredHeroesPanel(true);
        yield return new WaitForSeconds(1);
        SelectedMoreThanRequiredHeroesPanel(false);
    }

    private void SelectedMoreThanRequiredHeroesPanel(bool on, bool instant = false)
    {
        var targetScale = on ? Vector3.one : Vector3.zero;
        var duration = instant ? 0 : 0.15f;
        _selectedMoreThanRequiredHeroesPanel.DOScale(targetScale, duration);
    }
}
