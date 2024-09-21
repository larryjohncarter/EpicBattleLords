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
    void Start()
    {
        _selectedHeroes = HeroCollectionManager.Instance.SelectedHeroes;
        _battleFlowCoroutine = StartCoroutine(TurnBasedCombat());
    }

  

   

    public void SpawnCombatants()
    {
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
        _enemyInstance = enemy.GetComponent<EnemyHero>();
        EventManager.InvokeOnBattleInitiated(false);
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
            yield return new WaitUntil(() => _selectedHero != null && _isHeroTurn);
            yield return HeroAttack(_selectedHero);
            _selectedHero = null;
            _isHeroTurn = false;
            yield return EnemyAttack();
            _isHeroTurn = true;

            if (CheckForWinOrLose())
            {
                Debug.Log($"Hey");
                break;
            }

            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator HeroAttack(Hero hero)
    {
        Debug.Log($"{hero.CombantantConfig.Name} attacked the Enemy! :)");
        hero.Attack(_enemyInstance);
        yield return new WaitForSeconds(1f);
    }

    IEnumerator EnemyAttack()
    {
        Debug.Log($"Enemy attacks a  random selected hero");
        var aliveHeroes = _spawnedHeroes.FindAll(x => x.GetComponent<IHealthController>().IsAlive());
        if (aliveHeroes.Count > 0)
        {
            Hero targetHero = aliveHeroes[Random.Range(0, aliveHeroes.Count)];
            Debug.Log($"Enemy  Attacks: {targetHero.CombantantConfig.Name}");
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
            Debug.Log("Enemy has died! Heroes Win!");
            EndBattle(true);
            return true;
        }

        return allHeroesDefeated;
    }
    private void EndBattle(bool heroesWin)
    {
        if (heroesWin)
        {
            Debug.Log("Heroes have won the battle!");
            // Handle win condition (show UI, rewards, etc.)
        }
        else
        {
            Debug.Log("Heroes have lost the battle.");
            // Handle lose condition (show UI, game over, etc.)
        }

        StopCoroutine(_battleFlowCoroutine); // Stop the turn-based combat
    }
}
