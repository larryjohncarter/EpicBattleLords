using System;
using TMPro;
using UnityEngine;

public class HeroCollectionUI : MonoBehaviour
{
    [SerializeField] private GameObject _heroPanelPrefab;
    [SerializeField] private Transform _heroSelectionPanel;
    [SerializeField] private TextMeshProUGUI _selectedHeroCountText;
    private HeroCollectionManager _heroCollectionManager;

    public static Action<int,int> OnHeroAmountChange;
    
    private void OnEnable()
    {
        OnHeroAmountChange += SelectedHeroAmount;
    }

    private void OnDisable()
    {
        OnHeroAmountChange -= SelectedHeroAmount;
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
        var heroes = _heroCollectionManager.GetAllHeroes();
        foreach (var hero in heroes)
        {
            var newHeroButton = Instantiate(_heroPanelPrefab, _heroSelectionPanel);
            var heroPanelUIController = newHeroButton.GetComponent<HeroPanelUIController>();
            heroPanelUIController.SetNameText(hero.CombantantConfig.Name);
            heroPanelUIController.SetCombantant(hero);
            heroPanelUIController.SetHero(hero);
            if (hero.CombantantConfig.IsUnlocked)
            {
                heroPanelUIController.SetButtonListener(()=>ToggleHeroSelection(hero));
            }
            else
                heroPanelUIController.Button.interactable = false;
        }
    }

    private void SelectedHeroAmount(int currentAmount, int maxAmount)
    {
        _selectedHeroCountText.text = $"{currentAmount}/{maxAmount}";
    }


    private void ToggleHeroSelection(Hero hero)
    {
        if (hero.CombantantConfig.IsSelected)
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
