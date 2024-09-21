using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button _battleButton;
    [SerializeField] private Transform _selectedMoreThanRequiredHeroesPanel;
    [SerializeField] private TextMeshProUGUI _playerOrEnemyTurnText;
    [SerializeField] private GameObject _turnHudPanelObj;
    private void OnEnable()
    {
        EventManager.OnHeroSelected += BattleButtonState;
        EventManager.OnHeroSelectionMaxAmount += ShowHeroRequiredPanel;
        EventManager.OnTurnChangeTextSet += TurnTextSetter;
        EventManager.OnTurnHudPanelState += TurnHudPanelScale;
    }

    private void OnDisable()
    {
        EventManager.OnHeroSelected -= BattleButtonState;
        EventManager.OnHeroSelectionMaxAmount -= ShowHeroRequiredPanel;
        EventManager.OnTurnChangeTextSet -= TurnTextSetter;
        EventManager.OnTurnHudPanelState -= TurnHudPanelScale;
    }

    private void Awake()
    {
        SelectedMoreThanRequiredHeroesPanel(false,true);
        TurnHudPanelScale(false,true);
    }

    private void BattleButtonState(bool state)
    {
        _battleButton.interactable = state;
    }

    private void TurnTextSetter(bool playerTurn)
    {
        var text = playerTurn ? "Player" : "Enemy";
        _playerOrEnemyTurnText.text = text;
    }


    private void ShowHeroRequiredPanel() => StartCoroutine(SelectedMoreThanRequiredHeroCoroutine());
    
    private IEnumerator SelectedMoreThanRequiredHeroCoroutine()
    {
        SelectedMoreThanRequiredHeroesPanel(true);
        yield return new WaitForSeconds(1);
        SelectedMoreThanRequiredHeroesPanel(false);
    }
    
    private void TurnHudPanelScale(bool on, bool instant = false)
    {
        Toggle(_turnHudPanelObj.transform,on,instant);
    }
    private void SelectedMoreThanRequiredHeroesPanel(bool on, bool instant = false)
    {
        Toggle(_selectedMoreThanRequiredHeroesPanel,on,instant);
    }

    private void Toggle(Transform objToScale, bool on, bool instant = false)
    {
        var targetScale = on ? Vector3.one : Vector3.zero;
        var duration = instant ? 0 : 0.15f;
        objToScale.DOScale(targetScale, duration);
    }
}
