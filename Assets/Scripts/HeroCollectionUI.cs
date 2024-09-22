using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class HeroCollectionUI : MonoBehaviour
{
    [SerializeField] private GameObject _heroPanelPrefab;
    [SerializeField] private Transform _heroSelectionPanel;
    [SerializeField] private List<Transform> _uiToToggle = new();
    [SerializeField] private TextMeshProUGUI _selectedHeroCountText;
    
    private HeroCollectionManager _heroCollectionManager;

    public static Action<int,int> OnHeroAmountChange;
    private List<Hero> _alreadySpawnedHeroButton = new();
    private void OnEnable()
    {
        OnHeroAmountChange += SelectedHeroAmount;
        EventManager.OnBattleInitiated += ToggleHeroSelectionCanvas;
    }

    private void OnDisable()
    {
        OnHeroAmountChange -= SelectedHeroAmount;
        EventManager.OnBattleInitiated -= ToggleHeroSelectionCanvas;

    }

    private void Awake()
    {
        _heroCollectionManager = GetComponent<HeroCollectionManager>();
    }

    void Start()
    {
        PopulateHeroButtons();
    }

    private void PopulateHeroButtons()
    {
        var heroes = _heroCollectionManager.GetAvailableHeroes();
        foreach (var hero in heroes)
        {
            var newHeroButton = Instantiate(_heroPanelPrefab, _heroSelectionPanel);
            var heroPanelUIController = newHeroButton.GetComponent<HeroPanelUIController>();
            heroPanelUIController.SetNameText(hero.CombantantConfig.Name);
            heroPanelUIController.SetCombantant(hero);
            heroPanelUIController.SetHero(hero);
            if(!_alreadySpawnedHeroButton.Contains(hero))  _alreadySpawnedHeroButton.Add(hero);
            if (hero.IsUnlocked)
            {
                heroPanelUIController.SetButtonListener(()=>ToggleHeroSelection(hero));
            }
        }
    }

    public void RepopulateHeroButtons()
    {
        var heroes = _heroCollectionManager.GetAvailableHeroes();
        foreach (var hero in heroes)
        {
            if (_alreadySpawnedHeroButton.Contains(hero))
                continue;
            var newHeroButton = Instantiate(_heroPanelPrefab, _heroSelectionPanel);
            var heroPanelUIController = newHeroButton.GetComponent<HeroPanelUIController>();
            heroPanelUIController.SetNameText(hero.CombantantConfig.Name);
            heroPanelUIController.SetCombantant(hero);
            heroPanelUIController.SetHero(hero);
            _alreadySpawnedHeroButton.Add(hero);
            if (hero.IsUnlocked)
            {
                heroPanelUIController.SetButtonListener(()=>ToggleHeroSelection(hero));
            }
        }
    }

    private void SelectedHeroAmount(int currentAmount, int maxAmount)
    {
        _selectedHeroCountText.text = $"{currentAmount}/{maxAmount}";
    }

    private void ToggleHeroSelectionCanvas(bool on)
    {
        var targetScale = on ? Vector3.one : Vector3.zero;
        foreach (var ui in _uiToToggle)
        {
            ui.DOScale(targetScale, 0.2f);
        }
    }


    private void ToggleHeroSelection(Hero hero)
    {
        if (hero.HeroSelection.IsSelected)
        {
            _heroCollectionManager.DeselectHeroFromBattle(hero);
            HeroPanelUIController.OnHeroSelection.Invoke();
        }
        else
        {
            _heroCollectionManager.SelectHeroForBattle(hero);
            HeroPanelUIController.OnHeroSelection.Invoke();
        }
    }
}
