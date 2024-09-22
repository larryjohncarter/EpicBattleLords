using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class HeroCollectionManager : SingletonBehaviour<HeroCollectionManager>
{
    [SerializeField] private List<Hero> _allHeroes = new();
    private List<Hero> _selectedHeroes = new();
    private List<Hero> _availableHeroes;
    public List<Hero> SelectedHeroes => _selectedHeroes;
    private const int maxHeroCollectionHero = 10;
    public int MaxHeroCollectionHero => maxHeroCollectionHero;
    private void OnEnable()
    {
        ApplicationQuitOrPause.Add(SaveHeroIsUnlocked);
    }

    private void Start()
    {
       HeroCollectionUI.OnHeroAmountChange.Invoke(_selectedHeroes.Count,Locator.Instance.GameSettings.MaxSelectedHeroAmount);
       EventManager.InvokeOnHeroSelected(_selectedHeroes.Count == 3);
       LoadHeroIsUnlocked();
    }

    public void SelectHeroForBattle(Hero hero)
    {
        if (_selectedHeroes.Count < 3 && _allHeroes.Contains(hero) && !_selectedHeroes.Contains(hero))
        {
            _selectedHeroes.Add(hero);
            hero.HeroSelection.IsSelected = true;
            HeroCollectionUI.OnHeroAmountChange.Invoke(_selectedHeroes.Count,Locator.Instance.GameSettings.MaxSelectedHeroAmount);
            EventManager.InvokeOnHeroSelected(_selectedHeroes.Count == 3);
        }
        else
        {
            EventManager.InvokeOnHeroSelectionMaxAmount();
            Debug.LogWarning($"Cannot Select more than 3 heroes or hero not collected! ");
        }
    }

    public void DeselectHeroFromBattle(Hero hero)
    {
        if (_selectedHeroes.Contains(hero))
        {
            _selectedHeroes.Remove(hero);
            hero.HeroSelection.IsSelected = false;
            HeroCollectionUI.OnHeroAmountChange.Invoke(_selectedHeroes.Count,Locator.Instance.GameSettings.MaxSelectedHeroAmount);
            EventManager.InvokeOnHeroSelected(_selectedHeroes.Count == 3);

        }
    }

    // public List<Hero> GetAllHeroes() => _allHeroes;
    public List<Hero> GetAvailableHeroes() => _allHeroes.FindAll(hero => hero.IsUnlocked);
    public void AwardRandomHero()
    {
        List<Hero> availableHeroesToUnlock = _allHeroes.FindAll(hero => !hero.IsUnlocked);
        if (availableHeroesToUnlock.Count > 0)
        {
            Hero newHero = availableHeroesToUnlock[Random.Range(0, availableHeroesToUnlock.Count)];
            newHero.IsUnlocked = true;
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
        int unlockedCount = 0;
        
        foreach (var hero in _allHeroes)
        {
            hero.IsUnlocked = ES3.Load($"hero_{hero.CombantantConfig.Name} Is unlocked", hero.IsUnlocked);
            if (hero.IsUnlocked)
                unlockedCount++;
        }

        for (int i = 0; i < _allHeroes.Count && unlockedCount < 3; i++)
        {
            Hero hero = _allHeroes[i];

            // If the hero is locked, unlock it
            if (!hero.IsUnlocked)
            {
                hero.IsUnlocked = true;  // Unlock the hero
                unlockedCount++;

                // Save the unlocked status for the newly unlocked hero
                hero.IsUnlocked = ES3.Load($"hero_{hero.CombantantConfig.Name} Is unlocked", hero.IsUnlocked);
            }
        }
    }
    #endregion
}
