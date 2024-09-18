using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private List<Transform> _heroSpawnPoints = new();

    private List<Hero> _selectedHeroes;

    void Start()
    {
        _selectedHeroes = HeroCollectionManager.Instance.SelectedHeroes;
        
    }

    public void SpawnHeroes()
    {
        for (int i = 0; i < _selectedHeroes.Count; i++)
        {
            if (i < _heroSpawnPoints.Count)
            {
                GameObject heroInstance = Instantiate(_selectedHeroes[i].CombantantConfig.ModelPrefab,_heroSpawnPoints[i].position,Quaternion.identity);
            }
        }
        EventManager.InvokeOnBattleInitiated(false);
    }
}
