using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroPanelUIController : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _backGroundImage;
    [SerializeField] private TextMeshProUGUI _nameText;
    public Button Button => _button;
    public static Action OnHeroSelection;

    public Combantant_SO Combantant { get; private set;}
    public Hero Hero { get; private set; }
    

    private void OnEnable()
    {
        OnHeroSelection += SetImageBorderColor;
    }

    private void OnDisable()
    {
        OnHeroSelection -= SetImageBorderColor;
 
    }

    public void SetNameText(string name)
    {
        _nameText.text = name;
    }

    public void SetCombantant(Hero hero)
    {
        Combantant = hero.CombantantConfig;
    }
    public void SetHero(Hero hero)
    {
        Hero = hero;
    }
    
    public void SetButtonListener(Action eventInvoke)
    {
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(()=>
        {
            eventInvoke?.Invoke();
        });
    }

    private void SetImageBorderColor()
    {
        _backGroundImage.color = Hero.HeroSelection.IsSelected ? Color.green : Color.black;
    }
    
}
