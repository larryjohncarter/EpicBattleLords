using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroCollectionManager : MonoBehaviour
{
    [SerializeField] private List<Hero> _heroes = new();

    private const int maxHeroes = 10;

    private List<Hero> _selectedHeroes = new();

    
}
