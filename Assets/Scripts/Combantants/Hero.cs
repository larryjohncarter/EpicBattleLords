using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Hero : Combantant
{
   [SerializeField] private Combantant_SO _combantantConfig;
   public Combantant_SO CombantantConfig => _combantantConfig;
   public int Level { get; protected set; }
   public int Experience { get; protected set; }

   public void GainXp(int xp)
   {
      Experience += xp;
      if (Experience >= 5)
      {
         LevelUp();
         Experience = 0;
      }
   }

   private void LevelUp()
   {
      Level++;
      Health *= 1.1f;
      AttackPower *= 1.1f;
      Debug.Log($"{Name } Leveled Up");
   }
}
