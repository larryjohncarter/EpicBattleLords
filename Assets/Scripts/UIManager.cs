using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button _battleButton;
    [SerializeField] private Transform _selectedMoreThanRequiredHeroesPanel;
    [SerializeField] private TextMeshProUGUI _playerOrEnemyTurnText;
    [SerializeField] private GameObject _turnHudPanelObj;
    [SerializeField] private Transform _winPanel;
    [SerializeField] private Transform _losePanel;
    private void OnEnable()
    {
        EventManager.OnHeroSelected += BattleButtonState;
        EventManager.OnHeroSelectionMaxAmount += ShowHeroRequiredPanel;
        EventManager.OnTurnChangeTextSet += TurnTextSetter;
        EventManager.OnTurnHudPanelState += TurnHudPanelScale;
        EventManager.OnBattleEnd += TurnOnBattleEndPanel;
        EventManager.OnResetBattleResult += ResetBattleResultPanel;
    }

    private void OnDisable()
    {
        EventManager.OnHeroSelected -= BattleButtonState;
        EventManager.OnHeroSelectionMaxAmount -= ShowHeroRequiredPanel;
        EventManager.OnTurnChangeTextSet -= TurnTextSetter;
        EventManager.OnTurnHudPanelState -= TurnHudPanelScale;
        EventManager.OnBattleEnd -= TurnOnBattleEndPanel;
        EventManager.OnResetBattleResult -= ResetBattleResultPanel;

    }

    private void Awake()
    {
        SelectedMoreThanRequiredHeroesPanel(false,true);
        TurnHudPanelScale(false,true);
        ResetBattleResultPanel();
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

    private void TurnOnBattleEndPanel(bool hasWon)
    {
        if (hasWon)
        {
            _winPanel.DOScale(Vector3.one, 0.15f);
            _losePanel.DOScale(Vector3.zero, 0);
        }
        else
        {
            _losePanel.DOScale(Vector3.one, 0.15f);
            _winPanel.DOScale(Vector3.zero, 0);
        }
    }

    private void ResetBattleResultPanel()
    {
        _winPanel.DOScale(Vector3.zero, 0);
        _losePanel.DOScale(Vector3.zero, 0);

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
