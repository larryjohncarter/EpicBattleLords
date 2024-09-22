using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonBehaviour<GameManager>
{
    public GameStates GameStates { get; set; } = GameStates.MainMenu;
}

public enum GameStates
{
    MainMenu,
    Playing,
    BattleResult,
    HeroSelection
}
