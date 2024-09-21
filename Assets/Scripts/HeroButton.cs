using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (_isHolding)
        {
            _holdTime += Time.deltaTime;
            if (_holdTime >= _holdDuration)
            {
                ShowHeroPopUpAtMousePosition();
                _isHolding = false;
            }
        }
    }
    public void OnPointerDown()
    {
        _isHolding = true;
        _holdTime = 0f;  // Reset hold time
    }

    // Method called when the player releases the button
    public void OnPointerUp()
    {
        _isHolding = false;
        _holdTime = 0f;
        if (_heroPopUpController != null)
            _heroPopUpController.ToggleOffAfterWhile();
    }

    private void ShowHeroPopUpAtMousePosition()
    {
        if (_heroPopUpController != null)
        {
            Vector3 mousePosition = Input.mousePosition;
            _heroPopUpController.SetHeroInfo(_heroPanelUIController.Combantant,_heroPanelUIController.Hero,mousePosition);
            _heroPopUpController.Toggle(true); 

        }
    }
}
