using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HeroButton : MonoBehaviour
{
    private HeroPanelUIController _heroPanelUIController;
    private HeroPopUpController _heroPopUpController;
    private bool _isHolding;
    private float _holdTime;
    private float _holdDuration = 3f;

    void Start()
    {
        _heroPopUpController = Locator.Instance.HeroPopUpController;
        _heroPanelUIController = GetComponentInParent<HeroPanelUIController>();
    }

    void Update()
    {
        if (GameManager.Instance.GameStates != GameStates.HeroSelection) return;
        if (Input.GetMouseButtonDown(0))
        {
            if (IsPointerOverUIElemants())
            {
                _isHolding = true;
                _holdTime = 0f;  // Reset hold time
            }
        }
        if (Input.GetMouseButton(0) && _isHolding)
        {
            _holdTime += Time.deltaTime;
            if (_holdTime >= _holdDuration)
            {
                ShowHeroPopUpAtMousePosition();
                _isHolding = false;
                _holdTime = 0f;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            _isHolding = false;
            _holdTime = 0f;
            if (_heroPopUpController != null)
                _heroPopUpController.ToggleOffAfterWhile();
        }
    }

    private bool IsPointerOverUIElemants()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;  // Use touch position in mobile
        List<RaycastResult> results = new List<RaycastResult>();

        EventSystem.current.RaycastAll(eventData, results);
        foreach (RaycastResult result in results)
        {
            var heroButton = result.gameObject.GetComponent<HeroPanelUIController>();
            if (heroButton)  // Check if the UI element has a "HeroButton" tag
            {
                FillHeroPopUpInfo(heroButton.Hero);
                return true;  // We are over a button
            }
        }
        return false;  // No UI element was detected
    }
    
    private void ShowHeroPopUpAtMousePosition()
    {
        if (_heroPopUpController != null)
        {
            _heroPopUpController.Toggle(true); 
        }
    }

    private void FillHeroPopUpInfo(Hero hero)
    {
        if (_heroPopUpController != null)
        {
            _heroPopUpController.SetHeroInfo(hero.CombantantConfig,hero,Input.mousePosition);
        }
    }
}
