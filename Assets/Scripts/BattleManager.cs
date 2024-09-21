using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private List<Transform> _heroSpawnPoints = new();
    [SerializeField] private Transform _enemySpawnPoint;
    [SerializeField] private GameObject _enemyPrefab;
    private List<Hero> _selectedHeroes;

    void Start()
    {
        _selectedHeroes = HeroCollectionManager.Instance.SelectedHeroes;
        
    }

    public void SpawnCombatants()
    {
        for (var i = 0; i < _selectedHeroes.Count; i++)
        {
            if (i < _heroSpawnPoints.Count)
            {
                var heroInstance = Instantiate(_selectedHeroes[i].CombantantConfig.ModelPrefab,_heroSpawnPoints[i].position,Quaternion.identity);
            }
        }

        var enemy = Instantiate(_enemyPrefab, _enemySpawnPoint.position, Quaternion.identity);
        EventManager.InvokeOnBattleInitiated(false);
    }
}
