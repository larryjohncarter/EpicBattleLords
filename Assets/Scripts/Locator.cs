using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locator : SingletonBehaviour<Locator>
{
    public GameSettings_SO GameSettings;
    public HeroPopUpController HeroPopUpController;
    public Camera MainCamera;
}
