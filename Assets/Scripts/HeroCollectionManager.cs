using System.Collections.Generic;
using UnityEngine;

public class HeroCollectionManager : SingletonBehaviour<HeroCollectionManager>
{
    [SerializeField] private List<Hero> _allHeroes = new();
    private List<Hero> _selectedHeroes = new();

    public List<Hero> SelectedHeroes => _selectedHeroes;
    private void Start()
    {
       HeroCollectionUI.OnHeroAmountChange.Invoke(_selectedHeroes.Count,Locator.Instance.GameSettings.MaxSelectedHeroAmount);
       EventManager.InvokeOnHeroSelected(_selectedHeroes.Count == 3);

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

    public List<Hero> GetAllHeroes() => _allHeroes;
    public List<Hero> GetSelectedHeroes() => _selectedHeroes;
}
