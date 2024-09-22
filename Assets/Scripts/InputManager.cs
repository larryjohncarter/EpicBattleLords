using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private float _holdTime;
    private bool _isHolding;
    private bool _popUpShown;
    private Camera _mainCamera;
    private Hero _heldHero;

    private const float HoldDuration = 3f;
    private HeroPopUpController _heroPopUpController;
    private void Start()
    {
        _mainCamera = Locator.Instance.MainCamera;
        _heroPopUpController = Locator.Instance.HeroPopUpController;
    }

    private void Update()
    {
        if (GameManager.Instance.GameStates != GameStates.Playing)
            return;
        if (Input.GetMouseButtonDown(0))
        {
            DetectHeroClick();
        }

        if (Input.GetMouseButton(0) && _heldHero != null)
        {
            _holdTime += Time.deltaTime;
            if (_holdTime >= HoldDuration)
            {
                _isHolding = true;
                _heroPopUpController.SetHeroInfo(_heldHero.CombantantConfig,_heldHero,Input.mousePosition);
                _heroPopUpController.Toggle(true); 
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (_isHolding)
            {
                ResetHold();
            }
            else
            {
                if (_heldHero != null)
                {
                    BattleManager.Instance.SelectHeroForAttack(_heldHero);
                }
            }
        }
    }
    private void DetectHeroClick(Vector2? touchPosition = null)
    {
        var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Hero hero = hit.collider.GetComponentInParent<Hero>();  // Detect the Hero component in the parent object

            if (hero != null)
            {
                _heldHero = hero;
                _holdTime = 0f;
            }
        }
    }

    private void ResetHold()
    {
        if (_heldHero != null)
        {
            _heroPopUpController.ToggleOffAfterWhile();
        }

        _heldHero = null;
        _isHolding = false;
        _holdTime = 0;
    }
}
