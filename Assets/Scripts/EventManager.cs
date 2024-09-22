using System;

public static class EventManager
{
   public static event Action<bool> OnBattleInitiated;
   public static void InvokeOnBattleInitiated(bool state) => OnBattleInitiated?.Invoke(state);

   public static event Action<bool> OnHeroSelected;
   public static void InvokeOnHeroSelected(bool state) => OnHeroSelected?.Invoke(state);

   public static event Action OnHeroSelectionMaxAmount;
   public static void InvokeOnHeroSelectionMaxAmount() => OnHeroSelectionMaxAmount?.Invoke();

   public static event Action<bool> OnTurnChangeTextSet;
   public static void InvokeOnTurnChangeTextSet(bool isPlayerturn) => OnTurnChangeTextSet?.Invoke(isPlayerturn);

   public static event Action<bool,bool> OnTurnHudPanelState;
   public static void InvokeOnTurnHudPanelState(bool state, bool instant) => OnTurnHudPanelState?.Invoke(state,instant);

   public static event Action<bool> OnBattleEnd;
   public static void InvokeOnBattleEnd(bool state) => OnBattleEnd?.Invoke(state);
   
}
