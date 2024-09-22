using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class HeroCollectionManager : SingletonBehaviour<HeroCollectionManager>
{
    [SerializeField] private List<Hero> _allHeroes = new();
    private List<Hero> _selectedHeroes = new();
    public List<Hero> SelectedHeroes => _selectedHeroes;
    private const int maxHeroCollectionHero = 10;
    public int MaxHeroCollectionHero => maxHeroCollectionHero;
    public List<Hero> GetAvailableHeroes() => _allHeroes.FindAll(hero => hero.IsUnlocked);

    private HeroCollectionUI _heroCollectionUI;
    private void OnEnable()
    {
        ApplicationQuitOrPause.Add(SaveHeroIsUnlocked);
    }

    protected override void Awake()
    {
        LoadHeroIsUnlocked();
        base.Awake();
    }

    private void Start()
    {
        ResetHeroIsSelected();
        _heroCollectionUI = GetComponent<HeroCollectionUI>();
       HeroCollectionUI.OnHeroAmountChange.Invoke(_selectedHeroes.Count,Locator.Instance.GameSettings.MaxSelectedHeroAmount);
       EventManager.InvokeOnHeroSelected(_selectedHeroes.Count == 3);
       
    }

    private void ResetHeroIsSelected()
    {
        foreach (var hero in GetAvailableHeroes())
        {
            hero.IsSelected = false;
        }
    }

    public void SelectHeroForBattle(Hero hero)
    {
        if (_selectedHeroes.Count < 3 && _allHeroes.Contains(hero) && !_selectedHeroes.Contains(hero))
        {
            _selectedHeroes.Add(hero);
            hero.IsSelected = true;
            HeroCollectionUI.OnHeroAmountChange.Invoke(_selectedHeroes.Count,Locator.Instance.GameSettings.MaxSelectedHeroAmount);
            EventManager.InvokeOnHeroSelected(_selectedHeroes.Count == 3);
        }
        else
        {
            EventManager.InvokeOnHeroSelectionMaxAmount();
        }
    }

    public void DeselectHeroFromBattle(Hero hero)
    {
        if (_selectedHeroes.Contains(hero))
        {
            _selectedHeroes.Remove(hero);
            hero.IsSelected = false;
            HeroCollectionUI.OnHeroAmountChange.Invoke(_selectedHeroes.Count,Locator.Instance.GameSettings.MaxSelectedHeroAmount);
            EventManager.InvokeOnHeroSelected(_selectedHeroes.Count == 3);

        }
    }

    public void ResetSelectedHeroes()
    {
        ResetHeroIsSelected();
        _selectedHeroes.Clear();
        HeroCollectionUI.OnHeroAmountChange.Invoke(_selectedHeroes.Count,Locator.Instance.GameSettings.MaxSelectedHeroAmount);
        HeroPanelUIController.OnHeroSelection.Invoke();
    }

    public void AwardRandomHero()
    {
        List<Hero> availableHeroesToUnlock = _allHeroes.FindAll(hero => !hero.IsUnlocked);
        if (availableHeroesToUnlock.Count > 0)
        {
            Hero newHero = availableHeroesToUnlock[Random.Range(0, availableHeroesToUnlock.Count)];
            newHero.IsUnlocked = true;
            _heroCollectionUI.RepopulateHeroButtons();
        }
    }
    #region HeroIsUnlockedSave
    
    private void SaveHeroIsUnlocked()
    {
        foreach (var hero in _allHeroes)
        {
            ES3.Save($"hero_{hero.CombantantConfig.Name} Is unlocked", hero.IsUnlocked);
        }
    }

    private void LoadHeroIsUnlocked()
    {
        foreach (var hero in _allHeroes)
        {
            hero.IsUnlocked = ES3.Load($"hero_{hero.CombantantConfig.Name} Is unlocked",hero.CombantantConfig.DefaultIsUnlocked);
            if (hero.CombantantConfig.DefaultIsUnlocked)
            {
                hero.IsUnlocked = true;
            }
        }
    }
    #endregion
}
