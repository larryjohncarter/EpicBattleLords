using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class BattleManager : SingletonBehaviour<BattleManager>
{
    [SerializeField] private List<Transform> _heroSpawnPoints = new();
    [SerializeField] private Transform _enemySpawnPoint;
    [SerializeField] private GameObject _enemyPrefab;
    private List<Hero> _selectedHeroes;
    private List<Hero> _spawnedHeroes = new();
    private bool _isHeroTurn = true;
    private Hero _selectedHero;  // Hero Selected by  player to attack
    private Enemy _enemyInstance;
    private Coroutine _battleFlowCoroutine;
    private const int XpGain = 1;
    private int _battleCount;
    public bool IsHeroTurn
    {
        get => _isHeroTurn;
        set => _isHeroTurn = value;
    }

    private void OnEnable()
    {
        ApplicationQuitOrPause.Add(SaveBattleCount);
    }

    void Start()
    {
        _selectedHeroes = HeroCollectionManager.Instance.SelectedHeroes;
        LoadBattleCount();
    }
    
    public void SpawnCombatants()
    {
        _battleFlowCoroutine = StartCoroutine(TurnBasedCombat());
        for (var i = 0; i < _selectedHeroes.Count; i++)
        {
            if (i < _heroSpawnPoints.Count)
            {
                var heroInstance = Instantiate(_selectedHeroes[i].CombantantConfig.ModelPrefab,_heroSpawnPoints[i].position,Quaternion.identity);
                var hero = heroInstance.GetComponent<Hero>();
                _spawnedHeroes.Add(hero);
            }
        }

        var enemy = Instantiate(_enemyPrefab, _enemySpawnPoint.position, Quaternion.identity);
        _enemyInstance = enemy.GetComponent<EnemyMob>();
        EventManager.InvokeOnBattleInitiated(false);
        EventManager.InvokeOnTurnHudPanelState(true,false);
        GameManager.Instance.GameStates = GameStates.Playing;
        HeroCollectionManager.Instance.ResetSelectedHeroes();
    }

    public void SelectHeroForAttack(Hero hero)
    {
        if (_isHeroTurn)
        {
            _selectedHero = hero;
        }
    }
    
    private IEnumerator TurnBasedCombat()
    {
        while (true)
        {
            UpdateTurnText();
            yield return new WaitUntil(() => _selectedHero != null && _isHeroTurn);
            yield return HeroAttack(_selectedHero);
            _selectedHero = null;
            yield return new WaitUntil(() => !_isHeroTurn);
            UpdateTurnText();
            yield return EnemyAttack();

            if (CheckForWinOrLose())
            {
                break;
            }

            yield return new WaitForSeconds(1f);
        }
    }
    
    private void UpdateTurnText()
    {
        EventManager.InvokeOnTurnChangeTextSet(_isHeroTurn);
    }

    IEnumerator HeroAttack(Hero hero)
    {
        hero.Attack(_enemyInstance);
        yield return new WaitForSeconds(1f);
    }

    IEnumerator EnemyAttack()
    {
        var basicHealthController = _enemyInstance.GetComponent<IHealthController>();
        if (!basicHealthController.IsAlive())
            yield break;
        
        var aliveHeroes = _spawnedHeroes.FindAll(x => x.GetComponent<IHealthController>().IsAlive());
        if (aliveHeroes.Count > 0)
        {
            Hero targetHero = aliveHeroes[Random.Range(0, aliveHeroes.Count)];
            _enemyInstance.Attack(targetHero);
            yield return new WaitForSeconds(1f);
        }
    }

    private bool CheckForWinOrLose()
    {
        var allHeroesDefeated = true;
        foreach (var hero in _spawnedHeroes)
        {
            var basicHealthController = hero.GetComponent<IHealthController>();
            if (basicHealthController.IsAlive())
            {
                allHeroesDefeated = false;
                break;
            }
        }

        if (allHeroesDefeated)
        {
            EndBattle(false);
        }

        var enemyHealthController = _enemyInstance.GetComponent<IHealthController>();
        if (!enemyHealthController.IsAlive())
        {
            EndBattle(true);
            return true;
        }
        return allHeroesDefeated;
    }
    
    private void EndBattle(bool heroesWin)
    {
        GameManager.Instance.GameStates = GameStates.BattleResult;
        if (heroesWin)
        {
            foreach (var hero in _spawnedHeroes)
            {
                var basicHealthController = hero.GetComponent<IHealthController>();
                hero.GainXp(XpGain, basicHealthController.IsAlive());
            }
        }
        _battleCount++;
        var heroCollectionManager = HeroCollectionManager.Instance;
        if (_battleCount % 1 == 0 && heroCollectionManager.GetAvailableHeroes().Count <
            heroCollectionManager.MaxHeroCollectionHero)
        {
            heroCollectionManager.AwardRandomHero();
        }
        EventManager.InvokeOnBattleEnd(heroesWin); // Event to show the Win/Lose panel
        StopCoroutine(_battleFlowCoroutine); // Stop the turn-based combat
    }
    public void ResetBattleSceneAfterResult()
    {
        Destroy(_enemyInstance.gameObject);
        _enemyInstance = null;
        foreach (var hero in _spawnedHeroes.ToList())
        {
            Destroy(hero.gameObject);
            _spawnedHeroes.Remove(hero);
        }
        EventManager.InvokeOnTurnHudPanelState(false,true);
        EventManager.InvokeOnBattleInitiated(true);
        EventManager.InvokeOnResetBattleResult();
        _isHeroTurn = true;
        UpdateTurnText();
    }

    private void SaveBattleCount()
    {
        ES3.Save("BattleCount",_battleCount);
    }

    private void LoadBattleCount()
    {
        _battleCount = ES3.Load("BattleCount", _battleCount);
    }
}
