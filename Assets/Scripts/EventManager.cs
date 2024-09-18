using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
   public static event Action<bool> OnBattleInitiated;
   public static void InvokeOnBattleInitiated(bool state) => OnBattleInitiated?.Invoke(state);
}
