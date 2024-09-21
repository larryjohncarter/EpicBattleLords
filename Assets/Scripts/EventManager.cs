using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
   public static event Action<bool> OnBattleInitiated;
   public static void InvokeOnBattleInitiated(bool state) => OnBattleInitiated?.Invoke(state);

   public static event Action<bool> OnHeroSelected;
   public static void InvokeOnHeroSelected(bool state) => OnHeroSelected?.Invoke(state);

   public static event Action OnHeroSelectionMaxAmount;
   public static void InvokeOnHeroSelectionMaxAmount() => OnHeroSelectionMaxAmount?.Invoke();
}
