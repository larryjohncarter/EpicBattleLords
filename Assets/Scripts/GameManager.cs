using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonBehaviour<GameManager>
{
    [SerializeField] private GameSettings_SO _gameSettings;

    public GameSettings_SO GameSettings => _gameSettings;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
